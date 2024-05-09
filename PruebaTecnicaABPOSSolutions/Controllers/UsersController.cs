using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaABPOSSolutions.Data;
using PruebaTecnicaABPOSSolutions.Models;

namespace PruebaTecnicaABPOSSolutions.Controllers
{
    public class UsersController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _dbContext;

        public UsersController(SignInManager<User> signInManager, ApplicationDbContext dbContext)
        {
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        // GET: UsersController
        public ActionResult Index()
        {
            return View(_dbContext.Users.ToList());
        }
    }
}
