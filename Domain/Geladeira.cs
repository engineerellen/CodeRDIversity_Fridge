using System.Buffers;
using System.Drawing;

namespace Domain
{
    public sealed class Geladeira
    {
        private Stack<Andar> _Andares;

        public Geladeira(int numAndares = 3)
        {
            _Andares = new Stack<Andar>();

            for (int i = 0; i < numAndares; i++)
                _Andares.Push(new Andar(i));
        }

        private Andar[] RetornarAndares(int numAndar)
        {
            var arrAndares = _Andares.ToArray();

            Validarandares(numAndar, arrAndares);

            return arrAndares;
        }

        private static void Validarandares(int numAndar, Andar[] arrAndares)
        {
            if (numAndar < 0 || numAndar >= arrAndares.Length)
                throw new Exception("Numero do andar inválido!");
        }

        private static Container? RetornarContainer(int numAndar, int numContainer, Andar[] arrAndares)
        {
            Validarandares(numAndar, arrAndares);

            var container = arrAndares[numAndar].RetornarContainer(numContainer);
            return container;
        }

        public void AdicionarItem(int numAndar, int numContainer, int posicao, Item item)
        {
            var arrAndares = RetornarAndares(numAndar);

            Container? container = RetornarContainer(numAndar, numContainer, arrAndares);

            if (container == null)
                throw new Exception("Numero do container inválido!");

            container.AdicionarItem(posicao, item);
        }


        public void RemoverItem(int numAndar, int numContainer, int posicao)
        {
            var arrAndares = RetornarAndares(numAndar);

            Container? container = RetornarContainer(numAndar, numContainer, arrAndares);

            container?.RetornarItem(posicao);
        }

        public void LimparContainer(int numAndar, int numContainer)
        {
            var arrAndares = RetornarAndares(numAndar);

            Container? container = RetornarContainer(numAndar, numContainer, arrAndares);

            if (!Convert.ToBoolean(container?.EstaVazio()))
            {
                container?.LimparContainer();
            }
            else
            {
                Console.WriteLine("Container está vazio!");
                return;
            }
        }

        public void AddItensAoContainer(int numAndar, int numContainer, List<Item> itens)
        {
            var arrAndares = RetornarAndares(numAndar);

            Container? container = RetornarContainer(numAndar, numContainer, arrAndares);

            if (!Convert.ToBoolean(container?.EstaCheio()))
                container?.AdicionarItens(itens);
            else
            {
                Console.WriteLine("Container já está cheio!");
                return;
            }

        }

        public void ImprimeConteudo()
        {
            var arrAndares = _Andares.ToArray();

            for (int i = arrAndares.Length - 1; i >= 0; i--)
            {
                arrAndares[i].ImprimirItens();
            }
        }
    }
}
