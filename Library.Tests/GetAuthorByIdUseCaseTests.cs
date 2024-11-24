using AutoMapper;
using Library.Application.DTO.Requests;
using Library.Application.DTO.Responses;
using Library.Application.Exceptions;
using Library.Application.Interfaces;
using Library.Application.UseCases;
using Library.Core.Entities;
using Library.Core.Interfaces;
using Moq;
using Xunit;

namespace Library.Tests.UseCases
{
    public class GetAuthorByIdUseCaseTests
    {
        private readonly Mock<IUnitOfWork> unitOfWorkMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly GetAuthorByIdUseCase useCase;

        public GetAuthorByIdUseCaseTests()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            mapperMock = new Mock<IMapper>();
            useCase = new GetAuthorByIdUseCase(unitOfWorkMock.Object, mapperMock.Object);
        }

        [Fact]
        public async Task Execute_AuthorExists_ReturnsAuthorResponse()
        {
            var authorId = Guid.NewGuid();
            var author = new Author { Id = authorId, FirstName = "Ivan", LastName = "Ivanov", Country = "Russia", DateOfBirth = DateTime.Now.AddYears(-100) };
            var request = new GetAuthorByIdRequest
            { AuthorId = authorId };

            var authorResponse = new AuthorResponse
            {
                Id = author.Id,
                FirstName = "Ivan",
                LastName = "Ivanov",
                Country = "Russia",
                DateOfBirth = author.DateOfBirth
            };

            unitOfWorkMock
                .Setup(u => u.AuthorRepository.GetByIdAsync(authorId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(author);

            mapperMock
                .Setup(m => m.Map<AuthorResponse>(author))
                .Returns(authorResponse);

            var result = await useCase.Execute(request);

            Assert.NotNull(result);
            Assert.Equal(authorResponse.FirstName, result.FirstName);
            Assert.Equal(authorResponse.LastName, result.LastName);

            unitOfWorkMock.Verify(u => u.AuthorRepository.GetByIdAsync(authorId, It.IsAny<CancellationToken>()), Times.Once);
            mapperMock.Verify(m => m.Map<AuthorResponse>(author), Times.Once);
        }
    }
}