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
    public class InstituicoesController : ControllerBase
    {
        private readonly IInstituicaoApp _context;

        public InstituicoesController(IInstituicaoApp context)
        {
            _context = context;
        }

        // GET: api/Instituicoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instituicao>>> GetInstituicoes()
        {
            return await _context.List();
        }

        // GET: api/Instituicoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Instituicao>> GetInstituicao(int id)
        {
            var instituicao = await _context.GetEntityById(id);

            if (instituicao == null)
            {
                return NotFound();
            }

            return instituicao;
        }

        // PUT: api/Instituicoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstituicao(int id, Instituicao instituicao)
        {
            if (id != instituicao.Id)
            {
                return BadRequest();
            }

          //  _context.Entry(instituicao).State = EntityState.Modified;

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
        [HttpPost]
        public async Task<ActionResult<Instituicao>> PostInstituicao(Instituicao instituicao)
        {
            await _context.Add(instituicao);
          
            return CreatedAtAction("GetInstituicao", new { id = instituicao.Id }, instituicao);
        }

        // DELETE: api/Instituicoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Instituicao>> DeleteInstituicao(int id)
        {
            var instituicao = await _context.GetEntityById(id);
            if (instituicao == null)
            {
                return NotFound();
            }

            await _context.Delete(instituicao);
           
            return instituicao;
        }

        private async Task<bool> InstituicaoExists(int id)
        {
            var objeto = await _context.GetEntityById(id);

            return objeto != null;
        }
    }
}
