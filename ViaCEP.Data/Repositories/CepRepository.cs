using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using ViaCEP.Data.Contracts;
using ViaCEP.Data.Entities;

namespace ViaCEP.Data.Repositories
{
    public class CepRepository : ICepRepository
    {
        private string connectionString;

        public CepRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["viacep"].ConnectionString;
        }

        public void Inserir(CepEntity obj)
        {
            var query = "insert into Cep(Cep, Logradouro, Complemento, Bairro, Localidade, Uf, Ibge, Gia, DDD, Siafi) " +
                "values(@Cep, @Logradouro, @Complemento, @Bairro, @Localidade, @Uf, @Ibge, @Gia, @DDD, @Siafi)";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, obj);
            }
        }

        public CepEntity SelectCep(string Cep)
        {
            var query = "select * from Cep where Cep = @Cep";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<CepEntity>(query, new { Cep = Cep }).SingleOrDefault();
            }
        }
    }
}