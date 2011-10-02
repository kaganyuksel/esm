using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ESM.Model;

namespace ESM.Objetos
{
    public class CLecturaContextoEE
    {
        #region Propiedades Privadas y Publicas

        ESM.Model.ESMBDDataContext _db = new Model.ESMBDDataContext();

        #region Seccion 1
        private bool _f11;

        public bool F11
        {
            get { return _f11; }
            set { _f11 = value; }
        }

        private bool _f12;

        public bool F12
        {
            get { return _f12; }
            set { _f12 = value; }
        }

        private bool _f13;

        public bool F13
        {
            get { return _f13; }
            set { _f13 = value; }
        }

        private bool _f14;

        public bool F14
        {
            get { return _f14; }
            set { _f14 = value; }
        }

        private bool _f15;

        public bool F15
        {
            get { return _f15; }
            set { _f15 = value; }
        }

        private int _d;

        public int D
        {
            get { return _d; }
            set { _d = value; }
        }

        private bool __1_2_urbana;

        public bool _1_2_Urbana
        {
            get { return __1_2_urbana; }
            set { __1_2_urbana = value; }
        }

        private bool __1_2_rural;

        public bool _1_2_Rural
        {
            get { return __1_2_rural; }
            set { __1_2_rural = value; }
        }

        private bool __1_2_c1;

        public bool _1_2_c1
        {
            get { return __1_2_c1; }
            set { __1_2_c1 = value; }
        }

        private bool __1_2_c2;

        public bool _1_2_c2
        {
            get { return __1_2_c2; }
            set { __1_2_c2 = value; }
        }

        private bool __1_2_c3;

        public bool _1_2_c3
        {
            get { return __1_2_c3; }
            set { __1_2_c3 = value; }
        }

        private bool __1_2_c4;

        public bool _1_2_c4
        {
            get { return __1_2_c4; }
            set { __1_2_c4 = value; }
        }

        private int _numerosedes;

        public int Sedes
        {
            get { return _numerosedes; }
            set { _numerosedes = value; }
        }

        private bool __c_1;

        public bool C_1
        {
            get { return __c_1; }
            set { __c_1 = value; }
        }

        private bool __c_2;

        public bool C_2
        {
            get { return __c_2; }
            set { __c_2 = value; }
        }
        private bool __c_3;

        public bool C_3
        {
            get { return __c_3; }
            set { __c_3 = value; }
        }
        private bool __c_4;

        public bool C_4
        {
            get { return __c_4; }
            set { __c_4 = value; }
        }
        private bool __c_5;

        public bool C_5
        {
            get { return __c_5; }
            set { __c_5 = value; }
        }
        #endregion

        #region Seccion 2
        private int __2_1;

        public int _2_1_
        {
            get { return __2_1; }
            set { __2_1 = value; }
        }

        private int __2_2_e1;

        public int _2_2_E1_
        {
            get { return __2_2_e1; }
            set { __2_2_e1 = value; }
        }

        private int __2_2_e2;

        public int _2_2_E2_
        {
            get { return __2_2_e2; }
            set { __2_2_e2 = value; }
        }

        private int __2_2_e3;

        public int _2_2_E3_
        {
            get { return __2_2_e3; }
            set { __2_2_e3 = value; }
        }

        private int __2_2_e4;

        public int _2_2_E4_
        {
            get { return __2_2_e4; }
            set { __2_2_e4 = value; }
        }

        private int __2_2_e5;

        public int _2_2_E5_
        {
            get { return __2_2_e5; }
            set { __2_2_e5 = value; }
        }

        private int __2_2_e6;

        public int _2_2_E6_
        {
            get { return __2_2_e6; }
            set { __2_2_e6 = value; }
        }

        private int __2_3_s1;

        public int _2_3_S1_
        {
            get { return __2_3_s1; }
            set { __2_3_s1 = value; }
        }

        private int __2_3_s2;

        public int _2_3_S2_
        {
            get { return __2_3_s2; }
            set { __2_3_s2 = value; }
        }

        private int __2_3_s3;

        public int _2_3_S3_
        {
            get { return __2_3_s3; }
            set { __2_3_s3 = value; }
        }

        private int __2_3_s4;

        public int _2_3_S4_
        {
            get { return __2_3_s4; }
            set { __2_3_s4 = value; }
        }

        private int __2_3_sNosabe;

        public int _2_3_SNosabe_
        {
            get { return __2_3_sNosabe; }
            set { __2_3_sNosabe = value; }
        }

        private int __2_3_sNoTiene;

        public int _2_3_SNoTiene_
        {
            get { return __2_3_sNoTiene; }
            set { __2_3_sNoTiene = value; }
        }

        private bool ___2_4_Si;

