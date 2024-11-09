using AutoMapper;
using Library.Application.DTO.Responses;
using Library.Application.Exceptions;
using Library.Core.Interfaces;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;

namespace Library.Application.UseCases
{
    public class GetAllBookRentalsUseCase : IGetAllBookRentalsUseCase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllBookRentalsUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<BookRentalResponse>> Execute(PaginationParams paginationParams, 
            CancellationToken cancellationToken = default)
        {
            IEnumerable<BookRental>? bookRentals = await unitOfWork.BookRentalRepository.GetAllAsync(paginationParams, cancellationToken);
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
