using Microsoft.Extensions.Caching.Memory;
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
        HospitalContext context;
        private IMemoryCache MemoryCache; 

        public RepositoryDepartamentosSQL
            (HospitalContext context
            , IMemoryCache memorycache)
        {
            this.context = context;
            this.MemoryCache = memorycache;
        }

        public List<Departamento> GetDepartamentos()
        {
            //DEVOLVEMOS DEPARTAMENTOS DE LA MEMORIA CACHE
            //O RECUPERAMOS DEPARTAMENTOS DE SQL SERVER
            List<Departamento> lista;
            if (this.MemoryCache.Get("DEPARTAMENTOS") == null)
            {
                var consulta = from datos in context.Departamentos
                               select datos;
                lista = consulta.ToList();
                //ALMACENAMOS LA LISTA EN CACHE
                this.MemoryCache.Set("DEPARTAMENTOS", lista);
            }
            else
            {
                lista = this.MemoryCache.Get("DEPARTAMENTOS")
                    as List<Departamento>;
            }
            return lista;
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

        public void InsertDepartamento(int deptno, string nombre, string localidad, string imagen)
        {
            throw new NotImplementedException();
        }
    }
}
