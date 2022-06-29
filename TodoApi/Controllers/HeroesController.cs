using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HeroApi.Models;

namespace HeroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        private readonly HeroContext _context;

        public HeroesController(HeroContext context)
        {
            _context = context;
        }

        // GET: api/Heroes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HeroModel>>> GetHeroes()
        {
            if (_context.Heroes == null)
            {
                return NotFound();
            }
            return await _context.Heroes.ToListAsync();
        }

        // GET: api/Heroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HeroModel>> GetHero(int id)
        {
            if (_context.Heroes == null)
            {
                return NotFound();
            }
            var hero = await _context.Heroes.FindAsync(id);

            if (hero == null)
            {
                return NotFound();
            }

            return hero;
        }

        // PUT: api/Heroes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> EditHero(int id, HeroModel hero)
        {
            if (id != hero.Id)
            {
                return BadRequest();
            }

            _context.Entry(hero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HeroExists(id))
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

        // POST: api/Heroes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HeroModel>> PostTodoItem(HeroModel hero)
        {
            if (_context.Heroes == null)
            {
                return Problem("Entity set 'HeroContext.Heroes'  is null.");
            }
            _context.Heroes.Add(hero);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetHero", new { id = hero.Id }, hero);
            return CreatedAtAction(nameof(GetHero), new { id = hero.Id }, hero);
        }

        // DELETE: api/Heroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHero(int id)
        {
            if (_context.Heroes == null)
            {
                return NotFound();
            }
            var hero = await _context.Heroes.FindAsync(id);
            if (hero == null)
            {
                return NotFound();
            }

            _context.Heroes.Remove(hero);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HeroExists(long id)
        {
            return (_context.Heroes?.Any(h => h.Id == id)).GetValueOrDefault();
        }

        private static HeroDTO ItemToDTO(HeroModel hero) =>
             new HeroDTO
             {
                 Id = hero.Id,
                 FirstName = hero.firstName,
                 LastName = hero.lastName,
                 Description =hero.description
             };
    }
}
