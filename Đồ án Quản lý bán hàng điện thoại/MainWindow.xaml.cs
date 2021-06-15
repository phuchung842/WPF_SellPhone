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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace Đồ_án_Quản_lý_bán_hàng_điện_thoại
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public void RefreshDataGrid()
        {
            ConnectToSQL connectTo = new ConnectToSQL();
            SqlCommand command = new SqlCommand();
            command.Connection = connectTo.conn;
            command.CommandText = "SELECT* FROM SanPham";
            connectTo.connect();
            DataTable dataTable = new DataTable("SanPham");
            SqlDataReader sqlDataReader = command.ExecuteReader();
            dataTable.Load(sqlDataReader);
            dataGrid.DataContext = dataTable;
            dataGrid.ItemsSource = dataTable.DefaultView;
            connectTo.conn.Close();
        }

        //List<SanPham> lstProduct = new List<SanPham>();

        //BitmapImage image = new BitmapImage();
        //public bool flagSync;
        public MainWindow()
        {
            InitializeComponent();
            //flagSync = true;
            RefreshDataGrid();
            //lstProduct.Add(new SanPham("IP_6", "Iphone6", "Apple", "Trung Quốc", 700, 100));
            //dataGrid.ItemsSource = lstProduct.ToList();
            //dataGrid.Items.Refresh();

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            //string sql;
            Nhập_thông_tin_sản_phẩm InfoProduct = new Nhập_thông_tin_sản_phẩm();
            InfoProduct.ShowDialog();
            if (InfoProduct.flagcheckinfoproduct == true)
            {
                if (InfoProduct.txtGia_Info.Text != "" || InfoProduct.txtMaSP_Info.Text != "" || InfoProduct.txtNSX_Info.Text != "" || InfoProduct.txtSoLuong_Info.Text != "" || InfoProduct.txtTenSP_Info.Text != "" || InfoProduct.txtThHieu_Info.Text != "")
                {
                    txtMaSP.Text = InfoProduct.txtMaSP_Info.Text;
                    txtTenSP.Text = InfoProduct.txtTenSP_Info.Text;
                    txtGia.Text = InfoProduct.txtGia_Info.Text;
                    txtNSX.Text = InfoProduct.txtNSX_Info.Text;
                    txtSoLuong.Text = InfoProduct.txtSoLuong_Info.Text;
                    txtThHieu.Text = InfoProduct.txtThHieu_Info.Text;
                    txtRAMSP.Text = InfoProduct.txtRam_Info.Text;
                    txtChipSP.Text = InfoProduct.txtChip_Info.Text;
                    txtBoNhoSP.Text = InfoProduct.txtBoNho_Info.Text;
                    txtCameraSP.Text = InfoProduct.txtCamera_Info.Text;
                    txtDungLuongSP.Text = InfoProduct.txtDungLuong_Info.Text;
                    txtimgsourceSP.Text = InfoProduct.txtSourceImg_Info.Text;
                    //txtfilename.Text = InfoProduct.filename;
                    //SanPham Product = new SanPham();
                    //Product.MaSanPham = txtMaSP.Text;
                    //Product.TenSanPham = txtTenSP.Text;
                    //Product.NhaSanXuatSanPham = txtNSX.Text;
                    //Product.GiaSanPham = Convert.ToDecimal(txtGia.Text);
                    //Product.SoLuongSanPham = Convert.ToInt32(txtSoLuong.Text);
                    //Product.ThuongHieuSanPham = txtThHieu.Text;

                    ConnectToSQL connectToSQL = new ConnectToSQL();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connectToSQL.conn;
                    command.CommandText = "INSERT INTO SanPham(MaSP,TenSP,ThuongHieu,XuatXu,Gia,SoLuong,ImgSourceSP,RAM,Chip,BoNho,Camera,DungLuong) VALUES(@masp,@tensp,@thuonghieu,@xuatxu,@gia,@soluong,@img,@ram,@chip,@bonho,@camera,@dungluong)";
                    connectToSQL.connect();
                    command.Parameters.AddWithValue("@masp", txtMaSP.Text);
                    command.Parameters.AddWithValue("@tensp", txtTenSP.Text);
                    command.Parameters.AddWithValue("@thuonghieu", txtThHieu.Text);
                    command.Parameters.AddWithValue("@xuatxu", txtNSX.Text);
                    command.Parameters.AddWithValue("@gia", Convert.ToDecimal(txtGia.Text));
                    command.Parameters.AddWithValue("@soluong", Convert.ToInt32(txtSoLuong.Text));
                    command.Parameters.AddWithValue("@img", InfoProduct.txtSourceImg_Info.Text);
                    command.Parameters.AddWithValue("@ram", Convert.ToDecimal(InfoProduct.txtRam_Info.Text));
                    command.Parameters.AddWithValue("@chip", InfoProduct.txtChip_Info.Text);
                    command.Parameters.AddWithValue("@bonho", Convert.ToDecimal(InfoProduct.txtBoNho_Info.Text));
                    command.Parameters.AddWithValue("@camera", Convert.ToDecimal(InfoProduct.txtCamera_Info.Text));
                    command.Parameters.AddWithValue("@dungluong", Convert.ToDecimal(InfoProduct.txtDungLuong_Info.Text));
                    //command.Parameters.AddWithValue("@filename", txtfilename.Text);

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
                    //sql = "INSERT INTO SanPham (MaSP,TenSP,ThuongHieu,XuatXu,GiaTien,SoLuong)VALUES(" + txtMaSP.Text + ",'" + txtTenSP + "','" + txtThHieu.Text + "','" + txtNSX.Text + "','" + txtGia.Text + "','" + txtSoLuong.Text + "' )";
                    //conn.Open();
                    //command = new SqlCommand(sql, conn);
                    ////command.Parameters.Add();
                    //int x = command.ExecuteNonQuery();
                    //conn.Close();
                    //MessageBox.Show(x.ToString() + "record(s) saved.");
                    //lstProduct.Add(Product);
                    //dataGrid.ItemsSource = lstProduct.ToList();
                    //dataGrid.Items.Refresh();
                }
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtMaSP.Text != "")
            {
                Cập_nhật_sản_phẩm UpdateProduct = new Cập_nhật_sản_phẩm(this);
                UpdateProduct.ShowDialog();
                if (UpdateProduct.flagcheckupdateproduct == true)
                {
                    if (UpdateProduct.txtGia_Update.Text != "" || UpdateProduct.txtSoLuong_Update.Text != "")
                    {
                        //SanPham ProductUpdate = lstProduct.Find(m => m.MaSanPham == UpdateProduct.txtMaSP_Update.Text);
                        //ProductUpdate.MaSanPham = UpdateProduct.txtMaSP_Update.Text;
                        //ProductUpdate.TenSanPham = UpdateProduct.txtTenSP_Update.Text;
                        //ProductUpdate.ThuongHieuSanPham = UpdateProduct.txtThHieu_Update.Text;
                        //ProductUpdate.NhaSanXuatSanPham = UpdateProduct.txtNSX_Update.Text;
                        //ProductUpdate.GiaSanPham = Convert.ToDecimal(UpdateProduct.txtGia_Update.Text);
                        //ProductUpdate.SoLuongSanPham = Convert.ToInt32(UpdateProduct.txtSoLuong_Update.Text);

                        ConnectToSQL connectToSQL = new ConnectToSQL();
                        SqlCommand command = new SqlCommand();
                        command.Connection = connectToSQL.conn;
                        command.CommandText = "UPDATE SanPham SET TenSP=@tensp,ThuongHieu=@thuonghieu,XuatXu=@xuatxu,Gia=@gia,SoLuong=@soluong,ImgSourceSP=@img,RAM=@ram,Chip=@chip,BoNho=@bonho,Camera=@camera,DungLuong=@dungluong WHERE MaSP=@masp";
                        connectToSQL.connect();
                        command.Parameters.AddWithValue("@masp", this.txtMaSP.Text);
                        command.Parameters.AddWithValue("@tensp", UpdateProduct.txtTenSP_Update.Text);
                        command.Parameters.AddWithValue("@thuonghieu", UpdateProduct.txtThHieu_Update.Text);
                        command.Parameters.AddWithValue("@xuatxu", UpdateProduct.txtNSX_Update.Text);
                        command.Parameters.AddWithValue("@gia", Convert.ToDecimal(UpdateProduct.txtGia_Update.Text));
                        command.Parameters.AddWithValue("@soluong", Convert.ToInt32(UpdateProduct.txtSoLuong_Update.Text));
                        command.Parameters.AddWithValue("@img", UpdateProduct.txtSourceImg_Update.Text);
                        command.Parameters.AddWithValue("@ram", Convert.ToDecimal(UpdateProduct.txtRAM_Update.Text));
                        command.Parameters.AddWithValue("@chip", UpdateProduct.txtChip_Update.Text);
                        command.Parameters.AddWithValue("@bonho", Convert.ToDecimal(UpdateProduct.txtBoNho_Update.Text));
                        command.Parameters.AddWithValue("@camera", Convert.ToDecimal(UpdateProduct.txtCamera_Update.Text));
                        command.Parameters.AddWithValue("@dungluong", Convert.ToDecimal(UpdateProduct.txtDungLuong_Update.Text));
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
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn sản phẩm cập nhật.", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
            }               
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn muốn xóa sản phẩm ?", "Notification", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                ConnectToSQL connectToSQL = new ConnectToSQL();
                SqlCommand command = new SqlCommand();
                command.Connection = connectToSQL.conn;
                command.CommandText = "DELETE FROM SanPham WHERE MaSP=@masp";
                connectToSQL.connect();
                command.Parameters.AddWithValue("@masp", this.txtMaSP.Text);
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
            //SanPham ProductRemove = lstProduct.Find(m => m.MaSanPham == this.txtMaSP.Text);
            //this.lstProduct.Remove(ProductRemove);
            //this.dataGrid.ItemsSource = lstProduct.ToList();
            //this.dataGrid.Items.Refresh();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult boxResult = MessageBox.Show("Exit ?", "Notification", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (boxResult == MessageBoxResult.OK)
            {
                this.Close();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                BtnExit_Click(sender, e);
        }

        public string checking;
        public void FindDataGrid(string checking,string values)
        {
            ConnectToSQL connectToSQL = new ConnectToSQL();
            SqlCommand command = new SqlCommand();
            command.Connection = connectToSQL.conn;
            command.CommandText = "SELECT * FROM SanPham WHERE "+checking+" = @values";
            connectToSQL.connect();
            command.Parameters.AddWithValue("@values", values);

            DataTable dataTable = new DataTable("SanPham");
            SqlDataReader sqlDataReader = command.ExecuteReader();
            dataTable.Load(sqlDataReader);
            dataGrid.DataContext = dataTable;
            dataGrid.ItemsSource = dataTable.DefaultView;
            connectToSQL.conn.Close();

        }
        private void BtnFind_Click(object sender, RoutedEventArgs e)
        {
            switch (checking)
            {
                case "MaSP":
                    FindDataGrid(checking, txtFind.Text);
                    break;
                case "TenSP":
                    FindDataGrid(checking, txtFind.Text);
                    break;
                case "ThuongHieu":
                    FindDataGrid(checking,txtFind.Text);
                    break;
                case "XuatXu":
                    FindDataGrid(checking, txtFind.Text);
                    break;
                case "Gia":
                    FindDataGrid(checking, txtFind.Text);
                    break;
                case "All":
                    RefreshDataGrid();
                    break;
            }

        }

        private void RadioBtnMaSP_Click(object sender, RoutedEventArgs e)
        {
            checking = "MaSP";
            txtFind.Text = "";
        }

        private void RadioBtnTenSP_Click(object sender, RoutedEventArgs e)
        {
            checking = "TenSP";
            txtFind.Text = "";
        }

        private void RadioBtnThuongHieu_Click(object sender, RoutedEventArgs e)
        {
            checking = "ThuongHieu";
            txtFind.Text = "";
        }

        private void RadioBtnXuatXu_Click(object sender, RoutedEventArgs e)
        {
            checking = "XuatXu";
            txtFind.Text = "";
        }

        private void RadioBtnGia_Click(object sender, RoutedEventArgs e)
        {
            checking = "Gia";
            txtFind.Text = "";
        }

        private void RadioBtnTatCa_Click(object sender, RoutedEventArgs e)
        {
            checking = "All";
            txtFind.Text = "";
        }

        private void BtnDetailCustom_Click(object sender, RoutedEventArgs e)
        {
            Thông_tin_khách_hàng_admin InfoCusAd = new Thông_tin_khách_hàng_admin();
            InfoCusAd.ShowDialog();
        }

        private void BtnDetailEmployee_Click(object sender, RoutedEventArgs e)
        {
            Thông_tin_nhân_viên_admin InfoEmpAd = new Thông_tin_nhân_viên_admin();
            InfoEmpAd.ShowDialog();

        }

        private void BtnDetail_Click(object sender, RoutedEventArgs e)
        {
            if (txtMaSP.Text != "")
            {
                Thông_tin_chi_tiết_sản_phẩm InfoDetailProduct = new Thông_tin_chi_tiết_sản_phẩm();
                InfoDetailProduct.Synchronized_Main(this);
                InfoDetailProduct.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn sản phẩm", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnStatistic_Click(object sender, RoutedEventArgs e)
        {
            Thông_tin_hóa_đơn_admin InfoBillAD = new Thông_tin_hóa_đơn_admin();
            InfoBillAD.ShowDialog();
        }
    }
}
