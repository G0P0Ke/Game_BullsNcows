using System;
using System.Diagnostics;

namespace Bulls_n_cows
{
    class Program
    {
        // Метод для отображения коровы и быка.
        static void ASCIIArt()
        {
            Console.WriteLine("          COW                                   BULL");
            Console.WriteLine("      /( ,,,,, )\\                              /)  (\\      _____________        ");
            Console.WriteLine("     _\\ ;;;;;;; /_                        .-._((,~~.))_.-,|  Извините,  |");
            Console.WriteLine("  .-\"; ;;;;;;;;; ;\"-.                      `=.  ' '   ,=' | зря быканул |");
            Console.WriteLine("  '.__/`_ / \\ _`\\__.'_____________           / ,o~~o. \\   |_____________|");
            Console.WriteLine("     | (')| |(') |  |     Чё     |          { { .__. } }  /        ");
            Console.WriteLine("     | .--' '--. |  |   Пялишь?  |           ) `~~~\\' (");
            Console.WriteLine("     |/ o     o \\|  |____________|          /`-._  _\\.-\\");
            Console.WriteLine("     |           | /                       /         )  \\");
            Console.WriteLine("    / \\ _..=.._ / \\                      ,-X        #   X-.");
            Console.WriteLine("   /:. '._____.'   \\                    /    \\        /    \\");

            Console.WriteLine("* Back to menu");
            while (Console.ReadKey().Key != ConsoleKey.Enter) // Пока не нажат Enter меню не запустится.
            {
            }

            Menu(1, false);
            ControlByArrows();
        }

        // Метод для отображения правил игры.
        static void Rules()
        {
            Console.WriteLine("Правила игры Быки и коровы: \n" +
                              "  Я задумываю тайное N-значное число (N < 10) с неповторяющимися цифрами (без ведущего 0). \n" +
                              "  Вы делаете первую попытку отгадать число. \n" +
                              "  Попытка — это N-значное (N < 10) число с неповторяющимися цифрами, сообщаемое мне. \n" +
                              "  Я сообщаю в ответ, сколько цифр угадано без совпадения с их позициями в тайном числе (то есть количество коров)\n" +
                              "  и сколько угадано вплоть до позиции в тайном числе (то есть количество быков). \n" +
                              "  Играем до тех пор, пока Вы полностью не отгадаете тайное число. \n" +
                              "  Вперед!!!!");

            Console.WriteLine("* Back to menu");
            while (Console.ReadKey().Key != ConsoleKey.Enter) // Пока не нажат Enter меню не запустится.
            {
            }
            
            Menu(1, false);
            ControlByArrows();
        }

        // Метод для управления выбором в меню.
        static void ControlByArrows()
        {
            int position = 1;
            ConsoleKeyInfo key;
            key = Console.ReadKey();

            while (key.Key.ToString() != "Enter")
            {
                string choice = key.Key.ToString();
                switch (choice)
                {
                    case "UpArrow":
                        position -= 1;
                        if (position == 0)
                        {
                            position = 5;
                        }

                        break;
                    case "DownArrow":
                        position += 1;
                        if (position == 6)
                        {
                            position = 1;
                        }

                        break;
                }

                Menu(position, false);
                key = Console.ReadKey();
            }

            Menu(position, true);
        }


        /// <summary>
        /// Метод для меню.
        /// </summary>
        /// <param name="position"> Позиция в меню. </param>
        /// <param name="flag"> Маркер того, нажат ли Enter. </param>
        static void Menu(int position, bool flag)
        {
            Console.Clear();
            Console.WriteLine($"{(position == 1 ? "*" : "")}  Classic Game");
            Console.WriteLine($"{(position == 2 ? "*" : "")}  Hard Game");
            Console.WriteLine($"{(position == 3 ? "*" : "")}  Rules");
            Console.WriteLine($"{(position == 4 ? "*" : "")}  Funny art");
            Console.WriteLine($"{(position == 5 ? "*" : "")}  Quit");
            if (flag)
            {
                switch (position)
                {
                    case 1:
                        Console.WriteLine("Число загадано.");
                        string answer = GenerateNumber(4);
                        Game(4, answer);
                        break;
                    case 2:
                        HardGame();
                        break;
                    case 3:
                        Rules();
                        break;
                    case 4:
                        ASCIIArt();
                        break;
                    case 5:
                        Console.WriteLine("See u later, My friend!");
                        Process.GetCurrentProcess().Kill(); // Заверешние приложения.
                        break;
                }
            }
        }

        /// <summary>
        /// Метод для генерации случайного числа.
        /// </summary>
        /// <param name="n"> Разрядность числа. </param>
        /// <returns> Сгенерированное число. </returns>
        static string GenerateNumber(int n)
        {
            Random rand = new Random();
            int[] numberArray = new int[n];
            for (int i = 0; i < n; i++)
            {
                numberArray[i] = -1;
            }

            for (int i = 0; i < n; i++)
            {
                int flag = 0;
                while (flag != 1)
                {
                    int digit = rand.Next(0, 10);
                    if (Array.IndexOf(numberArray, digit) == -1)
                    {
                        if (i == 0 && digit != 0)
                        {
                            numberArray[i] = digit;
                            flag = 1;
                        }
                        else if (i != 0)
                        {
                            numberArray[i] = digit;
                            flag = 1;
                        }
                    }
                }
            }

            string number = String.Empty;
            for (int i = 0; i < n; i++)
            {
                number += numberArray[i].ToString();
            }

            return number;
        }

