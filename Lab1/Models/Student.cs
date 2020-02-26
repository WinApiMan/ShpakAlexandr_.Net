using System.Collections.Generic;
using System.Linq;

namespace Lab1.Models
{
    /// <summary>
    /// Student class
    /// </summary>

    public class Student
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public double AverageMark { get; set; }

        public IEnumerable<Subject> Subjects { get; set; }


        public Student(string name, string surname, string patronymic, IEnumerable<Subject> subjects)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Subjects = subjects.ToList();
        }
    }
}
