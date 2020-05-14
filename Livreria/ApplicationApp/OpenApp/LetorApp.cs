namespace ApplicationApp.OpenApp
{
    using ApplicationApp.Interfaces;
    using Domain;
    using Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public class LetorApp : ILetorApp
    {
        ILetor _ILector;
        public LetorApp(ILetor ILetor)
        {
            _ILector = ILetor;
        }
        public async Task Add(Letor Objeto)
        {
            await _ILector.Add(Objeto);
        }

        public async Task Delete(Letor Objeto)
        {
            await _ILector.Delete(Objeto);
        }

        public async Task<Letor> GetEntityById(int Id)
        {
            var record = await _ILector.GetEntityById(Id);
            if (record != null)
            {
                return record;
            }
            else
            {
                return new Letor();
            }
        }


        public async Task<List<Letor>> List()
        {
            var list = await _ILector.List();
            if (list != null)
            {
                return list;
            }
            else
            {
                return new List<Letor>();
            }
        }

        public async Task Update(Letor Objeto)
        {
            await _ILector.Update(Objeto);
        }
    }
}

