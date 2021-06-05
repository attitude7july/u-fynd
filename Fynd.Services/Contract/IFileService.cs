namespace Fynd.Services.Contract
{
    public interface IFileService
    {
        string ReadJsonFile(string fileName);
        T GetObject<T>(string jsonString);
        string Task3FileName { get; }
        string Task2FileName { get; }
    }
}
