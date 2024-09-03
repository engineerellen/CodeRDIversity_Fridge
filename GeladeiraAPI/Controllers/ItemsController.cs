using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace GeladeiraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {

        private readonly IItemService _service;
        public ItemsController(IItemService service)
        {
            _service = service;
        }

        [HttpGet("RetornarItens")]
        public async Task<ActionResult<List<Item>>> RetornarItens()
        {
            try
            {
                return Ok(await _service.RetornarItensAsync());
            }
            catch (Exception ex)
            {

                return  BadRequest(ex.Message);
            }

        }

        [HttpGet("{ItemID}")]
        public IActionResult GetItemById(int ItemID)
        {
            try
            {
                var item = _service.ObterItemPorID(ItemID);

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
        public async Task<IActionResult> InserirItem([FromBody] Item novoITem)
        {
            try
            {
                await _service.InserirItemAsync(novoITem);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPatch("AtualizarItem")]
        public async Task<IActionResult> AtualizarItem([FromBody] Item item)
        {
            try
            {
               await _service.AtualizarItemAsync(item);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!_service.ItemExistente(item.IdItem))
                    return NotFound();
                else
                    return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("{idItem}")]
        public async Task<IActionResult> DeleteItem(int idItem)
        {
            try
            {
                var item = _service.ObterItemPorID(idItem);

                if (item == null)
                    return NotFound();

                await _service.RetirarItemPorIDAsync(idItem);

                return Ok("Item retirado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost("AdicionarItens")]
        public async Task<IActionResult> AdicionarItens([FromBody] List<Item> items)
        {
            try
            {
                return Ok(await _service.AdicionarItensNaGeladeiraAsync(items));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("EsvaziarContainer")]
        public async Task<IActionResult> EsvaziarContainer(int numAndar, int numContainer)
        {
            try
            {
                return Ok(await _service.EsvaziarContainerAsync(numAndar, numContainer));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpHead]
        public async Task<IActionResult> CheckStatusGeladeiraAsync()
        {
            List<Item> Items = await _service.RetornarItensAsync();

            Response.Headers.Append("X-Total-Count", Items.Count.ToString());
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