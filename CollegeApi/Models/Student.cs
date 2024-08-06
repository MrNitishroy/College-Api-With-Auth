using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CollegeApi.Models
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [Phone]
        public string Number { get; set; }

        [Required]
        [Range(1, 120, ErrorMessage = "Age must be between 1 and 120")]
        public int Age { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        public string PostalCode { get; set; }

        [Required]
        public string EnrollmentNumber { get; set; }

        
        [Required]
        public string CollegeId { get; set; }

        [Range(0, 4.0, ErrorMessage = "GPA must be between 0 and 4.0")]
        public double GPA { get; set; }

        public DateTime DateOfBirth { get; set; }

       
      
    }
}
