using Library.Application.Interfaces.Services;
using Library.Application.Interfaces.UseCases;
using Microsoft.AspNetCore.Http;

namespace Library.Application.UseCases
{
    public class UploadBookImageUseCase : IUploadBookImageUseCase
    {
        private readonly IImageService _imageService;

        public UploadBookImageUseCase(IImageService imageService)
        {
            _imageService = imageService;
        }

        public async Task<string> UploadAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("The provided image file is invalid.");
            }

            var imagePath = await _imageService.SaveImageAsync(imageFile);
            if (string.IsNullOrEmpty(imagePath))
            {
                throw new InvalidOperationException("Failed to upload image.");
            }

            return imagePath; 
        }
    }
}
