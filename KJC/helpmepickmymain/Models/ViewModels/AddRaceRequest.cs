using helpmepickmymain.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace helpmepickmymain.Models.ViewModels
{
    public class AddRaceRequest
    {
        public string Name { get; set; }
        public Faction? Faction { get; set; }
        public ICollection<WowClass>? WowClasses { get; set; }

        public IEnumerable<SelectListItem>? AvailableFactions { get; set; }
        public string ?SelectedFaction { get; set; }
        public IEnumerable<SelectListItem>? AvailableWowClasses { get; set; }
        public string[] ?SelectedWowClasses { get; set; } = Array.Empty<string>();
    }
}
