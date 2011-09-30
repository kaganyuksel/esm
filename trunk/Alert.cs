using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace ESM
{
    public static class Alert
    {
        public static void Show(UpdatePanel objUpdatePanel, string Mensaje)
        {
            string alert = String.Format("alert('{0}');", Mensaje);
            Guid guidKey = Guid.NewGuid();
            ScriptManager.RegisterStartupScript(objUpdatePanel, objUpdatePanel.GetType(), guidKey.ToString(), alert, true);
        }

        
        
    }
}