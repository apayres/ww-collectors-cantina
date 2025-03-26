using CollectorsCantina.Domain.Entities;
using CollectorsCantina.Domain.Enums;
using CollectorsCantina.Domain.IUseCases.Collections;
using CollectorsCantina.Domain.IUseCases.Images;
using Microsoft.AspNetCore.Mvc;

namespace CollectorsCantina.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CollectionController : ControllerBase
    {
        private readonly ILogger<CollectionController> _logger;
        private readonly ICreateCollectionHandler _createCollectionHandler;
        private readonly IUpdateCollectionHandler _updateCollectionHandler;
        private readonly IDeleteCollectionHandler _deleteCollectionHandler;
        private readonly IGetCollectionHandler _getCollectionHandler;
        private readonly IUploadImageHandler _uploadImageHandler;

        public CollectionController(ILogger<CollectionController> logger, 
            ICreateCollectionHandler createCollectionHandler, 
            IUpdateCollectionHandler updateCollectionHandler, 
            IDeleteCollectionHandler deleteCollectionHandler, 
            IGetCollectionHandler getCollectionHandler,
            IUploadImageHandler uploadImageHandler)
        {
            _logger = logger;
            _createCollectionHandler = createCollectionHandler;
            _updateCollectionHandler = updateCollectionHandler;
            _deleteCollectionHandler = deleteCollectionHandler;
            _getCollectionHandler = getCollectionHandler;
            _uploadImageHandler = uploadImageHandler;
        }

        [HttpGet("/collection/{id}")]
        public async Task<ActionResult<IEnumerable<Collection>>> Get(Guid id)
        {
            var result = await _getCollectionHandler.Get(id);
            return Ok(result.Data);
        }

        [HttpGet("/Collections")]
        public async Task<ActionResult<IEnumerable<Collection>>> GetAll()
        {
            var result = await _getCollectionHandler.GetAll();
            return Ok(result.Data);
        }

        [HttpPost("/collection")]
        public async Task<ActionResult<Collection>> Post(Collection collection)
        {
            var result = await _createCollectionHandler.Create(collection);
            if(result.Status == ResponseStatus.Fail)
            {
                return BadRequest(result.Messages);
            }

            return Ok(result.Data);
        }

        [HttpPut]
        public async Task<ActionResult<Collection>> Put(Collection collection)
        {
            var result = await _updateCollectionHandler.Update(collection);
            if (result.Status == ResponseStatus.Fail)
            {
                return BadRequest(result.Messages);
            }

            return Ok(result.Data);
        }

        [HttpDelete("/collection/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _deleteCollectionHandler.Delete(id);
            if (result.Status == ResponseStatus.Fail)
            {
                return BadRequest(result.Messages);
            }

            return Ok(result);
        }

        [HttpPost("/collection/uploadimage")]
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
