using Fynd.Services.Contract;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Fynd.Services.Implementation
{
    public class FileService : IFileService
    {
        public string ReadJsonFile(string fileName)
        {
            return File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", fileName));
        }

        public T GetObject<T>(string jsonString)
        {

            return JsonConvert.DeserializeObject<T>(jsonString,
                     new JsonSerializerSettings
                     {
                         MissingMemberHandling = MissingMemberHandling.Ignore,
                         DateFormatString = "yyyy-MM-dd"
                     });

        }

        public string Task3FileName => "task3_hotelsrates.json";
        public string Task2FileName => "task2_ hotelrates.json";
    }

}
