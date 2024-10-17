using System;
using System.Collections.Generic;

namespace lab_domain.Model;

public partial class CardTag
{
    public int Id { get; set; }

    public int? CardId { get; set; }

    public string? Tag { get; set; }

    public virtual CardsDeck? Card { get; set; }
}
