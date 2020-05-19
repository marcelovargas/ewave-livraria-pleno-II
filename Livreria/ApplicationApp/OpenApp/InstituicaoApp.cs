namespace ApplicationApp.OpenApp
{
    using ApplicationApp.Interfaces;
    using Domain;
    using Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class InstituicaoApp : IInstituicaoApp
    {
        IInstituicao _IInstituicao;
        public InstituicaoApp(IInstituicao IInstituicao)
        {
            _IInstituicao = IInstituicao;
        }
        public async Task Add(Instituicao Objeto)
        {
            await _IInstituicao.Add(Objeto);
        }

        public async Task Delete(Instituicao Objeto)
        {
            await _IInstituicao.Delete(Objeto);
        }

        public Instituicao GetEntityById(int Id)
        {
            return _IInstituicao.GetEntityById(Id);
            
        }


        public async Task<List<Instituicao>> List()
        {
            var list = await _IInstituicao.List();
            if (list != null)
            {
                return list;
            }
            else
            {
                return new List<Instituicao>();
            }
        }

        public IList<Instituicao> List(string filter)
        {
            return _IInstituicao.List(filter);
        }

        public async Task Update(Instituicao Objeto)
        {
            await _IInstituicao.Update(Objeto);
        }
    }
}
