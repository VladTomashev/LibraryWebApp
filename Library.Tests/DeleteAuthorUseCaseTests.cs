using Library.Application.DTO.Requests;
using Library.Application.Exceptions;
using Library.Application.Interfaces;
using Library.Application.UseCases;
using Library.Core.Entities;
using Library.Core.Interfaces;
using Moq;
using Xunit;

namespace Library.Tests.UseCases
{
    public class DeleteAuthorUseCaseTests
    {
        private readonly Mock<IUnitOfWork> unitOfWorkMock;
        private readonly DeleteAuthorUseCase useCase;

        public DeleteAuthorUseCaseTests()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            useCase = new DeleteAuthorUseCase(unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Execute_AuthorExists_DeletesAuthorSuccessfully()
        {
            var authorId = Guid.NewGuid();
            var author = new Author { Id = authorId };
            var request = new DeleteAuthorRequest
            { AuthorId = authorId };

            unitOfWorkMock
                .Setup(u => u.AuthorRepository.GetByIdAsync(authorId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(author);

            unitOfWorkMock
                .Setup(u => u.AuthorRepository.DeleteAsync(author, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            unitOfWorkMock
                .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(1));

            await useCase.Execute(request);

            unitOfWorkMock.Verify(u => u.AuthorRepository.GetByIdAsync(authorId, It.IsAny<CancellationToken>()), Times.Once);
            unitOfWorkMock.Verify(u => u.AuthorRepository.DeleteAsync(author, It.IsAny<CancellationToken>()), Times.Once);
            unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}