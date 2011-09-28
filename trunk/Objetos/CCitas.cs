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


        public static List<ESM.Model.Citas> ObtenerCitas(int idconsultor)
        {
            try
            {
                var vcitas = from c in dbAgenda.Citas
                             join con in dbAgenda.Consultores on c.LLamadas.Secretaria_Educacion.IdConsultor equals con.IdConsultor
                             where con.IdConsultor == idconsultor
                             select c;

                return vcitas.ToList();
            }
            catch (Exception) { return null; }
        }
    }
}