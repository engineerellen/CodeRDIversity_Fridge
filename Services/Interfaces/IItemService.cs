using Domain.Models;

namespace Services.Interfaces
{
    public interface IItemService
    {
        Task<List<Item>> RetornarItensAsync();
        Item? ObterItemPorID(int id);
        Task<string> InserirItemAsync(Item item);
        Task<string> AtualizarItemAsync(Item item);
        Task<string> RetirarItemPorIDAsync(int id);
        Task<string> EsvaziarContainerAsync(int numAndar, int numContainer);
        Task<string> AdicionarItensNaGeladeiraAsync(List<Item> itens);
        bool ItemExistente(int idItem);
    }
}