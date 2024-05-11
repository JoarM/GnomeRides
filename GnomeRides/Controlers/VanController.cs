using GnomeRides.Classes;
using GnomeRides.Utils;
using MySql.Data.MySqlClient;

namespace GnomeRides.Controlers
{
    internal class VanController
    {
        /// <summary>
        /// Gives you a list of all vans
        /// </summary>
        /// <returns>A tuple containg a list of vans and an error message or null</returns>
        public static (List<Van>, string?) GetVans()
        {
            List<Van> VanList = new();
            try
            {
                using MySqlCommand cmd = MySqlAdapter.Connection.CreateCommand();
                cmd.CommandText = "SELECT vehicle.reg_nr, " +
                    "vehicle.seats, " +
                    "vehicle.manufacturer, " +
                    "vehicle.mileage, vehicle.wheels, " +
                    "vehicle.model, " +
                    "vehicle.fuel_type, " +
                    "vehicle.daily_rate, " +
                    "vehicle.owner_id, " +
                    "van.outer_width, " +
                    "van.outer_height, " +
                    "van.outer_length, " +
                    "van.inner_width, " +
                    "van.inner_height, " +
                    "van.inner_length, " +
                    "van.max_weight, " +
                    "van.volume " +
                    "FROM vehicle " +
                    "INNER JOIN van " +
                    "ON vehicle.reg_nr = van.reg_nr;";
                using MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Van van = new(
                        reader.GetString(0),
                        reader.GetUInt16(1),
                        Constants.VehicleManufacturers.Find(kvp => kvp.Key == reader.GetUInt16(2)).Value,
                        reader.GetUInt32(3),
                        reader.GetUInt16(4),
                        reader.GetString(5),
                        Constants.FuelTypes.Find(kvp => kvp.Key == reader.GetUInt16(6)).Value,
                        reader.GetUInt32(7),
                        reader.GetString(8),
                        reader.GetUInt32(9),
                        reader.GetUInt32(10),
                        reader.GetUInt32(11),
                        reader.GetUInt32(12),
                        reader.GetUInt32(13),
                        reader.GetUInt32(14),
                        reader.GetUInt32(15),
                        reader.GetUInt32(16)
                    );
                    VanList.Add(van);
                }
            }
            catch 
            {
                return (VanList, "Ett oväntat fel uppstod");
            }
            return (VanList, null);
        }

        /// <summary>
        /// Returns a given van by the registratinon nummber
        /// </summary>
        /// <param name="regNr"></param>
        /// <returns>A tuple containing a van or null and an error message or null</returns>
        public static (Van?, string?) GetVanByRegNr(string regNr)
        {
            Van? van = null;
            try
            {
                using MySqlCommand cmd = MySqlAdapter.Connection.CreateCommand();
                cmd.CommandText = "SELECT vehicle.reg_nr, " +
                    "vehicle.seats, " +
                    "vehicle.manufacturer, " +
                    "vehicle.mileage, vehicle.wheels, " +
                    "vehicle.model, " +
                    "vehicle.fuel_type, " +
                    "vehicle.daily_rate, " +
                    "vehicle.owner_id, " +
                    "van.outer_width, " +
                    "van.outer_height, " +
                    "van.outer_length, " +
                    "van.inner_width, " +
                    "van.inner_height, " +
                    "van.inner_length, " +
                    "van.max_weight, " +
                    "van.volume " +
                    "FROM vehicle " +
                    "INNER JOIN van " +
                    "ON vehicle.reg_nr = van.reg_nr " +
                    "WHERE vehicle.reg_nr = @reg_nr;";
                cmd.Parameters.AddWithValue("@reg_nr", regNr);
                using MySqlDataReader reader = cmd.ExecuteReader();
                if (!reader.Read())
                {
                    return (van, "Ett oväntat fel uppstod");
                }
                van = new(
                    reader.GetString(0),
                    reader.GetUInt16(1),
                    Constants.VehicleManufacturers.Find(kvp => kvp.Key == reader.GetUInt16(2)).Value,
                    reader.GetUInt32(3),
                    reader.GetUInt16(4),
                    reader.GetString(5),
                    Constants.FuelTypes.Find(kvp => kvp.Key == reader.GetUInt16(6)).Value,
                    reader.GetUInt32(7),
                    reader.GetString(8),
                    reader.GetUInt32(9),
                    reader.GetUInt32(10),
                    reader.GetUInt32(11),
                    reader.GetUInt32(12),
                    reader.GetUInt32(13),
                    reader.GetUInt32(14),
                    reader.GetUInt32(15),
                    reader.GetUInt32(16)
                );
            }
            catch
            {
                return (van, "Ett oväntat fel uppstod");
            }
            return (van, null);
        }

