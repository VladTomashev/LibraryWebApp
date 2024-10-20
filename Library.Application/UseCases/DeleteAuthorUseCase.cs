using Library.Application.Exceptions;
using Library.Application.Interfaces;
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
            if (unitOfWork.AuthorRepository.GetByIdAsync(id, cancellationToken) == null)
            {
                throw new NotFoundException("Author not found");
            }
            await unitOfWork.AuthorRepository.DeleteAsync(id, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
