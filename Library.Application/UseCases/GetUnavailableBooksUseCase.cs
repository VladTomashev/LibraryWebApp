using AutoMapper;
using Library.Application.DTO.Responses;
using Library.Application.Exceptions;
using Library.Core.Interfaces;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;
using Library.Application.DTO.Requests;

namespace Library.Application.UseCases
{
    public class GetUnavailableBooksUseCase : IGetUnavailableBooksUseCase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetUnavailableBooksUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<BookResponse>> Execute(GetUnavailableBooksRequest request, 
            CancellationToken cancellationToken = default)
        {
            IEnumerable<Book>? books = await unitOfWork.BookRepository
                .GetUnavailableBooksAsync(request.PaginationParams, cancellationToken);
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
