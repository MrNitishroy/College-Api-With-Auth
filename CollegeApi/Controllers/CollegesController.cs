using CollegeApi.Models;
using CollegeApi.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApi.Controllers
{
    [Route("api/colleges")]
    [ApiController]
    public class CollegesController : ControllerBase
    {
        private readonly ICollegeService collegeService;
        public CollegesController(ICollegeService collegeService)
        {
            this.collegeService = collegeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetColleges()
        {
            var colleges = await collegeService.GetColleges();
            return Ok(colleges);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCollegeById(string id)
        {
            var college = await collegeService.GetCollegeById(id);
            return Ok(college);
        }
        [HttpGet("filter")]
        public async Task<IActionResult> GetCollegeByFilter([FromQuery] string? name, [FromQuery] string? city, [FromQuery] string? number)
        {
            var colleges = await collegeService.GetCollegeByFilter(name, city, number);
            return Ok(colleges);
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateCollege([FromBody] College college)
        {
            var newCollege = await collegeService.CreateCollege(college);
            return Ok(newCollege);
        }
        [HttpPost("create-bulk")]
        public async Task<IActionResult> AddBulkCollege([FromBody] List<College> colleges)
        {
            var newColleges = await collegeService.AddBulkCollege(colleges);
            return Ok(newColleges);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCollege(string id, [FromBody] College college)
        {
            var updatedCollege = await collegeService.UpdateCollege(id, college);
            return Ok(updatedCollege);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollege(string id)
        {
            var deletedCollege = await collegeService.DeleteCollege(id);
            return Ok(deletedCollege);
        }

    }
}
