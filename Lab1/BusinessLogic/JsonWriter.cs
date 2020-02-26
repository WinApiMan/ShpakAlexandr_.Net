using Lab1.Models;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Lab1.BusinessLogic
{
    class JsonWriter : Interfaces.IWriter
    {
        /// <summary>
        /// Write result to json file
        /// </summary>
        /// <param name="path">path to file</param>
        /// <param name="students">students list</param>
        /// <param name="allAvarage">avarages for all subjects</param>
        public void WriteFile(string fileName, IEnumerable<Student> students, IEnumerable<Subject> allAvarage)
        {
            using var fileStream = new FileStream(string.Concat($"{fileName}.", FileFormat.Json.ToString().DefineFormat()), FileMode.OpenOrCreate);
            var studentList = ConsoleHelper.StudentToStudentAverageMark(students);
            var json = new DataContractJsonSerializer(typeof(AverageGroupRating));
            var averageGroupRating = new AverageGroupRating(ConsoleHelper.FindAverageGroupRating(students), studentList, allAvarage);
            json.WriteObject(fileStream, averageGroupRating);
        }
    }
}
