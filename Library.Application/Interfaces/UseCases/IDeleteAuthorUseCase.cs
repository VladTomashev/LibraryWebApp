namespace Library.Application.Interfaces.UseCases
{
    public interface IDeleteAuthorUseCase
    {
        public Task Execute(Guid id, CancellationToken cancellationToken = default);
    }
}
