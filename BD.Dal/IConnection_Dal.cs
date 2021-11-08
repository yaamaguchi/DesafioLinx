using System;
using System.Data.SqlClient;
namespace BD.Dal
{
    public interface IConnection : IDisposable
    {
        SqlConnection Connect { get; }
        SqlConnection Open();
        
        void Close();
        SqlCommand CreateCommand();
    }
}