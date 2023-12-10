namespace Catalog.API.Models.Types;

internal record AuthorId(Guid Value)
{
    internal static AuthorId Create()
    {
        return new(Guid.NewGuid());
    }
}
