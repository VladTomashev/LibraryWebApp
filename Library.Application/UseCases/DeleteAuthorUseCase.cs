using Library.Application.Exceptions;
using Library.Core.Interfaces;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;

namespace Library.Application.UseCases
{
    public class DeleteAuthorUseCase : IDeleteAuthorUseCase
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteAuthorUseCase(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Execute(Guid id, CancellationToken cancellationToken = default)
        {
            Author author = await unitOfWork.AuthorRepository.GetByIdAsync(id, cancellationToken);
            if (author == null)
            {
                throw new NotFoundException("Author not found");
            }
            await unitOfWork.AuthorRepository.DeleteAsync(author, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
