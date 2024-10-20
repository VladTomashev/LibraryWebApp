using AutoMapper;
using FluentValidation;
using Library.Application.DTO.Requests;
using Library.Application.Interfaces;
using Library.Application.Interfaces.Services;
using Library.Application.UseCases;
using Library.Core.Entities;
using Moq;
using Xunit;

namespace Library.Tests.UseCases
{
    public class AddAuthorUseCaseTests
    {
        private readonly Mock<IUnitOfWork> unitOfWorkMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IValidator<AuthorRequest>> validatorMock;
        private readonly Mock<IValidationService> validationServiceMock;
        private readonly AddAuthorUseCase useCase;

        public AddAuthorUseCaseTests()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            mapperMock = new Mock<IMapper>();
            validatorMock = new Mock<IValidator<AuthorRequest>>();
            validationServiceMock = new Mock<IValidationService>();

            useCase = new AddAuthorUseCase(
                unitOfWorkMock.Object,
                mapperMock.Object,
                validatorMock.Object,
                validationServiceMock.Object);
        }

        [Fact]
        public async Task Execute_ValidRequest_AddsAuthorSuccessfully()
        {

            var request = new AuthorRequest
            {
                FirstName = "Bob",
                LastName = "Bobov",
                Country = "USA",
                DateOfBirth = DateTime.Now.AddYears(-30)
            };

            var author = new Author
            {
                Id = Guid.NewGuid(),
                FirstName = "Bob",
                LastName = "Bobov",
                Country = "USA",
                DateOfBirth = DateTime.Now.AddYears(-30)
            };

            validationServiceMock
                .Setup(v => v.ValidateAsync(validatorMock.Object, request, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            mapperMock.Setup(m => m.Map<Author>(request)).Returns(author);

            unitOfWorkMock
                .Setup(u => u.AuthorRepository.AddAsync(It.IsAny<Author>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            unitOfWorkMock
                .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(1)); 

            await useCase.Execute(request);

            validationServiceMock.Verify(v => v.ValidateAsync(validatorMock.Object, request, It.IsAny<CancellationToken>()), Times.Once);
            mapperMock.Verify(m => m.Map<Author>(request), Times.Once);
            unitOfWorkMock.Verify(u => u.AuthorRepository.AddAsync(It.IsAny<Author>(), It.IsAny<CancellationToken>()), Times.Once);
            unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}