using AutoMapper;
using Library.Application.DTO.Responses;
using Library.Application.Exceptions;
using Library.Core.Interfaces;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;

namespace Library.Application.UseCases
{
    public class GetAuthorByIdUseCase : IGetAuthorByIdUseCase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAuthorByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<AuthorResponse> Execute(Guid id, CancellationToken cancellationToken = default)
        {
            Author? author = await unitOfWork.AuthorRepository.GetByIdAsync(id, cancellationToken);
            if (author == null)
            {
                throw new NotFoundException("Author not found");
            }
            else
            {
                AuthorResponse response = mapper.Map<AuthorResponse>(author);
                return response;
            }
        }
    }
}
