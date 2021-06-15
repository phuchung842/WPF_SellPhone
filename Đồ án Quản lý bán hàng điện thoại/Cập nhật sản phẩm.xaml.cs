using Microsoft.Win32;
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
using System.IO;
using Path = System.IO.Path;
using System.Reflection;

namespace Đồ_án_Quản_lý_bán_hàng_điện_thoại
{
    /// <summary>
    /// Interaction logic for Cập_nhật_sản_phẩm.xaml
    /// </summary>
    public partial class Cập_nhật_sản_phẩm : Window
    {
        MainWindow main;
        public Cập_nhật_sản_phẩm(MainWindow mainWindow)
        {
            InitializeComponent();
            this.main = mainWindow;
            txtMaSP_Update.Text = main.txtMaSP.Text;
            txtTenSP_Update.Text = main.txtTenSP.Text;
            txtThHieu_Update.Text = main.txtThHieu.Text;
            txtNSX_Update.Text = main.txtNSX.Text;
            txtGia_Update.Text = main.txtGia.Text;
            txtSoLuong_Update.Text = main.txtSoLuong.Text;
            imgInputUpdate.Source = main.imgBox.Source;
            txtSourceImg_Update.Text = main.txtimgsourceSP.Text;
            //try
            //{
            //    string name = Path.GetFileName(main.txtimgsourceSP.Text);
            //    string a = Path.GetFullPath("imgPhone/" + name);
            //    imgInputUpdate.Source = new BitmapImage(new Uri(a));
            //    txtSourceImg_Update.Text = main.txtimgsourceSP.Text;
            //}
            //catch { }
            txtRAM_Update.Text = main.txtRAMSP.Text;
            txtChip_Update.Text = main.txtChipSP.Text;
            txtBoNho_Update.Text = main.txtBoNhoSP.Text;
            txtCamera_Update.Text = main.txtCameraSP.Text;
            txtDungLuong_Update.Text = main.txtDungLuongSP.Text;
        }

        public bool flagcheckupdateproduct = true;
        private void BtnOK_Up_Click(object sender, RoutedEventArgs e)
        {
            flagcheckupdateproduct = true;
            if (txtGia_Update.Text == "" || txtSoLuong_Update.Text == ""||txtRAM_Update.Text==""||txtChip_Update.Text==""||txtBoNho_Update.Text==""||txtCamera_Update.Text==""||txtDungLuong_Update.Text=="")
            {
                MessageBox.Show("Điền đầy đũ thông tin", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                txtTenSP_Update.Text = main.txtTenSP.Text;
                txtThHieu_Update.Text = main.txtThHieu.Text;
                txtNSX_Update.Text = main.txtNSX.Text;
                txtGia_Update.Text = main.txtGia.Text;
                txtSoLuong_Update.Text = main.txtSoLuong.Text;
                txtRAM_Update.Text = main.txtRAMSP.Text;
                txtChip_Update.Text = main.txtChipSP.Text;
                txtBoNho_Update.Text = main.txtBoNhoSP.Text;
                txtCamera_Update.Text = main.txtCameraSP.Text;
                txtDungLuong_Update.Text = main.txtDungLuongSP.Text;
            }
            else
            {
                bool flaggia = false;
                try
                {
                    int gia = int.Parse(txtGia_Update.Text);
                    flaggia = true;
                }
                catch
                { MessageBox.Show("Giá phải nhập số", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error); }

                bool flagsoluong = false;
                try
                {
                    int soluong = int.Parse(txtSoLuong_Update.Text);
                    flagsoluong = true;
                }
                catch
                { MessageBox.Show("Số lượng phải nhập số", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error); }

                bool flagram = false;
                try
                {
                    decimal ram = Convert.ToDecimal(txtRAM_Update.Text);
                    flagram = true;
                }
                catch
                { MessageBox.Show("RAM phải nhập số", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error); }

                bool flagbonho = false;
                try
                {
                    decimal bonho = Convert.ToDecimal(txtBoNho_Update.Text);
                    flagbonho = true;
                }
                catch
                { MessageBox.Show("Bộ nhớ phải nhập số", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error); }

                bool flagcamera = false;
                try
                {
                    decimal camera = Convert.ToDecimal(txtCamera_Update.Text);
                    flagcamera = true;
                }
                catch
                { MessageBox.Show("Camera phải nhập số", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error); }

                bool flagdungluong = false;
                try
                {
                    decimal dungluong = Convert.ToDecimal(txtDungLuong_Update.Text);
                    flagdungluong = true;
                }
                catch
                { MessageBox.Show("Dung lượng phải nhập số", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error); }

                if (flaggia == true && flagsoluong == true && flagram == true && flagbonho == true && flagcamera == true && flagdungluong == true)
                {
                    this.Close();
                }
            }
        }

        private void BtnHuy_Up_Click(object sender, RoutedEventArgs e)
        {
            flagcheckupdateproduct = false;
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                BtnOK_Up_Click(sender, e);
        }

        public string filename;
        private void BtnBrowse_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Multiselect = false,
                Filter = "Image (*.jpg,*.png|*.jpg;*.png|All Files(*.*)|*.*"
            };

            if (dialog.ShowDialog() != true) { return; }
            //string filename = Path.GetFileName(dialog.FileName);
            //var outPutDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            //var relativepath = System.IO.Path.Combine(outPutDirectory, "imgPhone\\" + filename);
            //string relative_path = new Uri(relativepath).LocalPath;
            this.filename = Path.GetFileName(dialog.FileName);
            txtSourceImg_Update.Text = "ImageProduct/" + filename;
            imgInputUpdate.Source = new BitmapImage(new Uri(dialog.FileName));
        }
    }
}
