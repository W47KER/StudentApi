using Microsoft.AspNetCore.Mvc;
using StudentApi.Services.UploadService;

namespace StudentApi.Controllers
{
    [ApiController]
    [Route("api/upload")]
    public class UploadController : ControllerBase
    {
        private readonly IUploadService _uploadService;
        public UploadController(IUploadService uploadService)
        {
            _uploadService = uploadService;
        }

        [HttpPost]
        [Route("profile")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No file uploaded.");

                // check file type
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                    return BadRequest("Invalid file type. Only image files are allowed.");

                // check file size (max 1MB)
                const long maxFileSize = 1 * 1024 * 1024; // 1MB
                if (file.Length > maxFileSize)
                    return BadRequest("File size exceeds the maximum limit of 1MB.");

                var imageUrl = await _uploadService.UploadImageAsync(file);
                return Ok(imageUrl);
            }
            catch(Exception ex)
            {
                return BadRequest("An error occure while uploading profile image!");
            }
            
        }
    }
}
