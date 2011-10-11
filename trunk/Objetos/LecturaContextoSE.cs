using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ESM.Model;

namespace ESM.LecturasContexto
{
    public class LecturaContextoSECRE
    {
        ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();

        #region Propiedades Publicas y Privadas para Modulo Uno

        private int idlectura;

        public int IdLectura
        {
            get { return idlectura; }
            set { idlectura = value; }
        }


        private string _2_1;

        public string __2_1
        {
            get { return _2_1; }
            set { _2_1 = value; }
        }

        private bool _2_2;

        public bool __2_2
        {
            get { return _2_2; }
            set { _2_2 = value; }
        }

        private string _2_2_1;

        public string __2_2_1
        {
            get { return _2_2_1; }
            set { _2_2_1 = value; }
        }

        private string _2_2_2;

        public string __2_2_2
        {
            get { return _2_2_2; }
            set { _2_2_2 = value; }
        }

        private bool _2_2_3EE;

        public bool __2_2_3EE
        {
            get { return _2_2_3EE; }
            set { _2_2_3EE = value; }
        }

        private int _2_2_3EE_Cant;

        public int __2_2_3EE_Cant
        {
            get { return _2_2_3EE_Cant; }
            set { _2_2_3EE_Cant = value; }
        }

        private bool _2_2_3EST;

        public bool __2_2_3EST
        {
            get { return _2_2_3EST; }
            set { _2_2_3EST = value; }
        }

        private int _2_2_3EST_Cant;

        public int __2_2_3EST_Cant
        {
            get { return _2_2_3EST_Cant; }
            set { _2_2_3EST_Cant = value; }
        }

        private bool _2_2_3EDU;

        public bool __2_2_3EDU
        {
            get { return _2_2_3EDU; }
            set { _2_2_3EDU = value; }
        }

        private int _2_2_3EDU_Cant;

        public int __2_2_3EDU_Cant
        {
            get { return _2_2_3EDU_Cant; }
            set { _2_2_3EDU_Cant = value; }
        }

        private bool _2_2_3DIR;

        public bool __2_2_3DIR
        {
            get { return _2_2_3DIR; }
            set { _2_2_3DIR = value; }
        }

        private int _2_2_3DIR_Cant;

        public int __2_2_3DIR_Cant
        {
            get { return _2_2_3DIR_Cant; }
            set { _2_2_3DIR_Cant = value; }
        }

        private bool _2_2_3PAD;

        public bool __2_2_3PAD
        {
            get { return _2_2_3PAD; }
            set { _2_2_3PAD = value; }
        }

        private int _2_2_3PAD_Cant;

        public int __2_2_3PAD_Cant
        {
            get { return _2_2_3PAD_Cant; }
            set { _2_2_3PAD_Cant = value; }
        }

        private string _2_2_3Otro_Cual_1;

        public string __2_2_3Otro_Cual_1
        {
            get { return _2_2_3Otro_Cual_1; }
            set { _2_2_3Otro_Cual_1 = value; }
        }

        private int _2_2_3Otro_Cual_1_Cant;

        public int __2_2_3Otro_Cual_1_Cant
        {
            get { return _2_2_3Otro_Cual_1_Cant; }
            set { _2_2_3Otro_Cual_1_Cant = value; }
        }

        private string _2_2_3Otro_Cual_2;

        public string __2_2_3Otro_Cual_2
        {
            get { return _2_2_3Otro_Cual_2; }
            set { _2_2_3Otro_Cual_2 = value; }
        }

        private int _2_2_3Otro_Cual_2_Cant;

        public int __2_2_3Otro_Cual_2_Cant
        {
            get { return _2_2_3Otro_Cual_2_Cant; }
            set { _2_2_3Otro_Cual_2_Cant = value; }
        }

        private string _2_2_3Otro_Cual_3;

        public string __2_2_3Otro_Cual_3
        {
            get { return _2_2_3Otro_Cual_3; }
            set { _2_2_3Otro_Cual_3 = value; }
        }

