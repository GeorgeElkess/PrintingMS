using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SetUp_Remon_Application
{
    public partial class _ٍSign_Up : Form
    {
        public static _ٍSign_Up This;
        SqlConnection con;
        SqlCommand cmd;
        public _ٍSign_Up()
        {
            InitializeComponent();
            This = this;
            StreamReader streamReader = new StreamReader("info.txt");
            string server = streamReader.ReadLine();
            string Database = streamReader.ReadLine();
            con = new SqlConnection(@"Data Source=" + server + ";Initial Catalog=" + Database + ";Integrated Security=True;");
        }
        public void ErrorMessageBox(string x)
        {
            MessageBox.Show(x,
                            "خطأ",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.RtlReading);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب الاسم");
                return;
            }
            if (textBox2.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب كلمة السر");
                return;
            }
            string Name = textBox1.Text;
            string Password = textBox2.Text;
            con.Open();
            cmd = new SqlCommand("Select * From Password Where Name = N'" + Name + "'", con);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                ErrorMessageBox("الاسم متكرر\n يجب ان تكتب اسم مختلف");
                rdr.Close();
                con.Close();
                return;
            }
            rdr.Close();
            cmd = new SqlCommand("Select * From Password Where code = N'" + Password + "'", con);
            rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                ErrorMessageBox("كلمة السر متكرر\n يجب ان تكتب كلمة سر مختلف");
                rdr.Close();
                con.Close();
                return;
            }
            rdr.Close();
            con.Close();
            DialogResult a = MessageBox.Show("سيتم اضافة عميل جديد ",
                    "تاكيد ",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2,
                    MessageBoxOptions.RtlReading);
            if (a.ToString() != "OK")
                return;
            con.Open();
            cmd = new SqlCommand("Insert Into Password Values (N'" + Name + "',N'" + Password + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show
            (
                "لقد تم اضافة العميل الجديد بنجاح",
                "نجح",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RtlReading
            );
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }
        private void _ٍSign_Up_FormClosing(object sender, FormClosingEventArgs e)
        {
            Menu.This.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب الاسم");
                return;
            }
            if (textBox2.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب كلمة السر القديمة");
                return;
            }
            if (textBox3.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب كلمة السر الجديدة");
                return;
            }
            string Name = textBox1.Text;
            string Old_Password = textBox2.Text;
            string New_Password = textBox3.Text;
            con.Open();
            cmd = new SqlCommand("Select * From Password Where code = N'" + Old_Password + "' and Name = N'" + Name + "'", con);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (!rdr.HasRows)
            {
                ErrorMessageBox("الاسم او كلمة السر خطء");
                rdr.Close();
                con.Close();
                return;
            }
            rdr.Close();
            con.Close();
            DialogResult a = MessageBox.Show("سيتم تعديل كلمة السر ",
                    "تاكيد ",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2,
                    MessageBoxOptions.RtlReading);
            if (a.ToString() != "OK")
                return;
            con.Open();
            cmd = new SqlCommand("Update Password Set code = N'" + New_Password + "' Where code = N'" + Old_Password + "' and Name = N'" + Name + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show
            (
                "لقد تم تعديل كلمة السر بنجاح",
                "نجح",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RtlReading
            );
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
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
        Rectangle OLable1;
        Rectangle OLable2;
        Rectangle OLable3;
        Rectangle OLable4;
        Rectangle OTextBox1;
        Rectangle OTextBox2;
        Rectangle OTextBox3;
        Rectangle OButton1;
        Rectangle OButton2;

        private void _ٍSign_Up_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            OFormSize = this.Size;
            OLable1 = new Rectangle(label1.Location.X, label1.Location.Y, label1.Width, label1.Height);
            OLable2 = new Rectangle(label2.Location.X, label2.Location.Y, label2.Width, label2.Height);
            OLable3 = new Rectangle(label3.Location.X, label3.Location.Y, label3.Width, label3.Height);
            OLable4 = new Rectangle(label4.Location.X, label4.Location.Y, label4.Width, label4.Height);
            OButton1 = new Rectangle(button1.Location.X,button1.Location.Y,button1.Width,button1.Height);
            OButton2 = new Rectangle(button2.Location.X,button2.Location.Y,button2.Width,button2.Height);
            OTextBox1 = new Rectangle(textBox1.Location.X,textBox1.Location.Y,textBox1.Width,textBox1.Height);
            OTextBox2 = new Rectangle(textBox2.Location.X,textBox2.Location.Y,textBox2.Width,textBox2.Height);
            OTextBox3 = new Rectangle(textBox3.Location.X,textBox3.Location.Y,textBox3.Width,textBox3.Height);
        }

        private void _ٍSign_Up_Resize(object sender, EventArgs e)
        {
            ResizeControls(OLable1, label1);
            ResizeControls(OLable2, label2);
            ResizeControls(OLable3, label3);
            ResizeControls(OLable4, label4);
            ResizeControls(OButton1, button1);
            ResizeControls(OButton2, button2);
            ResizeControls(OTextBox1 , textBox1);
            ResizeControls(OTextBox2 , textBox2);
            ResizeControls(OTextBox3 , textBox3);
        }
    }
}
