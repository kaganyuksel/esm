using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Web.Security;
using System.Data;
using ESM.Model;



public static class SecuritySesion
{
    #region Propiedades Privadas
    private static string _idoperadort;

    public static string IdOperadorc
    {
        get { return _idoperadort; }
        set { _idoperadort = value; }
    }


    #endregion
    public static string AutenticacionUsuario(string usuario, string contrasena)
    {
        try
        {
            usuario = usuario.Trim();
            contrasena = contrasena.Trim();

            using (ESM.Model.ESMBDDataContext db = new ESM.Model.ESMBDDataContext())
            {
                string idusuario = null;
                Table<Usuarios> tUsuarios = db.GetTable<Usuarios>();
                var rUsuarios = from u in tUsuarios
                                where u.Contrasena == contrasena && u.Usuario == usuario
                                select new { u.IdUsuario };

                foreach (var r in rUsuarios)
                {
                    idusuario = r.IdUsuario.ToString();
                }

                if (rUsuarios.Count() != 0)
                {
                    var tkc = new FormsAuthenticationTicket(usuario, true, 15);
                    string encriptar = FormsAuthentication.Encrypt(tkc);
                    HttpContext.Current.Response.Cookies.Add(
                        new HttpCookie(FormsAuthentication.FormsCookieName, encriptar));

                }

                return idusuario;
            }
        }
        catch (Exception)
        {

            return null;
        }
    }

    public static void CerrarSesion()
    {
        FormsAuthentication.SignOut();
    }
}


