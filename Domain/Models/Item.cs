using Domain.Validators;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Item
    {
        public List<Item> Items;

        public Item()
        {

        }

        [Required(ErrorMessage = "O campo Id do Item é obrigatório!")]
        [RegularExpression("([0-9]+)", ErrorMessage = "O ID deverá ser numérico!")]
        public int IdItem { get; set; }

        [Required(ErrorMessage = "O campo Número do Andar é obrigatório!")]
        [RegularExpression("([0-9]+)", ErrorMessage = "O valor do Andar deverá ser numérico!")]
        [Range(0, 2, ErrorMessage = "A geladeira possui apenas 3 andares, das posições 0 a 2!")]
        public int NumeroAndar { get; set; }

        [Required(ErrorMessage = "O campo Número do Contaner é obrigatório!")]
        [RegularExpression("([0-9]+)", ErrorMessage = "O valor do Container deverá ser numérico!")]
        [Range(0, 1, ErrorMessage = "A geladeira possui apenas 2 containers, das posições 0 a 1!")]
        public int NumeroContainer { get; set; }


        [Required(ErrorMessage = "O campo Posição do container é obrigatório!")]
        [RegularExpression("([0-9]+)", ErrorMessage = "O valor da Posição deverá ser numérico!")]
        [Range(0, 3, ErrorMessage = "Os containers possuem apenas 4 posições, variando de 0 a 3!")]
        public int PosicaoDentroContainer { get; set; }


        [Required(ErrorMessage = "O campo Descrição é obrigatório!")]
        [StringLength(100, ErrorMessage = "O campo Descrição deverá ter até 100 caracteres!")]
        public string Descricao { get; set; } = string.Empty;


        [RegularExpression("([0-9]+)", ErrorMessage = "A Quantidade deverá ser numérica!")]
        public int Quantidade { get; set; }


        [StringLength(100, ErrorMessage = "O campo Unidade da Quantidade deverá ter até 100 caracteres!")]
        public string UnidadeQtd { get; set; } = string.Empty;


        [StringLength(200, ErrorMessage = "O campo classificação deverá ter até 200 caracteres!")]
        [ClassificacaoValidator]
        public string Classificacao { get; set; } = string.Empty;


        public void AdicionarItens(List<Item> itens)
        {
            if (itens.Count > Items.Count)
                throw new Exception("Quantidade de itens ultrapassa o limite permitido!");

            int posicao = 0;
            foreach (var item in itens)
            {
                while (posicao < Items.Count && !string.IsNullOrEmpty(Items[posicao]?.Descricao))
                    posicao++;

                if (posicao < Items.Count)
                    Items[posicao] = item;

                else
                    throw new Exception("Container está cheio! Não é permitido incluir itens no momento!");
            }
        }

        public void AdicionarItem(int numAndar, int numContainer, int posicao, Item item)
        {
            var arrAndares = new Andar().RetornarAndares(numAndar);

            Container? container = new Container().RetornarContainer(numAndar, numContainer, arrAndares);

            if (container is null)
                container = arrAndares?.Find(a => a.NumeroAndar == numAndar)?
                               .Containers.Find(c => c.NumeroContainer == numContainer);

            if (container == null)
                throw new Exception("Numero do container inválido!");

            container.AdicionarItem(posicao, item);
        }

        public void AddItens(int numAndar, int numContainer, List<Item> itens)
        {
            var arrAndares = new Andar().RetornarAndares(numAndar);

            Container? container = new Container().RetornarContainer(numAndar, numContainer, arrAndares);

            if (!Convert.ToBoolean(container?.EstaCheio()))
                AdicionarItens(itens);
            else
                throw new Exception("Container já está cheio!");

        }

        public void RemoverItem(int numAndar, int numContainer, int posicao)
        {
            var arrAndares = new Andar().RetornarAndares(numAndar);

            Container? container = new Container().RetornarContainer(numAndar, numContainer, arrAndares);

            container?.RemoverItem(posicao);
        }
    }
}