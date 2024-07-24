using CollegeApi.Models;
using CollegeApi.MongoSettings;
using CollegeApi.Services.Interface;
using MongoDB.Driver;

namespace CollegeApi.Services
{
    public class StudentService : IStudentService
    {
        private readonly IMongoCollection<Student> _students;
        public StudentService(DbContext dbContext)
        {
            _students = dbContext.Students;
        }
        public async Task<List<Student>> GetStudents()
        {
            return await _students.Find(_ => true).ToListAsync();
        }
        public async Task<Student> GetStudentById(string id)
        {
            return await _students.Find(s => s.Id == id).FirstOrDefaultAsync();
        }
        public async Task<List<Student>> GetStudentsByName(string name)
        {
            return await _students.Find(s => s.Name == name).ToListAsync();
        }
        public async Task<List<Student>> GetStudentByFilter(string? name, int? age, string? city, string? number)
        {
            var filter = Builders<Student>.Filter.Empty;
            if (!string.IsNullOrEmpty(name))
            {
                filter = Builders<Student>.Filter.Eq(s => s.Name, name);
            }
            if (age != 0)
            {
                filter = Builders<Student>.Filter.Eq(s => s.Age, age);
            }
            if (!string.IsNullOrEmpty(city))
            {
                filter = Builders<Student>.Filter.Eq(s => s.City, city);
            }
            if (!string.IsNullOrEmpty(number))
            {
                filter = Builders<Student>.Filter.Eq(s => s.Number, number);
            }
            return await _students.Find(filter).ToListAsync();
        }
        public async Task<Student> CreateStudent(Student student)
        {
            await _students.InsertOneAsync(student);
            return student;
        }
        public async Task<List<Student>> AddBulkStudent(List<Student> students)
        {
            await _students.InsertManyAsync(students);
            return students;
        }
        public async Task<Student> UpdateStudent(string id, Student student)
        {
            await _students.ReplaceOneAsync(s => s.Id == id, student);
            return student;
        }
        public async Task<Student> DeleteStudent(string id)
        {
            return await _students.FindOneAndDeleteAsync(s => s.Id == id);
        }

    }
}
