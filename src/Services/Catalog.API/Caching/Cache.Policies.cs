namespace Catalog.API.Caching;

public partial class Cache
{
    public static class Policies
    {
        public const string GetBooks = "books";
        public const string GetAllTags = "all-tags";
        public const string Authors = "authors";
    }
}
