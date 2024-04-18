using GnomeRides.Utils;
using MySql.Data.MySqlClient;

namespace GnomeRides.Classes
{
    internal class User
    {
        private readonly string _id;
        private readonly string _name;
        private readonly string _email;
        private static User? _currentUser = null;

        private User(string id, string name, string email)
        {
            _id = id;
            _name = name;
            _email = email;
        }

        public static User? CurrentUser 
        { 
            get
            {
                return _currentUser;
            }
        }

        public string Id { get { return _id; } }
        public string Name { get { return _name;  } }
        public string Email { get { return _email; } }

        public static string? Login(string id, string password)
        {
            if (_currentUser != null)
            {
                return "Already logged in";
            }
            try
            {
                MySqlCommand cmd = MySqlAdapter.Connection.CreateCommand();
                cmd.CommandText = "SELECT hashed_password, id, name, email FROM user WHERE id = @id;";
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) 
                {
                    if (Sha256Hash.CompareValueToHash(password, reader.GetString(0)))
                    {
                        _currentUser = new User(reader.GetString(1), reader.GetString(2), reader.GetString(3));
                        return null;
                    }
                } else
                {
                    return "Incorrect id or password";
                }
                
                return "Incorrect password";
            } catch
            {
                return "An unexpected error occured";
            }
        }

        public static string? CreateAccount(string id, string password, string email, string name)
        {
            if (_currentUser != null)
            {
                return "Already logged in";
            }

            //TODO add regex patterns to check all inputs

            try
            {
                MySqlCommand cmd = MySqlAdapter.Connection.CreateCommand();
                cmd.CommandText = "INSERT INTO user (id, password, email, name) VALUES (@id, @password, @email, @name);";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue ("@password", Sha256Hash.CreateHash(password));
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.ExecuteNonQuery();
                return null;
            } catch
            {
                return "An unexpected error occured";
            }
        }

        public static void Logout()
        {
            _currentUser = null;
        }
    }
}
