namespace Won7E1.Models
{
    public class Subject
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public List<Mark> Mark {  get; set; }
    }
}
