using AutoMapper;
using Library.Application.DTO.Responses;
using Library.Application.Exceptions;
using Library.Core.Interfaces;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;
using Library.Application.DTO.Requests;

namespace Library.Application.UseCases
{
    public class GetAvailableBooksUseCase : IGetAvailableBooksUseCase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAvailableBooksUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<BookResponse>> Execute(GetAvailableBooksRequest request, 
            CancellationToken cancellationToken = default)
        {
            IEnumerable<Book>? books = await unitOfWork.BookRepository
                .GetAvailableBooksAsync(request.PaginationParams, cancellationToken);
            if (!books.Any())
            {
                throw new NotFoundException("Books not found");
            }
            else
            {
                IEnumerable<BookResponse> response = mapper.Map<IEnumerable<BookResponse>>(books);
                return response;
            }
        }
    }
}
