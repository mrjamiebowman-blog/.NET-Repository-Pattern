using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;

namespace MrJB.KafkaModels.Models.Customer
{
    public class Customer
    {
        [BsonIgnore]
        public dynamic Id { get; set; }

        [Key]
        [BsonIgnore]
        public int? CustomerId { get; set; }

        [BsonId(IdGenerator = typeof(BsonObjectIdGenerator))]
        public ObjectId CustomerIdMg { get; set; }


        [Required]
        [BsonRequired]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [BsonRequired]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [BsonRequired]
        [BsonDateTimeOptions(DateOnly = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Birthdate")]
        public DateTime Birthdate { get; set; }

        [Required]
        [BsonRequired]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Billing Address")]
        public Address BillingAddress { get; set; }

        [Display(Name = "Shipping Address")]
        public Address ShippingAddress { get; set; }

        [BsonIgnore]
        public int? Age { get; set; }
    }
}
