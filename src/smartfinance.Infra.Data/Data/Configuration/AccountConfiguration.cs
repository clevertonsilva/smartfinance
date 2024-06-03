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
        }
    }
}
