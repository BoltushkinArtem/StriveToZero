using System;
using System.Collections.Generic;

namespace StriveToZero
{
    /// <summary>
    /// Класс взаимодействия с пользователем
    /// </summary>
    public class View
    {
        // Клавиша выхода из игры
        const ConsoleKey QUIT_KEY = ConsoleKey.Q;
        
        // Текст сообщения о некорректно введенных данных
        const string NOT_CORRECT_VALUE_MESSAGE = "\n! ВЫ ВВЕЛИ НЕКОРРЕКТНОЕ ЗНАЧЕНИЕ, ПОПРОБУЙТЕ СНОВА.\n";

        // Текст сообщения для запроса интервала игрового числа
        const string INTERVAL_MESSAGE = "Задайте {0} значения в интервате 1 - 255 для выбора игрового числа.\n{1} - выйти из игры";

        /// <summary>
        /// Метод вывода названия и описания игры
        /// </summary>
        public void WriteInfoMessage()
        {
            Console.Clear();
            
            WriteLineByHorizontalCenter("СТРЕМИСЬ К НУЛЮ");
            Console.WriteLine();
            WriteLineByHorizontalCenter("Правила игры:");
            WriteLineByHorizontalCenter(new string[]{
                "1. Программа загадывает случайное число в заданном игроками диапазоне сообщая это число игрокам.",
                "2. Игроки ходят по очереди.",
                "3. Игрок, ход которого указан вводит число от 1 до 10 в зависимости от загаданного числа.",
                "4. Введенное игроком число вычитается из загаданного.",
                "5. Выигрывает тот игрок, после чьего хода загаданное число обратилась в ноль."
            });
        }

        /// <summary>
        /// Метод ввода типа игры
        /// </summary>
        /// <param name="isGameOver">Ссылка на переменную, содержащую признак завершения игры</param>
        /// <returns>Тип игры</returns>
        public Game.Type ReadGameType(ref bool isGameOver)
        {
            Game.Type gameType = Game.Type.Player;
            
            // Ввод не прекратится до тех пор, пока пользователь не введет корректные значения для продолжения работы
            bool isValidValue = false;
            do
            {
                Console.WriteLine($"С кем бы Вы хотели играть?\n1 – пользователь;\n2 – компьютер;\n{QUIT_KEY.ToString()} - выйти из игры");
                Console.Write("Строка для ввода ~ % ");
                ConsoleKey readKey = Console.ReadKey().Key;
                Console.WriteLine();

                switch (readKey)
                {
                    case ConsoleKey.D1:
                    {
                        gameType = Game.Type.Player;
                        isValidValue = true;
                        break;
                    }
                    case ConsoleKey.D2:
                    {
                        gameType = Game.Type.Computer;
                        isValidValue = true;
                        break;
                    }
                    case ConsoleKey.Q:
                    {
                        isGameOver = true;
                        isValidValue = true;
                        break;
                    }
                    default:
                    {
                        WriteNotCorrectValueMessage();
                        break;
                    }
                }
            }
            while (!isValidValue);

            return gameType;
        }

        /// <summary>
        /// Метод ввода списка игроков
        /// </summary>
        /// <param name="isGameOver">Ссылка на переменную, содержащую признак завершения игры</param>
        /// <returns>Списк игроков</returns>
        public List<string> ReadPlayers(ref bool isGameOver)
        {
            List<string> players = new List<string>();

            // Ввод не прекратится до тех пор, пока пользователь не введет корректные значения для продолжения работы
            // Ввод имени игрока
            bool isReadFinished = false;
            do
            {
                Console.WriteLine($"Введите, пожалуйста, имена игроков.\n{QUIT_KEY.ToString()} - выйти из игры");
                Console.Write("Строка для ввода ~ % ");
                string line = Console.ReadLine();
                Console.WriteLine();

                // Если был инициирован выход из игры
                if (line.Length == QUIT_KEY.ToString().Length && string.Equals(line.ToUpper(), QUIT_KEY.ToString()))
                {
                    isGameOver = isReadFinished = true;
                    break;
                }

                players.Add(line);
                
                // Ввод не прекратится до тех пор, пока пользователь не введет корректные значения для продолжения работы
                // Ввод ответа на вопрос о добавлении новых игроков
                bool isValidAnswer = false;
                do
                {
                    Console.WriteLine("Хотите добавить еще одного игрока? [Y]/[N]");
                    Console.Write("Строка для ввода ~ % ");
                    
                    ConsoleKey key = Console.ReadKey().Key;
                    Console.WriteLine();

                    switch (key)
                    {
                        case ConsoleKey.N:
                        {
                            isReadFinished = isValidAnswer = true;
                            break;
                        }
                        case ConsoleKey.Y:
                        {
                            isValidAnswer = true;
                            Console.WriteLine();
                            break;
                        }
                        default:
                        {
                            isValidAnswer = false;
                            WriteNotCorrectValueMessage();
                            break;
                        }
                    }
                } while (!isValidAnswer);
                
            } while (!isReadFinished);

            return players;
        }

