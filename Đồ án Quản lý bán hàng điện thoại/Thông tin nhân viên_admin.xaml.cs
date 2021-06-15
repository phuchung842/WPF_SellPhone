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
    /// Interaction logic for Thông_tin_nhân_viên_admin.xaml
    /// </summary>
    public partial class Thông_tin_nhân_viên_admin : Window
    {
        public void RefreshDataGrid()
        {
            ConnectToSQL connectTo = new ConnectToSQL();
            SqlCommand command = new SqlCommand();
            command.Connection = connectTo.conn;
            command.CommandText = "SELECT* FROM [NhanVien]";
            connectTo.connect();
            DataTable dataTable = new DataTable("NhanVien");
            SqlDataReader sqlDataReader = command.ExecuteReader();
            dataTable.Load(sqlDataReader);
            dataGrid_InfoEmployee_AD.DataContext = dataTable;
            dataGrid_InfoEmployee_AD.ItemsSource = dataTable.DefaultView;
            connectTo.conn.Close();
        }
        public Thông_tin_nhân_viên_admin()
        {
            InitializeComponent();
            RefreshDataGrid();
        }

        public bool flagInfoEmpInput;
        private void RdbtnAddEmp_Checked(object sender, RoutedEventArgs e)
        {
            flagInfoEmpInput = true;
            txtMa.Text = "";
            txtHoTen.Text = "";
            txtGioiTinh.Text = "";
            txtNgaySinh.Text = "";
            txtSDT.Text = "";
            txtCMND.Text = "";
            txtDiaChi.Text = "";
        }

        private void RdbtnUpdateEmp_Checked(object sender, RoutedEventArgs e)
        {
            flagInfoEmpInput = false;
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (flagInfoEmpInput == true)
            {
                if (txtMa.Text != "" || txtHoTen.Text != "" || txtGioiTinh.Text != "" || txtNgaySinh.Text != "" || txtSDT.Text != "" || txtCMND.Text != "" || txtDiaChi.Text != "")
                {
                    try
                    {
                        DateTime d = Convert.ToDateTime(txtNgaySinh.Text);

                        ConnectToSQL connectToSQL = new ConnectToSQL();
                        SqlCommand command = new SqlCommand();
                        command.Connection = connectToSQL.conn;
                        command.CommandText = "INSERT INTO NhanVien(MaNV,HoTenNV,GioiTinhNV,NgaySinhNV,SDTNV,CMNDNV,DiaChiNV) VALUES(@manv,@hotennv,@gioitinhnv,@ngaysinhnv,@sdtnv,@cmndnv,@diachinv)";
                        connectToSQL.connect();
                        command.Parameters.AddWithValue("@manv", txtMa.Text);
                        command.Parameters.AddWithValue("@hotennv", txtHoTen.Text);
                        command.Parameters.AddWithValue("@gioitinhnv", txtGioiTinh.Text);
                        command.Parameters.AddWithValue("@ngaysinhnv", txtNgaySinh.Text);
                        command.Parameters.AddWithValue("@sdtnv", txtSDT.Text);
                        command.Parameters.AddWithValue("@cmndnv", txtCMND.Text);
                        command.Parameters.AddWithValue("@diachinv", txtDiaChi.Text);
                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch
                        {
                            MessageBox.Show("Lỗi", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        connectToSQL.conn.Close();

                        string tk = "TK_" + txtMa.Text;
                        string mk = "MK_" + txtMa.Text;
                        ConnectToSQL connectToSQL2 = new ConnectToSQL();
                        SqlCommand command2 = new SqlCommand();
                        command2.Connection = connectToSQL2.conn;
                        command2.CommandText = "INSERT INTO Key_NV(TK_NV_MaNV,MK_NV) VALUES(@tk,@mk)";
                        connectToSQL2.connect();
                        command2.Parameters.AddWithValue("@tk", tk);
                        command2.Parameters.Add("@mk", mk);
                        //command2.Parameters["@mk"].Value = mk;
                        try
                        {
                            command2.ExecuteNonQuery();
                        }
                        catch
                        {
                            MessageBox.Show("Lỗi", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        connectToSQL2.conn.Close();
                    }
                    catch { MessageBox.Show("Ngày tháng không hợp lệ", "Notification", MessageBoxButton.OK, MessageBoxImage.Information); }
                    RefreshDataGrid();
                }
                else
                {
                    MessageBox.Show("Bạn phải điền đủ thông tin", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {

                if (txtMa.Text != "" || txtHoTen.Text != "" || txtGioiTinh.Text != "" || txtNgaySinh.Text != "" || txtSDT.Text != "" || txtCMND.Text != "" || txtDiaChi.Text != "")
                {
                    try
                    {
                        DateTime d = Convert.ToDateTime(txtNgaySinh.Text);

                        ConnectToSQL connectToSQL = new ConnectToSQL();
                        SqlCommand command = new SqlCommand();
                        command.Connection = connectToSQL.conn;
                        command.CommandText = "UPDATE NhanVien SET HoTenNV=@hotennv,GioiTinhNV=@gioitinhnv,NgaySinhNV=@ngaysinhnv,SDTNV=@sdtnv,CMNDNV=@cmndnv,DiaChiNV=@diachinv WHERE MaNV=@manv";
                        connectToSQL.connect();
                        command.Parameters.AddWithValue("@manv", txtMa.Text);
                        command.Parameters.AddWithValue("@hotennv", txtHoTen.Text);
                        command.Parameters.AddWithValue("@gioitinhnv", txtGioiTinh.Text);
                        command.Parameters.AddWithValue("@ngaysinhnv", txtNgaySinh.Text);
                        command.Parameters.AddWithValue("@sdtnv", txtSDT.Text);
                        command.Parameters.AddWithValue("@cmndnv", txtCMND.Text);
                        command.Parameters.AddWithValue("@diachinv", txtDiaChi.Text);
                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch
                        {
                            MessageBox.Show("Lỗi, không tìm thấy nhân viên", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        connectToSQL.conn.Close();
                        RefreshDataGrid();
                    }
                    catch { MessageBox.Show("Ngày tháng không hợp lệ", "Notification", MessageBoxButton.OK, MessageBoxImage.Information); }
                }
                else
                {
                    MessageBox.Show("Bạn chưa chọn nhân viên", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult boxResult = MessageBox.Show("Exit ?", "Notification", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (boxResult == MessageBoxResult.OK)
            {
                this.Close();
            }
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult boxResult = MessageBox.Show("Remove ?", "Notification", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (boxResult == MessageBoxResult.OK)
            {
                ConnectToSQL connectToSQL = new ConnectToSQL();
                SqlCommand command = new SqlCommand();
                command.Connection = connectToSQL.conn;
                command.CommandText = "DELETE FROM NhanVien WHERE MaNV=@manv";
                connectToSQL.connect();
                command.Parameters.AddWithValue("@manv", this.txtMa.Text);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("Lỗi", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                connectToSQL.conn.Close();


                SqlCommand command2 = new SqlCommand();
                command2.Connection = connectToSQL.conn;
                command2.CommandText = "DELETE FROM Key_NV WHERE TK_NV_MaNV=@tk";
                connectToSQL.connect();
                command2.Parameters.AddWithValue("@tk", "TK_" + txtMa.Text);
                try
                {
                    command2.ExecuteNonQuery();
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
}
