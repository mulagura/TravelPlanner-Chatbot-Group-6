using System.IO;
using System;
using Microsoft.Data.Sqlite;

namespace OrderBot
{
    public class DB
    {
        public static string GetConnectionString(){
            string sFName = "/Orders.db";
            string sPrefix = "Data Source=";
            string sReturn = sPrefix + Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + sFName;
            string sPath = Directory.GetCurrentDirectory();
            string[] subs = sPath.Split(Path.DirectorySeparatorChar);
            for(int n = subs.Length - 1; n > 1; n-- ){
                // skip first empty path
                string sResult = "";
                for(int nsubs = 1; nsubs < n; nsubs++){ 
                    sResult += Path.DirectorySeparatorChar + subs[nsubs];
                }
                string[] aFiles = Directory.GetFiles(sResult, "README.md", System.IO.SearchOption.TopDirectoryOnly);
                if(aFiles.Length > 0){
                    sReturn =  sPrefix + sResult + sFName;
                    break;
                }

            }
            using (var connection = new SqliteConnection(sReturn))
            {
                connection.Open();

                var commandCreateTable = connection.CreateCommand();
                commandCreateTable.CommandText =
                    @"
                        CREATE TABLE IF NOT EXISTS orders (
                            to_location TEXT,
                            from_location TEXT,
                            travel_date TEXT,
                            nevent TEXT,
                            availibility TEXT,
                            accomodation TEXT,
                            email TEXT
                        );
                        ";
                commandCreateTable.ExecuteNonQuery();
            }
            return sReturn;
        }
    }
}
