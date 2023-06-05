using System.Data;
using System.Data.SqlClient;

namespace SetUp_Remon_Application
{
    public partial class Paper_Form : Form
    {
        public static Paper_Form This;
        SqlConnection con;
        SqlCommand cmd;
        private Size OFormSize;
        private Rectangle Obtn1; //
        private Rectangle Obtn2; //
        private Rectangle Obtn3; //
        private Rectangle Obtn4; //
        private Rectangle Obtn5; //
        private Rectangle ODateTime1;
        private Rectangle ODataGridVeiw1;
        private Rectangle OComboBox1; //
        private Rectangle OComboBox2; //
        private Rectangle OComboBox3; //
        private Rectangle OLable1; //
        private Rectangle OLable2; //
        private Rectangle OLable3; //
        private Rectangle OLable4; //
        private Rectangle OLable5; //
        private Rectangle OLable6; //
        private Rectangle OLable7; //
        private Rectangle OLable8; //
        private Rectangle OLenght; //
        private Rectangle OWidth; //
        private Rectangle OGrames; //
        private Rectangle ONumber; //
        private Rectangle OWaight; //
        private Rectangle OName; //
        private Rectangle OtextBox1; //
        private Rectangle OtextBox2; //
        public Paper_Form()
        {
            InitializeComponent();
            This = this;
            StreamReader streamReader = new StreamReader("info.txt");
            string server = streamReader.ReadLine();
            string Database = streamReader.ReadLine();
            con = new SqlConnection(@"Data Source=" + server + ";Initial Catalog=" + Database + ";Integrated Security=True;");
        }

