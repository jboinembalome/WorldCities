using WorldCities.Domain.Common;
using WorldCities.Domain.Interfaces;

namespace WorldCities.Domain.Entities
{
    public class City : AuditableEntity, IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name_ASCII { get; set; }
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
        public Country Country { get; set; }
    }
}
