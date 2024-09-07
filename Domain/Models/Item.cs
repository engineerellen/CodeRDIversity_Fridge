using Domain.Validators;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Item
    {
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
    }
}