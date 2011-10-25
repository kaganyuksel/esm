using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESM.Objetos
{
    public static class CCitas
    {
        #region Propiedades Publicas y Privadas

        static Model.ESMBDDataContext dbAgenda = new Model.ESMBDDataContext();

        #endregion


        public static List<ESM.Model.Cita> ObtenerCitas(int idconsultor)
        {
            try
            {
                if (idconsultor != 0)
                {
                    var vcitas = from c in new ESM.Model.ESMBDDataContext().Citas
                                 where c.Secretaria_Educacion.IdConsultor == idconsultor || c.Establecimiento_Educativo.Secretaria_Educacion.IdConsultor == idconsultor
                                 select c;
                    return vcitas.ToList();
                }
                else
                {
                    var vcitas = from c in new ESM.Model.ESMBDDataContext().Citas
                                 select c;
                    return vcitas.ToList();
                }



            }
            catch (Exception) { return null; }
        }

        public static bool AsignarCita(int id, DateTime datestart, DateTime dateend, int tipo)
        {
            try
            {
                if (datestart.Hour == 0)
                {
                    datestart = datestart.AddHours(8);
                    dateend = dateend.AddHours(14);
                }

                ESM.Model.Cita objCita = null;

                if (tipo == 1)
                {
                    objCita = new Model.Cita
                    {
                        IdEE = id,
                        FechaInicio = datestart,
                        FechaFin = dateend,
                        IdEstadoCita = 1
                    };

                }
                else if (tipo == 0)
                {
                    objCita = new Model.Cita
                    {
                        IdSE = id,
                        FechaInicio = datestart,
                        FechaFin = dateend,
                        IdEstadoCita = 1
                    };

                }

                dbAgenda.Citas.InsertOnSubmit(objCita);
                dbAgenda.SubmitChanges();
                return true;
            }
            catch (Exception) { return false; }

        }

        public static bool ActualizarCita(int idcita, DateTime fechaincial, DateTime fechafinal)
        {
            try
            {
                var cita = (from c in dbAgenda.Citas
                            where c.IdCita == idcita
                            select c).Single();

                cita.FechaInicio = fechaincial;
                cita.FechaFin = fechafinal;

                dbAgenda.SubmitChanges();
                return true;
            }
            catch (Exception) { return false; }

        }

        public static bool ActualizarCita(int idcita, DateTime fechaincial, int dias, int minutos)
        {
            try
            {
                var cita = (from c in dbAgenda.Citas
                            where c.IdCita == idcita
                            select c).Single();

                cita.FechaInicio = cita.FechaInicio.AddDays(dias).AddMinutes(minutos);
                cita.FechaFin = cita.FechaFin.AddDays(dias).AddMinutes(minutos);

                dbAgenda.SubmitChanges();
                return true;
            }
            catch (Exception) { return false; }

        }
    }
}