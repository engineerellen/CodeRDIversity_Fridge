using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Andar
    {
        public readonly List<Container> Containers;
        public Stack<Andar> _Andares;

        public int NumeroAndar { get; private set; }

        [StringLength(50)]
        public string Descricao { get; set; }

        private const int numContainers = 2;

        public Andar(int numAndar, string descricao)
        {
            NumeroAndar = numAndar;
            Containers = new List<Container>();
            Descricao = descricao;


            for (int i = 0; i < numContainers; i++)
                Containers.Add(new Container(i));

        }

        public Andar()
        {
        }

        public Stack<Andar> CriaAndar()
        {
            _Andares = new Stack<Andar>();

            _Andares.Push(new Andar(0, "Carnes, Ovos e Charcutaria"));
            _Andares.Push(new Andar(1, "Laticínios e Enlatados"));
            _Andares.Push(new Andar(2, "Hortifruti, Frutas e Verduras"));

            return _Andares;
        }


        public List<Andar> RetornarAndares() =>
            CriaAndar().ToList();

        public List<Andar> RetornarAndares(int numAndar)
        {
            var lstAndares = _Andares is null ? RetornarAndares() : _Andares.ToList();

            ValidarAndares(numAndar, lstAndares);

            return lstAndares;
        }

        public void ValidarAndares(int numAndar, List<Andar> lstAndares)
        {
            if (numAndar < 0 || numAndar >= lstAndares.Count)
                throw new Exception("Numero do andar inválido!");
        }

        public string ImprimirAndar()
        {
            string retorno = $"Andar {NumeroAndar}:";

            foreach (var container in Containers)
                retorno += container.ImprimirContainer();

            return retorno;
        }
    }
}