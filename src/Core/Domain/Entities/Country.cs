using System.Collections.Generic;
using WorldCities.Domain.Common;
using WorldCities.Domain.Interfaces;

namespace WorldCities.Domain.Entities
{
    public class Country : AuditableEntity, IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ISO2 { get; set; }
        public string ISO3 { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}
