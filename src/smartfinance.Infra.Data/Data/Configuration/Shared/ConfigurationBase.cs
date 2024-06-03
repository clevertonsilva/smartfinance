using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using smartfinance.Domain.Entities.Shared;

namespace smartfinance.Infra.Data.Data.Configuration.Shared
{
    public class ConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntityBase
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(typeof(TEntity).Name);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedBy)
                   .HasMaxLength(350)
                    .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.ModifiedAt);

            builder.Property(x => x.ModifiedBy)
                 .HasMaxLength(350);
        }
    }
}
