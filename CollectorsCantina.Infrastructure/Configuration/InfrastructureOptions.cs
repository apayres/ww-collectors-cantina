using Microsoft.Extensions.Options;

namespace CollectorsCantina.Infrastructure.Configuration
{
    public class InfrastructureOptions : IOptions<InfrastructureOptions>
    {
        public string CosmosConnectionString { get; set; }

        public string BlobStorageConnectionString { set; get; }

        public InfrastructureOptions Value => this;
    }
}