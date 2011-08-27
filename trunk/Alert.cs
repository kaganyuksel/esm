using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace ESM
{
    public static class Alert
    {
        public static void CreateMessageAlertInUpdatePanel(UpdatePanel up, string strMessage)
        {
            string strScript = "alert('" + strMessage + "');";
            Guid guidKey = Guid.NewGuid();
            ScriptManager.RegisterStartupScript(up, up.GetType(), guidKey.ToString(), strScript, true);
        }
    }
}