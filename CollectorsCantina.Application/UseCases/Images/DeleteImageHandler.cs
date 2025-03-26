using CollectorsCantina.Domain.IStorage;
using CollectorsCantina.Domain.IUseCases.Images;

namespace CollectorsCantina.Application.UseCases.Images
{
    public class DeleteImageHandler : IDeleteImageHandler
    {
        private readonly IFileStorageManager _fileStorageManager;

        public DeleteImageHandler(IFileStorageManager fileStorageManager)
        {
            _fileStorageManager = fileStorageManager;
        }

        public void DeleteImage(string containerName, string fileName)
        {
            _fileStorageManager.DeleteFile(fileName, containerName);
        }
    }
}
