using LibraryBackend_CleanArchitecture.Model;
using LibraryBackend_CleanArchitecture.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBackend_CleanArchitecture.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
                return NotFound("Student not found");

            return Ok(student);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search(string? searchText, string? memberType, string? status)
        {
            var result = await _studentService.SearchStudentsAsync(searchText ?? "", memberType ?? "", status ?? "");
            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(Student student)
        {
            await _studentService.AddStudentAsync(student);
            return Ok("Student added successfully");
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(Student student)
        {
            await _studentService.UpdateStudentAsync(student);
            return Ok("Student updated successfully");
        }
    }
}
