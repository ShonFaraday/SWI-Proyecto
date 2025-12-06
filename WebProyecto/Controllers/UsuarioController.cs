using Microsoft.AspNetCore.Mvc;
using WebProyecto.Models;
using WebProyecto.Repositories;

namespace WebProyecto.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepository _repo;

        // Inyección del repositorio
        public UsuarioController(UsuarioRepository repo)
        {
            _repo = repo;
        }

        // LISTAR 
        public IActionResult Index()
        {
            var lista = _repo.Listar();
            return View(lista);
        }

        // CREAR
        public IActionResult Create()
        {
            return View();
        }

        // CREAR - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Usuario u)
        {
            if (!ModelState.IsValid)
            {
                return View(u);
            }
            _repo.Crear(u);
            return RedirectToAction(nameof(Index));
        }

        // EDITAR 
        public IActionResult Edit(int id)
        {
            var lista = _repo.Listar();
            var u = lista.FirstOrDefault(x => x.ID_U == id);
            if (u == null)
                return NotFound();
            return View(u);
        }

        // EDITAR - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Usuario u)
        {
            if (!ModelState.IsValid)
            {
                return View(u);
            }
            _repo.Actualizar(u);
            return RedirectToAction(nameof(Index));
        }

        // ELIMINAR 
        public IActionResult Delete(int id)
        {
            var lista = _repo.Listar();
            var u = lista.FirstOrDefault(x => x.ID_U == id);
            if (u == null)
                return NotFound();
            return View(u);
        }

        // ELIMINAR - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repo.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}