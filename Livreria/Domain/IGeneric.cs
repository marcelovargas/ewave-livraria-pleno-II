﻿namespace Domain
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IGeneric<T> where T : class
    {
        Task Add(T objeto);
        Task Update(T objeto);
        Task Delete(T objeto);
        T GetEntityById(int Id);
        Task<List<T>> List();

    }
}
