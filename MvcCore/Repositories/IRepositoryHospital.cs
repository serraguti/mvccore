using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Repositories
{
    public interface IRepositoryHospital: IRepositoryDepartamentos
    {
        List<Empleado> GetEmpleados();

        List<Empleado> BuscarEmpleadosDepartamentos(List<int> iddepartamentos);
    }
}
