using CollegeApi.Models;
using CollegeApi.MongoSettings;
using CollegeApi.Services.Interface;
using MongoDB.Driver;

namespace CollegeApi.Services
{
    public class CollegeService : ICollegeService
    {
        private readonly IMongoCollection<College> _colleges;
        public CollegeService(DbContext context)
        {
            _colleges = context.Colleges;
        }
        public async Task<List<College>> GetColleges()
        {
            return await _colleges.Find(_ => true).ToListAsync();
        }
        public async Task<College> GetCollegeById(string id)
        {
            return await _colleges.Find(c => c.Id == id).FirstOrDefaultAsync();
        }
        public async Task<List<College>> GetCollegesByName(string name)
        {
            return await _colleges.Find(c => c.Name == name).ToListAsync();
        }
        public async Task<List<College>> GetCollegeByFilter(string? name, string? city, string? number)
        {
            var filter = Builders<College>.Filter.Empty;
            if (!string.IsNullOrEmpty(name))
            {
                filter = Builders<College>.Filter.Eq(c => c.Name, name);
            }
            if (!string.IsNullOrEmpty(city))
            {
                filter = Builders<College>.Filter.Eq(c => c.City, city);
            }
            if (!string.IsNullOrEmpty(number))
            {
                filter = Builders<College>.Filter.Eq(c => c.Number, number);
            }
            return await _colleges.Find(filter).ToListAsync();
        }
        public async Task<College> CreateCollege(College college)
        {
            await _colleges.InsertOneAsync(college);
            return college;
        }
        public async Task<List<College>> AddBulkCollege(List<College> colleges)
        {
            await _colleges.InsertManyAsync(colleges);
            return colleges;
        }
        public async Task<College> UpdateCollege(string id, College college)
        {
            await _colleges.ReplaceOneAsync(c => c.Id == id, college);
            return college;
        }
        public async Task<College> DeleteCollege(string id)
        {
            return await _colleges.FindOneAndDeleteAsync(c => c.Id == id);
        }


        public async Task IncreaseStudentCount(string collegeId)
        {
            var college = await _colleges.Find(c => c.Id == collegeId).FirstOrDefaultAsync();
            college.StudentCount++;
            await _colleges.ReplaceOneAsync(c => c.Id == collegeId, college);
        }
        public async Task DecreaseStudentCount(string collegeId)
        {
            var college = await _colleges.Find(c => c.Id == collegeId).FirstOrDefaultAsync();
            college.StudentCount--;
            await _colleges.ReplaceOneAsync(c => c.Id == collegeId, college);
        }
    }
}
