using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdresBook {
    internal class DatabaseHandler {
        private string dbname = "database.db";
        public DatabaseHandler(string dbname = "database.db") {
            this.dbname = dbname;
            SQLiteConnection connection = new SQLiteConnection($"data source={dbname}; version=3");
            connection.Open();
            SQLiteCommand cmd = connection.CreateCommand();
            cmd.CommandText = "select * from 'census' where 1;";
            try { cmd.ExecuteNonQuery(); } 
            catch(System.Data.SQLite.SQLiteException) {
                cmd.CommandText = "CREATE TABLE 'census' ('id'	INTEGER NOT NULL UNIQUE, 'name'  TEXT NOT NULL,'surname' TEXT NOT NULL, 'email' TEXT NOT NULL, 'phonenumber' INTEGER NOT NULL, 'adress' TEXT NOT NULL, PRIMARY KEY('id' AUTOINCREMENT));";
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}
