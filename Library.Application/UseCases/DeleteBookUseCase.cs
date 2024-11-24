using Library.Application.Exceptions;
using Library.Core.Interfaces;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;
using Library.Application.DTO.Requests;

namespace Library.Application.UseCases
{
    public class DeleteBookUseCase : IDeleteBookUseCase
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteBookUseCase(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Execute(DeleteBookRequest request, CancellationToken cancellationToken = default)
        {
            Book book = await unitOfWork.BookRepository.GetByIdAsync(request.BookId, cancellationToken);
            if (book == null)
            {
                throw new NotFoundException("Book not found");
            }
            await unitOfWork.BookRepository.DeleteAsync(book, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
