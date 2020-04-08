using System.Collections.Generic;

namespace BusinessLogic.Interfaces
{
    public interface IReader
    {
        IEnumerable<T> Read<T>(string path);
    }
}
