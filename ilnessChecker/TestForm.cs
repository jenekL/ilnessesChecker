using ilnessChecker.entities;
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
    public partial class TestForm : Form
    {
        List<SymptomsComp> symptomsComps = new List<SymptomsComp>();
        List<MatchesEntity> matches = new List<MatchesEntity>();
        List<SymptomEntity> symptoms = new List<SymptomEntity>();
        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            setTestList();
        }

      
        private void setTestList()
        {
            int x = 50, y = 50;

            Button test = new Button();
            test.Location = new System.Drawing.Point(x, y);
            test.Click += (object sender, EventArgs e) =>
            {
                foreach (SymptomsComp s in symptomsComps)
                {
                    if (s.isChecked())
                    {
                        this.symptoms.Add(new SymptomEntity(s.Id, s.Checkbox.Text));
                    }
                }
                var diseases = DBAPI.LoadDiseases();
                foreach (DiseaseEntity d in diseases) {
                    var matches = DBAPI.LoadMatchesByDisease(d.Id);

                }
            };
            y += 40;
         
            var symptoms = DBAPI.LoadSymptoms();

            foreach (SymptomEntity s in symptoms)
            {
                CheckBox cb = new CheckBox();

                cb.Location = new System.Drawing.Point(x, y);
                //label.Location = new System.Drawing.Point(x + 20, y);
                cb.Text = s.Symptom;
                cb.AutoSize = true;
                //label.AutoSize = true;

                this.Controls.AddRange(new System.Windows.Forms.Control[] { cb});
                symptomsComps.Add(new SymptomsComp(cb, new TextBox(), new TextBox(), s.Id, new Button()));

                y += 30;
            }

        }
    }
}
