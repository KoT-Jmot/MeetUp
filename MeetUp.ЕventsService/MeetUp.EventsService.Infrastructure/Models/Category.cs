namespace MeetUp.EventsService.Infrastructure.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public virtual IEnumerable<Event> Events { get; set;}
    }
}
