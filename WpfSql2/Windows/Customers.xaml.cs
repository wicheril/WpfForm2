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
using WpfSql2.Forms;

namespace WpfSql2
{
    /// <summary>
    /// Логика взаимодействия для Customers.xaml
    /// </summary>
    public partial class Customers : Window
    {
        Connection connection;
        public Customers()
        {
            connection = new Connection();
            connection.OpenCon();
            InitializeComponent();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            UpdateTable();
        }

        private void UpdateTable()
        {
            string sql = "SElECT * FROM public.\"Customers\"";
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, connection.Connect);
            DataTable data = new DataTable();
            adapter.Fill(data);
            CustomersView.ItemsSource = data.DefaultView;
           // CustomersView.Columns[0].Visibility = Visibility.Hidden;
        }

        private void LoadForm_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateTable();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddCustomer addCustomer = new AddCustomer();
            addCustomer.Show();
            UpdateTable();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var cellInfo = CustomersView.SelectedCells[0];
            var content = (cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text;
            int id = Convert.ToInt32(content);
            string sql = $"delete from \"Customers\" where \"IdCs\" = '{id}'";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection.Connect);
            if (cmd.ExecuteNonQuery() == 1)
            {
                UpdateTable();
                MessageBox.Show("Клиент успешно удален");
            }
            else MessageBox.Show("Что-то пошло не так");
        }

        private void getCurrentCell(object sender, MouseButtonEventArgs e)
        {
            /*var cellInfo = CustomersView.SelectedCells[0];
            var content = (cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text;
            MessageBox.Show(content);*/
        }
    }
}
