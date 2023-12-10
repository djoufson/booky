namespace Catalog.API.Models.Types;

internal record AuthorId(Guid Id)
{
    internal static AuthorId Create()
    {
        return new(Guid.NewGuid());
    }
}
