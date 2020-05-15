namespace LivreriaWebApplication.Controllers
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

    public class LivrosController : Controller
    {
        private readonly ILivroApp _context;
        
        public LivrosController(ILivroApp context)
        {
            _context = context;
        }

        // GET: Livros
        public async Task<IActionResult> Index()
        {
            var contextBase = _context.List(); //.Include(l => l.Autor).Include(l => l.Genero);
            return View(await contextBase);
        }

        // GET: Livros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           
            var livro = await _context.GetEntityById((int)id);
            //.Include(l => l.Autor)
            //.Include(l => l.Genero)
            //.FirstOrDefaultAsync(m => m.Id == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // GET: Livros/Create
        public IActionResult Create()
        {
            //ViewData["IdAutor"] = new SelectList(_context.Set<Autor>(), "Id", "Id");
            //ViewData["IdGenero"] = new SelectList(_context.Set<Genero>(), "Id", "Id");
            return View();
        }

        // POST: Livros/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,IdGenero,Sipnose,Capa,IdAutor")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                livro.IdAutor = 1;
                livro.IdGenero = 1;
                await _context.Add(livro);
                return RedirectToAction(nameof(Index));
            }
            //  ViewData["IdAutor"] = new SelectList(_context.Set<Autor>(), "Id", "Id", livro.IdAutor);
            //  ViewData["IdGenero"] = new SelectList(_context.Set<Genero>(), "Id", "Id", livro.IdGenero);
            return View(livro);
        }

        // GET: Livros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           
            var livro = await _context.GetEntityById((int)id);
            if (livro == null)
            {
                return NotFound();
            }
            // ViewData["IdAutor"] = new SelectList(_context.Set<Autor>(), "Id", "Id", livro.IdAutor);
            // ViewData["IdGenero"] = new SelectList(_context.Set<Genero>(), "Id", "Id", livro.IdGenero);
            return View(livro);
        }

        // POST: Livros/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,IdGenero,Sipnose,Capa,IdAutor")] Livro livro)
        {
            if (id != livro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Update(livro);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await LivroExists(livro.Id))
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
            //  ViewData["IdAutor"] = new SelectList(_context.Set<Autor>(), "Id", "Id", livro.IdAutor);
            //  ViewData["IdGenero"] = new SelectList(_context.Set<Genero>(), "Id", "Id", livro.IdGenero);
            return View(livro);
        }

        // GET: Livros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var livro = await _context.GetEntityById((int)id);
            //.Include(l => l.Autor)
            //.Include(l => l.Genero)
            //.FirstOrDefaultAsync(m => m.Id == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // POST: Livros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livro = await _context.GetEntityById(id);
            await _context.Delete(livro);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> LivroExists(int id)
        {
            var objeto = await _context.GetEntityById(id);

            return objeto != null;

        }
    }
}