        private void Paper_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Menu.This.Show();
        }
        private void Paper_Form_FormClosed(object sender, FormClosedEventArgs e)
        {

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

        private void Paper_Form_Load(object sender, EventArgs e)
        {
            dataGridView1.DefaultCellStyle.Font = new Font("arial", 10);
            OFormSize = this.Size;
            Obtn1 = new Rectangle(button1.Location.X, button1.Location.Y, button1.Width, button1.Height);
            Obtn2 = new Rectangle(button2.Location.X, button2.Location.Y, button2.Width, button2.Height);
            Obtn3 = new Rectangle(button3.Location.X, button3.Location.Y, button3.Width, button3.Height);
            Obtn4 = new Rectangle(button4.Location.X, button4.Location.Y, button4.Width, button4.Height);
            Obtn5 = new Rectangle(button5.Location.X, button5.Location.Y, button5.Width, button5.Height);
            OLable1 = new Rectangle(label1.Location.X, label1.Location.Y, label1.Width, label1.Height);
            OLable2 = new Rectangle(label2.Location.X, label2.Location.Y, label2.Width, label2.Height);
            OLable3 = new Rectangle(label3.Location.X, label3.Location.Y, label3.Width, label3.Height);
            OLable4 = new Rectangle(label4.Location.X, label4.Location.Y, label4.Width, label4.Height);
            OLable5 = new Rectangle(label5.Location.X, label5.Location.Y, label5.Width, label5.Height);
            OLable6 = new Rectangle(label6.Location.X, label6.Location.Y, label6.Width, label6.Height);
            OLable7 = new Rectangle(label7.Location.X, label7.Location.Y, label7.Width, label7.Height);
            OLable8 = new Rectangle(label8.Location.X, label8.Location.Y, label8.Width, label8.Height);
            OComboBox1 = new Rectangle(comboBox1.Location.X,comboBox1.Location.Y,comboBox1.Width,comboBox1.Height);
            OComboBox2 = new Rectangle(comboBox2.Location.X,comboBox2.Location.Y,comboBox2.Width,comboBox2.Height);
            OComboBox3 = new Rectangle(comboBox3.Location.X,comboBox3.Location.Y,comboBox3.Width,comboBox3.Height);
            OLenght = new Rectangle(Lenght.Location.X,Lenght.Location.Y,Lenght.Width,Lenght.Height);
            OWidth = new Rectangle(Width.Location.X,Width.Location.Y,Width.Width,Width.Height);
            OGrames = new Rectangle(Grames.Location.X,Grames.Location.Y,Grames.Width,Grames.Height);
            ONumber = new Rectangle(Number.Location.X,Number.Location.Y,Number.Width,Number.Height);
            OWaight = new Rectangle(Weight.Location.X, Weight.Location.Y, Weight.Width, Weight.Height);
            OName = new Rectangle(Name.Location.X,Name.Location.Y,Name.Width,Name.Height);
            OtextBox1 = new Rectangle(textBox1.Location.X,textBox1.Location.Y,textBox1.Width,textBox1.Height);
            OtextBox2 = new Rectangle(textBox2.Location.X,textBox2.Location.Y,textBox2.Width,textBox2.Height);
            ODateTime1 = new Rectangle(dateTimePicker1.Location.X,dateTimePicker1.Location.Y,dateTimePicker1.Width,dateTimePicker1.Height);
            ODataGridVeiw1 = new Rectangle(dataGridView1.Location.X, dataGridView1.Location.Y, dataGridView1.Width, dataGridView1.Height);
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
        public int Cheak()
        {
            if (comboBox2.SelectedIndex <= 0)
            {
                ErrorMessageBox("يجب ان تختار واحدة من نوع الورق");
                return 0;
            }
            if (Lenght.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب الطول");
                return 0;
            }
            if (Width.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب العرض");
                return 0;
            }
            if (Grames.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب الجرامات");
                return 0;
            }
            if (Name.Text == "")
            {
                ErrorMessageBox("يجب ان تكتب اسم المورد");
                return 0;
            }
            if (Number.Text != "" && Weight.Text == "")
            {

            }
            else if (Number.Text == "" && Weight.Text != "")
            {

            }
            else
            {
                ErrorMessageBox("يجب ان تكتب { واحدة فقط } وزن الورق او عدد الاوراق");
                return 0;
            }
            if (comboBox3.SelectedIndex <= 0)
            {
                ErrorMessageBox("يجب ان تكتب المخزن");
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
            float l, w, g, n = -1, s = -1, t;
            if (Cheak() == 1)
            {
                l = float.Parse(Lenght.Text);
                w = float.Parse(Width.Text);
                g = float.Parse(Grames.Text);
                if (Number.Text != "" && Weight.Text == "")
                {
                    n = float.Parse(Number.Text);
                    s = (n) * (l / 100) * (w / 100) * (g / 100);
                    s /= 10;
                }
                else if (Number.Text == "" && Weight.Text != "")
                {
                    s = float.Parse(Weight.Text);
                    n = (s * 10) / ((l / 100) * (w / 100) * (g / 100));
                }
                string Code = comboBox2.Text;
                string store = comboBox3.Text;
                string OrderName = textBox2.Text;
                string OrderNumber = textBox1.Text;
                DialogResult a = MessageBox.Show("سيتم اضافة " + n + " ورقة ",
                    "تاكيد ",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2,
                    MessageBoxOptions.RtlReading);
                if (a.ToString() != "OK")
                    return;
                con.Open();
                cmd = new SqlCommand("Select Number, Weight from Paper Where Code = N'" + Code + "' and Hight = " + l + " and Width = " + w + " and Grames = " + g + " and Store = N'" + store + "'", con);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    float num = float.Parse((string)rdr["Number"]);
                    num += n;
                    s = (num) * (l / 100) * (w / 100) * (g / 100);
                    s /= 10;
                    cmd = new SqlCommand("Update Paper Set Number = " + num + ", Weight = " + s + " Where Code = N'" + Code + "' and Hight = " + l + " and Width = " + w + " and Grames = " + g + " and Store = N'" + store + "'", con);
                }
                else
                {
                    cmd = new SqlCommand("Insert Into Paper Values (N'" + Code + "', " + g + ", " + l + ", " + w + ", " + n + ", " + s + ", N'" + store + "')", con);
                }
                rdr.Close();
                cmd.ExecuteNonQuery();
                string name = Name.Text;
                string date = dateTimePicker1.Text;
                date = GetFormatedDate(date);
                s = (n) * (l / 100) * (w / 100) * (g / 100);
                s /= 10;
                cmd = new SqlCommand("Insert Into TranPaper Values (N'" + Menu.User_Name + "',N'اضافة',N'" + name + "','" + date + "',N'" + Code + "', " + g + ", " + l + ", " + w + ", " + n + ", " + s + ", N'" + store + "', '" + OrderNumber + "', N'" + OrderName + "')", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show
                (
                    "لقد تم تخزين الورق الجديد بنجاح",
                    "نجح",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading
                );
                Lenght.Text = "";
                Width.Text = "";
                Grames.Text = "";
                Number.Text = "";
                Weight.Text = "";
                Name.Text = "";
                textBox1.Text = "";
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            string Command = "Select * from Paper ";
            int Temp = 0;
            if (comboBox2.SelectedIndex > 0)
            {
                if (Temp == 0)
                {
                    Command += "Where Code = N'" + comboBox2.Text + "' ";
                    Temp = 1;
                }
                else
                {
                    Command += "and Code = N'" + comboBox2.Text + "' ";
                }
            }
            if (Lenght.Text != "")
            {
                if (Temp == 0)
                {
                    Command += "Where Hight = " + Lenght.Text + " ";
                    Temp = 1;
                }
                else
                {
                    Command += "and Hight = " + Lenght.Text + " ";
                }
            }
            if (Width.Text != "")
            {
                if (Temp == 0)
                {
                    Command += "Where Width = " + Width.Text + " ";
                    Temp = 1;
                }
                else
                {
                    Command += "and Width = " + Width.Text + " ";
                }
            }
            if (Grames.Text != "")
            {
                if (Temp == 0)
                {
                    Command += "Where Grames = " + Grames.Text + " ";
                    Temp = 1;
                }
                else
                {
                    Command += "and Grames = " + Grames.Text + " ";
                }
            }
            if (Number.Text != "")
            {
                if (Temp == 0)
                {
                    Command += "Where Number = " + Number.Text + " ";
                    Temp = 1;
                }
                else
                {
                    Command += "and Number = " + Number.Text + " ";
                }
            }
            if (Weight.Text != "")
            {
                if (Temp == 0)
                {
                    Command += "Where Weight = " + Weight.Text + " ";
                    Temp = 1;
                }
                else
                {
                    Command += "and Weight = " + Weight.Text + " ";
                }
            }
            if (comboBox3.SelectedIndex > 0)
            {
                if (Temp == 0)
                {
                    Command += "Where Store = N'" + comboBox3.Text + "' ";
                    Temp = 1;
                }
                else
                {
                    Command += "and Store = N'" + comboBox3.Text + "' ";
                }
            }
            con.Open();
            cmd = new SqlCommand(Command);
            cmd.Connection = con;
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ErrorMessageBox("لا يوجد من الورق الذى حدته");
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
            Lenght.Text = "";
            Width.Text = "";
            Grames.Text = "";
            Number.Text = "";
            Weight.Text = "";
            Name.Text = "";
            textBox1.Text = "";
        }
        private void Take()
        {
            float l, w, g, n = -1, s = -1, t;
            if (Cheak() == 1)
            {
                if (textBox1.Text == "")
                {
                    ErrorMessageBox("يجب ان تكتب معلومات لماذا سيتم السحب");
                    return;
                }
                l = float.Parse(Lenght.Text);
                w = float.Parse(Width.Text);
                g = float.Parse(Grames.Text);
                if (Number.Text != "" && Weight.Text == "")
                {
                    n = float.Parse(Number.Text);
                    s = (n) * (l / 100) * (w / 100) * (g / 100);
                    s /= 10;
                }
                else if (Number.Text == "" && Weight.Text != "")
                {
                    s = float.Parse(Weight.Text);
                    n = (s * 10) / ((l / 100) * (w / 100) * (g / 100));
                }
                string Code = comboBox2.Text;
                string stor = comboBox3.Text;
                string OrderNumber = textBox1.Text;
                string OrderName = textBox2.Text;
                con.Open();
                cmd = new SqlCommand("Select Number, Weight from Paper Where Code = N'" + Code + "' and Hight = " + l + " and Width = " + w + " and Grames = " + g + " and Store = N'" + stor + "'", con);
                SqlDataReader rdr = cmd.ExecuteReader();
                float num;
                if (rdr.HasRows)
                {
                    rdr.Read();
                    num = float.Parse((string)rdr["Number"]);
                    if (num < n)
                    {
                        ErrorMessageBox("لا يوجد ورق كافي من هذا النوع");
                        con.Close();
                        rdr.Close();
                        return;
                    }
                }
                else
                {
                    ErrorMessageBox("لا يوجد ورق هذا النوع");
                    con.Close();
                    rdr.Close();
                    return;
                }
                rdr.Close();
                con.Close();
                DialogResult a = MessageBox.Show("سيتم سحب " + n + " ورقة ",
                    "تاكيد ",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2,
                    MessageBoxOptions.RtlReading);
                if (a.ToString() != "OK")
                    return;
                con.Open();
                num -= n;
                s = (num) * (l / 100) * (w / 100) * (g / 100);
                s /= 10;
                if (num == 0)
                {
                    cmd = new SqlCommand("Delete From Paper Where Code = N'" + Code + "' and Hight = " + l + " and Width = " + w + " and Grames = " + g + " and Store = N'" + stor + "'", con);
                }
                else
                {
                    cmd = new SqlCommand("Update Paper Set Number = " + num + ", Weight = " + s + " Where Code = N'" + Code + "' and Hight = " + l + " and Width = " + w + " and Grames = " + g + " and Store = N'" + stor + "'", con);
                }
                cmd.ExecuteNonQuery();
                string name = Name.Text;
                string date = dateTimePicker1.Text;
                date = GetFormatedDate(date);
                s = (n) * (l / 100) * (w / 100) * (g / 100);
                s /= 10;
                cmd = new SqlCommand("Insert Into TranPaper Values (N'" + Menu.User_Name + "',N'سحب',N'" + name + "','" + date + "',N'" + Code + "', " + g + ", " + l + ", " + w + ", " + n + ", " + s + ", N'" + stor + "', N'" + OrderNumber + "', N'" + OrderName + "')", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(" تم السحب بنجاح ",
                   "نجح",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information,
                   MessageBoxDefaultButton.Button1,
                   MessageBoxOptions.RtlReading);
                Lenght.Text = "";
                Width.Text = "";
                Grames.Text = "";
                Number.Text = "";
                Weight.Text = "";
                Name.Text = "";
                textBox1.Text = "";
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string Command = "Select * From TranPaper ";
            int Temp = 0;
            if (comboBox1.SelectedIndex == 1)
            {
                if (Temp == 0)
                {
                    Command += "Where Type = N'اضافة' ";
                    Temp = 1;
                }
                else
                {
                    Command += "and Type = N'اضافة' ";
                }
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                if (Temp == 0)
                {
                    Command += "Where Type = N'سحب' ";
                    Temp = 1;
                }
                else
                {
                    Command += "and Type = N'سحب' ";
                }
            }
            if (Name.Text != "")
            {
                if (Temp == 0)
                {
                    Command += "Where Name = N'" + Name.Text + "' ";
                    Temp = 1;
                }
                else
                {
                    Command += "and Number = N'" + Name.Text + "' ";
                }
            }
            if (comboBox2.SelectedIndex > 0)
            {
                if (Temp == 0)
                {
                    Command += "Where Code = N'" + comboBox2.Text + "' ";
                    Temp = 1;
                }
                else
                {
                    Command += "and Code = N'" + comboBox2.Text + "' ";
                }
            }
            if (Lenght.Text != "")
            {
                if (Temp == 0)
                {
                    Command += "Where Hight = " + Lenght.Text + " ";
                    Temp = 1;
                }
                else
                {
                    Command += "and Hight = " + Lenght.Text + " ";
                }
            }
            if (Width.Text != "")
            {
                if (Temp == 0)
                {
                    Command += "Where Width = " + Width.Text + " ";
                    Temp = 1;
                }
                else
                {
                    Command += "and Width = " + Width.Text + " ";
                }
            }
            if (Grames.Text != "")
            {
                if (Temp == 0)
                {
                    Command += "Where Grames = " + Grames.Text + " ";
                    Temp = 1;
                }
                else
                {
                    Command += "and Grames = " + Grames.Text + " ";
                }
            }
            if (Number.Text != "")
            {
                if (Temp == 0)
                {
                    Command += "Where Number = " + Number.Text + " ";
                    Temp = 1;
                }
                else
                {
                    Command += "and Number = " + Number.Text + " ";
                }
            }
            if (Weight.Text != "")
            {
                if (Temp == 0)
                {
                    Command += "Where Weight = " + Weight.Text + " ";
                    Temp = 1;
                }
                else
                {
                    Command += "and Weight = " + Weight.Text + " ";
                }
            }
            if (comboBox3.SelectedIndex > 0)
            {
                if (Temp == 0)
                {
                    Command += "Where Store = N'" + comboBox3.Text + "' ";
                    Temp = 1;
                }
                else
                {
                    Command += "and Store = N'" + comboBox3.Text + "' ";
                }
            }
            if(textBox1.Text != "")
            {
                if (Temp == 0)
                {
                    Command += "Where OrderNumber = N'" + textBox1.Text + "' ";
                    Temp = 1;
                }
                else
                {
                    Command += "and OrderNumber = N'" + textBox1.Text + "' ";
                }
            }
            if(textBox2.Text != "")
            {
                if (Temp == 0)
                {
                    Command += "Where OrderName = N'" + textBox2.Text + "' ";
                    Temp = 1;
                }
                else
                {
                    Command += "and OrderName = N'" + textBox2.Text + "' ";
                }
            }
            con.Open();
            cmd = new SqlCommand(Command);
            cmd.Connection = con;
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ErrorMessageBox("لا يوجد من النقل الذى حدته");
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
            Lenght.Text = "";
            Width.Text = "";
            Grames.Text = "";
            Number.Text = "";
            Weight.Text = "";
            Name.Text = "";
            textBox1.Text = "";
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
            {
                Add();
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                Take();
            }
            else
            {
                ErrorMessageBox("يجب ان تكتب اضافة ام سحب");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string Command = "Select * from Paper ";
            con.Open();
            cmd = new SqlCommand(Command);
            cmd.Connection = con;
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ErrorMessageBox("لا يوجد من الورق الذى حدته");
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
            string Command = "Select * from TranPaper ";
            con.Open();
            cmd = new SqlCommand(Command);
            cmd.Connection = con;
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                ErrorMessageBox("لا يوجد من الورق الذى حدته");
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

        private void Lenght_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.'&&!char.IsControl(e.KeyChar)) e.Handled = true;

        }

        private void Width_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar)) e.Handled = true;

        }

        private void Grames_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar)) e.Handled = true;

        }

        private void Number_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar)) e.Handled = true;

        }

        private void Weight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar)) e.Handled = true;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar)) e.Handled = true;
        }

        private void Paper_Form_Resize(object sender, EventArgs e)
        {
            ResizeControls(Obtn1, button1);
            ResizeControls(Obtn2, button2);
            ResizeControls(Obtn3, button3);
            ResizeControls(Obtn4, button4);
            ResizeControls(Obtn5, button5);
            ResizeControls(OLable1, label1);
            ResizeControls(OLable2, label2);
            ResizeControls(OLable3, label3);
            ResizeControls(OLable4, label4);
            ResizeControls(OLable5, label5);
            ResizeControls(OLable6, label6);
            ResizeControls(OLable7, label7);
            ResizeControls(OLable8, label8);
            ResizeControls(OComboBox1, comboBox1);
            ResizeControls(OComboBox2, comboBox2);
            ResizeControls(OComboBox3, comboBox3);
            ResizeControls(OLenght,Lenght);
            ResizeControls(OName, Name);
            ResizeControls(ODataGridVeiw1, dataGridView1);
            ResizeControls(ODateTime1, dateTimePicker1);
            ResizeControls(OtextBox1, textBox1);
            ResizeControls(OtextBox2, textBox2);
            ResizeControls(OWaight,Weight);
            ResizeControls(OWidth, Width);
            ResizeControls(OGrames, Grames);
            ResizeControls(ONumber, Number);
        }
    }
}
/*
برستول كوشية ( 00 )
دوبلكس ظ\كرفت( 01 )
كوشية مط ( 02 )
كرافت بني ( 03 )
كرافت ابيض ( 04 )
طبع ( 05 )
 استيكر ورق ( 06 )
استيكر بلاستك ( 07 )
استيكر شفاف ( 08 )
سولفيت ( 09 )
تربلكس ( 10 )
فبريانو ابيض ( 11 )
فبريانو كريمي ( 12 )
كرستال ( 13 )
دوبلكس رومادي ( 14 )


 */