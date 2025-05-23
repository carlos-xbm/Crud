﻿using Crud.Enums;
using Crud.Models;
using System.Data.SqlClient;


namespace Crud.Data
{
    public class UsuarioDataAccess
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

        public List<UsuarioModel> ListarUsuarios()
        {
            List<UsuarioModel> usuarios = new List<UsuarioModel>();
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[listar_usuarios]";

                _connection.Open();
                SqlDataReader reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    UsuarioModel usuario = new UsuarioModel();

                    usuario.Id = Convert.ToInt32(reader["Id"]);
                    usuario.Nome = reader["Nome"].ToString();
                    usuario.LoginUsuario = reader["LoginUsuario"].ToString();
                    usuario.Email = reader["Email"].ToString();
                    usuario.DataCadastro = Convert.ToDateTime(reader["DataCadastro"]).Date;
                    if (reader["DataAtualizacao"] != DBNull.Value)
                    {
                        usuario.DataAtualizacao = Convert.ToDateTime(reader["DataAtualizacao"]).Date;
                    }
                    else
                    {
                        usuario.DataAtualizacao = null; // Atribuindo null
                    }
                    usuario.Perfil = (PerfilEnum)Enum.Parse(typeof(PerfilEnum), reader["Perfil"].ToString());

                    usuarios.Add(usuario);
                }

                _connection.Close();
            }
            return usuarios;
        }

        public bool Cadastrar(UsuarioModel usuario)
        {
            int id = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[inserir_usuario]";

                _command.Parameters.AddWithValue("@Nome", usuario.Nome);
                _command.Parameters.AddWithValue("@LoginUsuario", usuario.LoginUsuario);
                _command.Parameters.AddWithValue("@Email", usuario.Email);
                _command.Parameters.AddWithValue("@Senha", usuario.Senha);
                _command.Parameters.AddWithValue("@Perfil", usuario.Perfil.ToString());


                _connection.Open();
                id = _command.ExecuteNonQuery();
                _connection.Close();

            }
            return id > 0 ? true : false;
        }

        public UsuarioModel BuscarUsuarioPorId(int id)
        {
            UsuarioModel usuario = new UsuarioModel();
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[listar_usuario_id]";

                _command.Parameters.AddWithValue("@Id", id);
                _connection.Open();

                SqlDataReader reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    usuario.Id = Convert.ToInt32(reader["Id"]);
                    usuario.Nome = reader["Nome"].ToString();
                    usuario.LoginUsuario = reader["LoginUsuario"].ToString();
                    usuario.Email = reader["Email"].ToString();
                    usuario.Perfil = (PerfilEnum)Enum.Parse(typeof(PerfilEnum), reader["Perfil"].ToString());
                }
                _connection.Close();
            }

            return usuario;
        }

        public bool Editar(UsuarioModel usuario)
        {
            var id = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[editar_usuario]";

                _command.Parameters.AddWithValue("id", usuario.Id);
                _command.Parameters.AddWithValue("@Nome", usuario.Nome);
                _command.Parameters.AddWithValue("@LoginUsuario", usuario.LoginUsuario);
                _command.Parameters.AddWithValue("@Email", usuario.Email);
                _command.Parameters.AddWithValue("@Senha",
                         string.IsNullOrWhiteSpace(usuario.Senha) ? DBNull.Value : usuario.Senha);
                _command.Parameters.AddWithValue("@Perfil", usuario.Perfil.ToString());

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
                _command.CommandText = "[DBO].[remover_usuario]";
                _command.Parameters.AddWithValue("@Id", id);
                _connection.Open();
                result = _command.ExecuteNonQuery();
                _connection.Close();


            }

            return result > 0 ? true : false;
        }

    }
}