        private int _2_2_3Otro_Cual_3_Cant;

        public int __2_2_3Otro_Cual_3_Cant
        {
            get { return _2_2_3Otro_Cual_3_Cant; }
            set { _2_2_3Otro_Cual_3_Cant = value; }
        }

        private string _2_2_3Otro_Cual_4;

        public string __2_2_3Otro_Cual_4
        {
            get { return _2_2_3Otro_Cual_4; }
            set { _2_2_3Otro_Cual_4 = value; }
        }

        private int _2_2_3Otro_Cual_4_Cant;

        public int __2_2_3Otro_Cual_4_Cant
        {
            get { return _2_2_3Otro_Cual_4_Cant; }
            set { _2_2_3Otro_Cual_4_Cant = value; }
        }

        private string _2_2_3Otro_Cual_5;

        public string __2_2_3Otro_Cual_5
        {
            get { return _2_2_3Otro_Cual_5; }
            set { _2_2_3Otro_Cual_5 = value; }
        }

        private int _2_2_3Otro_Cual_5_Cant;

        public int __2_2_3Otro_Cual_5_Cant
        {
            get { return _2_2_3Otro_Cual_5_Cant; }
            set { _2_2_3Otro_Cual_5_Cant = value; }
        }

        private string _2_2_4;

        public string __2_2_4
        {
            get { return _2_2_4; }
            set { _2_2_4 = value; }
        }

        private string _2_2_5;

        public string __2_2_5
        {
            get { return _2_2_5; }
            set { _2_2_5 = value; }
        }

        private bool _2_3;

        public bool __2_3
        {
            get { return _2_3; }
            set { _2_3 = value; }
        }

        private string _2_3_1;

        public string __2_3_1
        {
            get { return _2_3_1; }
            set { _2_3_1 = value; }
        }

        #endregion

        #region Propiedades Publicas y Privadas para Modulo Dos

        private string _3_1;

        public string __3_1
        {
            get { return _3_1; }
            set { _3_1 = value; }
        }

        private string _3_2;

        public string __3_2
        {
            get { return _3_2; }
            set { _3_2 = value; }
        }

        private string _3_3;

        public string __3_3
        {
            get { return _3_3; }
            set { _3_3 = value; }
        }

        private string _3_4;

        public string __3_4
        {
            get { return _3_4; }
            set { _3_4 = value; }
        }

        private string _3_5;

        public string __3_5
        {
            get { return _3_5; }
            set { _3_5 = value; }
        }
        private string _3_6;

        public string __3_6
        {
            get { return _3_6; }
            set { _3_6 = value; }
        }
        #endregion

        #region Propiedades Publicas y Privadas para Modulo Tres
        private bool _4_1;

        public bool __4_1
        {
            get { return _4_1; }
            set { _4_1 = value; }
        }

        private string _4_1_1;

        public string __4_1_1
        {
            get { return _4_1_1; }
            set { _4_1_1 = value; }
        }

        #endregion

        #region Propiedades Publicas y Privadas para Modulo Cuatro
        private bool _5_1_RADDEP;

        public bool __5_1_RADDEP
        {
            get { return _5_1_RADDEP; }
            set { _5_1_RADDEP = value; }
        }

        private bool _5_1_RADMUN;

        public bool __5_1_RADMUN
        {
            get { return _5_1_RADMUN; }
            set { _5_1_RADMUN = value; }
        }

        private bool _5_1_PRENDEP;

        public bool __5_1_PRENDEP
        {
            get { return _5_1_PRENDEP; }
            set { _5_1_PRENDEP = value; }
        }
        private bool _5_1_PRENMUN;

        public bool __5_1_PRENMUN
        {
            get { return _5_1_PRENMUN; }
            set { _5_1_PRENMUN = value; }
        }

        private bool _5_1_TELDEP;

