using MvcCore.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Repositories
{
    public class RepositoryDepartamentosOracle : IRepositoryDepartamentos
    {
        OracleDataAdapter addept;
        DataTable tabladept;
        OracleCommandBuilder builder;

        public RepositoryDepartamentosOracle(String cadenaoracle)
        {
            this.addept =
                new OracleDataAdapter("select * from dept", cadenaoracle);
            this.builder = new OracleCommandBuilder(this.addept);
            this.tabladept = new DataTable();
            this.addept.Fill(this.tabladept);
        }

        public List<Departamento> GetDepartamentos()
        {
            var consulta = from datos in this.tabladept.AsEnumerable()
                           select new Departamento
                           {
                                Numero = datos.Field<int>("DEPT_NO"),
                                Nombre = datos.Field<String>("DNOMBRE"),
                                Localidad = datos.Field<String>("LOC")
                           };
            return consulta.ToList();
        }

        public Departamento BuscarDepartamento(int deptno)
        {
            var consulta = from datos in this.tabladept.AsEnumerable()
                           where datos.Field<int>("DEPT_NO") == deptno
                           select new Departamento
                           {
                               Numero = datos.Field<int>("DEPT_NO"),
                               Nombre = datos.Field<String>("DNOMBRE"),
                               Localidad = datos.Field<String>("LOC")
                           };
            return consulta.FirstOrDefault();
        }

        private DataRow GetDataRowId(int iddept)
        {
            DataRow fila =
                this.tabladept.AsEnumerable()
                .Where(z => z.Field<int>("DEPT_NO") == iddept).FirstOrDefault();
            return fila;
        }

        public void EliminarDepartamento(int deptno)
        {
            //PARA ELIMINAR, DEBEMOS HACERLO SOBRE EL OBJETO DATATABLE
            //DEBEMOS BUSCAR LA FILA (DataRow) QUE CORRESPONDA CON EL ID
            //LA FILA TIENE UN METODO DELETE QUE MARCARA EN LA TABLA EL VALOR
            //PARA ELIMINAR
            //POSTERIORMENTE, EL ADAPTADOR, AL IGUAL QUE TIENE UN METODO
            //PARA TRAER LOS DATOS (Fill), TENEMOS UN METODO PARA 
            //AUTOMATIZAR LOS CAMBIOS (Update)
            DataRow row = this.GetDataRowId(deptno);
            row.Delete();
            this.addept.Update(this.tabladept);
            this.tabladept.AcceptChanges();
        }

        public void InsertDepartamento(int deptno, string nombre, string localidad)
        {
            DataRow row = this.tabladept.NewRow();
            row["DEPT_NO"] = deptno;
            row["DNOMBRE"] = nombre;
            row["LOC"] = localidad;
            this.tabladept.Rows.Add(row);
            this.addept.Update(this.tabladept);
            this.tabladept.AcceptChanges();
        }

        public void UpdateDepartamento(int deptno, string nombre, string localidad)
        {
            DataRow row = this.GetDataRowId(deptno);
            row["DNOMBRE"] = nombre;
            row["LOC"] = localidad;
            this.addept.Update(this.tabladept);
            this.tabladept.AcceptChanges();
        }

        public void InsertDepartamento(int deptno, string nombre, string localidad, string imagen)
        {
            throw new NotImplementedException();
        }
    }
}
