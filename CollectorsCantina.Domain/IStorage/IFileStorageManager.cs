namespace CollectorsCantina.Domain.IStorage
{
    public interface IFileStorageManager
    {
        void DeleteFile(string fileName, string containerName);
        void DeleteContainer(string containerName);
        List<string> ListFiles(string container);
        string UploadFile(Stream stream, string filename, string contentType, string containerName);
    }
}