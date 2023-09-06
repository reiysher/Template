using Application.Abstractions.Persistence;
using Application.Features.Notes.Commands.Create;
using Domain.Authors.Repositories;
using Domain.Notes.Exceptions;
using Domain.Notes.Repositories;
using FluentAssertions;
using Moq;

namespace Tests.Notes.Commands;

public class CreateNoteCommandHandlerTests
{
    private readonly Mock<INoteRepository> _noteRepository;
    private readonly Mock<IAuthorRepository> _authorRepository;
    private readonly Mock<IUnitOfWork> _unitOfWork;

    public CreateNoteCommandHandlerTests()
    {
        _noteRepository = new();
        _authorRepository = new();
        _unitOfWork = new();
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
            _noteRepository.Object,
            _authorRepository.Object,
            _unitOfWork.Object);

        // act
        var action = () => handler.Handle(command, default);

        // accert
        action.Should().ThrowAsync<NoteTitleTooLongException>();
    }
}
