using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaABPOSSolutions.Data;
using PruebaTecnicaABPOSSolutions.Inputs;
using PruebaTecnicaABPOSSolutions.Models;
using PruebaTecnicaABPOSSolutions.ViewModels;

namespace PruebaTecnicaABPOSSolutions.Controllers
{
    public class NegociosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public NegociosController(ApplicationDbContext context,
            IMapper mapper,
            UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: Negocios
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var negocios = await _context.Negocios.Include(n => n.User).ToListAsync();
            if (!user.IsAdmin)
            {
                negocios = negocios.Where(n => n.UserId == user.Id).ToList();
            }
            return View(_mapper.Map<IEnumerable<NegocioViewModel>>(negocios));
        }

        // GET: Negocios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Negocios == null)
            {
                return NotFound();
            }

            var negocio = await _context.Negocios
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (negocio == null)
            {
                return NotFound();
            }

            return View(negocio);
        }

        // GET: Negocios/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Nombres");
            return View();
        }

        // POST: Negocios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Descripcion,UserId")] NegocioInput negocio)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var nuevoNegocio = _mapper.Map<Negocio>(negocio);
                nuevoNegocio.FechaCreacion = DateTime.Now;
                if (!user.IsAdmin) 
                {
                    nuevoNegocio.UserId = user.Id;
                }
               
                _context.Add(nuevoNegocio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", negocio.UserId);
            return View(negocio);
        }

        // GET: Negocios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Negocios == null)
            {
                return NotFound();
            }

            var negocio = await _context.Negocios.FindAsync(id);
            if (negocio == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Nombres", negocio.UserId);
            return View(_mapper.Map<NegocioInput>(negocio));
        }

        // POST: Negocios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,FechaCreacion,UserId")] NegocioInput negocio)
        {
            if (id != negocio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (!user.IsAdmin)
                    {
                        negocio.UserId = user.Id;
                    }
                    _context.Update(_mapper.Map<Negocio>(negocio));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NegocioExists(negocio.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", negocio.UserId);
            return View(negocio);
        }

      

        // POST: Negocios/Delete/5
        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed([FromRoute]int id)
        {
            if (_context.Negocios == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Negocios'  is null.");
            }
            var negocio = await _context.Negocios.FindAsync(id);
            if (negocio != null)
            {
                _context.Negocios.Remove(negocio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NegocioExists(int id)
        {
          return (_context.Negocios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
