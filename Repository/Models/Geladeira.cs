using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Geladeira
{
    public int IdContainer { get; set; }

    public int IdItem { get; set; }

    public virtual Container IdContainerNavigation { get; set; } = null!;

    public virtual Item IdItemNavigation { get; set; } = null!;
}
