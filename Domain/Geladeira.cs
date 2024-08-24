using System.Buffers;
using System.Drawing;
using System.Text;

namespace Domain
{
    public sealed class Geladeira
    {
        private Stack<Andar> _Andares;

        public Geladeira()
        {
            _Andares = new Stack<Andar>();

            _Andares.Push(new Andar(0, "Charcutaria, Carnes e Ovos"));
            _Andares.Push(new Andar(1, "Laticínios e Enlatados"));
            _Andares.Push(new Andar(2, "Frutas e Verduras"));
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

        public string AdicionarItem(int numAndar, int numContainer, int posicao, Item item)
        {
            var arrAndares = RetornarAndares(numAndar);

            Container? container = RetornarContainer(numAndar, numContainer, arrAndares);

            if (container == null)
                throw new Exception("Numero do container inválido!");

            return container.AdicionarItem(posicao, item);
        }


        public string RemoverItem(int numAndar, int numContainer, int posicao)
        {
            var arrAndares = RetornarAndares(numAndar);

            Container? container = RetornarContainer(numAndar, numContainer, arrAndares);

            return container?.RemoverItem(posicao) ?? $"Não foi possível remover o item {posicao}";
        }

        public string LimparContainer(int numAndar, int numContainer)
        {
            var arrAndares = RetornarAndares(numAndar);

            Container? container = RetornarContainer(numAndar, numContainer, arrAndares);

            if (!Convert.ToBoolean(container?.EstaVazio()))
                container?.LimparContainer();
            else
                return "Container está vazio!";

            return "Container esvaziado com sucesso!";

        }

        public string AddItensAoContainer(int numAndar, int numContainer, List<Item> itens)
        {
            var arrAndares = RetornarAndares(numAndar);

            Container? container = RetornarContainer(numAndar, numContainer, arrAndares);

            if (!Convert.ToBoolean(container?.EstaCheio()))
                container?.AdicionarItens(itens);
            else
                return "Container já está cheio!";

            return "Itens adicionados ao Container com sucesso!";
        }

        public string ImprimeConteudo()
        {
            StringBuilder sbRetorno = new StringBuilder();
            var lstAndares = _Andares.ToList();

            foreach (var andar in lstAndares)
                sbRetorno.AppendLine(andar.ImprimirItens());

            return sbRetorno.ToString();
        }
    }
}