using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Products.Models;
using Products.ViewModels.Authantication;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IO;

public class ProfileController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager; // أضفنا الـ SignInManager لتحديث الـ Cookie
    private readonly IWebHostEnvironment _env;

    public ProfileController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IWebHostEnvironment env)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _env = env;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound();

        var model = new EditProfileViewModel
        {
            UserName = user.UserName,
            ExistingImagePath = user.PhotoPath
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Index(EditProfileViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound();

        var existingUser = await _userManager.FindByNameAsync(model.UserName);
        if (existingUser != null && existingUser.Id != int.Parse(userId))
        {
            ModelState.AddModelError("UserName", "This username is already in use.");
            return View(model);
        }

        user.UserName = model.UserName;

        if (model.ProfileImage != null)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "images/users");
            Directory.CreateDirectory(uploadsFolder);
            var uniqueFileName = Guid.NewGuid() + Path.GetExtension(model.ProfileImage.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.ProfileImage.CopyToAsync(stream);
            }

            user.PhotoPath = "/images/users/" + uniqueFileName;
        }

        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            // تحديث حالة تسجيل الدخول ليتم تطبيق التغييرات (كالاسم والصورة) في الهيدر فوراً
            await _signInManager.RefreshSignInAsync(user);

            TempData["Message"] = "Profile updated successfully!";
            return RedirectToAction("Index");
        }

        foreach (var error in result.Errors)
            ModelState.AddModelError("", error.Description);

        return View(model);
    }
}