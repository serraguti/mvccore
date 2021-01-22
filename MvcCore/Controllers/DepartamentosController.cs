using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcCore.Models;
using MvcCore.Repositories;

namespace MvcCore.Controllers
{
    public class DepartamentosController : Controller
    {
        IRepositoryDepartamentos repo;

        public DepartamentosController(IRepositoryDepartamentos repo)
        {
            this.repo = repo;
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
        public IActionResult Create(Departamento dept)
        {
            this.repo.InsertDepartamento(dept.Numero, dept.Nombre, dept.Localidad);
            return RedirectToAction("Index");
        }
    }
}
