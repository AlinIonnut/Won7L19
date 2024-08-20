using System.ComponentModel.DataAnnotations;

namespace Won7E1.DTOs
{
    public class MarkDto
    {
        [Range(1, 10, ErrorMessage = "Value must be between 1 and 10.")]
        public int Value { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
    }
}
