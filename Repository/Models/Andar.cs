using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Andar
{
    public int IdAndar { get; set; }

    public string Andar1 { get; set; } = null!;

    public int NumAndar { get; set; }

    public int? IdClassificacao { get; set; }

    public virtual ICollection<Container> Containers { get; set; } = new List<Container>();

    public virtual Classificacao? IdClassificacaoNavigation { get; set; }
}
