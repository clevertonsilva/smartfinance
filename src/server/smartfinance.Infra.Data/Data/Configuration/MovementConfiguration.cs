using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using smartfinance.Domain.Entities;
using smartfinance.Domain.Enums;
using smartfinance.Infra.Data.Data.Configuration.Shared;

namespace smartfinance.Infra.Data.Data.Configuration
{
    public class MovementConfiguration : ConfigurationBase<Movement>, IEntityTypeConfiguration<Movement>
    {
        public void Configure(EntityTypeBuilder<Movement> builder)
        {
            builder.Property(p => p.Amount)
               .HasColumnType("decimal(13, 2)")
               .IsRequired();

            builder.Property(p => p.Description)
               .HasColumnType("text")
               .IsRequired();

            builder.HasOne(p => p.Account)
                .WithMany(m => m.Movements)
                .HasForeignKey(p => p.AccountId)
                .IsRequired();

            builder.HasOne(o => o.Category)
                .WithOne(o => o.Movement)
                .HasForeignKey<Movement>(s => s.CategoryId)
                .IsRequired();

            builder.Property(p => p.Type).HasConversion(
             p => p.ToString(),
             p => (MovementType)Enum.Parse(typeof(Type), p))
             .HasMaxLength(1);
        }
    }
}
