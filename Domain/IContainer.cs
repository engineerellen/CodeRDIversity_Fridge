using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    internal interface IContainer
    {
        string AdicionarItem(int posicao, Item item);
        Item? RetornarItem(int posicao);
        string RemoverItem(int posicao);
        bool EstaCheio();
        bool EstaVazio();
    }
}