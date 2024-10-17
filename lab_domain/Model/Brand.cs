using System;
using System.Collections.Generic;

namespace lab_domain.Model;

public partial class Brand
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Country { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<CardsDeck> CardsDecks { get; set; } = new List<CardsDeck>();
}
