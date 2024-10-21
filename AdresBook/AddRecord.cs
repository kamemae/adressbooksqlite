using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdresBook {
    internal class AddRecord {
        public AddRecord(string dbname = "database.db") {
            Visuals visuals = new Visuals();
            //1: overflow, 2: format
            string[] phoneNumberErrorsTranslations = new string[] { "Wprowadzony numer telefonu jest za długi! Zostanie zastąpiony 0", "Uzyto niepoprawnych znaków, sprawdź czy użyleś liczb! Zostanie zastąpiony 0" };
            string[] databaseInfomations = new string[] { "Imie", "Nazwisko", "Email", "Numer Telefonu", "Adres" };
            
            Console.Clear();
            visuals.drawLine();

            Console.Write(databaseInfomations[0] + ": "); string inputName = Console.ReadLine();
            Console.Write(databaseInfomations[1] + ": "); string inputSurname = Console.ReadLine();
            Console.Write(databaseInfomations[2] + ": "); string inputEmail = Console.ReadLine();

            Console.Write(databaseInfomations[3] + ": "); int inputPhoneNumber = 0;
            try { inputPhoneNumber = int.Parse(Console.ReadLine()); } 
            catch(OverflowException) { visuals.colorRed(); Console.WriteLine(phoneNumberErrorsTranslations[0]); visuals.colorReset(); }
            catch(FormatException) { visuals.colorRed(); Console.WriteLine(phoneNumberErrorsTranslations[1]); visuals.colorReset(); }

            Console.Write("Address: "); string inputAddress = Console.ReadLine();
            visuals.drawLine();

            string[] options = new string[] { "Tak", "Nie" };
            int selectedOption = 0;
            while(true) {
                Console.WriteLine("Czy napewno chcesz dodać?");
                for(int i = 0; i < options.Length; i++) {
                    if(i == selectedOption) visuals.colorReverse();
                    Console.WriteLine($"\t{options[i]}\t\t");
                    visuals.colorReset();
                }
                visuals.drawInstructions();
                var key = Console.ReadKey().Key;
                switch(key) {
                    default: break;
                    case ConsoleKey.DownArrow: if(selectedOption + 1 > 1) { } else selectedOption++; break;
                    case ConsoleKey.UpArrow: if(selectedOption - 1 < 0) { } else selectedOption--; break;
                    case ConsoleKey.Enter:
                    switch(selectedOption) {
                        default: break;
                        case 0: Console.Clear(); Console.Write(addToDatabase(inputName, inputSurname, inputEmail, inputPhoneNumber, inputAddress, dbname) + ", nacisnij dowolny przycisk..."); Console.ReadKey(); return;
                        case 1: Console.Clear(); Console.WriteLine("Anulowano, nacisnij dowolny przycisk..."); Console.ReadKey(); return;
                    }
                    break;
                    case ConsoleKey.Escape: return;
                }

                Console.Clear();
                visuals.drawLine();
                Console.WriteLine($"{databaseInfomations[0]}: {inputName}\n{databaseInfomations[1]}: {inputSurname}\n{databaseInfomations[2]}: {inputEmail}\n{databaseInfomations[3]}: {inputPhoneNumber}\n{databaseInfomations[4]}: {inputAddress}");
                visuals.drawLine();
            }
        }
        string addToDatabase(string name, string surname, string email, int phone, string adress, string dbname = "database.db") {
            SQLiteConnection connection = new SQLiteConnection($"data source={dbname}; version=3");
            connection.Open();
            SQLiteCommand cmd = connection.CreateCommand();
            cmd.CommandText = $"insert into census (name, surname, email, phonenumber, adress) values ('{name}', '{surname}', '{email}', {phone}, '{adress}');";
            try { var read = cmd.ExecuteReader(); } catch(Exception) { connection.Close(); return "Wystąpił nieoczekiwany błąd!"; }
            connection.Close(); return "Dodano pomyślnie";
        }
    }
}