        /// <summary>
        /// Adds a van to the database
        /// </summary>
        /// <param name="regNr"></param>
        /// <param name="seats"></param>
        /// <param name="manufacturer"></param>
        /// <param name="mileage"></param>
        /// <param name="wheels"></param>
        /// <param name="model"></param>
        /// <param name="fuelType"></param>
        /// <param name="dailyRate"></param>
        /// <param name="outerWidth"></param>
        /// <param name="outerHeight"></param>
        /// <param name="outerLength"></param>
        /// <param name="innerWidth"></param>
        /// <param name="innerHeight"></param>
        /// <param name="innerLength"></param>
        /// <param name="maxWeight"></param>
        /// <param name="volume"></param>
        /// <returns>An error message on error and null on success</returns>
        public static string? AddVan(string regNr, uint seats, uint manufacturer, uint mileage, uint wheels, string model, uint fuelType, uint dailyRate, 
            uint outerWidth, uint outerHeight, uint outerLength, uint innerWidth, uint innerHeight, uint innerLength, uint maxWeight, uint volume)
        {
            if (User.CurrentUser == null)
            {
                return "Logga in för att lägga upp ett fordon för uthyrning";
            }

            try
            {
                using MySqlCommand cmd = MySqlAdapter.Connection.CreateCommand();
                cmd.CommandText = "INSERT INTO vehicle (reg_nr, seats, manufacturer, mileage, wheels, model, fuel_type, daily_rate, owner_id) " +
                    "VALUES (@reg_nr, @seats, @manufacturer, @mileage, @wheels, @model, @fuel_type, @daily_rate, @owner_id);" +
                    "INSERT INTO van (reg_nr, outer_width, outer_height, outer_length, inner_width, inner_height, inner_length, max_weight, volume) " +
                    "VALUES (@reg_nr, @outer_width, @outer_height, @outer_length, @inner_width, @inner_height, @inner_length, @max_weight, @volume);";
                cmd.Parameters.AddWithValue("@reg_nr", regNr);
                cmd.Parameters.AddWithValue("@seats", seats);
                cmd.Parameters.AddWithValue("@manufacturer", manufacturer);
                cmd.Parameters.AddWithValue("@mileage", mileage);
                cmd.Parameters.AddWithValue("@wheels", wheels);
                cmd.Parameters.AddWithValue("model", model);
                cmd.Parameters.AddWithValue("@fuel_type", fuelType);
                cmd.Parameters.AddWithValue("@daily_rate", dailyRate);
                cmd.Parameters.AddWithValue("@owner_id", User.CurrentUser.Id);
                cmd.Parameters.AddWithValue("@outer_width", outerWidth);
                cmd.Parameters.AddWithValue("@outer_height", outerHeight);
                cmd.Parameters.AddWithValue("@outer_length", outerLength);
                cmd.Parameters.AddWithValue("@inner_width", innerWidth);
                cmd.Parameters.AddWithValue("@inner_height", innerHeight);
                cmd.Parameters.AddWithValue("@inner_length", innerLength);
                cmd.Parameters.AddWithValue("@max_weight", maxWeight);
                cmd.Parameters.AddWithValue("@volume", volume);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                return "Ett oväntat fel uppstod";
            }
            return null;
        }
    }
}
