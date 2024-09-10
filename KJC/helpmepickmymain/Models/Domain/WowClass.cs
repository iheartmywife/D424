namespace helpmepickmymain.Models.Domain
{
    public class WowClass
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Spec> Specs { get; set; }
        public ICollection<Race> Races { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}
