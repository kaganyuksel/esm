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
    }
}