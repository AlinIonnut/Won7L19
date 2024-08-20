using System.ComponentModel.DataAnnotations;

namespace Won7E1.DTOs
{
    public class MarksFromASubject
    {
        public int? Id { get; set; }

        [Range(1, 10, ErrorMessage = "Value must be between 1 and 10.")]
        public int Value { get; set; }
        public DateTime DataAssigned { get; set; }
        public string Subject { get; set; }
    }
}
