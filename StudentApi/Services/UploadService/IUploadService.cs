namespace StudentApi.Services.UploadService
{
    public interface IUploadService
    {
        Task<string> UploadImageAsync(IFormFile file);
    }
}
