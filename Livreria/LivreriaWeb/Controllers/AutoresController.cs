namespace LivreriaWeb.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Entities;
    using Infrastructure.Configuration;
    using ApplicationApp.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using ReflectionIT.Mvc.Paging;
    using Microsoft.AspNetCore.Routing;

   [Authorize]
    public class AutoresController : Controller
    {
        private readonly IAutorApp _context;


        public AutoresController(IAutorApp context)
        {
            _context = context;
        }


        // GET: Autores        
        public async Task<IActionResult> Index(string filter, int page = 1, string sortExpression = "Nome")
        {
            var list = _context.List(filter);

            var model = PagingList.Create(list, 5, page, sortExpression, "Titulo");
            model.RouteValue = new RouteValueDictionary
            {  { "filter", filter}    };
            return View(model);

        }

        // GET: Autores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor =  _context.GetEntityById((int)id);
            // .FirstOrDefaultAsync(m => m.Id == id);
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // GET: Autores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autores/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Ativo")] Autor autor)
        {
            if (ModelState.IsValid)
            {
                await _context.Add(autor);

                return RedirectToAction(nameof(Index));
            }
            return View(autor);
        }

        // GET: Autores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor =  _context.GetEntityById((int)id);
            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);
        }

        // POST: Autores/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Ativo")] Autor autor)
        {
            if (id != autor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Update(autor);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await AutorExists(autor.Id))
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
            return View(autor);
        }

        // GET: Autores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor =  _context.GetEntityById((int)id);

            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // POST: Autores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var autor =  _context.GetEntityById(id);
            await _context.Delete(autor);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AutorExists(int id)
        {
            var objeto =  _context.GetEntityById(id);

            return objeto != null;
        }
    }
}
