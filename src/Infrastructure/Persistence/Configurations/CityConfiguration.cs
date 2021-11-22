using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorldCities.Domain.Entities;

namespace WorldCities.Infrastructure.Persistence.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.Property(b => b.Name)
               .IsRequired();

            builder.Property(b => b.Name_ASCII)
               .IsRequired();

            builder.Property(b => b.Lat)
               .IsRequired();

            builder.Property(b => b.Lon)
               .IsRequired();

        }
    }
}