        public bool __5_1_TELDEP
        {
            get { return _5_1_TELDEP; }
            set { _5_1_TELDEP = value; }
        }

        private bool _5_1_TELMUN;

        public bool __5_1_TELMUN
        {
            get { return _5_1_TELMUN; }
            set { _5_1_TELMUN = value; }
        }

        private bool _5_1_INTDEP;

        public bool __5_1_INTDEP
        {
            get { return _5_1_INTDEP; }
            set { _5_1_INTDEP = value; }
        }

        private bool _5_1_INTMUN;

        public bool __5_1_INTMUN
        {
            get { return _5_1_INTMUN; }
            set { _5_1_INTMUN = value; }
        }


        private string _5_2;

        public string __5_2
        {
            get { return _5_2; }
            set { _5_2 = value; }
        }

        #endregion

        #region Ultimo Modulo

        private bool _1_18;

        public bool __1_18
        {
            get { return _1_18; }
            set { _1_18 = value; }
        }

        private string _1_19;

        public string __1_19
        {
            get { return _1_19; }
            set { _1_19 = value; }
        }

        private string _observaciones;

        public string Observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; }
        }

        #endregion

        #region Propiedades SE

        private int _idse;

        public int IdSec
        {
            get { return _idse; }
            set { _idse = value; }
        }

        private string _nombreSe;

        public string NombreSe
        {
            get { return _nombreSe; }
            set { _nombreSe = value; }
        }


        private string _nombresecre;

        public string NombreSecre
        {
            get { return _nombresecre; }
            set { _nombresecre = value; }
        }


        private string _telefonoSe;

        public string TelefonoSe
        {
            get { return _telefonoSe; }
            set { _telefonoSe = value; }
        }

        private string _telefonoSecretario;

        public string TelefonoSecretrio
        {
            get { return _telefonoSecretario; }
            set { _telefonoSecretario = value; }
        }

        private string _correoelectronico;

        public string CorreoElectronico
        {
            get { return _correoelectronico; }
            set { _correoelectronico = value; }
        }

        private string _direcccion;

        public string Direccion
        {
            get { return _direcccion; }
            set { _direcccion = value; }
        }

        private bool _departamental;

        public bool Departamental
        {
            get { return _departamental; }
            set { _departamental = value; }
        }

        private bool _municipal;

        public bool Municipal
        {
            get { return _municipal; }
            set { _municipal = value; }
        }

        #endregion

        public bool Almacenar(int idmedicion)
        {
            try
            {
                #region Objeto Lectura de Contexto
                LecturaContextoSE objLecturaContextoSE = new LecturaContextoSE
                {
                    #region Modulo 2

                    _2_1_ = _2_1,
                    _2_2_ = _2_2,
                    _2_2_1_ = _2_2_1,
                    _2_2_2_ = _2_2_2,
                    _2_2_3_DIR_ = _2_2_3DIR,
                    _2_2_3_DIR_Cant = _2_2_3DIR_Cant,
                    _2_2_3_EDU = _2_2_3EDU,
                    _2_2_3_EDU_Cant = _2_2_3EDU_Cant,
                    _2_2_3_EE_ = _2_2_3EE,
                    _2_2_3_EE_Cant = _2_2_3EE_Cant,                    
                    _2_2_3_EST_ = _2_2_3EST,
                    _2_2_3_EST_Cant = _2_2_3EST_Cant,
                    _2_2_3_OTR_1 = _2_2_3Otro_Cual_1,
                    _2_2_3_OTR_1_Cant = _2_2_3Otro_Cual_1_Cant,
                    _2_2_3_OTR_2 = _2_2_3Otro_Cual_2,
                    _2_2_3_OTR_2_Cant = _2_2_3Otro_Cual_2_Cant,
                    _2_2_3_OTR_3_ = _2_2_3Otro_Cual_3,
                    _2_2_3_OTR_3_Cant = _2_2_3Otro_Cual_3_Cant,
                    _2_2_3_OTR_4_ = _2_2_3Otro_Cual_4,
                    _2_2_3_OTR_4_Cant = _2_2_3Otro_Cual_4_Cant,
                    _2_2_3_OTR_5_ = _2_2_3Otro_Cual_5,
                    _2_2_3_OTR_5_Cant = _2_2_3Otro_Cual_5_Cant,
                    _2_2_3_PAD_ = _2_2_3PAD,
                    _2_2_3_PAD_Cant = _2_2_3PAD_Cant,
                    _2_2_4_ = _2_2_4,
                    _2_2_5_ = _2_2_5,
                    _2_3_ = _2_3,
                    _2_3_1_ = _2_3_1,

                    #endregion

                    #region Modulo 3
                    _3_1_ = _3_1,
                    _3_2_ = _3_2,
                    _3_3_ = _3_3,
                    _3_4_ = _3_4,
                    _3_5_ = _3_5,
                    _3_6_ = _3_6,
                    #endregion

                    #region Modulo 4

                    _4_1_ = _4_1,
                    _4_1_1_ = _4_1_1,

                    #endregion

                    #region Modulo 5

                    _5_1_INT = _5_1_INTDEP,
                    _5_1_INTM = _5_1_INTMUN,
                    _5_1_PREND = _5_1_PRENDEP,
                    _5_1_PRENM = _5_1_PRENMUN,
                    _5_1_RADD_ = _5_1_RADDEP,
                    _5_1_RADM = _5_1_RADMUN,
                    _5_1_TELD = _5_1_TELDEP,
                    _5_1_TELM = _5_1_TELMUN,
                    _5_2_ = _5_2,

                    #endregion

                    #region Modulo 6
                    _1_1_8_ = _1_18,
                    _1_1_9_ = _1_19,
                    Observaciones = _observaciones,
                    #endregion

                    IdSecretaria = _idse,
                    IdMedicion = idmedicion
                };
                #endregion

                db.LecturaContextoSEs.InsertOnSubmit(objLecturaContextoSE);
                db.SubmitChanges();

                idlectura = objLecturaContextoSE.IdLecturaContexto;
                return true;
            }
            catch (Exception) { return false; }
        }

        public bool Actualizar(int idlecturapr)
        {
            try
            {
                var lecturaac = (from l in db.LecturaContextoSEs
                                 where l.IdLecturaContexto == idlecturapr
                                 select l).Single();

                #region Modulo 2

                lecturaac._2_1_ = _2_1;
                lecturaac._2_2_ = _2_2;
                lecturaac._2_2_1_ = _2_2_1;
                lecturaac._2_2_2_ = _2_2_2;
                lecturaac._2_2_3_DIR_ = _2_2_3DIR;
                lecturaac._2_2_3_DIR_Cant = _2_2_3DIR_Cant;
                lecturaac._2_2_3_EDU = _2_2_3EDU;
                lecturaac._2_2_3_EDU_Cant = _2_2_3EDU_Cant;
                lecturaac._2_2_3_EE_ = _2_2_3EE;
                lecturaac._2_2_3_EE_Cant = _2_2_3EE_Cant;
                lecturaac._2_2_3_EST_ = _2_2_3EST;
                lecturaac._2_2_3_EST_Cant = _2_2_3EST_Cant;
                lecturaac._2_2_3_OTR_1 = _2_2_3Otro_Cual_1;
                lecturaac._2_2_3_OTR_1_Cant = _2_2_3Otro_Cual_1_Cant;
                lecturaac._2_2_3_OTR_2 = _2_2_3Otro_Cual_2;
                lecturaac._2_2_3_OTR_2_Cant = _2_2_3Otro_Cual_2_Cant;
                lecturaac._2_2_3_OTR_3_ = _2_2_3Otro_Cual_3;
                lecturaac._2_2_3_OTR_3_Cant = _2_2_3Otro_Cual_3_Cant;
                lecturaac._2_2_3_OTR_4_ = _2_2_3Otro_Cual_4;
                lecturaac._2_2_3_OTR_4_Cant = _2_2_3Otro_Cual_4_Cant;
                lecturaac._2_2_3_OTR_5_ = _2_2_3Otro_Cual_5;
                lecturaac._2_2_3_OTR_5_Cant = _2_2_3Otro_Cual_5_Cant;
                lecturaac._2_2_3_PAD_ = _2_2_3PAD;
                lecturaac._2_2_3_PAD_Cant = _2_2_3PAD_Cant;
                lecturaac._2_2_4_ = _2_2_4;
                lecturaac._2_2_5_ = _2_2_5;
                lecturaac._2_3_ = _2_3;
                lecturaac._2_3_1_ = _2_3_1;

                #endregion

                #region Modulo 3
                lecturaac._3_1_ = _3_1;
                lecturaac._3_2_ = _3_2;
                lecturaac._3_3_ = _3_3;
                lecturaac._3_4_ = _3_4;
                lecturaac._3_5_ = _3_5;
                lecturaac._3_6_ = _3_6;
                #endregion

                #region Modulo 4

                lecturaac._4_1_ = _4_1;
                lecturaac._4_1_1_ = _4_1_1;

                #endregion

                #region Modulo 5

                lecturaac._5_1_INT = _5_1_INTDEP;
                lecturaac._5_1_INTM = _5_1_INTMUN;
                lecturaac._5_1_PREND = _5_1_PRENDEP;
                lecturaac._5_1_PRENM = _5_1_PRENMUN;
                lecturaac._5_1_RADD_ = _5_1_RADDEP;
                lecturaac._5_1_RADM = _5_1_RADMUN;
                lecturaac._5_1_TELD = _5_1_TELDEP;
                lecturaac._5_1_TELM = _5_1_TELMUN;
                lecturaac._5_2_ = _5_2;

                #endregion

                #region Modulo 6
                lecturaac._1_1_8_ = _1_18;
                lecturaac._1_1_9_ = _1_19;
                lecturaac.Observaciones = _observaciones;
                #endregion

                db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }
        }

        public Secretaria_Educacion RecuperarSE(int idse)
        {
            try
            {
                var se = (from s in db.Secretaria_Educacions
                          where s.IdSecretaria == idse
                          select s).Single();

                return se;
            }
            catch (Exception) { return null; }

        }

        public IQueryable CargarMediciones(int idse)
        {
            try
            {
                var m = (from med in db.LecturaContextoSEs
                         where med.IdSecretaria == idse
                         select new { Medicion = med.IdMedicion, Fecha = med.Medicione.FechaMedicion }).Distinct();

                return m;
            }
            catch (Exception) { return null; }
        }

        public IQueryable ObtenerSE()
        {
            try
            {
                var se = from s in db.Secretaria_Educacions
                         select new { IdSecretaria = s.IdSecretaria, s.Nombre, s.Telefono, s.Direccion, Secretario_Educacion = s.Nombre_Secretario_Educacion, s.Telefonos_Secretario_Educacion };

                return se;
            }
            catch { return null; }
        }

        public int RegistrarMedicion()
        {
            try
            {
                Medicione objMediciones = new Medicione
                {
                    FechaMedicion = DateTime.Now
                };

                db.Mediciones.InsertOnSubmit(objMediciones);
                db.SubmitChanges();

                return objMediciones.IdMedicion;
            }
            catch (Exception) { return 0; }
        }

        public ESM.Model.LecturaContextoSE CargarLCSE(int idmedicion, int idse)
        {
            try
            {
                var lcse = (from lc in db.LecturaContextoSEs
                           where lc.IdMedicion == idmedicion && lc.IdSecretaria == idse
                           select lc).Single();

                return lcse;
            }
            catch (Exception) { return null; }

        }
    }
}