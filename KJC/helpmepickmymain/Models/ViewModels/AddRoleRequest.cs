using helpmepickmymain.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace helpmepickmymain.Models.ViewModels
{
    public class AddRoleRequest
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
