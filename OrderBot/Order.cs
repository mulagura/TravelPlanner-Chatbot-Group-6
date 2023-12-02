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
        SET size = $size
        WHERE phone = $phone
    ";
                //commandUpdate.Parameters.AddWithValue("$size", Size);
                //commandUpdate.Parameters.AddWithValue("$phone", Phone);
                int nRows = commandUpdate.ExecuteNonQuery();
                if(nRows == 0){
                    var commandInsert = connection.CreateCommand();
                    commandInsert.CommandText =
                    @"
            INSERT INTO orders(size, phone)
            VALUES($size, $phone)
        ";
                    //commandInsert.Parameters.AddWithValue("$size", Size);
                    //commandInsert.Parameters.AddWithValue("$phone", Phone);
                    int nRowsInserted = commandInsert.ExecuteNonQuery();

                }
            }

        }
    }
}
