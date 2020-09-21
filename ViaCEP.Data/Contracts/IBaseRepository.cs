using System;
using System.Collections.Generic;
using System.Text;

namespace ViaCEP.Data.Contracts
{
    public interface IBaseRepository<T> where T : class
    {
        void Inserir(T obj);
    }
}
