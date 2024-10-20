using AutoMapper;
using Library.Application.DTO.Responses;
using Library.Application.Exceptions;
using Library.Application.Interfaces;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;

namespace Library.Application.UseCases
{
    public class GetBookByIdUseCase : IGetBookByIdUseCase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetBookByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<BookResponse> Execute(Guid id, CancellationToken cancellationToken = default)
        {
            Book? book = await unitOfWork.BookRepository.GetByIdAsync(id, cancellationToken);
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
