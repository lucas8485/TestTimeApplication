using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace TestTimeApplication
{
    public class MyEventArgs : EventArgs
    {
        private Color _my_color;
        public MyEventArgs():base()
        {
            this._my_color = Color.White;
        }
        public MyEventArgs(Color _this_color)
            : base()
        {
            this._my_color = _this_color;
        }
        public Color my_color
        {
            get
            { return _my_color;  }
        }
    }
    public delegate Color ColorChooseEventHandler(object from, MyEventArgs e);
    public class TimeGeter
    {
        public string GetTime()
        {
            if (DateTime.Now.Hour <= 9)
            {
                return "0" + DateTime.Now.ToLongTimeString();
            }else return DateTime.Now.ToLongTimeString();
        }
    }
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
