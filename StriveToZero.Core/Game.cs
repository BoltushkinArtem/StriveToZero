using System;
using System.Collections.Generic;

namespace StriveToZero.Core
{
    /// <summary>
    /// Класс данных игрового шага
    /// </summary>
    public class StepData
    {
        /// <summary>
        /// Имя игрока
        /// </summary>
        public string PlayerName;
        
        /// <summary>
        /// Игровое число
        /// </summary>
        public byte GameNumber;

        public StepData(string playerName, byte gameNumber)
        {
            PlayerName = playerName;
            GameNumber = gameNumber;
        }
    }

    /// <summary>
    /// Класс игровой логики
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Типы игры
        /// </summary>
        public enum Type
        {
            // Игра с людьми
            Player,
            
            // Игра с компьютером
            Computer
        }

        /// <summary>
        /// Признак завершения игры
        /// </summary>
        public bool IsGameOver = false;

        /// <summary>
        /// Типыигры
        /// </summary>
        public Type GameType;

        /// <summary>
        /// Список игроков
        /// </summary>
        private List<string> _players;

        /// <summary>
        /// Игровое число
        /// </summary>
        public byte GameNumber;

        /// <summary>
        /// Максимальное число для вычитания из игрового числа
        /// Диапазон от 1 до максимального числа
        /// </summary>
        public byte MaxNumberToSubtract;

        /// <summary>
        /// Метод генерирующий и устанавливающий игровое число в заданном интервале
        /// </summary>
        /// <param name="min">Минимальное число интервала</param>
        /// <param name="max">Максимальное число интервала</param>
        public void SetGameInterval(byte min, byte max)
        {
            GameNumber = byte.Parse(new Random().Next(min, max).ToString());
        }

        /// <summary>
        /// Метод установки списка игроков
        /// </summary>
        /// <param name="players">списк игроков</param>
        public void SetPlayers(List<string> players)
        {
            _players = players;
        }

        /// <summary>
        /// Метод добавления нового игрока в список игроков
        /// </summary>
        /// <param name="name">Имя игрока</param>
        public void AddPlayer(string name)
        {
            if (_players == null)
                _players = new List<string>();
            _players.Add(name);
        }

        /// <summary>
        /// Метод игрового цикла
        /// </summary>
        /// <param name="bodyOfLoop">Тело игрового цикла. Шаг игрового цикла</param>
        /// <returns>Имя победителя</returns>
        public string PlayersGameLoop(Func<StepData, byte> bodyOfLoop)
        {
            StepData stepData = new StepData(string.Empty, GameNumber);
            int PlayerStepId = -1;

            if (_players.Count <= 0)
                return stepData.PlayerName;

            do
            {
                PlayerStepId ++;
                if (PlayerStepId >= _players.Count)
                    PlayerStepId = 0;

                stepData.PlayerName = _players[PlayerStepId];
                byte playerNumber = bodyOfLoop(stepData);

                if (IsGameOver)
                {
                    stepData.PlayerName = string.Empty;
                    break;
                }

                stepData.GameNumber -= playerNumber;
            }
            while(stepData.GameNumber > 0);

            return stepData.PlayerName;
        }
    }
}