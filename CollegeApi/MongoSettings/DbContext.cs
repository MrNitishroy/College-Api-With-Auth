using CollegeApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CollegeApi.MongoSettings
{
    public class DbContext
    {
        private readonly IMongoCollection<Student> _students;
        private readonly IMongoCollection<College> _colleges;
        private readonly IMongoCollection<User> _users;
        private readonly string authKey;
        public DbContext(IOptions<DbModel> dbModel,IConfiguration configuration)
        {
            var clientSettings = MongoClientSettings.FromConnectionString(dbModel.Value.ConnectionString);
            var client = new MongoClient(clientSettings);
            var database = client.GetDatabase(dbModel.Value.Database);

            _students = database.GetCollection<Student>(dbModel.Value.StudentsDb);
            _colleges = database.GetCollection<College>(dbModel.Value.CollegesDb);
            _users = database.GetCollection<User>(dbModel.Value.UsersDb);
            authKey = configuration.GetSection("JwtConfig").ToString();
        }

        public IMongoCollection<Student> Students => _students;
        public IMongoCollection<College> Colleges => _colleges;
        public IMongoCollection<User> Users => _users;
    }
}
