using Library.Application.DTO.Requests;
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

        public async Task<string> UploadAsync(UploadBookImageRequest request)
        {
            if (request.ImageFile == null || request.ImageFile.Length == 0)
            {
                throw new ArgumentException("The provided image file is invalid.");
            }

            var imagePath = await _imageService.SaveImageAsync(request.ImageFile);
            if (string.IsNullOrEmpty(imagePath))
            {
                throw new InvalidOperationException("Failed to upload image.");
            }

            return imagePath;
        }

    }
}
