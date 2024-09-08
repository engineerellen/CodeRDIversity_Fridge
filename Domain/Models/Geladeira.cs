using System.Text;

namespace Domain.Models
{
    public sealed class Geladeira
    {
        private Stack<Andar> _Andares;

        public Item Item { get; set; }

        public Container Container { get; set; }

        public Geladeira()
        {
            Item = new Item();
            Container = new Container();
            _Andares = new Andar().CriaAndar();
        }


        public string ImprimeGeladeira()
        {
            StringBuilder sbRetorno = new StringBuilder();
            var lstAndares = _Andares.ToList();

            foreach (var andar in lstAndares)
                sbRetorno.AppendLine(andar.ImprimirAndar());

            return sbRetorno.ToString();
        }
    }
}