using System;
using System.Collections.Generic;

namespace lab_domain.Model;

public partial class Review
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? CardId { get; set; }

    public int? Rating { get; set; }

    public string? ReviewText { get; set; }

    public DateTime? ReviewDate { get; set; }

    public virtual CardsDeck? Card { get; set; }

    public virtual User? User { get; set; }
}
