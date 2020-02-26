using System.Collections.Generic;
using Lab1.Models;

namespace Lab1.Interfaces
{
    public interface IReader
    {
        IEnumerable<Student> ReadFile(string path);
    }
}
