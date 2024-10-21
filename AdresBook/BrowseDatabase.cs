using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Xml.Linq;

namespace AdresBook {
    internal class BrowseDatabase {
        public BrowseDatabase(string dbname) {
            Dictionary<int, string> census = new Dictionary<int, string>();
            FillCensus fillCensus = new FillCensus(census, 0, dbname);

            Console.Clear();
            int selectedOption = 0;
            Visuals visuals = new Visuals();
            
            int page = 0;
            while(true) {
                visuals.drawLine();
                for(int i = 0; i < census.Count; i++) {
                    if(i == selectedOption) visuals.colorReverse();
                    int dictionaryKey = census.Keys.ElementAt(i);
                    Console.WriteLine($"-+\t{census[dictionaryKey]}\t");
                    visuals.colorReset();
                }
                if(census.Keys.ElementAt(selectedOption) == -1 && page > 0) { } else Console.WriteLine("\t>>> Strona " + (page + 1) + " <<<");
                visuals.drawInstructionsModify();
                var key = Console.ReadKey().Key;
                switch(key) {
                    case ConsoleKey.Escape: return;
                    case ConsoleKey.DownArrow: if(selectedOption + 1 > census.Count() - 1) { } else { selectedOption++; } break;
                    case ConsoleKey.UpArrow: if(selectedOption - 1 < 0) { } else { selectedOption--; } break;
                    case ConsoleKey.RightArrow: page++; fillCensus.fill(census, page, dbname); /*if(census.Keys.ElementAt(selectedOption) == -1)*/ break;
                    case ConsoleKey.LeftArrow: if(page - 1 < 0) { } else page--; fillCensus.fill(census, page, dbname);break;
                    case ConsoleKey.E: if(!(census.Keys.ElementAt(selectedOption) == -1)) new ModifyRecord(census.Keys.ElementAt(selectedOption), dbname); census.Clear(); fillCensus.fill(census, page, dbname); break;
                    case ConsoleKey.D: if(!(census.Keys.ElementAt(selectedOption) == -1)) new DeleteRecord(census.Keys.ElementAt(selectedOption), dbname); census.Clear(); fillCensus.fill(census, page, dbname); break;
                    default: break;
                }
                Console.Clear();
            }
        }
    }
}
