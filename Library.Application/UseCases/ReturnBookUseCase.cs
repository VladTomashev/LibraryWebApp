using Library.Application.Exceptions;
using Library.Core.Interfaces;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;
using Library.Application.DTO.Requests;

namespace Library.Application.UseCases
{
    public class ReturnBookUseCase : IReturnBookUseCase
    {
        private readonly IUnitOfWork unitOfWork;

        public ReturnBookUseCase(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Execute(ReturnBookRequest request, CancellationToken cancellationToken = default)
        {
            BookRental bookRental = await unitOfWork.BookRentalRepository
                .GetByIdAsync(request.BookRentalId, cancellationToken);
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
