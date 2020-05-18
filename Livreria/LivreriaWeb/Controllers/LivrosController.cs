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
    using LivreriaWeb.Helpers;
    using System.IO;
    using LivreriaWeb.Models;
    using Domain;
    using ReflectionIT.Mvc.Paging;
    using Microsoft.AspNetCore.Routing;
    using LivroView = Models.LivroView;

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
        public async Task<IActionResult> Index(string filter, int page = 1, string sortExpression = "Titulo")
        {
            var list = _ILivroApp.List(filter);

            var model = PagingList.Create(list, 5, page, sortExpression, "Titulo");
            model.RouteValue = new RouteValueDictionary
            {  { "filter", filter}    };
            return View(model);

        }

        // GET: Livros/Details/5
        /// <summary>
        /// Retorna um cadastro de um livro para ser mostrado, associado a um Id exclusivo.
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

            var view = ToView(livro);
            return View(view);
        }

        private LivroView ToView(Livro livro)
        {
            var autor = _IAutorApp.GetEntityById(livro.IdAutor);
            var genero = _IGeneroApp.GetEntityById(livro.IdGenero);

            var view = new LivroView()
            {
                Id = livro.Id,
                Autor = autor.Result.Nome,
                Genero = genero.Result.Nome,
                Ativo = livro.Ativo,
                Sipnose = livro.Sipnose,
                Capa = livro.Capa,
                Titulo = livro.Titulo,

            };


            return view;
        }

        // GET: Livros/Create
        /// <summary>
        /// Envia o formulario base de livro.
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            ShowViewData();
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

            ShowViewData();

            if (ModelState.IsValid)
            {
                livroViewModel.Capa = await FilesHelper.UploadPhoto(livroViewModel.ImageFile, string.Empty);

                if (string.IsNullOrEmpty(livroViewModel.Capa))
                {
                    TempData["message"] = " Faltou a imagem...";
                    return View(livroViewModel);
                }

                livro = ToLivro(livroViewModel);
                await _ILivroApp.Add(livro);
                return RedirectToAction(nameof(Index));
            }


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
            ShowViewData();
            var vm = ToViewModel(livro);
            return View(vm);
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
        public async Task<IActionResult> Edit(int id, LivroViewModel livroViewModel)
        {
            if (id != livroViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    livroViewModel.Capa = await FilesHelper.UploadPhoto(livroViewModel.ImageFile, livroViewModel.Capa);
                    var livro = ToLivro(livroViewModel);
                    await _ILivroApp.Update(livro);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await LivroExists(livroViewModel.Id))
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

            ShowViewData();
            return View(livroViewModel);
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


        private LivroViewModel ToViewModel(Livro l)
        {
            return new LivroViewModel()
            {
                Id = l.Id,
                Titulo = l.Titulo,
                Capa = l.Capa,
                IdAutor = l.IdAutor,
                IdGenero = l.IdGenero,
                Sipnose = l.Sipnose,

            };
        }

        /// <summary>
        /// Mostrar as Viewdatas nos formularios
        /// </summary>
        private void ShowViewData()
        {
            ViewData["IdAutor"] = new SelectList(_IAutorApp.ListWithOption("A"), "Id", "Nome");
            ViewData["IdGenero"] = new SelectList(_IGeneroApp.ListWithOption("A"), "Id", "Nome");
        }
    }
}
