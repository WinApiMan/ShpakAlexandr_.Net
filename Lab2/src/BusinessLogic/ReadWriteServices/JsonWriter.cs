using BusinessLogic.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace BusinessLogic.Services.ReadWriteServices
{
    public class JsonWriter : IWriter
    {
        public void Write<T>(IEnumerable<T> list, string path)
        {
            using FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
            var json = new DataContractJsonSerializer(typeof(List<T>));
            try
            {
                json.WriteObject(fileStream, list);
            }
            catch (IOException)
            {
            }
        }
    }
}