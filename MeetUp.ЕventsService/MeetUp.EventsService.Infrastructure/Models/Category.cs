namespace MeetUp.EventsService.Infrastructure.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<Event> Events { get; set;}
    }
}
