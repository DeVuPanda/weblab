using System;
using System.Collections.Generic;

namespace lab_domain.Model;

public partial class CardType
{
    public int Id { get; set; }

    public string? TypeName { get; set; }

    public virtual ICollection<CardsDeck> CardsDecks { get; set; } = new List<CardsDeck>();
}
