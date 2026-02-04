using Microsoft.Data.SqlClient;

namespace GestionScolarite.DataAccessLayer.DataAccess
{
    public static class DatabaseConnection
    {
        private static string connectionString = "Server=localhost;Database=GestionScolarite;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static void SetConnectionString(string cs)
        {
            connectionString = cs;
        }
    }
}


