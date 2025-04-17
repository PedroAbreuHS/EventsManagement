using EventsManagement.Presentation.API.Data;
using EventsManagement.Presentation.API.DTO;
using EventsManagement.Presentation.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace EventsManagement.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Event>>> GetAllEvents()
        {
            var events = await _context.Events
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            return Ok(events);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetById(int id)
        {
            var evento = await _context.Events
                .FirstOrDefaultAsync(x => x.Id == id);

            if (evento == null)
            {
                return NotFound();
            }
                
            return Ok(evento);            
        }

        [HttpPost]
        public async Task<ActionResult<Event>> CreateEvent(EventDTO eventdto)
        {
            var novoEvento = new Event
            {
                Title = eventdto.Title,
                Description = eventdto.Description,
                Start = eventdto.Start,
                End = eventdto.End,
                AllDay = eventdto.AllDay,          
            };

            await _context.Events.AddAsync(novoEvento);
            await _context.SaveChangesAsync();

            return Ok(novoEvento);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Event>> EditEvent(int id, EventDTO eventdto)
        {
            var eventoAntigo = await _context.Events.FirstOrDefaultAsync(x => x.Id == id);
            if (eventoAntigo == null)
                return NotFound();
            
            eventoAntigo.Title = eventdto.Title;
            eventoAntigo.Description = eventdto.Description;
            eventoAntigo.Start = eventdto.Start;
            eventoAntigo.End = eventdto.End;
            eventoAntigo.AllDay = eventdto.AllDay;

            await _context.SaveChangesAsync();

            return Ok(eventoAntigo);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Event>> RemoveEvent(int id)
        {
            var evento = await _context.Events.FirstOrDefaultAsync(x => x.Id == id);

            if(evento == null)
                return NotFound();

            _context.Events.Remove(evento);
            await _context.SaveChangesAsync();

            return Ok(evento);
        }
    }
}
