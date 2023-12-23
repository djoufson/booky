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
        // using var fileStream = File.Create(path);
        // using var stream = file.OpenReadStream();
        // stream.Seek(0, SeekOrigin.Begin);
        // await stream.CopyToAsync(fileStream);
        // stream.Close();
        // fileStream.Close();
        return $"pics/{fileName}";
    }
    /*
        string imageName = $"{Guid.NewGuid().ToString()}.png";
        byte[] imageBytes = Convert.FromBase64String(request.FileBase64);
        string folderPath = Path.Combine("Files", "Images");
        if(!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);
        
        string path = Path.Combine(folderPath, imageName);
        await IoFile.WriteAllBytesAsync(path, imageBytes);
    */
}
