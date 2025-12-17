using StudentApi.Dtos;

namespace StudentApi.Services.StudentService
{
    public interface IStudentService
    {
        Task<List<StudentDto>> GetAllAsync();
        Task<StudentDto?> GetByIdAsync(int id);
        Task<string> CreateAsync(StudentCreateUpdateDto dto);
        Task<string> UpdateAsync(int id, StudentCreateUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
