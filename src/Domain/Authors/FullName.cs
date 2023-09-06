namespace Domain.Authors;

public sealed record FullName(
    string FirstName,
    string LastName,
    string MiddleName)
{
    internal string GetFullName()
    {
        var items = new string[]
        {
            FirstName, LastName, MiddleName
        };

        return string.Join(", ", items.Where(item => !String.IsNullOrWhiteSpace(item)));
    }
}
