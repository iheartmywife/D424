using helpmepickmymain.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace helpmepickmymain.Models.ViewModels
{
    public class User
    {
        public IEnumerable<SelectListItem> PotentialRolesList { get; set; }
        public string SelectedRole { get; set; }
        public IEnumerable<SelectListItem> PotentialSpecsList { get; set; }
        public string[] SelectedSpecs { get; set; } = Array.Empty<string>();
        public IEnumerable<SelectListItem> PotentialWowClassesList { get; set; }
        public string[] SelectedWowClasses { get; set; } = Array.Empty<string>();
        public IEnumerable<SelectListItem> PotentialFactionsList { get; set; }
        public string[] SelectedFactions { get; set; } = Array.Empty<string>();
        public IEnumerable<SelectListItem> PotentialRacesList { get; set; }
        public string[] SelectedRaces { get; set; } = Array.Empty<string>();
    }
}
