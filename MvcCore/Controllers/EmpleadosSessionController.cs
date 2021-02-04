using Microsoft.AspNetCore.Mvc;
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

        public IActionResult AlmacenarEmpleados()
        {
            List<Empleado> empleados = this.repo.GetEmpleados();
            return View(empleados);
        }
    }
}
