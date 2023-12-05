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
    //public class StartingNums {
    //    ConsoleKey k { get; set
    //        {
    //            value = value

    //        }
    //    }
    //    object[] nums { get {
    //        switch (k)
    //        {
    //            case ConsoleKey.UpArrow:
    //                return new object[] { 1, 0, -1, 0, 0, 1, 1, -3 };
    //            case ConsoleKey.DownArrow:
    //                return new object[] { 2, 0, 1, 0, 0, -1, 1, 3 };
    //                break;
    //            case ConsoleKey.LeftArrow:
    //                return  new object[] { 0, 1, 0, -1, 1, 0, -3, 1 };
    //            case ConsoleKey.RightArrow:
    //                return new object[] { 0, 2, 0, 1, -1, 0, 3, 1 };
    //            case ConsoleKey.Z:
    //                return new object[] { -1, -1, "Undo" };
    //            case ConsoleKey.Escape:
    //                return new object[] { -1, -1, "Exit" };
                
    //            default:
    //                    return new object[] { -1, -1, "Invalid" };
    //        }
    //    } }


    //    //ConsoleKey.UpArrow, new object[] { 1, 0, -1, 0, 0, 1, 1, -3 }
    //}

    public static class Game {
        static Random rnd = new();
        static Tile[,] table = new Tile[4, 4];
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
            Transpose(GetStartingNums(Console.ReadKey(true).Key));
            //nyert e? -- nem kell kulon fuggveny
        }
        public static bool Over() {
            return false;
        }
        private static object[] GetStartingNums(ConsoleKey k) {
            Dictionary<ConsoleKey, object[]> nums = new Dictionary<ConsoleKey, object[]>
            {
                { ConsoleKey.UpArrow, new object[] { 1, 0, -1, 0, 0, 1, 1, -3 } }, // ezt vhogy refactorolni kéne
                { ConsoleKey.DownArrow, new object[] { 2, 0, 1, 0, 0, -1, 1, 3 } },// de legalább műxik
                { ConsoleKey.LeftArrow, new object[] { 0, 1, 0, -1, 1, 0, -3, 1 } },
                { ConsoleKey.RightArrow, new object[] { 0, 2, 0, 1, -1, 0, 3, 1 } },
                { ConsoleKey.Z, new object[] { -1, -1, "Undo" } },
                { ConsoleKey.Escape, new object[] { -1, -1, "Exit" } },
            };
            try {
                return nums[k];
            }
            catch {
                return new object[] { -1, -1, "Invalid" };
            }
        }
        private static void Transpose(object[] nums) {
            int y = (int)nums[0], x = (int)nums[1];
            bool moved = false;
            if (y != -1) {
                int scout_y = y + (int)nums[2], scout_x = x + (int)nums[3];
                for (int i = 0; i < 4; i++) {
                    for (int j = 0; j < 3; j++) {
                        try {
                            while (table[scout_y, scout_x].isEmpty) {
                                moved = true;
                                scout_y += (int)nums[2];
                                scout_x += (int)nums[3];
                                //valahova ide kéne majd az összeadás
                            }
                        }
                        catch { }
                        scout_y -= (int)nums[2];
                        scout_x -= (int)nums[3];
                        table[scout_y, scout_x] = table[y, x];
                        table[y, x] = moved ? new(0) : table[y, x];
                        x += (int)nums[4];
                        y += (int)nums[5];
                        scout_y = y + (int)nums[2];
                        scout_x = x + (int)nums[3];
                        moved = false;
                    }
                    x += (int)nums[6];
                    y += (int)nums[7];
                    scout_y = y + (int)nums[2];
                    scout_x = x + (int)nums[3];
                }
                numberGen();
                tablePrint();
            }
            else {
                switch (nums[2]) {
                    case "Undo":
                        break;
                    case "Exit":
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
