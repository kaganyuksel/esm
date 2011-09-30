﻿using System;
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
                             where c.LLamadas.Secretaria_Educacion.IdConsultor == idconsultor || c.LLamadas.Establecimiento_Educativo.Secretaria_Educacion.IdConsultor == idconsultor
                             select c;

                return vcitas.ToList();
            }
            catch (Exception) { return null; }
        }
    }
}