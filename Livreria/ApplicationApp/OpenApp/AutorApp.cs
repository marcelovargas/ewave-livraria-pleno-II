﻿namespace ApplicationApp.OpenApp
{
    using ApplicationApp.Interfaces;
    using Domain;
    using Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public class AutorApp : IAutorApp
    {
        IAutor _IAutor;
        public AutorApp(IAutor IAutor)
        {
            _IAutor = IAutor;
        }
        public async Task Add(Autor Objeto)
        {
            await _IAutor.Add(Objeto);
        }

        public async Task Delete(Autor Objeto)
        {
            await _IAutor.Delete(Objeto);
        }

        public  Autor GetEntityById(int Id)
        {
            return  _IAutor.GetEntityById(Id);
           
            
        }


        public async Task<List<Autor>> List()
        {
            return await _IAutor.List();
            
        }

        public IList<Autor> List(string filter)
        {
            return  _IAutor.List(filter);
        }

        public IList<Autor> ListWithOption(string option)
        {
           var list = _IAutor.ListWithOption(option);
            if (list != null)
            {
                return list;
            }
            else
            {
                return new List<Autor>();
            }
        }

        public async Task Update(Autor Objeto)
        {
            await _IAutor.Update(Objeto);
        }
    }
}
