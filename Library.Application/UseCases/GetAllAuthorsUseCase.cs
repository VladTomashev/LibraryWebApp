using AutoMapper;
using Library.Application.DTO.Responses;
using Library.Application.Exceptions;
using Library.Application.Interfaces;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;

namespace Library.Application.UseCases
{
    public class GetAllAuthorsUseCase : IGetAllAuthorsUseCase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllAuthorsUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<AuthorResponse>> Execute(PaginationParams paginationParams, 
            CancellationToken cancellationToken = default)
        {
            IEnumerable<Author>? authors = await unitOfWork.AuthorRepository.GetAllAsync(paginationParams, cancellationToken);
            if (!authors.Any())
            {
                throw new NotFoundException("Authors not found");
            }
            else
            {
                IEnumerable<AuthorResponse> response = mapper.Map<IEnumerable<AuthorResponse>>(authors);
                return response;
            }
        }
    }
}
