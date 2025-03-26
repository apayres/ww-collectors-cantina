namespace CollectorsCantina.Domain.IUseCases.Images
{
    public interface IDeleteImageHandler
    {
        void DeleteImage(string containerName, string fileName);
    }
}