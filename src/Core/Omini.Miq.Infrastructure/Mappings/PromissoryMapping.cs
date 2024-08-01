using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Omini.Miq.Domain.Sales;

namespace Omini.Miq.Infrastructure.Mappings;

internal sealed class PromissoryMapping : DocumentEntityMapping<Promissory>
{
    public override void Configure(EntityTypeBuilder<Promissory> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ExternalStatus)
            .HasMaxLength(50);

        builder.ToTable("Promissories");
    }
}

internal sealed class PromissoryItemMapping : IEntityTypeConfiguration<PromissoryItem>
{
    public void Configure(EntityTypeBuilder<PromissoryItem> builder)
    {
        builder.HasKey(x => new { x.DocumentId, x.LineId });

        builder.HasOne<Promissory>()
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.DocumentId)
            .IsRequired();        

        builder.Property(x => x.ItemCode)
                .HasMaxLength(100)
                .IsRequired();

        builder.Property(x => x.ItemName)
                .HasMaxLength(100)
                .IsRequired();

        builder.ToTable("PromissoryItems");
    }
}