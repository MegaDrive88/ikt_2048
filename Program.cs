namespace ikt {
    public class Program {
        static Random rnd = new();
        static void Main() {
            ready();
            Console.ForegroundColor = ConsoleColor.Black;
            




            int numberGen() {
                return rnd.Next(1, 3)*2;
            }
            bool ready() {
                Console.WriteLine("\n   ___   ____  __ __  ____ \n  |__ \\ / __ \\/ // / ( __ )\n  __/ // / / / // /_/ __  |\n / __// /_/ /__  __/ /_/ / \n/____/\\____/  /_/  \\____/");
                Console.WriteLine("Press ENTER to start!");
                while (true){
                    if (Console.ReadKey(true).Key == ConsoleKey.Enter) {
                        Console.Clear();
                        return true;
                    }
                }                
            }
        }
    } 




    public class Number {
        public int value;
        private ConsoleColor color;
        public Number(int Value) {
            value = Value;
            if (value <= 4)
                color = ConsoleColor.White;
            else if (value >= 8 && value <= 64)
                color = ConsoleColor.DarkRed;
            else
                color = ConsoleColor.DarkYellow;
        }
        public void Show() {
            Console.BackgroundColor = color;
            string valueAsAString = $"{value}{(value.ToString().Length <= 2 ? " " : "")}";
            Console.Write($"[{new string(' ', 4-valueAsAString.Length)}{valueAsAString}]");
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
