using Lab1.Models;
using System.Collections.Generic;

namespace Lab1.Interfaces
{
    public interface IWriter
    {
        void WriteFile(string fileName, IEnumerable<Student> students, IEnumerable<Subject> allAvarage);
    }
}
