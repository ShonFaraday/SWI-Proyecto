using System.Data;
using System.Data.SqlClient;
using WebProyecto.Models;
using Microsoft.Extensions.Configuration;

namespace WebProyecto.Repositories
{
    public class MembresiaRepository
    {
        private readonly string _cadena;

        // cadena de conexión 
        public MembresiaRepository(IConfiguration configuration)
        {
            _cadena = configuration.GetConnectionString("MooneysConnection");
        }

        // LISTAR
        public List<Membresia> Listar()
        {
            List<Membresia> lista = new List<Membresia>();

            using (SqlConnection con = new SqlConnection(_cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_Membresias_Listar", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Membresia
                    {
                        ID_M = Convert.ToInt32(dr["ID_M"]),
                        Nombre = dr["Nombre"].ToString(),
                        Descripcion = dr["Descripcion"]?.ToString(),
                        Max_Libros_Prestamo = Convert.ToInt32(dr["Max_Libros_Prestamo"]),
                        Dias_Prestamo = Convert.ToInt32(dr["Dias_Prestamo"]),
                        Costo = Convert.ToDecimal(dr["Costo"])
                    });
                }
            }

            return lista;
        }

        //  CREAR
        public void Crear(Membresia m)
        {
            using (SqlConnection con = new SqlConnection(_cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_Membresias_Crear", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_M", m.ID_M);
                cmd.Parameters.AddWithValue("@Nombre", m.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", (object?)m.Descripcion ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Max_Libros_Prestamo", m.Max_Libros_Prestamo);
                cmd.Parameters.AddWithValue("@Dias_Prestamo", m.Dias_Prestamo);
                cmd.Parameters.AddWithValue("@Costo", m.Costo);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //  ACTUALIZAR
        public void Actualizar(Membresia m)
        {
            using (SqlConnection con = new SqlConnection(_cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_Membresias_Actualizar", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_M", m.ID_M);
                cmd.Parameters.AddWithValue("@Nombre", m.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", (object?)m.Descripcion ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Max_Libros_Prestamo", m.Max_Libros_Prestamo);
                cmd.Parameters.AddWithValue("@Dias_Prestamo", m.Dias_Prestamo);
                cmd.Parameters.AddWithValue("@Costo", m.Costo);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //  ELIMINAR
        public void Eliminar(int id)
        {
            using (SqlConnection con = new SqlConnection(_cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_Membresias_Eliminar", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_M", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}

