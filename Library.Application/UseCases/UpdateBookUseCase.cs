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
    public class UpdateBookUseCase : IUpdateBookUseCase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IValidator<UpdateBookRequest> validator;
        private readonly IValidationService validationService;

        public UpdateBookUseCase(IUnitOfWork unitOfWork, IMapper mapper,
            IValidator<UpdateBookRequest> validator, IValidationService validationService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.validator = validator;
            this.validationService = validationService;
        }

        public async Task Execute(UpdateBookRequest request,
            CancellationToken cancellationToken = default)
        {
            await validationService.ValidateAsync(validator, request, cancellationToken);

            if (await unitOfWork.BookRepository.GetByIdAsync(request.BookId, cancellationToken) == null)
            {
                throw new NotFoundException("Book not found");
            }

            Book book = mapper.Map<Book>(request.BookDto);
            book.Id = request.BookId;
            await unitOfWork.BookRepository.UpdateAsync(book, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
