using System.Data.SqlClient;

namespace SetUp_Remon_Application
{

    public partial class Form1 : Form
    {
        public static Form1 This;
        public void ErrorMessageBox(string x)
        {
            MessageBox.Show(x,
                            "خطأ",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.RtlReading);
        }
        public Form1()
        {
            InitializeComponent();
            This = this;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        public void CreateDataBaseTable()
        {
            // LAPTOP-82NJIQUH
            // RemonApplication
            StreamReader File = new StreamReader("info.txt");
            string server = File.ReadLine();
            string Database = File.ReadLine();
            File.Close();
            SqlConnection con = new SqlConnection(@"Data Source=" + server + ";Initial Catalog=" + Database + ";Integrated Security=True;");
            con.Open();
            SqlCommand cmd = new SqlCommand
            (
                "Create Table Paper       \n" +
                "(                        \n" +
                "   Code nvarchar(200),   \n" +
                "   Grames nvarchar(200), \n" +
                "   Hight nvarchar(200),  \n" +
                "   Width nvarchar(200),  \n" +
                "   Number nvarchar(200), \n" +
                "   Weight nvarchar(200), \n" +
                "   Store nvarchar(200)   \n" +
                ");                       \n" +
                "Create Table Solofan     \n" +
                "(                        \n" +
                "   Code nvarchar(200),   \n" +
                "   Number nvarchar(200), \n" +
                "   Size nvarchar(200),   \n" +
                "   Store nvarchar(200)   \n" +
                ");                       \n" +
                "Create Table Oil         \n" +
                "(                        \n" +
                "   Code nvarchar(200),   \n" +
                "   Number nvarchar(200), \n" +
                "   Store nvarchar(200)   \n" +
                ");                       \n" +
                "Create Table TranPaper   \n" +
                "(                        \n" +
                "   MangerName nvarchar(200),\n"+
                "   Type nvarchar(200),   \n" +
                "   Name nvarchar(200),   \n" +
                "   Date nvarchar(200),   \n" +
                "   Code nvarchar(200),   \n" +
                "   Grames nvarchar(200), \n" +
                "   Hight nvarchar(200),  \n" +
                "   Width nvarchar(200),  \n" +
                "   Number nvarchar(200), \n" +
                "   Weight nvarchar(200), \n" +
                "   Store nvarchar(200),  \n" +
                "OrderNumber nvarchar(200),\n" +
                "OrderName nvarchar(200)  \n" +
                ");                       \n" +
                "Create Table TranSolofan \n" +
                "(                        \n" +
                "   MangerName nvarchar(200),\n" +
                "   Type nvarchar(200),   \n" +
                "   Name nvarchar(200),   \n" +
                "   Date nvarchar(200),   \n" +
                "   Code nvarchar(200),   \n" +
                "   Number nvarchar(200), \n" +
                "   Size nvarchar(200),   \n" +
                "   Store nvarchar(200),  \n" +
                "OrderNumber nvarchar(200),\n" +
                "  OrderName nvarchar(200) \n" +
                ");                       \n" +
                "Create Table TranOil     \n" +
                "(                        \n" +
                "   MangerName nvarchar(200),\n" +
                "   Type nvarchar(200),   \n" +
                "   Name nvarchar(200),   \n" +
                "   Date nvarchar(200),   \n" +
                "   Code nvarchar(200),   \n" +
                "   Number nvarchar(200), \n" +
                "   Store nvarchar(200),  \n" +
                "OrderNumber nvarchar(200),\n" +
                "OrderName nvarchar(200)  \n"+
                ");                       \n" +
                "Create Table Password    \n" +
                "(                        \n" +
                "   Name nvarchar(200),   \n" +
                "   code nvarchar(400)    \n" +
                ");                       \n" +
                "Create Table Company     \n" +
                "(                        \n" +
     "   Id int primary key identity(1,1),\n" +
                "   Name nvarchar(200)    \n" +
                ");                       \n" +
                "Create Table Invoices    \n" +
                "(                        \n" +
     "   Id int primary key identity(1,1),\n" +
                "   Name nvarchar(200),   \n" +
                "   Price nvarchar(200),  \n" +
                "   Tax nvarchar(200),    \n" +
                "   Total nvarchar(200),  \n" +
                "   Date nvarchar(200)    \n" +
                ");                       \n", con
            );
            cmd.ExecuteNonQuery();
            con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            String ServerName = Server.Text;
            String DatabaseName = Database.Text;
            if (string.IsNullOrEmpty(ServerName) || string.IsNullOrEmpty(DatabaseName))
            {
                ErrorMessageBox("يجب ان تكتب السرفر و قاعدة البيانات");
                return;
            }
            StreamWriter File = new StreamWriter("info.txt");
            File.WriteLine(ServerName);
            File.WriteLine(DatabaseName);
            File.Close();
            CreateDataBaseTable();
            MessageBox.Show("تم انشاء التطبيق بنجاح",
                           "تم",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Information,
                           MessageBoxDefaultButton.Button1,
                           MessageBoxOptions.RtlReading);
            
            this.Close();
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _ٍSign_Up signUp = new _ٍSign_Up();
            signUp.Show();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}