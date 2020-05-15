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

    [Route("api/[controller]")]
    [ApiController]
    public class EmprestimosController : ControllerBase
    {
        private readonly IEmprestimoApp _context;

        public EmprestimosController(IEmprestimoApp context)
        {
            _context = context;
        }

        // GET: api/Emprestimos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Emprestimo>>> GetEmprestimos()
        {
            return await _context.List();
        }

        // GET: api/Emprestimos/5
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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmprestimo(int id, Emprestimo emprestimo)
        {
            if (id != emprestimo.Id)
            {
                return BadRequest();
            }

            // _context.Entry(emprestimo).State = EntityState.Modified;

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
        [HttpPost]
        public async Task<ActionResult<Emprestimo>> PostEmprestimo(Emprestimo emprestimo)
        {
            await _context.Add(emprestimo);
            return CreatedAtAction("GetEmprestimo", new { id = emprestimo.Id }, emprestimo);
        }

        // DELETE: api/Emprestimos/5
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

        private async Task<bool> EmprestimoExists(int id)
        {
            var objeto = await _context.GetEntityById(id);

            return objeto != null;
        }
    }
}