        public bool _2_4_Si
        {
            get { return ___2_4_Si; }
            set { ___2_4_Si = value; }
        }

        private bool ___2_4_No;

        public bool _2_4_No
        {
            get { return ___2_4_No; }
            set { ___2_4_No = value; }
        }

        private int __2_5_1;

        public int _2_5_1_
        {
            get { return __2_5_1; }
            set { __2_5_1 = value; }
        }

        private int __2_5_2;

        public int __2_5_2_
        {
            get { return __2_5_2; }
            set { __2_5_2 = value; }
        }

        private int __2_5_3_;

        public int _2_5_3_
        {
            get { return __2_5_3_; }
            set { __2_5_3_ = value; }
        }

        #endregion

        #region Seccion 3

        private string __3_1;

        public string _3_1_
        {
            get { return __3_1; }
            set { __3_1 = value; }
        }

        private string __3_2_;

        public string _3_2_
        {
            get { return __3_2_; }
            set { __3_2_ = value; }
        }

        private string __3_3_;

        public string _3_3
        {
            get { return __3_3_; }
            set { __3_3_ = value; }
        }

        private bool __3_4_Si;

        public bool _3_4_Si
        {
            get { return __3_4_Si; }
            set { __3_4_Si = value; }
        }

        private bool __3_4_No;

        public bool _3_4_No
        {
            get { return __3_4_No; }
            set { __3_4_No = value; }
        }
        private string __3_4_1;

        public string _3_4_1_
        {
            get { return __3_4_1; }
            set { __3_4_1 = value; }
        }

        private bool __3_5_Si;

        public bool _3_5_Si
        {
            get { return __3_5_Si; }
            set { __3_5_Si = value; }
        }

        private bool __3_5_No;

        public bool _3_5_No
        {
            get { return __3_5_No; }
            set { __3_5_No = value; }
        }

        private string __3_5_1;

        public string _3_5_1_
        {
            get { return __3_5_1; }
            set { __3_5_1 = value; }
        }

        private string __3_5_2;

        public string _3_5_2_
        {
            get { return __3_5_2; }
            set { __3_5_2 = value; }
        }

        private string __3_5_3;

        public string _3_5_3_
        {
            get { return __3_5_3; }
            set { __3_5_3 = value; }
        }

        private string __3_5_4;

        public string _3_5_4_
        {
            get { return __3_5_4; }
            set { __3_5_4 = value; }
        }

        private string __3_5_5;

        public string _3_5_5_
        {
            get { return __3_5_5; }
            set { __3_5_5 = value; }
        }

        private string __3_5_6;

        public string _3_5_6_
        {
            get { return __3_5_6; }
            set { __3_5_6 = value; }
        }

        private string __3_5_7;

        public string _3_5_7_
        {
            get { return __3_5_7; }
            set { __3_5_7 = value; }
        }


        private string __3_6;

        public string _3_6_
        {
            get { return __3_6; }
            set { __3_6 = value; }
        }

        private string __3_7;

        public string _3_7_
        {
            get { return __3_7; }
            set { __3_7 = value; }
        }

        private bool __3_8_Si;

        public bool _3_8_Si
        {
            get { return __3_8_Si; }
            set { __3_8_Si = value; }
        }

        private bool __3_8_No;

        public bool _3_8_No
        {
            get { return __3_8_No; }
            set { __3_8_No = value; }
        }

        private string __3_8_1;

        public string _3_8_1
        {
            get { return __3_8_1; }
            set { __3_8_1 = value; }
        }

        private bool __3_9_Si;

        public bool _3_9_Si
        {
            get { return __3_9_Si; }
            set { __3_9_Si = value; }
        }

        private bool __3_9_No;

        public bool _3_9_No
        {
            get { return __3_9_No; }
            set { __3_9_No = value; }
        }

        private string __3_9_1;

        public string _3_9_1_
        {
            get { return __3_9_1; }
            set { __3_9_1 = value; }
        }

        #endregion

        #region Seccion 4

        private bool __4_1_Si;

        public bool _4_1_Si
        {
            get { return __4_1_Si; }
            set { __4_1_Si = value; }
        }

        private bool __4_1_No;

        public bool _4_1_No
        {
            get { return __4_1_No; }
            set { __4_1_No = value; }
        }

        private bool __4_1_Algunas;

        public bool _4_1_Algunas
        {
            get { return __4_1_Algunas; }
            set { __4_1_Algunas = value; }
        }

        private int __4_2;

        public int _4_2_
        {
            get { return __4_2; }
            set { __4_2 = value; }
        }

        private bool __4_3_Si;

        public bool _4_3_Si
        {
            get { return __4_3_Si; }
            set { __4_3_Si = value; }
        }

