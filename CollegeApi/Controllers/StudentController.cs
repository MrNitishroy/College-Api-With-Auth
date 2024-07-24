using CollegeApi.Models;
using CollegeApi.Models.Request;
using CollegeApi.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService studentService;
        private readonly ICollegeService collegeService;

        public StudentController(IStudentService studentService,ICollegeService collegeService)
        {
            this.studentService = studentService;
            this.collegeService = collegeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await studentService.GetStudents();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(string id)
        {
            var student = await studentService.GetStudentById(id);
            return Ok(student);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetStudentByFilter([FromQuery] string? name, [FromQuery] int? age, [FromQuery] string? city, [FromQuery] string? number)
        {
            var students = await studentService.GetStudentByFilter(name, age, city, number);
            return Ok(students);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateStudent([FromBody] StudentRequest student)
        {
            var newStudent = await studentService.CreateStudent(student);
            if(collegeService.GetCollegeById(student.CollegeId) == null)
            {
                return BadRequest("College not found");
            }
            await collegeService.IncreaseStudentCount(student.CollegeId);
            return Ok(newStudent);
        }
        [HttpPost("create-bulk")]
        public async Task<IActionResult> AddBulkStudent([FromBody] List<StudentRequest> students)
        {
            var newStudents = await studentService.AddBulkStudent(students);
            return Ok(newStudents);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(string id, [FromBody] Student student)
        {
            var updatedStudent = await studentService.UpdateStudent(id, student);
            return Ok(updatedStudent);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            var student = await studentService.GetStudentById(id);
            var deletedStudent = await studentService.DeleteStudent(id);
            await collegeService.DecreaseStudentCount(student.CollegeId);
            return Ok(deletedStudent);
        }

    }
}
