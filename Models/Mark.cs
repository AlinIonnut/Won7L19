namespace Won7E1.Models
{
    public class Mark
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTime DateAssigned { get; set; }
        public int? StudentId { get; set; }
        public Student Student { get; set; }

        public int? SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
