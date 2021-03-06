﻿namespace LivreriaWeb.Controllers
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
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Routing;


    [Authorize]
    public class EmprestimosController : Controller
    {
        private readonly IEmprestimoApp _IEmprestimoApp;
        private readonly IReservaApp _IReservaApp;
        private readonly ILivroApp _ILivroApp;
        private readonly ILeitorApp _ILeitorApp;
        private readonly IAutorApp _IAutorApp;

        public EmprestimosController(IEmprestimoApp emprestimo, IReservaApp reserva, ILivroApp livro, ILeitorApp leitor, IAutorApp autor)
        {
            _IEmprestimoApp = emprestimo;
            _IReservaApp = reserva;
            _ILivroApp = livro;
            _ILeitorApp = leitor;
            _IAutorApp = autor;
        }

        public IActionResult Lend()
        {
            return View();
        }

        // GET: Emprestimos       
        public async Task<IActionResult> Index(int page = 1, string sortExpression = "Data")
        {
            var option = "A";
            var qry = _IEmprestimoApp.ListofReserved(option);
            var model = PagingList.Create(qry, 5, page, sortExpression, "Data");

            return View(model);

        }

        /// <summary>
        /// Retorna a lista dos livros que forom emprestadas.
        /// para ser devueltos.
        /// </summary>
        /// <param name="filter"> filtro</param>
        /// <param name="page">pagina</param>
        /// <param name="sortExpression">ordem</param>
        /// <returns></returns>
        public async Task<IActionResult> BookReturn(string filter, int page = 1, string sortExpression = "Titulo")
        {
            var qry = _IEmprestimoApp.ListWithDetails(filter).Where(x => x.DFIm == null);
            var model = PagingList.Create(qry, 5, page, sortExpression, "Titulo");
            model.RouteValue = new RouteValueDictionary
            {  { "filter", filter}    };
            return View(model);

        }

        // GET: Emprestimos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var emprestimo = await _context.firs(id);
            //    //.Include(e => e.Letor)
            //    //.Include(e => e.Livro)
            //    //.FirstOrDefaultAsync(m => m.Id == id);
            //if (emprestimo == null)
            //{
            //    return NotFound();
            //}

            return View();
        }

        // GET: Emprestimos/Create
        public IActionResult Create(int id)
        {
            var reserva = _IReservaApp.GetEntityById(id);

            var view = ToView(reserva);

            return View(view);
        }

        //POST: Emprestimos/Create
        /// <summary>
        /// Cria um cadastro de emprestamo
        /// Atualiza o cadastro de reserva, para dizer que ja foi 
        /// atendida.
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservaView view)
        {
            var emprestimo = new Emprestimo()
            {

                DInicio = DateTime.Now,
                IdLeitor = view.IdLeitor,
                IdLivro = view.IdLivro,

            };
            try
            {
                var message = await _IEmprestimoApp.AddWithControl(emprestimo);
                TempData["title"] = message.Titulo;
                TempData["message"] = message.Corpo;
            }
            catch (Exception)
            {

                return RedirectToAction(nameof(Index));
            }

            
            var reserva = _IReservaApp.GetEntityById(view.Id);
            reserva.Ativo = false;
            await _IReservaApp.Update(reserva);

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Cancela uma reserva.
        /// False => que naõ esta ativa para ser atendida.
        /// True  => ativa para ser atendida.
        /// </summary>
        /// <param name="id">Id da Reserva</param>
        /// <returns></returns>
        public async Task<IActionResult> CancelConfirmed(int? id)
        {
            var reserva = _IReservaApp.GetEntityById((int)id);
            reserva.Ativo = false;
            await _IReservaApp.Update(reserva);
            TempData["title"] = "Info";
            TempData["message"] = "Reserva cancelada.";
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Cadastra a devolução do Livro.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> BookSave(int? id)
        {
            var emprestimo = _IEmprestimoApp.GetEntityById((int)id);
            emprestimo.DFIm = DateTime.Now;
            
            var message = await _IEmprestimoApp.Update_msg(emprestimo);

            TempData["title"] = message.Titulo;
            TempData["message"] = message.Corpo;

            return RedirectToAction(nameof(BookReturn));
        }

        /// <summary>
        /// Tranforma o dado para mostrar na view.
        /// </summary>
        /// <param name="reserva"></param>
        /// <returns></returns>
        private ReservaView ToView(Reserva reserva)
        {
            var livro = _ILivroApp.GetEntityById(reserva.IdLivro);
            var leitor = _ILeitorApp.GetEntityById(reserva.IdLeitor);
            var autor = _IAutorApp.GetEntityById(livro.IdAutor);

            var view = new ReservaView()
            {
                Id = reserva.Id,
                Data = reserva.Data,
                IdLeitor = reserva.IdLeitor,
                IdLivro = reserva.IdLivro,
                Ativo = reserva.Ativo,
                LeitorNome = leitor.Nome,

                Titulo = livro.Titulo,
                Capa = livro.Capa,
                Sipnose = livro.Sipnose,


            };

            return view;
        }


        private async Task<bool> EmprestimoExists(int id)
        {
            var objeto = _IEmprestimoApp.GetEntityById(id);

            return objeto != null;
        }
    }
}
