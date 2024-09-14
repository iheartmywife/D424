using helpmepickmymain.Models.Domain;
using helpmepickmymain.Models.ViewModels;
using helpmepickmymain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace helpmepickmymain.Controllers
{
    public class AdminWowClassController : Controller
    {
        private readonly IRaceRepository raceRepository;
        private readonly ISpecRepository specRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IWowClassRepository wowClassRepository;

        public AdminWowClassController(IRaceRepository raceRepository, ISpecRepository specRepository, IRoleRepository roleRepository, IWowClassRepository wowClassRepository)
        {
            this.raceRepository = raceRepository;
            this.specRepository = specRepository;
            this.roleRepository = roleRepository;
            this.wowClassRepository = wowClassRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var roles = await roleRepository.GetAllRolesAsync();
            var races = await raceRepository.GetAllRacesAsync();
            var specs = await specRepository.GetAllSpecsAsync();

            var model = new AddWowClassRequest
            {
                AvailableRoles = roles.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.Id.ToString() }),
                AvailableSpecs = specs.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),
                AvailableRaces = races.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddWowClassRequest addWowClassRequest)
        {
            //map view model to domain model
            var wowClass = new WowClass
            {
                Name = addWowClassRequest.Name,
            };
            //getting all races
            var selectedRaces = new List<Race>();
            foreach (var selectedRaceId in addWowClassRequest.SelectedRaces)
            {
                var AsGuid = Guid.Parse(selectedRaceId);
                var existingRace = await raceRepository.GetRaceAsync(AsGuid);

                if (existingRace != null)
                {
                    selectedRaces.Add(existingRace);
                }
            }
            wowClass.Races = selectedRaces;

            //getting all Specs
            var selectedSpecs = new List<Spec>();
            foreach (var selectedSpecId in addWowClassRequest.SelectedSpecs)
            {
                var AsGuid = Guid.Parse(selectedSpecId);
                var existingSpec = await specRepository.GetSpecAsync(AsGuid);

                if (existingSpec != null)
                {
                    selectedSpecs.Add(existingSpec);
                }
            }
            wowClass.Specs = selectedSpecs;

            //getting all Specs
            var selectedRoles = new List<Role>();
            foreach (var selectedRoleId in addWowClassRequest.SelectedRoles)
            {
                var AsGuid = Guid.Parse(selectedRoleId);
                var existingRole = await roleRepository.GetRoleAsync(AsGuid);

                if (existingRole != null)
                {
                    selectedRoles.Add(existingRole);
                }
            }
            wowClass.Roles = selectedRoles;

            await wowClassRepository.AddWowClassAsync(wowClass);

            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var wowClasses = await wowClassRepository.GetAllWowClassesAsync();
            return View(wowClasses);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var currentWowClass = await wowClassRepository.GetWowClassAsync(id);
            var roleDomainModel = await roleRepository.GetAllRolesAsync();
            var specDomainModel = await specRepository.GetAllSpecsAsync();
            var raceDomainModel = await raceRepository.GetAllRacesAsync();

            if (currentWowClass != null)
            {
                var model = new EditWowClassRequest
                {
                    Id = currentWowClass.Id,
                    Name = currentWowClass.Name,
                    AvailableRaces = raceDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString(),
                    }),
                    SelectedRaces = currentWowClass.Races.Select(x => x.Id.ToString()).ToArray(),
                    AvailableRoles = roleDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString(),
                    }),
                    SelectedRoles = currentWowClass.Roles.Select(x => x.Id.ToString()).ToArray(),
                    AvailableSpecs = specDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString(),
                    }),
                    SelectedSpecs = currentWowClass.Specs.Select(x => x.Id.ToString()).ToArray(),
                };

                return View(model);
            }
            else if (currentWowClass == null)
            {
                Console.WriteLine($"Class with id {id} not found");
                return NotFound();
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditWowClassRequest editWowClassRequest)
        {
            var wowClassDomainModel = new WowClass
            {
                Id = editWowClassRequest.Id,
                Name = editWowClassRequest.Name,
            };

            //map all races
            var selectedRaces = new List<Race>();
            foreach (var selectedRace in editWowClassRequest.SelectedRaces)
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
            wowClassDomainModel.Races = selectedRaces;

            //map all roles
            var selectedRoles = new List<Role>();
            foreach (var selectedRole in editWowClassRequest.SelectedRoles)
            {
                if (Guid.TryParse(selectedRole, out var role))
                {
                    var foundRole = await roleRepository.GetRoleAsync(role);

                    if (foundRole != null)
                    {
                        selectedRoles.Add(foundRole);
                    }
                }
            }
            wowClassDomainModel.Roles = selectedRoles;

            //map all Specs
            var selectedSpecs = new List<Spec>();
            foreach (var selectedSpec in editWowClassRequest.SelectedSpecs)
            {
                if (Guid.TryParse(selectedSpec, out var spec))
                {
                    var foundSpec = await specRepository.GetSpecAsync(spec);

                    if (foundSpec != null)
                    {
                        selectedSpecs.Add(foundSpec);
                    }
                }
            }
            wowClassDomainModel.Specs = selectedSpecs;


            var updatedWowClass = await wowClassRepository.UpdateWowClassAsync(wowClassDomainModel);

            if (updatedWowClass != null)
            {
                //show success notification
                return RedirectToAction("List");
            }

            //show error notification
            return RedirectToAction("Edit", new { id = editWowClassRequest.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditWowClassRequest editWowClassRequest)
        {
            try
            {

                //talk to repo and delete this race
                var deletedWowClass = await wowClassRepository.DeleteWowClassAsync(editWowClassRequest.Id);

                if (deletedWowClass != null)
                {
                    //show success notification
                    return RedirectToAction("List");
                }
                //show error notification
                return RedirectToAction("Edit", new { id = editWowClassRequest.Id });
                //display response
            }
            catch (Exception e)
            {
                string errormessage = "You cannot delete a class that has a spec assigned to it. Delete the spec and remove the assigned faction and races first";
                return RedirectToAction("Error", "Home", new { ErrorMessage = errormessage});
            }
        }
    }
}
