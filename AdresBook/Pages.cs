using System;
using System.Collections.Generic;
using System.Data.SQLite;

internal class FillCensus {
    private Dictionary<int, string> census;
    private string dbname;

    public FillCensus(Dictionary<int, string> census, string dbname = "database.db") {
        this.census = census;
        this.dbname = dbname;
        fill(census, dbname);
    }

    public void fill(Dictionary<int, string> census, string dbname = "database.db") {
        using(SQLiteConnection connection = new SQLiteConnection($"data source={dbname}; version=3")) {
            connection.Open();
            using(SQLiteCommand cmd = connection.CreateCommand()) {
                cmd.CommandText = "SELECT * FROM 'census';";
                using(var read = cmd.ExecuteReader()) {
                    while(read.Read()) {
                        census.Add(int.Parse(read.GetDecimal(0).ToString()),
                                   $"{read.GetString(1)}\t {read.GetString(2)}\t {read.GetString(3)}\t {read.GetDecimal(4)}\t {read.GetString(5)}");
                    }
                }
            }
        }
    }

    public void DisplayPage(int pageNumber, int pageSize) {
        int startIndex = (pageNumber - 1) * pageSize;
        int endIndex = startIndex + pageSize;

        Console.WriteLine($"Displaying page {pageNumber}:");
        foreach(var entry in census) {
            if(entry.Key >= startIndex && entry.Key < endIndex) {
                Console.WriteLine($"ID: {entry.Key}, Data: {entry.Value}");
            }
        }
    }
}
