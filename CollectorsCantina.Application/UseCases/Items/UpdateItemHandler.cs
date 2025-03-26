using CollectorsCantina.Domain.Entities;
using CollectorsCantina.Domain.Enums;
using CollectorsCantina.Domain.IRepositories;
using CollectorsCantina.Domain.IStorage;
using CollectorsCantina.Domain.IUseCases.Items;
using CollectorsCantina.Domain.Objects;

namespace CollectorsCantina.Application.UseCases.Items
{
    public class UpdateItemHandler : IUpdateItemHandler
    {
        private readonly IItemRepository _repository;
        private readonly IFileStorageManager _fileStorageManager;

        public UpdateItemHandler(IItemRepository repository, IFileStorageManager fileStorageManager)
        {
            _repository = repository;
            _fileStorageManager = fileStorageManager;
        }

        public async Task<ApplicationResponse<Item>> Update(Item item)
        {
            var response = new ApplicationResponse<Item>();
            response.Data = item;

            var validator = new ItemValidator();
            if (!validator.ValidateItem(item))
            {
                response.Status = ResponseStatus.Fail;
                response.Messages = validator.Errors;
                return response;
            }

            var originalItem = await _repository.Get(item.ItemId.Value);
            if (originalItem.Images != null && originalItem.Images.Any())
            {
                foreach (var image in originalItem.Images)
                {
                    var isImageDeleted = item.Images == null || (item.Images != null && !item.Images.Any(x => x.ImageUrl == image.ImageUrl));
                    if (isImageDeleted)
                    {
                        var imageToDelete = originalItem.Images.First(x => x.ImageUrl == image.ImageUrl);
                        _fileStorageManager.DeleteFile(imageToDelete.ImageUrl, item.ItemId.Value.ToString());
                    }
                }
            }

            response.Data = await _repository.Update(item);
            response.Status = ResponseStatus.Success;
            return response;
        }
    }
}
