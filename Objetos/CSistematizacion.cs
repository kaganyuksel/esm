using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESM.Objetos
{
    public class CSistematizacion
    {
        #region Propiedades Publicas y Privadas

        ESM.Model.ESMBDDataContext _db = new ESM.Model.ESMBDDataContext();

        private string _p1;

        public string P1
        {
            get { return _p1; }
            set { _p1 = value; }
        }
        private string _p2;

        public string P2
        {
            get { return _p2; }
            set { _p2 = value; }
        }
        private string _p3;

        public string P3
        {
            get { return _p3; }
            set { _p3 = value; }
        }
        private string _p4;

        public string P4
        {
            get { return _p4; }
            set { _p4 = value; }
        }
        private string _p5;

        public string P5
        {
            get { return _p5; }
            set { _p5 = value; }
        }
        private string _p6;

        public string P6
        {
            get { return _p6; }
            set { _p6 = value; }
        }

        private int _idee;

        public int Idee
        {
            get { return _idee; }
            set { _idee = value; }
        }

        private int _idmed;

        public int Idmed
        {
            get { return _idmed; }
            set { _idmed = value; }
        }

        #endregion

        public CSistematizacion()
        {
            //TODO: JCMM: Codigo para constructor
        }

        public bool Almacenar(bool SE = false)
        {
            try
            {
                if (!SE)
                {
                    ESM.Model.Sistematizacion objSistematizacion = new Model.Sistematizacion
                    {
                        IdEE = _idee,
                        IdMedicion = _idmed,
                        P1 = _p1,
                        P2 = _p2,
                        P3 = _p3,
                        P4 = _p4,
                        P5 = _p5,
                        P6 = _p6
                    };

                    _db.Sistematizacions.InsertOnSubmit(objSistematizacion);
                    _db.SubmitChanges();
                }
                else
                {
                    ESM.Model.SistematizacionSE objSistematizacion = new Model.SistematizacionSE
                    {
                        IdSE = _idee,
                        IdMedicion = _idmed,
                        P1 = _p1,
                        P2 = _p2,
                        P3 = _p3,
                        P4 = _p4,
                        P5 = _p5,
                        P6 = _p6
                    };

                    _db.SistematizacionSEs.InsertOnSubmit(objSistematizacion);
                    _db.SubmitChanges();
                }

                return true;
            }
            catch (Exception) { return false; }

        }

        public bool Actualizar(int idsis, bool SE = false)
        {
            try
            {
                if (!SE)
                {
                    var upsistematizacion = (from s in _db.Sistematizacions
                                             where s.IdSistematizacion == idsis
                                             select s).Single();

                    upsistematizacion.P1 = _p1;
                    upsistematizacion.P2 = _p2;
                    upsistematizacion.P3 = _p3;
                    upsistematizacion.P4 = _p4;
                    upsistematizacion.P5 = _p5;
                    upsistematizacion.P6 = _p6;

                    _db.SubmitChanges();
                }
                else
                {
                    var upsistematizacion = (from s in _db.SistematizacionSEs
                                             where s.IdSistematizacion == idsis
                                             select s).Single();

                    upsistematizacion.P1 = _p1;
                    upsistematizacion.P2 = _p2;
                    upsistematizacion.P3 = _p3;
                    upsistematizacion.P4 = _p4;
                    upsistematizacion.P5 = _p5;
                    upsistematizacion.P6 = _p6;

                    _db.SubmitChanges();
                }
                return true;
            }
            catch (Exception) { return false; }

        }

        public static ESM.Model.Sistematizacion GetSistematizacion(int idmedicion)
        {
            try
            {
                var csist = (from s in new ESM.Model.ESMBDDataContext().Sistematizacions
                             where s.IdMedicion == idmedicion
                             select s).Single();

                return csist;

            }
            catch (Exception) { return null; }

        }

        public static ESM.Model.SistematizacionSE GetSistematizacionSE(int idmedicion)
        {
            try
            {
                var csist = (from s in new ESM.Model.ESMBDDataContext().SistematizacionSEs
                             where s.IdMedicion == idmedicion
                             select s).Single();

                return csist;

            }
            catch (Exception) { return null; }

        }

        public static IQueryable ObtenerMediciones(int idee)
        {
            try
            {
                var medsis = from s in new ESM.Model.ESMBDDataContext().Sistematizacions
                             where s.IdEE == idee
                             select new { s.Medicione.IdMedicion, Fecha = s.Medicione.FechaMedicion };

                return medsis;
            }
            catch (Exception) { return null; }

        }

        public static IQueryable ObtenerMedicionesSE(int idse)
        {
            try
            {
                var medsis = from s in new ESM.Model.ESMBDDataContext().SistematizacionSEs
                             where s.IdSE == idse
                             select new { s.Medicione.IdMedicion, Fecha = s.Medicione.FechaMedicion };

                return medsis;
            }
            catch (Exception) { return null; }

        }

    }
}