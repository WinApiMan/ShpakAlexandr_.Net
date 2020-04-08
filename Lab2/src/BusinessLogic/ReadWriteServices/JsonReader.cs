using BusinessLogic.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace BusinessLogic.Services.ReadWriteServices
{
    public class JsonReader : IReader
    {
        public IEnumerable<T> Read<T>(string path)
        {
            using var fileStream = new FileStream(path, FileMode.OpenOrCreate);
            var json = new DataContractJsonSerializer(typeof(List<T>));
            try
            {
                return json.ReadObject(fileStream) as List<T>;
            }
            catch
            {
                return new List<T>();
            }
        }
    }
}