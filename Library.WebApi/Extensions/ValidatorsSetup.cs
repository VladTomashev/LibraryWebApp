using FluentValidation;
using Library.Application.Validators.DtoValidators;
using Library.Application.Validators.RequestValidators;

namespace Library.WebApi.Extensions
{
    public static class ValidatorsSetup
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddValidatorsFromAssemblyContaining<AuthorDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<BookDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<BookRentalDtoValidator>();

            services.AddValidatorsFromAssemblyContaining<AddAuthorRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<AddBookRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<GiveBookRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<SignInRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<SignUpRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateAuthorRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateBookRequestValidator>();

            return services;
        }
    }
}
