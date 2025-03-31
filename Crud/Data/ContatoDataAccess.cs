using Crud.Models;
using System.Data.SqlClient;


namespace Crud.Data
{
    public class ContatoDataAccess
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;

        public static IConfiguration Configuration { get; set; }

        private string GetConnectionString()
        {

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");


            Configuration = builder.Build();
            return Configuration.GetConnectionString("DefaultConnection");

        }

        public List<ContatoModel> ListarContatos()
        {
            List<ContatoModel> contatos = new List<ContatoModel>();
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[listar_contatos]";

                _connection.Open();
                SqlDataReader reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    ContatoModel contato = new ContatoModel();

                    contato.Id = Convert.ToInt32(reader["Id"]);
                    contato.Nome = reader["Nome"].ToString();
                    contato.Email = reader["Email"].ToString();
                    contato.Cargo = reader["Cargo"].ToString();
                    contato.Sobrenome = reader["Sobrenome"].ToString();

                    contatos.Add(contato);
                }

                _connection.Close();
            }
            return contatos;
        }

        public bool Cadastrar(ContatoModel contato)
        {
            int id = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[inserir_contato]";

                _command.Parameters.AddWithValue("@Nome", contato.Nome);
                _command.Parameters.AddWithValue("@Sobrenome", contato.Sobrenome);
                _command.Parameters.AddWithValue("@Email", contato.Email);
                _command.Parameters.AddWithValue("@Cargo", contato.Cargo);

                _connection.Open();
                id = _command.ExecuteNonQuery();
                _connection.Close();

            }
            return id > 0 ? true : false;
        }

        public ContatoModel BuscarContatoPorId(int id)
        {
            ContatoModel contato = new ContatoModel();
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[listar_contato_id]";

                _command.Parameters.AddWithValue("@Id", id);
                _connection.Open();

                SqlDataReader reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    contato.Id = Convert.ToInt32(reader["Id"]);
                    contato.Nome = reader["Nome"].ToString();
                    contato.Sobrenome = reader["Sobrenome"].ToString();
                    contato.Cargo = reader["Cargo"].ToString();
                    contato.Email = reader["Email"].ToString();
                }
                _connection.Close();
            }

            return contato;
        }

        public bool Editar(ContatoModel contato)
        {
            var id = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[editar_contato]";
                _command.Parameters.AddWithValue("@Id", contato.Id);
                _command.Parameters.AddWithValue("@Nome", contato.Nome);
                _command.Parameters.AddWithValue("@Sobrenome", contato.Sobrenome);
                _command.Parameters.AddWithValue("@Email", contato.Email);
                _command.Parameters.AddWithValue("@Cargo", contato.Cargo);

                _connection.Open();

                id = _command.ExecuteNonQuery();

                _connection.Close();
            }

            return id > 0 ? true : false;
        }

        public bool Remover(int id)
        {
            var result = 0;

            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[remover_contato]";
                _command.Parameters.AddWithValue("@Id", id);
                _connection.Open();
                result = _command.ExecuteNonQuery();
                _connection.Close();


            }

            return result > 0 ? true : false;
        }

    }
}
