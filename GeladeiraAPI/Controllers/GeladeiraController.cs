using Domain;
using GeladeiraAPI.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GeladeiraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeladeiraController : ControllerBase
    {
        private List<Item> Itens;
        private Geladeira objGeladeira;


        public GeladeiraController()
        {
            //Conceito de pilha- Stack

            objGeladeira = new Geladeira();

            var objItemMaca = new Item() { Descricao = "Maçã", Quantidade = 1, UnidadeQtd = "Unidade", Classificacao = "Fruta", ID = 1 };
            var objItemBanana = new Item() { Descricao = "Banana", Quantidade = 1, UnidadeQtd = "Cacho", Classificacao = "Fruta", ID = 2 };
            var objItemLaranja = new Item() { Descricao = "Laranja", Quantidade = 1, UnidadeQtd = "Duzia", Classificacao = "Fruta", ID = 3 };

            var objItemLeite = new Item() { Descricao = "Leite", Quantidade = 1, UnidadeQtd = "Litro", Classificacao = "Laticínio", ID = 4 };
            var objItemQueijo = new Item() { Descricao = "Queijo", Quantidade = 1, UnidadeQtd = "Unidade", Classificacao = "Laticínio", ID = 5 };
            var objItemMilho = new Item() { Descricao = "Milho Enlatado", Quantidade = 1, UnidadeQtd = "Lata", Classificacao = "Enlatado", ID = 6 };

            var objItemPresunto = new Item() { Descricao = "Presunto", Quantidade = 100, UnidadeQtd = "Gramas", Classificacao = "Charcutaria", ID = 7 };
            var objItemOvos = new Item() { Descricao = "Ovos", Quantidade = 1, UnidadeQtd = "Duzia", Classificacao = "Ovo", ID = 8 };
            var objItemCarne = new Item() { Descricao = "Carne", Quantidade = 1, UnidadeQtd = "Kilo", Classificacao = "Carne", ID = 9 };

            Itens = new() { objItemMaca, objItemBanana, objItemLaranja, objItemLeite, objItemQueijo, objItemMilho, objItemPresunto, objItemOvos, objItemCarne };
        }

        [HttpGet]
        public ActionResult<IEnumerable<Item>> Get()
        {
            var mensagemGeladeiraVazia = "A geladeira está vazia!";

            if (Itens?.Count <= 0 )
                return NotFound(mensagemGeladeiraVazia);

            return Ok(Itens);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var retorno = Itens.Where(p => p.ID == id).FirstOrDefault() ?? new Item();

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