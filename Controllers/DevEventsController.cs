using minapi.Entities;
using minapi.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Newtonsoft.Json;

namespace minapi.Controllers
{
    [Route("api/dev-events")]
    [ApiController]
    public class DevEventsController : ControllerBase
    {
        private void print_r(object obj)
        {
            Console.WriteLine(JsonConvert.SerializeObject(obj, Formatting.Indented));
        }
        private void VarDump(object obj, int level = 0)
        {
            Console.WriteLine(new string(' ', level * 4) + obj.GetType().Name + ": " + obj);

            if (obj is IEnumerable enumerable)
            {
                foreach (var item in enumerable)
                {
                    VarDump(item, level + 1);
                }
            }
        }
        private readonly DevEventsDbContext _context;
        public DevEventsController(DevEventsDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var devEvents = _context.DevEvents.Where(d => !d.IsDeleted).ToList();

            return Ok(devEvents);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            // var devEvent = _context.DevEvents.SingleOrDefault(d => d.Id == id);
            // Comentei o código acima e troquei para Where para poder trazer mais de um resultado.
            var devEvent = _context.DevEvents.Where(d => d.Id == id);

            // Teste de fazer print_r. Não é exatamente o print_r do PHP porém me retorna
            // o conteúdo da variável em formato JSON, já indentado, o que é muito bom pois
            // já dá para ter uma ideia do conteúdo e do tipo. A função está declarada nesse
            // mesmo controller. Não é uma boa prática mas isso é apenas para fins de estudo.
            if (devEvent is IEnumerable)
            {
                this.print_r(new[] { new { Nome = "Diogo", Idade = 32 }, new { Nome = "Bruna", Idade = 30 } });
                this.print_r("Esse é um texto muito do bom.");
                this.print_r(new[] { "A", "B", "C" });
                this.print_r(new List<dynamic> { "sim", "isso", new[] { 12, 13 } });
                this.print_r(devEvent);
                this.VarDump(new List<dynamic> { 1, 2, new[] { "A", "B", "C" }, 4 });
                this.VarDump(new[] { new { Nome = "Diogo", Idade = 32 }, new { Nome = "Bruna", Idade = 30 } });
            }

            if (devEvent == null)
                return NotFound();
            
            return Ok(devEvent);
        }

        [HttpPost]
        public IActionResult Post(DevEvent devEvent)
        {
            var isThereAlready = _context.DevEvents.SingleOrDefault(d => d.Id == devEvent.Id);
            if (isThereAlready != null)
                return BadRequest("Já existe um registro com esse id.");
            
            _context.DevEvents.Add(devEvent);

            return CreatedAtAction(nameof(GetById), new { id = devEvent.Id }, devEvent);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, DevEvent input)
        {
            var devEvent = _context.DevEvents.SingleOrDefault(d => d.Id == id);

            if (devEvent == null)
                return NotFound();
            
            devEvent.Update(input.Title, input.Description, input.StartDate, input.EndDate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var devEvent = _context.DevEvents.SingleOrDefault(d => d.Id == id);

            if (devEvent == null)
                return NotFound();
            
            devEvent.Delete();

            return NoContent();
        }

        [HttpPost("{id}/speakers")]
        public IActionResult PostSpeaker(Guid id, DevEventSpeaker speaker)
        {
            var devEvent = _context.DevEvents.SingleOrDefault(d => d.Id == id);

            if (devEvent == null)
                return NotFound();
            
            devEvent.Speakers.Add(speaker);

            return NoContent();
        }
    }
}