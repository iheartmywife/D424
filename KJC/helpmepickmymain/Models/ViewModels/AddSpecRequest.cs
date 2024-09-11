using helpmepickmymain.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace helpmepickmymain.Models.ViewModels
{
    public class AddSpecRequest
    {
        public string Name { get; set; }
        public Role Role { get; set; }
        public WowClass? WowClass { get; set; }
        public string WowheadLink { get; set; }

        // Display Available Roles / Classes

        public IEnumerable<SelectListItem> AvailableRoles { get; set; }
        public string SelectedRole { get; set; }
        public IEnumerable<SelectListItem> ?AvailableWowClasses { get; set; }
        public string ?SelectedWowClass { get; set; }

    }
}
