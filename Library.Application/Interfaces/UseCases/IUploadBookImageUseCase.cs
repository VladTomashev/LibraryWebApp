using Microsoft.AspNetCore.Http;

namespace Library.Application.Interfaces.UseCases
{
    public interface IUploadBookImageUseCase
    {
        public Task<string> UploadAsync(IFormFile imageFile);
    }
}
