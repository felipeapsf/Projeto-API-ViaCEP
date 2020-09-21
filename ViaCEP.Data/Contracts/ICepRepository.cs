using System;
using System.Collections.Generic;
using System.Text;
using ViaCEP.Data.Entities;

namespace ViaCEP.Data.Contracts
{
    public interface ICepRepository : IBaseRepository<CepEntity>
    {
        CepEntity SelectCep(string Cep);
    }
}
