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
    /// Interaction logic for Thông_tin_chi_tiết_sản_phẩm.xaml
    /// </summary>
    public partial class Thông_tin_chi_tiết_sản_phẩm : Window
    {
        MainWindow main;
        Bán_hàng sell;
        public void Synchronized_Main(MainWindow mainWindow)
        {
            this.main = mainWindow;
            txtMaDetailSP.Text = main.txtMaSP.Text;
            txtTenDetailSP.Text = main.txtTenSP.Text;
            txtThuongHieuDetailSP.Text = main.txtThHieu.Text;
            txtXuatXuDetailSP.Text = main.txtNSX.Text;
            txtGiaDetailSP.Text = main.txtGia.Text;
            txtSoLuongDetailSP.Text = main.txtSoLuong.Text;
            imgDetailSP.Source = main.imgBox.Source;
            txtRAMDetailSP.Text = main.txtRAMSP.Text + "GB";
            txtChipDetailSP.Text = main.txtChipSP.Text;
            txtBoNhoDetailSP.Text = main.txtBoNhoSP.Text + "GB";
            txtCameraDetailSP.Text = main.txtCameraSP.Text + "MP";
            txtDungLuongDetailSP.Text = main.txtDungLuongSP.Text + "mAh";
        }
        public void Synchronized_Sell(Bán_hàng SellProduct)
        {
            this.sell = SellProduct;
            txtMaDetailSP.Text = sell.txtMaSP.Text;
            txtTenDetailSP.Text = sell.txtTenSP.Text;
            txtThuongHieuDetailSP.Text = sell.txtThHieu.Text;
            txtXuatXuDetailSP.Text = sell.txtNSX.Text;
            txtGiaDetailSP.Text = sell.txtGia.Text;
            txtSoLuongDetailSP.Text = sell.txtSoLuong.Text;
            imgDetailSP.Source = sell.imgBox.Source;
            txtRAMDetailSP.Text = sell.txtRAMSP.Text + "GB";
            txtChipDetailSP.Text = sell.txtChipSP.Text;
            txtBoNhoDetailSP.Text = sell.txtBoNhoSP.Text + "GB";
            txtCameraDetailSP.Text = sell.txtCameraSP.Text + "MP";
            txtDungLuongDetailSP.Text = sell.txtDungLuongSP.Text + "mAh";
        }
        
        public Thông_tin_chi_tiết_sản_phẩm()
        {
            InitializeComponent();
        }
    }
}