        /// <summary>
        /// Метод ввода минимального значения интервала игрового числа
        /// </summary>
        /// <param name="isGameOver">Ссылка на переменную, содержащую признак завершения игры</param>
        /// <returns>Минимальное значение интервала игрового числа</returns>
        public byte ReadMinIntervalValue(ref bool isGameOver)
        {
            return ReadByteGameValue(ref isGameOver, string.Format(INTERVAL_MESSAGE, "минимальное", QUIT_KEY.ToString()), 1);
        }

        /// <summary>
        /// Метод ввода максимального значения интервала игрового числа
        /// </summary>
        /// <param name="isGameOver">Ссылка на переменную, содержащую признак завершения игры</param>
        /// <returns>Максимальное значение интервала игрового числа</returns>
        public byte ReadMaxIntervalValue(ref bool isGameOver)
        {
            return ReadByteGameValue(ref isGameOver, string.Format(INTERVAL_MESSAGE, "максимальное", QUIT_KEY.ToString()), 1);
        }

        /// <summary>
        /// Метод вывода сообщения о некорректно введенных данных
        /// </summary>
        public void WriteNotCorrectValueMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(NOT_CORRECT_VALUE_MESSAGE);
            Console.ForegroundColor = ConsoleColor.White;
        }
        
        /// <summary>
        /// Метод ввода числа в диапазоне 0-255
        /// </summary>
        /// <param name="isGameOver">Ссылка на переменную, содержащую признак завершения игры</param>
        /// <param name="message">Сообщение для пользователя, описывающее условия ввода числа</param>
        /// <param name="minNumber">Искусственно заданное минимальное значение для вводимого числа</param>
        /// <returns>Число в диапазоне 0-255</returns>
        public byte ReadByteGameValue(ref bool isGameOver, string message, byte minNumber)
        {
            byte value = 0;

            // Ввод не прекратится до тех пор, пока пользователь не введет корректные значения для продолжения работы
            bool isValidValue = false;
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(message);
                Console.Write("Строка для ввода ~ % ");
                string line = Console.ReadLine();
                
                // Если был инициирован выход из игры
                if (line.Length == QUIT_KEY.ToString().Length && string.Equals(line.ToUpper(), QUIT_KEY.ToString()))
                {
                    isGameOver = isValidValue = true;
                    break;
                }

                // Если введенное значение невозможно преобразовать в число в диапазоне 0-255
                if (!byte.TryParse(line, out value))
                {
                    WriteNotCorrectValueMessage();
                    continue;
                }
                
                // Если введенное значение меньше искусственно заданного минимального значения для вводимого числа
                if (value < minNumber)
                {
                    WriteNotCorrectValueMessage();
                    continue;
                }
                
                isValidValue = true;
            }
            while (!isValidValue);

            return value;
        }

        /// <summary>
        /// Метод вывода строки текста по горизонтали, по центру экрана
        /// </summary>
        /// <param name="str">Строка текста</param>
        public void WriteLineByHorizontalCenter(string str)
        {
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (str.Length / 2)) + "}", str));
        }

        /// <summary>
        /// Метод вывода массива строк текста по горизонтали, по центру экрана 
        /// с выравниванием по левому краю
        /// </summary>
        /// <param name="strs">Массив строк</param>
        public void WriteLineByHorizontalCenter(string[] strs)
        {
            // Определение максимальной длины строки, для расчета отступа
            int maxLength = 0;
            foreach (string str in strs)
            {
                if (str.Length > maxLength) maxLength = str.Length;
            }

            foreach (string str in strs)
            {
                Console.SetCursorPosition(((Console.WindowWidth / 2) - (maxLength / 2)), Console.CursorTop);
                Console.WriteLine(str);
            }
        }
    }
}