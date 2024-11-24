using Library.Application.Exceptions;
using Library.Core.Interfaces;
using Library.Application.Interfaces.UseCases;
using Library.Core.Entities;
using System.Security.Claims;
using Library.Application.DTO.Requests;

namespace Library.Application.UseCases
{
    public class GetIdByJwtUseCase : IGetIdByJwtUseCase
    {
        private readonly IUnitOfWork unitOfWork;

        public GetIdByJwtUseCase(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Guid> Execute(GetIdByJwtRequest request, CancellationToken cancellationToken = default)
        {
            string? login = request.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            if (login == null)
                throw new NotFoundException("User not found");

            UserAuth? userAuth = await unitOfWork.UserAuthRepository.GetByLoginAsync(login, cancellationToken = default);
            if (userAuth == null)
                throw new NotFoundException("User not found");

            return userAuth.Id;
        }
    }
}
