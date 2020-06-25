using System;

namespace StriveToZero.Core
{
    public interface IConsole
    {
        void Clear();
        void Write(string message);
        void WriteLine();
        void WriteLine(string message);
        string ReadLine();
        ConsoleKeyInfo ReadKey();
    }
}