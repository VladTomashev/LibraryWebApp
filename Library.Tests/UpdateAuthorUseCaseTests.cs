﻿using AutoMapper;
using FluentValidation;
using Library.Application.DTO.Basics;
using Library.Application.DTO.Requests;
using Library.Application.Exceptions;
using Library.Application.Interfaces;
using Library.Application.Interfaces.Services;
using Library.Application.UseCases;
using Library.Core.Entities;
using Library.Core.Interfaces;
using Moq;
using Xunit;

namespace Library.Tests.UseCases
{
    public class UpdateAuthorUseCaseTests
    {
        private readonly Mock<IUnitOfWork> unitOfWorkMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IValidator<UpdateAuthorRequest>> validatorMock;
        private readonly Mock<IValidationService> validationServiceMock;
        private readonly UpdateAuthorUseCase useCase;

        public UpdateAuthorUseCaseTests()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            mapperMock = new Mock<IMapper>();
            validatorMock = new Mock<IValidator<UpdateAuthorRequest>>();
            validationServiceMock = new Mock<IValidationService>();

            useCase = new UpdateAuthorUseCase(
                unitOfWorkMock.Object,
                mapperMock.Object,
                validatorMock.Object,
                validationServiceMock.Object);
        }

        [Fact]
        public async Task Execute_ValidRequest_UpdatesAuthorSuccessfully()
        {
            var authorId = Guid.NewGuid();
            var authorDto = new AuthorDto
            {
                FirstName = "John",
                LastName = "Doe",
                Country = "USA",
                DateOfBirth = DateTime.Now.AddYears(-30)
            };

            var request = new UpdateAuthorRequest
            { 
                AuthorDto =  authorDto,
                AuthorId = authorId,
            };

            var author = new Author
            {
                Id = authorId,
                FirstName = "John",
                LastName = "Doe",
                Country = "USA",
                DateOfBirth = request.AuthorDto.DateOfBirth
            };

            validationServiceMock
                .Setup(v => v.ValidateAsync(validatorMock.Object, request, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            unitOfWorkMock
                .Setup(u => u.AuthorRepository.GetByIdAsync(authorId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(author);

            mapperMock.Setup(m => m.Map<Author>(request)).Returns(author);

            unitOfWorkMock
                .Setup(u => u.AuthorRepository.UpdateAsync(It.IsAny<Author>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            unitOfWorkMock
                .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            await useCase.Execute(request);

            validationServiceMock.Verify(v => v.ValidateAsync(validatorMock.Object, request, It.IsAny<CancellationToken>()), Times.Once);
            unitOfWorkMock.Verify(u => u.AuthorRepository.GetByIdAsync(authorId, It.IsAny<CancellationToken>()), Times.Once);
            unitOfWorkMock.Verify(u => u.AuthorRepository.UpdateAsync(It.IsAny<Author>(), It.IsAny<CancellationToken>()), Times.Once);
            unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}