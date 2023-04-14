using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ProjectAnswersAndQuestions.Controllers
{
    public class AboutPageController : Controller
    {
        public async Task<IActionResult> Aboutpage()
        {          
            return View();
        }
    }
}