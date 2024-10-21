using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdresBook {
    internal class FillCensus {
        private Dictionary<int, string> census;
        private string dbname;
        public FillCensus(Dictionary<int, string> census, int page, string dbname = "database.db") {
            this.census = census; this.dbname=dbname; fill(census, page, dbname);
        }
        public void fill(Dictionary<int, string> census, int page, string dbname = "database.db") {
            SQLiteConnection connection = new SQLiteConnection($"data source={dbname}; version=3");
            connection.Open();
            SQLiteCommand cmd = connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM 'census' limit 5 offset ({page}*5);";
            var read = cmd.ExecuteReader();
            census.Clear();
            if(read.HasRows) while(read.Read()) census.Add(int.Parse(read.GetDecimal(0).ToString()), $"{read.GetString(1)}\t {read.GetString(2)}\t {read.GetString(3)}\t {read.GetDecimal(4)}\t {read.GetString(5)}");
            else census.Add(-1, $"Brak Wyników");
            read.Close();
        }
    }
}
