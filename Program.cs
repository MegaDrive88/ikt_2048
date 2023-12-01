namespace ikt {
    public class Program {
        static void Main() {
            if (Game.Ready()) {
                Game.Starting();
            }
            while (!Game.Over()) {
                Game.Move();
            }            
        }
    } 
    public static class Game {
        static Random rnd = new();
        static Tile[,] table = new Tile[4, 4];
        public static bool Ready() {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\t   ___   ____  __ __  ____ \n\t  |__ \\ / __ \\/ // / ( __ )\n\t  __/ // / / / // /_/ __  |\n\t / __// /_/ /__  __/ /_/ / \n\t/____/\\____/  /_/  \\____/");
            Console.WriteLine("\tHigh score: $");
            Console.WriteLine("\tPress ENTER to start!");
            while (true) {
                if (Console.ReadKey(true).Key == ConsoleKey.Enter) {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Black;
                    return true;
                }
            }
        }
        public static void Starting() {
            for (int i = 0; i < table.GetLength(0); i++) {
                for (int j = 0; j < 4; j++) {
                    table[i, j] = new Tile(0);
                }
            }
            numberGen();
            numberGen();
            tablePrint();
        }
        public static void Move() {
            //fileWrite(prev);
            Transpose(Console.ReadKey(true).Key);
            //nyert e?
            numberGen();
            tablePrint();
        }
        public static bool Over() {
            return false;
        }
        private static void Transpose(ConsoleKey k) {
            /*
            bal --> [x][1]
            jobb --> [x][2]
            fel --> [1][x]
            le --> [2][x]
            */




            /*
            int y, x;
            switch (k) {
                case ConsoleKey.UpArrow:
                    y = 0; x = 0;
                    break;
                case ConsoleKey.LeftArrow:
                    y = 0; x = 0;
                    break;
                case ConsoleKey.DownArrow:
                    y = 2; x = 0;
                    break;
                case ConsoleKey.RightArrow:
                    y = 0; x = 2;
                    break;
            }
            */




            //for (int i = 0; i < 4; i++) {
            //    for (int j = 0; j < 4; j++) {
            //        Console.WriteLine("we good"); 
            //    }
            //}
        }
        private static void numberGen() {
            int x = rnd.Next(0, 4);
            int y = rnd.Next(0, 4);
            while (!table[y, x].isEmpty) {
                x = rnd.Next(0, 4);
                y = rnd.Next(0, 4);
            }
            table[y, x] = new(new[] { 2, 2, 4 }[rnd.Next(0, 3)]);
        }
        private static void fileRead() {
            // fajl olvasas
        }
        private static void fileWrite(string value) {
            // fajl iras
        }
        private static void tablePrint() {
            for (int i = 0; i < table.GetLength(0); i++) {
                for (int j = 0; j < 4; j++) {
                    table[i, j].Show();
                }
                Console.WriteLine();
            }
        }
    }
    public class Tile {
        public int value { get; private set; }
        private ConsoleColor color;
        public bool isEmpty { get { return value == 0; } }
        public Tile(int Value) {
            value = Value;
            if (value <= 4)
                color = ConsoleColor.White;
            else if (value >= 8 && value <= 64)
                color = ConsoleColor.DarkRed;
            else
                color = ConsoleColor.DarkYellow;
        }
        public void Show() {
            if (!isEmpty) {
                Console.BackgroundColor = color;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write($"[{new string(' ', (6 - value.ToString().Length) / 2)}{value}{new string(' ', (5 - value.ToString().Length) / 2)}]");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("[     ]");
            }
        }
        public static Tile operator+(Tile a, Tile b) {
            return new(a.value + b.value);
        }
    }
}
