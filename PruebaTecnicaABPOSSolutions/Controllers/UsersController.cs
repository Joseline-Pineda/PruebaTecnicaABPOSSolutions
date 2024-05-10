using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaABPOSSolutions.Data;
using PruebaTecnicaABPOSSolutions.Inputs;
using PruebaTecnicaABPOSSolutions.Models;

namespace PruebaTecnicaABPOSSolutions.Controllers
{
    public class UsersController : Controller
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UsersController(ApplicationDbContext dbContext, UserManager<User> userManager, IMapper mapper)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _mapper = mapper;
        }

        // GET: UsersController
        public ActionResult Index()
        {
            return View(_dbContext.Users.ToList());
        }

        [HttpGet]
        public ActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Nombres,Apellidos,UserName,PasswordHash,IsAdmin")] User user)
        {
            if (!ModelState.IsValid) return View();
            await _userManager.CreateAsync(user);
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<UserInput>(user));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Nombres,Apellidos,UserName,IsAdmin")] UserInput user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(user);
            try
            {
                var userOld = _dbContext.Users.FirstOrDefault(u => u.Id == id);
                _mapper.Map(user, userOld);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!UserExists(user.Id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Negocios/Delete/5
        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed([FromRoute] string id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user != null)
            {
                if (!_dbContext.Negocios?.Any(n => n.UserId == id)??false)
                {
                    _dbContext.Users.Remove(user);
                }
               
            }

            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }

}
