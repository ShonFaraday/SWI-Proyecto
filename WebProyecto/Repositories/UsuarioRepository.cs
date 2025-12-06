using System.Data;
using System.Data.SqlClient;
using WebProyecto.Models;
using Microsoft.Extensions.Configuration;

namespace WebProyecto.Repositories
{
    public class UsuarioRepository
    {
        private readonly string _cadena;

        // Cadena de conexión 
        public UsuarioRepository(IConfiguration configuration)
        {
            _cadena = configuration.GetConnectionString("MooneysConnection");
        }

        // LISTAR
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            using (SqlConnection con = new SqlConnection(_cadena))
            {
                SqlCommand cmd = new SqlCommand("listUsuario", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Usuario
                    {
                        ID_U = Convert.ToInt32(dr["ID_U"]),
                        Nombre = dr["Nombre"].ToString(),
                        Email = dr["Email"].ToString(),
                        PIN = dr["PIN"].ToString()
                    });
                }
            }
            return lista;
        }

        // CREAR
        public void Crear(Usuario u)
        {
            using (SqlConnection con = new SqlConnection(_cadena))
            {
                SqlCommand cmd = new SqlCommand("insertUsuario", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_U", u.ID_U);
                cmd.Parameters.AddWithValue("@Nombre", u.Nombre);
                cmd.Parameters.AddWithValue("@Email", u.Email);
                cmd.Parameters.AddWithValue("@PIN", u.PIN);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // ACTUALIZAR
        public void Actualizar(Usuario u)
        {
            using (SqlConnection con = new SqlConnection(_cadena))
            {
                SqlCommand cmd = new SqlCommand("updtUsuario", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_U", u.ID_U);
                cmd.Parameters.AddWithValue("@Nombre", u.Nombre);
                cmd.Parameters.AddWithValue("@Email", u.Email);
                cmd.Parameters.AddWithValue("@PIN", u.PIN);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // ELIMINAR
        public void Eliminar(int id)
        {
            using (SqlConnection con = new SqlConnection(_cadena))
            {
                SqlCommand cmd = new SqlCommand("deltUsuario", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_U", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}