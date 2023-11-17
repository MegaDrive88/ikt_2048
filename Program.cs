namespace ikt {
    public class Program {
        static void Main() {
            Number a = new(numberGen());
            a.Show();
            int numberGen() {
                Random rnd = new();
                return rnd.Next(1, 3)*2;
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
                color = ConsoleColor.Red;
            else
                color = ConsoleColor.Yellow;
        }
        public void Show() {
            Console.ForegroundColor = color;
            Console.Write(value);
            Console.ResetColor();
        }
    }
}
