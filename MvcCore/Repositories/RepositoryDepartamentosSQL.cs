using MvcCore.Data;
using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Repositories
{
    public class RepositoryDepartamentosSQL: IRepositoryDepartamentos
    {
        DepartamentosContext context;

        public RepositoryDepartamentosSQL(DepartamentosContext context)
        {
            this.context = context;
        }

        public List<Departamento> GetDepartamentos()
        {
            var consulta = from datos in context.Departamentos
                           select datos;
            return consulta.ToList();
        }

        public Departamento BuscarDepartamento(int deptno)
        {
            return this.context.Departamentos.Where(z => z.Numero == deptno).FirstOrDefault();
        }

        public void EliminarDepartamento(int deptno)
        {
            Departamento departamento = this.BuscarDepartamento(deptno);
            this.context.Departamentos.Remove(departamento);
            this.context.SaveChanges();
        }

        public void InsertDepartamento(int deptno, string nombre, string localidad)
        {
            Departamento departamento = new Departamento();
            departamento.Numero = deptno;
            departamento.Nombre = nombre;
            departamento.Localidad = localidad;
            this.context.Departamentos.Add(departamento);
            this.context.SaveChanges();
        }

        public void UpdateDepartamento(int deptno, string nombre, string localidad)
        {
            Departamento departamento = this.BuscarDepartamento(deptno);
            departamento.Nombre = nombre;
            departamento.Localidad = localidad;
            this.context.SaveChanges();
        }
    }
}
