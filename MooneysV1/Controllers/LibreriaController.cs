using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MooneysV1.Models;
using System.Data;

namespace MooneysV1.Controllers
{
    public class LibreriaController : Controller
    {
        private readonly IConfiguration iconfig;

        public LibreriaController(IConfiguration _iconfig)
        {
            iconfig = _iconfig;
        }


        IEnumerable<Libros> ListarLibros() 
        { 
            List<Libros> temporal = new List<Libros>();

            using (SqlConnection cn = new SqlConnection(iconfig["ConnectionStrings:cn"]))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarLibros", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Libros()
                    {
                        idLibro = dr.GetInt32(0),
                        titulo = dr.GetString(1),
                        autor = dr.GetString(2),
                        isbn = dr.IsDBNull(3) ? "" : dr.GetString(3),
                        stock = dr.GetInt32(4),
                        url = dr.IsDBNull(5) ? "" : dr.GetString(5),
                        sinopsis = dr.IsDBNull(6) ? "" : dr.GetString(6)
                    });
                }
                cn.Close();
            }
            return temporal;
        }


        public IActionResult Index()
        {
            return View(ListarLibros());
        }



        public IActionResult Create() 
        {
            return View(new Libros());
        }

        [HttpPost]
        public IActionResult Create(Libros libro)
        {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(iconfig["ConnectionStrings:cn"]))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_InsertarLibro", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Titulo", libro.titulo);
                cmd.Parameters.AddWithValue("@Autor", libro.autor);
                cmd.Parameters.AddWithValue("@ISBN", (object?)libro.isbn ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Stock", libro.stock);
                cmd.Parameters.AddWithValue("@URL", (object?)libro.url ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Sinopsis", (object?)libro.sinopsis ?? DBNull.Value);

                try
                {
                    cmd.ExecuteNonQuery();
                    mensaje = "Libro registrado correctamente.";
                }
                catch (Exception ex)
                {
                    mensaje = "Error al registrar libro: " + ex.Message;
                }
                cn.Close();
            }

            TempData["mensaje"] = mensaje;
            return RedirectToAction("Index");
        }


        Libros BuscarLibro(int id)
        {
            Libros libro = new Libros();

            using (SqlConnection cn = new SqlConnection(iconfig["ConnectionStrings:cn"]))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Libros WHERE ID_L = @id", cn);

                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    libro.idLibro = dr.GetInt32(0);
                    libro.titulo = dr.GetString(1);
                    libro.autor = dr.GetString(2);
                    libro.isbn = dr.IsDBNull(3) ? "" : dr.GetString(3);
                    libro.stock = dr.GetInt32(4);
                    libro.url = dr.IsDBNull(5) ? "" : dr.GetString(5);
                    libro.sinopsis = dr.IsDBNull(6) ? "" : dr.GetString(6);
                }
                cn.Close();
            }
            return libro;
        }

        public IActionResult Edit(int id)
        {
            return View(BuscarLibro(id));
        }

        [HttpPost]
        public IActionResult Edit(Libros libro)
        {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(iconfig["ConnectionStrings:cn"]))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_ActualizarLibro", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID_L", libro.idLibro);
                cmd.Parameters.AddWithValue("@Titulo", libro.titulo);
                cmd.Parameters.AddWithValue("@Autor", libro.autor);
                cmd.Parameters.AddWithValue("@ISBN", (object?)libro.isbn ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Stock", libro.stock);
                cmd.Parameters.AddWithValue("@URL", (object?)libro.url ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Sinopsis", (object?)libro.sinopsis ?? DBNull.Value);

                try
                {
                    int filas = cmd.ExecuteNonQuery();
                    mensaje = filas > 0
                        ? "Libro actualizado correctamente."
                        : "No se encontró el libro.";
                }
                catch (Exception ex)
                {
                    mensaje = "Error al actualizar libro: " + ex.Message;
                }
                cn.Close();
            }

            TempData["mensaje"] = mensaje;
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            return View(BuscarLibro(id));
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(iconfig["ConnectionStrings:cn"]))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("sp_EliminarLibro", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_L", id);

                try
                {
                    int filas = cmd.ExecuteNonQuery();
                    mensaje = filas > 0
                        ? "Libro eliminado correctamente."
                        : "No se encontró el libro.";
                }
                catch (Exception ex)
                {
                    mensaje = "Error al eliminar libro: " + ex.Message;
                }
                cn.Close();
            }

            TempData["mensaje"] = mensaje;
            return RedirectToAction("Index");
        }

    }
}
