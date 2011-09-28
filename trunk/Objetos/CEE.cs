using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ESM.Model;

namespace ESM.Objetos
{
    public static class CEE
    {
        #region Propiedades Privadas y Publicas

        static ESM.Model.ESMBDDataContext _db = new Model.ESMBDDataContext();

        #endregion

        public static IQueryable<Establecimiento_Educativo> ObtenerEEs(int idconsultor)
        {
            try
            {
                var cee = from ee in _db.Establecimiento_Educativo
                          where ee.Secretaria_Educacion.IdConsultor == idconsultor
                          select ee;

                return cee;
            }
            catch (Exception) { return null; }
        }

        public static Establecimiento_Educativo ObtenerEE(int idie)
        {
            try
            {
                var ee = (from ie in _db.Establecimiento_Educativo
                          where ie.IdIE == idie
                          select ie).Single();

                return ee;
            }
            catch (Exception) { return null; }

        }

    }
}