using Library.Application.DTO.Requests;
using Library.Application.DTO.Responses;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;
using Library.Core.Enums;
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

        public BookController(IAddBookUseCase addBookUseCase, IDeleteBookUseCase deleteBookUseCase,
            IGetAllBooksUseCase getAllBooksUseCase, IGetAvailableBooksUseCase getAvailableBooksUseCase,
            IGetUnavailableBooksUseCase getUnavailableBooksUseCase, IGetBookByIdUseCase getBookByIdUseCase,
            IGetBookByIsbnUseCase getBookByIsbnUseCase, IGetBooksByAuthorIdUseCase getBooksByAuthorIdUseCase,
            IUpdateBookUseCase updateBookUseCase)
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
        }

        [HttpPost]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> AddBook([FromBody] BookRequest request, CancellationToken cancellationToken)
        {
            await addBookUseCase.Execute(request, cancellationToken);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> DeleteBook(Guid id, CancellationToken cancellationToken)
        {
            await deleteBookUseCase.Execute(id, cancellationToken);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks([FromQuery] PaginationParams paginationParams, 
            CancellationToken cancellationToken)
        {
            IEnumerable<BookResponse> response = await getAllBooksUseCase
                .Execute(paginationParams, cancellationToken);
            return Ok(response);
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableBooks([FromQuery] PaginationParams paginationParams, 
            CancellationToken cancellationToken)
        {
            IEnumerable<BookResponse> response = await getAvailableBooksUseCase
                .Execute(paginationParams, cancellationToken);
            return Ok(response);
        }

        [HttpGet("unavailable")]
        public async Task<IActionResult> GetUnavailableBooks([FromQuery] PaginationParams paginationParams, 
            CancellationToken cancellationToken)
        {
            IEnumerable<BookResponse> response = await getUnavailableBooksUseCase
                .Execute(paginationParams, cancellationToken);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(Guid id, CancellationToken cancellationToken)
        {
            BookResponse response = await getBookByIdUseCase.Execute(id, cancellationToken);
            return Ok(response);
        }

        [HttpGet("isbn/{isbn}")]
        public async Task<IActionResult> GetBookByIsbn(string isbn, CancellationToken cancellationToken)
        {
            BookResponse response = await getBookByIsbnUseCase.Execute(isbn, cancellationToken);
            return Ok(response);
        }

        [HttpGet("author/{id}")]
        public async Task<IActionResult> GetBooksByAuthorId(Guid id, [FromQuery] PaginationParams paginationParams,
            CancellationToken cancellationToken)
        {
            IEnumerable<BookResponse> response = await getBooksByAuthorIdUseCase
                .Execute(id, paginationParams, cancellationToken);
            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] BookRequest request, CancellationToken cancellationToken)
        {
            await updateBookUseCase.Execute(id, request, cancellationToken);
            return Ok();
        }
    }
}
