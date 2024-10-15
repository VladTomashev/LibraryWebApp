using AutoMapper;
using FluentValidation;
using Library.Application.DTO.Requests;
using Library.Application.Interfaces.Services;
using Library.Application.Interfaces;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;

namespace Library.Application.UseCases
{
    public class GiveBookUseCase : IGiveBookUseCase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IValidator<BookRentalRequest> validator;
        private readonly IValidationService validationService;

        public GiveBookUseCase(IUnitOfWork unitOfWork, IMapper mapper,
            IValidator<BookRentalRequest> validator, IValidationService validationService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.validator = validator;
            this.validationService = validationService;
        }

        public async void Execute(BookRentalRequest request, CancellationToken cancellationToken = default)
        {
            await validationService.ValidateAsync(validator, request, cancellationToken);
            BookRental bookRental = mapper.Map<BookRental>(request);
            bookRental.Id = Guid.NewGuid();
            await unitOfWork.BookRentalRepository.AddAsync(bookRental, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
