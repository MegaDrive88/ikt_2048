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
    public static class StartingNums {
        public static int[] Get(ConsoleKey k) {
            return k switch {
                ConsoleKey.UpArrow => new[] { 1, 0, -1, 0, 0, 1, 1, -3 },
                ConsoleKey.DownArrow => new[] { 2, 0, 1, 0, 0, -1, 1, 3 },
                ConsoleKey.LeftArrow => new[] { 0, 1, 0, -1, 1, 0, -3, 1 },
                ConsoleKey.RightArrow => new[] { 0, 2, 0, 1, -1, 0, 3, 1 },
                ConsoleKey.Z => new[] { -1, -1, 0 },
                ConsoleKey.Escape => new[] { -1, -1, 1 },
                _ => new[] { -1, -1, -1 },
            };
        }
    }
    public static class Game {
        private static Random rnd = new();
        private static Tile[,] table = new Tile[4, 4];
        static int score = 0;
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
            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 4; j++) {
                    table[i, j] = new(0);
                }
            }
            numberGen();
            numberGen();
            tablePrint();
        }
        public static void Move() {
            //fileWrite(prev);
            Transpose(StartingNums.Get(Console.ReadKey(true).Key));
            //nyert e? -- nem kell kulon fuggveny  
        }
        public static bool Over() {
            return false;
        }
        private static void Transpose(int[] nums) {
            int y = nums[0], x = nums[1];
            bool moved = false;
            if (y != -1) {
                int scout_y = y + nums[2], scout_x = x + nums[3];
                for (int i = 0; i < 4; i++) {
                    for (int j = 0; j < 3; j++) {
                        try {
                            while (table[scout_y, scout_x].isEmpty) {
                                moved = true;
                                scout_y += nums[2];
                                scout_x += nums[3];
                                //valahova ide kéne majd az összeadás
                            }
                        }
                        catch { }
                        scout_y -= nums[2];
                        scout_x -= nums[3];
                        table[scout_y, scout_x] = table[y, x];
                        table[y, x] = moved ? new(0) : table[y, x];
                        x += nums[4];
                        y += nums[5];
                        scout_y = y + nums[2];
                        scout_x = x + nums[3];
                        moved = false;
                    }
                    x += nums[6];
                    y += nums[7];
                    scout_y = y + nums[2];
                    scout_x = x + nums[3];
                }
                numberGen(); // csak ha volt mozgás
                tablePrint();
            }
            else {
                switch (nums[2]) {
                    case 0:
                        //undo
                        break;
                    case 1:
                        // exit
                        break;
                    default:
                        break;
                }
            }
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
            Console.Clear();
            for (int i = 0; i < table.GetLength(0); i++) {
                for (int j = 0; j < 4; j++) {
                    table[i, j].Show();
                }
                Console.WriteLine();
            }
        }
    }
    public class Tile {
        public int Value { get; private set; }
        private ConsoleColor color;
        public bool isEmpty { get { return Value == 0; } }
        public Tile(int value) {
            Value = value;
            if (Value <= 4)
                color = ConsoleColor.White;
            else if (Value >= 8 && Value <= 64)
                color = ConsoleColor.DarkRed;
            else
                color = ConsoleColor.DarkYellow;
        }
        public void Show() {
            if (!isEmpty) {
                Console.BackgroundColor = color;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write($"[{new string(' ', (6 - Value.ToString().Length) / 2)}{Value}{new string(' ', (5 - Value.ToString().Length) / 2)}]");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("[     ]");
            }
        }
        public static Tile operator+(Tile a, Tile b) {
            return new(a.Value + b.Value);
        }
    }
}
