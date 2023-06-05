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
    public partial class _ِAccounts_Information_Form : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        public _ِAccounts_Information_Form()
        {
            InitializeComponent();
            StreamReader streamReader = new StreamReader("info.txt");
            string server = streamReader.ReadLine();
            string Database = streamReader.ReadLine();
            con = new SqlConnection(@"Data Source=" + server + ";Initial Catalog=" + Database + ";Integrated Security=True;");
        }

        private void _ِAccounts_Information_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Accounts_Menu.This.Show();
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
            if(textBox1.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب اسم الشركة");
                return;
            }
            DialogResult a = MessageBox.Show("سيتم اضافة شركة جديدة",
                    "تاكيد ",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2,
                    MessageBoxOptions.RtlReading);
            if (a.ToString() != "OK")
                return;
            string Company_Name = textBox1.Text;
            con.Open();
            cmd = new SqlCommand($"Select * From Company Where Name = N'{Company_Name}'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                reader.Close();
                con.Close();
                ErrorMessageBox("هذة الشركة موجودة بالفعل");
                return;
            }
            reader.Close();
            cmd = new SqlCommand($"Insert Into Company Values(N'{Company_Name}')", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand($"Select Id From Company Where Name = N'{Company_Name}'", con);
            reader = cmd.ExecuteReader();
            reader.Read();
            string Id = reader["Id"].ToString();
            reader.Close();
            cmd = new SqlCommand($"Create Table Company{Id} \n" +
                                 "(                         \n" +
                                 "  Item nvarchar(200),     \n" +
                                 "  Price nvarchar(200)     \n" +
                                 ");", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand($"Create Table TranCompany{Id} \n" +
                                  "(                            \n" +
                                  "  Type      nvarchar(200),   \n" +
                                  "  InvoiceId nvarchar(200),   \n" +
                                  "  Number1   nvarchar(200),   \n" +
                                  "  Total     nvarchar(200),   \n" +
                                  "  Balance   nvarchar(200),   \n" +
                                  ");                           \n", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand($"Insert Into TranCompany{Id} Values(N'الاجمالي',N'',N'',N'',N'0')", con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show
            (
                "لقد تم اضافة الشركة الجديدة بنجاح",
                "نجح",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RtlReading
            );
            textBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب اسم الشركة");
                return;
            }
            if(textBox2.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب اسم العنصر الجديد");
                return;
            }
            if(textBox3.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب سعر العنصر الجديد");
                return;
            }
            string Comany_Name = textBox1.Text;
            string Item_Name = textBox2.Text;
            string Price = textBox3.Text;
            con.Open();
            cmd = new SqlCommand($"Select Id From Company Where Name = N'{Comany_Name}'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            string Id;
            if(reader.Read())
            {
                Id = reader["Id"].ToString();
            }
            else
            {
                reader.Close();
                con.Close();
                ErrorMessageBox("لا يوجد شركة بهذا الاسم");
                return;
            }
            reader.Close();
            cmd = new SqlCommand($"Select * From Company{Id} Where Item = N'{Item_Name}'", con);
            reader = cmd.ExecuteReader();
            if(reader.HasRows)
            {
                reader.Close();
                con.Close();
                ErrorMessageBox("هذا العنصر موجود بالفعل");
                return;
            }
            con.Close();
            DialogResult a = MessageBox.Show("سيتم اضافة العنصر جديدة",
                    "تاكيد ",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2,
                    MessageBoxOptions.RtlReading);
            if (a.ToString() != "OK")
                return;
            con.Open();
            cmd = new SqlCommand($"Insert Into Company{Id} Values (N'{Item_Name}', N'{Price}')", con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show
            (
                "لقد تم اضافة العنصر الجديدة بنجاح",
                "نجح",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RtlReading
            );
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                con.Open();
                cmd = new SqlCommand($"Select * From Company", con);
                SqlDataAdapter data = new SqlDataAdapter(cmd);
                DataTable dte = new DataTable();
                data.Fill(dte);
                dataGridView1.DataSource = dte;
                cmd.ExecuteNonQuery();
                con.Close();
                return;
            }
            string ComanyName = textBox1.Text;
            con.Open();
            cmd = new SqlCommand($"Select Id From Company Where Name = N'{ComanyName}'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            string Id;
            if(reader.Read())
            {
                Id = reader["Id"].ToString();
            }
            else
            {
                reader.Close();
                con.Close();
                ErrorMessageBox("لا يوجد شركة بهذا الاسم");
                return;
            }
            reader.Close();
            cmd = new SqlCommand($"Select * From Company{Id}",con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            cmd.ExecuteNonQuery();
            con.Close();
        }

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

        private Size OFormSize;
        private Rectangle OButton1;
        private Rectangle OButton2;
        private Rectangle OButton3;
        private Rectangle OButton4;
        private Rectangle OButton5;
        private Rectangle OButton6;
        private Rectangle OTextBox1;
        private Rectangle OTextBox2;
        private Rectangle OTextBox3;
        private Rectangle OTextBox4;
        private Rectangle OLable1;
        private Rectangle OLable2;
        private Rectangle OLable3;
        private Rectangle OLable4;
        private Rectangle OLable5;
        private Rectangle ODataGridView1;
        private void _ِAccounts_Information_Form_Load(object sender, EventArgs e)
        {
            OFormSize = this.Size;
            OButton1 = new Rectangle(button1.Location.X,button1.Location.Y,button1.Width,button1.Height);
            OButton2 = new Rectangle(button2.Location.X,button2.Location.Y,button2.Width,button2.Height);
            OButton3 = new Rectangle(button3.Location.X,button3.Location.Y,button3.Width,button3.Height);
            OButton4 = new Rectangle(button4.Location.X,button4.Location.Y,button4.Width,button4.Height);
            OButton5 = new Rectangle(button5.Location.X,button5.Location.Y,button5.Width,button5.Height);
            OButton6 = new Rectangle(button6.Location.X,button6.Location.Y,button6.Width,button6.Height);
            OTextBox1 = new Rectangle(textBox1.Location.X,textBox1.Location.Y,textBox1.Width,textBox1.Height);
            OTextBox2 = new Rectangle(textBox2.Location.X,textBox2.Location.Y,textBox2.Width,textBox2.Height);
            OTextBox3 = new Rectangle(textBox3.Location.X,textBox3.Location.Y,textBox3.Width,textBox3.Height);
            OTextBox4 = new Rectangle(textBox4.Location.X,textBox4.Location.Y,textBox4.Width,textBox4.Height);
            OLable1 = new Rectangle(label1.Location.X,label1.Location.Y,label1.Width,label1.Height);
            OLable2 = new Rectangle(label2.Location.X,label2.Location.Y,label2.Width,label2.Height);
            OLable3 = new Rectangle(label3.Location.X,label3.Location.Y,label3.Width,label3.Height);
            OLable4 = new Rectangle(label4.Location.X,label4.Location.Y,label4.Width,label4.Height);
            OLable5 = new Rectangle(label5.Location.X,label5.Location.Y,label5.Width,label5.Height);
            ODataGridView1 = new Rectangle(dataGridView1.Location.X, dataGridView1.Location.Y, dataGridView1.Width, dataGridView1.Height);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب اسم الشركة");
                return;
            }
            if(textBox2.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب اسم العنصر");
                return;
            }
            if(textBox4.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب الاسم الجديد");
                return;
            }
            string Comany_Name = textBox1.Text;
            string OldName = textBox2.Text;
            string NewName = textBox4.Text;
            con.Open();
            cmd = new SqlCommand($"Select Id From Company Where Name = N'{Comany_Name}'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            string Id;
            if (reader.Read())
            {
                Id = reader["Id"].ToString();
            }
            else
            {
                reader.Close();
                con.Close();
                ErrorMessageBox("لا يوجد شركة بهذا الاسم");
                return;
            }
            reader.Close();
            con.Close();
            DialogResult a = MessageBox.Show("سيتم تعديل اسم العنصر",
                    "تاكيد ",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2,
                    MessageBoxOptions.RtlReading) ;
            if (a.ToString() != "OK")
                return;
            con.Open();
            cmd = new SqlCommand($"Update Company{Id} Set Item = N'{NewName}' Where Item = N'{OldName}'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show
            (
                "لقد تم تعديل اسم العنصر",
                "نجح",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RtlReading
            );
            textBox2.Text = "";
            textBox4.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب اسم الشركة");
                return;
            }
            if (textBox2.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب اسم العنصر");
                return;
            }
            if(textBox3.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب السعر الجديد");
                return;
            }
            string CompanyName = textBox1.Text;
            string ItemName  =textBox2.Text;
            string NewPrice = textBox3.Text;
            con.Open();
            cmd = new SqlCommand($"Select Id From Company Where Name = N'{CompanyName}'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            string Id;
            if (reader.Read())
            {
                Id = reader["Id"].ToString();
            }
            else
            {
                reader.Close();
                con.Close();
                ErrorMessageBox("لا يوجد شركة بهذا الاسم");
                return;
            }
            reader.Close();
            con.Close();
            DialogResult a = MessageBox.Show("سيتم تعديل سعر العنصر",
                    "تاكيد ",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2,
                    MessageBoxOptions.RtlReading);
            if (a.ToString() != "OK")
                return;
            con.Open();
            cmd = new SqlCommand($"Update Company{Id} Set Price = N'{NewPrice}' Where Item = N'{ItemName}'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show
            (
                "لقد تم تعديل سعر العنصر",
                "نجح",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RtlReading
            );
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب اسم الشركة");
                return;
            }
            if(textBox4.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب الاسم الجديد للشركة");
                return ;
            }
            string OldName = textBox1.Text;
            string NewName = textBox4.Text;
            DialogResult a = MessageBox.Show("سيتم تعديل اسم الشركة",
                    "تاكيد ",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2,
                    MessageBoxOptions.RtlReading);
            if (a.ToString() != "OK")
                return;
            con.Open();
            cmd = new SqlCommand($"Update Company Set Name = N'{NewName}' Where Name = N'{OldName}'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show
            (
                "لقد تم تعديل اسم الشركة",
                "نجح",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RtlReading
            );
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar)) e.Handled = true;

        }

        private void _ِAccounts_Information_Form_Resize(object sender, EventArgs e)
        {
            ResizeControls(OLable1, label1);
            ResizeControls(OLable2, label2);
            ResizeControls(OLable3, label3);
            ResizeControls(OLable4, label4);
            ResizeControls(OLable5, label5);
            ResizeControls(OButton1, button1);
            ResizeControls(OButton2, button2);
            ResizeControls(OButton3, button3);
            ResizeControls(OButton4, button4);
            ResizeControls(OButton5, button5);
            ResizeControls(OButton6, button6);
            ResizeControls(OTextBox1, textBox1);
            ResizeControls(OTextBox2, textBox2);
            ResizeControls(OTextBox3, textBox3);
            ResizeControls(OTextBox4, textBox4);
            ResizeControls(ODataGridView1, dataGridView1);
        }
    }
}
