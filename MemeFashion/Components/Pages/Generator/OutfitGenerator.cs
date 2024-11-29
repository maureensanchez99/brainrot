using Microsoft.AspNetCore.Components.Forms;

public class OutfitGeneratorService
{
    private readonly IWebHostEnvironment _environment;

    public OutfitGeneratorService(IWebHostEnvironment environment) {
        _environment = environment;
    }

    public async Task<string> SaveImage(IBrowserFile file) {
        var uploadPath = Path.Combine(_environment.WebRootPath, "uploads");
        
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.Name)}";
        var filePath = Path.Combine(uploadPath, fileName);

        using (var stream = file.OpenReadStream())
        using (var fs = new FileStream(filePath, FileMode.Create)) {
            await stream.CopyToAsync(fs);
        }

        return $"/uploads/{fileName}";
    }
}
