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
    using ApplicationApp.OpenApp;
    using ReflectionIT.Mvc.Paging;

    public class ReservasController : Controller
    {
        private readonly IReservaApp _context;
        private readonly ILivroApp _contextLivros;
        private readonly ILeitorApp _contextUsuarios;



        public ReservasController(IReservaApp context)
        {
            _context = context;
        }

        //GET: Reservas
        public IActionResult Index(int page = 1, string sortExpression = "Titulo")
        {
            var qry = _context.ListOfDetails();
            var model = PagingList.Create(qry, 5, page, sortExpression, "Titulo");
            var leitor = "2";
            ViewBag.Carinho = _context.List(leitor);

            return View(model);

        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.List();
            //.Include(r => r.Leitor)
            //.Include(r => r.Livro)
            //.FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reservas/Create      
        /// <summary>
        /// lksdfj
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Create(int? id)
        {
            var objeto = new Reserva()
            {
                IdLivro = 1,
                Ativo = true,
                Data = DateTime.Now,
                IdLeitor = "2",

            };
            await _context.Add(objeto);
            return this.RedirectToAction("Index");
        }




        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.GetEntityById((int)id);
            if (reserva == null)
            {
                return NotFound();
            }
            // ViewData["IdLetor"] = new SelectList(_context.Leitores, "Id", "Id", reserva.IdLetor);
            // ViewData["IdLivro"] = new SelectList(_context.Livros, "Id", "Id", reserva.IdLivro);
            return View(reserva);
        }

        // POST: Reservas/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Data,Ativo,IdUsuario,IdLivro")] Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Update(reserva);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ReservaExists(reserva.Id))
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
            // ViewData["IdLetor"] = new SelectList(_context.Leitores, "Id", "Id", reserva.IdLetor);
            //  ViewData["IdLivro"] = new SelectList(_context.Livros, "Id", "Id", reserva.IdLivro);
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.GetEntityById((int)id);
            //.Include(r => r.Leitor)
            //.Include(r => r.Livro)
            //.FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.GetEntityById(id);
            await _context.Delete(reserva);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ReservaExists(int id)
        {
            var objeto = await _context.GetEntityById(id);

            return objeto != null;
        }
    }
}
