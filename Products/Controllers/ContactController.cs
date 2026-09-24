using Microsoft.AspNetCore.Mvc;
using Products.Infrastructure;
using Products.Models;
using Products.ViewModels;

namespace Products.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationContext _context;

        public ContactController(ApplicationContext context)
        {
            _context = context;
        }
        

        [HttpPost]
        public async Task<IActionResult> SendMessage(ContactMessageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var message = new ContactMessage
                {
                    Name = model.Name,
                    Phone = model.Phone,
                    Message = model.Message,
                    CreatedAt = DateTime.Now
                };

                _context.ContactMessages.Add(message);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Your message has been sent successfully!";
                return RedirectToAction("Index", "Home");
            }

            TempData["Error"] = "❌ Please fill in all required fields.";
            return RedirectToAction("Index", "Home");
           
        }


    }

}
