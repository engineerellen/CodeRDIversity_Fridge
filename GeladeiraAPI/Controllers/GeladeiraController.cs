using Domain;
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

        [HttpGet("RetornarItens")]
        public ActionResult<List<Item>> RetornarItens()
        {
            try
            {
                return Ok(_service.RetornarItens());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{ItemID}")]
        public IActionResult GetItemById(int ItemID)
        {
            try
            {
                var item = _service.GetItemById(ItemID);

                if (item == null)
                    return NotFound();

                return Ok(item);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost("InserirItem")]
        public IActionResult InserirItem([FromBody] Item novoITem)
        {
            try
            {
                _service.InserirItem(novoITem);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPatch("AtualizarItem")]
        public IActionResult AtualizarItem( [FromBody] Item item)
        {
            try
            {
                _service.AtualizarItem(item);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!_context.Items.Any(e => e.IdItem == item.IdItem))
                    return NotFound();
                else
                    return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("{idItem}")]
        public IActionResult DeleteItem(int idItem)
        {
            try
            {
                var item = _service.GetItemById(idItem);

                if (item == null)
                    return NotFound();

                _service.RetirarItemPorID(idItem);

                return Ok("Item retirado com sucesso!");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }


        [HttpPost("AdicionarItens")]
        public IActionResult AdicionarItens([FromBody] List<Item> items)
        {
            try
            {
                return Ok(_service.AdicionarItensNaGeladeira(items));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("EsvaziarContainer")]
        public IActionResult EsvaziarContainer(int numAndar, int numContainer)
        {
            try
            {
                return Ok(_service.EsvaziarContainer(numAndar, numContainer));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpHead]
        public IActionResult CheckStatusGeladeira()
        {
            var count = _context.Items.Count();
            Response.Headers.Append("X-Total-Count", count.ToString());
            return Ok();
        }

        [HttpOptions]
        public IActionResult GetOptions()
        {
            Response.Headers.Append("Allow", "GET,POST,PUT,PATCH,DELETE,HEAD,OPTIONS");
            return Ok();
        }
    }
}