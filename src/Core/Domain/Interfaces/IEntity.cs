namespace WorldCities.Domain.Interfaces
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
    }
}
