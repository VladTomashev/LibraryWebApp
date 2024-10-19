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
    public class UpdateBookUseCase : IUpdateBookUseCase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IValidator<BookRequest> validator;
        private readonly IValidationService validationService;

        public UpdateBookUseCase(IUnitOfWork unitOfWork, IMapper mapper,
            IValidator<BookRequest> validator, IValidationService validationService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.validator = validator;
            this.validationService = validationService;
        }

        public async Task Execute(Guid id, BookRequest request, CancellationToken cancellationToken = default)
        {
            await validationService.ValidateAsync(validator, request, cancellationToken);

            if (await unitOfWork.BookRepository.GetByIdAsync(id, cancellationToken) == null)
            {
                throw new NotFoundException("Book not found");
            }

            Book book = mapper.Map<Book>(request);
            book.Id = id;
            await unitOfWork.BookRepository.UpdateAsync(book, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
