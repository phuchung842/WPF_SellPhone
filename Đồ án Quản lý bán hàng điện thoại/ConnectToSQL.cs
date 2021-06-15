using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Đồ_án_Quản_lý_bán_hàng_điện_thoại
{
    class ConnectToSQL
    {
        public SqlConnection conn = new SqlConnection();
        public void connect()
        {
            conn.ConnectionString = @"Data Source=DESKTOP-G6IUGH8\SQLEXPRESS;Initial Catalog=SQL_BHDT;Integrated Security=True";
            conn.Open();
        }
    }
}
