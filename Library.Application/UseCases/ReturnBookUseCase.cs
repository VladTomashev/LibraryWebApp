using Library.Application.Exceptions;
using Library.Application.Interfaces;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;

namespace Library.Application.UseCases
{
    public class ReturnBookUseCase : IReturnBookUseCase
    {
        private readonly IUnitOfWork unitOfWork;

        public ReturnBookUseCase(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async void Execute(Guid bookRentalId, CancellationToken cancellationToken = default)
        {
            BookRental bookRental = await unitOfWork.BookRentalRepository.GetByIdAsync(bookRentalId, cancellationToken);
            if (bookRental == null)
            {
                throw new NotFoundException("Book rental not found");
            }
            else if (bookRental.IsReturned == true)
            {
                throw new BadRequestException("Book rental already returned");
            }
            else
            {
                bookRental.IsReturned = true;
                await unitOfWork.BookRentalRepository.UpdateAsync(bookRental, cancellationToken);
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
