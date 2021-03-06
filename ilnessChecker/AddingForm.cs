﻿using ilnessChecker.entities;
using ilnessChecker.formComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ilnessChecker
{
    public partial class AddingForm : Form
    {
        List<SymptomsComp> symptomsComps = new List<SymptomsComp>();
        List<Label> diseases = new List<Label>();
        List<Button> diseaseButtons = new List<Button>();
        TextBox diseaseText;
        TextBox symptomText;
        Button addDiseaseButton;
        public AddingForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            addDiseaseButton = new Button();
            addDiseaseButton.Text = "Добавить болезнь";
            addDiseaseButton.Click += setAddButton_Click;
            this.Controls.Add(addDiseaseButton);

            diseaseText = new TextBox();
            diseaseText.Location = new System.Drawing.Point(50, 50);
            this.Controls.Add(diseaseText);


        }

        private void AddingForm_Load(object sender, EventArgs e)
        {
            setDiseaseAdd();
            setSymptomsAdd();
            setDiseaseList();
        }

        private void setSymptomsAdd()
        {
            int x = this.Width / 2;
            int y = 50;
            Label naming = new Label();
            naming.Text = "Добавить симптом:";
            naming.AutoSize = true;
            naming.Location = new System.Drawing.Point(x, y);
            this.Controls.Add(naming);
            y += 30;

            symptomText = new TextBox();
            symptomText.Location = new System.Drawing.Point(x, y);
            Button addSympbutton = new Button();
            addSympbutton.Location = new System.Drawing.Point(x, y + 20);
            addSympbutton.Text = "Добавить симптом";
            addSympbutton.Click += (object sender, EventArgs e) =>
            {
                if (symptomText.Text != "")
                {
                    try
                    {
                        DBAPI.SaveSymptom(new SymptomEntity(DBAPI.getMAXSymptomID() + 1, symptomText.Text));
                        clearDiseaseAdd();
                        setDiseaseAdd();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            };

            Controls.AddRange(new System.Windows.Forms.Control[] { symptomText, addSympbutton });
        }

        private void clearDiseaseAdd()
        {
            foreach(SymptomsComp s in symptomsComps)
            {
                s.Checkbox.Checked = false;
                s.Confidence.Text = "";
                s.Distrust.Text = "";

                this.Controls.Remove(s.Checkbox);
                this.Controls.Remove(s.Confidence);
                this.Controls.Remove(s.Distrust);
                this.Controls.Remove(s.Button);
            }
           // this.Controls.Remove(addDiseaseButton);
           // addDiseaseButton.Click -= setAddButton_Click;
           // addDiseaseButton.Click += setAddButton_Click;
            diseaseText.Text = "";
            
        }
        private void clearDiseaseList()
        {
            foreach(Label label in diseases)
            {
                this.Controls.Remove(label);
            }
            foreach(Button b in diseaseButtons)
            {
                Controls.Remove(b);
            }
        }

        private bool checkVxojdenie()
        {
            var diseases = DBAPI.LoadDiseases();

            var comps = symptomsComps.GroupBy(k => k.isChecked()).ToList();

            foreach (DiseaseEntity d in diseases)
            {
                var matchesids = DBAPI.LoadMatchesByDisease(d.Id);
                var sympts = matchesids.Select(k => k.Symptoms_id).ToList();

                int count = 0;

                foreach (SymptomsComp m in comps[0])
                {
                    foreach(MatchesEntity match in matchesids)
                    {
                        if (m.Id == match.Symptoms_id)
                        {
                            count++;
                        }
                    }
                   
                }

                if (comps[0].Count() == count && comps[0].Count() < matchesids.Count)
                {
                    MessageBox.Show("Содержится");
                    System.Console.WriteLine("Содержится");
                    return false;
                }
                if (comps[0].Count() == count && comps[0].Count() == matchesids.Count)
                {
                    MessageBox.Show("Существует");
                    System.Console.WriteLine("Существует");
                    return false;
                }
                if (matchesids.Count == count && comps[0].Count() > matchesids.Count && matchesids != null)
                {
                    MessageBox.Show("Содержит");
                    System.Console.WriteLine("Содержит");
                    return false;
                }
            }

            return true;
        }
       
        private void setDiseaseList()
        {
            var list = DBAPI.LoadDiseases();
            int x = this.Width - 400;
            int y = 50;
            Label naming = new Label();
            naming.Text = "Список болезней:";
            naming.AutoSize = true;
            naming.Location = new System.Drawing.Point(x, y);
            this.Controls.Add(naming);
            y += 30;

            foreach (DiseaseEntity d in list)
            {
                Label name = new Label();
                Button delete = new Button();
                delete.Location = new System.Drawing.Point(x - 30, y);
                delete.AutoSize = true;
                delete.Text = "X";
                delete.Width = 20;
                delete.Click += (object sender, EventArgs e) =>
                {
                    DBAPI.DeleteDisease(d);
                    //this.Controls.Remove(delete);
                   
                    clearDiseaseList();
                    setDiseaseList();
                };

                name.Text = d.Disease;
                name.AutoSize = true;
              
                name.Click += (object sender, EventArgs e) =>
                {
                    var matches = DBAPI.LoadMatchesByDisease(d.Id);
                    StringBuilder s = new StringBuilder();
                    foreach(MatchesEntity m in matches)
                    {
                        s.Append(DBAPI.getSymptomByID(m.Symptoms_id).Symptom + " [" + m.Confidence_measure + " : " + m.Distrust_measure +  "]\n");
                    }
                    MessageBox.Show(s.ToString());
                };
                name.Location = new System.Drawing.Point(x, y);
                y += 30;
                diseases.Add(name);
                diseaseButtons.Add(delete);
                this.Controls.Add(name);
                this.Controls.Add(delete);
            }
        }
        private void setDiseaseAdd()
        {
            symptomsComps.Clear();
            int x = 50, y = 90;
            try
            {

                var symptoms = DBAPI.LoadSymptoms();

                foreach (SymptomEntity s in symptoms)
                {
                    CheckBox cb = new CheckBox();
                    TextBox conf = new TextBox();
                    TextBox distr = new TextBox();
                    Button delbutton = new Button();

                    cb.Location = new System.Drawing.Point(x, y);
                    //label.Location = new System.Drawing.Point(x + 20, y);
                    cb.Text = s.Symptom;
                    cb.AutoSize = true;
                    //label.AutoSize = true;
                    conf.Location = new System.Drawing.Point(x + 220, y);
                    conf.Width = 50;
                    distr.Location = new System.Drawing.Point(x + 280, y);
                    distr.Width = 50;

                    distr.Validating += (object sender, CancelEventArgs e) =>
                    {
                        // if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                        // {
                        double d1 = -1, d2 = -1;
                        double.TryParse(distr.Text, out d1);
                        double.TryParse(conf.Text, out d2);
                            if (d1 > d2 && d1 != -1 && d2 != -1)
                            {
                                MessageBox.Show("Значение должно быть меньше меры доверия");
                            }
                            else
                            {
                                if (d1 <= 0)
                                {
                                    MessageBox.Show("Значение должно быть положительным");
                                }
                            }
                      //  }
                    };
                    conf.Validating += (object sender, CancelEventArgs e) =>
                    {
                        double d1 = -1;
                        double.TryParse(conf.Text, out d1);
                            if ((d1 <= 0 || d1 > 1) && d1 != -1)
                            {
                                MessageBox.Show("Значение должно быть в диапазоне от 0 до 1");
                            }
                    };


                 
                    delbutton.Location = new System.Drawing.Point(x + 340, y);
                    delbutton.Text = "X";
                    delbutton.AutoSize = true;
                    delbutton.Click += (object sender, EventArgs e) =>
                    {
                        DBAPI.DeleteSymptom(s);
                        clearDiseaseAdd();
                        setDiseaseAdd();
                    };

                    this.Controls.AddRange(new System.Windows.Forms.Control[] { cb, conf, distr, delbutton });
                    symptomsComps.Add(new SymptomsComp(cb, conf, distr, s.Id, delbutton));

                    y += 30;
                }
                addDiseaseButton.Location = new System.Drawing.Point(x, y + 20);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void setAddButton_Click(object sender, EventArgs e)
        {
            if (diseaseText.Text != "")
            {
                if (checkVxojdenie())
                {
                    DiseaseEntity diseaseEntity = new DiseaseEntity(DBAPI.getMAXDiseaseID() + 1, diseaseText.Text);
                    DBAPI.SaveDisease(diseaseEntity);
                    foreach (SymptomsComp s in symptomsComps)
                    {
                        if (s.isChecked())
                        {
                            try
                            {
                                DBAPI.SaveMatch(new MatchesEntity(diseaseEntity.Id,
                                    s.Id, Double.Parse(s.Confidence.Text), Double.Parse(s.Distrust.Text)));
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }
                    }
                    clearDiseaseAdd();
                    setDiseaseAdd();
                    clearDiseaseList();
                    setDiseaseList();
                }
                else
                {
                    MessageBox.Show("Так нельзя");
                }

            }
        }

    }
}
