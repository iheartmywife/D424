using helpmepickmymain.Database;
using helpmepickmymain.Models.Domain;
using helpmepickmymain.Models.ViewModels;
using helpmepickmymain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace helpmepickmymain.Controllers
{
    public class AdminRoleController : Controller
    {
        private readonly IRoleRepository roleRepository;

        public AdminRoleController(IRoleRepository roleRepository)
        {;
            this.roleRepository = roleRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddRoleRequest addRoleRequest)
        {
            var role = new Role
            {
                Name = addRoleRequest.Name,
                DisplayName = addRoleRequest.DisplayName,
            };

            await roleRepository.AddRoleAsync(role);

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var roles = await roleRepository.GetAllRolesAsync();


            return View(roles);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var role = await roleRepository.GetRoleAsync(id);

            if (role != null)
            {
                var editRoleRequest = new EditRoleRequest
                {
                    Id = role.Id,
                    Name = role.Name,
                    DisplayName = role.DisplayName,
                };
                return View(editRoleRequest);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRoleRequest editRoleRequest)
        {
            var role = new Role
            {
                Id = editRoleRequest.Id,
                Name = editRoleRequest.Name,
                DisplayName = editRoleRequest.DisplayName,
            };

            var updatedRole = await roleRepository.UpdateRoleAsync(role);

            //For Success/Failure Notifications should we add them later

            if (updatedRole != null)
            {
            }
            else
            {
            }

            return RedirectToAction("Edit", new { id = editRoleRequest.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditRoleRequest editRoleRequest)
        {
            var deletedRole = await roleRepository.DeleteRoleAsync(editRoleRequest.Id);

            if (deletedRole != null)
            {

                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new { id = editRoleRequest.Id });
        }
    }
}
