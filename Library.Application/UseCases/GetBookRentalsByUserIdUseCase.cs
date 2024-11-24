using AutoMapper;
using Library.Application.DTO.Responses;
using Library.Application.Exceptions;
using Library.Core.Interfaces;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;
using Library.Application.DTO.Requests;

namespace Library.Application.UseCases
{
    public class GetBookRentalsByUserIdUseCase : IGetBookRentalsByUserIdUseCase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetBookRentalsByUserIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<BookRentalResponse>> Execute(GetBookRentalsByUserIdRequest request,
            CancellationToken cancellationToken = default)
        {
            IEnumerable<BookRental>? bookRentals = await unitOfWork.BookRentalRepository
                .GetByUserIdAsync(request.UserId);
            if (!bookRentals.Any())
            {
                throw new NotFoundException("Book rentals not found");
            }
            else
            {
                IEnumerable<BookRentalResponse> response = mapper.Map<IEnumerable<BookRentalResponse>>(bookRentals);
                return response;
            }
        }
    }
}
