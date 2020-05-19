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
    /// Instituiação 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class InstituicoesController : ControllerBase
    {
        private readonly IInstituicaoApp _context;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="context"></param>
        public InstituicoesController(IInstituicaoApp context)
        {
            _context = context;
        }

        // GET: api/Instituicoes
        /// <summary>
        /// Obtenção da lista de instituições.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instituicao>>> GetInstituicoes()
        {
            return await _context.List();
        }

        // GET: api/Instituicoes/5
        /// <summary>
        /// Obtenção de un cadastro de instituição, associado a um Id exclusivo.
        /// </summary>
        /// <param name="id">Id exclusivo de instituição.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Instituicao>> GetInstituicao(int id)
        {
            var instituicao =  _context.GetEntityById(id);

            if (instituicao == null)
            {
                return NotFound();
            }

            return instituicao;
        }

        // PUT: api/Instituicoes/5
        /// <summary>
        /// Alteração de un cadastro de instituição, associado a um Id exclusivo.
        /// </summary>
        /// <param name="id">Id exclusivo de instituição.</param>
        /// <param name="instituicao">Corpo de dados de instituição.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstituicao(int id, Instituicao instituicao)
        {
            if (id != instituicao.Id)
            {
                return BadRequest();
            }

            try
            {
                await _context.Update(instituicao);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await InstituicaoExists(id))
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

        // POST: api/Instituicoes
        /// <summary>
        /// Criação de un cadastro de instituição.
        /// </summary>
        /// <param name="instituicao">Corpo de dados de instuitição.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Instituicao>> PostInstituicao(Instituicao instituicao)
        {
            await _context.Add(instituicao);

            return CreatedAtAction("GetInstituicao", new { id = instituicao.Id }, instituicao);
        }

        // DELETE: api/Instituicoes/5
        /// <summary>
        /// Exclusão de un cadastro de instituição, associado a um Id exclusivo.
        /// </summary>
        /// <param name="id">Id exclusivo de instituição.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Instituicao>> DeleteInstituicao(int id)
        {
            var instituicao =  _context.GetEntityById(id);
            if (instituicao == null)
            {
                return NotFound();
            }

            await _context.Delete(instituicao);

            return instituicao;
        }

        /// <summary>
        /// Informa se existe um castrado, associado a um Id exclusivo.
        /// </summary>
        /// <param name="id">Id exclusivo de instituição.</param>
        /// <returns></returns>
        private async Task<bool> InstituicaoExists(int id)
        {
            var objeto =  _context.GetEntityById(id);

            return objeto != null;
        }
    }
}
