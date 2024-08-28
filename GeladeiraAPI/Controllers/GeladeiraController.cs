using Domain;
using GeladeiraAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Services;

namespace GeladeiraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeladeiraController : ControllerBase
    {
        private List<Domain.Item> Itens;
        private Domain.Geladeira objGeladeira;
        GeladeiraService _service;

        private readonly GeladeiraContext _context;
        public GeladeiraController(GeladeiraContext context)
        {
            _context = context;

            _service = new GeladeiraService(_context);
            //Conceito de pilha - Stack

            objGeladeira = new Domain.Geladeira();

            var objItemMaca = new Domain.Item() { Descricao = "Maçã", Quantidade = 1, UnidadeQtd = "Unidade", Classificacao = "Fruta", ID = 1 };
            var objItemBanana = new Domain.Item() { Descricao = "Banana", Quantidade = 1, UnidadeQtd = "Cacho", Classificacao = "Fruta", ID = 2 };
            var objItemLaranja = new Domain.Item() { Descricao = "Laranja", Quantidade = 1, UnidadeQtd = "Duzia", Classificacao = "Fruta", ID = 3 };

            var objItemLeite = new Domain.Item() { Descricao = "Leite", Quantidade = 1, UnidadeQtd = "Litro", Classificacao = "Laticínio", ID = 4 };
            var objItemQueijo = new Domain.Item() { Descricao = "Queijo", Quantidade = 1, UnidadeQtd = "Unidade", Classificacao = "Laticínio", ID = 5 };
            var objItemMilho = new Domain.Item() { Descricao = "Milho Enlatado", Quantidade = 1, UnidadeQtd = "Lata", Classificacao = "Enlatado", ID = 6 };

            var objItemPresunto = new Domain.Item() { Descricao = "Presunto", Quantidade = 100, UnidadeQtd = "Gramas", Classificacao = "Charcutaria", ID = 7 };
            var objItemOvos = new Domain.Item() { Descricao = "Ovos", Quantidade = 1, UnidadeQtd = "Duzia", Classificacao = "Ovo", ID = 8 };
            var objItemCarne = new Domain.Item() { Descricao = "Carne", Quantidade = 1, UnidadeQtd = "Kilo", Classificacao = "Carne", ID = 9 };

            Itens = new() { objItemMaca, objItemBanana, objItemLaranja, objItemLeite, objItemQueijo, objItemMilho, objItemPresunto, objItemOvos, objItemCarne };
        }

        [HttpGet]
        public ActionResult<IEnumerable<Repository.Models.Item>> Get()=>
             _service.GetAllItems();



        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var retorno = Itens.Where(p => p.ID == id).FirstOrDefault() ?? new Domain.Item();

            if (retorno is null)
                return NotFound("A geladeira está vazia!");

            return Ok(retorno);
        }

        [HttpPost]
        public IActionResult Post(int posicaoContainer, [FromBody] ItemDTO item)
        {
            try
            {
                objGeladeira.AdicionarItem(item.NumAndar, item.NumContainer, posicaoContainer, item.Item);
                return CreatedAtAction(nameof(Get), new { id = item.Item.ID }, item.Item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("AddItensAoContainer")]
        public IActionResult AddItensAoContainer([FromBody] ContainerDTO container)
        {
            try
            {
                objGeladeira.AddItensAoContainer(container.NumAndar, container.NumContainer, Itens);
                return Ok("itens adicionado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut("{posicao}")]
        public IActionResult Put(int posicao, [FromBody] ItemDTO item)
        {
            try
            {
                objGeladeira.RemoverItem(item.NumAndar, item.NumContainer, posicao);
                objGeladeira.AdicionarItem(item.NumAndar, item.NumContainer, posicao, item.Item);

                return Ok($"Posição {posicao} alterada com sucesso no andar {item.NumAndar}. Item {item.Item.Descricao} adicionado com sucesso! ");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{posicao}")]
        public IActionResult Delete(int posicao, [FromBody] ContainerDTO container)
        {
            try
            {
                objGeladeira.RemoverItem(container.NumAndar, container.NumContainer, posicao);
                return Ok($"Item da posição {posicao} do andar {container.NumAndar} container {container.NumContainer} removido com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpDelete("LimparContainer")]
        public IActionResult LimparContainer([FromBody] ContainerDTO container)
        {
            try
            {
                objGeladeira.LimparContainer(container.NumAndar, container.NumContainer);
                return Ok($"Container {container.NumContainer} do andar {container.NumAndar} limpo com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}