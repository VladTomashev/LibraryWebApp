using AutoMapper;
using FluentValidation;
using Library.Application.DTO.Requests;
using Library.Application.Interfaces.Services;
using Library.Core.Interfaces;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;
using Library.Application.Exceptions;

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

        public async Task Execute(BookRentalRequest request, CancellationToken cancellationToken = default)
        {
            await validationService.ValidateAsync(validator, request, cancellationToken);

            if (await unitOfWork.BookRepository.GetByIdAsync(request.BookId, cancellationToken) == null)
            {
                throw new NotFoundException("Book not found");
            }

            if (await unitOfWork.UserProfileRepository.GetByIdAsync(request.UserProfileId, cancellationToken) == null)
            {
                throw new NotFoundException("User not found");
            }

            IEnumerable<BookRental> rentals = await unitOfWork.BookRentalRepository.GetAllAsync(null, cancellationToken);
            if (rentals.Any(r => r.BookId == request.BookId && r.IsReturned == false))
            {
                throw new BadRequestException("Book unavailable");
            }

            BookRental bookRental = mapper.Map<BookRental>(request);
            bookRental.Id = Guid.NewGuid();
            await unitOfWork.BookRentalRepository.AddAsync(bookRental, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
