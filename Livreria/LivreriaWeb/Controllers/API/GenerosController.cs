namespace LivreriaWeb.Controllers.API
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
    /// Generos dos livros.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        private readonly IGeneroApp _context;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public GenerosController(IGeneroApp context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtenção da lista dos generos de livros.
        /// </summary>
        /// <returns></returns>
        // GET: api/Generos
        [HttpGet]
        public ActionResult<IEnumerable<Genero>> GetGeneros()
        {
            var list = _context.List();
            return Ok(list);
        }

        // GET: api/Generos/5
        /// <summary>
        /// Obtenção de um cadastro de genero de livros, associado a um Id exclusivo.
        /// </summary>
        /// <param name="id">Id exclusivo de genero</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Genero>> GetGenero(int id)
        {
            var genero =  _context.GetEntityById(id);

            if (genero == null)
            {
                return NotFound();
            }

            return genero;
        }

        // PUT: api/Generos/5
        /// <summary>
        /// Alteração de cadastro de genero de livros, associado a um Id exclusivo.
        /// </summary>
        /// <param name="id">Id eclusivo de genero.</param>
        /// <param name="genero">Corpo de dados de genero.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenero(int id, Genero genero)
        {
            if (id != genero.Id)
            {
                return BadRequest();
            }


            try
            {
                await _context.Update(genero);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await GeneroExists(id))
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

        // POST: api/Generos
        /// <summary>
        /// Criação de um cadastro de um genero de livros.
        /// </summary>
        /// <param name="genero">Corpo de dados de genero.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Genero>> PostGenero(Genero genero)
        {
            await _context.Add(genero);

            return CreatedAtAction("GetGenero", new { id = genero.Id }, genero);
        }

        // DELETE: api/Generos/5
        /// <summary>
        /// Excluisão de um cadastro de um genero de livros, associado a um Id exclusivo.
        /// </summary>
        /// <param name="id">Id exclusivo de genero.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Genero>> DeleteGenero(int id)
        {
            var genero =  _context.GetEntityById(id);
            if (genero == null)
            {
                return NotFound();
            }

            await _context.Delete(genero);

            return genero;
        }

        /// <summary>
        /// Informa se existe um castrado de livros, associado a um Id exclusivo.
        /// </summary>
        /// <param name="id">Id exclusivo de genero.</param>
        /// <returns></returns>
        private async Task<bool> GeneroExists(int id)
        {
            var objeto =  _context.GetEntityById(id);

            return objeto != null;
        }
    }
}
