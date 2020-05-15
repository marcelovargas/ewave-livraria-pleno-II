

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

    public class InstituicoesController : Controller
    {
        private readonly IInstituicaoApp _context;

        public InstituicoesController(IInstituicaoApp context)
        {
            _context = context;
        }

        // GET: Instituicoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.List());
        }

        // GET: Instituicoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituicao = await _context.GetEntityById((int)id);
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (instituicao == null)
            //{
            //    return NotFound();
            //}

            return View(instituicao);
        }

        // GET: Instituicoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Instituicoes/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Endereco,CPNJ,Telefone,Id,Nome,Ativo")] Instituicao instituicao)
        {
            if (ModelState.IsValid)
            {
               await _context.Add(instituicao);               
                return RedirectToAction(nameof(Index));
            }
            return View(instituicao);
        }

        // GET: Instituicoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituicao = await _context.GetEntityById((int)id);
            if (instituicao == null)
            {
                return NotFound();
            }
            return View(instituicao);
        }

        // POST: Instituicoes/Edit/5       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Endereco,CPNJ,Telefone,Id,Nome,Ativo")] Instituicao instituicao)
        {
            if (id != instituicao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Update(instituicao);
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await InstituicaoExists(instituicao.Id))
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
            return View(instituicao);
        }

        // GET: Instituicoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instituicao = await _context.GetEntityById((int)id);
               // .FirstOrDefaultAsync(m => m.Id == id);
            if (instituicao == null)
            {
                return NotFound();
            }

            return View(instituicao);
        }

        // POST: Instituicoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instituicao = await _context.GetEntityById(id);
           await _context.Delete(instituicao);
           
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> InstituicaoExists(int id)
        {
            var objeto = await _context.GetEntityById(id);

            return objeto != null;
        }
    }
}
