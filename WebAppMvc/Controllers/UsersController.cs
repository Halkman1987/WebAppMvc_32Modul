using Microsoft.AspNetCore.Mvc;
using WebAppMvc.Models;

namespace WebAppMvc.Controllers
{
    public class UsersController: Controller
    {
        public IBlogRepository _repo;

        public UsersController(IBlogRepository repo)
        {
            _repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            var autors = await _repo.GetUsers();
            return View(autors);
        }
        public async Task<IActionResult> Register()
        {
            return View();
        }
    }
}
