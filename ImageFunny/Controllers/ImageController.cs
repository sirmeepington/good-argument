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
            string ip = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            if (HttpContext.Request.Headers.TryGetValue("X-Forwarded-For", out var val)){
                ip = val.Last();
            }
            Stream outStream = await _imageService.GenerateImage(ip);
            return new FileStreamResult(outStream,"image/jpeg");
        }

    }
}
