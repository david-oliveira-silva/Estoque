using FirebirdSql.Data.FirebirdClient;

namespace REPOSITORY.Data
{
    public class FirebirdConnection
    {
        private  static string? ConexaoString;

        public static FbConnection GetFbConnection()
        {
            return new FbConnection(ConexaoString);
        }
        public static void Inicializar(string ConnectionString)
        {
            ConexaoString = ConnectionString;
        }

        public static void OpenConnection(FbConnection fbConnection)
        {
            try
            {
                fbConnection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void CloseConnection(FbConnection fbConnection)
        {
            try
            {
                fbConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
