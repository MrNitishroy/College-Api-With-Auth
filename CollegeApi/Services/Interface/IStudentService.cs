using CollegeApi.Models;

namespace CollegeApi.Services.Interface
{
    public interface IStudentService
    {
        public Task<List<Student>> GetStudents();
        public Task<Student> GetStudentById(string id);
        public Task<List<Student>> GetStudentsByName(string name);
        public Task<List<Student>> GetStudentByFilter(string? name, int? age, string? city, string? number);
        public Task<Student> CreateStudent(Student student);
        public Task<List<Student>> AddBulkStudent(List<Student> students);
        public Task<Student> UpdateStudent(string id, Student student);
        public Task<Student> DeleteStudent(string id);
    }
}
