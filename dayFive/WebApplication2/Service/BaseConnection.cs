using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using WebApplication2.Model;
using WebApplication2.Repository.Common;

namespace Repository
{
    public class BaseConnection  
    {
        public NpgsqlConnection connection;

        private const string CONNECTION_STRING = "Host=localhost;" +
           "Port=5432;" +
           "Username=postgres;" +
           "Password=nk26cdJx;" +
           "Database=postgres";

        public BaseConnection()
        {
            connection = new NpgsqlConnection(CONNECTION_STRING);
        }

        public void OpenConnection()
        {
            connection.Open();
            return;
        }

        public void CloseConnection()
        {
            connection.Close();
            return;
        }


    }
}
