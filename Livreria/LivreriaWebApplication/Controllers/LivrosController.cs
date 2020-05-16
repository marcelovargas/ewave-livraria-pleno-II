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
    using LivreriaWebApplication.Helpers;
    using System.IO;
    using LivreriaWebApplication.Models;
    using Domain;

    /// <summary>
    /// Controller de livros.
    /// </summary>
    public class LivrosController : Controller
    {
        private readonly ILivroApp _ILivroApp;
       private readonly IAutorApp _IAutorApp;
        private readonly IGeneroApp _IGeneroApp;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context"></param>
        public LivrosController(ILivroApp livro, IAutorApp autor, IGeneroApp genero)
        {
            _ILivroApp = livro;
            _IAutorApp = autor;
            _IGeneroApp = genero;
        }

        // GET: Livros
        /// <summary>
        /// Retorna uma lista de livros.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var contextBase = _ILivroApp.List(); 
            return View(await contextBase);
        }

        // GET: Livros/Details/5
        /// <summary>
        /// Retorna un cadastro de um livro para ser mostrado, associado a um Id exclusivo.
        /// </summary>
        /// <param name="id">Id exclusivo de livro</param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _ILivroApp.GetEntityById((int)id);
           
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // GET: Livros/Create
        /// <summary>
        /// Envia o formulario base de livro.
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            ViewData["IdAutor"] = new SelectList(_IAutorApp.ListWithOption("A"), "Id", "Nome");
            //  ViewData["IdGenero"] = new SelectList(_context.Set<Genero>(), "Id", "Id");
            return View();
        }

        // POST: Livros/Create
        /// <summary>
        /// Cria um novo cadastro de livro.
        /// </summary>
        /// <param name="livroViewModel">Corpo de dados de livro.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LivroViewModel livroViewModel)
        {
            var livro = new Livro();
            if (ModelState.IsValid)
            {               
                livroViewModel.Capa = await FilesHelper.UploadPhoto(livroViewModel.ImageFile);

                livro = ToLivro(livroViewModel);
                await _ILivroApp.Add(livro);
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdAutor"] = new SelectList(_IAutorApp.ListWithOption("A"), "Id", "Nome");
           // ViewData["IdGenero"] = new SelectList(_IGeneroApp.ListWithOption("A"), "Id", "Nome");
            return View(livro);
        }

        
        // GET: Livros/Edit/5
        /// <summary>
        /// Retorna un cadastro de livro para alterar os dados, associado a um Id exclusivo.
        /// </summary>
        /// <param name="id">Id exclusivo de livro.</param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _ILivroApp.GetEntityById((int)id);
            if (livro == null)
            {
                return NotFound();
            }
            // ViewData["IdAutor"] = new SelectList(_context.Set<Autor>(), "Id", "Id", livro.IdAutor);
            // ViewData["IdGenero"] = new SelectList(_context.Set<Genero>(), "Id", "Id", livro.IdGenero);
            return View(livro);
        }

        // POST: Livros/Edit/5
        /// <summary>
        /// Executa a ação de alterar os cadastro de livro, associado a um Id exclusivo.
        /// </summary>
        /// <param name="id">Id exclusivo de livro</param>
        /// <param name="livro">Corpo de dados de livro.</param>
        /// <returns></returns>
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
                    await _ILivroApp.Update(livro);

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
        /// <summary>
        /// Retorna um cadastro de livro para ser excluido, associado a um Id exclusivo.
        /// </summary>
        /// <param name="id">id Exclusivo.</param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _ILivroApp.GetEntityById((int)id);
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
        /// <summary>
        /// Executa a açaõ de exclusão do cadastro de livro.
        /// </summary>
        /// <param name="id">Id exclusivo de livro.</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livro = await _ILivroApp.GetEntityById(id);
            await _ILivroApp.Delete(livro);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Verifica a existencia de um cadastro de livro, associado a um Id exclusivo..
        /// </summary>
        /// <param name="id">Id Exclusivo.</param>
        /// <returns></returns>
        private async Task<bool> LivroExists(int id)
        {
            var objeto = await _ILivroApp.GetEntityById(id);

            return objeto != null;

        }

        /// <summary>
        /// Transforma um objeto LivroViewModel a Livro.
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        private Livro ToLivro(LivroViewModel l)
        {
            return new Livro()
            {
                Id = l.Id,
                Titulo = l.Titulo,
                Capa = l.Capa,
                IdAutor = l.IdAutor,
                IdGenero = l.IdGenero,
                Sipnose = l.Sipnose,

            };
        }
    }
}
