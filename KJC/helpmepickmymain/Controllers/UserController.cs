
using helpmepickmymain.Models.Domain;
using helpmepickmymain.Models.ViewModels;
using helpmepickmymain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace helpmepickmymain.Controllers
{
    public class UserController : Controller
    {
        private readonly IFactionRepository factionRepository;
        private readonly IRaceRepository raceRepository;
        private readonly IRoleRepository roleRepository;
        private readonly ISpecRepository specRepository;
        private readonly IWowClassRepository wowClassRepository;

        public UserController(IFactionRepository factionRepository, IRaceRepository raceRepository, IRoleRepository roleRepository, ISpecRepository specRepository, IWowClassRepository wowClassRepository)
        {
            this.factionRepository = factionRepository;
            this.raceRepository = raceRepository;
            this.roleRepository = roleRepository;
            this.specRepository = specRepository;
            this.wowClassRepository = wowClassRepository;
        }

        // USER ROUTE 1: AESTHETICS: FACTION->RACE->ROLE->CLASS
        [HttpGet]
        public async Task<IActionResult> AestheticsFactionSelect()
        {
            var factions = await factionRepository.GetAllFactionsAsync();

            var model = new User
            {
                PotentialFactionsList = factions.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString()  }),

            };
            HttpContext.Session.SetString("UserData", JsonConvert.SerializeObject(model));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AestheticsRaceSelect(User user)
        {
            var userDataJson = HttpContext.Session.GetString("UserData");
            if (userDataJson == null || user.SelectedFactions == null)
            {
                return RedirectToAction("AestheticsFactionSelect");
            }
            var existingUser = JsonConvert.DeserializeObject<User>(userDataJson);
            existingUser.SelectedFactions = user.SelectedFactions;

            var races = await raceRepository.GetAllRacesAsync();
            var selectedFactions = new List<Faction>();
            var potentialRaces = new List<Race>();

            foreach(var selectedFactionId in existingUser.SelectedFactions)
            {
                var AsGuid = Guid.Parse(selectedFactionId);
                var faction = await factionRepository.GetFactionAsync(AsGuid);
                selectedFactions.Add(faction);

                foreach (var race in races)
                {
                    if (race.Faction.Id == faction.Id)
                    {
                        potentialRaces.Add(race);
                    }
                }
            }
            var model = new User
            {
                PotentialFactionsList = selectedFactions.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),
                PotentialRacesList = potentialRaces.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };
            HttpContext.Session.SetString("UserData", JsonConvert.SerializeObject(model));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AestheticsRoleSelect(User user)
        {
            var userDataJson = HttpContext.Session.GetString("UserData");
            if (userDataJson == null || user.SelectedRaces == null)
            {
                return RedirectToAction("AestheticsRaceSelect");
            }
            var existingUser = JsonConvert.DeserializeObject<User>(userDataJson);
            existingUser.SelectedRaces = user.SelectedRaces;

            var wowClasses = new List<WowClass>();
            foreach (var race in existingUser.SelectedRaces)
            {
                var raceAsGuid = Guid.Parse(race);
                var _race = await raceRepository.GetRaceAsync(raceAsGuid);
                foreach (var playableClass in _race.WowClasses)
                {
                    if (!wowClasses.Contains(playableClass))
                    {
                        wowClasses.Add(playableClass);
                    }
                }
            }
            var playableRoles = new List<Role>();
            foreach (var wowClass in wowClasses)
            {
                var wowClassWithRoles = await wowClassRepository.GetWowClassAsync(wowClass.Id);

                foreach (var _role in wowClassWithRoles.Roles)
                {
                    if (!playableRoles.Contains(_role) && (_role.Name == "dps" || _role.Name == "tank" || _role.Name == "healer"))
                    {
                        playableRoles.Add(_role);
                    }
                }
            }

            var model = new User
            {
                PotentialWowClassesList = wowClasses.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),
                PotentialFactionsList = existingUser.PotentialFactionsList,
                PotentialRacesList = existingUser.PotentialRacesList,
                PotentialRolesList = playableRoles.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.Id.ToString() })
            };
            HttpContext.Session.SetString("UserData", JsonConvert.SerializeObject(model));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AestheticsWowClassSelect(User user)
        {
            var userDataJson = HttpContext.Session.GetString("UserData");
            if (userDataJson == null || user.SelectedRole == null)
            {
                return RedirectToAction("AestheticsRoleSelect");
            }
            var existingUser = JsonConvert.DeserializeObject<User>(userDataJson);
            existingUser.SelectedRole = user.SelectedRole;

            var selectedRoleAsGuid = Guid.Parse(existingUser.SelectedRole);
            var selectedRoleAsRole = await roleRepository.GetRoleAsync(selectedRoleAsGuid);

            var potentialWowClasses = await GetClassesForRole(existingUser);


            var model = new User
            {
                PotentialRacesList = existingUser.PotentialRacesList,
                SelectedRole = existingUser.SelectedRole,
                PotentialFactionsList = existingUser.PotentialFactionsList,
                PotentialWowClassesList = potentialWowClasses.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),
            };
            HttpContext.Session.SetString("UserData", JsonConvert.SerializeObject(model));

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AestheticsSpecSelect(User user)
        {
            var userDataJson = HttpContext.Session.GetString("UserData");
            if (userDataJson == null || user.SelectedWowClasses == null)
            {
                return RedirectToAction("AestheticsWowClassSelect");
            }
            var existingUser = JsonConvert.DeserializeObject<User>(userDataJson);
            existingUser.SelectedWowClasses = user.SelectedWowClasses;
            existingUser.SelectedRole = user.SelectedRole;

            var potentialSpecs = await GetSpecsForSelectedClasses(existingUser);

            var model = new User
            {
                PotentialRacesList = existingUser.PotentialRacesList,
                SelectedRole = existingUser.SelectedRole,
                PotentialFactionsList = existingUser.PotentialFactionsList,
                PotentialWowClassesList = existingUser.PotentialWowClassesList,
                PotentialSpecsList = potentialSpecs.Select(x => new SelectListItem { Text = (x.Name + " " + x.WowClass.Name), Value = x.Id.ToString() }),
            };
            HttpContext.Session.SetString("UserData", JsonConvert.SerializeObject(model));

            return View(model);

        }

        [HttpPost] 
        public async Task<IActionResult> AestheticsRemainingChoices(User user)
        {
            var userDataJson = HttpContext.Session.GetString("UserData");
            if (userDataJson == null || user.SelectedSpecs == null)
            {
                return RedirectToAction("AestheticsSpecSelect");
            }
            var existingUser = JsonConvert.DeserializeObject<User>(userDataJson);
            existingUser.SelectedSpecs = user.SelectedSpecs;

            var remainingSpecs = await GetRemainingSpecsData(existingUser);


            HttpContext.Session.SetString("RemainingSpecs", JsonConvert.SerializeObject(remainingSpecs));
            HttpContext.Session.SetString("UserData", JsonConvert.SerializeObject(existingUser));

            return View(remainingSpecs);
        }


        //COMMENTS TO EVALUATOR: TO BE DEVELOPED LATER AS A PASSION PROJECT.

        //[HttpGet]
        //public async Task<IActionResult> GameplayOnly()
        //{
        //    var roles = await roleRepository.GetAllRolesAsync();
        //    var specs = await specRepository.GetAllSpecsAsync();
        //    var wowClasses = await wowClassRepository.GetAllWowClassesAsync();

        //    var model = new User
        //    {
        //        PotentialSpecsList = specs.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),
        //        PotentialWowClassesList = wowClasses.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),
        //        PotentialRolesList = roles.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.Id.ToString() }),
        //    };
        //    HttpContext.Session.SetString("UserData", JsonConvert.SerializeObject(model));
        //    return View(model);
        //}

        [HttpGet]
        public async Task<IActionResult> Preferences()
        {
            var userDataJson = HttpContext.Session.GetString("RemainingSpecs");
            if (userDataJson == null)
            {
                return RedirectToAction("AestheticsRemainingChoices");
            }
            var remainingSpecs = JsonConvert.DeserializeObject<List<RemainingUserChoice>>(userDataJson);

            string specQuery = "I am trying to decide on my main for the current World of Warcraft season. I have narrowed my options down to: ";
            int specCount = remainingSpecs.Count();
            for (int i = 0; i < specCount - 1; i++)
            {
                specQuery += $"{remainingSpecs[i].Name} {remainingSpecs[i].WowClassName}, ";
            }
            specQuery += $"and {remainingSpecs[(specCount-1)].Name} {remainingSpecs[(specCount - 1)].WowClassName}.";

            var userPreferences = new UserPreferences
            {
                SelectedSpecs = specQuery,
            };

            return View(userPreferences);
        }





        // PRIVATE METHODS FOR DATABASE CRUD OPERATIONS
        private async Task<List<RemainingUserChoice>> GetRemainingSpecsData(User user)
        {
            var remainingSpecChoices = new List<RemainingUserChoice>();
            foreach (var spec in user.SelectedSpecs)
            {
                var asguid = Guid.Parse(spec);
                var _spec = await specRepository.GetSpecAsync(asguid);
                var remainingChoice = new RemainingUserChoice
                {
                    Name = _spec.Name,
                    WowClassName = _spec.WowClass.Name,
                    WowheadLink = _spec.WowheadLink,
                };
                remainingSpecChoices.Add(remainingChoice);
            }

            return remainingSpecChoices;
        }


        private async Task<List<WowClass>> GetClassesForRole(User user)
        {
            var selectedRoleAsGuid = Guid.Parse(user.SelectedRole);
            var selectedRoleAsRole = await roleRepository.GetRoleAsync(selectedRoleAsGuid);

            var potentialWowClasses = new List<WowClass>();

            foreach (var wowClass in user.PotentialWowClassesList)
            {
                var AsGuid = Guid.Parse(wowClass.Value);
                var _wowClass = await wowClassRepository.GetWowClassAsync(AsGuid);
                if (_wowClass.Roles.Contains(selectedRoleAsRole) && !potentialWowClasses.Contains(_wowClass))
                {
                    potentialWowClasses.Add(_wowClass);
                }
            }
            return potentialWowClasses;
        }

        private async Task<List<Spec>> GetSpecsForSelectedClasses(User user)
        {
            var selectedRoleAsGuid = Guid.Parse(user.SelectedRole);
            var selectedRoleAsRole = await roleRepository.GetRoleAsync(selectedRoleAsGuid);

            var potentialSpecs = new List<Spec>();
            foreach (var wowClass in user.SelectedWowClasses)
            {
                var debugWowClass = wowClass;

                var selectedWowClassAsGuid = Guid.Parse(wowClass);
                var selectedWowClassAsWowClass = await wowClassRepository.GetWowClassAsync(selectedWowClassAsGuid);
                foreach (var spec in selectedWowClassAsWowClass.Specs)
                {
                    var debugSpec = spec;
                    if (spec.Role == selectedRoleAsRole)
                    {
                        potentialSpecs.Add(spec);
                    }
                }
            }
            return potentialSpecs;
        }
    }
}
