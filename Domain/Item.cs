using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Item
    {
        public int? ID { get; set; }

        public string Descricao { get; set; } = string.Empty;

        public int Quantidade { get; set; }

        public string UnidadeQtd { get; set; } = string.Empty;

        public string Classificacao { get; set; } = string.Empty;
    }
}