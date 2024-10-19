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
    public class BookRentalController : Controller
    {
        private IGetIdByJwtUseCase getIdByJwtUseCase;
        private IGetAllBookRentalsUseCase getAllBookRentalsUseCase;
        private IGetBookRentalByIdUseCase getBookRentalByIdUseCase;
        private IGetBookRentalsByUserIdUseCase getBookRentalsByUserIdUseCase;
        private IGiveBookUseCase giveBookUseCase;
        private IReturnBookUseCase returnBookUseCase;

        public BookRentalController(IGetIdByJwtUseCase getIdByJwtUseCase,
            IGetAllBookRentalsUseCase getAllBookRentalsUseCase,
            IGetBookRentalByIdUseCase getBookRentalByIdUseCase,
            IGetBookRentalsByUserIdUseCase getBookRentalsByUserIdUseCase,
            IGiveBookUseCase giveBookUseCase, IReturnBookUseCase returnBookUseCase)
        {
            this.getIdByJwtUseCase = getIdByJwtUseCase;
            this.getAllBookRentalsUseCase = getAllBookRentalsUseCase;
            this.getBookRentalByIdUseCase = getBookRentalByIdUseCase;
            this.getBookRentalsByUserIdUseCase = getBookRentalsByUserIdUseCase;
            this.giveBookUseCase = giveBookUseCase;
            this.returnBookUseCase = returnBookUseCase;
        }

        [HttpGet]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> GetAllBookRentals([FromQuery] PaginationParams paginationParams,
            CancellationToken cancellationToken)
        {
            IEnumerable<BookRentalResponse> response = await getAllBookRentalsUseCase
                .Execute(paginationParams, cancellationToken);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> GetBookRentalById(Guid id, CancellationToken cancellationToken)
        {
            BookRentalResponse response = await getBookRentalByIdUseCase.Execute(id, cancellationToken);
            return Ok(response);
        }

        [HttpGet("user/{id}")]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> GetBookRentalsByUserId(Guid id, CancellationToken cancellationToken)
        {
            IEnumerable<BookRentalResponse> response = await getBookRentalsByUserIdUseCase.Execute(id, cancellationToken);
            return Ok(response);
        }

        [HttpGet("my")]
        [Authorize]
        public async Task<IActionResult> GetMyBookRentals(CancellationToken cancellationToken)
        {
            Guid myId = await getIdByJwtUseCase.Execute(HttpContext.User, cancellationToken);
            IEnumerable<BookRentalResponse> response = await getBookRentalsByUserIdUseCase.Execute(myId, cancellationToken);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> AddBookRental([FromBody]BookRentalRequest request, CancellationToken cancellationToken)
        {
            await giveBookUseCase.Execute(request, cancellationToken);
            return Ok();
        }

        [HttpPut("{id}/end")]
        [Authorize(Roles = nameof(Role.Admin))]
        public async Task<IActionResult> EndBookRental(Guid id, CancellationToken cancellationToken)
        {
            await returnBookUseCase.Execute(id, cancellationToken);
            return Ok();
        }


    }
}
