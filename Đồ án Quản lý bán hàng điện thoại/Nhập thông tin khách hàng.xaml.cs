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
    /// Interaction logic for Nhập_thông_tin_khách_hàng.xaml
    /// </summary>
    public partial class Nhập_thông_tin_khách_hàng : Window
    {
        public Nhập_thông_tin_khách_hàng()
        {
            InitializeComponent();
        }
        public bool flag;
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            flag = true;
            if (txtHoTenCus_Input.Text == "" || txtGioiTinhCus_Input.Text == "" || txtNgaySinhCus_Input.Text == "" || txtSDTCus_Input.Text == "")
            {
                MessageBox.Show("Phải nhập đầy đủ thông tin", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    DateTime d = Convert.ToDateTime(txtNgaySinhCus_Input.Text);
                    this.Close();
                }
                catch { MessageBox.Show("Ngày tháng không hợp lệ", "Notification", MessageBoxButton.OK, MessageBoxImage.Information); }
            }

        }

        private void Huy_Click(object sender, RoutedEventArgs e)
        {
            flag = false;
            this.Close();
        }
    }
}
