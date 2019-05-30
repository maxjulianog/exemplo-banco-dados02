using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace Repositorio
{
    public class FilmeRepositorio
    {
        string CadeiaDeConexao = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\65978\Documents\ExemploBancoDados02.mdf;Integrated Security=True;Connect Timeout=30";
        /*
        Método que ira retornar os dados dos filmes(List<Filme>) da tabela de filmes.
        */
        public List<Filme> ObterTodos()
        {

             //* Cria conexao com banco de dados e abre.

            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = CadeiaDeConexao;
            conexao.Open();

            //Cria o comando para ser executado no bd e diz para este comando qual é a conexao que esta aberta.

            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            comando.CommandText = "SELECT * FROM filmes";

            //Cria uma tabela em memoria para poder obter os dados que são retornados do bd executando o comando do bd.

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());

            //Cria uma lista para adicionar os filmes do bd.

            List<Filme> filmes = new List<Filme>();

            //Percorre todos os registros lidos do bd.

            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                DataRow linha = tabela.Rows[i];
                //Cria um objeto com as informações obtidas do bd
                Filme filme = new Filme();
                filme.Id = Convert.ToInt32(linha["id"]);
                filme.Nome = linha["nome"].ToString();
                filme.avaliacao = Convert.ToDecimal(linha["avaliacao"]);
                filme.Duracao = Convert.ToDateTime(linha["duracao"]);
                filme.Curtiu = Convert.ToBoolean(linha["curtiu"]);
                filme.Categoria = linha["categoria"].ToString();
                filme.TemSequencia = Convert.ToBoolean(linha["tem_sequencia"]);
                //Adiciona o objeto que foi criado a lista de filme.
                filmes.Add(filme);
            }
            //Fecha a conexao do bd.
            conexao.Close();
            //retorna a lista de filmes
            return filmes;
        }
    }
}
