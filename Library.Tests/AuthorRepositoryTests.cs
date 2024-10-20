using Xunit;
using Microsoft.EntityFrameworkCore;
using Library.Infrastructure.EntityFramework;
using Library.Infrastructure.Repositories;
using Library.Core.Entities;

namespace Library.Tests
{
    public class AuthorRepositoryTests
    {
        private readonly DataContext context;
        private readonly AuthorRepository repository;

        public AuthorRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "LibraryDB")
                .Options;

            context = new DataContext(options);
            repository = new AuthorRepository(context);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllAuthors()
        {
            List<Author> authors = new List<Author>()
            {
                new Author{Id = Guid.NewGuid(), FirstName = "Ivan", LastName = "Ivanov",
                    Country = "Russia", DateOfBirth = DateTime.Now.AddYears(-100)},
                new Author{Id = Guid.NewGuid(), FirstName = "Vlad", LastName = "Vladov",
                    Country = "Belarus", DateOfBirth = DateTime.Now.AddYears(-50)},
            };
            
            await context.Authors.AddRangeAsync(authors);
            await context.SaveChangesAsync();

            var result = await repository.GetAllAsync(paginationParams: null);
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task AddAsync_SavesAuthorSuccessfully()
        {
            var author = new Author
            {
                Id = Guid.NewGuid(),
                FirstName = "Bob",
                LastName = "Bobov",
                Country = "USA",
                DateOfBirth = DateTime.Now.AddYears(-30)
            };

            await repository.AddAsync(author);
            await context.SaveChangesAsync();

            var result = await context.Authors.FindAsync(author.Id);  
            Assert.NotNull(result);
            Assert.Equal(author.FirstName, result.FirstName);
            Assert.Equal(author.LastName, result.LastName);
            Assert.Equal(author.Country, result.Country);
        }
    }
}
