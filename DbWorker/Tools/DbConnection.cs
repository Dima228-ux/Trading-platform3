using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbWorker.Tools
{
    class DbConnection
    {
        public static MySqlConnection GetConnection()
        {
            string connectionString = "Server=localhost;port=3306;User=root;Password=1234;Database=trading_platform;charset=utf8";
            return new MySqlConnection(connectionString);
        }
    }
}
