using Fynd.Services.Contract;
using System;
using System.IO;

namespace Fynd.Services.Implementation
{
    public class FileService : IFileService
    {
        public string GetFilePath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "task3_hotelsrates.json");
        }
    }
}
