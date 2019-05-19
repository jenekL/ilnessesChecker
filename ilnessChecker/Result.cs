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
    public partial class Result : Form
    {
        Dictionary<string, double> coefs = new Dictionary<string, double>();

        public Result(Dictionary<string, double> coefs)
        {
            InitializeComponent();
            this.AutoSize = true;
            this.coefs = coefs;
            int x = 10, y = 10;
            // coefs.Reverse();
            coefs.GroupBy(r => r.Value);
            foreach(KeyValuePair<string, double> k in coefs)
            {
                if (k.Value >= 0.5)
                {
                    Label label = new Label();
                    label.Text = k.Key + " " + k.Value;
                    label.AutoSize = true;
                    label.Location = new System.Drawing.Point(x, y);
                    y += 20;
                    this.Controls.Add(label);
                }
               
            }
        }

        public Dictionary<string, double> Coefs { get => coefs; set => coefs = value; }

        private void Result_Load(object sender, EventArgs e)
        {

        }
    }
}
