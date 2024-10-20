using Microsoft.AspNetCore.Http;

namespace Library.Application.Interfaces.Services
{
    public interface IImageService
    {
        public Task<string> SaveImageAsync(IFormFile imageFile);
    }
}
