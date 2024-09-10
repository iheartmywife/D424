namespace helpmepickmymain.Models.Domain
{
    public class Spec
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
        public WowClass WowClass { get; set; }
        public string WowheadLink { get; set; }
    }
}
