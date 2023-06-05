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
    public partial class Account_Payable_From_Companys : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        public Account_Payable_From_Companys()
        {
            InitializeComponent();
            StreamReader streamReader = new StreamReader("info.txt");
            string server = streamReader.ReadLine();
            string Database = streamReader.ReadLine();
            con = new SqlConnection(@"Data Source=" + server + ";Initial Catalog=" + Database + ";Integrated Security=True;");
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
        private void Account_Payable_From_Companys_FormClosing(object sender, FormClosingEventArgs e)
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

        private void Account_Payable_From_Companys_Load(object sender, EventArgs e)
        {
            OFormSize = this.Size;
            con.Open();
            cmd = new SqlCommand($"Select Name From Company", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                reader.Close();
                con.Close();
                ErrorMessageBox("لا يوجد شريكات لاضافة فاتورة");
                return;
            }
            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetString(0));
            }
            reader.Close();
            con.Close();
        }

        void Finishing(string Id)
        {
            cmd = new SqlCommand($"Select * From TranCompany{Id}", con);
            SqlDataReader reader = cmd.ExecuteReader();
            string BackBalance = "0";

            List<string> vs = new List<string>();
            List<float> temp = new List<float>();
            while (reader.Read())
            {
                string InvoiceId = reader["InvoiceId"].ToString();
                vs.Add(InvoiceId);
                if (reader["Type"].ToString() == "دين")
                {
                    temp.Add(-1);
                }
                else if (reader["Type"].ToString() == "سداد")
                {
                    temp.Add(float.Parse(reader["Total"].ToString()));
                }
                else
                { 
                    break;
                }
            }
            reader.Close();
            for (int i = 0; i < vs.Count - 1; i++)
            {
                string InvoiceId = vs[i];
                if (temp[i] != -1)
                {
                    BackBalance = (float.Parse(BackBalance) - temp[i]).ToString();
                    cmd = new SqlCommand($"Update TranCompany{Id} Set Balance = {BackBalance} Where InvoiceId = {InvoiceId} and Type = N'سداد'", con);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd = new SqlCommand($"Select Total From Invoices Where Id = {InvoiceId}", con);
                    SqlDataReader sqlData = cmd.ExecuteReader();
                    sqlData.Read();
                    string total = sqlData.GetString(0);
                    string The1 = (float.Parse(total) / 100).ToString();
                    string Total = (float.Parse(total) - float.Parse(The1)).ToString();
                    BackBalance = (float.Parse(BackBalance) + float.Parse(Total)).ToString();
                    sqlData.Close();
                    cmd = new SqlCommand($"Update TranCompany{Id} Set Number1 = {The1}, Total = {Total}, Balance = {BackBalance} Where InvoiceId = {InvoiceId} and Type = N'دين'", con);
                    cmd.ExecuteNonQuery();
                }
            }

            cmd = new SqlCommand($"Update TranCompany{Id} Set Balance = {BackBalance} Where Type = N'الاجمالي'", con);
            cmd.ExecuteNonQuery();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                ErrorMessageBox("يجب ان تختار الشركة");
                return;
            }
            string CompanyName = comboBox1.Text;
            con.Open();
            cmd = new SqlCommand($"Select Id From Company Where Name = N'{CompanyName}'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            string Id = reader["Id"].ToString();
            reader.Close();
            Finishing(Id);
            cmd = new SqlCommand($"Select X.Type,X.InvoiceId,Y.Name,Y.Price,Y.Tax,Y.Total,Y.Date,X.Number1,X.Total,X.Balance From TranCompany{Id} X Left Outer Join Invoices Y On X.InvoiceId = Y.Id and X.Type != N'سداد'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                ErrorMessageBox("يجب ان تختار الشركة");
                return;
            }
            if(textBox1.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب عدد دفع الشركة");
                return;
            }
            string CompanyName = comboBox1.Text;
            string Price = textBox1.Text;
            DialogResult a = MessageBox.Show("سيتم اضافة السداد جديد",
                    "تاكيد ",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2,
                    MessageBoxOptions.RtlReading);
            if (a.ToString() != "OK")
                return;
            con.Open();
            cmd = new SqlCommand($"Select Id From Company Where Name = N'{CompanyName}'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            string Id = reader["Id"].ToString();
            reader.Close();
            cmd = new SqlCommand($"Select Balance From TranCompany{Id} Where Type = N'الاجمالي'", con);
            reader = cmd.ExecuteReader();
            reader.Read();
            string Balance = reader["Balance"].ToString();
            Balance = (float.Parse(Balance) - float.Parse(Price)).ToString();
            reader.Close();
            cmd = new SqlCommand($"Select InvoiceId From TranCompany{Id} Where Type = N'سداد'",con);
            reader = cmd.ExecuteReader();
            int Count = 1;
            while(reader.Read())
            {
                Count++;
            }
            reader.Close();
            cmd = new SqlCommand($"Update TranCompany{Id} Set Type = N'سداد', Total = {Price}, Balance = {Balance},InvoiceId = {Count} Where Type = N'الاجمالي'", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand($"Insert Into TranCompany{Id} Values(N'الاجمالي',N'',N'',N'',N'{Balance}')", con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("لقد تم الاضافة بنجاح",
                   "نجح",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information,
                   MessageBoxDefaultButton.Button1,
                   MessageBoxOptions.RtlReading);
            textBox1.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == -1)
            {
                ErrorMessageBox("يجب ان تختار الشركة");
                return;
            }
            string CompanyName = comboBox1.Text;
            if(textBox1.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب رقم السداد الذي تريد ان تحذفة");
                return ;
            }
            string InvoiceId = textBox1.Text;
            con.Open();
            cmd = new SqlCommand($"Select Id From Company Where Name = N'{CompanyName}'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            string CompanyId = reader["Id"].ToString();
            reader.Close();
            cmd = new SqlCommand($"Select * From TranCompany{CompanyId} where InvoiceId = N'{InvoiceId}' and Type = N'سداد'", con);
            reader = cmd.ExecuteReader();
            if(!reader.HasRows)
            {
                con.Close();
                reader.Close();
                ErrorMessageBox("لا يوجد سداد بهذا الرقم");
                return;
            }
            reader.Close();
            DialogResult a = MessageBox.Show("سيتم حذف السداد",
                    "تاكيد ",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2,
                    MessageBoxOptions.RtlReading);
            if (a.ToString() != "OK")
                return;
            cmd = new SqlCommand($"Delete TranCompany{CompanyId} where InvoiceId = N'{InvoiceId}' and Type = N'سداد'", con);
            cmd.ExecuteNonQuery();
            Finishing(CompanyId);
            con.Close();
            MessageBox.Show("لقد تم الحذف بنجاح",
                   "نجح",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information,
                   MessageBoxDefaultButton.Button1,
                   MessageBoxOptions.RtlReading);
            textBox1.Text = "";
        }
    }
}
