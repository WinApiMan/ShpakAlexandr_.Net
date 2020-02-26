using Lab1.Interfaces;
using Lab1.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Lab1.BusinessLogic
{
    public static class ConsoleHelper
    {
        /// <summary>
        /// Find average
        /// </summary>
        /// <param name="students">students list</param>
        /// <returns></returns>
        public static IEnumerable<Student> FindAverageMark(this IEnumerable<Student> students)
        {
            foreach (var student in students)
                student.AverageMark = Math.Round(student.Subjects.Average(e => e.Mark),2);
            return students;
        }

        /// <summary>
        /// Find average
        /// </summary>
        /// <param name="students"></param>
        /// <returns></returns>
        public static double FindAverageGroupRating(IEnumerable<Student> students)
        {
            return Math.Round(students.Average(e => e.AverageMark), 2);
        }

        /// <summary>
        /// Convert Student to StudentAverageMark
        /// </summary>
        /// <param name="students"></param>
        /// <returns></returns>
        public static IEnumerable<StudentAverageMark> StudentToStudentAverageMark(IEnumerable<Student> students)
        {
            var resultList = new List<StudentAverageMark>();
            foreach (var student in students)
            {
                resultList.Add(new StudentAverageMark(student.Name, student.Surname, student.Patronymic, student.AverageMark));
            }
            return resultList;
        }

        /// <summary>
        /// Find average for all subjects
        /// </summary>
        /// <param name="students">students list</param>
        /// <returns></returns>
        public static IEnumerable<Subject> FindAverageInAllSubjects(IEnumerable<Student> students)
        {
            var allMarks = new List<Subject>();
            var averageMarks = new List<Subject>();

            foreach (var student in students)
                allMarks.AddRange(student.Subjects);

            foreach (var subject in students.Last().Subjects)
                averageMarks.Add(new Subject(subject.SubjectName,Math.Round(allMarks.Where(e => e.SubjectName.Equals(subject.SubjectName)).Average(e => e.Mark),2)));

            return averageMarks;
        }

        public static string DefineFormat(this string name)
        {
            var type = typeof(FileFormat);
            var memInfo = type.GetMember(name);
            var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            var description = ((DescriptionAttribute)attributes[0]).Description;
            return description;
        }

        /// <summary>
        /// Choose writer class
        /// </summary>
        /// <param name="commandLineArguments">parametrs of command line</param>
        /// <returns></returns>
        public static (IWriter,string[]) ChooseWriteFile(string[] line)
        {
            const int CommandCount = 3;

            bool flag = false;

            IWriter writerFormat = null;

            while (!flag)
            {
                if (line.Length == CommandCount)
                {
                    if (line[(int)InputData.Format].Equals(DefineFormat(FileFormat.Excel.ToString())))
                    {
                        writerFormat = new ExcelWriter();
                        flag = true;
                    }
                    else if (line[(int)InputData.Format].Equals(FileFormat.Json.ToString().DefineFormat()))
                    {
                        writerFormat = new JsonWriter();
                        flag = true;
                    }
                    else
                    {
                        Console.WriteLine($"Incorrect format:{line[(int)InputData.Format]}.Enter:");
                        line[(int)InputData.Format] = Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("Wrong number of parameters enter,enter the correct paramerts:");
                    Console.WriteLine("'Input file name' 'Output file name' 'format'");
                    line = Console.ReadLine().Split();
                }
            }
            return (writerFormat,line);
        }
    }
}
