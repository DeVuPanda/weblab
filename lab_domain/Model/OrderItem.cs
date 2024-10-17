using System;
using System.Collections.Generic;

namespace lab_domain.Model;

public partial class OrderItem
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public int? CardId { get; set; }

    public int? Quantity { get; set; }

    public virtual CardsDeck? Card { get; set; }

    public virtual Order? Order { get; set; }
}
