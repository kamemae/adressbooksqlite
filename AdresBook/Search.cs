using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdresBook {
    internal class Search {
        public Search(string dbname = "database.db") {
            Console.Clear();
            Visuals visuals = new Visuals();
            visuals.drawLine();
            Console.Write("Wyszukaj: ");
            var what = Console.ReadLine();

            SQLiteConnection connection = new SQLiteConnection($"data source={dbname}; version=3");
            connection.Open();

            SQLiteCommand cmd = connection.CreateCommand();
            if(Int32.TryParse(what, out int whatNumeric)) cmd.CommandText = $"select * from census where phonenumber like'%{what}%' or adress like '%{what}%'";
            else cmd.CommandText = $"select * from census where name like '%{what}%' or surname like '%{what}%' or email like '%{what}%' or adress like '%{what}%';";
            
            var read = cmd.ExecuteReader();
            if(!read.HasRows) Console.WriteLine("Nie ma nic do wyświetlenia :("); else while(read.Read()) Console.WriteLine($"{read.GetString(1)} {read.GetString(2)} {read.GetString(3)} {read.GetDecimal(4)} {read.GetString(5)}");
            connection.Close();
            Console.ReadKey();
        }
    }
}
