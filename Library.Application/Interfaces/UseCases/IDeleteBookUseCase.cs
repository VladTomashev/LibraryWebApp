﻿namespace Library.Application.Interfaces.UseCases
{
    public interface IDeleteBookUseCase
    {
        public Task Execute(Guid id, CancellationToken cancellationToken = default);
    }
}
