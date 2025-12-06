using Microsoft.AspNetCore.Mvc;
using WebProyecto.Models;
using WebProyecto.Repositories;

namespace WebProyecto.Controllers
{
    public class MembresiasController : Controller
    {
        private readonly MembresiaRepository _repo;

        // Inyección del repositorio
        public MembresiasController(MembresiaRepository repo)
        {
            _repo = repo;
        }

        //  LISTAR 
        public IActionResult Index()
        {
            var lista = _repo.Listar();
            return View(lista);
        }

        //  CREAR
        public IActionResult Create()
        {
            return View();
        }

        //  CREAR - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Membresia m)
        {
            if (!ModelState.IsValid)
            {
                return View(m);
            }

            _repo.Crear(m);
            return RedirectToAction(nameof(Index));
        }

        //  EDITAR 
        public IActionResult Edit(int id)
        {
            var lista = _repo.Listar();
            var m = lista.FirstOrDefault(x => x.ID_M == id);

            if (m == null)
                return NotFound();

            return View(m);
        }

        //  EDITAR - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Membresia m)
        {
            if (!ModelState.IsValid)
            {
                return View(m);
            }

            _repo.Actualizar(m);
            return RedirectToAction(nameof(Index));
        }

        //  ELIMINAR 
        public IActionResult Delete(int id)
        {
            var lista = _repo.Listar();
            var m = lista.FirstOrDefault(x => x.ID_M == id);

            if (m == null)
                return NotFound();

            return View(m);
        }

        //  ELIMINAR - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repo.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

