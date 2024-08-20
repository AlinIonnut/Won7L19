using System.Text.Json.Serialization;

namespace Won7E1.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }
        public int? AddressId { get; set; }
        [JsonIgnore]
        public Address Address {  get; set; }

        public List<Mark> Mark { get; set; }
    }
}
