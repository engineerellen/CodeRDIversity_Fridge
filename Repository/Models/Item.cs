using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Item
{
    public string Descricao { get; set; } = null!;

    public int Quantidade { get; set; }

    public string UnidadeQuantidade { get; set; } = null!;

    public int IdClassificacao { get; set; }

    public int IdItem { get; set; }

    public virtual Classificacao IdClassificacaoNavigation { get; set; } = null!;
}
