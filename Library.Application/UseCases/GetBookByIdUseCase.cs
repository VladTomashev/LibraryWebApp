﻿using AutoMapper;
using Library.Application.DTO.Responses;
using Library.Application.Exceptions;
using Library.Core.Interfaces;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;
using Library.Application.DTO.Requests;

namespace Library.Application.UseCases
{
    public class GetBookByIdUseCase : IGetBookByIdUseCase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetBookByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<BookResponse> Execute(GetBookByIdRequest request,
            CancellationToken cancellationToken = default)
        {
            Book? book = await unitOfWork.BookRepository.GetByIdAsync(request.BookId, cancellationToken);
            if (book == null)
            {
                throw new NotFoundException("Book not found");
            }
            else
            {
                BookResponse response = mapper.Map<BookResponse>(book);
                return response;
            }
        }
    }
}
