using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    internal class Andar
    {
        private readonly List<Container> _containers;
        private  Stack<Andar> _Andares;

        public int NumeroAndar { get; private set; }

        [StringLength(50)]
        public string Descricao { get; set; }

        private const int numContainers = 2;

        public Andar(int numAndar, string descricao)
        {
            NumeroAndar = numAndar;
            _containers = new List<Container>();
            Descricao = descricao;

            for (int i = 0; i < numContainers; i++)
                _containers.Add(new Container(i));
        }

        public Andar()
        { }

        internal Stack<Andar> CriaAndar()
        {
            _Andares = new Stack<Andar>();

            _Andares.Push(new Andar(0, "Carnes, Ovos e Charcutaria"));
            _Andares.Push(new Andar(1, "Laticínios e Enlatados"));
            _Andares.Push(new Andar(2, "Hortifruti, Frutas e Verduras"));

            return _Andares;
        }

        public Container? RetornarContainer(int numContainer) =>
            _containers?.Find(container => container?.NumeroContainer == numContainer);

        internal List<Andar> RetornarAndares() =>
            CriaAndar().ToList();

        internal List<Andar> RetornarAndares(int numAndar)
        {
            var lstAndares = _Andares.ToList();

            ValidarAndares(numAndar, lstAndares);

            return lstAndares;
        }

        internal void ValidarAndares(int numAndar, List<Andar> lstAndares)
        {
            if (numAndar < 0 || numAndar >= lstAndares.Count)
                throw new Exception("Numero do andar inválido!");
        }

        public string ImprimirAndar()
        {
            string retorno = $"Andar {NumeroAndar}:";

            foreach (var container in _containers)
                retorno += container.ImprimirContainer();

            return retorno;
        }
    }
}