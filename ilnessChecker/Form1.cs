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
    public partial class Form1 : Form
    {
        AddingForm addingForm;
        TestForm testForm;
        public Form1()
        {
            InitializeComponent();

            var dis = DBAPI.LoadDiseases();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addingForm = new AddingForm();
            addingForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            testForm = new TestForm();
            testForm.Show();
        }
    }
}
