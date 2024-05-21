using GnomeRides.Utils;
using MySql.Data.MySqlClient;

namespace GnomeRides.Classes
{
    public class Loan
    {
        private readonly DateOnly _start_date;
        private readonly DateOnly _end_date;
        private readonly string _loan_owner_id;
        private readonly string _reg_nr;
        private readonly int _price;

        public Loan(DateOnly start_date, DateOnly end_date, string loan_owner_id, string reg_nr, int price)
        {
            _start_date = start_date;
            _end_date = end_date;
            _loan_owner_id = loan_owner_id;
            _reg_nr = reg_nr;
            _price = price;
        }

        public DateOnly StartDate { get { return _start_date; } }
        public DateOnly EndDate { get { return _end_date; } }
        public string LoanOwnerId { get {  return _loan_owner_id; } }
        public string RegNr { get { return _reg_nr; } }
        public int Price { get { return _price; } }
        /// <summary>
        /// Get the user loaning a vehicle
        /// </summary>
        /// <returns>A tuple containing a user or a string</returns>
        public (User?, string?) GetLoanOwner()
        {
            try
            {
                using MySqlCommand cmd = MySqlAdapter.Connection.CreateCommand();
                cmd.CommandText = "GET id, name, email FROM user WHERE id = @id;";
                cmd.Parameters.AddWithValue("@id", LoanOwnerId);
                using MySqlDataReader reader = cmd.ExecuteReader();
                if (!reader.Read())
                {
                    return (null, "Failed to get user");
                }
                return (new User(reader.GetString(0), reader.GetString(1), reader.GetString(2)), null);
            } catch
            {
                return (null, "An unexpected error occured");
            }
        }
    }
}
