namespace ikt {
    public class Program {
        static Random rnd = new();
        static Tile[,] table = new Tile[4, 4];
        static void Main() {
            Console.ForegroundColor = ConsoleColor.White;
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
                table[y, x] = new(rnd.Next(1, 3) * 2);
            }
            void fileRead() {
                // fajl olvasas
            }
            bool ready() {
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
        public int value;
        private ConsoleColor color;
        public bool isEmpty = false;
        public Tile(int Value) {
            value = Value;
            if (value == 0)
                isEmpty = true;
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
                string valueAsAString = $"{value}{(value.ToString().Length <= 2 ? " " : "")}";
                Console.Write($"[{new string(' ', 4 - valueAsAString.Length)}{valueAsAString}]");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("[    ]");
            }


        }
    }
}
