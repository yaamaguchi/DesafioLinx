using System;
using System.Data.SqlClient;

namespace BD.Dal
{
    public class Connection : IConnection
    {
        public SqlConnection Connect { get; private set; }

        public Connection(SqlConnection sqlConnection)
        {
            Connect = sqlConnection;
        }

        public Connection(string connectionString)
        {
            Connect = new SqlConnection(connectionString);
        }

        public SqlCommand CreateCommand()
        {
            return Open()
                .CreateCommand();
        }

        public SqlConnection Open()
        {
            if (Connect == null)
                throw new Exception("No instance class ...");
            if (Connect.State != System.Data.ConnectionState.Open)
            {
                Connect.Open();
            }
            return Connect;
        }

        public void Close()
        {
            if (Connect != null && Connect.State == System.Data.ConnectionState.Open)
            {
                Connect.Close();
                Connect.Dispose();
            }
        }

        public void Dispose()
        {
            Close();
        }

    }
}