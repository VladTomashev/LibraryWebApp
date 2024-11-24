using Library.Application.DTO.Requests;
using Library.Application.DTO.Responses;
using Library.Application.Interfaces.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {
        private IAddBookUseCase addBookUseCase;
        private IDeleteBookUseCase deleteBookUseCase;
        private IGetAllBooksUseCase getAllBooksUseCase;
        private IGetAvailableBooksUseCase getAvailableBooksUseCase;
        private IGetUnavailableBooksUseCase getUnavailableBooksUseCase;
        private IGetBookByIdUseCase getBookByIdUseCase;
        private IGetBookByIsbnUseCase getBookByIsbnUseCase;
        private IGetBooksByAuthorIdUseCase getBooksByAuthorIdUseCase;
        private IUpdateBookUseCase updateBookUseCase;
        private IUploadBookImageUseCase uploadBookImageUseCase;

        public BookController(IAddBookUseCase addBookUseCase, IDeleteBookUseCase deleteBookUseCase,
            IGetAllBooksUseCase getAllBooksUseCase, IGetAvailableBooksUseCase getAvailableBooksUseCase,
            IGetUnavailableBooksUseCase getUnavailableBooksUseCase, IGetBookByIdUseCase getBookByIdUseCase,
            IGetBookByIsbnUseCase getBookByIsbnUseCase, IGetBooksByAuthorIdUseCase getBooksByAuthorIdUseCase,
            IUpdateBookUseCase updateBookUseCase, IUploadBookImageUseCase uploadBookImageUseCase)
        {
            this.addBookUseCase = addBookUseCase;
            this.deleteBookUseCase = deleteBookUseCase;
            this.getAllBooksUseCase = getAllBooksUseCase;
            this.getAvailableBooksUseCase = getAvailableBooksUseCase;
            this.getUnavailableBooksUseCase = getUnavailableBooksUseCase;
            this.getBookByIdUseCase = getBookByIdUseCase;
            this.getBookByIsbnUseCase = getBookByIsbnUseCase;
            this.getBooksByAuthorIdUseCase = getBooksByAuthorIdUseCase;
            this.updateBookUseCase = updateBookUseCase;
            this.uploadBookImageUseCase = uploadBookImageUseCase;
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> AddBook([FromBody] AddBookRequest request, CancellationToken cancellationToken)
        {
            await addBookUseCase.Execute(request, cancellationToken);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteBook([FromQuery] DeleteBookRequest request, CancellationToken cancellationToken)
        {
            await deleteBookUseCase.Execute(request, cancellationToken);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks([FromQuery] GetAllBooksRequest request, 
            CancellationToken cancellationToken)
        {
            IEnumerable<BookResponse> response = await getAllBooksUseCase
                .Execute(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableBooks([FromQuery] GetAvailableBooksRequest request, 
            CancellationToken cancellationToken)
        {
            IEnumerable<BookResponse> response = await getAvailableBooksUseCase
                .Execute(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("unavailable")]
        public async Task<IActionResult> GetUnavailableBooks([FromQuery] GetUnavailableBooksRequest request, 
            CancellationToken cancellationToken)
        {
            IEnumerable<BookResponse> response = await getUnavailableBooksUseCase
                .Execute(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById([FromQuery] GetBookByIdRequest request,
            CancellationToken cancellationToken)
        {
            BookResponse response = await getBookByIdUseCase.Execute(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("isbn/{isbn}")]
        public async Task<IActionResult> GetBookByIsbn([FromQuery] GetBookByIsbnRequest request,
            CancellationToken cancellationToken)
        {
            BookResponse response = await getBookByIsbnUseCase.Execute(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("author/{id}")]
        public async Task<IActionResult> GetBooksByAuthorId([FromQuery] GetBooksByAuthorIdRequest request,
            CancellationToken cancellationToken)
        {
            IEnumerable<BookResponse> response = await getBooksByAuthorIdUseCase
                .Execute(request, cancellationToken);
            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookRequest request,
            CancellationToken cancellationToken)
        {
            await updateBookUseCase.Execute(request, cancellationToken);
            return Ok();
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage([FromForm] UploadBookImageRequest request)
        {
            string imagePath = await uploadBookImageUseCase.UploadAsync(request);
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            return Ok(new { ImageUrl = $"{baseUrl}/images/books/{imagePath}" });

        }

    }
}
