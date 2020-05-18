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
    using Microsoft.AspNetCore.Authorization;

    
    public class EmprestimosController : Controller
    {
        private readonly IEmprestimoApp _IEmprestimoApp;
        private readonly IReservaApp _IReservaApp;

        public EmprestimosController(IEmprestimoApp emprestimo, IReservaApp reserva)
        {
            _IEmprestimoApp = emprestimo;
            _IReservaApp = reserva;
        }


        // GET: Emprestimos       
        public async Task<IActionResult> Index(int page = 1, string sortExpression = "Data")
        {
            var qry = _IEmprestimoApp.ListofReserved();
            var model = PagingList.Create(qry, 5, page, sortExpression, "Data");
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
        public IActionResult Create()
        {
            // ViewData["IdLetor"] = new SelectList(_context.Leitores, "Id", "Id");
            // ViewData["IdLivro"] = new SelectList(_context.Livros, "Id", "Id");
            return View();
        }

        // POST: Emprestimos/Create

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,DInicio,DFIm,IdLeitor,IdLivro")] Emprestimo emprestimo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _context.Add(emprestimo);

        //        return RedirectToAction(nameof(Index));
        //    }
        //    //ViewData["IdLetor"] = new SelectList(_context.Leitores, "Id", "Id", emprestimo.IdLetor);
        //    //ViewData["IdLivro"] = new SelectList(_context.Livros, "Id", "Id", emprestimo.IdLivro);
        //    return View(emprestimo);
        //}

        // GET: Emprestimos/Send/5
        //public async Task<IActionResult> Send(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var emprestimo = await _context.GetEntityById((int)id);
        //    if (emprestimo == null)
        //    {
        //        return NotFound();
        //    }
        //    // ViewData["IdLetor"] = new SelectList(_context.Leitores, "Id", "Id", emprestimo.IdLetor);
        //    // ViewData["IdLivro"] = new SelectList(_context.Livros, "Id", "Id", emprestimo.IdLivro);
        //    return View(emprestimo);
        //}

        //// POST: Emprestimos/Send/5

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Send(int id, [Bind("Id,DInicio,DFIm,IdLetor,IdLivro")] Emprestimo emprestimo)
        //{
        //    if (id != emprestimo.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            await _context.Update(emprestimo);

        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!await EmprestimoExists(emprestimo.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    //ViewData["IdLetor"] = new SelectList(_context.Leitores, "Id", "Id", emprestimo.IdLetor);
        //    //ViewData["IdLivro"] = new SelectList(_context.Livros, "Id", "Id", emprestimo.IdLivro);
        //    return View(emprestimo);
        //}

      
       
        public async Task<IActionResult> CancelConfirmed(int? id)
        {
            var reserva = await _IReservaApp.GetEntityById((int)id);
            reserva.Ativo = false;
            await _IReservaApp.Update(reserva);
            return RedirectToAction(nameof(Index));
        }

        //private async Task<bool> EmprestimoExists(int id)
        //{
        //    var objeto = await _context.GetEntityById(id);

        //    return objeto != null;
        //}
    }
}
