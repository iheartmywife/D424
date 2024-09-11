﻿using helpmepickmymain.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace helpmepickmymain.Models.ViewModels
{
    public class AddFactionRequest
    {
        public string Name { get; set; }
        public ICollection<Race> Races { get; set; }
        public IEnumerable<SelectListItem>? AvailableRaces { get; set; }
        public string?[] SelectedRaces { get; set; } = Array.Empty<string>();
    }
}
