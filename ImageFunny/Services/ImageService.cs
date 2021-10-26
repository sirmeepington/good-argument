using Microsoft.AspNetCore.Hosting;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageFunny.Services
{
    public class ImageService
    {
        private readonly IWebHostEnvironment _webHostEnv;

        public ImageService(IWebHostEnvironment webHostEnv)
        {
            _webHostEnv = webHostEnv;
        }

        public async Task<Stream> GenerateImage(string ipAddress)
        {
            using Image image = Image.Load(Path.Combine(_webHostEnv.WebRootPath, "base.png"));

            FontCollection coll = new FontCollection();
            FontFamily fam = coll.Install(Path.Combine(_webHostEnv.WebRootPath, "impact.ttf"));

            TextOptions options = new TextOptions()
            {
                ApplyKerning = true,
                HorizontalAlignment = HorizontalAlignment.Center // right align
            };

            Font draw = fam.CreateFont(34);
            image.Mutate(x => {
                x.SetTextOptions(options);
                x.DrawText("That's a good \nargument", draw, Color.White, new PointF(image.Width / 2, 0));
                x.DrawText($"but unfortunately \n{ipAddress}", draw, Color.White, new PointF(image.Width / 2, image.Height-(image.Height/3)));
            });

            Stream stream = new MemoryStream();
            await image.SaveAsJpegAsync(stream, new JpegEncoder() { Quality = 10 });
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

    }
}
