using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ESM
{
    public partial class save_dates : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            try
            {
                string Resp;
                if (Request.QueryString.Get("id") != null && Request.QueryString.Get("valor") != null)
                {
                    Resp = Guardar_fechas(Convert.ToInt32(Request.QueryString.Get("id")), Request.QueryString.Get("valor").ToString(), Convert.ToByte(Request.QueryString.Get("tipo")));
                    if (Resp == "Ok")
                        Response.Write("Fecha actualizada correctamente");
                    else
                        Response.Write(Resp);
                }
                else
                    Response.Write("Se esperaban datos correctos");

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

            Response.End();
        }

        private string Guardar_fechas(int IndicadorId, string Newdate, Byte Tipo)
        {
            try
            {
                ESM.Model.ESMBDDataContext db = new ESM.Model.ESMBDDataContext();

                var ind = (from c in db.Indicadores
                           where c.Id == IndicadorId
                           select c).Single();

                if (Tipo == 1)
                    ind.fecha_indicador_inicial = Convert.ToDateTime(Newdate);
                else if (Tipo == 2)
                    ind.fecha_indicador_final = Convert.ToDateTime(Newdate);

                try
                {
                    db.SubmitChanges();
                    return "Ok";
                }
                catch (LinqDataSourceValidationException exlinq)
                {
                    return exlinq.Message;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}