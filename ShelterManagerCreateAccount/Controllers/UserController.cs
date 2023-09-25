using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class UserController : Controller
{
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(User model)
    {
        if (ModelState.IsValid)
        {
            // Here, you would typically save the user to a database or perform user registration logic.
            // For simplicity, we're not including database interactions in this example.

            return RedirectToAction("Login", "Account"); // Redirect to the login page after successful registration
        }

        return View(model);
    }
}
