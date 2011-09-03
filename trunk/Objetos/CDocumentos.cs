using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ESM.Model;

namespace EvaluationSettings
{
    public class CDocumentos
    {

        #region Propiedades Publicas y Privadas
        ESM.Model.ESMBDDataContext db = new ESM.Model.ESMBDDataContext();
        #endregion

        public CDocumentos()
        {

        }

        public IQueryable Show(int idmedicion)
        {
            try
            {
                var doc = from d in db.AsignaDocumentos
                          where d.IdMedicion == idmedicion
                          select new { Documento = d.Documentos.Documento, Actualizado = d.Fecha, Ruta = d.Ruta };
                return doc;
            }
            catch (Exception) { return null; }

        }

        public bool CargarDocumento(int idmedicion, int iddocumento, string ruta)
        {
            try
            {
                AsignaDocumentos objAsignaDocumentos = new AsignaDocumentos
                {
                    IdMedicion = idmedicion,
                    IdDocumento = iddocumento,
                    Fecha = DateTime.Now,
                    Ruta = ruta
                };

                db.AsignaDocumentos.InsertOnSubmit(objAsignaDocumentos);
                db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }
        }

        public bool ExisteDocumento(int iddocumento, int idmedicion)
        {
            try
            {
                var exist = from ad in db.AsignaDocumentos
                            where ad.IdDocumento == iddocumento && ad.IdMedicion == idmedicion
                            select ad;

                if (exist.Count() == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception) { return false; }
        }

        public bool EliminarArchivo(int idmedicion, int iddocumento)
        {
            var deletedocumentos = from a in db.AsignaDocumentos
                                   where a.IdMedicion == idmedicion && a.IdDocumento == iddocumento
                                   select a;
            foreach (var item in deletedocumentos)
            {
                db.AsignaDocumentos.DeleteOnSubmit(item);
            }


            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }
    }
}