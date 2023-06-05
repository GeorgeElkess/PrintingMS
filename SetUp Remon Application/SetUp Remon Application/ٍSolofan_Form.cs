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
    public partial class _ٍSolofan_Form : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        private Size OFormSize;
        private Rectangle OComboBox1;
        private Rectangle OComboBox2;
        private Rectangle OComboBox3;
        private Rectangle OLable1;
        private Rectangle OLable2;
        private Rectangle OLable3;
        private Rectangle OLable6;
        private Rectangle OLable8;
        private Rectangle OTextBox1;
        private Rectangle OTextBox2;
        private Rectangle OTextBox3;
        private Rectangle OTextBox5;
        private Rectangle OTextBox6;
        private Rectangle OButton1;
        private Rectangle OButton2;
        private Rectangle OButton3;
        private Rectangle OButton4;
        private Rectangle OButton5;
        private Rectangle ODataGridView1;
        private Rectangle ODateTime1;

        private void _ٍSolofan_Form_Load(object sender, EventArgs e)
        {
            OFormSize = this.Size;
            ODateTime1 = new Rectangle(dateTimePicker1.Location.X, dateTimePicker1.Location.Y, dateTimePicker1.Width, dateTimePicker1.Height);
            ODataGridView1 = new Rectangle(dataGridView1.Location.X,dataGridView1.Location.Y,dataGridView1.Width,dataGridView1.Height);
            OButton1 = new Rectangle(button1.Location.X, button1.Location.Y, button1.Width, button1.Height);
            OButton2 = new Rectangle(button2.Location.X, button2.Location.Y, button2.Width, button2.Height);
            OButton3 = new Rectangle(button3.Location.X, button3.Location.Y, button3.Width, button3.Height);
            OButton4 = new Rectangle(button4.Location.X, button4.Location.Y, button4.Width, button4.Height);
            OButton5 = new Rectangle(button5.Location.X, button5.Location.Y, button5.Width, button5.Height);
            OTextBox1 = new Rectangle(textBox1.Location.X, textBox1.Location.Y, textBox1.Width, textBox1.Height);
            OTextBox2 = new Rectangle(textBox2.Location.X, textBox2.Location.Y, textBox2.Width, textBox2.Height);
            OTextBox3 = new Rectangle(textBox3.Location.X, textBox3.Location.Y, textBox3.Width, textBox3.Height);
            OTextBox5 = new Rectangle(textBox5.Location.X, textBox5.Location.Y, textBox5.Width, textBox5.Height);
            OTextBox6 = new Rectangle(textBox6.Location.X, textBox6.Location.Y, textBox6.Width, textBox6.Height);
            OLable1 = new Rectangle(label1.Location.X, label1.Location.Y, label1.Width, label1.Height);
            OLable2 = new Rectangle(label2.Location.X, label2.Location.Y, label2.Width, label2.Height);
            OLable3 = new Rectangle(label3.Location.X, label3.Location.Y, label3.Width, label3.Height);
            OLable6 = new Rectangle(label6.Location.X, label6.Location.Y, label6.Width, label6.Height);
            OLable8 = new Rectangle(label8.Location.X, label8.Location.Y, label8.Width, label8.Height);
            OComboBox1 = new Rectangle(comboBox1.Location.X,comboBox1.Location.Y,comboBox1.Width,comboBox1.Height);
            OComboBox2 = new Rectangle(comboBox2.Location.X,comboBox2.Location.Y,comboBox2.Width,comboBox2.Height);
            OComboBox3 = new Rectangle(comboBox3.Location.X,comboBox3.Location.Y,comboBox3.Width,comboBox3.Height);
        }

        public _ٍSolofan_Form()
        {
            InitializeComponent();
            StreamReader streamReader = new StreamReader("info.txt");
            string server = streamReader.ReadLine();
            string Database = streamReader.ReadLine();
            con = new SqlConnection(@"Data Source=" + server + ";Initial Catalog=" + Database + ";Integrated Security=True;");
        }

        private void ResizeControls(Rectangle r, Control c)
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

        public void ErrorMessageBox(string x)
        {
            MessageBox.Show(x,
                            "خطأ",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.RtlReading);
        }

        public int Check()
        {
            if(comboBox3.SelectedIndex<=0)
            {
                ErrorMessageBox("يجب ان تختار المخزن");
                return 0;
            }
            if(comboBox2.SelectedIndex<=0)
            {
                ErrorMessageBox("يجب ان تختار نوع السلوفان");
                return 0;
            }
            if(textBox1.Text.Length<=0)
            {
                ErrorMessageBox("يجب ان تكتب عدد البكرات");
                return 0;
            }
            if(textBox2.Text.Length<=0)
            {
                ErrorMessageBox("يجب ان تكتب مقاص البكرة");
                return 0;
            }
            if(textBox3.Text.Length<=0)
            {
                ErrorMessageBox("يجب ان تكتب اسم المورد");
                return 0;
            }
            return 1;
        }

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

        public void Add()
        {
            if (Check() == 0) return;
            string Store  = comboBox3.Text;
            string Code   = comboBox2.Text;
            string Number = textBox1.Text;
            string Size   = textBox2.Text;
            string Name   = textBox3.Text;
            string Date   = dateTimePicker1.Text;
            Date = GetFormatedDate(Date);
            string User_Name = Menu.User_Name;
            DialogResult a = MessageBox.Show("سيتم اضافة " + Number + " سولوفان ",
                    "تاكيد ",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2,
                    MessageBoxOptions.RtlReading);
            if (a.ToString() != "OK")
                return;
            con.Open();
            cmd = new SqlCommand($"Select Number From Solofan Where Store = N'{Store}' and Code = N'{Code}' and Size = N'{Size}'", con);
            SqlDataReader rdr = cmd.ExecuteReader();
            if(rdr.HasRows)
            {
                rdr.Read();
                string OldNumber = (string)rdr["Number"];
                string NewNumber = (float.Parse(Number) + float.Parse(OldNumber)).ToString();
                cmd = new SqlCommand($"Update Solofan Set Number = N'{NewNumber}' Where Store = N'{Store}' and Code = N'{Code}' and Size = N'{Size}'", con);
            }
            else
            {
                cmd = new SqlCommand($"Insert Into Solofan Values(N'{Code}', N'{Number}', N'{Size}', N'{Store}')", con);
            }
            rdr.Close();
            cmd.ExecuteNonQuery();
            string OrderNumber = textBox5.Text;
            string OrderName = textBox6.Text;
            cmd = new SqlCommand($"Insert Into TranSolofan Values(N'{User_Name}', N'اضافة', N'{Name}', N'{Date}', N'{Code}', N'{Number}', N'{Size}', N'{Store}', N'{OrderNumber}', N'{OrderName}')", con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show
            (
                "لقد تم تخزين السلوفان الجديد بنجاح",
                "نجح",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RtlReading
            );
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        public void Take()
        {
            if (Check() == 0) return;
            string Store = comboBox3.Text;
            string Code = comboBox2.Text;
            string Number = textBox1.Text;
            string Size = textBox2.Text;
            string Name = textBox3.Text;
            string Date = dateTimePicker1.Text;
            Date = GetFormatedDate(Date);
            string User_Name = Menu.User_Name;
            
            string OrderNumber = textBox5.Text;
            DialogResult a = MessageBox.Show("سيتم سحب " + Number + " سولوفان ",
                    "تاكيد ",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2,
                    MessageBoxOptions.RtlReading);
            if (a.ToString() != "OK")
                return;
            con.Open();
            cmd = new SqlCommand($"Select Number From Solofan Where Store = N'{Store}' and Code = N'{Code}' and Size = N'{Size}'", con);
            SqlDataReader rdr = cmd.ExecuteReader();   
            if(rdr.HasRows)
            {
                rdr.Read();
                string OldNumber = (string)rdr["Number"];
                string NewNumber = (float.Parse(OldNumber) - float.Parse(Number)).ToString();
                if(float.Parse(NewNumber) < 0)
                {
                    ErrorMessageBox("لا يوجد سلوفان كافي");
                    rdr.Close();
                    con.Close();
                    return;
                }
                else if(float.Parse(NewNumber) == 0)
                {
                    cmd = new SqlCommand($"Delete Solofan Where Store = N'{Store}' and Code = N'{Code}' and Size = N'{Size}'", con);
                }
                else
                {
                    cmd = new SqlCommand($"Update Solofan Set Number = N'{NewNumber}' Where Store = N'{Store}' and Code = N'{Code}' and Size = N'{Size}'", con);
                }
            }
            else
            {
                ErrorMessageBox("لا يوجد سلوفان من النوع الذي حدتة");
                rdr.Close();
                con.Close();
                return;
            }
            rdr.Close();
            cmd.ExecuteNonQuery();
            string OrderName = textBox6.Text;
            cmd = new SqlCommand($"Insert Into TranSolofan Values(N'{User_Name}', N'سحب', N'{Name}', N'{Date}', N'{Code}', N'{Number}', N'{Size}', N'{Store}', N'{OrderNumber}', N'{OrderName}')", con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show
            (
                "لقد تم سحب السلوفان بنجاح",
                "نجح",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RtlReading
            );
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex <= 0)
            {
                ErrorMessageBox("يجب ان تكتب اضافة ام سحب");
                return;
            }
            if(comboBox1.SelectedIndex == 1)
            {
                Add();
            }
            else
            {
                Take();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Command = "Select * from Solofan ";
            con.Open();
            cmd = new SqlCommand(Command);
            cmd.Connection = con;
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ErrorMessageBox("لا يوجد سولوفان");
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
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string Command = "Select * from TranSolofan ";
            con.Open();
            cmd = new SqlCommand(Command);
            cmd.Connection = con;
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ErrorMessageBox("لا يوجد اي نقل للسولوفان");
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
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string Command = "Select * From Solofan ";
            int temp = 0;
            if(comboBox3.SelectedIndex > 0)
            {
                if(temp == 0)
                {
                    Command += $"Where Store = N'{comboBox3.Text}' ";
                    temp = 1;
                }
                else
                {
                    Command += $"and Store = N'{comboBox3.Text}' ";
                }
            }
            if(comboBox2.SelectedIndex > 0)
            {
                if(temp == 0)
                {
                    Command += $"Where Code = N'{comboBox2.Text}' ";
                    temp = 1;
                }
                else
                {
                    Command += $"and Code = N'{comboBox2.Text}' ";
                }
            }
            if(textBox2.Text.Length > 0)
            {
                if(temp == 0)
                {
                    Command += $"Where Size = N'{textBox2.Text}' ";
                    temp = 1;
                }
                else
                {
                    Command += $"and Size = N'{textBox2.Text}' ";
                }
            }
            con.Open();
            cmd = new SqlCommand(Command);
            cmd.Connection = con;
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ErrorMessageBox("لا يوجد سولوفان من الذي حدتة");
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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Command = "Select * From TranSolofan ";
            int temp = 0;
            if (comboBox3.SelectedIndex > 0)
            {
                if (temp == 0)
                {
                    Command += $"Where Store = N'{comboBox3.Text}' ";
                    temp = 1;
                }
                else
                {
                    Command += $"and Store = N'{comboBox3.Text}' ";
                }
            }
            if (comboBox2.SelectedIndex > 0)
            {
                if (temp == 0)
                {
                    Command += $"Where Code = N'{comboBox2.Text}' ";
                    temp = 1;
                }
                else
                {
                    Command += $"and Code = N'{comboBox2.Text}' ";
                }
            }
            if (textBox2.Text.Length > 0)
            {
                if (temp == 0)
                {
                    Command += $"Where Size = N'{textBox2.Text}' ";
                    temp = 1;
                }
                else
                {
                    Command += $"and Size = N'{textBox2.Text}' ";
                }
            }
            if(comboBox1.SelectedIndex > 0)
            {
                if (temp == 0)
                {
                    Command += $"Where Type = N'{comboBox1.Text}' ";
                    temp = 1;
                }
                else
                {
                    Command += $"and Type = N'{comboBox1.Text}' ";
                }
            }
            if(textBox3.Text.Length > 0)
            {
                if (temp == 0)
                {
                    Command += $"Where Name = N'{textBox3.Text}' ";
                    temp = 1;
                }
                else
                {
                    Command += $"and Name = N'{textBox3.Text}' ";
                }
            }
            if(textBox5.Text.Length > 0)
            {
                if (temp == 0)
                {
                    Command += $"Where OrderNumber = N'{textBox5.Text}' ";
                    temp = 1;
                }
                else
                {
                    Command += $"and OrderNumber = N'{textBox5.Text}' ";
                }
            }
            if(textBox6.Text.Length > 0)
            {
                if (temp == 0)
                {
                    Command += $"Where OrderName = N'{textBox6.Text}' ";
                    temp = 1;
                }
                else
                {
                    Command += $"and OrderName = N'{textBox6.Text}' ";
                }
            }
            con.Open();
            cmd = new SqlCommand(Command);
            cmd.Connection = con;
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ErrorMessageBox("لا يوجد نقل سولوفان من الذي حدتة");
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
        }

        private void _ٍSolofan_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Menu.This.Show();
        }

        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar)) e.Handled = true;

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar)) e.Handled = true;

        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar)) e.Handled = true;

        }

        private void _ٍSolofan_Form_Resize(object sender, EventArgs e)
        {
            ResizeControls(OComboBox1, comboBox1);
            ResizeControls(OComboBox2, comboBox2);
            ResizeControls(OComboBox3, comboBox3);
            ResizeControls(OLable1, label1);
            ResizeControls(OLable2, label2);
            ResizeControls(OLable3, label3);
            ResizeControls(OLable6, label6);
            ResizeControls(OLable8, label8);
            ResizeControls(OTextBox1, textBox1);
            ResizeControls(OTextBox2, textBox2);
            ResizeControls(OTextBox3, textBox3);
            ResizeControls(OTextBox5, textBox5);
            ResizeControls(OTextBox6, textBox6);
            ResizeControls(OButton1,button1);
            ResizeControls(OButton2,button2);
            ResizeControls(OButton3,button3);
            ResizeControls(OButton4,button4);
            ResizeControls(OButton5,button5);
            ResizeControls(ODataGridView1, dataGridView1);
            ResizeControls(ODateTime1, dateTimePicker1);
        }
    }
}
