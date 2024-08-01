using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using smartfinance.Domain.Entities;
using smartfinance.Domain.Enums;
using smartfinance.Infra.Data.Data.Configuration.Shared;

namespace smartfinance.Infra.Data.Data.Configuration
{
    public class CategoryConfiguration : ConfigurationBase<Category>, IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(p => p.Name)
               .HasMaxLength(450)
               .IsRequired();

            builder.Property(p => p.Type).HasConversion(
              p => p.ToString(),
              p => (MovementType)Enum.Parse(typeof(Type), p))
              .HasMaxLength(1);
        }
    }
}
