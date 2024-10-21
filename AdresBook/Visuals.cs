using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdresBook {
    internal class Visuals {
        public void drawLine(int x = 82) { for(int i = 0; i < x; i++) Console.Write("="); Console.WriteLine(); }
        public void drawSpaces(int x = 1) { for(int i = 0; i < x; i++) Console.Write(" "); Console.WriteLine(); }
        public void drawLogo() { Console.WriteLine("             _           _                     _                                  \r\n    /\\ /\\___(_) __ _ ___| | ____ _    __ _  __| |_ __ ___  ___  _____      ____ _ \r\n   / //_/ __| |/ _` |_  / |/ / _` |  / _` |/ _` | '__/ _ \\/ __|/ _ \\ \\ /\\ / / _` |\r\n  / __ \\\\__ \\ | (_| |/ /|   < (_| | | (_| | (_| | | |  __/\\__ \\ (_) \\ V  V / (_| |\r\n  \\/  \\/|___/_|\\__,_/___|_|\\_\\__,_|  \\__,_|\\__,_|_|  \\___||___/\\___/ \\_/\\_/ \\__,_|"); }
        public void drawEmpty(int x = 1) { for(int i = 0; i < x; i++) Console.WriteLine(""); }
        public void drawInstructions(int x = 1) { drawEmpty(); Console.ForegroundColor = ConsoleColor.DarkGray; Console.WriteLine("ArrowUp&ArrowDown - przeglądaj między opcjami\nEnter - wybierz zaznaczoną opcje\nESC - zamknij"); Console.ForegroundColor = ConsoleColor.White; }
        public void drawInstructionsModify(int x = 1) { drawEmpty(); Console.ForegroundColor = ConsoleColor.DarkGray; Console.WriteLine("ArrowUp&ArrowDown - przeglądaj między opcjami\nE - Edytuj Dane\nD - Usuń Wpis"); Console.ForegroundColor = ConsoleColor.White; }
        public void colorReverse() { Console.BackgroundColor = ConsoleColor.White; Console.ForegroundColor = ConsoleColor.Black; }
        public void colorReset() { Console.BackgroundColor = ConsoleColor.Black; Console.ForegroundColor = ConsoleColor.White; }
        public void tabs(int x = 1) { for(int i = 0; i<x; i++) Console.Write("\t"); }
        public void colorRed() { Console.ForegroundColor = ConsoleColor.Red; }
        
    }
}
