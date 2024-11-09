using AutoMapper;
using Library.Application.DTO.Responses;
using Library.Application.Exceptions;
using Library.Core.Interfaces;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;

namespace Library.Application.UseCases
{
    public class GetAllBooksUseCase : IGetAllBooksUseCase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllBooksUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<BookResponse>> Execute(PaginationParams paginationParams, 
            CancellationToken cancellationToken = default)
        {
            IEnumerable<Book>? books = await unitOfWork.BookRepository.GetAllAsync(paginationParams, cancellationToken);
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
