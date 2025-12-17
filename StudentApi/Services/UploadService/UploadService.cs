namespace StudentApi.Services.UploadService
{
    public class UploadService: IUploadService
    {
        public async Task<string> UploadImageAsync(IFormFile file)
        {
            try
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string filename = Path.GetFileName(file.FileName);
                // add date tiem ticks to make filename unique
                filename = $"profile_{DateTime.Now.Ticks}{Path.GetExtension(filename)}";
                var filePath = Path.Combine(uploadsFolder, filename);

                using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);

                return Path.Combine("uploads", filename).Replace("\\", "/");
            }
            catch(Exception ex)
            {
                // log file upload fail log for further analysis
                return null;
            }
        }
    }
}
