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
    public partial class New_Invoice : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        public New_Invoice()
        {
            InitializeComponent();
            StreamReader streamReader = new StreamReader("info.txt");
            string server = streamReader.ReadLine();
            string Database = streamReader.ReadLine();
            con = new SqlConnection(@"Data Source=" + server + ";Initial Catalog=" + Database + ";Integrated Security=True;");
        }
        private int Temp = 0;

        public string GetFormatedDate(string Date)
        {
            string date = "";
            int c = 0;
            int pos = 0;
            int pos2 = 0;
            int pos3 = 0;
            for (int i = 0; i < Date.Length; i++)
            {
                if (Date[i] == ' ')
                {
                    c++;
                    if (c == 1) pos2 = i;
                    if (c == 2) pos = i;
                    if (c == 3) pos3 = i;
                }
            }
            pos++;
            for (int i = pos; i < Date.Length; i++)
            {
                if (Date[i] == ',') break;
                date += Date[i];
            }
            date += '/';
            if (Date.Contains("January"))
            {
                date += "1";
            }
            else if (Date.Contains("February")) date += "2";
            else if (Date.Contains("March")) date += "3";
            else if (Date.Contains("April")) date += "4";
            else if (Date.Contains("May")) date += "5";
            else if (Date.Contains("June")) date += "6";
            else if (Date.Contains("July")) date += "7";
            else if (Date.Contains("August")) date += "8";
            else if (Date.Contains("September")) date += "9";
            else if (Date.Contains("October")) date += "10";
            else if (Date.Contains("November")) date += "11";
            else if (Date.Contains("December")) date += "12";
            date += '/';
            for (int i = pos3 + 1; i < Date.Length; i++)
            {
                date += Date[i];
            }
            return date;
        }

        private void New_Invoice_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Temp == 0 && comboBox1.SelectedIndex > -1)
            {
                DialogResult a = MessageBox.Show("سيتم انهاء الفاتورة",
                        "تاكيد ",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2,
                        MessageBoxOptions.RtlReading);
                if (a.ToString() != "OK")
                    return;
                con.Open();
                cmd = new SqlCommand($"Select Total From Invoice{Id}", con);
                SqlDataReader reader = cmd.ExecuteReader();
                float Price = 0;
                while (reader.Read())
                {
                    Price += float.Parse(reader.GetString(0));
                }
                reader.Close();
                float tax = (Price * 14) / 100;
                float total = Price + tax;
                string CompanyName = comboBox1.Text;
                string Date = DateTime.Today.ToLongTimeString();
                cmd = new SqlCommand($"Update Invoices Set Name = N'{CompanyName}', Price = N'{Price}', Total = N'{total}', tax = N'{tax}',Date = N'{GetFormatedDate(Date)}' Where Id = {Id}", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show
                (
                    "لقد تم اضافة الفاتورة الجديدة بنجاح",
                    "نجح",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading
                );
            }
            con.Open();
            cmd = new SqlCommand($"Delete Invoices Where Total = N'NULL'", con);
            cmd.ExecuteNonQuery();
            con.Close();
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
        private string Id;
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
        Rectangle OLabel1;
        Rectangle OLabel2;
        Rectangle OLabel3;
        Rectangle OLabel4;
        Rectangle OComboBox1;
        Rectangle OComboBox2;
        Rectangle Obutton1;
        Rectangle Obutton2;
        Rectangle OTextBox1;
        Rectangle ODataGridView1;
        private void New_Invoice_Load(object sender, EventArgs e)
        {
            OFormSize = this.Size;
            OLabel1 = new Rectangle(label1.Location,label1.Size);
            OLabel2 = new Rectangle(label2.Location,label2.Size);
            OLabel3 = new Rectangle(label3.Location,label3.Size);
            OLabel4 = new Rectangle(label4.Location,label4.Size);
            OComboBox1 = new Rectangle(comboBox1.Location, comboBox1.Size);
            OComboBox2 = new Rectangle(comboBox2.Location, comboBox2.Size);
            Obutton1 = new Rectangle(button1.Location,button1.Size);
            Obutton2 = new Rectangle(button2.Location,button2.Size);
            OTextBox1 = new Rectangle(textBox1.Location,textBox1.Size);
            ODataGridView1 = new Rectangle(dataGridView1.Location,dataGridView1.Size);
            con.Open();
            cmd = new SqlCommand($"Select Name From Company", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if(!reader.HasRows)
            {
                reader.Close();
                con.Close();
                ErrorMessageBox("لا يوجد شريكات لاضافة فاتورة");
                return;
            }
            while(reader.Read())
            {
                comboBox1.Items.Add(reader.GetString(0));
            }
            reader.Close();
            cmd = new SqlCommand($"Select Id From Invoices Where Name = N'NULL'", con);
            reader = cmd.ExecuteReader();
            int temp = 0;
            if(!reader.HasRows)
            {
                temp = 1;
                reader.Close();
                cmd = new SqlCommand($"Insert Into Invoices Values(N'NULL',N'NULL',N'NULL',N'NULL',N'NULL')", con);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand($"Select Id From Invoices Where Name = N'NULL'", con);
                reader = cmd.ExecuteReader();
            }
            reader.Read();
            Id = reader["Id"].ToString();
            reader.Close();
            if(temp == 1)
            {
                cmd = new SqlCommand($"Create Table Invoice{Id} \n" +
                                  "(                        \n" +
                                  "     Item nvarchar(200),  \n" +
                                  "     Number nvarchar(200),\n" +
                                  "     Price nvarchar(200), \n" +
                                  "     Total nvarchar(200)  \n" +
                                  ");                         ", con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand($"Select Id From Company Where Name = N'{comboBox1.Text}'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            string Id = reader["Id"].ToString();
            reader.Close();
            cmd = new SqlCommand($"Select Item From Company{Id} ", con);
            reader = cmd.ExecuteReader();
            if(!reader.HasRows)
            {
                reader.Close();
                con.Close();
                ErrorMessageBox("لا يوجد عناصر في هذة الشركة");
                return;
            }
            while( reader.Read())
            {
                comboBox2.Items.Add(reader.GetString(0));
            }
            reader.Close();
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == -1)
            {
                ErrorMessageBox("يجب ان تختار الشركة");
                return;
            }
            if(comboBox2.SelectedIndex == -1)
            {
                ErrorMessageBox("يجب ان تختار العنصر");
                return;
            }
            if(textBox1.Text == "")
            {
                ErrorMessageBox("يجب ان تكتر العدد");
                return;
            }
            DialogResult a = MessageBox.Show("سيتم اضافة عنصر جديد جديدة",
                    "تاكيد ",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2,
                    MessageBoxOptions.RtlReading);
            if (a.ToString() != "OK")
                return;
            con.Open();
            string CompanyName = comboBox1.Text;
            string ItemName = comboBox2.Text;
            string Number = textBox1.Text;
            cmd = new SqlCommand($"Select Id From Company Where Name = N'{CompanyName}'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            string CompanyId = reader["Id"].ToString();
            reader.Close();
            cmd = new SqlCommand($"Select Price From Company{CompanyId} Where Item = N'{ItemName}'", con);
            reader = cmd.ExecuteReader();
            reader.Read();
            string ItemPrice = reader.GetString(0);
            reader.Close();
            string Total = (float.Parse(Number) * float.Parse(ItemPrice)).ToString();
            cmd = new SqlCommand($"Insert Into Invoice{Id} values(N'{ItemName}',N'{Number}',N'{ItemPrice}',N'{Total}')", con);
            cmd.ExecuteNonQuery();
            reader.Close();
            MessageBox.Show
            (
                "لقد تم اضافة العنصر الجديدة بنجاح",
                "نجح",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RtlReading
            );
            cmd = new SqlCommand($"Select * From Invoice{Id}",con);
            SqlDataAdapter data = new SqlDataAdapter(cmd);
            DataTable dte = new DataTable();
            data.Fill(dte);
            dataGridView1.DataSource = dte;
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult a = MessageBox.Show("سيتم انهاء الفاتورة",
                    "تاكيد ",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2,
                    MessageBoxOptions.RtlReading);
            if (a.ToString() != "OK")
                return;
            con.Open();
            cmd = new SqlCommand($"Select Total From Invoice{Id}", con);
            SqlDataReader reader = cmd.ExecuteReader();
            float Price = 0;
            while(reader.Read())
            {
                Price += float.Parse(reader.GetString(0));
            }
            reader.Close();
            float tax = (Price * 14) / 100;
            float total = Price + tax;
            string CompanyName = comboBox1.Text;
            cmd = new SqlCommand($"Select Id From Company Where Name = N'{CompanyName}'",con);
            reader = cmd.ExecuteReader();
            reader.Read();
            string CompanyId = reader["Id"].ToString();
            reader.Close();
            string Date = DateTime.Now.ToLongDateString();
            cmd = new SqlCommand($"Update Invoices Set Name = N'{CompanyName}', Price = N'{Price}', Total = N'{total}', tax = N'{tax}',Date = N'{GetFormatedDate(Date)}' Where Id = {Id}", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand($"Select Balance From TranCompany{CompanyId} Where Type = N'الاجمالي'", con);
            reader = cmd.ExecuteReader();
            reader.Read();
            string Balance = reader.GetString(0);
            
            string the1 = (total / 100).ToString();
            string tot = (total - float.Parse(the1)).ToString();
            Balance = (float.Parse(Balance) + float.Parse(tot) ).ToString();
            reader.Close();
            cmd = new SqlCommand($"Update TranCompany{CompanyId} Set Type = N'دين', InvoiceId = N'{Id}', Number1 = N'{the1}', Total = N'{tot}', Balance = N'{Balance}' Where Type = N'الاجمالي'", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand($"Insert Into TranCompany{CompanyId} Values(N'الاجمالي',N'',N'',N'',N'{Balance}')", con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show
            (
                "لقد تم اضافة الفاتورة الجديدة بنجاح",
                "نجح",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RtlReading
            );
            Temp = 1;
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar)) e.Handled = true;
        }

        private void New_Invoice_Resize(object sender, EventArgs e)
        {
            ResizeControls(OLabel1, label1);
            ResizeControls(OLabel2, label2);
            ResizeControls(OLabel3, label3);
            ResizeControls(OLabel4, label4);
            ResizeControls(Obutton1, button1);
            ResizeControls(Obutton2, button2);
            ResizeControls(OComboBox1, comboBox1);
            ResizeControls(OComboBox2, comboBox2);
            ResizeControls(OTextBox1,textBox1);
            ResizeControls(ODataGridView1, dataGridView1);
        }
    }
}
