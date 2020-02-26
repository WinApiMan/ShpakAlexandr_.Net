using CsvHelper;
using Lab1.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Lab1.BusinessLogic
{
    class CsvReader : Interfaces.IReader
    {
        /// <summary>
        /// Read start csv file
        /// </summary>
        /// <param name="path">path to file</param>
        /// <returns></returns>
        public IEnumerable<Student> ReadFile(string path)
        {
            const int SkipThreeItems = 3;
            using var reader = new StreamReader(path);
            using var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture);
            var records = new List<Student>();

            csv.Read();
            csv.ReadHeader();

            while (csv.Read())
            {
                if (csv.Context.Record.Length != csv.Context.HeaderRecord.Length)
                {
                    throw new InvalidDataException("Wrong number of parameters");
                }
                var listOfSubjects = new List<Subject>();
                for(int index=SkipThreeItems;index< csv.Context.Record.Length;index++)
                {
                    var subject = new Subject(csv.Context.HeaderRecord[index], csv.GetField<int>(index));
                    listOfSubjects.Add(subject);
                }

                records.Add(new Student(csv.GetField(0), csv.GetField(1), csv.GetField(2), listOfSubjects));
            }

            return records;
        }


    }
}
