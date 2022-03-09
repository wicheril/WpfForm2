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
using WpfSql2.Windows;

namespace WpfSql2.Forms
{
    /// <summary>
    /// Логика взаимодействия для AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Window
    {
        Connection connection;
        public AddOrder()
        {
            connection = new Connection();
            connection.OpenCon();
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (CommodityBox.Text != "" && NumberBox.Text != "" && LimitBox.Text != "")
            {
                string sql = $"insert into \"Orders\" (\"Commodity\", \"Number\", \"Limit\")" +
                             $"values ('{CommodityBox.Text}','{NumberBox.Text}','{LimitBox.Text}')";
                var adap = new NpgsqlDataAdapter(sql, connection.Connect);
                DataTable data = new DataTable();
                adap.Fill(data);
                MessageBox.Show("Заказ добавлен!");
                this.Close();
            }
            else MessageBox.Show("Что-то пошло не так");
        }
    }
}
