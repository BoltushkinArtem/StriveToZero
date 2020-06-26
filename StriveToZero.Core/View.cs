using System;
using System.Collections.Generic;

namespace StriveToZero.Core
{
    /// <summary>
    /// Класс взаимодействия с пользователем
    /// </summary>
    public class View
    {
        IConsole console;
        // Клавиша выхода из игры
        const ConsoleKey QUIT_KEY = ConsoleKey.Q;

        // Текст сообщения о некорректно введенных данных
        const string NOT_CORRECT_VALUE_MESSAGE = "\n! ВЫ ВВЕЛИ НЕКОРРЕКТНОЕ ЗНАЧЕНИЕ, ПОПРОБУЙТЕ СНОВА.\n";

        // Текст сообщения для запроса интервала игрового числа
        const string INTERVAL_MESSAGE = "Задайте {0} значения в интервате 1 - 255 для выбора игрового числа.\n{1} - выйти из игры";

        // Текст сообщения, описывающего строку для ввода
        const string THE_LINE_TO_ENTER = "Строка для ввода ~ % ";

        /// <summary>
        /// Метод вывода строки текста по горизонтали, по центру экрана
        /// </summary>
        /// <param name="str">Строка текста</param>
        private void WriteLineByHorizontalCenter(string str)
        {
            console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (str.Length / 2)) + "}", str));
        }

        /// <summary>
        /// Метод вывода массива строк текста по горизонтали, по центру экрана 
        /// с выравниванием по левому краю
        /// </summary>
        /// <param name="strs">Массив строк</param>
        private void WriteLineByHorizontalCenter(string[] strs)
        {
            // Определение максимальной длины строки, для расчета отступа
            int maxLength = 0;
            foreach (string str in strs)
            {
                if (str.Length > maxLength) maxLength = str.Length;
            }

            // Вывод строк
            foreach (string str in strs)
            {
                // Если ширина экрана больше или равна длине максимальной строки
                if (Console.WindowWidth >= maxLength)
                    Console.SetCursorPosition(((Console.WindowWidth / 2) - (maxLength / 2)), Console.CursorTop);

                console.WriteLine(str);
            }
        }

        /// <summary>
        /// Метод вывода сообщения о некорректно введенных данных
        /// </summary>
        private void WriteNotCorrectValueMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            console.WriteLine(NOT_CORRECT_VALUE_MESSAGE);
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Метод проверки инициализации выхода из игры
        /// </summary>
        /// <param name="line">Введенное значение</param>
        /// <returns>Результат проверки</returns>
        private bool IsGameOver(string line)
        {
            return line.Length == QUIT_KEY.ToString().Length && string.Equals(line.ToUpper(), QUIT_KEY.ToString());
        }

        /// <summary>
        /// Метод цикличного ввода данных с клавиатуры  
        /// </summary>
        /// <param name="message">Текст выводимого сообщения перед строкой ввода</param>
        /// <param name="bodyOfLoop">Метод должен обрабатывать введенное пользователем значение и условие выхода из цикла ввод. Выход из цикла – return true</param>
        /// <typeparam name="T">Тип вводимого значения. Строка/символ</typeparam>
        private void ReadToLoop<T>(string message, Func<T, bool> bodyOfLoop)
        {
            // Ввод не прекратится до тех пор, пока пользователь не введет корректные значения для продолжения работы
            T value = default(T);
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                if (!string.IsNullOrEmpty(message))
                {
                    console.WriteLine(message);
                }
                console.Write(THE_LINE_TO_ENTER);

                if (typeof(T) == typeof(string))
                {
                    value = (T)(object)console.ReadLine();
                }
                else if (typeof(T) == typeof(ConsoleKey))
                {
                    value = (T)(object)console.ReadKey().Key;
                    console.WriteLine();
                }
            }
            while (!bodyOfLoop(value));
        }

        /// <summary>
        /// Метод ввода числа в диапазоне 0-255
        /// </summary>
        /// <param name="isGameOver">Ссылка на переменную, содержащую признак завершения игры</param>
        /// <param name="message">Сообщение для пользователя, описывающее условия ввода числа</param>
        /// <param name="minNumber">Искусственно заданное минимальное значение для вводимого числа</param>
        /// <returns>Число в диапазоне 0-255</returns>
        private byte ReadByteGameValue(ref bool isGameOver, string message, byte minNumber)
        {
            byte value = 0;
            bool isLocalGameOver = isGameOver;

            // Ввод не прекратится до тех пор, пока пользователь не введет корректные значения для продолжения работы
            ReadToLoop<string>(message, line =>
            {
                // Если был инициирован выход из игры
                if (IsGameOver(line))
                {
                    return isLocalGameOver = true;
                }

                // Если введенное значение невозможно преобразовать в число в диапазоне 0-255
                if (!byte.TryParse(line, out value))
                {
                    WriteNotCorrectValueMessage();
                    return false;
                }

                // Если введенное значение меньше искусственно заданного минимального значения для вводимого числа
                if (value < minNumber)
                {
                    WriteNotCorrectValueMessage();
                    return false;
                }

                return true;
            });

            isGameOver = isLocalGameOver;

            return value;
        }

        public View(IConsole console)
        {
            this.console = console;
        }

        /// <summary>
        /// Метод вывода названия и описания игры
        /// </summary>
        public void WriteInfoMessage()
        {
            console.Clear();

            WriteLineByHorizontalCenter("СТРЕМИСЬ К НУЛЮ");
            console.WriteLine();
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
            string message = $"С кем бы Вы хотели играть?\n1 – пользователь;\n2 – компьютер;\n{QUIT_KEY.ToString()} - выйти из игры";
            bool isLocalGameOver = isGameOver;
            ReadToLoop<ConsoleKey>(message, readKey =>
            {
                switch (readKey)
                {
                    case ConsoleKey.D1:
                        {
                            gameType = Game.Type.Player;
                            return true;
                        }
                    case ConsoleKey.D2:
                        {
                            gameType = Game.Type.Computer;
                            return true;
                        }
                    case ConsoleKey.Q:
                        {
                            isLocalGameOver = true;
                            return true;
                        }
                    default:
                        {
                            WriteNotCorrectValueMessage();
                            return false;
                        }
                }
            });

            isGameOver = isLocalGameOver;

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
            bool isLocalGameOver = isGameOver;
            ReadToLoop<string>($"Введите, пожалуйста, имена игроков.\n{QUIT_KEY.ToString()} - выйти из игры", line =>
            {
                // Если был инициирован выход из игры
                if (IsGameOver(line))
                {
                    isLocalGameOver = true;
                    return true;
                }

                players.Add(line);

                // Ввод не прекратится до тех пор, пока пользователь не введет корректные значения для продолжения работы
                bool isReadFinished = false;
                ReadToLoop<ConsoleKey>("Хотите добавить еще одного игрока? [Y]/[N]", key =>
                {
                    switch (key)
                    {
                        case ConsoleKey.N:
                            {
                                return isReadFinished = true;
                            }
                        case ConsoleKey.Y:
                            {
                                console.WriteLine();
                                isReadFinished = false;
                                return true;
                            }
                        default:
                            {
                                WriteNotCorrectValueMessage();
                                return isReadFinished = false;
                            }
                    }
                });

                return isReadFinished;
            });

            isGameOver = isLocalGameOver;

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
        /// Метод ввода максимального числа для вычитания из игрового числа
        /// </summary>
        /// <param name="isGameOver">Ссылка на переменную, содержащую признак завершения игры</param>
        /// <param name="min">Минимальное число интервала</param>
        /// <param name="max">Максимальное число интервала</param>
        /// <returns></returns>
        public byte ReadMaxNumberToSubtract(ref bool isGameOver, byte min, byte max)
        {
            byte value = 0;

            // Ввод не прекратится до тех пор, пока пользователь не введет корректные значения для продолжения работы
            string message = $"Задайте, пожалуйста максимальное число, которое можно будет вычесть из игрового числа.\nЗначение в интервате {min} - {max}.\n{QUIT_KEY.ToString()} - выйти из игры";
            bool isLocalGameOver = isGameOver;
            ReadToLoop<string>(message, line => 
            {
                // Если был инициирован выход из игры
                if (IsGameOver(line))
                {
                    return isLocalGameOver = true;
                }

                // Если введенное значение невозможно преобразовать в число в диапазоне 0-255
                if (!byte.TryParse(line, out value))
                {
                    WriteNotCorrectValueMessage();
                    return false;
                }

                // Если введенное значение не попадает в заданный интервал
                if ((value < min) || (value > max))
                {
                    WriteNotCorrectValueMessage();
                    return false;
                }

                return true;
            });

            isGameOver = isLocalGameOver;

            return value;
        }
    }
}