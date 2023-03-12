using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWorkRebuild
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {

        }

        public void initValues(Double T, Double a, int BuildingCount)
        {
            this.textBox1.Text = T.ToString();
            this.textBox2.Text = a.ToString();
            this.textBox3.Text = BuildingCount.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //updateTAndAlphaValues(this.textBox1, this.textBox2, this.textBox3);
        }
    }
}
