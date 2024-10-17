using System;
using System.Collections.Generic;

namespace lab_domain.Model;

public partial class Discount
{
    public int Id { get; set; }

    public int? CardsdeckId { get; set; }

    public int? DiscountPercentage { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual CardsDeck? Cardsdeck { get; set; }
}
