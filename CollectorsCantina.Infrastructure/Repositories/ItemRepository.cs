using CollectorsCantina.Domain.Entities;
using CollectorsCantina.Domain.IRepositories;
using CollectorsCantina.Infrastructure.Configuration;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace CollectorsCantina.Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly string _connectionString;

        private const string DATABASE = "collectors-cantina-db";
        private const string CONTAINER = "Items";

        public ItemRepository(IOptions<InfrastructureOptions> options)
        {
            _connectionString = options.Value.CosmosConnectionString;
        }

        public async Task<Item> Get(Guid id)
        {
            Item item = null;

            using (var cosmos = new CosmosClient(_connectionString))
            {
                var container = cosmos.GetContainer(DATABASE, CONTAINER);

                using (var iterator = container.GetItemQueryIterator<Item>($"SELECT * FROM c WHERE c.id = '{id.ToString()}'"))
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

        public async Task<List<Item>> GetByCollection(Guid id)
        {
            var items = new List<Item>();

            using (var cosmos = new CosmosClient(_connectionString))
            {
                var container = cosmos.GetContainer(DATABASE, CONTAINER);

                using (var iterator = container.GetItemQueryIterator<Item>($"SELECT * FROM c WHERE c.collectionId = '{id.ToString()}'"))
                {
                    while (iterator.HasMoreResults)
                    {
                        var result = await iterator.ReadNextAsync().ConfigureAwait(false); ;
                        items.AddRange(result.ToList());
                    }
                }
            }

            return items;
        }

        public async Task<List<Item>> GetAll()
        {
            var items = new List<Item>();

            using (var cosmos = new CosmosClient(_connectionString))
            {
                var container = cosmos.GetContainer(DATABASE, CONTAINER);

                using (var iterator = container.GetItemQueryIterator<Item>($"SELECT * FROM c"))
                {
                    while (iterator.HasMoreResults)
                    {
                        var result = await iterator.ReadNextAsync().ConfigureAwait(false); ;
                        items.AddRange(result.ToList());
                    }
                }
            }

            return items;
        }

        public async Task<Item> Insert(Item item)
        {
            using (var cosmos = new CosmosClient(_connectionString))
            {
                var container = cosmos.GetContainer(DATABASE, CONTAINER);

                item.ItemId = Guid.NewGuid();
                return await container.CreateItemAsync(item);
            }
        }

        public async Task<Item> Update(Item item)
        {
            using (var cosmos = new CosmosClient(_connectionString))
            {
                var container = cosmos.GetContainer(DATABASE, CONTAINER);
                return await container.ReplaceItemAsync(item, item.ItemId.ToString(), new PartitionKey(item.CollectionId.ToString()));
            }
        }

        public async Task Delete(Guid itemId, Guid collectionId)
        {
            using (var cosmos = new CosmosClient(_connectionString))
            {
                var container = cosmos.GetContainer(DATABASE, CONTAINER);
                await container.DeleteItemAsync<Item>(itemId.ToString(), new PartitionKey(collectionId.ToString()));
            }
        }
    }
}
