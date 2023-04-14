using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ProjectAnswersAndQuestions.Controllers
{
    public class AdminPageController : Controller
    {
        
        public async Task<IActionResult> AdminPage()
        {
            return View();
        }

  
    }
}
