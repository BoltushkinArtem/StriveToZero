using System;

namespace StriveToZero.Core
{
    class Program
    {
        /// <summary>
        /// Минимальное число для вычитания из игрового числа
        /// </summary>
        const byte MIN_NUMBER_TO_SUBTRACT = 1;

        static void Main(string[] args)
        {
            // Инициализация объекта интерфейса
            View view = new View(new ConsoleWrapper());
            // Инициализация объекта игровой логики
            Game game = new Game();
            // Инициализация игровых данных
            GameInitialization(ref view, ref game);

            // Если был инициирован выход из игры
            if (game.IsGameOver)
            {
                view.WriteGameOverMessage();
                return;
            }
            
            // Запуск игрового процесса
            string winnerName = StartTheGame(view, game);

            // Если был инициирован выход из игры
            if (game.IsGameOver)
            {
                view.WriteGameOverMessage();
                return;
            }

            // Вывод сообщения о победителе
            view.WtiteGameWinnerMessage(winnerName);
        }

        /// <summary>
        /// Метод инициализации игровых данных
        /// </summary>
        /// <param name="view">Объект интерфейса</param>
        /// <param name="game">Объект игровой логики</param>
        static void GameInitialization(ref View view, ref Game game)
        {
            // Вывод на экран названия и описания игры
            view.WriteInfoMessage();
            Console.WriteLine();

            // Ввод и установка типа игры
            game.GameType = view.ReadGameType(ref game.IsGameOver);
            // Если был инициирован выход из игры
            if (game.IsGameOver)
                return;
            // Если была выбрана игра с людьми
            if (game.GameType == Game.Type.Player)
            {
                Console.WriteLine();
                // Ввод и установка списка игроков
                game.SetPlayers(view.ReadPlayers(ref game.IsGameOver));
                // Если был инициирован выход из игры
                if (game.IsGameOver)
                    return;
            }
            else
            {
                Console.WriteLine();
                // 
            }
            Console.WriteLine();

            // Ввод минимального значения интервала игрового числа
            byte min = view.ReadMinIntervalValue(ref game.IsGameOver);
            // Если был инициирован выход из игры
            if (game.IsGameOver)
                return;
            Console.WriteLine();

            // Ввод максимального значения интервала игрового числа
            byte max = view.ReadMaxIntervalValue(ref game.IsGameOver);
            // Если был инициирован выход из игры
            if (game.IsGameOver)
                return;
            Console.WriteLine();

            // Генерация и установка игрового число в заданном интервале
            game.SetGameInterval(min, max);

            // Ввод максимального числа для вычитания из игрового числа
            game.MaxNumberToSubtract = view.ReadMaxNumberToSubtract(ref game.IsGameOver, MIN_NUMBER_TO_SUBTRACT, game.GameNumber);
            // Если был инициирован выход из игры
            if (game.IsGameOver)
                return;
            Console.WriteLine();
        }

        /// <summary>
        /// Метод запуска игрового процесса
        /// </summary>
        /// <param name="view">Объект интерфейса</param>
        /// <param name="game">Объект игровой логики</param>
        /// <returns>Имя победителя</returns>
        static string StartTheGame(View view, Game game)
        {
            // Запуск игрового процесса
            return game.GameLoop(stepData => 
            {
                // Компьютер генерирует свое число
                if (stepData.IsPCStep)
                {
                    byte playerNumber = game.GetPCNumber();
                    view.WtiteCPStepMessage(
                        stepData.PlayerName, 
                        stepData.GameNumber.ToString(),
                        playerNumber.ToString());
                    return playerNumber;
                }
                else
                {
                    // Ввод числа, который будет вычитаться из игрового числа
                    return view.ReadPlayerNumber(
                        ref game.IsGameOver, 
                        stepData.PlayerName, 
                        stepData.GameNumber.ToString(), 
                        MIN_NUMBER_TO_SUBTRACT, 
                        game.MaxNumberToSubtract
                    );
                }
            });
        }
    }
}
