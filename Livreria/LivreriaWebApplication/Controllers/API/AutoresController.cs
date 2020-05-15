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
    /// Autores dos Livros.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly IAutorApp _context;

        /// <summary>
        /// Cosntructor
        /// </summary>
        /// <param name="context"></param>
        public AutoresController(IAutorApp context)
        {
            _context = context;
        }

        // GET: api/Autores
        /// <summary>
        /// Obtenção da lista de autores de livros.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autor>>> GetAutores()
        {
            return await _context.List();
        }

        // GET: api/Autores/5
        /// <summary>
        /// Obtenção de um cadastro de autor de livros, associado a um Id exclusivo.
        /// </summary>
        /// <param name="id">Id exclusivo de autor.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Autor>> GetAutor(int id)
        {
            var autor = await _context.GetEntityById(id);

            if (autor == null)
            {
                return NotFound();
            }

            return autor;
        }

        // PUT: api/Autores/5
        /// <summary>
        /// Alteração de um cadastro de autor de livros, associado a um Id exclusivo.
        /// </summary>
        /// <param name="id">Id exclusivo de autor.</param>
        /// <param name="autor">Corpo de dados de autor.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAutor(int id, Autor autor)
        {
            if (id != autor.Id)
            {
                return BadRequest();
            }

            try
            {
                await _context.Update(autor);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AutorExists(id))
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

        // POST: api/Autores
        /// <summary>
        /// Criação de um cadastro de autor de livros.
        /// </summary>
        /// <param name="autor">Corpo de dados de autor.</param>
        /// <returns></returns>
         [HttpPost]
        public async Task<ActionResult<Autor>> PostAutor(Autor autor)
        {
            await _context.Add(autor);
           
            return CreatedAtAction("GetAutor", new { id = autor.Id }, autor);
        }

        // DELETE: api/Autores/5
        /// <summary>
        /// Exclusão de um cadastro de autor de livros, associado a um Id exclusivo.
        /// </summary>
        /// <param name="id">Id exclusivo de autor.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Autor>> DeleteAutor(int id)
        {
            var autor = await _context.GetEntityById(id);
            if (autor == null)
            {
                return NotFound();
            }

           await _context.Delete(autor);
          
            return autor;
        }

        /// <summary>
        /// Informa se existe um castrado, associado a um Id exclusivo.
        /// </summary>
        /// <param name="id">Id Exclusivo de autor.</param>
        /// <returns></returns>
        private async Task<bool> AutorExists(int id)
        {
            var objeto = await _context.GetEntityById(id);

            return objeto != null;
        }
    }
}
