using AutoMapper;
using Library.Application.DTO.Responses;
using Library.Application.Exceptions;
using Library.Application.Interfaces;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;

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

        public async Task<IEnumerable<BookRentalResponse>> Execute(Guid id, CancellationToken cancellationToken = default)
        {
            IEnumerable<BookRental>? bookRentals = await unitOfWork.BookRentalRepository.GetByUserIdAsync(id);
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
