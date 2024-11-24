using AutoMapper;
using Library.Application.DTO.Responses;
using Library.Application.Exceptions;
using Library.Core.Interfaces;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;
using Library.Application.DTO.Requests;

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

        public async Task<IEnumerable<AuthorResponse>> Execute(GetAllAuthorsRequest request, 
            CancellationToken cancellationToken = default)
        {
            IEnumerable<Author>? authors = await unitOfWork.AuthorRepository
                .GetAllAsync(request.PaginationParams, cancellationToken);

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
