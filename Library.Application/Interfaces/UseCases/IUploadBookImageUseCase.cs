using Library.Application.DTO.Requests;
namespace Library.Application.Interfaces.UseCases
{
    public interface IUploadBookImageUseCase
    {
        public Task<string> UploadAsync(UploadBookImageRequest request);
    }
}
