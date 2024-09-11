using helpmepickmymain.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace helpmepickmymain.Models.ViewModels
{
    public class AddWowClassRequest
    {
        public string Name { get; set; }
        public ICollection<Spec> Specs { get; set; }
        public IEnumerable<SelectListItem> AvailableSpecs { get; set; }
        public string[] SelectedSpecs { get; set; } = Array.Empty<string>();
        public ICollection<Race> Races { get; set; }
        public IEnumerable<SelectListItem> AvailableRaces { get; set; }
        public string[] SelectedRaces { get; set; } = Array.Empty<string>();
        public ICollection<Role> Roles { get; set; }
        public IEnumerable<SelectListItem> AvailableRoles { get; set; }
        public string[] SelectedRoles { get; set; } = Array.Empty<string>();
    }
}
