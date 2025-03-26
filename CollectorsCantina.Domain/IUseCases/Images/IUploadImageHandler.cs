namespace CollectorsCantina.Domain.IUseCases.Images
{
    public interface IUploadImageHandler
    {
        string UploadImage(Stream stream, string container, string filename, string contentType);
    }
}