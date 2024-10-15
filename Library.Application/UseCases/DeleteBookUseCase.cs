using Library.Application.Exceptions;
using Library.Application.Interfaces;
using Library.Application.Interfaces.UseCases;

namespace Library.Application.UseCases
{
    public class DeleteBookUseCase : IDeleteBookUseCase
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteBookUseCase(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async void Execute(Guid id, CancellationToken cancellationToken = default)
        {
            if (unitOfWork.BookRepository.GetByIdAsync(id, cancellationToken) == null)
            {
                throw new NotFoundException("Book not found");
            }
            await unitOfWork.BookRepository.DeleteAsync(id, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
