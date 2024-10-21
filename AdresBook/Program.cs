using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace AdresBook {
    internal class Program {
        static void Main(string[] args) {
            string dbname = "database.db";
            Menu menu = new Menu(dbname);
        }
    }
}
