using System;
using System.Collections.Generic;

namespace lab_domain.Model;

public partial class CardsDeck
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? BrandId { get; set; }

    public int? TypeId { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public decimal? Price { get; set; }

    public int? Stock { get; set; }

    public string? Description { get; set; }

    public double? Rating { get; set; }

    public string? ImageUrl { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual ICollection<CardTag> CardTags { get; set; } = new List<CardTag>();

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual CardType? Type { get; set; }
}
