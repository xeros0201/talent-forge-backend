namespace TFBackend.Models
{
    public class Roll
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Staff>? Staffs { get; set;}
    }
}
