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

namespace Đồ_án_Quản_lý_bán_hàng_điện_thoại
{
    /// <summary>
    /// Interaction logic for Thông_tin_khách_hàng_admin.xaml
    /// </summary>
    public partial class Thông_tin_khách_hàng_admin : Window
    {
        public void RefreshDataGrid()
        {
            ConnectToSQL connectTo = new ConnectToSQL();
            SqlCommand command = new SqlCommand();
            command.Connection = connectTo.conn;
            command.CommandText = "SELECT* FROM [KhachHang]";
            connectTo.connect();
            DataTable dataTable = new DataTable("KhachHang");
            SqlDataReader sqlDataReader = command.ExecuteReader();
            dataTable.Load(sqlDataReader);
            dataGrid_InfoCustom_AD.DataContext = dataTable;
            dataGrid_InfoCustom_AD.ItemsSource = dataTable.DefaultView;
            connectTo.conn.Close();
        }
        public Thông_tin_khách_hàng_admin()
        {
            InitializeComponent();
            RefreshDataGrid();
        }

        private void BtnCancel_InfoCustom_AD_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult boxResult = MessageBox.Show("Exit ?", "Notification", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (boxResult == MessageBoxResult.OK)
            {
                this.Close();
            }
        }

        private void BtnUpdate_InfoCustom_AD_Click(object sender, RoutedEventArgs e)
        {
            if (txtMaKHInfoAd.Text != "" && txtHoTenKHInfoAd.Text != "" && txtGioiTinhKHInfoAd.Text != "" && txtNgaySinhKHInfoAd.Text != "" && txtSDTKHInfoAd.Text != "")
            {
                try
                {
                    DateTime d = Convert.ToDateTime(txtNgaySinhKHInfoAd.Text);
                    ConnectToSQL connectToSQL = new ConnectToSQL();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connectToSQL.conn;
                    command.CommandText = "UPDATE KhachHang SET HoTenKH=@hotenkh,GioiTinhKH=@gioitinhkh,NgaySinhKH=@ngaysinhkh,SDTKH=@sdtkh WHERE MaKH=@makh";
                    connectToSQL.connect();
                    command.Parameters.AddWithValue("@makh", txtMaKHInfoAd.Text);
                    command.Parameters.AddWithValue("@hotenkh", txtHoTenKHInfoAd.Text);
                    command.Parameters.AddWithValue("@gioitinhkh", txtGioiTinhKHInfoAd.Text);
                    command.Parameters.AddWithValue("@ngaysinhkh", txtNgaySinhKHInfoAd.Text);
                    command.Parameters.AddWithValue("@sdtkh", txtSDTKHInfoAd.Text);

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch
                    {
                        MessageBox.Show("Lỗi", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    connectToSQL.conn.Close();
                    RefreshDataGrid();
                }
                catch { MessageBox.Show("Ngày tháng không hợp lệ", "Notification", MessageBoxButton.OK, MessageBoxImage.Information); }
            }
            else
            {
                MessageBox.Show("Bạn phải điền đầy đủ thông tin hoặc chưa chọn khách hàng", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
