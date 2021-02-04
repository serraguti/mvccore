using Microsoft.AspNetCore.Mvc;
using MvcCore.Extensions;
using MvcCore.Models;
using MvcCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Controllers
{
    public class EmpleadosSessionController : Controller
    {
        IRepositoryHospital repo;

        public EmpleadosSessionController(IRepositoryHospital repo) {
            this.repo = repo;
        }

        public IActionResult AlmacenarEmpleados(int? idempleado)
        {
            //NECESITAMOS PREGUNTAR SI HEMOS RECIBIDO DATOS
            if (idempleado != null)
            {
                List<int> sessionemp;
                //SI EXISTE LA SESION, RECUPERAMOS LA LISTA
                //SI NO EXISTE, CREAMOS LA LISTA
                if (HttpContext.Session
                    .GetObject<List<int>>("EMPLEADOS") == null)
                {
                    sessionemp = new List<int>();
                }
                else
                {
                    sessionemp = HttpContext.Session
                         .GetObject<List<int>>("EMPLEADOS");
                }
                if (sessionemp.Contains(idempleado.Value) == false)
                {
                    sessionemp.Add(idempleado.GetValueOrDefault());
                    //ALMACENAMOS LOS NUEVOS DATOS EN SESSION
                    HttpContext.Session.SetObject("EMPLEADOS", sessionemp);
                }

                ViewData["MENSAJE"] =
                    "Datos almacenados: "
                    + sessionemp.Count;
            }
            List<Empleado> empleados = this.repo.GetEmpleados();
            return View(empleados);
        }

        public IActionResult MostrarEmpleados(int? eliminar)
        {
            List<int> sessionemp =
                HttpContext.Session.GetObject<List<int>>("EMPLEADOS");
            if (sessionemp == null)
            {
                return View();
            }
            else
            {
                if (eliminar != null)
                {
                    sessionemp.Remove(eliminar.Value);
                    HttpContext.Session.SetObject("EMPLEADOS"
                        , sessionemp);
                }
                List<Empleado> empleados =
                    this.repo.GetEmpleadosSession(sessionemp);
                return View(empleados);
            }
        }
    }
}
