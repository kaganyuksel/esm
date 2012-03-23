using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Web.Security;
using System.Data;
using ESM.Model;
using Encrypt_and_decrypt;


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
            CCryptography objCCryptography = new CCryptography();
            usuario = usuario.Trim();
            contrasena = objCCryptography.Encrypt(contrasena,"{MD5/MGGROUP@Security}");

            using (ESM.Model.ESMBDDataContext db = new ESM.Model.ESMBDDataContext())
            {
                string idusuario = null;
                Table<Usuario> tUsuarios = db.GetTable<Usuario>();
                var rUsuarios = from u in tUsuarios
                                where u.Contrasena == contrasena && u.Usuario1 == usuario
                                select new { u.IdUsuario };

                foreach (var r in rUsuarios)
                {
                    idusuario = r.IdUsuario.ToString();
                }

                if (rUsuarios.Count() != 0)
                {
                    var tkc = new FormsAuthenticationTicket(usuario, true, 15000);
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


