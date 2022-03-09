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
    /// Логика взаимодействия для AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : Window
    {
        Connection connection;
        public AddCustomer()
        {
            connection = new Connection();
            connection.OpenCon();
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (FamilyBox.Text != "" && AdresBox.Text != "" && BalanceBox.Text != "" && PhoneBox.Text != "")
            {
                string sql = $"insert into \"Customers\" (\"Family\", \"Address\", \"Balance\", \"TelephonCs\")" +
                             $"values ('{FamilyBox.Text}','{AdresBox.Text}','{BalanceBox.Text}','{PhoneBox.Text}')";
                var adap = new NpgsqlDataAdapter(sql, connection.Connect);
                DataTable data = new DataTable();
                adap.Fill(data);
                MessageBox.Show("Клиент добавлен!");
                this.Close();
            }
            else MessageBox.Show("Что-то пошло не так");
        }
    }
}
