using CollegeApi.Models;
using CollegeApi.Models.Request;
using CollegeApi.MongoSettings;
using CollegeApi.Services.Interface;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.RegularExpressions;

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
        public async Task<Student> CreateStudent(StudentRequest studentReq)
        {
            var enrollmentNumber = await GenerateEnrollmentNumber(studentReq.Name);
            var student = new Student
            {
                Name = studentReq.Name,
                Age = studentReq.Age,
                City = studentReq.City,
                CollegeId = studentReq.CollegeId,
                EnrollmentNumber = enrollmentNumber,
                Address = studentReq.Address,
                Country = studentReq.Country,
                Email = studentReq.Email,
                Number = studentReq.Number,
                PostalCode = studentReq.PostalCode

            };
            await _students.InsertOneAsync(student);
            return student;
        }
        //public async Task<List<Student>> AddBulkStudent(List<Student> students)
        //{
        //    await _students.InsertManyAsync(students);
        //    return students;
        //}
        public async Task<List<Student>> AddBulkStudent(List<StudentRequest> studentRequests)
        {
            var students = new List<Student>();

            foreach (var studentReq in studentRequests)
            {
                try
                {
                    var enrollmentNumber = await GenerateEnrollmentNumber(studentReq.Name);
                    var student = new Student
                    {
                        Name = studentReq.Name,
                        Age = studentReq.Age,
                        City = studentReq.City,
                        CollegeId = studentReq.CollegeId,
                        EnrollmentNumber = enrollmentNumber,
                        Address = studentReq.Address,
                        Country = studentReq.Country,
                        Email = studentReq.Email,
                        Number = studentReq.Number,
                        PostalCode = studentReq.PostalCode
                    };
                    students.Add(student);
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it as needed
                    // For example: _logger.LogError(ex, "Failed to process student request.");
                }
            }
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
        private async Task<string> GenerateEnrollmentNumber(string name)
        {
            // Extract initials from the name
            string initials = new string(name.Split(' ').Select(n => n[0]).ToArray()).ToUpper();

            // Find the current highest order number for students with the same initials
            var filter = Builders<Student>.Filter.Regex("EnrollmentNumber", new BsonRegularExpression($"^{initials}"));
            var lastStudentWithInitials = await _students.Find(filter)
                                                         .SortByDescending(s => s.EnrollmentNumber)
                                                         .FirstOrDefaultAsync();

            int orderNumber = 1;
            if (lastStudentWithInitials != null)
            {
                string lastEnrollmentNumber = lastStudentWithInitials.EnrollmentNumber;
                string orderNumberStr = Regex.Match(lastEnrollmentNumber, @"\d+$").Value;
                orderNumber = int.Parse(orderNumberStr) + 1;
            }

            // Generate new enrollment number
            return $"{initials}{orderNumber:D2}";
        }
    }
}
