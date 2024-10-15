﻿namespace Library.Application.Interfaces.UseCases
{
    public interface IDeleteAuthorUseCase
    {
        public void Execute(Guid id, CancellationToken cancellationToken = default);
    }
}