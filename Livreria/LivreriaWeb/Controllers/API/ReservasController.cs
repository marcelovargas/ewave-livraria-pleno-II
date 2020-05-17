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
    /// Reserva de Livros.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly IReservaApp _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public ReservasController(IReservaApp context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtenção da lista de reservas.
        /// </summary>
        /// <returns></returns>
        // GET: api/Reservas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva>>> GetReservas()
        {
            return await _context.List();
        }

        /// <summary>
        /// Obtenção de um cadastro de reserva de livro, associado a um Id exclusivo.
        /// </summary>
        /// <param name="id">Id exclusivo de reserva de livro.</param>
        /// <returns></returns>
        // GET: api/Reservas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> GetReserva(int id)
        {
            var reserva = await _context.GetEntityById(id);

            if (reserva == null)
            {
                return NotFound();
            }

            return reserva;
        }

        // PUT: api/Reservas/5
        /// <summary>
        /// Alteração de um cadastro de reserva de libro, associado a um Id exclusivo.
        /// </summary>
        /// <param name="id">Id excluivo de reserva de livro.</param>
        /// <param name="reserva">Corpo de dados de reserva de livro.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReserva(int id, Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return BadRequest();
            }


            try
            {
                await _context.Update(reserva);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ReservaExists(id))
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

        // POST: api/Reservas
        /// <summary>
        /// Criação de um cadastro de reserva de livro.
        /// </summary>
        /// <param name="reserva">Corpo de dados de reserva de livro.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Reserva>> PostReserva(Reserva reserva)
        {
            await _context.Add(reserva);

            return CreatedAtAction("GetReserva", new { id = reserva.Id }, reserva);
        }

        // DELETE: api/Reservas/5
        /// <summary>
        /// Exclusão de um cadastro de reserva de livro, associado a um Id exclusivo.
        /// </summary>
        /// <param name="id">Id esclusivo de reserva de livro.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Reserva>> DeleteReserva(int id)
        {
            var reserva = await _context.GetEntityById(id);
            if (reserva == null)
            {
                return NotFound();
            }

            await _context.Delete(reserva);
            return reserva;
        }

        /// <summary>
        /// Informa se existe um castrado., associado a um Id exclusivo.
        /// </summary>
        /// <param name="id">Id exclusivo de reserva de livro.</param>
        /// <returns></returns>
        private async Task<bool> ReservaExists(int id)
        {
            var objeto = await _context.GetEntityById(id);

            return objeto != null;
        }
    }
}
