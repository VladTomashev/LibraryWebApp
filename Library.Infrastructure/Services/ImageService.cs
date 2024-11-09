using Library.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Library.Infrastructure.Services
{
    public class ImageService : IImageService
    {
        private readonly string imageDirectory;

        public ImageService(IConfiguration configuration)
        {
            imageDirectory = configuration["ImageDirectory"];
        }

        public async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                return null;

            var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(imageDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}
