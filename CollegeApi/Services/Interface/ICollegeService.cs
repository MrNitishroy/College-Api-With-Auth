using CollegeApi.Models;

namespace CollegeApi.Services.Interface
{
    public interface ICollegeService
    {
        public Task<List<College>> GetColleges();
        public Task<College> GetCollegeById(string id);
        public Task<List<College>> GetCollegesByName(string name);
        public Task<List<College>> GetCollegeByFilter(string? name, string? city, string? number);
        public Task<College> CreateCollege(College college);
        public Task<List<College>> AddBulkCollege(List<College> colleges);
        public Task<College> UpdateCollege(string id, College college);
        public Task<College> DeleteCollege(string id);
        public Task IncreaseStudentCount(string collegeId);
        public Task DecreaseStudentCount(string collegeId);
    }
}
