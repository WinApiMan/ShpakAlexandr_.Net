using Lab1.BusinessLogic;
using NLog;
using System;

namespace Lab1
{
    /// <summary>
    /// Main class
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args">console arguments</param>
        static void Main(string[] args)
        {
            const int CommandCount = 4,IndexOfFirstParametr=1;
            var reader = new CsvReader();
            string[] commandLine = Environment.GetCommandLineArgs()[IndexOfFirstParametr..CommandCount];
            var (writer,line) = ConsoleHelper.ChooseWriteFile(commandLine);
            var conventer = new Processing(LogManager.GetCurrentClassLogger(),reader,writer);
            conventer.Process(line);
            Console.ReadKey();
        }
    }
}
