namespace Catalog.API.Services;

public class ImageService(IWebHostEnvironment environment)
{
    private readonly string _folderPath = Path.Combine(environment.ContentRootPath, "Files", "Images");

    public async Task<string?> Save(string fileBase64, string bookTitle)
    {
        if(!Path.Exists(_folderPath))
            Directory.CreateDirectory(_folderPath);

        string fileName = $"{Guid.NewGuid()}-{bookTitle}.png";
        string path = Path.Combine(_folderPath, fileName);
        byte[] imageBytes = Convert.FromBase64String(fileBase64);

        await File.WriteAllBytesAsync(path, imageBytes);
        return fileName;
    }

    public (string, string) Retrieve(string fileName)
    {
        string imageFileExtension = Path.GetExtension(fileName);
        string path = Path.Combine(_folderPath, fileName);
        string mimetype = GetImageMimeTypeFromImageFileExtension(imageFileExtension);

        return (path, mimetype);
    }

    private static string GetImageMimeTypeFromImageFileExtension(string extension) => extension switch
    {
        ".png" => "image/png",
        ".gif" => "image/gif",
        ".jpg" or ".jpeg" => "image/jpeg",
        ".bmp" => "image/bmp",
        ".tiff" => "image/tiff",
        ".wmf" => "image/wmf",
        ".jp2" => "image/jp2",
        ".svg" => "image/svg+xml",
        _ => "application/octet-stream",
    };
}
