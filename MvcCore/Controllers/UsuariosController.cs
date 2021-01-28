using Microsoft.AspNetCore.Mvc;
using MvcCore.Models;
using MvcCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Controllers
{
    public class UsuariosController : Controller
    {
        RepositoryUsuarios repo;
        
        public UsuariosController(RepositoryUsuarios repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(int idusuario
            , String nombre, String username, String password)
        {
            this.repo.InsertarUsuario(idusuario
                , nombre, username, password);
            ViewData["MENSAJE"] = "Datos almacenados";
            return View();
        }

        public IActionResult Credenciales()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Credenciales(String username, String password)
        {
            Usuario user = this.repo.UserLogIn(username, password);
            if (user == null)
            {
                ViewData["MENSAJE"] = "Usuario/Password no válidos";
            }
            else
            {
                ViewData["MENSAJE"] =
                    "Credenciales correctas, Sr/Sra " + user.Nombre;
            }
            return View();
        }
    }
}
