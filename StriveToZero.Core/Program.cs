using System;

namespace StriveToZero.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            // Инициализация объекта интерфейса
            View view = new View();
            //  Инициализация объекта игровой логики
            Game game = new Game();

            // Вывод на экран названия и описания игры
            view.WriteInfoMessage();
            Console.WriteLine();

            // Ввода и установка типа игры
            game.GameType = view.ReadGameType(ref game.isGameOver);
            // Если был инициирован выход из игры
            if (game.isGameOver)
                return;
            // Если была выбрана игра с людьми
            if (game.GameType == Game.Type.Player)
            {
                Console.WriteLine();
                // Ввода и установка списка игроков
                game.Players = view.ReadPlayers(ref game.isGameOver);
                // Если был инициирован выход из игры
                if (game.isGameOver)
                    return;
            }
            Console.WriteLine();

            // Ввода минимального значения интервала игрового числа
            byte min = view.ReadMinIntervalValue(ref game.isGameOver);
            // Если был инициирован выход из игры
            if (game.isGameOver)
                return;
            Console.WriteLine();
            
            // Ввода максимального значения интервала игрового числа
            byte max = view.ReadMaxIntervalValue(ref game.isGameOver);
            // Если был инициирован выход из игры
            if (game.isGameOver)
                return;
            Console.WriteLine();

            // Генерирующия и установка игровое число в заданном интервале
            game.SetGameInterval(min, max);

            Console.ReadKey();
        }
    }
}
