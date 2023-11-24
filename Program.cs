namespace ikt {
    public class Program {
        static Random rnd = new();
        static Tile[,] table = new Tile[4, 4];
        static void Main() {
            if (ready()) {
                starting();
            }
            

            






            void numberGen() {
                int x = rnd.Next(0, 4);
                int y = rnd.Next(0, 4);
                while (!table[y, x].isEmpty) {
                    x = rnd.Next(0, 4);
                    y = rnd.Next(0, 4);
                }
                table[y, x] = new(new[] { 2, 2, 4 }[rnd.Next(0, 3)]);
            }
            void fileRead() {
                // fajl olvasas
            }
            void fileWrite(string value) {
                // fajl iras
            }
            bool ready() {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\t   ___   ____  __ __  ____ \n\t  |__ \\ / __ \\/ // / ( __ )\n\t  __/ // / / / // /_/ __  |\n\t / __// /_/ /__  __/ /_/ / \n\t/____/\\____/  /_/  \\____/");
                Console.WriteLine("\tHigh score: $");
                Console.WriteLine("\tPress ENTER to start!");
                while (true){
                    if (Console.ReadKey(true).Key == ConsoleKey.Enter) {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Black;
                        return true;
                    }
                }                
            }
            void starting() {
                for (int i = 0; i < table.GetLength(0); i++) {
                    for (int j = 0; j < 4; j++) {
                        table[i,j] = new Tile(0);
                    }
                }
                numberGen();
                numberGen();
                tablePrint();
            }
            void move() {
                // fel le bal jobb

                //végére tableprint, fájlírás, lehetőségek
                numberGen();
            }






            void tablePrint() {
                for(int i = 0; i < table.GetLength(0); i++) {
                    for(int j = 0;j < 4; j++) {
                        table[i, j].Show();
                    }
                    Console.WriteLine();
                }
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
