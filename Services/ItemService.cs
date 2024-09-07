using Domain.Models;
using Microsoft.Data.SqlClient;
using Repository.Interface;
using Services.Interfaces;

namespace Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository) =>
            _itemRepository = itemRepository;

        public async Task<string> InserirItemAsync(Item item)
        {
            try
            {
                if (item != null)
                {
                    if (await PosicaoPreenchida(item))
                        throw new Exception("Posição já preenchida!");

                    var itemExistente = ObterItemPorID(item.IdItem);

                    if (itemExistente == null)
                    {
                        await _itemRepository.InserirItemAsync(item);

                        return "Item cadastrado com sucesso!";
                    }
                    else
                        throw new Exception("Item já existente na geladeira.");
                }
                else
                    return "Item inválido!";
            }
            catch (SqlException)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> AtualizarItemAsync(Item item)
        {
            try
            {

                if (item != null)
                {
                    await _itemRepository.AtualizarItemAsync(item);

                    return "Item alterado com sucesso!";
                }
                else
                    return "Item inválido!";
            }
            catch (SqlException)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public Item? ObterItemPorID(int idItem)
        {
            Item? item = new Item();

            try
            {
                if (idItem == 0)
                    return null;

                _itemRepository.ObterItemPorID(idItem);

                if (item != null)
                    return item;
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool ItemExistente(int idItem) =>
            _itemRepository.ItemExistente(idItem);

        private async Task<bool> PosicaoPreenchida(Item item)
        {
            var lstItems = await RetornarItensAsync();
            lstItems = lstItems?.Where(i => i.NumeroAndar == item.NumeroAndar
                                                            && i.NumeroContainer == item.NumeroContainer
                                                            && i.PosicaoDentroContainer == item.PosicaoDentroContainer).ToList();
            var itemRetornado = lstItems?.FirstOrDefault();

            return itemRetornado != null;
        }

        public async Task<List<Item>> RetornarItensAsync()=>
            await _itemRepository.RetornarItensAsync();

        public async Task<string> RetirarItemPorIDAsync(int idItem)
        {
            try
            {
                if (idItem == 0)
                    throw new Exception("Item inválido! Por favor tente novamente.");
                else
                {
                    await _itemRepository.RetirarItemPorIDAsync(idItem);

                    return "Item deletado com sucesso!";
                }
            }
            catch (SqlException)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> EsvaziarContainerAsync(int numAndar, int numContainer)
        {
            var itensContainer = await RetornarItensAsync();

            itensContainer = itensContainer?.Where(c => c.NumeroAndar == numAndar && c.NumeroContainer == numContainer).ToList();

            if (itensContainer is not null)
            {
                foreach (var item in itensContainer)
                    await _itemRepository.RetirarItemPorIDAsync(item.IdItem);
            }

            else
                throw new Exception("Container está vazio!");

            return "Container esvaziado com sucesso!";
        }

        public async Task<string> AdicionarItensNaGeladeiraAsync(List<Item> itens)
        {
            foreach (Item item in itens)
            {
                var itensContainer = await RetornarItensAsync();

                itensContainer = itensContainer?.Where(c => c.NumeroAndar == item.NumeroAndar && c.NumeroContainer == item.NumeroContainer).ToList();

                if (itensContainer is not null)
                    await _itemRepository.InserirItemAsync(item);

                else
                    throw new Exception("Container já está cheio!");
            }

            return "Itens adicionados com sucesso!";
        }
    }
}