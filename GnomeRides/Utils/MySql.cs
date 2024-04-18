using MySql.Data.MySqlClient;

namespace GnomeRides.Utils
{
    internal class MySqlAdapter
    {
        private static MySqlConnection db = null;
        private static readonly object db_lock = new object();
        private static readonly string connStr = $"server={Environment.GetEnvironmentVariable("DB_SERVER")};user={Environment.GetEnvironmentVariable("DB_USER")};database={Environment.GetEnvironmentVariable("DB_DATABASE")};password={Environment.GetEnvironmentVariable("DB_PASSWORD")};";

        public static MySqlConnection Connection
        {
            get
            {
                lock (db_lock)
                {
                    if (db == null)
                    {
                        db = new MySqlConnection(connStr);
                        //db.Open();
                    }
                    return db;
                }
            }
        }
    }
}
