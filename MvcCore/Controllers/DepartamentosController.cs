using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcCore.Helpers;
using MvcCore.Models;
using MvcCore.Repositories;

namespace MvcCore.Controllers
{
    public class DepartamentosController : Controller
    {
        IRepositoryHospital repo;
        PathProvider pathprovider;

        public DepartamentosController(IRepositoryHospital repo
            , PathProvider pathprovider)
        {
            this.repo = repo;
            this.pathprovider = pathprovider;
        }

        public IActionResult Index()
        {
            List<Departamento> departamentos = this.repo.GetDepartamentos();
            return View(departamentos);
        }

        public IActionResult Details(int deptno)
        {
            Departamento dept = this.repo.BuscarDepartamento(deptno);
            return View(dept);
        }

        public IActionResult Delete(int deptno)
        {
            this.repo.EliminarDepartamento(deptno);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int deptno)
        {
            Departamento dept = this.repo.BuscarDepartamento(deptno);
            return View(dept);
        }

        [HttpPost]
        public IActionResult Edit(Departamento dept)
        {
            this.repo.UpdateDepartamento(dept.Numero, dept.Nombre, dept.Localidad);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Departamento dept, 
            IFormFile ficheroimagen)
        {
            String filename = ficheroimagen.FileName;
            String path =
                this.pathprovider.MapPath(filename, Folders.Images);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await ficheroimagen.CopyToAsync(stream);
            }
            this.repo.InsertDepartamento(dept.Numero, dept.Nombre
                , dept.Localidad, filename);
            return RedirectToAction("Index");
        }

        public IActionResult SeleccionMultiple()
        {
            List<Departamento> departamentos = this.repo.GetDepartamentos();
            List<Empleado> empleados = this.repo.GetEmpleados();
            ViewData["DEPARTAMENTOS"] = departamentos;
            return View(empleados);
        }

        [HttpPost]
        public IActionResult SeleccionMultiple(List<int> iddepartamentos)
        {
            List<Departamento> departamentos = this.repo.GetDepartamentos();
            List<Empleado> empleados =
                this.repo.BuscarEmpleadosDepartamentos(iddepartamentos);
            ViewData["DEPARTAMENTOS"] = departamentos;
            return View(empleados);
        }
    }
}
