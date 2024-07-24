using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CollegeApi.Models
{
    public class College
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        [Required]
       
        public string PostalCode { get; set; }

        [Required]
        [Phone]
        public string Number { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Url]
        public string Website { get; set; }

        public int EstablishedYear { get; set; }

        public int NumberOfStudents { get; set; }

        public string Description { get; set; }
        public int StudentCount { get; internal set; }
    }
}
