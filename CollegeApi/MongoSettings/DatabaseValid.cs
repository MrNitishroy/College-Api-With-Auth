using CollegeApi.Models;
using MongoDB.Driver;

namespace CollegeApi.MongoSettings
{
    public class DatabaseValid
    {
        private readonly IMongoDatabase _database;
        public DatabaseValid(IMongoDatabase database) {
                        _database = database;
        }



        public void Initialize()
        {
            var colleges = _database.GetCollection<College>("Colleges");
            var students= _database.GetCollection<Student>("Students");
            var users= _database.GetCollection<User>("Users");

            var indexKeyDefination = Builders<College>.IndexKeys.Ascending(item => item.Email);
            var indexOptions = new CreateIndexOptions { Unique = true };
            var indexModel = new CreateIndexModel<College>(indexKeyDefination, indexOptions);
            colleges.Indexes.CreateOne(indexModel);

            var  studentIndexKeyDefination = Builders<Student>.IndexKeys.Ascending(item => item.Email );
            var studentIndexOptions = new CreateIndexOptions { Unique = true };
            var studentIndexModel = new CreateIndexModel<Student>(studentIndexKeyDefination, studentIndexOptions);
            students.Indexes.CreateOne(studentIndexModel);
        }   
    }
}
