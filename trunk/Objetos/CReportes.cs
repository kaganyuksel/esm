using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESM.Objetos
{
    public static class CReportes
    {

        public static IQueryable ReportAgendaSE()
        {
            var db = new ESM.Model.ESMBDDataContext();
            try
            {
                var cse = (from c in db.Citas
                           where c.IdSE != null
                           orderby c.Secretaria_Educacion.Nombre
                           orderby c.FechaInicio
                           select new { c.Secretaria_Educacion.Nombre, Consultor = c.Secretaria_Educacion.Consultore.Nombre, DepartamentoMunicipio = c.Secretaria_Educacion.DepMun, c.FechaInicio });

                return cse;
            }
            catch (Exception) { return null; }

        }

        public static List<object> ReportAgendaEE()
        {
            var db = new ESM.Model.ESMBDDataContext();
            try
            {
                List<object> objList = new List<object>();

                var cee = from c in db.Citas
                          where c.IdEE != null
                          orderby c.Secretaria_Educacion.Nombre
                          orderby c.FechaInicio
                          select new
                          {
                              c.Establecimiento_Educativo.Secretaria_Educacion.Nombre,
                              Consultor = c.Establecimiento_Educativo.Secretaria_Educacion.Consultore.Nombre,
                              c.Establecimiento_Educativo.CodigoDane,
                              Establecimiento = c.Establecimiento_Educativo.Nombre,
                              c.Establecimiento_Educativo.Municipio,
                              c.FechaInicio
                          };

                DateTime fecha_actual = DateTime.Now.AddHours(2);

                int visitados = (from v in cee
                                 where v.FechaInicio <= fecha_actual
                                 select v).Count();

                int porvisitar = (from v in cee
                                  where v.FechaInicio > fecha_actual
                                  select v).Count();

                objList.Add(cee);
                objList.Add(visitados);
                objList.Add(porvisitar);

                return objList;
            }
            catch (Exception) { return null; }

        }

    }
}