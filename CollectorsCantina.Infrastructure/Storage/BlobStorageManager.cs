using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CollectorsCantina.Domain.IStorage;
using CollectorsCantina.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace CollectorsCantina.Infrastructure.Storage
{
    public class BlobStorageManager : IFileStorageManager
    {
        private readonly string _connection;

        public BlobStorageManager(IOptions<InfrastructureOptions> config)
        {
            _connection = config.Value.BlobStorageConnectionString;
        }

        public string UploadFile(Stream stream, string filename, string contentType, string containerName)
        {
            var container = GetOrCreateContainer(containerName);
            var blobClient = container.GetBlobClient(filename);
            if (blobClient.Exists())
            {
                blobClient.Delete();
            }

            blobClient.Upload(stream);
            blobClient.SetHttpHeaders(new BlobHttpHeaders()
            {
                ContentType = contentType
            });

            return blobClient.Uri.AbsoluteUri;
        }

        public void DeleteFile(string fileName, string containerName)
        {
            var container = GetOrCreateContainer(containerName);
            var file = fileName.Substring(fileName.LastIndexOf('/') + 1);

            var blobClient = container.GetBlobClient(file);
            if (blobClient.Exists())
            {
                blobClient.Delete();
            }
        }

        public void DeleteContainer(string containerName)
        {
            var containerClient = new BlobContainerClient(_connection, containerName);
            if (!containerClient.Exists())
            {
                return;
            }

            containerClient.Delete();
        }

        public List<string> ListFiles(string container)
        {
            var files = new List<string>();
            var containerClient = new BlobContainerClient(_connection, container);
            if (!containerClient.Exists())
            {
                return new List<string>();
            }

            var blobs = containerClient.GetBlobs(BlobTraits.All, BlobStates.None);
            foreach (var blob in blobs)
            {
                var blobClient = containerClient.GetBlobClient(blob.Name);
                files.Add(blobClient.Uri.AbsoluteUri);
            }

            return files;
        }

        private BlobContainerClient GetOrCreateContainer(string containerName)
        {
            var storageClient = new BlobServiceClient(_connection);
            var containerClient = storageClient.GetBlobContainerClient(containerName);
            containerClient.CreateIfNotExists(PublicAccessType.BlobContainer);
            return containerClient;
        }
    }
}
