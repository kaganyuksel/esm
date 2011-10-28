using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESM.Objetos;

namespace ESM
{
    public partial class SistematizacionSE : System.Web.UI.Page
    {
        #region Objetos, Propiedades Publicas y Privadas
        ESM.Objetos.CRoles _objCRoles = new Objetos.CRoles();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;

            if (Request.IsAuthenticated)
            {
                if (!Page.IsPostBack)
                {

                    if (Session["idusuario"] != null)
                    {
                        int idusuario = Convert.ToInt32(Session["idusuario"]);
                        string rol = _objCRoles.ObtenerRol(idusuario);
                        if (rol == "Administrador")
                        {
                            ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();
                            var se = from s in db.Secretaria_Educacions
                                     where s.Sistematizacion == true
                                     select s;

                            gvSE.DataSource = se;
                            gvSE.DataBind();
                        }
                        else if (rol == "Consultor" || rol == "MEN")
                        {
                            ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();
                            var se = from s in db.Secretaria_Educacions
                                     where s.IdConsultor == _objCRoles.IdConsultor && s.Sistematizacion == true
                                     select s;

                            gvSE.DataSource = se;
                            gvSE.DataBind();

                        }
                    }
                    else
                        Response.Redirect("/Login.aspx");
                }
            }
            else
                Response.Redirect("Login.aspx");
        }

        protected void gvSE_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow objRow = gvSE.SelectedRow;
            int idsec = Convert.ToInt32(objRow.Cells[1].Text);
            Session.Add("idsesis", objRow.Cells[1].Text);

            CargarSistematizacion(idsec);

            IQueryable<ESM.Model.SistematizacionSE> mediciones = CEE.ObtenerMedicionesSESis(idsec);

            if (mediciones.Count() != 0 && mediciones != null)
            {
                var med = (from m in mediciones
                           select new { IdMedicion = m.IdMedicion, Fecha = m.Medicione.FechaMedicion }).Take(1);

                gvMediciones.DataSource = med;
                gvMediciones.DataBind();
                gvMediciones.Visible = true;
                btnRegistrar.Visible = false;
                gvSE.Visible = false;
            }
            else
                btnRegistrar.Visible = true;
        }

        protected void gvMediciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow objRow = gvMediciones.SelectedRow;

            int idmedicion = Convert.ToInt32(objRow.Cells[1].Text);
            Session.Add("sisidmedicion", idmedicion);
            CargarSistematizacion(idmedicion);
            LC.Visible = true;

        }

        protected void CargarSistematizacion(int idmedicion)
        {
            ESM.Model.SistematizacionSE objSistematizacion = CSistematizacion.GetSistematizacionSE(idmedicion);

            if (objSistematizacion != null)
            {
                Session.Add("idsis", objSistematizacion.IdSistematizacion);

                txt1.Text = objSistematizacion.P1;
                txt2.Text = objSistematizacion.P2;
                txt3.Text = objSistematizacion.P3;
                txt4.Text = objSistematizacion.P4;
                txt5.Text = objSistematizacion.P5;
                txt6.Text = objSistematizacion.P6;
            }

        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            int idmedicion = CEE.CrearMedicionLC(DateTime.Now);

            if (idmedicion != 0)
            {
                Session.Add("sisidmedicion", idmedicion);
                LC.Visible = true;
                btnRegistrar.Visible = false;
                gvSE.Visible = false;
            }
            else
                Response.Write("<script>alert('No es posible completar la operación.');</script>");
        }

        protected CSistematizacion Asignacion()
        {
            int idee = Convert.ToInt32(Session["idsesis"]);
            int idmedicion = Convert.ToInt32(Session["sisidmedicion"]);

            CSistematizacion objCSistematizacion = new CSistematizacion();

            objCSistematizacion.Idee = idee;
            objCSistematizacion.Idmed = idmedicion;
            objCSistematizacion.P1 = txt1.Text;
            objCSistematizacion.P2 = txt2.Text;
            objCSistematizacion.P3 = txt3.Text;
            objCSistematizacion.P4 = txt4.Text;
            objCSistematizacion.P5 = txt5.Text;
            objCSistematizacion.P6 = txt6.Text;

            return objCSistematizacion;
        }

        protected void btnalmacenar_Click(object sender, EventArgs e)
        {
            CSistematizacion objCSistematizacion = Asignacion();

            if (Session["idsis"] == null)
            {
                if (objCSistematizacion.Almacenar(true))
                    Response.Write("<script>alert('El proceso de almacenamiento finalizó correctamente.');</script>");
                else
                    Response.Write("<script>alert('El proceso de almacenamiento finalizó sin éxito.');</script>");
            }
            else
            {
                int idsis = Convert.ToInt32(Session["idsis"]);

                if (objCSistematizacion.Actualizar(idsis, true))
                    Response.Write("<script>alert('El proceso de actualización finalizó correctamente.');</script>");
                else
                    Response.Write("<script>alert('El proceso de actualización finalizó sin éxito.');</script>");
            }
        }

    }
}