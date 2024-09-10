using helpmepickmymain.Database;
using helpmepickmymain.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace helpmepickmymain.Controllers
{
    public class AdminFactionController : Controller
    {
        private readonly HmpmmDbContext hmpmmDbContext;

        public AdminFactionController(HmpmmDbContext hmpmmDbContext)
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
        public IActionResult Add(AddFactionRequest addFactionRequest)
        {


            return View("Add");
        }
    }
}
