using System;
using System.Collections.Generic;
using lab_domain.Model;
using Microsoft.EntityFrameworkCore;

namespace lab_infrastructure;

public partial class CardsDeckShopWebContext : DbContext
{
    public CardsDeckShopWebContext()
    {
    }

    public CardsDeckShopWebContext(DbContextOptions<CardsDeckShopWebContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<CardTag> CardTags { get; set; }

    public virtual DbSet<CardType> CardTypes { get; set; }

    public virtual DbSet<CardsDeck> CardsDecks { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Delivery> Deliveries { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<SupportTicket> SupportTickets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=Denis\\SQLEXPRESS; Database=CardsDeckShop_WEB; Trusted_Connection=True; TrustServerCertificate=True; ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Brand__3213E83F9592CAF7");

            entity.ToTable("Brand");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Country)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<CardTag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Card_Tag__3213E83F4045977F");

            entity.ToTable("Card_Tags");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CardId).HasColumnName("card_id");
            entity.Property(e => e.Tag)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("tag");

            entity.HasOne(d => d.Card).WithMany(p => p.CardTags)
                .HasForeignKey(d => d.CardId)
                .HasConstraintName("FK__Card_Tags__card___7A672E12");
        });

        modelBuilder.Entity<CardType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Card_Typ__3213E83F0B2DD2C3");

            entity.ToTable("Card_Types");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TypeName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<CardsDeck>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CardsDec__3213E83F8BB6CE38");

            entity.ToTable("CardsDeck");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("image_url");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.ReleaseDate).HasColumnName("release_date");
            entity.Property(e => e.Stock).HasColumnName("stock");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Brand).WithMany(p => p.CardsDecks)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK__CardsDeck__brand__778AC167");

            entity.HasOne(d => d.Type).WithMany(p => p.CardsDecks)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK__CardsDeck__type___787EE5A0");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cart__3213E83FA57E32A3");

            entity.ToTable("Cart");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CardId).HasColumnName("card_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Card).WithMany(p => p.Carts)
                .HasForeignKey(d => d.CardId)
                .HasConstraintName("FK__Cart__card_id__71D1E811");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Cart__user_id__70DDC3D8");
        });

        modelBuilder.Entity<Delivery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Delivery__3213E83F7B065A96");

            entity.ToTable("Delivery");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DeliveryAdress)
                .HasColumnType("text")
                .HasColumnName("delivery_adress");
            entity.Property(e => e.DeliveryDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("delivery_date");
            entity.Property(e => e.DeliveryMethod)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("delivery_method");
            entity.Property(e => e.OrderId).HasColumnName("order_id");

            entity.HasOne(d => d.Order).WithMany(p => p.Deliveries)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Delivery__order___76969D2E");
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Discount__3213E83F45A95559");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CardsdeckId).HasColumnName("cardsdeck_id");
            entity.Property(e => e.DiscountPercentage).HasColumnName("discount_percentage");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasOne(d => d.Cardsdeck).WithMany(p => p.Discounts)
                .HasForeignKey(d => d.CardsdeckId)
                .HasConstraintName("FK__Discounts__cards__797309D9");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Favorite__3213E83F02744127");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CardId).HasColumnName("card_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Card).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.CardId)
                .HasConstraintName("FK__Favorites__card___6FE99F9F");

            entity.HasOne(d => d.User).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Favorites__user___6EF57B66");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3213E83F82E9E349");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("order_date");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_price");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Orders__user_id__6C190EBB");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order_It__3213E83F7DE7D6C6");

            entity.ToTable("Order_Items");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CardId).HasColumnName("card_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Card).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.CardId)
                .HasConstraintName("FK__Order_Ite__card___74AE54BC");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Order_Ite__order__73BA3083");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payment__3213E83FD5D0D101");

            entity.ToTable("Payment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("payment_date");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("payment_method");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Payment__order_i__75A278F5");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Promotio__3213E83F8A0A13ED");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CardId).HasColumnName("card_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.PromoName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("promo_name");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasOne(d => d.Card).WithMany(p => p.Promotions)
                .HasForeignKey(d => d.CardId)
                .HasConstraintName("FK__Promotion__card___7B5B524B");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reviews__3213E83FF098A487");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CardId).HasColumnName("card_id");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.ReviewDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("review_date");
            entity.Property(e => e.ReviewText)
                .HasColumnType("text")
                .HasColumnName("review_text");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Card).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.CardId)
                .HasConstraintName("FK__Reviews__card_id__6E01572D");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Reviews__user_id__6D0D32F4");
        });

        modelBuilder.Entity<SupportTicket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Support___3213E83F3381100B");

            entity.ToTable("Support_Tickets");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.ResolvedDate)
                .HasColumnType("datetime")
                .HasColumnName("resolved_date");
            entity.Property(e => e.TicketStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ticket_status");
            entity.Property(e => e.TicketSubject)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ticket_subject");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.SupportTickets)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Support_T__user___72C60C4A");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83FB411505B");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E6164AF493696").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password_hash");
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("registration_date");
            entity.Property(e => e.UserType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("customer")
                .HasColumnName("user_type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
