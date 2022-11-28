using Filmes.CONNECTION;
using Filmes.MODELS;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmes.DAO
{
    internal class FilmeDAO
    {
        #region Conexao
        private MySqlConnection conexao;

        public FilmeDAO()
        {
            this.conexao = new ConectionFactory().getConnection();
        }
        #endregion

        #region CadastrarFilme

        public void CadastrarFilme(Filme obj)
        {
            string comando = @"INSERT INTO tb_filmes (nome, ano, genero) VALUES (@nome, @ano, @genero);";

            try
            {
                MySqlCommand executeCommand = new MySqlCommand(comando, conexao);

                executeCommand.Parameters.AddWithValue("@nome", obj.nome);
                executeCommand.Parameters.AddWithValue("@ano", obj.ano);
                executeCommand.Parameters.AddWithValue("@genero", obj.genero);

                conexao.Open();
                executeCommand.ExecuteNonQuery();

                MessageBox.Show("Filme cadastrado com sucesso...");
                conexao.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Um erro ocorreu: " + erro);
                throw;
            }
        }

        #endregion

        #region ListarFilmes

        public DataTable? listarFilmes()
        {
            try
            {
                DataTable tabelaFilmes = new DataTable();
                string comando = "SELECT * FROM tb_filmes";

                MySqlCommand executeComando = new MySqlCommand(comando, conexao);
                conexao.Open();
                executeComando.ExecuteNonQuery();

                MySqlDataAdapter listaAdaptada = new MySqlDataAdapter(executeComando);
                listaAdaptada.Fill(tabelaFilmes);
                conexao.Close();

                return tabelaFilmes;
            }
            catch (Exception erro)
            {
                MessageBox.Show("Ocorreu um erro: " + erro);
                return null;
            }
        }

        #endregion

        #region AlterarFilme

        public void alterarFilme(Filme obj)
        {
            string comando = @"UPDATE tb_filmes SET nome=@nome, ano=@ano, genero=@genero WHERE id = @id;";

            try
            {
                MySqlCommand execute = new MySqlCommand(comando, conexao);

                execute.Parameters.AddWithValue("@nome", obj.nome);
                execute.Parameters.AddWithValue("@ano", obj.ano);
                execute.Parameters.AddWithValue("@genero", obj.genero);
                execute.Parameters.AddWithValue("@id", obj.id);

                conexao.Open();
                execute.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Ocorreu um erro ao buscar o filme: " + erro);
                throw;
            }
        }

        #endregion

        #region DeletarFilme

        public void deletarFilme(Filme obj)
        {
            string comando = @"DELETE FROM tb_filmes WHERE id = @id;";

            try
            {
                MySqlCommand execute = new MySqlCommand(comando, conexao);

                execute.Parameters.AddWithValue("@id", obj.id);

                conexao.Open();
                execute.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Um error ocorreu ao tentar deletar um filme: " + erro);
                throw;
            }
        }

        #endregion

        #region SelecionarFilme

        public DataTable selecionarFilme(string nome)
        {
            try
            {
                DataTable filme = new DataTable();

                string comando = @"SELECT * FROM tb_filmes WHERE nome = @nome;";
                MySqlCommand executar = new MySqlCommand(comando, conexao);
                executar.Parameters.AddWithValue("@nome", nome);

                conexao.Open();
                executar.ExecuteNonQuery();

                MySqlDataAdapter adapter = new MySqlDataAdapter(executar);
                adapter.Fill(filme);

                conexao.Close();
                return filme;  
            }
            catch (Exception)
            {
                MessageBox.Show(nome + " não encontrado...");
                throw;
            }
        }

        #endregion

        #region SelecionaFilmeAproximado

        public DataTable selecionarFilmeAproximado(string filmeNome)
        {
            try
            {
                DataTable filmes = new DataTable();

                string command = @"SELECT * FROM tb_filmes WHERE nome LIKE @nome;";
                MySqlCommand execute = new MySqlCommand(command, conexao);

                execute.Parameters.AddWithValue("@nome", filmeNome);
                conexao.Open();
                execute.ExecuteNonQuery();

                MySqlDataAdapter adapter = new MySqlDataAdapter(execute);
                adapter.Fill(filmes);

                conexao.Close();
                return filmes;
            }
            catch (Exception erro)
            {
                MessageBox.Show("Um erro ocorreu: " + erro);
                throw;
            }
            
        }

        #endregion
    }

}
