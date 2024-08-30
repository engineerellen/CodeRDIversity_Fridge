
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Item
    {
        [Required]
        public int IdItem { get; set; }

        [Required]
        public int NumeroAndar { get; set; }
        [Required]
        public int NumeroContainer { get; set; }
        [Required]
        public int PosicaoDentroContainer { get; set; }

        [StringLength(100)]
        [Required]
        public string Descricao { get; set; } = string.Empty;

        public int Quantidade { get; set; }

        [StringLength(100)]
        public string UnidadeQtd { get; set; } = string.Empty;

        [StringLength(200)]
        public string Classificacao { get; set; } = string.Empty;
    }
}
