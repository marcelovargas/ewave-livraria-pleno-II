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

        public async Task<Mensagem> AddWithControl(Emprestimo emprestimo)
        {
            return await _IEmprestimo.AddWithControl(emprestimo);

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
            return await _IEmprestimo.List();
            
        }

        public IList<ReservaView> ListofReserved(string option)
        {
           return _IEmprestimo.ListofReserved(option);
        }

        public IList<EmprestimoView> ListWithDetails(string filter)
        {
            return _IEmprestimo.ListWithDetails(filter);
        }

        public async Task Update(Emprestimo Objeto)
        {
             await _IEmprestimo.Update(Objeto);
        }
         
        public async Task<Mensagem> Update_msg(Emprestimo Objeto)
        {
           return  await _IEmprestimo.Update_msg(Objeto);
        }
    }
}
