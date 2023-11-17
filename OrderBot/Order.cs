using Microsoft.Data.Sqlite;

namespace OrderBot
{
    public class Order : ISQLModel
    {
        private string _to_location = String.Empty;
        private string _from_location = String.Empty;
        private string _travel_date = String.Empty;
        private string _event = String.Empty;
        private string _availibility = String.Empty;
        private string _accomodation = String.Empty;
        private string _email = String.Empty;

        public string ToLocation
        {
            get => _to_location;
            set => _to_location = value;
        }

        public string FromLocation
        {
            get => _from_location;
            set => _from_location = value;
        }

        public string TravelDate
        {
            get => _travel_date;
            set => _travel_date = value;
        }

        public string Event
        {
            get => _event;
            set => _event = value;
        }

        public string Availibility
        {
            get => _availibility;
            set => _availibility = value;
        }

        public string Accomodation
        {
            get => _accomodation;
            set => _accomodation = value;
        }

        public string Email
        {
            get => _email;
            set => _email = value;
        }

        //public string Phone{
        //    get => _phone;
        //    set => _phone = value;
        //}

        //public string Size{
        //    get => _size;
        //    set => _size = value;
        //}

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
