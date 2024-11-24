using AutoMapper;
using Library.Application.DTO.Responses;
using Library.Application.Exceptions;
using Library.Core.Interfaces;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;
using Library.Application.DTO.Requests;

namespace Library.Application.UseCases
{
    public class GetBookByIsbnUseCase : IGetBookByIsbnUseCase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetBookByIsbnUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<BookResponse> Execute(GetBookByIsbnRequest request,
            CancellationToken cancellationToken = default)
        {
            Book? book = await unitOfWork.BookRepository.GetByIsbnAsync(request.Isbn, cancellationToken);
            if (book == null)
            {
                throw new NotFoundException("Book not found");
            }
            else
            {
                BookResponse response = mapper.Map<BookResponse>(book);
                return response;
            }
        }
    }
}
