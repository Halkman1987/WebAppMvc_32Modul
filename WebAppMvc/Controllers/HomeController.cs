using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppMvc.Models;
using WebAppMvc.Models.Db;

namespace WebAppMvc.Controllers
{
    public class HomeController : Controller
    {
        private ILogger<HomeController> _logger;
        private IBlogRepository _blogRepository;

        public HomeController(ILogger<HomeController> logger, IBlogRepository blogRepository)
        {
            _logger = logger;
            _blogRepository = blogRepository;
        }

        public async Task <IActionResult> Index()
        {
            var newUser = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Andrey",
                LastName = "Petrov",
                JoinDate = DateTime.Now
            };
            await _blogRepository.AddUser(newUser);
            Console.WriteLine($" User with id {newUser.Id} , named {newUser.FirstName} was successfully added on {newUser.JoinDate}");
            return View();
        }
        public async Task <IActionResult> Autors()
        {
            var autors = await _blogRepository.GetUsers();
            Console.WriteLine("See all blog autors");
            foreach (var autor in autors)
                Console.WriteLine($"autor name {autor.FirstName }, joined {autor.JoinDate}");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}