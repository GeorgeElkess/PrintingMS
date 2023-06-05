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
    public partial class Accounts_Menu : Form
    {
        public static Accounts_Menu This;
        public Accounts_Menu()
        {
            InitializeComponent();
            This = this;
        }

        private void Accounts_Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Menu.This.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _ِAccounts_Information_Form a = new _ِAccounts_Information_Form();
            a.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            New_Invoice new_Invoice = new New_Invoice();
            new_Invoice.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Invoices_Search invoices_Search = new Invoices_Search();
            invoices_Search.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }
        Size OFormSize;
        private void ResizeControls(Rectangle r, Control c)
        {
            float xRatio = (float)(this.Size.Width) / (float)(OFormSize.Width);
            float yRatio = (float)(this.Height) / (float)(OFormSize.Height);

            int newX = (int)(r.X * xRatio);
            int newY = (int)(r.Y * yRatio);

            int newWidth = (int)(r.Width * xRatio);
            int newHeight = (int)(r.Height * yRatio);

            c.Location = new Point(newX, newY);
            c.Size = new Size(newWidth, newHeight);
        }
        Rectangle Obutton1;
        Rectangle Obutton2;
        Rectangle Obutton3;
        Rectangle Obutton4;
        private void Accounts_Menu_Load(object sender, EventArgs e)
        {
            OFormSize = this.Size;
            Obutton1 = new Rectangle(button1.Location, button1.Size);
            Obutton2 = new Rectangle(button2.Location, button2.Size);
            Obutton3 = new Rectangle(button3.Location, button3.Size);
            Obutton4 = new Rectangle(button4.Location, button4.Size);
        }

        private void Accounts_Menu_Resize(object sender, EventArgs e)
        {
            ResizeControls(Obutton1, button1);
            ResizeControls(Obutton2, button2);
            ResizeControls(Obutton3, button3);
            ResizeControls(Obutton4, button4);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Account_Payable_From_Companys account_Payable_From_Companys = new Account_Payable_From_Companys();
            account_Payable_From_Companys.Show();
            this.Hide();
        }
    }
}
