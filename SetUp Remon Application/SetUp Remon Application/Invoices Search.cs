using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SetUp_Remon_Application
{
    /*
        
        label 8
        label 9
        label 10



    */
    public partial class Invoices_Search : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        public Invoices_Search()
        {
            InitializeComponent();
            StreamReader streamReader = new StreamReader("info.txt");
            string server = streamReader.ReadLine();
            string Database = streamReader.ReadLine();
            con = new SqlConnection(@"Data Source=" + server + ";Initial Catalog=" + Database + ";Integrated Security=True;");
        }

        private void Invoices_Search_FormClosing(object sender, FormClosingEventArgs e)
        {
            Accounts_Menu.This.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string Command = "Select * From Invoices ";
            int temp = 0;
            if (comboBox1.SelectedIndex > 0)
            {
                if (temp == 0)
                {
                    Command += $"Where Name = N'{comboBox1.Text}' ";
                    temp = 1;
                }
                else
                {
                    Command += $"and Name = N'{comboBox1.Text}' ";
                }
            }
            if (textBox2.Text.Length > 0)
            {
                if (temp == 0)
                {
                    Command += $"Where Id = {textBox2.Text} ";
                    temp = 1;
                }
                else
                {
                    Command += $"and Id = {textBox2.Text} ";
                }
            }
            con.Open();
            cmd = new SqlCommand(Command);
            cmd.Connection = con;
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ErrorMessageBox("لا يوجد من فاتورة الذى حدته");
                reader.Close();
                con.Close();
                return;
            }
            reader.Close();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            cmd.ExecuteNonQuery();
            con.Close();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
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
        Rectangle OLable5;
        Rectangle OLable6;
        Rectangle OLable7;
        Rectangle OLable8;
        Rectangle OLable9;
        Rectangle OLable10;
        Rectangle OLable11;
        Rectangle OLable12;
        Rectangle OButton1;
        Rectangle OButton2;
        Rectangle OButton3;
        Rectangle OButton4;
        Rectangle OButton5;
        Rectangle OButton6;
        Rectangle OButton7;
        Rectangle OTextBox1;
        Rectangle OTextBox2;
        Rectangle OTextBox3;
        Rectangle OComboBox1;
        Rectangle OComboBox2;
        Rectangle ODataGridView1;

        private void Invoices_Search_Load(object sender, EventArgs e)
        {
            OFormSize = this.Size;
            OLable1 = new Rectangle(label1.Location, label1.Size);
            OLable2 = new Rectangle(label2.Location, label2.Size);
            OLable3 = new Rectangle(label3.Location, label3.Size);
            OLable4 = new Rectangle(label4.Location, label4.Size);
            OLable5 = new Rectangle(label5.Location, label5.Size);
            OLable6 = new Rectangle(label6.Location, label6.Size);
            OLable7 = new Rectangle(label7.Location, label7.Size);
            OLable8 = new Rectangle(label8.Location, label8.Size);
            OLable9 = new Rectangle(label9.Location, label9.Size);
            OLable10 = new Rectangle(label10.Location, label10.Size);
            OLable11 = new Rectangle(label11.Location, label11.Size);
            OLable12 = new Rectangle(label12.Location, label12.Size);
            OButton1 = new Rectangle(button1.Location, button1.Size);
            OButton2 = new Rectangle(button2.Location, button2.Size);
            OButton3 = new Rectangle(button3.Location, button3.Size);
            OButton4 = new Rectangle(button4.Location, button4.Size);
            OButton5 = new Rectangle(button5.Location, button5.Size);
            OButton6 = new Rectangle(button6.Location, button6.Size);
            OButton7 = new Rectangle(button7.Location, button7.Size);
            OTextBox1 = new Rectangle(textBox1.Location, textBox1.Size);
            OTextBox2 = new Rectangle(textBox2.Location, textBox2.Size);
            OTextBox3 = new Rectangle(textBox3.Location, textBox3.Size);
            OComboBox1 = new Rectangle(comboBox1.Location, comboBox1.Size);
            OComboBox2 = new Rectangle(comboBox2.Location, comboBox2.Size);
            ODataGridView1 = new Rectangle(dataGridView1.Location, dataGridView1.Size);
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
            comboBox1.Items.Add("الكل");
            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetString(0));
            }
            reader.Close();
            con.Close();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب رقم الفاتورة");
                return;
            }
            string Id = textBox2.Text;
            con.Open();
            cmd = new SqlCommand($"Select * From Invoices Where Id = {Id}", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ErrorMessageBox("لا يوجد هذة الفاتورة");
                reader.Close();
                con.Close();
                return;
            }
            reader.Read();
            string Price = reader["Price"].ToString();
            string tax = reader["tax"].ToString();
            string Total = reader["Total"].ToString();
            label8.Text = Price;
            label9.Text = tax;
            label10.Text = Total;
            reader.Close();
            cmd = new SqlCommand($"Select * From Invoice{Id}", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            cmd.ExecuteNonQuery();
            con.Close();
            textBox1.Text = "";
            textBox3.Text = "";
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar)) e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar)) e.Handled = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand($"Select Id From Company Where Name = N'{comboBox1.Text}'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                reader.Close();
                con.Close();
                return;
            }
            reader.Read();
            string Id = reader["Id"].ToString();
            reader.Close();
            cmd = new SqlCommand($"Select Item From Company{Id} ", con);
            reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                reader.Close();
                con.Close();
                ErrorMessageBox("لا يوجد عناصر في هذة الشركة");
                return;
            }
            comboBox2.Items.Clear();
            comboBox2.Items.Add("الكل");
            while (reader.Read())
            {
                comboBox2.Items.Add(reader.GetString(0));
            }
            reader.Close();
            con.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Finishing(string Id)
        {
            cmd = new SqlCommand($"Select Total From Invoice{Id}", con);
            SqlDataReader reader = cmd.ExecuteReader();
            float Price = 0;
            while (reader.Read())
            {
                Price += float.Parse(reader.GetString(0));
            }
            reader.Close();
            float tax = (Price * 14) / 100;
            float Total = Price + tax;
            string CompanyName = comboBox1.Text;
            cmd = new SqlCommand($"Update Invoices Set Name = N'{CompanyName}', Price = N'{Price}', Total = N'{Total}', tax = N'{tax}' Where Id = {Id}", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand($"Select Id From Company Where Name = N'{CompanyName}'", con);
            reader = cmd.ExecuteReader();
            reader.Read();
            string CompanyId = reader["Id"].ToString();
            reader.Close();
            cmd = new SqlCommand($"Select * From TranCompany{CompanyId}", con);
            reader = cmd.ExecuteReader();
            int pos = 0;
            float Diff = 0;
            string BackBalance = "0";
            string LastBalance = "0";
            for (int i = 0; reader.Read(); i++)
            {
                if (Id == reader["InvoiceId"].ToString() && reader["Type"].ToString() == "دين")
                {
                    string Type = reader["Type"].ToString();
                    pos = i;
                    float The1 = Total / 100;
                    float tot = Total - The1;
                    float Balance = tot + float.Parse(BackBalance);
                    LastBalance = Balance.ToString();
                    BackBalance = reader["Balance"].ToString();
                    if (Balance > float.Parse(BackBalance))
                    {
                        Diff = Balance - float.Parse(BackBalance);
                    }
                    else
                    {
                        Diff = -(float.Parse(BackBalance) - Balance);
                    }
                    reader.Close();
                    cmd = new SqlCommand($"Update TranCompany{CompanyId} Set Number1 = {The1}, Total = {tot}, Balance = {Balance} where InvoiceId = {Id} and Type = N'{Type}'", con);
                    cmd.ExecuteNonQuery();
                    break;
                }
                BackBalance = reader["Balance"].ToString();
            }
            reader.Close();
            cmd = new SqlCommand($"Select * From TranCompany{CompanyId}", con);
            SqlDataReader data = cmd.ExecuteReader();
            List<string> Ids = new List<string>();
            List<string> Types = new List<string>();
            for (int i = 0; data.Read(); i++)
            {
                if (i > pos)
                {
                    string InvoiceId = data["InvoiceId"].ToString();
                    string Type = data["Type"].ToString();
                    if (!string.IsNullOrEmpty(InvoiceId))
                    {
                        Ids.Add(InvoiceId);
                        Types.Add(Type);
                    }
                }
            }
            data.Close();
            for (int i = 0; i < Ids.Count; i++)
            {
                cmd = new SqlCommand($"Select Balance From TranCompany{CompanyId} Where InvoiceId = {Ids[i]} and type = N'{Types[i]}' ", con);
                SqlDataReader sqlData = cmd.ExecuteReader();
                sqlData.Read();
                float Balance = float.Parse(sqlData.GetString(0));
                Balance += Diff;
                LastBalance = Balance.ToString();
                sqlData.Close();
                cmd = new SqlCommand($"Update TranCompany{CompanyId} Set Balance = {Balance} Where InvoiceId = {Ids[i]} and Type = N'{Types[i]}' ", con);
                cmd.ExecuteNonQuery();
            }
            cmd = new SqlCommand($"Update TranCompany{CompanyId} Set Balance = {LastBalance} Where Type = N'الاجمالي'",con);
            cmd.ExecuteNonQuery();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex <= 0)
            {
                ErrorMessageBox("يجب ان تختار الشركة");
                return;
            }
            if (textBox2.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب رقم الفتورة");
                return;
            }
            if (comboBox2.SelectedIndex <= 0)
            {
                ErrorMessageBox("يجب ان تختار العنصر");
                return;
            }
            if (textBox3.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب السعر الجديد للعنصر");
                return;
            }
            DialogResult a = MessageBox.Show("سيتم تعديل السعر",
                    "تاكيد ",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2,
                    MessageBoxOptions.RtlReading);
            if (a.ToString() != "OK")
                return;
            string Id = textBox2.Text;
            string Item = comboBox2.Text;
            string NewPrice = textBox3.Text;
            con.Open();
            cmd = new SqlCommand($"Select * From Invoices Where Id = {Id}", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ErrorMessageBox("لا يوجد هذة الفاتورة");
                reader.Close();
                con.Close();
                return;
            }
            reader.Close();
            cmd = new SqlCommand($"Select Number From Invoice{Id} Where Item = N'{Item}'", con);
            reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ErrorMessageBox("لا يوجد هذا العنصر في هذة الفاتورة");
                reader.Close();
                con.Close();
                return;
            }
            reader.Read();
            float Number = float.Parse(reader.GetString(0));
            string total = (Number * float.Parse(NewPrice)).ToString();
            reader.Close();
            cmd = new SqlCommand($"Update Invoice{Id} Set Price = N'{NewPrice}', Total = N'{total}' Where Item = N'{Item}'", con);
            cmd.ExecuteNonQuery();
            Finishing(Id);
            con.Close();
            MessageBox.Show
            (
                "لقد تم تعديل بنجاح",
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

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex <= 0)
            {
                ErrorMessageBox("يجب ان تختار الشركة");
                return;
            }
            if (textBox2.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب رقم الفتورة");
                return;
            }
            if (comboBox2.SelectedIndex <= 0)
            {
                ErrorMessageBox("يجب ان تختار العنصر");
                return;
            }
            if (textBox1.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب العدد الجديد");
                return;
            }
            string CompanyName = comboBox1.Text;
            string Id = textBox2.Text;
            string Item = comboBox2.Text;
            string NewNumber = textBox1.Text;
            DialogResult a = MessageBox.Show("سيتم تعديل العدد",
                    "تاكيد ",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2,
                    MessageBoxOptions.RtlReading);
            if (a.ToString() != "OK")
                return;
            con.Open();
            cmd = new SqlCommand($"Select * From Invoices Where Id = {Id}", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ErrorMessageBox("لا يوجد هذة الفاتورة");
                reader.Close();
                con.Close();
                return;
            }
            reader.Close();
            cmd = new SqlCommand($"Select Price From Invoice{Id} Where Item = N'{Item}'", con);
            reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ErrorMessageBox("لا يوجد عنصر بهذا لاسم");
                reader.Close();
                con.Close();
                return;
            }
            reader.Read();
            string Price = reader.GetString(0);
            string total = (float.Parse(NewNumber) * float.Parse(Price)).ToString();
            reader.Close();
            cmd = new SqlCommand($"Update Invoice{Id} Set Number = N'{NewNumber}', Total = N'{total}' Where Item = N'{Item}'", con);
            cmd.ExecuteNonQuery();
            Finishing(Id);
            con.Close();
            MessageBox.Show
            (
                "لقد تم تعديل بنجاح",
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب رقم الفتورة");
                return;
            }
            if (comboBox1.SelectedIndex == -1)
            {
                ErrorMessageBox("يجب ان تختار الشركة");
                return;
            }
            if (comboBox2.SelectedIndex == -1)
            {
                ErrorMessageBox("يجب ان تختار العنصر");
                return;
            }
            if (textBox1.Text == "")
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
            string Id = textBox2.Text;
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
            cmd = new SqlCommand($"Select * From Invoice{Id}", con);
            SqlDataAdapter data = new SqlDataAdapter(cmd);
            DataTable dte = new DataTable();
            data.Fill(dte);
            dataGridView1.DataSource = dte;
            cmd.ExecuteNonQuery();
            Finishing(Id);
            con.Close();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex <= 0)
            {
                ErrorMessageBox("يجب ان تختار الشركة");
                return;
            }
            if (textBox2.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب رقم الفتورة");
                return;
            }
            if (comboBox2.SelectedIndex <= 0)
            {
                ErrorMessageBox("يجب ان تختار العنصر");
                return;
            }
            DialogResult a = MessageBox.Show("سيتم حذف العنصر",
                    "تاكيد ",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2,
                    MessageBoxOptions.RtlReading);
            if (a.ToString() != "OK")
                return;
            string CompanyName = comboBox1.Text;
            string Id = textBox2.Text;
            string Item = comboBox2.Text;
            con.Open();
            cmd = new SqlCommand($"Select * From Invoices Where Id = {Id}", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ErrorMessageBox("لا يوجد هذة الفاتورة");
                reader.Close();
                con.Close();
                return;
            }
            reader.Close();
            cmd = new SqlCommand($"Delete From Invoice{Id} Where Item = N'{Item}'", con);
            cmd.ExecuteNonQuery();
            Finishing(Id);
            con.Close();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }
        //                                  
        private void CreateHorizontalLine(int x, int y, int Lenth, PrintPageEventArgs e)
        {
            for (int i = 0; i < Lenth; i++)
            {
                e.Graphics.DrawString($"_", new Font("Arial", 24, FontStyle.Bold), Brushes.Black, new Point(x + i, y));
            }
        }

        private void CreateVerticalLine(int x, int y, int Lenth, PrintPageEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Brushes.Black, 2), new Point(x, y), new Point(x, y + Lenth));
        }

        private void CreateTable(int x, int y, int L, int W, PrintPageEventArgs e)
        {
            CreateHorizontalLine(x, y, W, e);

            CreateVerticalLine(x - 3, y + 30, L - 40, e);

            CreateHorizontalLine(x, L, W, e);

            CreateVerticalLine(L + x + 10, y + 30, L - 40, e);
        }

        private void Print(string x, Point point, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(x, new Font("Arial", 18, FontStyle.Bold), Brushes.Black, point);
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            string Id = textBox2.Text;
            con.Open();
            cmd = new SqlCommand($"Select * From Invoices Where Id = {Id}", con);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            string CompanyName = (string)reader["Name"].ToString();
            string Price = (string)reader["Price"].ToString();
            string Tax = (string)reader["Tax"].ToString();
            string Total = (string)reader["Total"].ToString();
            string Date = (string)reader["Date"].ToString();
            reader.Close();
            List<string> Items = new List<string>();
            List<string> Prices = new List<string>();
            List<string> Numbers = new List<string>();
            List<string> Totals = new List<string>();
            cmd = new SqlCommand($"Select * From Invoice{Id}", con);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Items.Add(reader["Item"].ToString());
                Prices.Add(reader["Price"].ToString());
                Numbers.Add(reader["Number"].ToString());
                Totals.Add(reader["Total"].ToString());
            }
            reader.Close();
            con.Close();
            e.Graphics.DrawString($"{Id}: فاتورة", new Font("Arial", 24, FontStyle.Bold), Brushes.Black, new Point(375, 50));
            e.Graphics.DrawRectangle(new Pen(Brushes.Black, 2), new Rectangle(new Point(30, 100), new Size(790, 50)));
            Print(": التريخ", new Point(700, 115), e);
            Print(Date, new Point(550, 115), e);
            e.Graphics.DrawRectangle(new Pen(Brushes.Black, 2), new Rectangle(new Point(30, 155), new Size(790, 50)));
            Print(": اسم العميل", new Point(700, 170), e);
            Print(CompanyName, new Point(400, 170), e);
            e.Graphics.DrawRectangle(new Pen(Brushes.Black, 2), new Rectangle(new Point(30, 220), new Size(790, 50)));
            Print("رقم", new Point(775, 235), e);
            int VerticalLine1 = 765;
            int Lenght = 50;
            Print("البيان", new Point(700, 235), e);
            int VerticalLine2 = 400;
            Print("العدد", new Point(340, 235), e);
            int VerticalLine3 = 250;
            Print("السعر", new Point(190, 235), e);
            int VerticalLine4 = 165;
            Print("الاجمالي", new Point(40, 235), e);
            int VerticalLine5 = 30;


            for (int i = 0; i < Items.Count; i++)
            {
                Lenght += 50;
                e.Graphics.DrawLine(new Pen(Brushes.Black, 2), 30, Lenght + 220, 820, Lenght + 220);
                Print($"{i + 1}", new Point(785, Lenght + 180), e);
                Print(Items[i], new Point(580, Lenght + 180), e);
                Print(Numbers[i], new Point(300, Lenght + 180), e);
                Print(Prices[i], new Point(185, Lenght + 180), e);
                Print(Totals[i], new Point(40, Lenght + 180), e);
            }

            CreateVerticalLine(820, 220, Lenght, e);
            CreateVerticalLine(VerticalLine1, 220, Lenght, e);
            CreateVerticalLine(VerticalLine2, 220, Lenght, e);
            CreateVerticalLine(VerticalLine3, 220, Lenght, e);
            CreateVerticalLine(VerticalLine4, 220, Lenght, e);
            CreateVerticalLine(VerticalLine5, 220, Lenght, e);

            e.Graphics.DrawRectangle(new Pen(Brushes.Black, 2), new Rectangle(new Point(30, Lenght + 220), new Size(135, 50)));
            Lenght += 50;
            e.Graphics.DrawLine(new Pen(Brushes.Black, 2), 30, Lenght + 220, 820, Lenght + 220);
            CreateVerticalLine(820, 220, Lenght, e);
            CreateVerticalLine(VerticalLine1, 220, Lenght, e);
            CreateVerticalLine(VerticalLine2, 220, Lenght, e);
            e.Graphics.DrawRectangle(new Pen(Brushes.Black, 2), new Rectangle(new Point(30, Lenght + 220), new Size(135, 50)));
            Print("الاجمالي", new Point(235, Lenght + 180), e);
            Print($"{Price}", new Point(40, Lenght + 180), e);
            e.Graphics.DrawRectangle(new Pen(Brushes.Black, 2), new Rectangle(new Point(30, Lenght + 220), new Size(135, 50)));
            Lenght += 50;
            e.Graphics.DrawLine(new Pen(Brushes.Black, 2), 30, Lenght + 220, 820, Lenght + 220);
            CreateVerticalLine(820, 220, Lenght, e);
            CreateVerticalLine(VerticalLine1, 220, Lenght, e);
            CreateVerticalLine(VerticalLine2, 220, Lenght, e);
            Print("14%", new Point(235, Lenght + 180), e);
            Print($"{Tax}", new Point(40, Lenght + 180), e);
            e.Graphics.DrawRectangle(new Pen(Brushes.Black, 2), new Rectangle(new Point(30, Lenght + 220), new Size(135, 50)));
            Lenght += 50;
            Print($"{Total}", new Point(40, Lenght + 180), e);
            Print($"فقط و قدرة", new Point(700, Lenght + 200), e);
            for (int j = 0; j < 4; j++)
            {
                for (int i = 300; i < 650; i++)
                {
                    Print("_", new Point(i, Lenght + 200), e);
                }
                Lenght += 40;
            }
            Lenght += 10;
            Print($"توقيع التسليم", new Point(700, Lenght + 200), e);
            for (int i = 350; i < 650; i++)
            {
                Print("_", new Point(i, Lenght + 200), e);
            }
            Print("التاريخ", new Point(230, Lenght + 200), e);
            for (int i = 50; i < 200; i++)
            {
                Print("_", new Point(i, Lenght + 200), e);
            }
            Lenght += 40;
            for (int i = 350; i < 650; i++)
            {
                Print("_", new Point(i, Lenght + 200), e);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب رقم الفتورة");
                return;
            }
            con.Open();
            string Id = textBox2.Text;
            cmd = new SqlCommand($"Select * From Invoices Where Id = {Id}", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ErrorMessageBox("لا فاتورة بهذا الرقم");
                reader.Close();
                con.Close();
                return;
            }
            con.Close();
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void Invoices_Search_Resize(object sender, EventArgs e)
        {
            ResizeControls(OLable1, label1);
            ResizeControls(OLable2, label2);
            ResizeControls(OLable3, label3);
            ResizeControls(OLable4, label4);
            ResizeControls(OLable5, label5);
            ResizeControls(OLable6, label6);
            ResizeControls(OLable7, label7);
            ResizeControls(OLable8, label8);
            ResizeControls(OLable9, label9);
            ResizeControls(OLable10, label10);
            ResizeControls(OLable11, label11);
            ResizeControls(OLable12, label12);
            ResizeControls(OButton1, button1);
            ResizeControls(OButton2, button2);
            ResizeControls(OButton3, button3);
            ResizeControls(OButton4, button4);
            ResizeControls(OButton5, button5);
            ResizeControls(OButton6, button6);
            ResizeControls(OButton7, button7);
            ResizeControls(OTextBox1, textBox1);
            ResizeControls(OTextBox2, textBox2);
            ResizeControls(OTextBox3, textBox3);
            ResizeControls(OComboBox1, comboBox1);
            ResizeControls(OComboBox2, comboBox2);
            ResizeControls(ODataGridView1, dataGridView1);
        }
    }
}
