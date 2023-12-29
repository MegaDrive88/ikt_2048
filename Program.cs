﻿using System.IO;

namespace ikt {
    public class Program {
        static void Main() {
            if (Game.Ready()) {
                Game.Starting(Game.NewGameChoice());
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
        private static List<string?> file = new();
        public static int score;
        private static bool moveReady = true;
        public static bool Ready() {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\t   ___   ____  __ __  ____ \n\t  |__ \\ / __ \\/ // / ( __ )\n\t  __/ // / / / // /_/ __  |\n\t / __// /_/ /__  __/ /_/ / \n\t/____/\\____/  /_/  \\____/");
            Console.WriteLine($"\tHigh score: {getHighscore()}");
            Console.WriteLine("\tPress ENTER to start!");
            while (true) {
                if (Console.ReadKey(true).Key == ConsoleKey.Enter) {
                    Console.Clear();
                    return true;
                }
            }
        }
        public static bool NewGameChoice() {
            try {
                getTableFromFile();
            }
            catch {
                return true;
            }
            Console.WriteLine("\tContinue last game (ENTER) or start a new one (N)?");
            Console.ForegroundColor = ConsoleColor.Black;
            while (true) {
                switch (Console.ReadKey(true).Key) {
                    case ConsoleKey.Enter:
                        Console.Clear();
                        return false;
                    case ConsoleKey.N: 
                        Console.Clear();
                        return true;
                }
            }
        }
        public static void Starting(bool wantsNewGame) {
            if (wantsNewGame) {
                score = 0;
                for (int i = 0; i < 4; i++) {
                    for (int j = 0; j < 4; j++) {
                        table[i, j] = new(0);
                    }
                }
                numberGen();
                numberGen();
                writeTableToFile();
                writeTableToFile2();
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (table[i,j].Value >= 2048)
                    {
                        alrwon = true;
                    }
                }
            }
        }
        public static void Move() {
            if (moveReady) {
                moveReady = false;
                getTableFromFile();
                tablePrint();
                Transpose(table, StartingNums.Get(Console.ReadKey(true).Key));
                writeTableToFile();
                refreshHighscore();
                CheckWinCondition();
                //nyert e? 
                moveReady = true;
            }
        }
        public static bool Over()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (table[i, j].isEmpty)
                    {
                        return false;
                    }
                    if (j < 3 && table[i, j].Value == table[i, j + 1].Value)
                    {
                        return false;
                    }
                    if (i < 3 && table[i, j].Value == table[i + 1, j].Value)
                    {
                        return false;
                    }
                }
            }
            getTableFromFile();
            tablePrint();
            Console.ResetColor();
            Console.WriteLine("\r\n $$$$$$\\   $$$$$$\\  $$\\      $$\\ $$$$$$$$\\        $$$$$$\\  $$\\    $$\\ $$$$$$$$\\ $$$$$$$\\  \r\n$$  __$$\\ $$  __$$\\ $$$\\    $$$ |$$  _____|      $$  __$$\\ $$ |   $$ |$$  _____|$$  __$$\\ \r\n$$ /  \\__|$$ /  $$ |$$$$\\  $$$$ |$$ |            $$ /  $$ |$$ |   $$ |$$ |      $$ |  $$ |\r\n$$ |$$$$\\ $$$$$$$$ |$$\\$$\\$$ $$ |$$$$$\\          $$ |  $$ |\\$$\\  $$  |$$$$$\\    $$$$$$$  |\r\n$$ |\\_$$ |$$  __$$ |$$ \\$$$  $$ |$$  __|         $$ |  $$ | \\$$\\$$  / $$  __|   $$  __$$< \r\n$$ |  $$ |$$ |  $$ |$$ |\\$  /$$ |$$ |            $$ |  $$ |  \\$$$  /  $$ |      $$ |  $$ |\r\n\\$$$$$$  |$$ |  $$ |$$ | \\_/ $$ |$$$$$$$$\\        $$$$$$  |   \\$  /   $$$$$$$$\\ $$ |  $$ |\r\n \\______/ \\__|  \\__|\\__|     \\__|\\________|       \\______/     \\_/    \\________|\\__|  \\__|\r\n                                                                                          \r\n                                                                                          \r\n                                                                                          \r\n");
            Console.ForegroundColor = ConsoleColor.Black;
            File.WriteAllText("../prev.txt", string.Empty);
            return true;
        }
        public static void Exit()
        {
            Console.ResetColor();
            Console.WriteLine("Exiting the game...");
            Console.ForegroundColor = ConsoleColor.Black;
            Environment.Exit(0);
        }
        public static bool alrwon = false;
        public static void CheckWinCondition()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (table[i, j].Value >= 65000)
                    {
                        Won();
                    }
                    if (table[i, j].Value >= 2048 && alrwon == false)
                    {
                        Won();
                    }
                }
            }
        }
        public static void Won()
        {
            if (alrwon == true)
            {
                Console.Clear();
                Console.ResetColor();
                Console.WriteLine("\r\n$$\\     $$\\  $$$$$$\\  $$\\   $$\\       $$\\      $$\\  $$$$$$\\  $$\\   $$\\ $$\\ \r\n\\$$\\   $$  |$$  __$$\\ $$ |  $$ |      $$ | $\\  $$ |$$  __$$\\ $$$\\  $$ |$$ |\r\n \\$$\\ $$  / $$ /  $$ |$$ |  $$ |      $$ |$$$\\ $$ |$$ /  $$ |$$$$\\ $$ |$$ |\r\n  \\$$$$  /  $$ |  $$ |$$ |  $$ |      $$ $$ $$\\$$ |$$ |  $$ |$$ $$\\$$ |$$ |\r\n   \\$$  /   $$ |  $$ |$$ |  $$ |      $$$$  _$$$$ |$$ |  $$ |$$ \\$$$$ |\\__|\r\n    $$ |    $$ |  $$ |$$ |  $$ |      $$$  / \\$$$ |$$ |  $$ |$$ |\\$$$ |    \r\n    $$ |     $$$$$$  |\\$$$$$$  |      $$  /   \\$$ | $$$$$$  |$$ | \\$$ |$$\\ \r\n    \\__|     \\______/  \\______/       \\__/     \\__| \\______/ \\__|  \\__|\\__|\r\n                                                                           \r\n                                                                           \r\n                                                                           \r\n");
                Console.ForegroundColor = ConsoleColor.Black;
                File.WriteAllText("../prev.txt", string.Empty);
                Environment.Exit(0);
            }
            else
            {
                alrwon = true;
                Console.Clear();
                Console.ResetColor();
                Console.WriteLine("\r\n$$\\     $$\\  $$$$$$\\  $$\\   $$\\       $$\\      $$\\  $$$$$$\\  $$\\   $$\\ $$\\ \r\n\\$$\\   $$  |$$  __$$\\ $$ |  $$ |      $$ | $\\  $$ |$$  __$$\\ $$$\\  $$ |$$ |\r\n \\$$\\ $$  / $$ /  $$ |$$ |  $$ |      $$ |$$$\\ $$ |$$ /  $$ |$$$$\\ $$ |$$ |\r\n  \\$$$$  /  $$ |  $$ |$$ |  $$ |      $$ $$ $$\\$$ |$$ |  $$ |$$ $$\\$$ |$$ |\r\n   \\$$  /   $$ |  $$ |$$ |  $$ |      $$$$  _$$$$ |$$ |  $$ |$$ \\$$$$ |\\__|\r\n    $$ |    $$ |  $$ |$$ |  $$ |      $$$  / \\$$$ |$$ |  $$ |$$ |\\$$$ |    \r\n    $$ |     $$$$$$  |\\$$$$$$  |      $$  /   \\$$ | $$$$$$  |$$ | \\$$ |$$\\ \r\n    \\__|     \\______/  \\______/       \\__/     \\__| \\______/ \\__|  \\__|\\__|\r\n                                                                           \r\n                                                                           \r\n                                                                           \r\n");
                Console.WriteLine("Continue playing (ENTER) exit the game (ESC)");
                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    Exit();
                }
            }
        }
        private static void Transpose(Tile[,] table, int[] nums) {
            int y = nums[0], x = nums[1];
            bool moved = false;
            bool numberGenNeeded = false;
            if (y != -1) {
                writeTableToFile2();
                int scout_y = y + nums[2], scout_x = x + nums[3];
                for (int i = 0; i < 4; i++) {
                    for (int j = 0; j < 3; j++) {
                        try {
                            while (table[scout_y, scout_x].isEmpty) {
                                moved = true;
                                scout_y += nums[2];
                                scout_x += nums[3];
                            }
                            if (table[scout_y, scout_x].Value == table[y, x].Value && !table[scout_y, scout_x].isEmpty && !table[scout_y, scout_x].comesFromMerge) {
                                table[scout_y, scout_x] += table[y, x];
                                table[scout_y, scout_x].comesFromMerge = true;
                                score += table[scout_y, scout_x].Value;
                                table[y, x] = new(0);
                                numberGenNeeded = true;
                            }
                        } catch { }
                        scout_y -= nums[2];
                        scout_x -= nums[3];
                        table[scout_y, scout_x] = table[y, x];
                        if (moved && table[y, x].Value != 0) {
                            table[y, x] = new(0);
                            numberGenNeeded = true;
                        }
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
                if (numberGenNeeded)
                    numberGen();
            }
            else {
                switch (nums[2]) {
                    case 0:
                        //undo
                        //gettablefromfile
                        getTableFromFile2();
                        tablePrint();
                        break;
                    case 1:
                        // exit
                        Exit();
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
        private static void fileRead(string filename) {
            using (StreamReader sr = new(filename)) {
                while (!sr.EndOfStream) {
                    file.Add(sr.ReadLine());
                }
            }                
        }
        private static void fileWrite(string filename, string value, bool appendMode = false) {
            try {
                using (StreamWriter sw = new(filename, appendMode)) {
                    sw.Write(value);
                }
            }
            catch {
                fileWrite(filename, value, appendMode);
            }
        }
        private static void getTableFromFile() {
            fileRead("../prev.txt");
            for(int i = 0; i < 4; i++) {
                string[] oneLine = file[i].Split('\t');
                for (int j = 0; j < 4; j++) {
                    table[i, j] = new(int.Parse(oneLine[j]));
                }
            }
            score = int.Parse(file[4]);
        }
        private static void writeTableToFile() {
            fileWrite("../prev.txt", "");
            file = new();
            for (int i = 0; i < 4; i++) {
                fileWrite("../prev.txt", $"{table[i, 0].Value}\t{table[i, 1].Value}\t{table[i, 2].Value}\t{table[i, 3].Value}\n", true);
            }
            fileWrite("../prev.txt", $"{score}", true);
        }
        private static void getTableFromFile2()
        {
            fileRead("../undo.txt");
            for (int i = 0; i < 4; i++)
            {
                string[] oneLine = file[i].Split('\t');
                for (int j = 0; j < 4; j++)
                {
                    table[i, j] = new(int.Parse(oneLine[j]));
                }
            }
            score = int.Parse(file[4]);
        }
        private static void writeTableToFile2()
        {
            fileWrite("../undo.txt", "");
            file = new();
            for (int i = 0; i < 4; i++)
            {
                fileWrite("../undo.txt", $"{table[i, 0].Value}\t{table[i, 1].Value}\t{table[i, 2].Value}\t{table[i, 3].Value}\n", true);
            }
            fileWrite("../undo.txt", $"{score}", true);
        }
        private static void refreshHighscore() {
            fileRead("../hiscore.txt");
            if (score > int.Parse(file[^1])) {
                fileWrite("../hiscore.txt", $"{score}");
            }
            file = new();
        }
        private static int getHighscore() {
            fileRead("../hiscore.txt");
            int hs = int.Parse(file[^1]);
            file = new();
            return hs;
        }
        private static void tablePrint() {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Score: {score}; High score: {getHighscore()}");
            for (int i = 0; i < table.GetLength(0); i++) {
                for (int j = 0; j < 4; j++) {
                    table[i, j].comesFromMerge = false;
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
        public bool comesFromMerge { get; set; }
        public Tile(int value) {
            Value = value;
            comesFromMerge = false;
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
