using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorldCities.Domain.Entities;

namespace WorldCities.Infrastructure.Persistence.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.Property(a => a.Name)
                .IsRequired();

            builder.Property(a => a.ISO2)
                .IsRequired();

            builder.Property(a => a.ISO3)
                .IsRequired();
        }
    }
}