        // Метод для ввода длины числа в сложном режиме игры.
        static void HardGame()
        {
            Console.WriteLine($"Введите N: ");
            string length = Console.ReadLine();
            if (int.TryParse(length, out int result) && result <= 10 && result > 0)
            {
                Console.WriteLine("Число загадано.");
                string answer = GenerateNumber(result);
                Game(result, answer);
            }
            else
            {
                Console.WriteLine("Incorrect input (N should be in range of [1,10]).");
                HardGame();
            }
        }

        /// <summary>
        /// Метод самой игры. 
        /// </summary>
        /// <param name="n"> Разрядность загаданного числа. </param>
        /// <param name="answer"> Загаданное число. </param>
        static void Game(int n, string answer)
        {
            int length = n;
            Console.WriteLine("Введите свое число или латинскую букву \"M\", чтобы вернуться в меню." +
                              "(нынешяя игры будет закончена)");
            string number = Console.ReadLine();
            Console.WriteLine(answer);
            int flag = 1;
            if (number.Length == 0)
            {
                Console.WriteLine("Incorrect input.");
                Game(n, answer);
            }

            if (NumberCheck(number) == false)
            {
                Console.WriteLine("Incorrect input.");
                Game(n, answer);
            }
            char firstDigit = number[0];
            while (flag == 1)
            {
                if (number == "M")
                {
                    flag = 0;
                    Menu(1, false);
                    ControlByArrows();
                }
                else if (long.TryParse(number, out long result) && firstDigit != '0' && number.Length == answer.Length && firstDigit != '-')
                {
                    int bulls = HowManyBulls(answer, number);
                    int cows = HowManyCows(answer, number);
                    Console.WriteLine($"Bulls: {bulls} Cows: {cows}");
                    if (bulls == answer.Length)
                    {
                        Console.WriteLine("Allright Allright Allright \n" +
                                          "Congratulations!!! \n" +
                                          "Menu - \"M\",Quit - \"Q\"");
                        FinishTheGame();
                    }

                    Game(length, answer);
                }
                else
                {
                    Console.WriteLine("Incorrect input.");
                    Game(length, answer);
                }
            }
        }

        /// <summary>
        /// Метод для подсчета цифр, стоящих на тех же местах, что и в загаданном числе (быков).
        /// </summary>
        /// <param name="answer"> Загаданное число. </param>
        /// <param name="number"> Число, введенное пользователем. </param>
        /// <returns> bulls = Количество быков. </returns>
        static int HowManyBulls(string answer, string number)
        {
            int bulls = 0;
            for (int i = 0; i < answer.Length; i++)
            {
                if (number[i] == answer[i])
                {
                    bulls += 1;
                }
            }
            
            return bulls;
        }
        
        /// <summary>
        /// Метод для проверки повторяющихся цифр в числе.
        /// </summary>
        /// <param name="number"> Введенное число. </param>
        /// <returns> False - если есть повторяющиеся, True - в противном случае. </returns>
        static bool NumberCheck(string number)
        {
            for (int i = 0; i < number.Length; i++)
            {
                for (int j = 0; j < number.Length; j++)
                {
                    if (number[i] == number[j] && i != j)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        
        // Метод для выбора действия после победы в игре.
        static void FinishTheGame()
        {
            Console.WriteLine("Ваш ответ: ");
            string finish = Console.ReadLine();
            if (finish == "M")
            {
                Menu(1, false);
                ControlByArrows();
            }
            else if (finish == "Q")
            {
                Console.WriteLine("See u later, My friend!");
                Process.GetCurrentProcess().Kill(); // Заверешние приложения.
            }
            else
            {
                Console.WriteLine("Incorrect input.");
                FinishTheGame();
            }
        }

        /// <summary>
        /// Метод для подсчета совпадающих цифр введенного и загаданного чисел.
        /// </summary>
        /// <param name="answer"> Загаданное число. </param>
        /// <param name="number"> Введенное пользователем число. </param>
        /// <returns> cows = Количество коров. </returns>
        static int HowManyCows(string answer, string number)
        {
            int cows = 0;
            int[] answerArray = new int[answer.Length];
            int[] numberArray = new int[number.Length];
            for (int j = 0; j < answer.Length; j++)
            {
                answerArray[j] = int.Parse(answer[j].ToString());
                numberArray[j] = int.Parse(number[j].ToString());
            }

            for (int i = 0; i < answer.Length; i++)
            {
                if (Array.IndexOf(answerArray, numberArray[i]) != -1 && Array.IndexOf(answerArray, numberArray[i]) != i)
                {
                    cows += 1;
                }
            }

            return cows;
        }    
        
        static void Main(string[] args)
        {
            while (true)
            {
                Menu(1, false);
                ControlByArrows();
            }
        }
    }
}