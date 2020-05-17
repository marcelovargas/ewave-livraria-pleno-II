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
    using ReflectionIT.Mvc.Paging;
    using Microsoft.AspNetCore.Routing;

    public class GenerosController : Controller
    {
        private readonly IGeneroApp _context;

        public GenerosController(IGeneroApp context)
        {
            _context = context;
        }

        // GET: Generos
        /// <summary>
        /// Retorna uma lista de generos
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sortExpression"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string filter, int page = 1, string sortExpression = "Nome")
        {
            var list = _context.List(filter);

            var model = PagingList.Create(list, 5, page, sortExpression, "Nome");
            model.RouteValue = new RouteValueDictionary
            {  { "filter", filter}    };
            return View(model);

        }

        // GET: Generos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genero = await _context.GetEntityById((int)id);

            if (genero == null)
            {
                return NotFound();
            }

            return View(genero);
        }

        // GET: Generos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Generos/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Ativo")] Genero genero)
        {
            if (ModelState.IsValid)
            {
                await _context.Add(genero);
                return RedirectToAction(nameof(Index));
            }
            return View(genero);
        }

        // GET: Generos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genero = await _context.GetEntityById((int)id);
            if (genero == null)
            {
                return NotFound();
            }
            return View(genero);
        }

        // POST: Generos/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Ativo")] Genero genero)
        {
            if (id != genero.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Update(genero);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await GeneroExists(genero.Id))
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
            return View(genero);
        }

        // GET: Generos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genero = await _context.GetEntityById((int)id);
            if (genero == null)
            {
                return NotFound();
            }

            return View(genero);
        }

        // POST: Generos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var genero = await _context.GetEntityById(id);
            await _context.Delete(genero);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GeneroExists(int id)
        {
            var objeto = await _context.GetEntityById(id);

            return objeto != null;
        }
    }
}
