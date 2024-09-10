using helpmepickmymain.Database;
using helpmepickmymain.Models.Domain;
using helpmepickmymain.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace helpmepickmymain.Controllers
{
    public class AdminRoleController : Controller
    {
        private readonly HmpmmDbContext hmpmmDbContext;

        public AdminRoleController(HmpmmDbContext hmpmmDbContext)
        {
            this.hmpmmDbContext = hmpmmDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(AddRoleRequest addRoleRequest)
        {
            var role = new Role
            {
                Name = addRoleRequest.Name,
                DisplayName = addRoleRequest.DisplayName,
            };

            hmpmmDbContext.Roles.Add(role);
            hmpmmDbContext.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public IActionResult List()
        {
            var roles = hmpmmDbContext.Roles.ToList();


            return View(roles);
        }

        public IActionResult Edit(Guid id)
        {
            var role = hmpmmDbContext.Roles.FirstOrDefault(x => x.Id == id);

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
        public IActionResult Edit(EditRoleRequest editRoleRequest)
        {
            var role = new Role
            {
                Id = editRoleRequest.Id,
                Name = editRoleRequest.Name,
                DisplayName = editRoleRequest.DisplayName,
            };

            var existingRole = hmpmmDbContext.Roles.Find(role.Id);
            if (existingRole != null)
            {
                existingRole.Name = role.Name;
                existingRole.DisplayName = role.DisplayName;

                hmpmmDbContext.SaveChanges();
                //show success notification
                return RedirectToAction("Edit", new { id = editRoleRequest.Id });
            }
            //show failure notification
            return RedirectToAction("Edit", new { id = editRoleRequest.Id });
        }

        [HttpPost]
        public IActionResult Delete(EditRoleRequest editRoleRequest)
        {
            var role = hmpmmDbContext.Roles.Find(editRoleRequest.Id);

            if (role != null)
            {
                hmpmmDbContext.Roles.Remove(role);
                hmpmmDbContext.SaveChanges();

                //show success notification
                return RedirectToAction("List");
            }

            //show error notifcation
            return RedirectToAction("Edit", new { id = editRoleRequest.Id });
        }
    }
}
