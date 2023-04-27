using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FormDeneme.Areas.Identity.Data;
using FormDeneme.Models;
using Microsoft.AspNetCore.Authorization;

namespace FormDeneme.Controllers
{
    public class FormsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FormsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Forms
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return _context.Form != null ?
                          View(await _context.Form.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Form'  is null.");
        }

        public JsonResult FormList()
        {
            var data = _context.Form.ToList();
            return new JsonResult(data);
        }
        [HttpPost]
        public JsonResult AddForm(Form form)
        {
            var f = new Form()
            {
                Name = form.Name,
                Description = form.Description,
                CreatedAt = form.CreatedAt

            };
            _context.Form.Add(f);
            _context.SaveChanges();
            return new JsonResult("Veri Kaydedildi");
        }

        public async Task<IActionResult> Fields(int id)
        {
            return View();
        }

        // GET: Forms/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Form == null)
            {
                return NotFound();
            }

            var form = await _context.Form
                .FirstOrDefaultAsync(m => m.Id == id);
            if (form == null)
            {
                return NotFound();
            }

            return View(form);
        }

        // GET: Forms/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Forms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,CreatedAt")] Form form)
        {
            if (ModelState.IsValid)
            {
                _context.Add(form);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(form);
        }

        // GET: Forms/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Form == null)
            {
                return NotFound();
            }

            var form = await _context.Form.FindAsync(id);
            if (form == null)
            {
                return NotFound();
            }
            return View(form);
        }

        // POST: Forms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CreatedAt")] Form form)
        {
            if (id != form.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(form);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormExists(form.Id))
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
            return View(form);
        }

        // GET: Forms/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Form == null)
            {
                return NotFound();
            }

            var form = await _context.Form
                .FirstOrDefaultAsync(m => m.Id == id);
            if (form == null)
            {
                return NotFound();
            }

            return View(form);
        }

        // POST: Forms/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Form == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Form'  is null.");
            }
            var form = await _context.Form.FindAsync(id);
            if (form != null)
            {
                _context.Form.Remove(form);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormExists(int id)
        {
          return (_context.Form?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
