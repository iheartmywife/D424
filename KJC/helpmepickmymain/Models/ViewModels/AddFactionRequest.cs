using helpmepickmymain.Models.Domain;

namespace helpmepickmymain.Models.ViewModels
{
    public class AddFactionRequest
    {
        public string Name { get; set; }
        public ICollection<Race> Races { get; set; }
    }
}
