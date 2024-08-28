
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Item
    {
        
        public int? ID { get; set; }

        [StringLength(100)]
        public string Descricao { get; set; } = string.Empty;

        public int Quantidade { get; set; }

        [StringLength(100)]
        public string UnidadeQtd { get; set; } = string.Empty;

        [StringLength(200)]
        public string Classificacao { get; set; } = string.Empty;
    }
}