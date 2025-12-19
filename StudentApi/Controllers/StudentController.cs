using Microsoft.AspNetCore.Mvc;
using StudentApi.Dtos;
using StudentApi.Services.StudentService;

namespace StudentApi.Controllers
{
    [ApiController]
    [Route("api/")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        [Route("students")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var students = await _studentService.GetAllAsync();
                if (students == null)
                    return BadRequest(new { message = "An error occure while fetching students data!" }); // error while fetching students data
                return Ok(students);
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = "An errror occure while fetching students data!" });
            }
        }

        [HttpGet]
        [Route("student/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                // check if provided id id not 0 or negative
                if (id <= 0)
                    return BadRequest(new { message = "Invalid student ID." });

                var student = await _studentService.GetByIdAsync(id);
                if (student == null)
                    return NotFound(new { message = "Student not found!" });
                return Ok(student);
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = "An error occure while fetching student data!" });
            }
        }

        [HttpPost]
        [Route("student")]
        public async Task<IActionResult> Create([FromBody] StudentCreateUpdateDto dto)
        {
            try
            {
                // check reqiured fields
                if (dto == null)
                    return BadRequest(new { message = "Student data is required." });
                if (string.IsNullOrWhiteSpace(dto.FirstName) || string.IsNullOrWhiteSpace(dto.LastName))
                    return BadRequest(new { message = "First name and Last name are required." });
                if (dto.RollNumber <= 0)
                    return BadRequest(new { message = "Please add valid Roll number" });
                if (string.IsNullOrWhiteSpace(dto.Email))
                    return BadRequest(new { message = "Email is required." });

                string result = await _studentService.CreateAsync(dto);
                if(!string.IsNullOrEmpty(result)) 
                    return BadRequest(new { message = result });

                return Ok(new { message = "Student created successfully!" });
            }
            catch(Exception ex)
            {
                // log error
                return BadRequest(new { message = "An error occure while saving student data!" });
            }
        }

        [HttpPut]
        [Route("student/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StudentCreateUpdateDto dto)
        {
            try
            {
                // check reqiured fields
                if (id <= 0)
                    return BadRequest(new { message = "Invalid student ID." });
                if (dto == null)
                    return BadRequest(new { message = "Student data is required." });
                if (string.IsNullOrWhiteSpace(dto.FirstName) || string.IsNullOrWhiteSpace(dto.LastName))
                    return BadRequest(new { message = "First name and Last name are required." });
                if (dto.RollNumber <= 0)
                    return BadRequest(new { message = "Please add valid Roll number" });
                if (string.IsNullOrWhiteSpace(dto.Email))
                    return BadRequest(new { message = "Email is required." });

                string result = await _studentService.UpdateAsync(id, dto);
                if (!string.IsNullOrEmpty(result))
                    return BadRequest(new { message = result });

                return Ok(new { message = "Student data updated successfully!" });
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = "An error occure while updating student data!" });
            }
        }

        [HttpDelete]
        [Route("student/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { message = "Invalid student ID." });

                var deleted = await _studentService.DeleteAsync(id);
                if (!deleted)
                    return NotFound(new { message = "Student not found!" });

                return Ok(new { message = "Student deleted successfully!" });

            }
            catch(Exception ex)
            {
                return BadRequest(new { message = "An error occure while deleting a record!" });
            }
        }
    }
}
