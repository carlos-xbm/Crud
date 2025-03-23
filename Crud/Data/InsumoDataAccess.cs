using Crud.Models;
using System.Data.SqlClient;

namespace Crud.Data
{
    public class InsumoDataAccess
    {
        SqlConnection _connetion = null;
        SqlCommand _command = null;

        public static IConfiguration Configuration { get; set; }

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            return Configuration.GetConnectionString("DefaultConnection");
        }

        public List<Insumo> ListarInsumos()
        {
            List<Insumo> insumos = new List<Insumo>();
            using (_connetion = new SqlConnection(GetConnectionString()))
            {
                _command = _connetion.CreateCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[listar_insumos]";
                _connetion.Open();
                SqlDataReader reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    Insumo insumo = new Insumo();

                    insumo.Id = Convert.ToInt32(reader["Id"]);
                    insumo.Nome = reader["Nome"].ToString();
                    insumo.Fornecedor = reader["Fornecedor"].ToString();
                    insumo.NF = reader["NF"].ToString();
                    insumo.Lote = reader["Lote"].ToString();
                    insumo.Quantidade = Convert.ToInt32(reader["Quantidade"]);
                    insumo.Fabricacao = Convert.ToDateTime(reader["Fabricacao"]).Date;
                    insumo.Vencimento = Convert.ToDateTime(reader["Vencimento"]).Date;
                    insumo.Entrada = Convert.ToDateTime(reader["Entrada"]).Date;

                    insumos.Add(insumo);
                }
                _connetion.Close();
            }
            return insumos;
        }

        public bool Cadastrar(Insumo insumo)
        {
            int id = 0;
            using (_connetion = new SqlConnection(GetConnectionString()))
            {
                _command = _connetion.CreateCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[inserir_insumo]";

                _command.Parameters.AddWithValue("@Nome", insumo.Nome);
                _command.Parameters.AddWithValue("@Fornecedor", insumo.Fornecedor);
                _command.Parameters.AddWithValue("@NF", insumo.NF);
                _command.Parameters.AddWithValue("@Lote", insumo.Lote);
                _command.Parameters.AddWithValue("@Quantidade", insumo.Quantidade);
                _command.Parameters.AddWithValue("@Fabricacao", insumo.Fabricacao);
                _command.Parameters.AddWithValue("@Vencimento", insumo.Vencimento);
                _command.Parameters.AddWithValue("@Entrada", insumo.Entrada);

                _connetion.Open();
                id = _command.ExecuteNonQuery();
                _connetion.Close();
            }
            return id > 0 ? true : false;
        }

        public Insumo BuscarInsumoPorId(int id)
        {
            Insumo insumo = new Insumo();
            using (_connetion = new SqlConnection(GetConnectionString()))
            {
                _command = _connetion.CreateCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[listar_insumo_id]";

                _command.Parameters.AddWithValue("@Id", id);
                _connetion.Open();

                SqlDataReader reader = _command.ExecuteReader();

                while (reader.Read())
                {

                    insumo.Id = Convert.ToInt32(reader["Id"]);
                    insumo.Nome = reader["Nome"].ToString();
                    insumo.Fornecedor = reader["Fornecedor"].ToString();
                    insumo.NF = reader["NF"].ToString();
                    insumo.Lote = reader["Lote"].ToString();
                    insumo.Quantidade = Convert.ToInt32(reader["Quantidade"]);
                    insumo.Fabricacao = Convert.ToDateTime(reader["Fabricacao"]).Date;
                    insumo.Vencimento = Convert.ToDateTime(reader["Vencimento"]).Date;
                    insumo.Entrada = Convert.ToDateTime(reader["Entrada"]).Date;

                }
                _connetion.Close();
            }
            return insumo;
        }

        public bool Editar(Insumo insumo)
        {
            var id = 0;
            using (_connetion = new SqlConnection(GetConnectionString()))
            {
                _command = _connetion.CreateCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[editar_insumo]";

                _command.Parameters.AddWithValue("@Id",insumo.Id);
                _command.Parameters.AddWithValue("@Nome",insumo.Nome);
                _command.Parameters.AddWithValue("@Fornecedor",insumo.Fornecedor);
                _command.Parameters.AddWithValue("@NF",insumo.NF);
                _command.Parameters.AddWithValue("@Lote",insumo.Lote);
                _command.Parameters.AddWithValue("@Quantidade",insumo.Quantidade);
                _command.Parameters.AddWithValue("@Fabricacao",insumo.Fabricacao);
                _command.Parameters.AddWithValue("@Vencimento",insumo.Vencimento);
                _command.Parameters.AddWithValue("@Entrada", insumo.Entrada);

                _connetion.Open();

                id = _command.ExecuteNonQuery();

                _connetion.Close();
            }
            return id > 0 ? true : false;
        }


        public bool Remover(int id)
        {
            var result = 0;
            using (_connetion = new SqlConnection(GetConnectionString()))
            {
                _command = _connetion.CreateCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[remover_insumo]";
                _command.Parameters.AddWithValue("@Id", id);
                _connetion.Open();
                result = _command.ExecuteNonQuery();
                _connetion.Close();
            }
            return result > 0 ? true : false;
        }

    }
}
