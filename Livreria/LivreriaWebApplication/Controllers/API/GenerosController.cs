﻿namespace LivreriaWebApplication.Controllers.API
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
    public class GenerosController : ControllerBase
    {
        private readonly IGeneroApp _context;

        public GenerosController(IGeneroApp context)
        {
            _context = context;
        }

        // GET: api/Generos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genero>>> GetGeneros()
        {
            return await _context.List();
        }

        // GET: api/Generos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Genero>> GetGenero(int id)
        {
            var genero = await _context.GetEntityById(id);

            if (genero == null)
            {
                return NotFound();
            }

            return genero;
        }

        // PUT: api/Generos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenero(int id, Genero genero)
        {
            if (id != genero.Id)
            {
                return BadRequest();
            }

           // _context.Entry(genero).State = EntityState.Modified;

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
        [HttpPost]
        public async Task<ActionResult<Genero>> PostGenero(Genero genero)
        {
            await _context.Add(genero);
          
            return CreatedAtAction("GetGenero", new { id = genero.Id }, genero);
        }

        // DELETE: api/Generos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Genero>> DeleteGenero(int id)
        {
            var genero = await _context.GetEntityById(id);
            if (genero == null)
            {
                return NotFound();
            }

           await _context.Delete(genero);
          
            return genero;
        }

        private async Task<bool> GeneroExists(int id)
        {
            var objeto = await _context.GetEntityById(id);

            return objeto != null; 
        }
    }
}
