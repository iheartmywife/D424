namespace helpmepickmymain.Models.Domain
{
    public class Faction
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Race> Races { get; set; }
    }
}
