using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using ESM.Objetos;

namespace ESM
{
    public partial class json : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            try
            {
                if (Request.QueryString.Get("id") != null)
                {
                    int idconsultor = Convert.ToInt32(Request.QueryString.Get("id"));
                    Response.Write(Consulta(idconsultor));
                }
                else
                    Response.Write("La información proporcionada no es correcta.");

            }
            catch (Exception) { Response.Write(""); }

            Response.End();
        }

        protected string Consulta(int idConsultor)
        {
            try
            {
                string objevents = String.Empty;
                JavaScriptSerializer objJavaScriptSerializer = new JavaScriptSerializer();
                var objFullCalendar = new CFullCalendar();

                List<Model.Cita> objCitas = CCitas.ObtenerCitas(idConsultor);

                foreach (var item in objCitas)
                {
                    objFullCalendar.id = item.IdCita;
                    objFullCalendar.backgroundColor = "#0369A8";
                    objFullCalendar.textColor = "white";

                    objFullCalendar.start = item.FechaInicio.ToString("yyyy/MM/dd HH:mm:ss");
                    objFullCalendar.end = item.FechaFin.ToString("yyyy/MM/dd HH:mm:ss");

                    int idrama = 0;
                    int tipo = -1;
                    if (item.IdEE != null)
                    {
                        idrama = (int)item.IdEE;
                        tipo = 1;
                        objFullCalendar.title = item.Establecimiento_Educativo.Nombre;
                    }
                    else
                    {
                        idrama = (int)item.IdSE;
                        tipo = 0;
                        objFullCalendar.title = item.Secretaria_Educacion.Nombre;
                    }

                    objFullCalendar.editable = true;
                    objFullCalendar.url = "JavaScript:$.prettyPhoto.open('/cita.aspx?id=" + idrama.ToString() + "&tipo=" + tipo.ToString() + "&idcita=" + item.IdCita.ToString() + "&iframe=true&width=100%&height=100%');";
                    objFullCalendar.clases = "pretty";
                    objevents = String.Concat(objevents, objJavaScriptSerializer.Serialize(objFullCalendar), ",");

                }
                objevents = objevents.Replace("start_date", "start").Trim();
                objevents = objevents.Replace("end_date", "end").Trim();
                objevents = String.Concat("[", objevents.Substring(0, objevents.Length - 1), "]");


                return objevents;
            }
            catch (Exception) { return null; }

        }

    }
}