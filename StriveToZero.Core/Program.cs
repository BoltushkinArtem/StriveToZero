using System;

namespace StriveToZero.Core
{
    class Program
    {
        const byte MIN_NUMBER_TO_SUBTRACT = 1;
        static void Main(string[] args)
        {
            // Инициализация объекта интерфейса
            View view = new View(new ConsoleWrapper());
            //  Инициализация объекта игровой логики
            Game game = new Game();

            GameInitialization(ref view, ref game);

            // Если был инициирован выход из игры
            if (game.isGameOver)
                return;

            Console.ReadKey();
        }

        static void GameInitialization(ref View view, ref Game game)
        {
            // Вывод на экран названия и описания игры
            view.WriteInfoMessage();
            Console.WriteLine();

            // Ввод и установка типа игры
            game.GameType = view.ReadGameType(ref game.isGameOver);
            // Если был инициирован выход из игры
            if (game.isGameOver)
                return;
            // Если была выбрана игра с людьми
            if (game.GameType == Game.Type.Player)
            {
                Console.WriteLine();
                // Ввод и установка списка игроков
                game.Players = view.ReadPlayers(ref game.isGameOver);
                // Если был инициирован выход из игры
                if (game.isGameOver)
                    return;
            }
            Console.WriteLine();

            // Ввод минимального значения интервала игрового числа
            byte min = view.ReadMinIntervalValue(ref game.isGameOver);
            // Если был инициирован выход из игры
            if (game.isGameOver)
                return;
            Console.WriteLine();
            
            // Ввод максимального значения интервала игрового числа
            byte max = view.ReadMaxIntervalValue(ref game.isGameOver);
            // Если был инициирован выход из игры
            if (game.isGameOver)
                return;
            Console.WriteLine();

            // Генерация и установка игрового число в заданном интервале
            game.SetGameInterval(min, max);

            // Ввод максимального числа для вычитания из игрового числа
            game.MaxNumberToSubtract = view.ReadMaxNumberToSubtract(ref game.isGameOver, MIN_NUMBER_TO_SUBTRACT, game.GameNumber);
            // Если был инициирован выход из игры
            if (game.isGameOver)
                return;
            Console.WriteLine();
        }
    }
}
