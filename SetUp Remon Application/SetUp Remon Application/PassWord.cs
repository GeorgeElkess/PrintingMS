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
    
    public partial class PassWord : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        public PassWord()
        {
            InitializeComponent();
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

        public void function()
        {
            StreamReader streamReader = new StreamReader("info.txt");
            string server = streamReader.ReadLine();
            string Database = streamReader.ReadLine();
            con = new SqlConnection(@"Data Source=" + server + ";Initial Catalog=" + Database + ";Integrated Security=True;");

            if (textBox1.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب كلمة السر");
                return;
            }
            con.Open();
            string PassWord = textBox1.Text;
            cmd = new SqlCommand("Select Name From Password Where code = N'" + PassWord + "'", con);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                rdr.Read();
                Menu.User_Name = (string)rdr["Name"];
                Menu menu = new Menu();
                this.Hide();
                menu.ShowDialog();
            }
            else
            {
                ErrorMessageBox("كلمة السر خطء\nحاول مرة اخرة");
            }
            rdr.Close();
            con.Close();
            textBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            function();
        }

        private void PassWord_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void PassWord_Load(object sender, EventArgs e)
        {
            textBox1.PasswordChar = '*';
            try
            {
                StreamReader streamReader = new StreamReader("info.txt");
                if (string.IsNullOrEmpty(streamReader.ReadLine()))
                {
                    Form1 form1 = new Form1();
                    this.Hide();
                    form1.ShowDialog();
                }
            }
            catch (Exception)
            {
                Form1 form1 = new Form1();
                this.Hide();
                form1.ShowDialog();
                return;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                function();
            }
        }
    }
}
