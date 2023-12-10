namespace Catalog.API.Models.Types;

internal record BookId(Guid Value)
{
    internal static BookId Create()
    {
        return new(Guid.NewGuid());
    }
}
