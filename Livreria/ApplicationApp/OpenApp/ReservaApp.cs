﻿namespace ApplicationApp.OpenApp
{
    using ApplicationApp.Interfaces;
    using Domain;
    using Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ReservaApp : IReservaApp
    {
        IReserva _IReserva;
        public ReservaApp(IReserva IReservaApp)
        {
            _IReserva = IReservaApp;
        }
        public async Task Add(Reserva Objeto)
        {
            await _IReserva.Add(Objeto);
        }

        public async Task<Mensagem> AddUnique(Reserva objeto)
        {
           return  await _IReserva.AddUnique(objeto);
        }

        public async Task Delete(Reserva Objeto)
        {
            await _IReserva.Delete(Objeto);
        }

        public Reserva GetEntityById(int Id)
        {
            return _IReserva.GetEntityById(Id);
           
        }


        public async Task<List<Reserva>> List()
        {
            var list = await _IReserva.List();
            if (list != null)
            {
                return list;
            }
            else
            {
                return new List<Reserva>();
            }
        }

        public IList<ReservaView> List(string leitor)
        {
           return _IReserva.List(leitor);
        }

        

        public async Task Update(Reserva Objeto)
        {
            await _IReserva.Update(Objeto);
        }

        IList<LivroView> IReservaApp.ListOfDetails()
        {
            return _IReserva.ListOfDetails();
        }
    }
}
