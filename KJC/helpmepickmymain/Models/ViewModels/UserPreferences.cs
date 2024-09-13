using System.ComponentModel.DataAnnotations;

namespace helpmepickmymain.Models.ViewModels
{
    public class UserPreferences
    {
        public string SelectedSpecs { get; set; }
        public  string UtilityPreference { get; set; }
        public string DurabilityPreference { get; set; }
        public  string RotationStylePreference { get; set; }
        public  string ClassFantasyPreference { get; set; }
        public  string MobilityPreference { get; set; }
        public  string SpecStrengthPreference { get; set; }

    }
}
