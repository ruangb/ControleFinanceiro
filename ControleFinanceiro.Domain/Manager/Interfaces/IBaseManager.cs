﻿namespace ControleFinanceiro.Domain.Manager.Interfaces
{
    public interface IBaseManager<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(int id);
    }
}
