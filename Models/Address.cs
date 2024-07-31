using System.Text.Json.Serialization;

namespace Won7E1.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string City {  get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        [JsonIgnore]
        public List<Student> Student { get; set; }
    }
}
