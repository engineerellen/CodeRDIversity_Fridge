using System.Buffers;
using System.Drawing;

namespace Domain
{
    public sealed class Geladeira
    {
        private Stack<Andar> _Andares;

        private const int numAndares = 3;

        public Geladeira()
        {
            _Andares = new Stack<Andar>();

            for (int i = 0; i < numAndares; i++)
                _Andares.Push(new Andar(i));
        }

        private List<Andar> RetornarAndares(int numAndar)
        {
            var lstAndares = _Andares.ToList();

            Validarandares(numAndar, lstAndares);

            return lstAndares;
        }

        private static void Validarandares(int numAndar, List<Andar> lstAndares)
        {
            if (numAndar < 0 || numAndar >= lstAndares.Count)
                throw new Exception("Numero do andar inválido!");
        }

        private static Container? RetornarContainer(int numAndar, int numContainer, List<Andar> lstAndares)
        {
            Validarandares(numAndar, lstAndares);

            var container = lstAndares[numAndar].RetornarContainer(numContainer);
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
            var lstAndares = _Andares.ToList();

            foreach (var andar in lstAndares)
                andar.ImprimirItens();
        }
    }
}
