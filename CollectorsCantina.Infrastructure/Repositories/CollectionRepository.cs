using CollectorsCantina.Domain.Entities;
using CollectorsCantina.Domain.IRepositories;
using CollectorsCantina.Infrastructure.Configuration;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace CollectorsCantina.Infrastructure.Repositories
{
    public class CollectionRepository : ICollectionRepository
    {
        private readonly string _connectionString;

        private const string DATABASE = "collectors-cantina-db";
        private const string CONTAINER = "Collections";

        public CollectionRepository(IOptions<InfrastructureOptions> options)
        {
            _connectionString = options.Value.CosmosConnectionString;
        }

        public async Task<List<Collection>> GetAll()
        {
            var items = new List<Collection>();

            using (var cosmos = new CosmosClient(_connectionString))
            {
                var container = cosmos.GetContainer(DATABASE, CONTAINER);
                using (var iterator = container.GetItemQueryIterator<Collection>())
                {
                    while (iterator.HasMoreResults)
                    {
                        var response = await iterator.ReadNextAsync().ConfigureAwait(false);
                        items.AddRange(response.ToList());
                    }
                }
            }

            return items;
        }


        public async Task<Collection> Get(Guid id)
        {
            Collection item = null;

            using (var cosmos = new CosmosClient(_connectionString))
            {
                var container = cosmos.GetContainer(DATABASE, CONTAINER);

                using (var iterator = container.GetItemQueryIterator<Collection>($"SELECT * FROM c WHERE c.id = '{id.ToString()}'"))
                {
                    while (iterator.HasMoreResults)
                    {
                        var result = await iterator.ReadNextAsync();
                        item = result.FirstOrDefault();
                    }
                }
            }

            return item;
        }


        public async Task<Collection> Insert(Collection collection)
        {
            using (var cosmos = new CosmosClient(_connectionString))
            {
                var container = cosmos.GetContainer(DATABASE, CONTAINER);

                collection.CollectionId = Guid.NewGuid();
                return await container.CreateItemAsync(collection);
            }
        }

        public async Task<Collection> Update(Collection collection)
        {
            using (var cosmos = new CosmosClient(_connectionString))
            {
                var container = cosmos.GetContainer(DATABASE, CONTAINER);
                return await container.ReplaceItemAsync(collection, collection.CollectionId.ToString(), new PartitionKey(collection.CollectionId.ToString()));
            }
        }

        public async Task Delete(Guid collectionId)
        {
            using (var cosmos = new CosmosClient(_connectionString))
            {
                var container = cosmos.GetContainer(DATABASE, CONTAINER);
                await container.DeleteItemAsync<Collection>(collectionId.ToString(), new PartitionKey(collectionId.ToString()));
            }
        }
    }
}
