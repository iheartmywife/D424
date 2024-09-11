using helpmepickmymain.Models.Domain;
using helpmepickmymain.Models.ViewModels;
using helpmepickmymain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace helpmepickmymain.Controllers
{
    public class AdminSpecController : Controller
    {
        private readonly IRoleRepository roleRepository;
        private readonly ISpecRepository specRepository;
        private readonly IWowClassRepository wowClassRepository;

        public AdminSpecController(IRoleRepository roleRepository, ISpecRepository specRepository, IWowClassRepository wowClassRepository) //TO-DO ADD CLASS REPO
        {
            this.roleRepository = roleRepository;
            this.specRepository = specRepository;
            this.wowClassRepository = wowClassRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            // get Roles and classes from Repository
            var roles = await roleRepository.GetAllRolesAsync();
            var wowClasses = await wowClassRepository.GetAllWowClassesAsync();

            var model = new AddSpecRequest
            {
                AvailableRoles = roles.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.Id.ToString() }),
                AvailableWowClasses = wowClasses.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddSpecRequest addSpecRequest)
        {
            //map view model to domain model
            var spec = new Spec
            {
                Name = addSpecRequest.Name,
                WowheadLink = addSpecRequest.WowheadLink,
            };
            var selectedRoleId = Guid.Parse(addSpecRequest.SelectedRole);
            var selectedRole = await roleRepository.GetRoleAsync(selectedRoleId);

            if (selectedRole != null)
            {
                spec.Role = selectedRole;
            }

            //getting all classes
            var selectedWowClassId = Guid.Parse(addSpecRequest.SelectedWowClass);
            var selectedWowClass = await wowClassRepository.GetWowClassAsync(selectedWowClassId);

            if (selectedWowClass != null)
            {
                spec.WowClass = selectedWowClass;
            }

            //mapping spec back to domain model
            await specRepository.AddSpecAsync(spec);

            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var Specs = await specRepository.GetAllSpecsAsync();
            return View(Specs);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var currentSpec = await specRepository.GetSpecAsync(id);
            var roleDomainModel = await roleRepository.GetAllRolesAsync();
            var wowClassDomainModel = await wowClassRepository.GetAllWowClassesAsync();

            if (currentSpec != null)
            {
                var model = new EditSpecRequest
                {
                    Id = currentSpec.Id,
                    Name = currentSpec.Name,
                    Role = currentSpec.Role,
                    WowheadLink = currentSpec.WowheadLink,
                    AvailableRoles = roleDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString(),
                        Selected = (x.Id == currentSpec.Role.Id)
                    }),
                    SelectedRole = currentSpec.Role.Id.ToString(),


                    AvailableWowClasses = wowClassDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString(),
                        Selected = (x.Id == currentSpec.WowClass.Id)
                    }),
                    SelectedWowClass = currentSpec.WowClass.Id.ToString(),
                };

                return View(model);
            }
            else if (currentSpec == null)
            {
                Console.WriteLine($"Spec with id {id} not found");
                return NotFound();
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditSpecRequest editSpecRequest)
        {
            var selectedRole = await roleRepository.GetRoleAsync(Guid.Parse(editSpecRequest.SelectedRole));
            var specDomainModel = new Spec
            {
                Id = editSpecRequest.Id,
                Name = editSpecRequest.Name,
                Role = selectedRole,
                WowClass = editSpecRequest.WowClass,
                WowheadLink = editSpecRequest.WowheadLink,
            };

            var updatedSpec = await specRepository.UpdateSpecAsync(specDomainModel);

            if (updatedSpec != null)
            {
                //show success notification
                return RedirectToAction("List");
            }

            //show error notification
            return RedirectToAction("Edit", new { id = editSpecRequest.Id});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditSpecRequest editSpecRequest)
        {
            //talk to repo and delete this spec
            var deletedSpec = await specRepository.DeleteSpecAsync(editSpecRequest.Id);

            if (deletedSpec != null)
            {
                //show success notification
                return RedirectToAction("List");
            }
            //show error notification
            return RedirectToAction("Edit", new { id = editSpecRequest.Id });
            //display response
        }
    }

}
