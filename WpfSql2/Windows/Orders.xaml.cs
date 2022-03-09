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

namespace WpfSql2.Windows
{
    /// <summary>
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : Window
    {
        Connection connection;
        public Orders()
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
            string sql = "SElECT * FROM public.\"Orders\"";
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, connection.Connect);
            DataTable data = new DataTable();
            adapter.Fill(data);
            OrdersView.ItemsSource = data.DefaultView;
            //OrdersView.Columns[0].Visibility = Visibility.Hidden;
        }

        private void LoadForm_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateTable();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var addOrder = new AddOrder();
            addOrder.Show();
            UpdateTable();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var cellInfo = OrdersView.SelectedCells[0];
            var content = (cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text;
            int id = Convert.ToInt32(content);
            MessageBox.Show(id.ToString());
            string sql = $"delete from \"Orders\" where \"IdCs\" = '{id}'";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection.Connect);
            if (cmd.ExecuteNonQuery() == 1)
            {
                UpdateTable();
                MessageBox.Show("Заказ успешно удален");
            }
            else MessageBox.Show("Что-то пошло не так");
        }
    }
}
