using helpmepickmymain.AI;
using Microsoft.AspNetCore.Mvc;

namespace helpmepickmymain.Controllers
{
    public class TestController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly OpenAi openAi;

        public TestController(OpenAi openAi)
        {
            this.openAi = openAi;
        }

        public async Task<IActionResult> Test()
        {
            var response = await openAi.GetSpecRecommendationAsync("test", "test");
            ViewBag.Content = response;
            return View();
        }
    }
}
