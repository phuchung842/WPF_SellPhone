using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Diagnostics;


namespace Đồ_án_Quản_lý_bán_hàng_điện_thoại
{
    /// <summary>
    /// Interaction logic for Thông_tin_hóa_đơn_admin.xaml
    /// </summary>
    public partial class Thông_tin_hóa_đơn_admin : Window
    {
        public void FindDataGrid(string valuesfrom,string valuesto)
        {
            ConnectToSQL connectToSQL = new ConnectToSQL();
            SqlCommand command = new SqlCommand();
            command.Connection = connectToSQL.conn;
            command.CommandText = "SELECT * FROM HoaDon WHERE Today<= @valuesto and Today>=@valuesfrom";
            connectToSQL.connect();
            command.Parameters.AddWithValue("@valuesto", valuesto);
            command.Parameters.AddWithValue("@valuesfrom", valuesfrom);

            DataTable dataTable = new DataTable("HoaDon");
            SqlDataReader sqlDataReader = command.ExecuteReader();
            dataTable.Load(sqlDataReader);
            dataGrid.DataContext = dataTable;
            dataGrid.ItemsSource = dataTable.DefaultView;
            connectToSQL.conn.Close();
        }
        public void RefreshDataGrid()
        {
            ConnectToSQL connectTo = new ConnectToSQL();
            SqlCommand command = new SqlCommand();
            command.Connection = connectTo.conn;
            command.CommandText = "SELECT* FROM HoaDon";
            connectTo.connect();
            DataTable dataTable = new DataTable("HoaDon");
            SqlDataReader sqlDataReader = command.ExecuteReader();
            dataTable.Load(sqlDataReader);
            dataGrid.DataContext = dataTable;
            dataGrid.ItemsSource = dataTable.DefaultView;
            connectTo.conn.Close();
        }
        public Thông_tin_hóa_đơn_admin()
        {
            InitializeComponent();
            RefreshDataGrid();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult boxResult = MessageBox.Show("Exit ?", "Notification", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (boxResult == MessageBoxResult.OK)
            {
                this.Close();
            }
        }

        private void BtnDetailInfoBill_Click(object sender, RoutedEventArgs e)
        {
            string txtnew = txtTempMaHD.Text.Remove(6);
            string path = txtnew + ".pdf";
            Process.Start(path);
        }

        private void BtnThongKe_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime d1 = Convert.ToDateTime(datePickerFrom.Text);
                DateTime d2 = Convert.ToDateTime(datePickerTo.Text);

                bool flag = true;
                if (DateTime.Compare(Convert.ToDateTime(datePickerFrom.Text), Convert.ToDateTime(datePickerTo.Text)) > 0)
                {
                    lbLoiStart.Visibility = Visibility.Visible;
                    lbLoiEnd.Visibility = Visibility.Visible;
                    flag = false;
                }
                if (flag == true)
                {
                    
                    ConnectToSQL connectToSQL = new ConnectToSQL();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connectToSQL.conn;
                    command.CommandText = "SELECT SUM(Tong) FROM HoaDon WHERE Today<= @valuesto and Today>=@valuesfrom";
                    connectToSQL.connect();
                    command.Parameters.AddWithValue("@valuesto", datePickerTo.Text);
                    command.Parameters.AddWithValue("@valuesfrom", datePickerFrom.Text);
                    SqlDataReader sqlDataReader = command.ExecuteReader();
                    sqlDataReader.Read();
                    txtTongDoanhThu.Text = sqlDataReader[0].ToString();
                    connectToSQL.conn.Close();
                    FindDataGrid(datePickerFrom.Text, datePickerTo.Text);
                }
            }
            catch { MessageBox.Show("Ngày tháng không hợp lệ", "Notification", MessageBoxButton.OK, MessageBoxImage.Information); }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
            txtTongDoanhThu.Text = "";
            datePickerFrom.Text = "";
            datePickerTo.Text = "";
        }

        private void DatePickerFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            lbLoiStart.Visibility = Visibility.Hidden;
            lbLoiEnd.Visibility = Visibility.Hidden;
        }

        private void DatePickerTo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            lbLoiStart.Visibility = Visibility.Hidden;
            lbLoiEnd.Visibility = Visibility.Hidden;
        }
    }
}
