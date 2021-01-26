using MvcCore.Helpers;
using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvcCore.Repositories
{
    public class RepositoryDepartamentosXML: IRepositoryDepartamentos
    {
        private PathProvider pathprovider;
        private XDocument docxml;
        private String path;

        public RepositoryDepartamentosXML(PathProvider pathprovider)
        {
            this.pathprovider = pathprovider;
            this.path = this.pathprovider.MapPath("departamentos.xml", Folders.Documents);
            this.docxml = XDocument.Load(this.path);
        }

        public List<Departamento> GetDepartamentos()
        {
            var consulta = from datos in this.docxml.Descendants("DEPARTAMENTO")
                           select new Departamento { 
                            Numero = int.Parse(datos.Attribute("NUMERO").Value)
                            , Nombre = datos.Element("NOMBRE").Value
                            , Localidad = datos.Element("LOCALIDAD").Value
                           };
            return consulta.ToList();
        }

        public Departamento BuscarDepartamento(int deptno)
        {
            var consulta = from datos in this.docxml.Descendants("DEPARTAMENTO")
                           where datos.Attribute("NUMERO").Value == deptno.ToString()
                           select new Departamento
                           {
                             Numero = int.Parse(datos.Attribute("NUMERO").Value)
                             , Nombre = datos.Element("NOMBRE").Value
                             , Localidad = datos.Element("LOCALIDAD").Value
                           };
            return consulta.FirstOrDefault();
        }

        private XElement GetXElementDepartamento(int deptno)
        {
            var consulta = from datos in this.docxml.Descendants("DEPARTAMENTO")
                           where datos.Attribute("NUMERO").Value == deptno.ToString()
                           select datos;
            return consulta.FirstOrDefault();
        }

        public void EliminarDepartamento(int deptno)
        {
            XElement xelem = this.GetXElementDepartamento(deptno);
            xelem.Remove();
            this.docxml.Save(this.path);
        }

        public void UpdateDepartamento(int deptno, String nombre, String localidad)
        {
            XElement xelem = this.GetXElementDepartamento(deptno);
            xelem.Element("NOMBRE").Value = nombre;
            xelem.Element("LOCALIDAD").Value = localidad;
            this.docxml.Save(this.path);
        }

        public void InsertDepartamento(int deptno, String nombre, String localidad)
        {
            XElement xelem = new XElement("DEPARTAMENTO");
            //xelem.Add(new XAttribute("NUMERO", deptno));
            xelem.SetAttributeValue("NUMERO", deptno);
            xelem.Add(new XElement("NOMBRE", nombre));
            xelem.Add(new XElement("LOCALIDAD", localidad));
            this.docxml.Element("DEPARTAMENTOS").Add(xelem);
            this.docxml.Save(this.path);
        }

        public void InsertDepartamento(int deptno, string nombre, string localidad, string imagen)
        {
            throw new NotImplementedException();
        }
    }
}
