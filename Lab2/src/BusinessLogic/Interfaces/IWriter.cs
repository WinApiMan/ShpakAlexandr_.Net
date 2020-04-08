using System.Collections.Generic;

namespace BusinessLogic.Interfaces
{
    public interface IWriter
    {
        void Write<T>(IEnumerable<T> list, string path);
    }
}
