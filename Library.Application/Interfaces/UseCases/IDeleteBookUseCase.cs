namespace Library.Application.Interfaces.UseCases
{
    public interface IDeleteBookUseCase
    {
        public void Execute(Guid id, CancellationToken cancellationToken = default);
    }
}
