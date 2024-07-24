using CollegeApi.Models;
using CollegeApi.Models.Request;
using CollegeApi.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CollegeApi.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ICollegeService _collegeService;

        public StudentsController(IStudentService studentService, ICollegeService collegeService)
        {
            _studentService = studentService;
            _collegeService = collegeService;
        }

        /// <summary>
        /// Retrieves a list of all students.
        /// </summary>
        /// <returns>List of students.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Student>))]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _studentService.GetStudents();
            return Ok(students);
        }

        /// <summary>
        /// Retrieves a student by their unique identifier.
        /// </summary>
        /// <param name="id">The student's unique identifier.</param>
        /// <returns>The student with the specified id.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Student))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStudentById(string id)
        {
            var student = await _studentService.GetStudentById(id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            return Ok(student);
        }

        /// <summary>
        /// Retrieves students based on the provided filter criteria.
        /// </summary>
        /// <param name="name">The student's name.</param>
        /// <param name="age">The student's age.</param>
        /// <param name="city">The student's city.</param>
        /// <param name="number">The student's contact number.</param>
        /// <returns>List of students matching the filter criteria.</returns>
        [HttpGet("filter")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Student>))]
        public async Task<IActionResult> GetStudentByFilter([FromQuery] string? name, [FromQuery] int? age, [FromQuery] string? city, [FromQuery] string? number)
        {
            var students = await _studentService.GetStudentByFilter(name, age, city, number);
            return Ok(students);
        }

        /// <summary>
        /// Creates a new student.
        /// </summary>
        /// <param name="student">The student information to create.</param>
        /// <returns>The newly created student.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Student))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateStudent([FromBody] StudentRequest student)
        {
            var college = await _collegeService.GetCollegeById(student.CollegeId);
            if (college == null)
            {
                return BadRequest("College not found.");
            }

            var newStudent = await _studentService.CreateStudent(student);
            await _collegeService.IncreaseStudentCount(student.CollegeId);

            return CreatedAtAction(nameof(GetStudentById), new { id = newStudent.Id }, newStudent);
        }

        /// <summary>
        /// Creates multiple students in bulk.
        /// </summary>
        /// <param name="students">A list of students to create.</param>
        /// <returns>The list of newly created students.</returns>
        [HttpPost("bulk")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IEnumerable<Student>))]
        public async Task<IActionResult> AddBulkStudent([FromBody] List<StudentRequest> students)
        {
            var newStudents = await _studentService.AddBulkStudent(students);
            return CreatedAtAction(nameof(GetStudents), newStudents);
        }

        /// <summary>
        /// Updates an existing student's information.
        /// </summary>
        /// <param name="id">The student's unique identifier.</param>
        /// <param name="student">The updated student information.</param>
        /// <returns>The updated student.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Student))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStudent(string id, [FromBody] Student student)
        {
            var existingStudent = await _studentService.GetStudentById(id);
            if (existingStudent == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }

            var updatedStudent = await _studentService.UpdateStudent(id, student);
            return Ok(updatedStudent);
        }

        /// <summary>
        /// Deletes a student by their unique identifier.
        /// </summary>
        /// <param name="id">The student's unique identifier.</param>
        /// <returns>Confirmation of deletion.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Student))]
        [ProducesResponseType(StatusCodes.Status404NotFound,Type =typeof(string))]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            var student = await _studentService.GetStudentById(id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }

            var deletedStudent = await _studentService.DeleteStudent(id);
            await _collegeService.DecreaseStudentCount(student.CollegeId);

            return Ok(deletedStudent);
        }
    }
}
