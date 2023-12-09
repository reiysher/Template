using Application.Abstractions.Persistence;
using Application.Features.Notes.Commands.Create;
using Domain.Authors.Repositories;
using Domain.Notes.Exceptions;
using Domain.Notes.Repositories;

namespace IntegrationTests.Features.Notes;

public class CreateNoteCommandHandlerTests
{
    private readonly INoteRepository _noteRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateNoteCommandHandlerTests()
    {
        _noteRepository = Substitute.For<INoteRepository>();
        _authorRepository = Substitute.For<IAuthorRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
    }

    [Fact]
    public void Handle_Shoud_ThrowingException_WhenTitleTooLong()
    {
        // arrange
        var command = new CreateNoteCommand(
            Guid.NewGuid(),
            "Too_loooooong_title",
            "Some content");

        var handler = new CreateNoteCommandHandler(
            _noteRepository,
            _authorRepository,
            _unitOfWork);

        // act
        var action = () => handler.Handle(command, default);

        // accert
        action.Should().ThrowAsync<NoteTitleTooLongException>();
    }
}
