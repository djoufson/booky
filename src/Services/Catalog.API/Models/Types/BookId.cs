namespace Catalog.API.Models.Types;

internal record BookId(Guid Id)
{
    internal static BookId Create()
    {
        return new(Guid.NewGuid());
    }
}
