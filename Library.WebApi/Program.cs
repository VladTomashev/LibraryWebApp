using FluentValidation;
using Library.Application.DTO.Validators;
using Library.Application.Interfaces;
using Library.Application.Interfaces.Services;
using Library.Application.Interfaces.UseCases;
using Library.Application.Services;
using Library.Application.UseCases;
using Library.Core.Interfaces;
using Library.Infrastructure.EntityFramework;
using Library.Infrastructure.Repositories;
using Library.WebApi.Middlewares;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddJsonFile("appsettings.json");

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddValidatorsFromAssemblyContaining<AuthorRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<BookRentalRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<BookRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<SignInRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<SignUpRequestValidator>();

        builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
        {
            var configuration = builder.Configuration.GetSection("AppSettings");
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(configuration["JwtSecretKey"]))
            };
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(opt =>
        {
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Please enter token",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer"
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });

        builder.Services.AddDbContext<DataContext>();

        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IValidationService, ValidationService>();

        builder.Services.AddScoped<IAddAuthorUseCase, AddAuthorUseCase>();
        builder.Services.AddScoped<IAddBookUseCase, AddBookUseCase>();
        builder.Services.AddScoped<IDeleteAuthorUseCase, DeleteAuthorUseCase>();
        builder.Services.AddScoped<IDeleteBookUseCase, DeleteBookUseCase>();
        builder.Services.AddScoped<IGetAllAuthorsUseCase, GetAllAuthorsUseCase>();
        builder.Services.AddScoped<IGetAllBookRentalsUseCase, GetAllBookRentalsUseCase>();
        builder.Services.AddScoped<IGetAllBooksUseCase, GetAllBooksUseCase>();
        builder.Services.AddScoped<IGetAllUserProfilesUseCase, GetAllUserProfilesUseCase>();
        builder.Services.AddScoped<IGetAuthorByIdUseCase, GetAuthorByIdUseCase>();
        builder.Services.AddScoped<IGetAvailableBooksUseCase, GetAvailableBooksUseCase>();
        builder.Services.AddScoped<IGetBookByIdUseCase, GetBookByIdUseCase>();
        builder.Services.AddScoped<IGetBookByIsbnUseCase, GetBookByIsbnUseCase>();
        builder.Services.AddScoped<IGetBookRentalByIdUseCase, GetBookRentalByIdUseCase>();
        builder.Services.AddScoped<IGetBookRentalsByUserIdUseCase, GetBookRentalsByUserIdUseCase>();
        builder.Services.AddScoped<IGetBooksByAuthorIdUseCase, GetBooksByAuthorIdUseCase>();
        builder.Services.AddScoped<IGetIdByJwtUseCase, GetIdByJwtUseCase>();
        builder.Services.AddScoped<IGetUnavailableBooksUseCase, GetUnavailableBooksUseCase>();
        builder.Services.AddScoped<IGetUserProfileByIdUseCase, GetUserProfileByIdUseCase>();
        builder.Services.AddScoped<IGiveBookUseCase, GiveBookUseCase>();
        builder.Services.AddScoped<IRefreshTokenUseCase, RefreshTokenUseCase>();
        builder.Services.AddScoped<IReturnBookUseCase, ReturnBookUseCase>();
        builder.Services.AddScoped<ISignInUseCase, SignInUseCase>();
        builder.Services.AddScoped<ISignUpUseCase, SignUpUseCase>();
        builder.Services.AddScoped<IUpdateAuthorUseCase, UpdateAuthorUseCase>();
        builder.Services.AddScoped<IUpdateBookUseCase, UpdateBookUseCase>();

        builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
        builder.Services.AddScoped<IBookRentalRepository, BookRentalRepository>();
        builder.Services.AddScoped<IBookRepository, BookRepository>();
        builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        builder.Services.AddScoped<IUserAuthRepository, UserAuthRepository>();
        builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}