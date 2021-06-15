using System;
using System.Collections.Generic;
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
    /// Interaction logic for Đăng_nhập.xaml
    /// </summary>
    public partial class Đăng_nhập : Window
    {
        public Đăng_nhập()
        {
            InitializeComponent();
        }

        private void BtnThoat_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult boxResult = MessageBox.Show("Bạn muốn thoát khỏi chương trình ?", "Notification", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (boxResult == MessageBoxResult.OK)
            {
                this.Close();
            }
        }

        public string accountuser;
        public string accountadmin;
        public string passuser="";
        public string passadmin = "";
        private void BtnDangnhap_Click(object sender, RoutedEventArgs e)
        {
            bool flagad = true;
            bool flaguser = true;
            //phần bên nhân viên
            accountuser = txtUser.Text.Replace(" ","");
            ConnectToSQL connectToSQL = new ConnectToSQL();
            SqlCommand command = new SqlCommand();
            command.Connection = connectToSQL.conn;
            command.CommandText = "SELECT MK_NV FROM Key_NV WHERE TK_NV_MaNV=@tk";
            connectToSQL.connect();
            command.Parameters.AddWithValue("@tk", accountuser);
            try
            {
                SqlDataReader sqlDataReader = command.ExecuteReader();
                sqlDataReader.Read();
                passuser = sqlDataReader[0].ToString().Replace(" ","");
            }
            catch
            {
                flaguser = false;
            }
            connectToSQL.conn.Close();
            //phần bên admin
            accountadmin = txtUser.Text.Replace(" ","");
            ConnectToSQL connectToSQL2 = new ConnectToSQL();
            SqlCommand command2 = new SqlCommand();
            command2.Connection = connectToSQL2.conn;
            command2.CommandText = "SELECT MK_AD FROM Key_AD WHERE TK_AD=@tk";
            connectToSQL2.connect();
            command2.Parameters.AddWithValue("@tk", accountuser);
            try
            {
                SqlDataReader sqlDataReader = command2.ExecuteReader();
                sqlDataReader.Read();
                passadmin = sqlDataReader[0].ToString().Replace(" ","");
            }
            catch
            {
                flagad = false;
            }
            connectToSQL.conn.Close();

            if (nhappass.Password == "" || nhappass.Password == null)
            {
                MessageBox.Show("Bạn chưa nhập pass.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (flagad == false && flaguser == false)
            {
                MessageBox.Show("Tài khoản không tồn tại", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (passadmin == nhappass.Password && flagad == true)
            {
                this.Hide();
                MainWindow main = new MainWindow();
                main.ShowDialog();
                this.txtUser.Text = "";
                this.nhappass.Password = "";
                this.ShowDialog();
            }
            else if (passuser == nhappass.Password && flaguser == true)
            {
                this.Hide();
                Bán_hàng sell = new Bán_hàng(this);
                sell.ShowDialog();
                this.txtUser.Text = "";
                this.nhappass.Password = "";
                this.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bạn đã nhập sai pass hoặc user xin vui lòng nhập lại.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnDangnhap_Click(sender, e);
            }
            if (e.Key == Key.Escape)
                BtnThoat_Click(sender, e);
        }

        private void LbFixPass_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            Cài_đặt_mật_khẩu SetupPass = new Cài_đặt_mật_khẩu();
            SetupPass.ShowDialog();
            this.txtUser.Text = "";
            this.nhappass.Password = "";
            this.ShowDialog();
        }
    }
}
