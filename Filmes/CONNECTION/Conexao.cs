using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmes.CONNECTION
{
    internal class ConectionFactory
    {
        public MySqlConnection getConnection()
        {
            string conexao = ConfigurationManager.ConnectionStrings["dbfilmes"].ConnectionString;
            return new MySqlConnection(conexao);
        }
    }
}
