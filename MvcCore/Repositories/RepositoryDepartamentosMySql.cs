using MvcCore.Data;
using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Repositories
{
    public class RepositoryDepartamentosMySql : IRepositoryDepartamentos
    {
        HospitalContext context;
        public RepositoryDepartamentosMySql(HospitalContext context)
        {
            this.context = context;
        }

        public Departamento BuscarDepartamento(int deptno)
        {
            return this.context.Departamentos
                .Where(x => x.Numero == deptno).FirstOrDefault();
        }

        public void EliminarDepartamento(int deptno)
        {
            Departamento departamento = this.BuscarDepartamento(deptno);
            this.context.Departamentos.Remove(departamento);
            this.context.SaveChanges();
        }

        public List<Departamento> GetDepartamentos()
        {
            return this.context.Departamentos.ToList();
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

        public void InsertDepartamento(int deptno, string nombre, string localidad, string imagen)
        {
            throw new NotImplementedException();
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
