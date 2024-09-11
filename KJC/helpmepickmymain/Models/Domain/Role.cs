namespace helpmepickmymain.Models.Domain
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public IEnumerable<WowClass> WowClasses { get; set; }
        public IEnumerable<Spec> Specs { get; set; }
    }
}
