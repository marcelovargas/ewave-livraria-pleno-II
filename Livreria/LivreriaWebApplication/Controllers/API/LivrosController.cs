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
    /// <summary>
    /// Livros a serem emprestados.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly ILivroApp _context;

        /// <summary>
        /// Cosntructor
        /// </summary>
        /// <param name="context"></param>
        public LivrosController(ILivroApp context)
        {
            _context = context;
        }

        // GET: api/Livros
        /// <summary>
        /// Obtenção da lista de livros.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livro>>> GetLivros()
        {
            return await _context.List();
        }

        // GET: api/Livros/5
        /// <summary>
        /// Obtençaõ de un cadastro de um livro, associado a um Id exclusivo.
        /// </summary>
        /// <param name="id">Id exclusivo de livro.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Alteração de um cadastro de livro, associado a um Id exclusivo.
        /// </summary>
        /// <param name="id">Id exclusivo de livro.</param>
        /// <param name="livro">Corpo de dados de livro.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivro(int id, Livro livro)
        {
            if (id != livro.Id)
            {
                return BadRequest();
            }

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
        /// <summary>
        /// Criação de um cadastro de livro.
        /// </summary>
        /// <param name="livro">Corpo de dados de livro.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Livro>> PostLivro(Livro livro)
        {
           await _context.Add(livro);         

            return CreatedAtAction("GetLivro", new { id = livro.Id }, livro);
        }

        // DELETE: api/Livros/5
        /// <summary>
        /// Excluisão de um cadastro de livro, associado a um Id exclusivo.
        /// </summary>
        /// <param name="id">Id exclusivo de livro.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Informa se existe um castrado., associado a um Id exclusivo.
        /// </summary>
        /// <param name="id">Id exclusivo de livro.</param>
        /// <returns></returns>
        private async Task<bool> LivroExists(int id)
        {
            var objeto = await _context.GetEntityById(id);

            return objeto != null;
        }
    }
}
