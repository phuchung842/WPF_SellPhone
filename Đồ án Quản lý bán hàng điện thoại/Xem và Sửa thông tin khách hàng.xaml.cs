using System;
using System.Collections.Generic;
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
    /// Interaction logic for Xem_và_Sửa_thông_tin_khách_hàng.xaml
    /// </summary>
    public partial class Xem_và_Sửa_thông_tin_khách_hàng : Window
    {
        Bán_hàng bán_Hàng;
        public Xem_và_Sửa_thông_tin_khách_hàng(Bán_hàng bán)
        {
            InitializeComponent();
            this.bán_Hàng = bán;
            txtMaCus_Update.Text = bán_Hàng.makh;
            txtHoTenCus_Update.Text = bán_Hàng.hoten;
            txtGioiTinhCus_Update.Text = bán_Hàng.gioitinh;
            txtNgaySinhCus_Update.Text = bán_Hàng.ngaysinh;
            txtSDTCus_Update.Text = bán_Hàng.sdt;
        }

        public bool flag;
        private void BtnFix_Click(object sender, RoutedEventArgs e)
        {

            flag = true;
            if (txtHoTenCus_Update.Text == "" || txtGioiTinhCus_Update.Text == "" || txtNgaySinhCus_Update.Text == "" || txtSDTCus_Update.Text == "")
            {
                MessageBox.Show("Điền đầy đũ thông tin", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                txtHoTenCus_Update.Text = bán_Hàng.hoten;
                txtGioiTinhCus_Update.Text = bán_Hàng.gioitinh;
                txtNgaySinhCus_Update.Text = bán_Hàng.ngaysinh;
                txtSDTCus_Update.Text = bán_Hàng.sdt;
            }
            else
            {
                try
                {
                    DateTime d = Convert.ToDateTime(txtNgaySinhCus_Update.Text);
                    this.Close();
                }
                catch { MessageBox.Show("Ngày tháng không hợp lệ", "Notification", MessageBoxButton.OK, MessageBoxImage.Information); }

            }

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            flag = false;
            this.Close();
        }
    }
}
