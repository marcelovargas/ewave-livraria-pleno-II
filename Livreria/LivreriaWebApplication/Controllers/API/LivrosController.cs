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

namespace LivreriaWebApplication.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly ILivroApp _context;

        public LivrosController(ILivroApp context)
        {
            _context = context;
        }

        // GET: api/Livros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livro>>> GetLivros()
        {
            return await _context.List();
        }

        // GET: api/Livros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Livro>> GetLivro(int id)
        {
            var livro = await _context.GetEntityById(id);

            if (livro == null)
            {
                return NotFound();
            }

            return livro;
        }

        // PUT: api/Livros/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivro(int id, Livro livro)
        {
            if (id != livro.Id)
            {
                return BadRequest();
            }

           // _context.Entry(livro).State = EntityState.Modified;

            try
            {
                await _context.Update(livro);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await LivroExists(id))
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

        // POST: api/Livros
        [HttpPost]
        public async Task<ActionResult<Livro>> PostLivro(Livro livro)
        {
           await _context.Add(livro);         

            return CreatedAtAction("GetLivro", new { id = livro.Id }, livro);
        }

        // DELETE: api/Livros/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Livro>> DeleteLivro(int id)
        {
            var livro = await _context.GetEntityById(id);
            if (livro == null)
            {
                return NotFound();
            }

            await _context.Delete(livro);
           
            return livro;
        }

        private async Task<bool> LivroExists(int id)
        {
            var objeto = await _context.GetEntityById(id);

            return objeto != null;
        }
    }
}
