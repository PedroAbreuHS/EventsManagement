

namespace EventsManagement.Presentation.Front.DTO
{
    public class EventDto
    {
        public EventDto() { }
        public EventDto(string title, string description, DateTime start, DateTime? end, bool allDay)
        {
            Title = title;
            Description = description;
            Start = start;
            End = end;
            AllDay = allDay;
        }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public bool AllDay { get; set; }
    }
}
