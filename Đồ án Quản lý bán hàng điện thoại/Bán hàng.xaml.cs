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
using System.Data.SqlClient;
using System.Data;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.factories;
using iTextSharp.text.pdf.parser;
using System.IO;
using Paragraph = iTextSharp.text.Paragraph;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace Đồ_án_Quản_lý_bán_hàng_điện_thoại
{
    /// <summary>
    /// Interaction logic for Bán_hàng.xaml
    /// </summary>
    public partial class Bán_hàng : Window
    {
        public void RefreshDataGrid()
        {
            ConnectToSQL connectTo = new ConnectToSQL();
            SqlCommand command = new SqlCommand();
            command.Connection = connectTo.conn;
            command.CommandText = "SELECT* FROM [SanPham]";
            connectTo.connect();
            DataTable dataTable = new DataTable("SanPham");
            SqlDataReader sqlDataReader = command.ExecuteReader();
            dataTable.Load(sqlDataReader);
            dataGrid.DataContext = dataTable;
            dataGrid.ItemsSource = dataTable.DefaultView;
            connectTo.conn.Close();
        }

        System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();
        private void Time_Click(object sender, EventArgs e)
        {
            DateTime d;
            d = DateTime.Now;
            txtbloNgay.Text = "Date : " + d.Month + "/" + d.Day + "/" + d.Year + "\n" + "Time : " + d.Hour + ":" + d.Minute + ":" + d.Second;
        }
        Đăng_nhập login;
        public string manv;
        public Bán_hàng(Đăng_nhập LoginReal)
        {
            this.login = LoginReal;
            InitializeComponent();
            Timer.Tick += new EventHandler(Time_Click);
            Timer.Interval = new TimeSpan(0, 0, 0);
            Timer.Start();
            RefreshDataGrid();
            txtbloNgay.Text = DateTime.Now.ToString();

            ConnectToSQL connectToSQL = new ConnectToSQL();
            SqlCommand command = new SqlCommand();
            command.Connection = connectToSQL.conn;
            command.CommandText = "SELECT MaNV,HoTenNV,GioiTinhNV,NgaySinhNV,SDTNV FROM NhanVien WHERE MaNV = @values";
            connectToSQL.connect();
            command.Parameters.AddWithValue("@values", login.txtUser.Text.Remove(0,3)+"   ");
            SqlDataReader sqlDataReader = command.ExecuteReader();
            sqlDataReader.Read();
            manv = sqlDataReader[0].ToString();
            txtblMaNV.Text = "Mã nhân viên :"+sqlDataReader[0].ToString();
            txtblHoTenNV.Text = "Họ tên :"+sqlDataReader[1].ToString();
            txtblGioiTinhNV.Text = "Giới tính :"+sqlDataReader[2].ToString();
            txtblNgaySinhNV.Text = "Ngày sinh :"+sqlDataReader[3].ToString();
            txtblSDTNV.Text = "SĐT :" + sqlDataReader[4].ToString();
            connectToSQL.conn.Close();
        }
        public string checking;
        public void FindDataGrid(string checking, string values)
        {
            ConnectToSQL connectToSQL = new ConnectToSQL();
            SqlCommand command = new SqlCommand();
            command.Connection = connectToSQL.conn;
            command.CommandText = "SELECT * FROM SanPham WHERE " + checking + " = @values";
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
                    FindDataGrid(checking, txtFind.Text);
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

        private void LbDetail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (txtMaSP.Text != "")
            {
                Thông_tin_chi_tiết_sản_phẩm InfoDetailProduct = new Thông_tin_chi_tiết_sản_phẩm();
                InfoDetailProduct.Synchronized_Sell(this);
                InfoDetailProduct.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn sản phẩm", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static int slsp = 0;       
        public static int j = 1;
        public string[] A = new string[slsp];
        public decimal[] AThanhTien = new decimal[slsp];
        public int[] ASoLuongBan = new int[slsp];
        public string[] AMaSP = new string[slsp];
        public int[] vitri = new int[j];
        public int i = -1, k = 0;
        public decimal ThanhTien = 0;
        public decimal Tong = 0;
        public int soluong;

        private void BtnChoose_Click(object sender, RoutedEventArgs e)
        {
            ConnectToSQL connectToSQL3 = new ConnectToSQL();
            SqlCommand command3 = new SqlCommand();
            command3.Connection = connectToSQL3.conn;
            command3.CommandText = "SELECT SoLuong FROM SanPham WHERE MaSP = @values";
            connectToSQL3.connect();
            command3.Parameters.AddWithValue("@values", txtMaSP.Text);
            SqlDataReader sqlDataReader3 = command3.ExecuteReader();
            sqlDataReader3.Read();
            soluong = Convert.ToInt32(sqlDataReader3[0]);
            int comsl = Convert.ToInt32(txtSoLuongBanHang.Text);

            if (txtSoLuongBanHang.Text == "")
            {
                MessageBox.Show("Nhập số lượng bán", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (comsl > soluong)
            {
                MessageBox.Show("Không đủ số lượng sản phẩm", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                //Array
                slsp++;
                j++;
                //phần tử array
                i++;
                k++;
                Array.Resize(ref A, slsp);
                Array.Resize(ref AThanhTien, slsp);
                Array.Resize(ref ASoLuongBan, slsp);
                Array.Resize(ref AMaSP, slsp);
                Array.Resize(ref vitri, j);
                ASoLuongBan[i] = Convert.ToInt32(txtSoLuongBanHang.Text);
                AMaSP[i] = txtMaSP.Text;
                AThanhTien[i] = (Convert.ToDecimal(txtGia.Text) * Convert.ToDecimal(txtSoLuongBanHang.Text));//dùng mảng để lưu lại thành tiền
                A[i] = "___________Sản phẩm :" + (i + 1).ToString() + "___________" + "\nMã sản phẩm :" + txtMaSP.Text + "\nTên sản phẩm :" + txtTenSP.Text + "\nThương Hiệu :" + txtThHieu.Text + "\nXuất Xứ :" + txtNSX.Text + "\nGiá tiền :" + txtGia.Text + "\nSố lượng :" + txtSoLuongBanHang.Text + "\nThành tiền :" + AThanhTien[i].ToString() + "\n" + "\n";
                txtblAllProduct.Text = txtblAllProduct.Text + A[i];
                Tong = Tong + AThanhTien[i];
                txtblSum.Text = "Tổng :" + Tong.ToString();
                vitri[0] = 0;
                vitri[k] = txtblAllProduct.Text.Length;

            }
            txtSoLuongBanHang.Text = "";
        }
        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            k--;
            if (k >= 0)
            {
                if (txtblAllProduct.Text != "")
                {
                    Tong = Tong - AThanhTien[i];//trừ đúng thành tiền của mỗi sản phẩm
                    txtblSum.Text = "Tổng :" + Tong.ToString();
                    if(Tong==0)
                    {
                        txtblSum.Text = "";
                    }
                    txtblAllProduct.Text = txtblAllProduct.Text.Remove(vitri[k]);
                }
                else
                {
                    MessageBox.Show("Chưa chọn sản phẩm", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Không còn sản phẩm nào", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            i--;
            slsp--;
            j--;
            Array.Resize(ref A, slsp);
            Array.Resize(ref AThanhTien, slsp);
            Array.Resize(ref ASoLuongBan, slsp);
            Array.Resize(ref AMaSP, slsp);
            Array.Resize(ref vitri, j);
        }

        public string makh = "";
        public string hoten = "";
        public string gioitinh = "";
        public string ngaysinh = "";
        public string sdt = "";
        public bool flagcheckcus = false;
        private void BtnInfoCustom_Click(object sender, RoutedEventArgs e)
        {
            Thông_tin_khách_hàng_admin InfoCusAd = new Thông_tin_khách_hàng_admin();
            Nhập_thông_tin_khách_hàng InputCusInfo = new Nhập_thông_tin_khách_hàng();
            InputCusInfo.ShowDialog();

            //bool flagtest = true;
            //ConnectToSQL connectToSQLtest = new ConnectToSQL();
            //SqlCommand command2 = new SqlCommand();
            ////SqlCommand command3 = new SqlCommand();
            //command2.Connection = connectToSQLtest.conn;
            //command2.CommandText = "SELECT MaKH FROM KhachHang WHERE HoTenKH like N'@hoten'";
            //connectToSQLtest.connect();
            //command2.Parameters.AddWithValue("@hoten", InputCusInfo.txtHoTenCus_Input.Text);
            ////command2.Parameters.AddWithValue("@gioitinh", InputCusInfo.txtGioiTinhCus_Input.Text);
            //try
            //{
            //    SqlDataReader sqlDataReader2 = command2.ExecuteReader();
            //    sqlDataReader2.Read();
            //    makh = sqlDataReader2[0].ToString();
            //    hoten = InputCusInfo.txtHoTenCus_Input.Text;
            //    gioitinh = InputCusInfo.txtGioiTinhCus_Input.Text;
            //    ngaysinh = InputCusInfo.txtNgaySinhCus_Input.Text;
            //    sdt = InputCusInfo.txtSDTCus_Input.Text;
            //    txtblMaKH.Text = "Mã khách hàng :" + makh;
            //    txtblHoTenKH.Text = "Họ tên :" + hoten;
            //    txtblGioiTinhKH.Text = "Giới tính :" + gioitinh;
            //    txtblNgaySinhKH.Text = "Ngày sinh :" + ngaysinh;
            //}
            //catch
            //{
            //    flagtest = false;
            //}

            if (InputCusInfo.flag == true)
            {
                flagcheckcus = true;
                int count = 0;
                count = InfoCusAd.dataGrid_InfoCustom_AD.Items.Count;
                string chuoi = "";
                if ((count + 1) < 10)
                {
                    chuoi = "KH000" + (count + 1).ToString();
                }
                else if ((count) < 100)
                {
                    chuoi = "KH00" + (count + 1).ToString();
                }
                else if ((count) < 1000)
                {
                    chuoi = "KH0" + (count + 1).ToString();
                }
                else
                {
                    chuoi = "KH" + (count + 1).ToString();
                }
                makh = chuoi;
                hoten = InputCusInfo.txtHoTenCus_Input.Text;
                gioitinh = InputCusInfo.txtGioiTinhCus_Input.Text;
                ngaysinh = InputCusInfo.txtNgaySinhCus_Input.Text;
                sdt = InputCusInfo.txtSDTCus_Input.Text;
                txtblMaKH.Text = "Mã khách hàng :" + makh;
                txtblHoTenKH.Text = "Họ tên :" + hoten;
                txtblGioiTinhKH.Text = "Giới tính :" + gioitinh;
                txtblNgaySinhKH.Text = "Ngày sinh :" + ngaysinh;
                Thông_tin_khách_hàng_admin ad = new Thông_tin_khách_hàng_admin();
                ad.RefreshDataGrid();
            }
        }

        private void BtnFixInfoCustom_Click(object sender, RoutedEventArgs e)
        {
            if (flagcheckcus == true)
            {
                Xem_và_Sửa_thông_tin_khách_hàng xem_Và_Sửa_Thông_Tin_Khách_Hàng = new Xem_và_Sửa_thông_tin_khách_hàng(this);
                xem_Và_Sửa_Thông_Tin_Khách_Hàng.ShowDialog();
                if (xem_Và_Sửa_Thông_Tin_Khách_Hàng.flag == true)
                {
                    //ConnectToSQL connectToSQL = new ConnectToSQL();
                    //SqlCommand command = new SqlCommand();
                    //command.Connection = connectToSQL.conn;
                    //command.CommandText = "UPDATE KhachHang SET HoTenKH=@hoten,GioiTinhKH=@gioitinh,NgaySinhKH=@ngaysinh,SDTKH=@sdt WHERE MaKH=@makh";
                    //connectToSQL.connect();
                    //command.Parameters.AddWithValue("@makh", xem_Và_Sửa_Thông_Tin_Khách_Hàng.txtMaCus_Update.Text);
                    //command.Parameters.AddWithValue("@hoten", xem_Và_Sửa_Thông_Tin_Khách_Hàng.txtHoTenCus_Update.Text);
                    //command.Parameters.AddWithValue("@gioitinh", xem_Và_Sửa_Thông_Tin_Khách_Hàng.txtGioiTinhCus_Update.Text);
                    //command.Parameters.AddWithValue("@ngaysinh", xem_Và_Sửa_Thông_Tin_Khách_Hàng.txtNgaySinhCus_Update.Text);
                    //command.Parameters.AddWithValue("@sdt", xem_Và_Sửa_Thông_Tin_Khách_Hàng.txtSDTCus_Update.Text);
                    //try
                    //{
                    //    command.ExecuteNonQuery();
                    //}
                    //catch
                    //{
                    //    MessageBox.Show("Lỗi", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                    //}
                    //connectToSQL.conn.Close();
                    DateTime d = Convert.ToDateTime(xem_Và_Sửa_Thông_Tin_Khách_Hàng.txtNgaySinhCus_Update.Text);
                    makh = xem_Và_Sửa_Thông_Tin_Khách_Hàng.txtMaCus_Update.Text;
                    hoten = xem_Và_Sửa_Thông_Tin_Khách_Hàng.txtHoTenCus_Update.Text;
                    gioitinh = xem_Và_Sửa_Thông_Tin_Khách_Hàng.txtGioiTinhCus_Update.Text;
                    ngaysinh = xem_Và_Sửa_Thông_Tin_Khách_Hàng.txtNgaySinhCus_Update.Text;
                    sdt = xem_Và_Sửa_Thông_Tin_Khách_Hàng.txtSDTCus_Update.Text;
                    txtblMaKH.Text = "Mã khách hàng :" + makh;
                    txtblHoTenKH.Text = "Họ tên :" + hoten;
                    txtblGioiTinhKH.Text = "Giới tính :" + gioitinh;
                    txtblNgaySinhKH.Text = "Ngày sinh :" + ngaysinh;
                    Thông_tin_khách_hàng_admin ad = new Thông_tin_khách_hàng_admin();
                    ad.RefreshDataGrid();
                }
            }
            else
            { MessageBox.Show("Chưa nhập thông tin khách hàng", "Notification", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void BtnBill_Click(object sender, RoutedEventArgs e)
        {

            if (txtblAllProduct.Text == "" || makh == "")
            {
                MessageBox.Show("Chưa nhập đầy đủ thông tin", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult boxResult = MessageBox.Show("Bạn muốn xuất hóa đơn ?", "Notification", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (boxResult == MessageBoxResult.OK)
                {
                    bool flag = false;
                    //xử lý số lượng sản phẩm
                    for (int i = 0; i < A.Length; i++)
                    {
                        ConnectToSQL connectToSQL3 = new ConnectToSQL();
                        SqlCommand command3 = new SqlCommand();
                        command3.Connection = connectToSQL3.conn;
                        command3.CommandText = "SELECT SoLuong FROM SanPham WHERE MaSP = @values";
                        connectToSQL3.connect();
                        command3.Parameters.AddWithValue("@values", AMaSP[i]);
                        SqlDataReader sqlDataReader3 = command3.ExecuteReader();
                        sqlDataReader3.Read();
                        soluong = Convert.ToInt32(sqlDataReader3[0]);
                        bool flagsl = true;
                        if (soluong > ASoLuongBan[i])
                        {
                            soluong = soluong - ASoLuongBan[i];
                            flag = true;
                        }
                        else
                        {
                            flagsl = false;
                            MessageBox.Show(AMaSP[i] + "không đủ số lượng sản phẩm", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        connectToSQL3.conn.Close();

                        if (flagsl == true)
                        {
                            ConnectToSQL connectToSQL4 = new ConnectToSQL();
                            SqlCommand command4 = new SqlCommand();
                            command4.Connection = connectToSQL4.conn;
                            command4.CommandText = "UPDATE SanPham SET SoLuong=@values WHERE MaSP = @masp";
                            connectToSQL4.connect();
                            command4.Parameters.AddWithValue("@masp", AMaSP[i]);
                            command4.Parameters.AddWithValue("@values", soluong);
                            command4.ExecuteNonQuery();
                            connectToSQL4.conn.Close();
                        }
                        RefreshDataGrid();
                    }

                    if (flag == true)
                    {
                        Thông_tin_hóa_đơn_admin InfoDetailBill = new Thông_tin_hóa_đơn_admin();
                        int count = 0;
                        count = InfoDetailBill.dataGrid.Items.Count;
                        string chuoihd = "";
                        if ((count) < 10)
                        {
                            chuoihd = "HD000" + (count).ToString();
                        }
                        else if ((count) < 100)
                        {
                            chuoihd = "HD00" + (count).ToString();
                        }
                        else if ((count) < 1000)
                        {
                            chuoihd = "HD0" + (count).ToString();
                        }
                        else
                        {
                            chuoihd = "HD" + (count).ToString();
                        }

                        ConnectToSQL connectToSQL = new ConnectToSQL();
                        SqlCommand command = new SqlCommand();
                        command.Connection = connectToSQL.conn;
                        command.CommandText = "INSERT INTO HoaDon(MaHD,MaNV_HD,MaKH_HD,NgayHD,Tong,Today) VALUES(@mahd,@manv,@makh,@ngayhd,@tonghd,@today)";
                        connectToSQL.connect();
                        command.Parameters.AddWithValue("@mahd", chuoihd);
                        command.Parameters.AddWithValue("@manv", manv);
                        command.Parameters.AddWithValue("@makh", makh);
                        command.Parameters.AddWithValue("@ngayhd", txtbloNgay.Text);
                        command.Parameters.AddWithValue("@tonghd", Tong);
                        command.Parameters.AddWithValue("@today", DateTime.Now.ToString());
                        command.ExecuteNonQuery();
                        connectToSQL.conn.Close();

                        ConnectToSQL connectToSQL2 = new ConnectToSQL();
                        SqlCommand command2 = new SqlCommand();
                        command2.Connection = connectToSQL2.conn;
                        command2.CommandText = "INSERT INTO KhachHang(MaKH,HoTenKH,GioiTinhKH,NgaySinhKH,SDTKH) VALUES(@makh,@hotenkh,@gioitinhkh,@ngaysinhkh,@sdtkh)";
                        connectToSQL2.connect();
                        command2.Parameters.AddWithValue("@makh", makh);
                        command2.Parameters.AddWithValue("@hotenkh", hoten);
                        command2.Parameters.AddWithValue("@gioitinhkh", gioitinh);
                        command2.Parameters.AddWithValue("@ngaysinhkh", ngaysinh);
                        command2.Parameters.AddWithValue("@sdtkh", sdt);
                        try
                        {
                            command2.ExecuteNonQuery();
                        }
                        catch
                        {
                            MessageBox.Show("Lỗi", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        connectToSQL2.conn.Close();

                        //xuất hóa đơn
                        Document doc = new Document(iTextSharp.text.PageSize.LETTER, 20, 20, 20, 20);
                        string path = chuoihd + ".pdf";

                        FileStream fs = new FileStream(path, FileMode.Create);
                        PdfWriter pdfWriter = PdfWriter.GetInstance(doc, fs);
                        //chuyển thành unicode
                        string Arial = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "Arial.ttf");
                        BaseFont bf = BaseFont.CreateFont(Arial, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                        Font f = new Font(bf, 13, Font.NORMAL);
                        doc.Open();
                        //BaseFont baseFont = BaseFont.CreateFont("Abc", BaseFont.IDENTITY_H, false);
                        //Font font = new Font(baseFont, 12, Font.NORMAL);

                        Paragraph paragraph1 = new Paragraph("HÓA ĐƠN \n\n", f);
                        Paragraph paragraph = new Paragraph(chuoihd, f);
                        Paragraph paragraph2 = new Paragraph(txtbloNgay.Text + "\n", f);
                        Paragraph paragraph3 = new Paragraph("Nhân viên :\n" + txtblMaNV.Text + "\n" + txtblHoTenNV.Text + "\n" + txtblGioiTinhNV.Text + "\n" + txtblNgaySinhNV.Text + "\n" + txtblSDTNV.Text + "\n\n", f);
                        Paragraph paragraph4 = new Paragraph("Khách hàng :\n" + txtblMaKH.Text + "\n" + txtblHoTenKH.Text + "\n" + txtblGioiTinhKH.Text + "\n" + txtblNgaySinhKH.Text + "\n" + txtblSDTKH.Text + "\n\n", f);
                        Paragraph paragraph5 = new Paragraph("Sản phẩm : \n" + txtblAllProduct.Text + "\n" + txtblSum.Text, f);
                        doc.Add(paragraph1);
                        doc.Add(paragraph2);
                        doc.Add(paragraph3);
                        doc.Add(paragraph4);
                        doc.Add(paragraph5);
                        doc.Close();
                        Process.Start(path);
                        txtblMaKH.Text = "";
                        txtblHoTenKH.Text = "";
                        txtblGioiTinhKH.Text = "";
                        txtblNgaySinhKH.Text = "";
                        txtblSDTKH.Text = "";
                        txtblAllProduct.Text = "";
                        txtblSum.Text = "";
                        txtSoLuongBanHang.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Không đủ số lượng bán hàng của sản phẩm");
                    }
                }
            }
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult boxResult = MessageBox.Show("Exit ?", "Notification", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (boxResult == MessageBoxResult.OK)
            {
                this.Close();
            }
        }

        
    }
}
