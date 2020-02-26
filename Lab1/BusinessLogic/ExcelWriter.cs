using Lab1.Models;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab1.BusinessLogic
{
    class ExcelWriter : Interfaces.IWriter
    {
        /// <summary>
        /// Write result to csv file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="students"></param>
        public void WriteFile(string fileName, IEnumerable<Student> students, IEnumerable<Subject> allAvarage)
        {
            var excelFile = new FileInfo(string.Concat($"{fileName}.", FileFormat.Excel.ToString().DefineFormat()));

            using var package = new ExcelPackage(excelFile);

            var studentsList = ConsoleHelper.StudentToStudentAverageMark(students);

            var avarageGroupRating = new AverageGroupRating(ConsoleHelper.FindAverageGroupRating(students), studentsList, allAvarage.ToList());

            var worksheet = package.Workbook.Worksheets.Add($"{typeof(StudentAverageMark).ToString()}{package.Workbook.Worksheets.Count}");

            worksheet.Cells[1, 1].LoadFromCollection(avarageGroupRating.Students, true);

            int lastRow = worksheet.Dimension.End.Row;

            worksheet.Cells[lastRow + 1, 1].LoadFromCollection(avarageGroupRating.Subjects, true);

            lastRow = worksheet.Dimension.End.Row;

            worksheet.Cells[lastRow + 1, 1].Value = avarageGroupRating.Average;

            package.Save();
        }
    }
}
