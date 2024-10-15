namespace Library.Application.Interfaces.UseCases
{
    public interface IReturnBookUseCase
    {
        public void Execute(Guid bookRentalId, CancellationToken cancellationToken = default);
    }
}
