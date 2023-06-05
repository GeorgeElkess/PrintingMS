using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SetUp_Remon_Application
{
    public partial class Menu : Form
    {
        public static Menu This;
        public static string User_Name;
        private Size OFormSize;
        private Rectangle OBtn1;
        private Rectangle OBtn2;
        private Rectangle OBtn3;
        private Rectangle OBtn4;
        private Rectangle OBtn5;
        public Menu()
        {
            InitializeComponent();
            This = this;
        }

        private void ResizeControls(Rectangle r,Control c)
        {
            float xRatio = (float)(this.Width) / (float)(OFormSize.Width);
            float yRatio = (float)(this.Height) / (float)(OFormSize.Height);

            int newX = (int)(r.X * xRatio);
            int newY = (int)(r.Y * yRatio);

            int newWidth = (int)(r.Width * xRatio);
            int newHeight = (int)(r.Height * yRatio);

            c.Location = new Point(newX, newY);
            c.Size = new Size(newWidth, newHeight);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Paper_Form paper = new Paper_Form();
            this.Hide();
            paper.ShowDialog();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            OFormSize = this.Size;
            OBtn1 = new Rectangle(button1.Location.X,button1.Location.Y, button1.Width,button1.Height);
            OBtn3 = new Rectangle(button3.Location.X,button3.Location.Y, button3.Width,button3.Height);
            OBtn4 = new Rectangle(button4.Location.X,button4.Location.Y, button4.Width,button4.Height);
            OBtn5 = new Rectangle(button5.Location.X,button5.Location.Y, button5.Width,button5.Height);
            OBtn2 = new Rectangle(button2.Location.X,button2.Location.Y, button2.Width,button2.Height);
        }

        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _ٍSign_Up signUp = new _ٍSign_Up();
            this.Hide();
            signUp.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _ٍSolofan_Form Solofan = new _ٍSolofan_Form();
            this.Hide();
            Solofan.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Oil oil = new Oil();
            this.Hide();
            oil.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
             Accounts_Menu accountsMenu = new Accounts_Menu();
            this.Hide();
            accountsMenu.ShowDialog();
        }

        private void Menu_Resize(object sender, EventArgs e)
        {
            ResizeControls(OBtn1, button1);
            ResizeControls(OBtn3, button3);
            ResizeControls(OBtn4, button4);
            ResizeControls(OBtn5, button5);
            ResizeControls(OBtn2, button2);
        }
    }
}
