using Microsoft.AspNetCore.Http;

namespace Library.Infrastructure.Interfaces
{
    public interface IImageService
    {
        public Task<string> SaveImageAsync(IFormFile imageFile);
    }
}
