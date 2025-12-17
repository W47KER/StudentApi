using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.Dtos;
using StudentApi.Models;

namespace StudentApi.Services.StudentService
{
    public class StudentService: IStudentService
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        #region get all students
        public async Task<List<StudentDto>> GetAllAsync()
        {
            try
            {
                var students = await _context.Students
                .AsNoTracking()
                .Where(s => s.IsDelete == false)
                .ToListAsync();

                return students.Select(ToDto).ToList();
            }
            catch(Exception ex)
            {
                // log exception for further analysis

                return null;
            }

        }
        #endregion

        #region get student details by studentId
        public async Task<StudentDto?> GetByIdAsync(int id)
        {
            try
            {
                var student = await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id && s.IsDelete == false);

                return student == null ? null : ToDto(student);
            }
            catch(Exception ex)
            {
                // log exception for further analysis
                return null;    
            }
        }
        #endregion

        #region create new student
        public async Task<string> CreateAsync(StudentCreateUpdateDto dto)
        {
            try
            {
                // check if student with same roll number exists
                bool exists = await _context.Students
                    .AnyAsync(s => s.RollNumber == dto.RollNumber && s.IsDelete == false);

                if (exists)
                    return "Student with the same roll number already exists.";

                // check if student with same email exists
                exists = await _context.Students
                    .AnyAsync(s => s.Email == dto.Email && s.IsDelete == false);

                if (exists)
                    return "Student with the same email already exists.";

                var student = new Student
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    RollNumber = dto.RollNumber,
                    Gender = dto.Gender,
                    Email = dto.Email,
                    ProfilePhoto = dto.ProfilePhoto,
                    CreatedAt = DateTime.Now
                };

                _context.Students.Add(student);
                await _context.SaveChangesAsync(); 

                return string.Empty;
            }
            catch(Exception ex)
            {
                // log exception for further analysis
                return "An error occure while saving a student data!";
            }
        }
        #endregion

        #region update user by userId
        public async Task<string> UpdateAsync(int id, StudentCreateUpdateDto dto)
        {
            try
            {
                var existing = await _context.Students.FirstOrDefaultAsync(s => s.Id == id && s.IsDelete == false);
                if (existing == null)
                    return "Student not found!";

                // before updating, check if another student with same roll number exists
                bool exists = await _context.Students
                    .AnyAsync(s => s.Id != id && s.RollNumber == dto.RollNumber && s.IsDelete == false);
                if (exists)
                    return "Another student with the same roll number already exists.";

                // check if another student with same email exists
                exists = await _context.Students
                    .AnyAsync(s => s.Id != id && s.Email == dto.Email && s.IsDelete == false);
                if (exists)
                    return "Another student with the same email already exists.";

                existing.FirstName = dto.FirstName;
                existing.LastName = dto.LastName;
                existing.RollNumber = dto.RollNumber;
                existing.Gender = dto.Gender;
                existing.Email = dto.Email;
                existing.ProfilePhoto = dto.ProfilePhoto;
                existing.UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:s");

                await _context.SaveChangesAsync();
                return string.Empty;
            }
            catch(Exception ex)
            {
                // log exception for further analysis
                return "An error occure while updating student data!";
            }
        }
        #endregion

        #region soft delete user
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var existing = await _context.Students.FirstOrDefaultAsync(s => s.Id == id && s.IsDelete == false);
                if (existing == null)
                    return false;

                existing.IsDelete = true;

                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region helper function
        // Mapping helpers
        private static StudentDto ToDto(Student s)
        {
            return new StudentDto
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                RollNumber = s.RollNumber,
                Gender = s.Gender,
                CreatedAt = s.CreatedAt,
                Email = s.Email,
                ProfilePhoto = s.ProfilePhoto
            };
        }
        #endregion
    }
}
