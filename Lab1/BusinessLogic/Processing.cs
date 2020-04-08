using Lab1.Interfaces;
using Lab1.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;

namespace Lab1.BusinessLogic
{
    public class Processing
    {
        private readonly Logger logger;

        private readonly IReader reader;

        private readonly IWriter writer;

        public Processing(Logger logger, IReader reader, IWriter writer)
        {
            this.logger = logger;
            this.reader = reader;
            this.writer = writer;
        }

        /// <summary>
        /// Basiv method with all act
        /// </summary>
        public void Process(string[] commandLine)
        {
            IEnumerable<Student> students;
            try
            {
                students = reader.ReadFile(commandLine[(int)InputData.InputFilePath]);
            }
            catch (FileNotFoundException message)
            {
                Console.WriteLine(message.Message);
                logger.Error($"File not found: {message.Message}");
                return;
            }
            catch (ArgumentException message)
            {
                Console.WriteLine(message.Message);
                logger.Error($"Invalid argument: {message.Message}");
                return;
            }
            catch (IOException message)
            {
                Console.WriteLine(message.Message);
                logger.Error($"Input/output error: {message.Message}");
                return;
            }
            catch (Exception message)
            {
                Console.WriteLine(message.Message);
                logger.Error($"Read error: {message.Message}");
                return;
            }

            try
            {
                students = students.FindAverageMark();
                var allAverage = ConsoleHelper.FindAverageInAllSubjects(students);
                writer.WriteFile(commandLine[(int)InputData.OutputFilePath], students, allAverage);
            }
            catch (ArgumentOutOfRangeException message)
            {
                Console.WriteLine(message.Message);
                logger.Error($"Index out of range: {message.Message}");
                return;
            }
            catch (ArgumentException message)
            {
                Console.WriteLine(message.Message);
                logger.Error($"Invalid argument: {message.Message}");
                return;
            }
            catch (IOException message)
            {
                Console.WriteLine(message.Message);
                logger.Error($"Input/output error: {message.Message}");
                return;
            }
            catch (Exception message)
            {
                Console.WriteLine(message.Message);
                logger.Error($"Write error: {message.Message}");
                return;
            }
            Console.WriteLine("Successfully");
        }
    }
}