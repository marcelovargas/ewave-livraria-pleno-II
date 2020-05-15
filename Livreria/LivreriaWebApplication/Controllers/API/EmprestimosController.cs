namespace LivreriaWebApplication.Controllers.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Entities;
    using Infrastructure.Configuration;
    using ApplicationApp.Interfaces;

    /// <summary>
    /// Emprestimos de livros.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmprestimosController : ControllerBase
    {
        private readonly IEmprestimoApp _context;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="context"></param>
        public EmprestimosController(IEmprestimoApp context)
        {
            _context = context;
        }

        // GET: api/Emprestimos
        /// <summary>
        /// Obtenção de lista de emprestimos de livro.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Emprestimo>>> GetEmprestimos()
        {
            return await _context.List();
        }

        // GET: api/Emprestimos/5
        /// <summary>
        ///  Obtenção de cadastro de emprestimo de livro, associado a um Id exclusivo.
        /// </summary>
        /// <param name="id">Id excluivo de emprestimo de livro.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Emprestimo>> GetEmprestimo(int id)
        {
            var emprestimo = await _context.GetEntityById(id);

            if (emprestimo == null)
            {
                return NotFound();
            }

            return emprestimo;
        }

        // PUT: api/Emprestimos/5
        /// <summary>
        /// Alteração de um cadastro de emprestimo de livro, associado a um Id exclusivo.
        /// </summary>
        /// <param name="id">Id esclusivo de emprestimo de livro.</param>
        /// <param name="emprestimo">Corpo de dados de emprestimo de livro.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmprestimo(int id, Emprestimo emprestimo)
        {
            if (id != emprestimo.Id)
            {
                return BadRequest();
            }
                        
            try
            {
                await _context.Update(emprestimo);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EmprestimoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Emprestimos
        /// <summary>
        /// Criacão de cadastro de emprestimo de livro.
        /// </summary>
        /// <param name="emprestimo">Corpo de dados de um emprestimo de livro.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Emprestimo>> PostEmprestimo(Emprestimo emprestimo)
        {
            await _context.Add(emprestimo);
            return CreatedAtAction("GetEmprestimo", new { id = emprestimo.Id }, emprestimo);
        }

        // DELETE: api/Emprestimos/5
        /// <summary>
        /// Exclusão de um cadastro de emprestimo de livro, associado a um Id exclusivo.
        /// </summary>
        /// <param name="id">Id exclusivo de emprestimo de livro.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Emprestimo>> DeleteEmprestimo(int id)
        {
            var emprestimo = await _context.GetEntityById(id);
            if (emprestimo == null)
            {
                return NotFound();
            }

            await _context.Delete(emprestimo);
            
            return emprestimo;
        }

        /// <summary>
        /// Informa se existe um castrado., associado a um Id exclusivo.
        /// </summary>
        /// <param name="id">Id exclusivo de emprestimo de livro.</param>
        /// <returns></returns>
        private async Task<bool> EmprestimoExists(int id)
        {
            var objeto = await _context.GetEntityById(id);

            return objeto != null;
        }
    }
}
