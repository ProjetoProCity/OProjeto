using MySql.Data.MySqlClient;
using ProjetoEcommerce.Models;
using System.Data;

namespace ProjetoEcommerce.Repositorio
{
    public class ProdutoRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySql = configuration.GetConnectionString("ConexaoMySql");

        public void Cadastrar(Produto produto)
        {
            using (var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into produto(Prod, Descr, Qtd, Preco) values(@prod, @descr, @qtd, @preco)", conexao);
                cmd.Parameters.Add("@prod", MySqlDbType.VarChar).Value = produto.Prod;
                cmd.Parameters.Add("@descr", MySqlDbType.VarChar).Value = produto.Descr;
                cmd.Parameters.Add("@qtd", MySqlDbType.Int64).Value = produto.Qtd;
                cmd.Parameters.Add("@preco", MySqlDbType.Double).Value = produto.Preco;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }





        public bool Atualizar(Produto produto)
        {
            try
            {
                using (var conexao = new MySqlConnection(_conexaoMySql))
                {
                    conexao.Open();
                    MySqlCommand cmd = new MySqlCommand("update Produto set Prod=@prod, Descr=@descr, Qtd=@qtd, Preco=@preco" + " WHERE Id = @id", conexao);
                    cmd.Parameters.Add("Id", MySqlDbType.Int64).Value = produto.Id;
                    cmd.Parameters.Add("Prod", MySqlDbType.VarChar).Value = produto.Prod;
                    cmd.Parameters.Add("Descr", MySqlDbType.VarChar).Value = produto.Descr;
                    cmd.Parameters.Add("Qtd", MySqlDbType.Int64).Value = produto.Qtd;
                    cmd.Parameters.Add("Preco", MySqlDbType.Double).Value = produto.Preco;
                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar o cliente: {ex.Message}");
                return false;
            }
        }

        public IEnumerable<Produto> TodosProdutos()
        {
            List<Produto> produtos = new List<Produto>();

            using (var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from Produto", conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conexao.Close();

                foreach(DataRow dr in dt.Rows)
                {
                    produtos.Add(
                            new Produto
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Prod = ((string)dr["Prod"]),
                                Descr = ((string)dr["Descr"]),
                                Qtd = Convert.ToInt32(dr["Qtd"]),
                                Preco = Convert.ToDouble(dr["Preco"])
                            });
                }
                return produtos;
            }
        }

        public Produto ObterProduto(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySql)) 
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("Select * from Produto where Id = @id", conexao);
                cmd.Parameters.AddWithValue("id", id);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr = cmd.ExecuteReader();
                Produto produto = new Produto();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    produto.Id = Convert.ToInt32(dr["Id"]);
                    produto.Prod = (string)(dr["Prod"]);
                    produto.Descr = (string)(dr["Descr"]);
                    produto.Qtd = Convert.ToInt32(dr["Qtd"]);
                    produto.Preco = Convert.ToDouble(dr["Preco"]);
                }
                return produto;
            }
        }
        public void Excluir(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySql))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("delete from Produto where Id = @id", conexao);
                cmd.Parameters.AddWithValue("id", Id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
    }
}

