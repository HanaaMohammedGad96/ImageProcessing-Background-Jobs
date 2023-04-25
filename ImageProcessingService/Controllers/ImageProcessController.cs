using ImageProcessingService.Core.Models.Image;
using ImageProcessingService.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageProcessingService.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class ImageProcessController : ControllerBase
    {
        /*
         * TODO:
         *   upload image
         *   get images
         *   get imageById
         *   download image
         */
        private readonly IImageRepository _imageRepository;

        public ImageProcessController(IImageRepository imageRepository)
        =>_imageRepository = imageRepository;

        [HttpGet]
        public async Task<ActionResult<ImageDetailsVm>> GetById(Guid id, CancellationToken ct)
        {
            try
            {
                return Ok(await _imageRepository.GetById(id, ct));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet]
        public async Task<ActionResult<List<ImageVm>>> GetAll(CancellationToken ct)
        {
            try
            {
                await _imageRepository.RemoveNonWebpImages();
                return Ok(await _imageRepository.GetAllImages(ct));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ImageDto model, CancellationToken ct)
        {
            if (model is null)
                return NotFound("There is no entered data ");
            try
            {
                await _imageRepository.UploadImage(model, ct);
                return Ok("image upload successfully..");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
