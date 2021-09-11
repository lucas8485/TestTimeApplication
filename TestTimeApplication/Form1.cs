using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace TestTimeApplication
{
    public partial class Form1 : Form
    {
        private List<System.Windows.Forms.Control> ContainControls;
        public ColorChooseEventHandler h;
        Form2 color_form;
        Graphics g;
        Point posStart, posEnd;
        int choice_graph;
        int pen_width;
        Color pen_color;
        Thread oThread;
        int old_x, old_y, new_x, new_y;
        Image image;
        enum pen_type
        {
            cursor,
            pen,
            eraser
        };
        public Form1()
        {
            InitializeComponent();
            ContainControls = new List<Control> {label1,label2,label3,label4,label5,pictureBox1,button1,button2,button3,button4,button5,numericUpDown1,groupBox1,groupBox2};
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //g = this.pictureBox1.CreateGraphics();
            image = new Bitmap(this.pictureBox1.Width,this.pictureBox1.Height);
            this.pictureBox1.Image = image;
            g = Graphics.FromImage(image);
            choice_graph = (int)pen_type.cursor;
            pen_width = 3;
            pen_color = Color.Red;
            oThread = new Thread(new ThreadStart(TimeChangeInvoker));
            oThread.Start();
            old_x = this.Size.Width;
            old_y = this.Size.Height;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            choice_graph = (int)pen_type.cursor;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            choice_graph = (int)pen_type.pen;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            choice_graph = (int)pen_type.eraser;
        }

        private void pictureBox1_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (choice_graph)
                {
                    case (int)pen_type.pen:
                        Pen pen1 = new Pen(pen_color, pen_width);
                        posEnd.X = e.X;
                        posEnd.Y = e.Y;
                        g.DrawLine(pen1, posStart, posEnd);
                        posStart = posEnd;
                        this.pictureBox1.Image = image;
                        break;
                    case (int)pen_type.eraser:
                        Pen pen2 = new Pen(Color.White, 15);
                        posEnd.X = e.X;
                        posEnd.Y = e.Y;
                        g.DrawLine(pen2, posStart, posEnd);
                        posStart = posEnd;
                        this.pictureBox1.Image = image;
                        break;
                    default:
                        break;
                }
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                posStart.X = e.X;
                posStart.Y = e.Y;
            }
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            pen_width = (int)numericUpDown1.Value;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            image  = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            this.pictureBox1.Image = image;
            g = Graphics.FromImage(image);
            choice_graph = (int)pen_type.cursor;
            button1.Focus();
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 abtFrame = new AboutBox1();
            abtFrame.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (color_form == null)
            {
                color_form = new Form2();
                color_form.TopMost = true;
                color_form.color_choose += new ColorChooseEventHandler(fuck_color);
                color_form.Show();
            }
        }
        private Color fuck_color(object sender, MyEventArgs p)
        {
            color_form.Close();
            GC.Collect();
            color_form = null;
            this.pen_color = p.my_color;
            return p.my_color;
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void InvokeTimeChanger()
        {
            TimeGeter t = new TimeGeter();
            label1.Text = t.GetTime();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            oThread.Abort();
            oThread.Join();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            new_x = this.Size.Width;
            new_y = this.Size.Height;
            double Bili_X = (double)new_x / (double)old_x;
            double Bili_Y = (double)new_y / (double)old_y;
            foreach(System.Windows.Forms.Control kongjian in this.ContainControls)
            {
                Point new_point=kongjian.Location;
                double x = new_point.X * Bili_X;
                double y = new_point.Y * Bili_Y;
                int iX = (int)x;
                int iY = (int)y;
                kongjian.Location = new Point(iX,iY);
                Size new_size = kongjian.Size;
                x = new_size.Width  * Bili_X;
                y = new_size.Height * Bili_Y;
                iX = (int)x;
                iY = (int)y;
                kongjian.Size = new Size(iX, iY);
            }
            string tempName = Path.GetTempFileName();
            image.Save(tempName);
            image = new Bitmap(pictureBox1.Width,pictureBox1.Height);
            Image newImage = Image.FromFile(tempName);
            g = Graphics.FromImage(image);
            g.DrawImage(newImage, 0, 0);
            pictureBox1.Image = image;
            old_x = new_x;
            old_y = new_y;
        }

        private void TimeChangeInvoker()
        {
            MethodInvoker mi = new MethodInvoker(this.InvokeTimeChanger);
            while (true)
            {
                this.BeginInvoke(mi);
                DateTime now = DateTime.Now;
                while (now.AddSeconds(1) > DateTime.Now) { }
            }
        }
    }
}