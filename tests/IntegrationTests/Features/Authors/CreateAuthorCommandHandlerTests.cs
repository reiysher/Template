using Application.Abstractions.Messaging.DomainEvents;
using Application.Abstractions.Persistence;
using Application.Features.Authors.Commands.Create;
using Domain.Authors;
using Domain.Authors.Repositories;
using Infrastructure.Messaging;
using IntegrationTests.Abstractions;
using IntegrationTests.Utils.Spies;
using IntegrationTests.Utils.SQLite;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Persistence.Contexts;
using Persistence.Repositories.Authors;

namespace IntegrationTests.Features.Authors;

public class CreateAuthorCommandHandlerTests : BaseIntegrationTests
{
    private readonly IServiceProvider _serviceProvider;

    public CreateAuthorCommandHandlerTests() : base()
    {
        _serviceProvider = new ServiceCollection()
            .AddScoped<CreateAuthorCommandHandler>() // think
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<ApplicationDbContext>(_ => new TestsDbContext(_dbContextOptions))
            .AddScoped<IDomainEventDispatcher, DomainEventDispatcher>()
            .AddScoped<IAuthorRepository, AuthorRepository>()
            .AddMediatR(options => options.RegisterServicesFromAssemblyContaining<CreateAuthorCommandHandler>()) // think
            .BuildServiceProvider();
    }

    [Fact]
    public async Task Author_created_successfully()
    {
        // arrange
        Guid authorId;
        var firstName = "Maksim";
        var birthDay = new DateTime(1990, 1, 8, 0, 0, 0, DateTimeKind.Utc);

        // act
        using (var scope = _serviceProvider.CreateScope())
        {
            var sut = _serviceProvider.GetRequiredService<CreateAuthorCommandHandler>();
            var command = new CreateAuthorCommand(firstName, birthDay);

            authorId = await sut.Handle(command, CancellationToken.None);
        }

        // assert
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var createdAuthor = await dbContext.Set<Author>().SingleOrDefaultAsync(a => a.Id == authorId);

            createdAuthor.Should().NotBeNull();
            createdAuthor!.FullName!.Should().NotBeNull();
            createdAuthor!.FullName!.FirstName.Should().Be(firstName);
            createdAuthor!.BirthDay.Should().Be(birthDay);
        }
    }
}
