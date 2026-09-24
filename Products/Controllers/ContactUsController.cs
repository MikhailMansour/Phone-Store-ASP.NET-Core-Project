using Microsoft.AspNetCore.Mvc;

namespace Products.Controllers
{
    public class ContactUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
