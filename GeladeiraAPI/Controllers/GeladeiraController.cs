using Domain;
using GeladeiraAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using Services;

namespace GeladeiraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeladeiraController : ControllerBase
    {
        GeladeiraService _service;

        private readonly GeladeiraContext _context;
        public GeladeiraController(GeladeiraContext context)
        {
            _context = context;

            _service = new GeladeiraService(_context);

         }

        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            var contents = await _context.Items.ToListAsync();
            return Ok(contents);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemById(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] Item newItem)
        {
            try
            {
                _context.Items.Add(newItem);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetItemById), new { id = newItem.IdItem }, newItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] Item updatedItem)
        {
            if (id != updatedItem.IdItem)
                return BadRequest();

            _context.Entry(updatedItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!_context.Items.Any(e => e.IdItem == id))
                    return NotFound();
                else
                    return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
                return NotFound();

            _context.Items.Remove(item);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}