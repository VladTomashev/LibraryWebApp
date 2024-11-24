using Library.Application.DTO.Requests;
using Library.Application.DTO.Responses;
using Library.Application.Interfaces.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : Controller
    {
        private IAddAuthorUseCase addAuthorUseCase;
        private IDeleteAuthorUseCase deleteAuthorUseCase;
        private IGetAllAuthorsUseCase getAllAuthorsUseCase;
        private IGetAuthorByIdUseCase getAuthorByIdUseCase;
        private IUpdateAuthorUseCase updateAuthorUseCase;

        public AuthorController(IAddAuthorUseCase addAuthorUseCase, IDeleteAuthorUseCase deleteAuthorUseCase,
            IGetAllAuthorsUseCase getAllAuthorsUseCase, IGetAuthorByIdUseCase getAuthorByIdUseCase,
            IUpdateAuthorUseCase updateAuthorUseCase)
        {
            this.addAuthorUseCase = addAuthorUseCase;
            this.deleteAuthorUseCase = deleteAuthorUseCase;
            this.getAllAuthorsUseCase = getAllAuthorsUseCase;
            this.getAuthorByIdUseCase = getAuthorByIdUseCase;
            this.updateAuthorUseCase = updateAuthorUseCase;
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> AddAuthor([FromBody] AddAuthorRequest request,
            CancellationToken cancellationToken)
        {
            await addAuthorUseCase.Execute(request, cancellationToken);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteAuthor([FromQuery] DeleteAuthorRequest request,
            CancellationToken cancellationToken)
        {
            await deleteAuthorUseCase.Execute(request, cancellationToken);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors([FromQuery] GetAllAuthorsRequest request,
            CancellationToken cancellationToken)
        {
            IEnumerable<AuthorResponse> response = await getAllAuthorsUseCase
                .Execute(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById([FromQuery] GetAuthorByIdRequest request,
            CancellationToken cancellationToken)
        {
            AuthorResponse response = await getAuthorByIdUseCase.Execute(request, cancellationToken);
            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateAuthor([FromBody] UpdateAuthorRequest request,
            CancellationToken cancellationToken)
        {
            await updateAuthorUseCase.Execute(request, cancellationToken);
            return Ok();
        }


    }
}
