using AutoMapper;
using Library.Application.DTO.Responses;
using Library.Application.Interfaces;
using Library.Application.UseCases;
using Library.Core.Entities;
using Moq;
using Xunit;

namespace Library.Tests.UseCases
{
    public class GetAllAuthorsUseCaseTests
    {
        private readonly Mock<IUnitOfWork> unitOfWorkMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly GetAllAuthorsUseCase useCase;

        public GetAllAuthorsUseCaseTests()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            mapperMock = new Mock<IMapper>();
            useCase = new GetAllAuthorsUseCase(unitOfWorkMock.Object, mapperMock.Object);
        }

        [Fact]
        public async Task Execute_ReturnsAuthorResponses()
        {
            var authors = new List<Author>
            {
                new Author { Id = Guid.NewGuid(), FirstName = "Ivan", LastName = "Ivanov", Country = "Russia", DateOfBirth = DateTime.Now.AddYears(-100) },
                new Author { Id = Guid.NewGuid(), FirstName = "Vlad", LastName = "Vladov", Country = "Belarus", DateOfBirth = DateTime.Now.AddYears(-50) }
            };

            var authorResponses = new List<AuthorResponse>
            {
                new AuthorResponse { Id = authors[0].Id, FirstName = "Ivan", LastName = "Ivanov", Country = "Russia", DateOfBirth = authors[0].DateOfBirth },
                new AuthorResponse { Id = authors[1].Id, FirstName = "Vlad", LastName = "Vladov", Country = "Belarus", DateOfBirth = authors[1].DateOfBirth }
            };

            var paginationParams = new PaginationParams();

            unitOfWorkMock
                .Setup(u => u.AuthorRepository.GetAllAsync(paginationParams, It.IsAny<CancellationToken>()))
                .ReturnsAsync(authors);

            mapperMock
                .Setup(m => m.Map<IEnumerable<AuthorResponse>>(authors))
                .Returns(authorResponses);

            var result = await useCase.Execute(paginationParams);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal(authorResponses.First().FirstName, result.First().FirstName);
            Assert.Equal(authorResponses.Last().LastName, result.Last().LastName);

            unitOfWorkMock.Verify(u => u.AuthorRepository.GetAllAsync(paginationParams, It.IsAny<CancellationToken>()), Times.Once);
            mapperMock.Verify(m => m.Map<IEnumerable<AuthorResponse>>(authors), Times.Once);
        }
    }
}