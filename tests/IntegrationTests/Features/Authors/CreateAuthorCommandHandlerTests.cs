using Application.Abstractions.Messaging.DomainEvents;
using Application.Abstractions.Persistence;
using Application.Features.Authors.Commands.Create;
using Domain.Authors;
using Domain.Authors.Repositories;
using Infrastructure.Messaging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Repositories.Authors;

namespace IntegrationTests.Features.Authors;

public class CreateAuthorCommandHandlerTests : BaseIntegrationTests
{
    [Fact]
    public async Task Author_created_successfully()
    {
        // arrange
        Guid authorId;
        var firstName = "Maksim";
        var birthDay = new DateTime(1990, 01, 08, 0, 0, 0, DateTimeKind.Utc);

        using (var dbContext = GetDbContext())
        {
            var publisher = Substitute.For<IPublisher>(); // подумать как без мока обойтись

            IDomainEventDispatcher dispatcher = new DomainEventDispatcher(publisher);
            IAuthorRepository authorRepository = new AuthorRepository(dbContext);
            IUnitOfWork unitOfWork = new UnitOfWork(dbContext, dispatcher);
            var sut = new CreateAuthorCommandHandler(authorRepository, unitOfWork);

            // act
            authorId = await sut.Handle(new CreateAuthorCommand(firstName, birthDay), CancellationToken.None);
        }

        // assert
        using (var dbContext = GetDbContext())
        {
            var createdAuthor = await dbContext.Set<Author>().SingleOrDefaultAsync(a => a.Id == authorId);

            createdAuthor.Should().NotBeNull();
            createdAuthor!.FullName!.Should().NotBeNull();
            createdAuthor!.FullName!.FirstName.Should().Be(firstName);
            createdAuthor!.BirthDay.Should().Be(birthDay);
        }
    }
}
