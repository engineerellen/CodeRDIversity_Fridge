using Domain.Interfaces;

namespace Domain.Models
{
    public class Container : IContainer
    {
        public readonly List<Item> Items;
        private const int itemsLength = 4;
        public Andar _andar;
        public Item _item;


        public int NumeroContainer { get; private set; }

        public Container(int numContainer)
        {
            NumeroContainer = numContainer;
            Items = new List<Item>();
            _andar = new Andar();
            _item = new Item();

            ResetarContainer(itemsLength);
        }

        public Container()
        {
            if (_andar is null)
            {
                _andar = new Andar();
                _andar.RetornarAndares();
            }
        }

        private void ResetarContainer(int qtdePosicoes)
        {
            for (int i = 0; i < qtdePosicoes; i++)
            {
                if (Items?.Count < qtdePosicoes)
                    Items?.Add(new Item());
                else
                    Items[i] = new Item();
            }

        }

        private static void ValidarPosicao(int posicao)
        {
            if (posicao < 0 || posicao >= itemsLength)
                throw new Exception("Posição inválida!");
        }


        public Container? RetornarContainer(int numContainer) =>
           _andar.Containers?.Find(container => container?.NumeroContainer == numContainer);

        public void AdicionarItem(int posicao, Item item)
        {
            ValidarPosicao(posicao);

            if (Items[posicao] != null && !string.IsNullOrEmpty(Items[posicao]?.Descricao))
                throw new Exception($"Posicao {posicao} já esta preenchida!");

            Items[posicao] = item;
        }

        public Item? RetornarItem(int posicao)
        {
            ValidarPosicao(posicao);

            return Items[posicao];
        }

        public void RemoverItemDoContainer(int posicao)
        {
            ValidarPosicao(posicao);

            if (Items[posicao] != null && string.IsNullOrEmpty(Items[posicao]?.Descricao))
                throw new Exception($"Posição {posicao} já está vazia");

            Items[posicao] = new Item();
        }

        public bool EstaCheio()
        {
            foreach (var item in Items)
            {
                if (string.IsNullOrEmpty(item.Descricao))
                    return false;
            }
            return true;
        }

        public bool EstaVazio()
        {
            foreach (var item in Items)
            {
                if (item != null && !string.IsNullOrEmpty(item?.Descricao))
                    return false;
            }

            return true;
        }

        public void LimparContainer() => ResetarContainer(itemsLength);

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

            var container = RetornarContainer(numContainer);
            return container;
        }

        public string ImprimirContainer()
        {
            string retorno = $"  Container {NumeroContainer}:";
            for (int posicao = 0; posicao < Items.Count; posicao++)
            {
                var item = Items[posicao];
                if (item != null && !string.IsNullOrEmpty(item?.Descricao))
                    retorno += $"    Posição {posicao}: {item.Descricao}";
            }
            return retorno;
        }
    }
}