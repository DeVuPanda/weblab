using System;
using System.Collections.Generic;

namespace lab_domain.Model;

public partial class Favorite
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? CardId { get; set; }

    public virtual CardsDeck? Card { get; set; }

    public virtual User? User { get; set; }
}
