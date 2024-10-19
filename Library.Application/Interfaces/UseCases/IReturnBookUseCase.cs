namespace Library.Application.Interfaces.UseCases
{
    public interface IReturnBookUseCase
    {
        public Task Execute(Guid bookRentalId, CancellationToken cancellationToken = default);
    }
}
