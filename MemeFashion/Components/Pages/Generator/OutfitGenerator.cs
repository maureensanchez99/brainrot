public class MemeGalleryService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly string _memesPath;

    public MemeGalleryService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
        _memesPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "memes");
    }

    public IEnumerable<string> GetAllMemes()
    {
        if (!Directory.Exists(_memesPath))
        {
            return Enumerable.Empty<string>();
        }

        return Directory.GetFiles(_memesPath)
            .Select(Path.GetFileName)
            .Where(filename => filename.EndsWith(".jpg") || 
                             filename.EndsWith(".jpeg") || 
                             filename.EndsWith(".png"));
    }

    public IEnumerable<string> GetMemesPaginated(int page, int pageSize)
    {
        return GetAllMemes()
            .Skip(page * pageSize)
            .Take(pageSize);
    }

    public int GetTotalMemeCount()
    {
        return GetAllMemes().Count();
    }

    public bool MemeExists(string filename)
    {
        var fullPath = Path.Combine(_memesPath, filename);
        return File.Exists(fullPath);
    }
}
