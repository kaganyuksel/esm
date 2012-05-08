using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ESM
{
    public partial class Cita : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["id"] != null && Request.QueryString["tipo"] != null)
                {
                    string nombre = null;
                    string telefono = null;
                    string direccion = null;
                    string municipio = null;

                    int tipo = Convert.ToInt32(Request.QueryString["tipo"]);
                    int id = Convert.ToInt32(Request.QueryString["id"]);
                    int idcita = Convert.ToInt32(Request.QueryString["idcita"]);
                    if (tipo == 1)
                    {
                        Model.Establecimiento_Educativo objee = ESM.Objetos.CEE.ObtenerEE(id);
                        nombre = objee.Nombre.ToString();
                        telefono = objee.Telefono.ToString();
                        direccion = objee.Direccion.ToString();
                        municipio = objee.Municipio.ToString();
                    }
                    else
                    {
                        var se = (from s in new ESM.Model.ESMBDDataContext().Secretaria_Educacions
                                  where s.IdSecretaria == id
                                  select s).Single();

                        nombre = se.Nombre.ToString();
                        telefono = se.Telefono.ToString();
                        direccion = se.Direccion.ToString();
                        municipio = se.DepMun.ToString();
                    }

                    var cita = (from c in new ESM.Model.ESMBDDataContext().Citas
                                where c.IdCita == idcita
                                select c).Single();
                    
                    lblNombre.Text = nombre;
                    lblTelefono.Text = telefono;
                    lblDirección.Text = direccion;
                    lblmunicipio.Text = municipio;
                    string address = lblDirección.Text.Replace("KR", "Carrera") + ", " + municipio + ", Colombia";
                    //GoogleMap1.Address = address;
                    //Artem.Web.UI.Controls.GoogleMarker objGoogleMarker = new Artem.Web.UI.Controls.GoogleMarker(address);
                    ban_direccion.Value = address;
                    //GoogleMap1.Markers.Add(objGoogleMarker);
                }
            }
        }
    }
}