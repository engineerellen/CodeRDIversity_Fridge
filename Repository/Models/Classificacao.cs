using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Classificacao
{
    public int IdClassificacao { get; set; }

    public string Classificacao1 { get; set; } = null!;

    public virtual ICollection<Andar> Andars { get; set; } = new List<Andar>();

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
