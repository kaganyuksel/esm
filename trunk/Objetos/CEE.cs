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

        public static IQueryable<Establecimiento_Educativo> ObtenerEEs(int idconsultor, bool sistematizacion = false)
        {
            try
            {
                IQueryable<ESM.Model.Establecimiento_Educativo> cee = null;

                cee = from ee in _db.Establecimiento_Educativos
                      where ee.Secretaria_Educacion.IdConsultor == idconsultor && ee.Estado == true
                      select ee;

                if (sistematizacion)
                {
                    var csistematizacion = from s in cee
                                           where s.Sistematizacion == true
                                           select s;

                    return csistematizacion;
                }
                else
                    return cee;
            }
            catch (Exception) { return null; }
        }

        public static Establecimiento_Educativo ObtenerEE(int idie)
        {
            try
            {
                var ee = (from ie in _db.Establecimiento_Educativos
                          where ie.IdIE == idie
                          select ie).Single();

                return ee;
            }
            catch (Exception) { return null; }

        }

        public static IQueryable<ESM.Model.LecturaContextoEE> ObtenerMedicionesEE(int idee)
        {
            try
            {
                var med = (from m in _db.LecturaContextoEEs
                           where m.IdIE == idee
                           select m).Take(1);

                return med;
            }
            catch (Exception) { return null; }
        }

        public static IQueryable<ESM.Model.Sistematizacion> ObtenerMedicionesEESis(int idee)
        {
            try
            {
                var csistematizacion = (from m in _db.Sistematizacions
                                        where m.IdEE == idee
                                        select m).Take(1);

                return csistematizacion;

            }
            catch (Exception) { return null; }

        }

        public static int CrearMedicionLC(DateTime fecha)
        {
            try
            {
                Medicione objMediciones = new Medicione
                {
                    FechaMedicion = fecha.AddHours(2)
                };

                _db.Mediciones.InsertOnSubmit(objMediciones);
                _db.SubmitChanges();

                return objMediciones.IdMedicion;
            }
            catch (Exception) { return 0; }
        }

    }
}