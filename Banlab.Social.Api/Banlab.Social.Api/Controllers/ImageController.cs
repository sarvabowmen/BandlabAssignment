using Banlab.Social.Api.Services.Interfaces;
using Banlab.Social.Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Banlab.Social.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;
        public static string[] allowedFileTypes = { "image/jpg", "image/png", "image/bnp", "image/jpeg" };
        public const long maxFileSize = 104_857_600;
        private readonly IImageService _imageService;

        public ImageController(ILogger<ImageController> logger, IImageService imageService)
        {
            _logger = logger;
            _imageService = imageService;
        }

        [HttpPost]
        public async Task<ImageResultViewModel> UploadFile([FromForm] IFormFile image)
        {
            var req = HttpContext.Request;
            if (image == null)
                throw new ArgumentNullException("Image not found");

            if (!allowedFileTypes.Any(fileType => fileType.Equals(image.ContentType, StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentNullException("Image format not supported");

            if (image.Length > maxFileSize)
                throw new ArgumentNullException("Image too large");

            using var stream = image.OpenReadStream();
            var fileName = image.FileName;

            var originalImageUrl = await _imageService.UploadImage(stream, IsOriginalImage: true, fileName);
            stream.Seek(0, SeekOrigin.Begin);

            using var processedImageStream = await _imageService.ResizeImage(stream, width: 600, height: 600, resetStream: true);
            var resizedImageUrl = await _imageService.UploadImage(processedImageStream, IsOriginalImage: false, fileName);

            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new ImageResultViewModel() { OriginalImageBlobUrl = originalImageUrl, ResizedImageBlobUrl = resizedImageUrl };

        }
    }
}
