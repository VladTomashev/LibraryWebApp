using Microsoft.AspNetCore.Http;

namespace Library.Application.DTO.Requests
{
    public class UploadBookImageRequest
    {
        public required IFormFile ImageFile { get; set; }
    }
}