using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAccessLayer
{
    public class DataBase
    {

        public static SqlConnection Connect()
        {
            var connection =
                    new SqlConnection(
                        "Server=DESKTOP-5TM1PFC\\SQLEXPRESS;Database=ChatProgram;User Id=remote;Password=remote;");
                connection.Open();
            return connection;
        }

    }
}
