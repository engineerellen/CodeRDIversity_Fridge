namespace Domain
{
    public class Container : IContainer
    {
        private readonly List<Item> _items;
        private const int itemsLength = 4;

        public int NumeroContainer { get; private set; }

        public Container(int numContainer)
        {
            NumeroContainer = numContainer;
            _items = new List<Item>();

            ResetarItens(itemsLength);
        }

        private void ResetarItens(int qtdePosicoes)
        {
            for (int i = 0; i < qtdePosicoes; i++)
                _items.Add(new Item());
        }

        private static void ValidarPosicaoItem(int posicao)
        {
            if (posicao < 0 || posicao >= itemsLength)
                throw new Exception("Posição inválida!");
        }

        public void AdicionarItem(int posicao, Item item)
        {
            ValidarPosicaoItem(posicao);

            if (_items[posicao] != null && _items[posicao].ID != null)
            {
                Console.WriteLine($"Posicao {posicao} já esta preenchida!");
                return;
            }
            _items[posicao] = item;
        }

        public Item? RetornarItem(int posicao)
        {
            ValidarPosicaoItem(posicao);

            return _items[posicao];
        }


        public void RemoverItem(int posicao)
        {
            ValidarPosicaoItem(posicao);

            if (_items[posicao] != null && _items[posicao].ID == null)
            {
                Console.WriteLine($"Posição {posicao} já está vazia");
                return;
            }
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
                if (item != null && item.ID != null)
                    return false;
            }

            return true;
        }

        public void LimparContainer()=>ResetarItens(itemsLength);

        public void AdicionarItens(List<Item> itens)
        {
            if (itens.Count > _items.Count)
            {
                Console.WriteLine("Quantidade de itens ultrapassa o limite permitido!");
                return;
            }

            int posicao = 0;
            foreach (var item in itens)
            {
                while (posicao < _items.Count && _items[posicao].ID != null)
                    posicao++;

                if (posicao < _items.Count)
                    _items[posicao] = item;

                else
                {
                    Console.WriteLine("Container está cheio! Não é permitido incluir itens no momento!");
                    break;
                }
            }
        }

        public void ImprimirItens()
        {
            Console.WriteLine($"  Container {NumeroContainer}:");
            for (int posicao = 0; posicao < _items.Count; posicao++)
            {
                var item = _items[posicao];
                if (item != null && item.ID != null)
                    Console.WriteLine($"    Posição {posicao}: {item.Descricao}");
            }
        }
    }
}