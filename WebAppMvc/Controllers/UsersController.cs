using Microsoft.AspNetCore.Mvc;
using WebAppMvc.Models;
using WebAppMvc.Models.Db;

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
        
       [HttpGet]
        public IActionResult Register()
        {
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(User newUser)
        {
            await _repo.AddUser(newUser);
            return View(newUser);
        }
    }
}
