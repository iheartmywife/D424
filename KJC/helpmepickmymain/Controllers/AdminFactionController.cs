using helpmepickmymain.Database;
using helpmepickmymain.Models.Domain;
using helpmepickmymain.Models.ViewModels;
using helpmepickmymain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace helpmepickmymain.Controllers
{
    public class AdminFactionController : Controller
    {
        private readonly IRaceRepository raceRepository;
        private readonly IFactionRepository factionRepository;

        public AdminFactionController(IRaceRepository raceRepository, IFactionRepository factionRepository)
        {
            this.raceRepository = raceRepository;
            this.factionRepository = factionRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Add()
        {

            var races = await raceRepository.GetAllRacesAsync();

            var model = new AddFactionRequest
            {
                AvailableRaces = races.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),
            };
            return View(model);
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddFactionRequest addFactionRequest)
        {
            //map view model to domain model
            var faction = new Faction
            {
                Name = addFactionRequest.Name,
            };
            //getting all races
            var selectedRaces = new List<Race>();
            foreach (var selectedRaceId in addFactionRequest.SelectedRaces)
            {
                var AsGuid = Guid.Parse(selectedRaceId);
                var existingRace = await raceRepository.GetRaceAsync(AsGuid);

                if (existingRace != null)
                {
                    selectedRaces.Add(existingRace);
                }
            }
            faction.Races = selectedRaces;
            await factionRepository.AddFactionAsync(faction);

            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var factions = await factionRepository.GetAllFactionsAsync();
            return View(factions);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var currentFaction = await factionRepository.GetFactionAsync(id);
            var raceDomainModel = await raceRepository.GetAllRacesAsync();

            if (currentFaction != null)
            {
                var model = new EditFactionRequest
                {
                    Id = currentFaction.Id,
                    Name = currentFaction.Name,

                    AvailableRaces = raceDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString(),
                    }),
                    SelectedRaces = currentFaction.Races.Select(x => x.Id.ToString()).ToArray(),
                };

                return View(model);
            }
            else if (currentFaction == null)
            {
                Console.WriteLine($"Faction with id {id} not found");
                return NotFound();
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditFactionRequest editFactionRequest)
        {
            var factionDomainModel = new Faction
            {
                Id = editFactionRequest.Id,
                Name = editFactionRequest.Name,
            };

            var selectedRaces = new List<Race>();
            foreach (var selectedRace in editFactionRequest.SelectedRaces)
            {
                if (Guid.TryParse(selectedRace, out var race))
                {
                    var foundRace = await raceRepository.GetRaceAsync(race);

                    if (foundRace != null)
                    {
                        selectedRaces.Add(foundRace);
                    }
                }
            }

            factionDomainModel.Races = selectedRaces;

            var updatedFaction = await factionRepository.UpdateFactionAsync(factionDomainModel);

            if (updatedFaction != null)
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new { id = editFactionRequest.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditFactionRequest editFactionRequest)
        {
            try
            {

                var deletedFaction = await factionRepository.DeleteFactionAsync(editFactionRequest.Id);

                if (deletedFaction != null)
                {
                    return RedirectToAction("List");
                }
                return RedirectToAction("Edit", new { id = editFactionRequest.Id });
            }
            catch (Exception e)
            {
                string errormessage = "You cannot delete a faction that has a race assigned to it. Delete the race or remove the assigned race first";
                return RedirectToAction("Error", "Home", new { ErrorMessage = errormessage });
            }
        }
    }
}
