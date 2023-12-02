using System.Drawing;
using Microsoft.Data.Sqlite;

namespace OrderBot
{
    public class Order : ISQLModel
    {
        private string to_location = String.Empty;
        private string from_location = String.Empty;
        private string travel_date = String.Empty;
        private string nevent = String.Empty;
        private string availibility = String.Empty;
        private string accomodation = String.Empty;
        private string email = String.Empty;

        public string ToLocation
        {
            get => to_location;
            set => to_location = value;
        }

        public string FromLocation
        {
            get => from_location;
            set => from_location = value;
        }

        public string TravelDate
        {
            get => travel_date;
            set => travel_date = value;
        }

        public string Event
        {
            get => nevent;
            set => nevent = value;
        }

        public string Availibility
        {
            get => availibility;
            set => availibility = value;
        }

        public string Accomodation
        {
            get => accomodation;
            set => accomodation = value;
        }

        public string Email
        {
            get => email;
            set => email = value;
        }


        public void Save(){
           using (var connection = new SqliteConnection(DB.GetConnectionString()))
            {
                connection.Open();

                var commandUpdate = connection.CreateCommand();
                commandUpdate.CommandText =
                @"
        UPDATE orders
        SET to_location = $to_location,
from_location = $from_location,
travel_date = $travel_date,
nevent = $nevent,
availibility = $availibility,
accomodation = $accomodation,
email = $email
        WHERE to_location = $to_location
    ";
                commandUpdate.Parameters.AddWithValue("$to_location", ToLocation);
                commandUpdate.Parameters.AddWithValue("$from_location", FromLocation);
                commandUpdate.Parameters.AddWithValue("$travel_date", TravelDate);
                commandUpdate.Parameters.AddWithValue("$nevent", Event);
                commandUpdate.Parameters.AddWithValue("$availibility", Availibility);
                commandUpdate.Parameters.AddWithValue("$accomodation", Accomodation);
                commandUpdate.Parameters.AddWithValue("$email", Email);

                int nRows = commandUpdate.ExecuteNonQuery();
                if(nRows == 0){
                    var commandInsert = connection.CreateCommand();
                    commandInsert.CommandText =
                    @"
            INSERT INTO orders(to_location, from_location,travel_date,nevent,availibility,accomodation,email)
            VALUES($to_location, $from_location,$travel_date,$nevent,$availibility,$accomodation,$email)
        ";
                    commandInsert.Parameters.AddWithValue("$to_location", ToLocation);
                    commandInsert.Parameters.AddWithValue("$from_location", FromLocation);
                    commandInsert.Parameters.AddWithValue("$travel_date", TravelDate);
                    commandInsert.Parameters.AddWithValue("$nevent", Event);
                    commandInsert.Parameters.AddWithValue("$availibility", Availibility);
                    commandInsert.Parameters.AddWithValue("$accomodation", Accomodation);
                    commandInsert.Parameters.AddWithValue("$email", Email);
                    int nRowsInserted = commandInsert.ExecuteNonQuery();

                }
            }

        }
    }
}
