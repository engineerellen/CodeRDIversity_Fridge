using Domain.Models;

namespace Repository.Interface
{
    public interface IItemRepository
    {
        Task<List<Item>> RetornarItensAsync();
        Item? ObterItemPorID(int idItem);
        Task InserirItemAsync(Item item);
        Task AtualizarItemAsync(Item item);
        Task RetirarItemPorIDAsync(int idItem);
        bool ItemExistente(int idItem);
    }

}