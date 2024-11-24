using AutoMapper;
using FluentValidation;
using Library.Application.DTO.Requests;
using Library.Core.Interfaces;
using Library.Application.Interfaces.Services;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;
using Library.Application.DTO.Basics;


namespace Library.Application.UseCases
{
    public class AddAuthorUseCase : IAddAuthorUseCase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IValidator<AddAuthorRequest> validator;
        private readonly IValidationService validationService;

        public AddAuthorUseCase(IUnitOfWork unitOfWork, IMapper mapper,
            IValidator<AddAuthorRequest> validator, IValidationService validationService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.validator = validator;
            this.validationService = validationService;
        }
        public async Task Execute(AddAuthorRequest request, CancellationToken cancellationToken = default)
        {
            await validationService.ValidateAsync(validator, request, cancellationToken);
            Author author = mapper.Map<Author>(request.AuthorDto);
            author.Id = Guid.NewGuid();
            await unitOfWork.AuthorRepository.AddAsync(author, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
