using System.ComponentModel.DataAnnotations;

namespace Domain
{
    internal class Andar
    {
        private readonly List<Container> _containers;

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

        public Container? RetornarContainer(int numContainer) =>
            _containers?.Find(container => container?.NumeroContainer == numContainer);

        public string ImprimirItens()
        {
            string retorno = $"Andar {NumeroAndar}:";

            foreach (var container in _containers)
               retorno += container.ImprimirItens();

            return retorno;
        }
    }
}