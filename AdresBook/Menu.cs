using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdresBook {
    internal class Menu {
        private string dbname;
        public Menu(string dbname) {
            this.dbname = dbname;
            new DatabaseHandler(dbname);
            try { selectOptions(); } catch(Exception) { Console.Clear(); Console.WriteLine("Wystąpił nieoczekiwany błąd"); }
            
        }
        Visuals visuals = new Visuals();
        
        void selectOptions() {
            string[] options = { "+ Przegladaj", "+ Dodaj       ", "+ Wyszukaj     ", "+ Zamknij      " };
            int selectedOption = 0;

            while(true) {
                visuals.drawLine();
                visuals.drawLogo();
                visuals.drawEmpty();

                for(int i = 0; i < options.Length; i++) {
                    visuals.tabs(3);
                    if(selectedOption == i) visuals.colorReverse();
                    Console.WriteLine($"\t{options[i]}\t\t");
                    visuals.colorReset();
                }
                visuals.drawInstructions();

                var option = Console.ReadKey().Key;
                switch(option) {
                    case ConsoleKey.Escape: Console.Clear(); Environment.Exit(0); break;
                    case ConsoleKey.DownArrow: if(selectedOption + 1 > options.Count() - 1) { } else { selectedOption++; } break;
                    case ConsoleKey.UpArrow: if(selectedOption - 1 < 0) { } else { selectedOption--; } break;
                    case ConsoleKey.Enter:
                    switch(selectedOption) {
                        default: Console.Clear(); break;
                        case 0: new BrowseDatabase(dbname); break;
                        case 1: new AddRecord(dbname); break;
                        case 2: new Search(dbname); break;
                        case 3: Console.Clear(); return;
                    }
                    break;
                    default: break;
                }
                Console.Clear();
            }
        }

    }
}
