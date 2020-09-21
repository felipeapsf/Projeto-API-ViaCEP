using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Windows;
using ViaCEP.Data.Entities;
using ViaCEP.Data.Repositories;

namespace ViaCEP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void btn_consulta_Click(object sender, RoutedEventArgs e)
        {
            if (textbox_cep.Text.Length == 8)
            {
                try
                {
                    RestClient restClient = new RestClient(string.Format("https://viacep.com.br/ws/{0}/json/", textbox_cep.Text));
                    RestRequest restRequest = new RestRequest(Method.GET);

                    IRestResponse restResponse = restClient.Execute(restRequest);

                    if (restResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                        MessageBox.Show("Houve um problema com sua requisição: " + restResponse.Content, "Erro!", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                    else
                    {

                        CepEntity cepEntity = new JsonDeserializer().Deserialize<CepEntity>(restResponse);

                        if (cepEntity.Cep is null)
                        {
                            MessageBox.Show("CEP não encontrado!", "Atenção!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            return;
                        }

                        tb_cep.Text = cepEntity.Cep;
                        tb_logradouro.Text = cepEntity.Logradouro;
                        tb_complemento.Text = cepEntity.Complemento;
                        tb_bairro.Text = cepEntity.Bairro;
                        tb_localidade.Text = cepEntity.Localidade;
                        tb_uf.Text = cepEntity.Uf;
                        tb_ibge.Text = cepEntity.Ibge;
                        tb_gia.Text = cepEntity.Gia;
                        tb_ddd.Text = cepEntity.DDD;
                        tb_siafi.Text = cepEntity.Siafi;

                    }
                }
                catch (Exception erro)
                {
                    MessageBox.Show("Erro geral ao consultar a API: " + erro.Message, "Erro!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btn_sair_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Sair da aplicação?", "Saindo...", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                Close();
        }

        private void btn_gravar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var cepentity = new CepEntity();
                cepentity.Cep = textbox_cep.Text;

                var repository = new CepRepository();
                var carregarCep = repository.SelectCep(cepentity.Cep);

                if (carregarCep is null)
                {
                    cepentity.Cep = textbox_cep.Text;
                    cepentity.Logradouro = tb_logradouro.Text;
                    cepentity.Complemento = tb_complemento.Text;
                    cepentity.Bairro = tb_bairro.Text;
                    cepentity.Localidade = tb_localidade.Text;
                    cepentity.Uf = tb_uf.Text;
                    cepentity.Ibge = tb_ibge.Text;
                    cepentity.Gia = tb_gia.Text;
                    cepentity.DDD = tb_ddd.Text;
                    cepentity.Siafi = tb_siafi.Text;

                    repository.Inserir(cepentity);

                    MessageBox.Show("CEP cadastrado com sucesso.");

                    textbox_cep.Text = String.Empty;
                    tb_cep.Text = String.Empty;
                    tb_logradouro.Text = String.Empty;
                    tb_complemento.Text = String.Empty;
                    tb_bairro.Text = String.Empty;
                    tb_localidade.Text = String.Empty;
                    tb_uf.Text = String.Empty;
                    tb_ibge.Text = String.Empty;
                    tb_gia.Text = String.Empty;
                    tb_ddd.Text = String.Empty;
                    tb_siafi.Text = String.Empty;
                }
                else
                {
                    MessageBox.Show("CEP já existe no banco.", "Atenção!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro!");
            }
        }

        private void btn_consulta_db_Click(object sender, RoutedEventArgs e)
        {
            SelecionarCep();
        }

        private void SelecionarCep()
        {
            try
            {
                var cepentity = new CepEntity();
                cepentity.Cep = textbox_cep.Text;

                var repository = new CepRepository();
                var carregarCep = repository.SelectCep(cepentity.Cep);

                if (carregarCep != null)
                {
                    tb_cep.Text = repository.SelectCep(cepentity.Cep).Cep;
                    tb_logradouro.Text = repository.SelectCep(cepentity.Cep).Logradouro;
                    tb_complemento.Text = repository.SelectCep(cepentity.Cep).Complemento;
                    tb_bairro.Text = repository.SelectCep(cepentity.Cep).Bairro;
                    tb_localidade.Text = repository.SelectCep(cepentity.Cep).Localidade;
                    tb_uf.Text = repository.SelectCep(cepentity.Cep).Uf;
                    tb_ibge.Text = repository.SelectCep(cepentity.Cep).Ibge;
                    tb_gia.Text = repository.SelectCep(cepentity.Cep).Gia;
                    tb_ddd.Text = repository.SelectCep(cepentity.Cep).DDD;
                    tb_siafi.Text = repository.SelectCep(cepentity.Cep).Siafi;
                }
                else
                {
                    MessageBox.Show("CEP não encontrado no banco.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro!");
            }
        }
    }
}