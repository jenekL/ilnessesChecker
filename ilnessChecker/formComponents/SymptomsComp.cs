using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ilnessChecker.formComponents
{
    class SymptomsComp
    {
        private int id;
        private CheckBox checkbox;
        private TextBox confidence;
        private TextBox distrust;
        private Button button;

        public SymptomsComp(CheckBox checkbox, TextBox confidence, TextBox distrust, int id, Button button)
        {
            this.checkbox = checkbox;
            this.confidence = confidence;
            this.distrust = distrust;
            this.id = id;
            this.button = button;

            this.confidence.ReadOnly = true;
            this.distrust.ReadOnly = true;

            this.checkbox.Click += Cb_CLick;
            this.checkbox.CheckedChanged += Cb_CheckedChanged;

        }

        private void Cb_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbox.Checked)
            {
                this.confidence.ReadOnly = false;
                this.distrust.ReadOnly = false;
            }
        }

        private void Cb_CLick(object sender, EventArgs e)
        {
            if (checkbox.Checked)
            {
                this.confidence.ReadOnly = false;
                this.distrust.ReadOnly = false;
            }
            else
            {
                this.confidence.ReadOnly = true;
                this.distrust.ReadOnly = true;
            }
        }

        public bool isChecked()
        {
            return checkbox.Checked;
        }

        public CheckBox Checkbox { get => checkbox; set => checkbox = value; }
        public TextBox Confidence { get => confidence; set => confidence = value; }
        public TextBox Distrust { get => distrust; set => distrust = value; }
        public int Id { get => id; set => id = value; }
        public Button Button { get => button; set => button = value; }
    }
}
