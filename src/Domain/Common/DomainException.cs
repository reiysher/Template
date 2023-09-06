namespace Domain.Common;

public abstract class DomainException : ApplicationException
{
    protected DomainException()
    {
    }

    protected DomainException(string message)
        : base(message)
    {
    }
}
