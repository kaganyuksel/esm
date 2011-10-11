using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace ESM.Objetos
{
    public class CRoles
    {
        #region Propiedades Privadas y Publicas

        ESM.Model.ESMBDDataContext _db = new Model.ESMBDDataContext();

        private int _identificacion;

        public int Identificacion
        {
            get { return _identificacion; }
        }

        private int _idconsultor;

        public int IdConsultor
        {
            get { return _idconsultor; }
            set { _idconsultor = value; }
        }

        #endregion
        public CRoles()
        {

        }

        public string[] ObtenerRoles()
        {
            try
            {
                var rRoles = from r in _db.Roles
                             select r;
                string[] RolesToReturn = new string[rRoles.Count()];

                int count = 0;
                foreach (var item in rRoles)
                {
                    RolesToReturn[count] = item.Rol;
                    count++;
                }

                return RolesToReturn;
            }
            catch (Exception)
            {
                string[] Error = { "Error" };
                return Error;
            }
        }

        public string ObtenerRol(int idusuario)
        {
            try
            {
                var us = (from u in _db.Usuarios
                          join c in _db.Consultores on u.IdConsultor equals c.IdConsultor
                          where u.IdUsuario == idusuario
                          select new { u.Role.Rol, c.Identificacion, c.IdConsultor }).Single();
                _idconsultor = us.IdConsultor;
                _identificacion = (int)us.Identificacion;
                return us.Rol;
            }
            catch (Exception) { return String.Empty; }
        }

    }
}