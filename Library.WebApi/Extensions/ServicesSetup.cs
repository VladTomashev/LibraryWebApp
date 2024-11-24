using Library.Application.Interfaces.Services;
using Library.Application.Interfaces.UseCases;
using Library.Application.Services;
using Library.Application.UseCases;
using Library.Core.Interfaces;
using Library.Infrastructure.Repositories;
using Library.Infrastructure.Services;

namespace Library.WebApi.Extensions
{
    public static class ScopedServicesSetup
    {
        public static IServiceCollection AddScopedServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<IImageService, ImageService>();

            services.AddScoped<IAddAuthorUseCase, AddAuthorUseCase>();
            services.AddScoped<IAddBookUseCase, AddBookUseCase>();
            services.AddScoped<IDeleteAuthorUseCase, DeleteAuthorUseCase>();
            services.AddScoped<IDeleteBookUseCase, DeleteBookUseCase>();
            services.AddScoped<IGetAllAuthorsUseCase, GetAllAuthorsUseCase>();
            services.AddScoped<IGetAllBookRentalsUseCase, GetAllBookRentalsUseCase>();
            services.AddScoped<IGetAllBooksUseCase, GetAllBooksUseCase>();
            services.AddScoped<IGetAllUserProfilesUseCase, GetAllUserProfilesUseCase>();
            services.AddScoped<IGetAuthorByIdUseCase, GetAuthorByIdUseCase>();
            services.AddScoped<IGetAvailableBooksUseCase, GetAvailableBooksUseCase>();
            services.AddScoped<IGetBookByIdUseCase, GetBookByIdUseCase>();
            services.AddScoped<IGetBookByIsbnUseCase, GetBookByIsbnUseCase>();
            services.AddScoped<IGetBookRentalByIdUseCase, GetBookRentalByIdUseCase>();
            services.AddScoped<IGetBookRentalsByUserIdUseCase, GetBookRentalsByUserIdUseCase>();
            services.AddScoped<IGetBooksByAuthorIdUseCase, GetBooksByAuthorIdUseCase>();
            services.AddScoped<IGetIdByJwtUseCase, GetIdByJwtUseCase>();
            services.AddScoped<IGetUnavailableBooksUseCase, GetUnavailableBooksUseCase>();
            services.AddScoped<IGetUserProfileByIdUseCase, GetUserProfileByIdUseCase>();
            services.AddScoped<IGiveBookUseCase, GiveBookUseCase>();
            services.AddScoped<IRefreshTokenUseCase, RefreshTokenUseCase>();
            services.AddScoped<IReturnBookUseCase, ReturnBookUseCase>();
            services.AddScoped<ISignInUseCase, SignInUseCase>();
            services.AddScoped<ISignUpUseCase, SignUpUseCase>();
            services.AddScoped<IUpdateAuthorUseCase, UpdateAuthorUseCase>();
            services.AddScoped<IUpdateBookUseCase, UpdateBookUseCase>();
            services.AddScoped<IUploadBookImageUseCase, UploadBookImageUseCase>();

            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRentalRepository, BookRentalRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IUserAuthRepository, UserAuthRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
