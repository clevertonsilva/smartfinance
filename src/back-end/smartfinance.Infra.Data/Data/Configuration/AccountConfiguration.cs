using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using smartfinance.Domain.Entities;
using smartfinance.Infra.Data.Data.Configuration.Shared;

namespace smartfinance.Infra.Data.Data.Configuration
{
    public class AccountConfiguration : ConfigurationBase<Account>, IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
             builder.Property(p => p.Name)
                .HasMaxLength(450)
                .IsRequired();

            builder.Property(p => p.LastName)
                .HasMaxLength(450)
                .IsRequired();

            builder.Property(p => p.Email)
               .HasMaxLength(350)
               .IsRequired();

            builder.Property(p => p.CellPhone)
               .HasMaxLength(20);

            builder.Property(p => p.IsDeleted)
               .HasDefaultValue(false)
               .IsRequired();

            builder.Property(p => p.IsActive)
               .HasDefaultValue(false)
               .IsRequired();
        }
    }
}