        private bool __4_3_No;

        public bool _4_3_No
        {
            get { return __4_3_No; }
            set { __4_3_No = value; }
        }

        private string __4_3_1;

        public string _4_3_1_
        {
            get { return __4_3_1; }
            set { __4_3_1 = value; }
        }

        #endregion

        #region Seccion 5

        private bool __5_1_si;

        public bool _5_1_Si
        {
            get { return __5_1_si; }
            set { __5_1_si = value; }
        }

        private bool __5_1_no;

        public bool _5_1_No
        {
            get { return __5_1_no; }
            set { __5_1_no = value; }
        }

        private int __5_1_1;

        public int _5_1_1
        {
            get { return __5_1_1; }
            set { __5_1_1 = value; }
        }


        #endregion

        #region Observaciones

        private string _observaciones;

        public string Observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; }
        }

        #endregion

        #endregion

        /// <summary>
        /// Almacena la información de Lectura de Contexto para el Establecimiento Educativo.
        /// </summary>
        /// <param name="idee">Identificador del Establecimiento Educativo</param>
        /// <param name="idmedicion">Identificador de la medición a la que corresponde</param>
        /// <returns>Valor que representa el estado de la transacción</returns>
        public bool Almacenar(int idee, int idmedicion)
        {
            try
            {
                Model.LecturaContextoEE objLecturaContextoEE = new Model.LecturaContextoEE
                {
                    IdIE = idee,
                    IdMedicion = idmedicion,

                    #region Seccion 1

                    f11 = this._f11,
                    f12 = this._f12,
                    f13 = this._f13,
                    f14 = this._f14,
                    f15 = this._f15,
                    d = this._d,
                    _1_2bRural = this.__1_2_rural,
                    _1_2bUrbana = this.__1_2_urbana,

                    C_1 = __c_1,
                    C_2 = __c_2,
                    C_3 = __c_3,
                    C_4 = __c_4,
                    C_5 = __c_5,

                    #endregion

                    #region Seccion 2

                    _2_1 = this.__2_1,
                    _2_2_E1 = this.__2_2_e1,
                    _2_2_E2 = this.__2_2_e2,
                    _2_2_E3 = this.__2_2_e3,
                    _2_2_E4 = this.__2_2_e4,
                    _2_2_E5 = this.__2_2_e5,
                    _2_2_E6 = this.__2_2_e6,
                    _2_3_S1 = this.__2_3_s1,
                    _2_3_S2 = this.__2_3_s2,
                    _2_3_S3 = this.__2_3_s3,
                    _2_3_S4 = this.__2_3_s4,
                    _2_3_NoSabe = this.__2_3_sNosabe,
                    _2_3_NoTiene = this.__2_3_sNoTiene,
                    _2_4_Si = this.___2_4_Si,
                    _2_4_No = this.___2_4_No,
                    _2_5_1 = this.__2_5_1,
                    _2_5_2 = this.__2_5_2,
                    _2_5_3 = this.__2_5_3_,

                    #endregion

                    #region Seccion 3

                    _3_1 = this.__3_1,
                    _3_2 = this.__3_2_,
                    _3_3 = this.__3_3_,
                    _3_4_Si = this.__3_4_Si,
                    _3_4_No = this.__3_4_No,
                    _3_4_1 = this.__3_4_1,
                    _3_5_Si = this.__3_5_Si,
                    _3_5_No = this.__3_5_No,
                    _3_5_1 = this.__3_5_1,
                    _3_5_2 = this.__3_5_2,
                    _3_5_3 = this.__3_5_3,
                    _3_5_4 = this.__3_5_4,
                    _3_5_5 = this.__3_5_5,
                    _3_5_6 = this.__3_5_6,
                    _3_5_7 = this.__3_5_7,
                    _3_6 = this.__3_6,
                    _3_7 = this.__3_7,
                    _3_8_Si = this.__3_8_Si,
                    _3_8_No = this.__3_8_No,
                    _3_8_1 = this.__3_8_1,
                    _3_9_Si = this.__3_9_Si,
                    _3_9_No = this.__3_9_No,
                    _3_9_1 = this.__3_9_1,

                    #endregion

                    #region Seccion 4

                    _4_1_Si = this.__4_1_Si,
                    _4_1_No = this.__4_1_No,
                    _4_1_Algunas = this.__4_1_Algunas,
                    _4_2 = this.__4_2,
                    _4_3_Si = this._4_3_Si,
                    _4_3_No = this._4_3_No,
                    _4_3_1 = this.__4_3_1,

                    #endregion

                    #region Seccion 5

                    _5_1_Si = this.__5_1_si,
                    _5_1_No = this.__5_1_no,
                    _5_1_1 = this.__5_1_1,

                    #endregion

                    #region Seccion Observaciones

                    Observaciones = _observaciones

                    #endregion

                };

                _db.LecturaContextoEE.InsertOnSubmit(objLecturaContextoEE);
                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }
        }

        public bool Actualizar(int idee, int idmedicion)
        {
            try
            {
                var lcee = (from lc in _db.LecturaContextoEE
                            where lc.IdIE == idee && lc.IdMedicion == idmedicion
                            select lc).Single(); ;

                #region Seccion 1

                lcee.Mediciones.FechaMedicion = DateTime.Now.AddHours(2);

                lcee.f11 = this._f11;
                lcee.f12 = this._f12;
                lcee.f13 = this._f13;
                lcee.f14 = this._f14;
                lcee.f15 = this._f15;
                lcee.C_1 = this.__c_1;
                lcee.C_2 = this.__c_2;
                lcee.C_3 = this.__c_3;
                lcee.C_4 = this.__c_4;
                lcee.C_5 = this.__c_5;
                lcee.d = this._d;
                lcee._1_2bRural = this.__1_2_rural;
                lcee._1_2bUrbana = this.__1_2_urbana;

                #endregion

                #region Seccion 2

                lcee._2_1 = this.__2_1;
                lcee._2_2_E1 = this.__2_2_e1;
                lcee._2_2_E2 = this.__2_2_e2;
                lcee._2_2_E3 = this.__2_2_e3;
                lcee._2_2_E4 = this.__2_2_e4;
                lcee._2_2_E5 = this.__2_2_e5;
                lcee._2_2_E6 = this.__2_2_e6;
                lcee._2_3_S1 = this.__2_3_s1;
                lcee._2_3_S2 = this.__2_3_s2;
                lcee._2_3_S3 = this.__2_3_s3;
                lcee._2_3_S4 = this.__2_3_s4;
                lcee._2_3_NoSabe = this.__2_3_sNosabe;
                lcee._2_3_NoTiene = this.__2_3_sNoTiene;
                lcee._2_4_Si = this.___2_4_Si;
                lcee._2_4_No = this.___2_4_No;
                lcee._2_5_1 = this.__2_5_1;
                lcee._2_5_2 = this.__2_5_2;
                lcee._2_5_3 = this.__2_5_3_;

                #endregion

                #region Seccion 3

                lcee._3_1 = this.__3_1;
                lcee._3_2 = this.__3_2_;
                lcee._3_3 = this.__3_3_;
                lcee._3_4_Si = this.__3_4_Si;
                lcee._3_4_No = this.__3_4_No;
                lcee._3_4_1 = this.__3_4_1;
                lcee._3_5_Si = this.__3_5_Si;
                lcee._3_5_No = this.__3_5_No;
                lcee._3_5_1 = this.__3_5_1;
                lcee._3_5_2 = this.__3_5_2;
                lcee._3_5_3 = this.__3_5_3;
                lcee._3_5_4 = this.__3_5_4;
                lcee._3_5_5 = this.__3_5_5;
                lcee._3_5_6 = this.__3_5_6;
                lcee._3_5_7 = this.__3_5_7;
                lcee._3_6 = this.__3_6;
                lcee._3_7 = this.__3_7;
                lcee._3_8_Si = this.__3_8_Si;
                lcee._3_8_No = this.__3_8_No;
                lcee._3_8_1 = this.__3_8_1;
                lcee._3_9_Si = this.__3_9_Si;
                lcee._3_9_No = this.__3_9_No;
                lcee._3_9_1 = this.__3_9_1;

                #endregion

                #region Seccion 4

                lcee._4_1_Si = this.__4_1_Si;
                lcee._4_1_No = this.__4_1_No;
                lcee._4_1_Algunas = this.__4_1_Algunas;
                lcee._4_2 = this.__4_2;
                lcee._4_3_Si = this._4_3_Si;
                lcee._4_3_No = this._4_3_No;
                lcee._4_3_1 = this.__4_3_1;

                #endregion

                #region Seccion 5

                lcee._5_1_Si = this.__5_1_si;
                lcee._5_1_No = this.__5_1_no;
                lcee._5_1_1 = this.__5_1_1;

                #endregion

                #region Seccion Observaciones

                lcee.Observaciones = _observaciones;

                #endregion

                _db.SubmitChanges();

                return true;
            }
            catch (Exception) { return false; }

        }


        public ESM.Model.LecturaContextoEE ObtenerLCEE(int idmedicion)
        {
            try
            {
                var lcee = (from l in _db.LecturaContextoEE
                            where l.IdMedicion == idmedicion
                            select l).Single();

                return lcee;
            }
            catch (Exception) { return null; }
        }
    }
}