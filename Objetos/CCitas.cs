using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESM.Objetos
{
    public static class CCitas
    {
        #region Propiedades Publicas y Privadas

        static Model.AgendaESMDataContext dbAgenda = new Model.AgendaESMDataContext();

        #endregion


        public static List<ESM.Model.CitasAgenda> ObtenerCitas(string idconsultor)
        {
            try
            {
                var vcitas = from c in dbAgenda.CitasAgenda
                             join con in dbAgenda.ConsultoresAgenda on c.IdConsultor equals con.IdConsultor
                             where con.Identificacion == idconsultor
                             select c;

                return vcitas.ToList();
            }
            catch (Exception) { return null; }
        }
    }
}