using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using Path = System.IO.Path;

namespace Đồ_án_Quản_lý_bán_hàng_điện_thoại
{
    /// <summary>
    /// Interaction logic for Nhập_thông_tin_sản_phẩm.xaml
    /// </summary>
    public partial class Nhập_thông_tin_sản_phẩm : Window
    {
        public Nhập_thông_tin_sản_phẩm()
        {
            InitializeComponent();
        }
        public bool flagcheckinfoproduct = true;
        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            flagcheckinfoproduct = true;
            if (txtGia_Info.Text == "" || txtMaSP_Info.Text == "" || txtNSX_Info.Text == "" || txtSoLuong_Info.Text == "" || txtTenSP_Info.Text == "" || txtThHieu_Info.Text == ""||txtRam_Info.Text==""||txtChip_Info.Text==""||txtBoNho_Info.Text==""||txtCamera_Info.Text==""||txtDungLuong_Info.Text=="")
            {
                MessageBox.Show("Phải nhập đầy đủ thông tin", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                bool flaggia = false;
                try
                {
                    int gia = int.Parse(txtGia_Info.Text);
                    flaggia = true;
                }
                catch
                { MessageBox.Show("Giá phải nhập số", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error); }

                bool flagsoluong = false;
                try
                {
                    int soluong = int.Parse(txtSoLuong_Info.Text);
                    flagsoluong = true;
                }
                catch
                { MessageBox.Show("Số lượng phải nhập số", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error); }

                bool flagram = false;
                try
                {
                    decimal ram = Convert.ToDecimal(txtRam_Info.Text);
                    flagram = true;
                }
                catch
                { MessageBox.Show("RAM phải nhập số", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error); }

                bool flagbonho = false;
                try
                {
                    decimal bonho = Convert.ToDecimal(txtBoNho_Info.Text);
                    flagbonho = true;
                }
                catch
                { MessageBox.Show("Bộ nhớ phải nhập số", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error); }

                bool flagcamera = false;
                try
                {
                    decimal camera = Convert.ToDecimal(txtCamera_Info.Text);
                    flagcamera = true;
                }
                catch
                { MessageBox.Show("Camera phải nhập số", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error); }

                bool flagdungluong = false;
                try
                {
                    decimal dungluong = Convert.ToDecimal(txtDungLuong_Info.Text);
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

        private void BtnHuy_Click(object sender, RoutedEventArgs e)
        {
            flagcheckinfoproduct = false;
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                BtnOK_Click(sender, e);
        }

        //public byte[] _imageBytes = null;

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


            //cần có button
            //var outPutDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            //var relativepath = System.IO.Path.Combine(outPutDirectory, "imgPhone\\"+filename);
            //string relative_path = new Uri(relativepath).LocalPath;
            filename = Path.GetFileName(dialog.FileName);
            txtSourceImg_Info.Text = "ImageProduct/" + filename;
            imgInputInfo.Source = new BitmapImage(new Uri(dialog.FileName));
           
            //BinaryReader br = new BinaryReader(fs);
            //_imageBytes = br.ReadBytes((int)fs.Length);

            //using (var fs = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read))
            //{
            //    _imageBytes = new byte[fs.Length];
            //    fs.Read(_imageBytes, 0, System.Convert.ToInt32(fs.Length));
            //}
            //txtSourceImg.Text = _imageBytes.ToString();
        }
    }
}
