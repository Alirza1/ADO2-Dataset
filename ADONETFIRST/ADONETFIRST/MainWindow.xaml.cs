using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ADONETFIRST
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Author
        {
            public object Id;
            public object Name;
            public object SurName;
        }
        public Author authors { get; set; }
        SqlDataAdapter adapter;
        DataSet dataSet;
        SqlConnection con;
        public MainWindow()
        {
            InitializeComponent();



        }

        private void Button_Click(object sender, RoutedEventArgs e)//showall
        {
            this.DataContext = this;
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
                conn.Open();
                adapter = new SqlDataAdapter();
                dataSet = new DataSet();


                adapter = new SqlDataAdapter("SELECT * FROM Authors", conn);

                adapter.Fill(dataSet);
                listview1.ItemsSource = dataSet.Tables[0].DefaultView;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            listview1.Items.Clear();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
                conn.Open();
                adapter = new SqlDataAdapter();
                dataSet = new DataSet();

                adapter = new SqlDataAdapter(@"INSERT INTO Authors(Id,FirstName,LastName)
                VALUES(@id,@firstName,@lastName)", conn);

                SqlCommand sqlCommand = new SqlCommand("insert into Authors(Id,FirstName,LastName) VALUES(@id,@name,@surname) ", con);
                sqlCommand.Parameters.Add(new SqlParameter()
                {
                    SqlDbType = SqlDbType.Int,
                    ParameterName = "@id",
                    Value = int.Parse(textbox1.Text)
                });
                sqlCommand.Parameters.Add(new SqlParameter()
                {
                    SqlDbType = SqlDbType.NVarChar,
                    ParameterName = "@name",
                    Value = textbox2.Text
                });
                sqlCommand.Parameters.Add(new SqlParameter()
                {
                    SqlDbType = SqlDbType.NVarChar,
                    ParameterName = "@surname",
                    Value = textbox3.Text
                });

                adapter.InsertCommand = sqlCommand;
                adapter.InsertCommand.ExecuteNonQuery();
                adapter.Fill(dataSet);
            }

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
                conn.Open();
                adapter = new SqlDataAdapter();
                dataSet = new DataSet();

                adapter = new SqlDataAdapter(@"select * from Authors", conn);

                SqlCommand sqlCommand = new SqlCommand("delete Authors" +
                    " where Id=@id");

                sqlCommand.Parameters.Add(new SqlParameter()
                {
                    SqlDbType = SqlDbType.Int,
                    ParameterName = "@id",
                    Value = int.Parse(textbox1.Text)
                });


                adapter.DeleteCommand = sqlCommand;
                adapter.DeleteCommand.ExecuteNonQuery();
                adapter.Fill(dataSet);
            }
        }
    }
}
