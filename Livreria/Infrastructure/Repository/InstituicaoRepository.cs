namespace Infrastructure.Repository
{
    using Domain;
    using Entities;
    using Infrastructure.Configuration;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class InstituicaoRepository : GenericRepository<Instituicao>, IInstituicao
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;

        public InstituicaoRepository()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }

        /// <summary>
        /// Retorna uma lista filtrada de instituições de acordo ao filtro.
        /// </summary>
        /// <param name="option">Campos Nome,Endereço, CPNJ, Telefone</param>
        /// <returns></returns>
        IList<Instituicao> IInstituicao.List(string filter)
        {
            using (var db = new ContextBase(_OptionsBuilder))
            {
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    return db.Instituicoes.Where(p => p.Nome.Contains(filter) || p.Endereco.Contains(filter)|| p.CPNJ.Contains(filter) || p.Telefone.Contains(filter)).ToList();
                }
                else
                {
                    return db.Instituicoes.ToList();
                }

            }
        }
    }
}
