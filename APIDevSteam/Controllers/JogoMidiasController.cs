using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDevSteam.Data;
using APIDevSteam.Models;

namespace APIDevSteam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogoMidiasController : ControllerBase
    {
        private readonly APIContext _context;

        public JogoMidiasController(APIContext context)
        {
            _context = context;
        }

        // GET: api/JogoMidias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoMidia>>> GetJogosMidia()
        {
            return await _context.JogosMidia.ToListAsync();
        }

        // GET: api/JogoMidias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JogoMidia>> GetJogoMidia(string id)
        {
            var jogoMidia = await _context.JogosMidia.FindAsync(id);

            if (jogoMidia == null)
            {
                return NotFound();
            }

            return jogoMidia;
        }

        // PUT: api/JogoMidias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJogoMidia(string id, JogoMidia jogoMidia)
        {
            if (id != jogoMidia.JogoMidiaId)
            {
                return BadRequest();
            }

            _context.Entry(jogoMidia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JogoMidiaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/JogoMidias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JogoMidia>> PostJogoMidia(JogoMidia jogoMidia)
        {
            _context.JogosMidia.Add(jogoMidia);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (JogoMidiaExists(jogoMidia.JogoMidiaId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetJogoMidia", new { id = jogoMidia.JogoMidiaId }, jogoMidia);
        }

        // DELETE: api/JogoMidias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJogoMidia(string id)
        {
            var jogoMidia = await _context.JogosMidia.FindAsync(id);
            if (jogoMidia == null)
            {
                return NotFound();
            }

            _context.JogosMidia.Remove(jogoMidia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JogoMidiaExists(string id)
        {
            return _context.JogosMidia.Any(e => e.JogoMidiaId == id);
        }
    }
}
