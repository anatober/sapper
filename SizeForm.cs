using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace saper
{
    public partial class SizeForm : Form
    {
        int difficulty;
        public SizeForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked || radioButton2.Checked || radioButton3.Checked)
            {
                GameForm newGameForm = new GameForm(Convert.ToInt32(textBox1.Text), difficulty);
                newGameForm.Show();
                this.Hide();
            }
            else
                MessageBox.Show("Choose difficulty!");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            difficulty = 7;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            difficulty = 5;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            difficulty = 3;
        }
    }
}
