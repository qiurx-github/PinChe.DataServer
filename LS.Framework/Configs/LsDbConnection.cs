namespace LS.Framework
{
    public class LsDbConnection
    {
        public static bool Encrypt { get; set; }
        public LsDbConnection(bool encrypt)
        {
            Encrypt = encrypt;
        }
        public static string ConnectionString
        {
            get
            {
                string connection = System.Configuration.ConfigurationManager.ConnectionStrings["LsDbContext"].ConnectionString;
                if (Encrypt == true)
                {
                    return DesEncrypt.Decrypt(connection);
                }
                else
                {
                    return connection;
                }
            }
        }
    }
}
