using Library.Application.DTO.Requests;
using Library.Application.DTO.Responses;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;
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
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetAllBookRentals([FromQuery] GetAllBookRentalsRequest request,
            CancellationToken cancellationToken)
        {
            IEnumerable<BookRentalResponse> response = await getAllBookRentalsUseCase
                .Execute(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetBookRentalById([FromQuery] GetBookRentalByIdRequest request,
            CancellationToken cancellationToken)
        {
            BookRentalResponse response = await getBookRentalByIdUseCase.Execute(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("user/{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetBookRentalsByUserId([FromQuery] GetBookRentalsByUserIdRequest request,
            CancellationToken cancellationToken)
        {
            IEnumerable<BookRentalResponse> response = await getBookRentalsByUserIdUseCase
                .Execute(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("my")]
        [Authorize]
        public async Task<IActionResult> GetMyBookRentals([FromQuery] PaginationParams paginationParams,
            CancellationToken cancellationToken)
        {
            Guid myId = await getIdByJwtUseCase
                .Execute(new GetIdByJwtRequest { Principal = HttpContext.User }, cancellationToken);

            GetBookRentalsByUserIdRequest request = 
                new GetBookRentalsByUserIdRequest { UserId = myId, PaginationParams = paginationParams };

            IEnumerable<BookRentalResponse> response = await getBookRentalsByUserIdUseCase
                .Execute(request, cancellationToken);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> AddBookRental([FromBody]GiveBookRequest request,
            CancellationToken cancellationToken)
        {
            await giveBookUseCase.Execute(request, cancellationToken);
            return Ok();
        }

        [HttpPut("{id}/end")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> EndBookRental([FromQuery] ReturnBookRequest request,
            CancellationToken cancellationToken)
        {
            await returnBookUseCase.Execute(request, cancellationToken);
            return Ok();
        }


    }
}
