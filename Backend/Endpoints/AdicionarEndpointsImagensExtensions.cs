

public static class AdicionarEndpointsImagensExtensions
{
    public static void AdicionarEndpointsImagens(this WebApplication app)
    {
        app.MapPost("/api/imagens/upload", async (IWebHostEnvironment env, IFormFile file) =>
        {
            if (file == null || file.Length == 0)
                return Results.BadRequest("No file uploaded.");

            var uploadPath = Path.Combine(env.WebRootPath, "img");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            var imageUrl = $"/img/{fileName}";
            return Results.Ok(new { ImageUrl = imageUrl });
        });

    }
}
