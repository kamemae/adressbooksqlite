using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdresBook {
    internal class ModifyRecord {
        private int id;
        private string dbname;    
        public ModifyRecord(int _id, string dbname = "database.db") {
            this.id = _id;
            this.dbname = dbname;   
            Visuals visuals = new Visuals();
            Console.Clear();
            visuals.drawLine();

            SQLiteConnection connection = new SQLiteConnection($"data source={dbname}; version=3");
            connection.Open();
            SQLiteCommand cmd = connection.CreateCommand();
            cmd.CommandText = $"SELECT * FROM 'census' where id={id};";
            var read = cmd.ExecuteReader();
            string values = "";
            while(read.Read()) { Console.WriteLine($"{read.GetString(1)}\t {read.GetString(2)}\t {read.GetString(3)}\t {read.GetDecimal(4)}\t {read.GetString(5)}"); values = $"{read.GetString(1)}\t {read.GetString(2)}\t {read.GetString(3)}\t {read.GetDecimal(4)}\t {read.GetString(5)}"; }
            visuals.drawLine();

            string[] options = new string[] { "Imie", "Nazwisko", "Email", "Numer Telefonu", "Adres" };
            int selectedOption = 0;
            while(true) {
                Console.Clear();
                visuals.drawLine();
                Console.WriteLine(values);
                visuals.drawLine();
                Console.WriteLine("Co chcesz zmienić?");
                for(int i = 0; i < options.Length; i++) {
                    if(i == selectedOption) visuals.colorReverse();
                    Console.WriteLine($"\t{options[i]}\t\t");
                    visuals.colorReset();
                }
                visuals.drawInstructions();
                var key = Console.ReadKey().Key;
                switch(key) {
                    default: break;
                    case ConsoleKey.DownArrow: if(selectedOption + 1 > options.Count()-1) { } else selectedOption++; break;
                    case ConsoleKey.UpArrow: if(selectedOption - 1 < 0) { } else selectedOption--; break;
                    case ConsoleKey.Enter: changeValue(selectedOption); return;
                    case ConsoleKey.Escape: return;
                }

                Console.Clear();

            }

        }
        void changeValue(int x) {
            string[] options = new string[] { "name", "surname", "email", "phonenumber", "adress" };
            Visuals visuals = new Visuals();
            Console.WriteLine(id);
            visuals.drawLine();

            SQLiteConnection connection = new SQLiteConnection($"data source={dbname}; version=3");
            connection.Open();
            SQLiteCommand cmd = connection.CreateCommand();

            cmd.CommandText = $"select {options[x]} from 'census' where id={id};";
         
            var read = cmd.ExecuteReader();

            if(x == 3) while(read.Read()) Console.Write($"{read.GetDecimal(0)} -> ");
            else while(read.Read()) Console.Write($"{read.GetString(0)} -> ");
            read.Close();

            string value = Console.ReadLine();
            visuals.drawLine();

            Console.Write("Status: ");
            if(string.IsNullOrEmpty(value)) Console.WriteLine("Puste Pola nie są dopuszczalne");
            else if(x == 3) {
                int number = -1;
                try { number = int.Parse(value); } 
                catch(OverflowException) { Console.WriteLine("Numer telefonu jest za długi"); } catch(FormatException) { Console.WriteLine("Numer telefonu zawiera nieprawidłowe znaki!"); }
                cmd.CommandText = $"update census set {options[x]}='{number}' where id={id}";
                cmd.ExecuteNonQuery();
                Console.WriteLine("Gotowe");
            } else {
                cmd.CommandText = $"update census set {options[x]}='{value}' where id={id}";
                cmd.ExecuteNonQuery();
                Console.WriteLine("Gotowe");
            }
            connection.Close();
            Console.ReadKey();
        }

    }
}