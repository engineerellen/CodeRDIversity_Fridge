using Domain.Interfaces;

namespace Domain.Models
{
    public class Container : IContainer
    {
        private readonly List<Item> _items;
        private const int itemsLength = 4;
        private Andar _andar;
        private Item _item;

        public int NumeroContainer { get; private set; }

        public Container(int numContainer)
        {
            NumeroContainer = numContainer;
            _items = new List<Item>();
            _andar = new Andar();
            _item = new Item();

            ResetarItens(itemsLength);
        }

        public Container() { }

        private void ResetarItens(int qtdePosicoes)
        {
            for (int i = 0; i < qtdePosicoes; i++)
                _items?.Add(new Item());
        }

        private static void ValidarPosicao(int posicao)
        {
            if (posicao < 0 || posicao >= itemsLength)
                throw new Exception("Posição inválida!");
        }

        public void AdicionarItem(int posicao, Item item)
        {
            ValidarPosicao(posicao);

            if (_items[posicao] != null && _items[posicao]?.IdItem != null)
                throw new Exception($"Posicao {posicao} já esta preenchida!");

            _items[posicao] = item;
        }

        public Item? RetornarItem(int posicao)
        {
            ValidarPosicao(posicao);

            return _items[posicao];
        }

        public void RemoverItem(int posicao)
        {
            ValidarPosicao(posicao);

            if (_items[posicao] != null && _items[posicao]?.IdItem == null)
                throw new Exception($"Posição {posicao} já está vazia");

            _items[posicao] = new Item();
        }

        public bool EstaCheio()
        {
            foreach (var item in _items)
            {
                if (item == null)
                    return false;
            }
            return true;
        }

        public bool EstaVazio()
        {
            foreach (var item in _items)
            {
                if (item != null && item?.IdItem != null)
                    return false;
            }

            return true;
        }

        public void LimparContainer() => ResetarItens(itemsLength);

        public void LimparContainer(int numAndar, int numContainer)
        {
            var arrAndares = _andar.RetornarAndares(numAndar);

            Container? container = RetornarContainer(numAndar, numContainer, arrAndares);

            if (!Convert.ToBoolean(container?.EstaVazio()))
                container?.LimparContainer();
            else
                throw new Exception("Container está vazio!");
        }

        internal Container? RetornarContainer(int numAndar, int numContainer, List<Andar> lstAndares)
        {
            _andar.ValidarAndares(numAndar, lstAndares);

            var container = lstAndares[numAndar].RetornarContainer(numContainer);
            return container;
        }

        public string ImprimirContainer()
        {
            string retorno = $"  Container {NumeroContainer}:";
            for (int posicao = 0; posicao < _items.Count; posicao++)
            {
                var item = _items[posicao];
                if (item != null && item?.IdItem != null)
                    retorno += $"    Posição {posicao}: {item.Descricao}";
            }
            return retorno;
        }
    }
}