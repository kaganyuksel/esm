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
                    string idconsultor = Request.QueryString.Get("id").ToString();
                    Response.Write(Consulta(idconsultor));
                }
                else
                    Response.Write("La información proporcionada no es correcta.");

            }
            catch (Exception) { Response.Write(""); }

            Response.End();
        }

        protected string Consulta(string idConsultor)
        {
            try
            {
                string objevents = String.Empty;
                JavaScriptSerializer objJavaScriptSerializer = new JavaScriptSerializer();
                var objFullCalendar = new CFullCalendar();

                List<Model.CitasAgenda> objCitas = CCitas.ObtenerCitas(idConsultor);

                foreach (var item in objCitas)
                {
                    objFullCalendar.id = item.IdCita;
                    objFullCalendar.backgroundColor = "#ffgghh";
                    objFullCalendar.textColor = "white";
                    if (item.FechaInicio == item.FechaFin)
                    {
                        objFullCalendar.allDay = true;
                        objFullCalendar.start = item.FechaInicio.ToString("yyyy/MM/dd");
                        objFullCalendar.end = item.FechaFin.ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        objFullCalendar.start = item.FechaInicio.ToString("yyyy/MM/dd");
                        objFullCalendar.end = item.FechaFin.ToString("yyyy/MM/dd");
                    }
                    objFullCalendar.title = item.LLamadasAgenda.InstitucionEducativaAgenda.Nombre;
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