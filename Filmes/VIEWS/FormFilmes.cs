using Filmes.DAO;
using Filmes.MODELS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Filmes.VIEWS
{
    public partial class FormFilmes : Form
    {
        public FormFilmes()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Filme obj = new Filme
            {
                nome = txtNome.Text,
                ano = int.Parse(txtAno.Text),
                genero = cbGenero.Text
            };

            FilmeDAO filme = new FilmeDAO();

            try
            {
                filme.CadastrarFilme(obj);
                tbFilmes.DataSource = filme.listarFilmes();
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao cadastrar o filme...");
                throw;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormFilmes_Load(object sender, EventArgs e)
        {
            FilmeDAO obj = new FilmeDAO();
            tbFilmes.DataSource = obj.listarFilmes();
        }

        private void tbFilmes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = tbFilmes.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = tbFilmes.CurrentRow.Cells[1].Value.ToString();
            txtAno.Text = tbFilmes.CurrentRow.Cells[2].Value.ToString();
            cbGenero.Text = tbFilmes.CurrentRow.Cells[3].Value.ToString();

            tabFilmes.SelectedTab = tabPage1;
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            Filme obj = new Filme();
            obj.id = int.Parse(txtId.Text);

            FilmeDAO filme = new FilmeDAO();
            filme.deletarFilme(obj);

            tbFilmes.DataSource = filme.listarFilmes();
            txtNome.Clear();
            txtAno.Clear();
            txtId.Clear();
            cbGenero.SelectedIndex = -1;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Filme obj = new Filme
            {
                nome = txtNome.Text,
                ano = int.Parse(txtAno.Text),
                genero = cbGenero.Text,
                id = int.Parse(txtId.Text),
            };

            FilmeDAO filme = new FilmeDAO();
            filme.alterarFilme(obj);

            tbFilmes.DataSource = filme.listarFilmes();
            txtNome.Clear();
            txtAno.Clear();
            txtId.Clear();
            cbGenero.SelectedIndex = -1;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            FilmeDAO filme = new FilmeDAO();

            tbFilmes.DataSource = filme.selecionarFilme(txtPesquisa.Text);

            if(tbFilmes.Rows.Count == 1)
            {
                tbFilmes.DataSource = filme.listarFilmes();
                MessageBox.Show(txtPesquisa.Text + " não encontrado...");
            }
        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPesquisa_KeyPress(object sender, KeyPressEventArgs e)
        {
            FilmeDAO filme = new FilmeDAO();
            string nome = "%" + txtPesquisa.Text + "%";

            tbFilmes.DataSource = filme.selecionarFilmeAproximado(nome);

            if(txtPesquisa.Text == "")
            {
                tbFilmes.DataSource = filme.listarFilmes();
            }
        }
    }
}
