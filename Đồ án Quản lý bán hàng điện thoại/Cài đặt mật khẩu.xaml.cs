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
    /// Interaction logic for Cài_đặt_mật_khẩu.xaml
    /// </summary>
    public partial class Cài_đặt_mật_khẩu : Window
    {
        public Cài_đặt_mật_khẩu()
        {
            InitializeComponent();
        }

        public string accountuser="";
        public string passuser;
        public bool flagacc;
        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            flagacc = true;
            accountuser = txtSetupAccount.Text;
            if (accountuser == "")
            {
                lbChuaNhap.Visibility = Visibility.Visible;
                lbAcc.Visibility = Visibility.Hidden;
                flagacc = false;
            }
            else
            {
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
                    passuser = sqlDataReader[0].ToString().Replace(" ", "");
                }
                catch
                {
                    lbAcc.Visibility = Visibility.Visible;
                    lbChuaNhap.Visibility = Visibility.Hidden;
                    flagacc = false;
                    //MessageBox.Show("Tài khoản không tồn tại", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            if (flagacc == true)
            {
                if (passSetupOld.Password == "")
                {
                    lbChuaNhapPassOld.Visibility = Visibility.Visible;
                    //lbPassOld.Visibility = Visibility.Hidden;
                }
                else if (passuser == passSetupOld.Password)
                {
                    if (passSetupNew.Password.Length == 0)
                    {
                        lbChuaNhapPassNew.Visibility = Visibility.Visible;
                        //lbPassNew.Visibility = Visibility.Hidden;
                        //lbTuongDoi.Visibility = Visibility.Hidden;
                        //lbKha.Visibility = Visibility.Hidden;
                        //lbTot.Visibility = Visibility.Hidden;
                    }
                    else if (passSetupNew.Password.Length > 5)
                    {
                        if (passSetupNewCompare.Password == "")
                        {
                            lbChuaNhapPassReNew.Visibility = Visibility.Visible;
                            //lbPassReNew.Visibility = Visibility.Hidden;
                        }
                        else if (passSetupNew.Password == passSetupNewCompare.Password)
                        {
                            ConnectToSQL toSQL = new ConnectToSQL();
                            SqlCommand sqlCommand = new SqlCommand();
                            sqlCommand.Connection = toSQL.conn;
                            sqlCommand.CommandText = "UPDATE Key_NV SET MK_NV=@mk WHERE TK_NV_MaNV=@tk";
                            toSQL.connect();
                            sqlCommand.Parameters.AddWithValue("@tk", accountuser);
                            sqlCommand.Parameters.AddWithValue("@mk", passSetupNew.Password);
                            try
                            {
                                sqlCommand.ExecuteNonQuery();
                                MessageBox.Show("Đổi thành công", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                                txtSetupAccount.Text = "";
                                passSetupNew.Password = "";
                                passSetupNewCompare.Password = "";
                                passSetupOld.Password = "";
                                lbTuongDoi.Visibility = Visibility.Hidden;
                                lbKha.Visibility = Visibility.Hidden;
                                lbTot.Visibility = Visibility.Hidden;
                                lbPassNew.Visibility = Visibility.Hidden;
                            }
                            catch
                            {
                                MessageBox.Show("Lỗi", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else
                        {
                            lbPassReNew.Visibility = Visibility.Visible;
                            //MessageBox.Show("Password của bạn chưa trùng khớp", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        lbPassNew.Visibility = Visibility.Visible;
                        //MessageBox.Show("Password phải hơn 5 kí tự", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    lbPassOld.Visibility = Visibility.Visible;
                    //MessageBox.Show("Bạn nhập sai password", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult boxResult = MessageBox.Show("Bạn muốn thoát khỏi chương trình ?", "Notification", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (boxResult == MessageBoxResult.OK)
            {
                this.Close();
            }
        }

        private void TxtSetupAccount_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbAcc.Visibility = Visibility.Hidden;
            lbChuaNhap.Visibility = Visibility.Hidden;
        }

        private void PassSetupOld_PasswordChanged(object sender, RoutedEventArgs e)
        {
            lbPassOld.Visibility = Visibility.Hidden;
            lbChuaNhapPassOld.Visibility = Visibility.Hidden;
        }

        private void PassSetupNew_PasswordChanged(object sender, RoutedEventArgs e)
        {
            //lbPassNew.Visibility = Visibility.Hidden;
            lbChuaNhapPassNew.Visibility = Visibility.Hidden;
            if (passSetupNew.Password.Length == 0)
            {
                lbPassNew.Visibility = Visibility.Hidden;
                lbTuongDoi.Visibility = Visibility.Hidden;
                lbKha.Visibility = Visibility.Hidden;
                lbTot.Visibility = Visibility.Hidden;
            }
            else if (passSetupNew.Password.Length <= 5)
            {
                lbPassNew.Visibility = Visibility.Visible;
                lbTuongDoi.Visibility = Visibility.Hidden;
                lbKha.Visibility = Visibility.Hidden;
                lbTot.Visibility = Visibility.Hidden;
            }
            else if (passSetupNew.Password.Length <= 8 && passSetupNew.Password.Length > 5)
            {
                lbPassNew.Visibility = Visibility.Hidden;
                lbTuongDoi.Visibility = Visibility.Visible;
                lbKha.Visibility = Visibility.Hidden;
                lbTot.Visibility = Visibility.Hidden;
            }
            else if (passSetupNew.Password.Length <= 15)
            {
                lbPassNew.Visibility = Visibility.Hidden;
                lbTuongDoi.Visibility = Visibility.Hidden;
                lbKha.Visibility = Visibility.Visible;
                lbTot.Visibility = Visibility.Hidden;
            }
            else
            {
                lbPassNew.Visibility = Visibility.Hidden;
                lbTuongDoi.Visibility = Visibility.Hidden;
                lbKha.Visibility = Visibility.Hidden;
                lbTot.Visibility = Visibility.Visible;
            }
        }

        private void PassSetupNewCompare_PasswordChanged(object sender, RoutedEventArgs e)
        {
            lbPassReNew.Visibility = Visibility.Hidden;
            lbChuaNhapPassReNew.Visibility = Visibility.Hidden;
        }
    }
}
