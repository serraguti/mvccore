using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Repositories
{
    public interface IRepositoryDepartamentos
    {
        List<Departamento> GetDepartamentos();

        Departamento BuscarDepartamento(int deptno);

        void EliminarDepartamento(int deptno);

        void UpdateDepartamento(int deptno, String nombre, String localidad);

        void InsertDepartamento(int deptno, String nombre, String localidad);

        void InsertDepartamento(int deptno, String nombre, String localidad
            , String imagen);
    }
}
