using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Container
{
    public int IdContainer { get; set; }

    public int NumContainer { get; set; }

    public int IdAndar { get; set; }

    public virtual Andar IdAndarNavigation { get; set; } = null!;
}
