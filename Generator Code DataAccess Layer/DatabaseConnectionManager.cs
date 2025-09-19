using System.Data.SqlClient;

namespace Generator_Code_DataAccess_Layer
{
    public class clsDatabaseConnectionManager_DataAccess
    {
        public static bool ConnectionStatus(string Server, string Password, string UserID)
        {
            bool IsConnect = false;
            try
            {
                using (SqlConnection connection = new SqlConnection($"Server={Server};User Id={UserID};Password={Password};"))
                {
                    connection.Open();
                    IsConnect = true;
                }
            }
            catch
            {
                IsConnect = false;
            }
            return IsConnect;
        }

    }
}
