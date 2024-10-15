﻿using AutoMapper;
using FluentValidation;
using Library.Application.DTO.Requests;
using Library.Application.Interfaces.Services;
using Library.Application.Interfaces;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;

namespace Library.Application.UseCases
{
    public class AddBookUseCase : IAddBookUseCase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IValidator<BookRequest> validator;
        private readonly IValidationService validationService;

        public AddBookUseCase(IUnitOfWork unitOfWork, IMapper mapper,
            IValidator<BookRequest> validator, IValidationService validationService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.validator = validator;
            this.validationService = validationService;
        }

        public async void Execute(BookRequest request, CancellationToken cancellationToken = default)
        {
            await validationService.ValidateAsync(validator, request, cancellationToken);
            Book book = mapper.Map<Book>(request);
            book.Id = Guid.NewGuid();
            await unitOfWork.BookRepository.AddAsync(book, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}