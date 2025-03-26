using CollectorsCantina.Domain.IStorage;
using CollectorsCantina.Domain.IUseCases.Images;

namespace CollectorsCantina.Application.UseCases.Images
{
    public class UploadImageHandler : IUploadImageHandler
    {
        private readonly IFileStorageManager _fileStorageManager;

        public UploadImageHandler(IFileStorageManager fileStorageManager)
        {
            _fileStorageManager = fileStorageManager;
        }

        public string UploadImage(Stream stream, string container, string filename, string contentType)
        {
            return _fileStorageManager.UploadFile(
                stream,
                filename,
                contentType,
                container
                );

        }
    }
}
