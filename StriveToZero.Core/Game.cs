using System;
using System.Collections.Generic;

namespace StriveToZero.Core
{
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
        public bool isGameOver = false;

        /// <summary>
        /// Типыигры
        /// </summary>
        public Type GameType;

        /// <summary>
        /// Список игроков
        /// </summary>
        public List<string> Players;

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
        /// Метод, генерирующий и устанавливающий игровое число в заданном интервале
        /// </summary>
        /// <param name="min">Минимальное число интервала</param>
        /// <param name="max">Максимальное число интервала</param>
        public void SetGameInterval(byte min, byte max)
        {
            GameNumber = byte.Parse(new Random().Next(min, max).ToString());
        }
    }
}