using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ProjectAnswersAndQuestions.Controllers
{
    public class ContactPage : Controller
    {
        public async Task<IActionResult> Contactpage()
        {
            return View();
        }
    }
}
