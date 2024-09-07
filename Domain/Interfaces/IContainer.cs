using Domain.Models;

namespace Domain.Interfaces
{
    internal interface IContainer
    {
        void AdicionarItem(int posicao, Item item);
        Item? RetornarItem(int posicao);
        void RemoverItem(int posicao);
        bool EstaCheio();
        bool EstaVazio();
    }
}