namespace TFBackend.Entities.Dto.Location
{
    public class LocationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //one-to-many relationship - Project
        //public ICollection<BBProject>? Projects { get; set; }
    }
}
