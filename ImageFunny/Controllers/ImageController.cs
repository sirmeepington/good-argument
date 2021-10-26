using ImageFunny.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageFunny.Controllers
{
    [Route("/")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ImageService _imageService;

        public ImageController(ImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> Image()
        {
            Stream outStream = await _imageService.GenerateImage(HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
            return new FileStreamResult(outStream,"image/jpeg");
        }

    }
}
