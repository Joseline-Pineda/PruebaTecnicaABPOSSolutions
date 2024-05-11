using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaABPOSSolutions.Data;
using PruebaTecnicaABPOSSolutions.Inputs;
using PruebaTecnicaABPOSSolutions.Models;

namespace PruebaTecnicaABPOSSolutions.Controllers
{
    public class MenusController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public MenusController(ApplicationDbContext context, 
            IMapper mapper,  
            UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: Menus
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var menus = await _context.Menus!.Include(m => m.Categoria).Include(m => m.Negocio).ToListAsync();
            if (user.IsAdmin) return View(menus);
            var negocios = await _context.Negocios!.Include(n => n.User).Where(n => n.UserId == user.Id).ToListAsync();
            menus = menus.Where(m=> negocios.Any(n=>n.Id == m.NegocioId)).ToList();
            return View(menus);
        }

        // GET: Menus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Menus == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .Include(m => m.Categoria)
                .Include(m => m.Negocio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // GET: Menus/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            var negocios = await _context.Negocios!.Include(n => n.User).ToListAsync();
            if (!user.IsAdmin)
            {
                negocios = negocios.Where(n => n.UserId == user.Id).ToList();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre");
            ViewData["NegocioId"] = new SelectList(negocios, "Id", "Nombre");
            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Precio,CategoriaId,NegocioId")] MenuInput menu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(_mapper.Map<Menu>(menu));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var user = await _userManager.GetUserAsync(User);
            var negocios = await _context.Negocios!.Include(n => n.User).ToListAsync();
            if (!user.IsAdmin)
            {
                negocios = negocios.Where(n => n.UserId == user.Id).ToList();
            }

            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", menu.CategoriaId);
            ViewData["NegocioId"] = new SelectList(negocios, "Id", "Nombre", menu.NegocioId);
            return View(menu);
        }

        // GET: Menus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Menus == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            var negocios = await _context.Negocios!.Include(n => n.User).ToListAsync();
            if (!user.IsAdmin)
            {
                negocios = negocios.Where(n => n.UserId == user.Id).ToList();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", menu.CategoriaId);
            ViewData["NegocioId"] = new SelectList(negocios, "Id", "Nombre", menu.NegocioId);
            return View(menu);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,Precio,CategoriaId,NegocioId")] Menu menu)
        {
            if (id != menu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var user = await _userManager.GetUserAsync(User);
            var negocios = await _context.Negocios!.Include(n => n.User).ToListAsync();
            if (!user.IsAdmin)
            {
                negocios = negocios.Where(n => n.UserId == user.Id).ToList();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", menu.CategoriaId);
            ViewData["NegocioId"] = new SelectList(negocios, "Id", "Nombre", menu.NegocioId);
            return View(menu);
        }

       
        // POST: Menus/Delete/5
        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Menus == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Menus'  is null.");
            }
            var menu = await _context.Menus.FindAsync(id);
            if (menu != null)
            {
                _context.Menus.Remove(menu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(int id)
        {
          return (_context.Menus!.FirstOrDefault(e => e.Id == id) != null);
        }
    }
}
