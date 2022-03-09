using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace WpfSql2.Forms
{
    /// <summary>
    /// Логика взаимодействия для AddProvider.xaml
    /// </summary>
    public partial class AddProvider : Window
    {
        Connection connection;
        public AddProvider()
        {
            connection = new Connection();
            connection.OpenCon();
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (CommodityBox.Text != "" && NameBox.Text != "" && PriceBox.Text != "" && PhoneBox.Text != "" && AdressBox.Text != "")
            {
                string sql = $"insert into \"Providers\" (\"Commodity\", \"Name\", \"Address\", \"Price\", \"TelephonPr\")" +
                             $"values ('{CommodityBox.Text}','{NameBox.Text}','{AdressBox.Text}','{PriceBox.Text}','{PhoneBox.Text}')";
                var adap = new NpgsqlDataAdapter(sql, connection.Connect);
                DataTable data = new DataTable();
                adap.Fill(data);
                MessageBox.Show("Provider добавлен!");
                this.Close();
            }
            else MessageBox.Show("Что-то пошло не так");
        }
    }
}
