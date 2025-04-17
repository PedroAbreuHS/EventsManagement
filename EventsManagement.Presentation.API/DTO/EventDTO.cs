using System.ComponentModel.DataAnnotations;

namespace EventsManagement.Presentation.API.DTO
{
    public class EventDTO
    {
        protected EventDTO()
        {
        }

        public EventDTO(string title, string description, DateTime start, DateTime? end, bool allDay)
        {
            Title = title;
            Description = description;
            Start = start;
            End = end;
            AllDay = allDay;            
        }

        [Required(ErrorMessage = "Título é obrigatório")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500,ErrorMessage = "O campo descrição suporta apenas 500 caracteres")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de início é obrigatório")]
        public DateTime Start { get; set; }

        public DateTime? End { get; set; }

        public bool AllDay { get; set; }
    }
}
