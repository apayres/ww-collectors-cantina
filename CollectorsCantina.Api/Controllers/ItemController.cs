using CollectorsCantina.Domain.Entities;
using CollectorsCantina.Domain.Enums;
using CollectorsCantina.Domain.IUseCases.Images;
using CollectorsCantina.Domain.IUseCases.Items;
using Microsoft.AspNetCore.Mvc;

namespace CollectorsCantina.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly ICreateItemHandler _createItemHandler;
        private readonly IUpdateItemHandler _updateItemHandler;
        private readonly IDeleteItemHandler _deleteItemHandler;
        private readonly IGetItemHandler _getItemHandler;
        private readonly IUploadImageHandler _uploadImageHandler;

        public ItemController(ILogger<ItemController> logger, 
            ICreateItemHandler createItemHandler, 
            IUpdateItemHandler updateItemHandler, 
            IDeleteItemHandler deleteItemHandler, 
            IGetItemHandler getItemHandler,
            IUploadImageHandler uploadImageHandler)
        {
            _logger = logger;
            _createItemHandler = createItemHandler;
            _updateItemHandler = updateItemHandler;
            _deleteItemHandler = deleteItemHandler;
            _getItemHandler = getItemHandler;
            _uploadImageHandler = uploadImageHandler;
        }

        [HttpGet("/item/{id}")]
        public async Task<ActionResult<Item>> Get(Guid id)
        {
            var result = await _getItemHandler.Get(id);
            return Ok(result.Data);
        }

        [HttpGet("/item/ByCollection/{id}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetByCollection(Guid id)
        {
            var result = await _getItemHandler.GetByCollection(id);
            return Ok(result.Data);
        }

        [HttpGet("/item/search/{term}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetByTag(string term)
        {
            var result = await _getItemHandler.GetByTerm(term);
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<ActionResult<Item>> Post(Item item)
        {
            var result = await _createItemHandler.Create(item);
            if (result.Status == ResponseStatus.Fail)
            {
                return BadRequest(result.Messages);
            }

            return Ok(result.Data);
        }

        [HttpPut]
        public async Task<ActionResult<Item>> Put(Item item)
        {
            var result = await _updateItemHandler.Update(item);
            if (result.Status == ResponseStatus.Fail)
            {
                return BadRequest(result.Messages);
            }

            return Ok(result.Data);
        }

        [HttpDelete("/item/{collectionId}/{id}")]
        public async Task<ActionResult> Delete(Guid id, Guid collectionId)
        {
            var result = await _deleteItemHandler.Delete(id, collectionId);
            if (result.Status == ResponseStatus.Fail)
            {
                return BadRequest(result.Messages);
            }

            return Ok(result);
        }

        [HttpPost("/item/uploadimage")]
        public ActionResult<string> UploadImage(IFormFile file, string name, string container)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Name must exist.");
            }

            if (string.IsNullOrEmpty(container))
            {
                return BadRequest("container name must exist.");
            }

            var ext = file.FileName.Substring(file.FileName.LastIndexOf(".") + 1);

            var uri = _uploadImageHandler.UploadImage(file.OpenReadStream(), container, name, "image/" + ext);

            return uri;
        }
    }
}
