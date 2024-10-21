using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdresBook {
    internal class DeleteRecord {
        public DeleteRecord(int x, string dbname = "database.db") {
            SQLiteConnection connection = new SQLiteConnection($"data source={dbname}; version=3");
            connection.Open();
            SQLiteCommand cmd = connection.CreateCommand();
            cmd.CommandText = $"delete from census where id={x};";
            var read = cmd.ExecuteReader();
            connection.Close();
        }
    }
}
