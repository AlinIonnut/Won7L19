using System.ComponentModel.DataAnnotations;

namespace Won7E1.DTOs
{
    public class StudentsOrderByGrades
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }

        [Range(0, int.MaxValue)]
        public int Age { get; set; }
        public double AverageMarks {  get; set; }
    }
}
