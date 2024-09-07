using System.Text;

namespace Domain
{
    public sealed class Geladeira
    {
        private Stack<Andar> _Andares;

        public Geladeira()
        {
            _Andares = new Stack<Andar>();

            _Andares.Push(new Andar(0, "Carnes, Ovos e Charcutaria"));
            _Andares.Push(new Andar(1, "Laticínios e Enlatados"));
            _Andares.Push(new Andar(2, "Frutas e Verduras"));
        }

        internal List<Andar> RetornarAndares()=>
            _Andares.ToList();


        private List<Andar> RetornarAndares(int numAndar)
        {
            var lstAndares = _Andares.ToList();

            Validarandares(numAndar, lstAndares);

            return lstAndares;
        }

        internal static void Validarandares(int numAndar, List<Andar> lstAndares)
        {
            if (numAndar < 0 || numAndar >= lstAndares.Count)
                throw new Exception("Numero do andar inválido!");
        }

        private Container? RetornarContainer(int numAndar, int numContainer, List<Andar> lstAndares)
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

            container?.RemoverItem(posicao);
        }

        public void LimparContainer(int numAndar, int numContainer)
        {
            var arrAndares = RetornarAndares(numAndar);

            Container? container = RetornarContainer(numAndar, numContainer, arrAndares);

            if (!Convert.ToBoolean(container?.EstaVazio()))
                container?.LimparContainer();
            else
                throw new Exception("Container está vazio!");
        }

        public void AddItensAoContainer(int numAndar, int numContainer, List<Item> itens)
        {
            var arrAndares = RetornarAndares(numAndar);

            Container? container = RetornarContainer(numAndar, numContainer, arrAndares);

            if (!Convert.ToBoolean(container?.EstaCheio()))
                container?.AdicionarItens(itens);
            else
                throw new Exception("Container já está cheio!");

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