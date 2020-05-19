namespace ApplicationApp.OpenApp
{
    using ApplicationApp.Interfaces;
    using Domain;
    using Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class EmprestimoApp : IEmprestimoApp
    {
        IEmprestimo _IEmprestimo;
        public EmprestimoApp(IEmprestimo IEmprestimo)
        {
            _IEmprestimo = IEmprestimo;
        }
        public async Task Add(Emprestimo Objeto)
        {
            await _IEmprestimo.Add(Objeto);
        }

        public async Task AddWithControl(Emprestimo emprestimo)
        {
            await _IEmprestimo.AddWithControl(emprestimo);
        }

        public async Task Delete(Emprestimo Objeto)
        {
            await _IEmprestimo.Delete(Objeto);
        }

        public  Emprestimo GetEntityById(int Id)
        {
            return _IEmprestimo.GetEntityById(Id);
           
        }


        public async Task<List<Emprestimo>> List()
        {
            var list = await _IEmprestimo.List();
            if (list != null)
            {
                return list;
            }
            else
            {
                return new List<Emprestimo>();
            }
        }

        public IList<ReservaView> ListofReserved(string option)
        {
           return _IEmprestimo.ListofReserved(option);
        }

        public async Task Update(Emprestimo Objeto)
        {
            await _IEmprestimo.Update(Objeto);
        }
    }
}
