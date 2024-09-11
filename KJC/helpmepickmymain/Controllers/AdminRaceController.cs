using helpmepickmymain.Models.Domain;
using helpmepickmymain.Models.ViewModels;
using helpmepickmymain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace helpmepickmymain.Controllers
{
    public class AdminRaceController : Controller
    {
        private readonly IRaceRepository raceRepository;
        private readonly IFactionRepository factionRepository;

        public AdminRaceController(IRaceRepository raceRepository, IFactionRepository factionRepository) //TO-DO: IMPLEMENT FACTION AND CLASS REPOS
        {
            this.raceRepository = raceRepository;
            this.factionRepository = factionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //UNCOMMENT WHEN YOU IMPLEMENT THE CORRESPONDING CLASS

            var factions = await factionRepository.GetAllFactionsAsync();
            //var wowClasses = await wowClassesRepository.GetAllWowClassesAsync();

            var model = new AddRaceRequest
            {
                AvailableFactions = factions.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),
                //AvailableWowClasses = wowClasses.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.Id.ToString() })
            };
            //TO DO ADD MODEL TO THE VIEW
            return View(model);
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddRaceRequest addRaceRequest)
        {
            //map view model to domain model
            var race = new Race
            {
                Name = addRaceRequest.Name,
            };
            ////getting all classes
            //var selectedWowClasses = new List<WowClass>();
            //foreach (var selectedWowClassId in addRaceRequest.SelectedWowClasses)
            //{
            //    var AsGuid = Guid.Parse(selectedWowClassId);
            //    var existingWowClass = await classRepository.GetClassAsync(AsGuid);

            //    if (existingWowClass != null)
            //    {
            //        selectedWowClasses.Add(existingWowClass);
            //    }
            //}
            //race.WowClasses = selectedWowClasses;

            //getting selectedFaction
            var selectedFactionId = Guid.Parse(addRaceRequest.SelectedFaction);

            var selectedFaction = await factionRepository.GetFactionAsync(selectedFactionId);

            if (selectedFaction != null)
            {
                race.Faction = selectedFaction;
            }
            //todo: Add in class!

            //mapping spec back to domain model
            await raceRepository.AddRaceAsync(race);

            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var Races = await raceRepository.GetAllRacesAsync();
            return View(Races);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var currentRace = await raceRepository.GetRaceAsync(id);
            var factionDomainModel = await factionRepository.GetAllFactionsAsync();
            //var wowClassDomainModel = await WowClassRepository.GetAllRacesAsync();

            if (currentRace != null)
            {
                var model = new EditRaceRequest
                {
                    Id = currentRace.Id,
                    Name = currentRace.Name,
                    Faction = currentRace.Faction,
                    AvailableFactions = factionDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString(),
                        Selected = (x.Id == currentRace.Faction.Id)
                    }),
                    SelectedFaction = currentRace.Faction.Id.ToString()

                    //WowClasses = currentSpec.WowClasses,
                    //AvailableClasses = wowClassDomainModel.Select(x => new SelectListItem
                    //{
                    //    Text = x.Name,
                    //    value = x.id.tostring(),
                    //}),
                    //SelectedWowClasses = currentRace.WowClasses.Select(x => x.Id.ToString()).ToArray(),
                };

                return View(model);
            }
            else if (currentRace == null)
            {
                Console.WriteLine($"Spec with id {id} not found");
                return NotFound();
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRaceRequest editRaceRequest)
        {
            var selectedFaction = await factionRepository.GetFactionAsync(Guid.Parse(editRaceRequest.SelectedFaction));
            var raceDomainModel = new Race
            {
                Id = editRaceRequest.Id,
                Name = editRaceRequest.Name,
                Faction = selectedFaction,
            };

            //var selectedWowClasses = new List<WowClass>();
            //foreach (var selectedWowClass in editRaceRequest.SelectedWowClasses)
            //{
            //    if (Guid.TryParse(selectedWowClass, out var wowClass))
            //    {
            //        var foundWowClass = await classRepository.GetWowClassAsync(wowClass);

            //        if (foundWowClass != null)
            //        {
            //            selectedWowClasses.Add(foundWowClass);
            //        }
            //    }
            //}

            //raceDomainModel.WowClasses = selectedWowClasses;

            var updatedRace = await raceRepository.UpdateRaceAsync(raceDomainModel);

            if (updatedRace != null)
            {
                //show success notification
                return RedirectToAction("List");
            }

            //show error notification
            return RedirectToAction("Edit", new { id = editRaceRequest.Id });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(EditRaceRequest editRaceRequest)
        {
            //talk to repo and delete this race
            var deletedRace = await raceRepository.DeleteRaceAsync(editRaceRequest.Id);

            if (deletedRace != null)
            {
                //show success notification
                return RedirectToAction("List");
            }
            //show error notification
            return RedirectToAction("Edit", new { id = editRaceRequest.Id });
            //display response
        }
    }
}
