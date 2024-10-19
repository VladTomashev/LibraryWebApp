using AutoMapper;
using FluentValidation;
using Library.Application.DTO.Requests;
using Library.Application.Interfaces.Services;
using Library.Application.Interfaces;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;
using Library.Application.Exceptions;

namespace Library.Application.UseCases
{
    public class UpdateAuthorUseCase : IUpdateAuthorUseCase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IValidator<AuthorRequest> validator;
        private readonly IValidationService validationService;

        public UpdateAuthorUseCase(IUnitOfWork unitOfWork, IMapper mapper,
            IValidator<AuthorRequest> validator, IValidationService validationService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.validator = validator;
            this.validationService = validationService;
        }
        public async Task Execute(Guid id, AuthorRequest request, CancellationToken cancellationToken = default)
        {
            await validationService.ValidateAsync(validator, request, cancellationToken);

            if (await unitOfWork.AuthorRepository.GetByIdAsync(id, cancellationToken) == null)
            {
                throw new NotFoundException("Author not found");
            }

            Author author = mapper.Map<Author>(request);
            author.Id = id;
            await unitOfWork.AuthorRepository.UpdateAsync(author, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
