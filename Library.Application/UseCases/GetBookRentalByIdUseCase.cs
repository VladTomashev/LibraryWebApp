using AutoMapper;
using Library.Application.DTO.Responses;
using Library.Application.Exceptions;
using Library.Core.Interfaces;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;
using Library.Application.DTO.Requests;

namespace Library.Application.UseCases
{
    public class GetBookRentalByIdUseCase : IGetBookRentalByIdUseCase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetBookRentalByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<BookRentalResponse> Execute(GetBookRentalByIdRequest request,
            CancellationToken cancellationToken = default)
        {
            BookRental? bookRental = await unitOfWork.BookRentalRepository
                .GetByIdAsync(request.BookRentalId, cancellationToken);
            if (bookRental == null)
            {
                throw new NotFoundException("Book rental not found");
            }
            else
            {
                BookRentalResponse response = mapper.Map<BookRentalResponse>(bookRental);
                return response;
            }
        }
    }
}
