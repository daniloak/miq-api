using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Omini.Miq.Domain.Authentication;
using Omini.Miq.Domain.Common;

internal abstract class DocumentEntityMapping<T> : IEntityTypeConfiguration<T> where T : DocumentEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.CreatedBy)
            .IsRequired();

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UpdatedBy);
    }
}