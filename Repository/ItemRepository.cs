using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Interface;

namespace Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly GeladeiraContext _contexto;

        public ItemRepository(GeladeiraContext contexto)=>
            _contexto = contexto;

        public async Task AtualizarItemAsync(Item item)
        {
            _contexto.Entry(item).State = EntityState.Modified;

            _contexto.Update(item);
            _contexto.SaveChangesAsync();
        }

        public async Task InserirItemAsync(Item item)
        {
            _contexto.Add(item);
            _contexto.SaveChangesAsync();
        }

        public Item? ObterItemPorID(int idItem)
        {
            var lstItems = _contexto.Items.Where(x => x.IdItem == idItem).ToList();

            return lstItems != null ? lstItems.FirstOrDefault() : null;
        }

        public async Task RetirarItemPorIDAsync(int idItem)
        {
            var item = await _contexto.Items.FindAsync(idItem);

            if (item != null)
            {
                _contexto.Items.Remove(item);
                _contexto.SaveChangesAsync();
            }

        }

        public async Task<List<Item>> RetornarItensAsync() => await _contexto.Items.ToListAsync();

        public bool ItemExistente(int idItem) => _contexto.Items.Any(e => e.IdItem == idItem);

    }
}