namespace KafkaModels.Models.Customer
{
    public class Address
    {
        public int? AddressId { get; set; }
        public string Street1 { get; set; }

        public string Street2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }
    }
}
