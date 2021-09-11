using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestTimeApplication
{
    public partial class Form2 : Form
    {
        public event ColorChooseEventHandler color_choose;
        private Color return_type;
        public Form2()
        {
            InitializeComponent();
        }
        private void OnColorChoose(Color e) 
        {
            color_choose(this, new MyEventArgs(return_type));
        }
        private void button1_Click(object sender, EventArgs e)
        {
            return_type=Color.Red;
            OnColorChoose(return_type);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            return_type = Color.DodgerBlue;
            OnColorChoose(return_type);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            return_type = Color.Black;
            OnColorChoose(return_type);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }
    }
}
