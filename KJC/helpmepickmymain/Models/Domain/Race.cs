namespace helpmepickmymain.Models.Domain
{
    public class Race
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Faction Faction { get; set; }
        public ICollection<WowClass> WowClasses { get; set; }
    }
}
