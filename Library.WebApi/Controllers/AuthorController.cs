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
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorRequest request,
            CancellationToken cancellationToken)
        {
            await addAuthorUseCase.Execute(request, cancellationToken);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> DeleteAuthor(Guid id,
            CancellationToken cancellationToken)
        {
            await deleteAuthorUseCase.Execute(id, cancellationToken);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors([FromQuery] PaginationParams paginationParams,
            CancellationToken cancellationToken)
        {
            IEnumerable<AuthorResponse> response = await getAllAuthorsUseCase
                .Execute(paginationParams, cancellationToken);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(Guid id, CancellationToken cancellationToken)
        {
            AuthorResponse response = await getAuthorByIdUseCase.Execute(id, cancellationToken);
            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> UpdateAuthor(Guid id, [FromBody]AuthorRequest request,
            CancellationToken cancellationToken)
        {
            await updateAuthorUseCase.Execute(id, request, cancellationToken);
            return Ok();
        }


    }
}
