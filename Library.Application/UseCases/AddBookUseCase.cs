﻿using AutoMapper;
using FluentValidation;
using Library.Application.DTO.Requests;
using Library.Application.Interfaces.Services;
using Library.Core.Interfaces;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;
using Library.Application.Exceptions;

namespace Library.Application.UseCases
{
    public class AddBookUseCase : IAddBookUseCase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IValidator<AddBookRequest> validator;
        private readonly IValidationService validationService;

        public AddBookUseCase(IUnitOfWork unitOfWork, IMapper mapper,
            IValidator<AddBookRequest> validator, IValidationService validationService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.validator = validator;
            this.validationService = validationService;
        }

        public async Task Execute(AddBookRequest request, CancellationToken cancellationToken = default)
        {
            await validationService.ValidateAsync(validator, request, cancellationToken);

            if (await unitOfWork.AuthorRepository.GetByIdAsync(request.BookDto.AuthorId, cancellationToken) == null)
            {
                throw new NotFoundException("Author not found");
            }
            if (await unitOfWork.BookRepository.GetByIsbnAsync(request.BookDto.Isbn, cancellationToken) != null)
            {
                throw new NotFoundException("Book with this isbn already exists");
            }

            Book book = mapper.Map<Book>(request.BookDto);
            book.Id = Guid.NewGuid();
            await unitOfWork.BookRepository.AddAsync(book, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
