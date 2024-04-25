using GnomeRides.Utils;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace GnomeRides.Classes
{
    internal class User
    {
        private readonly string _id;
        private readonly string _name;
        private readonly string _email;
        private static User? _currentUser = null;

        public User(string id, string name, string email)
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
        /// <summary>
        /// Performs a user login and if succesful sets currentUser
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns>An error message on failure and null on succcess</returns>
        public static string? Login(string id, string password)
        {
            if (_currentUser != null)
            {
                return "Already logged in";
            }

            try
            {
                using MySqlCommand cmd = MySqlAdapter.Connection.CreateCommand();
                cmd.CommandText = "SELECT hashed_password, id, name, email FROM user WHERE id = @id;";
                cmd.Parameters.AddWithValue("@id", id);
                using MySqlDataReader reader = cmd.ExecuteReader();
                if (!reader.Read())
                {
                    return "Incorrect password";
                }

                if (!Sha256Hash.CompareValueToHash(password, reader.GetString(0)))
                {
                    return "Incorrect id or password";
                }

                _currentUser = new User(reader.GetString(1), reader.GetString(2), reader.GetString(3));
            } catch
            {
                return "An unexpected error occured";
            }
            return null;
        }
        /// <summary>
        /// Creates an account with the given credentials and sets currentUser to the created user on success
        /// </summary>
        /// <param name="id">The persons social security number</param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <returns>An error message on error and null on success</returns>
        public static string? CreateAccount(string id, string password, string email, string name)
        {
            if (_currentUser != null)
            {
                return "Already logged in";
            }

            //TODO add regex patterns to check all inputs
            if (!new Regex("\\d\\d\\d\\d\\d\\d\\d\\d\\d\\d\\d\\d", RegexOptions.IgnoreCase).IsMatch(id))
            {
                return "Ogiltigt personnummer";
            }

            if (name.Length <2)
            {
                return "Fyll i ditt namn";
            }

            if (!new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", RegexOptions.IgnoreCase).IsMatch(email))
            {
                return "Ange en giltig e-post";
            }
            
            if (password.Length < 4)
            {
                return "Lösenord måste minst 4 karaktärer";
            }

            try
            {
                using MySqlCommand cmd = MySqlAdapter.Connection.CreateCommand();
                cmd.CommandText = "INSERT INTO user (id, hashed_password, email, name) VALUES (@id, @hashed_password, @email, @name);";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue ("@hashed_password", Sha256Hash.CreateHash(password));
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.ExecuteNonQuery();
                _currentUser = new User(id, name, email);
            } catch (Exception ex)
            {
                //Divine interlect or Ni***rlishous
                if ((int)ex.GetType().GetProperty("Number").GetValue(ex, null) == 1062)
                {
                    return "Ett konot med detta personnummer finns redan.";
                }
                return "An unexpected error occured";
            }
            return null;
        }
        /// <summary>
        /// Logout the current user
        /// </summary>
        public static void Logout()
        {
            _currentUser = null;
        }
        /// <summary>
        /// Change the current users password
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns>An error message on error and null on success</returns>
        public static string? ChangePassword(string oldPassword, string newPassword)
        {
            if (_currentUser == null)
            {
                return "Unauthorized";
            }

            try
            {
                using (MySqlCommand cmd = MySqlAdapter.Connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT hashed_password FROM user WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@id", _currentUser._id);
                    using MySqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        return "Incorrect id or password";
                    }
                    if (!Sha256Hash.CompareValueToHash(oldPassword, reader.GetString(0)))
                    {
                        return "Incorrect id or password";
                    }
                }

                using (MySqlCommand cmd = MySqlAdapter.Connection.CreateCommand())
                {
                    cmd.CommandText = "UPDATE user SET hashed_password = @hashed_password WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@hashed_password", Sha256Hash.CreateHash(newPassword));
                    cmd.Parameters.AddWithValue("@id", _currentUser._id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                return "An unexpected error occured";
            }
            return null;
        }
        /// <summary>
        /// Delete the current user
        /// </summary>
        /// <param name="password"></param>
        /// <returns>An error message on error and null on success</returns>
        public static string? DeleteUser(string password)
        {
            if (_currentUser == null)
            {
                return "Unauthorized";
            }

            try
            {
                using (MySqlCommand cmd = MySqlAdapter.Connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT hashed_password FROM user WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@id", _currentUser._id);
                    using MySqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        return "Incorrect id or password";
                    }
                    if (!Sha256Hash.CompareValueToHash(password, reader.GetString(0)))
                    {
                        return "Incorrect id or password";
                    }
                }

                using (MySqlCommand cmd = MySqlAdapter.Connection.CreateCommand())
                {
                    cmd.CommandText = "DELETE user WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@id", _currentUser._id);
                    cmd.ExecuteNonQuery();
                }
            } catch
            {
                return "Failed to delete account";
            }
            _currentUser = null;
            return null;
        }
    }
}
