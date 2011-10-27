using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESM.Objetos;

namespace ESM
{
    public partial class Sistematizacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                CRoles objCRoles = new CRoles();
                if (!Page.IsPostBack)
                {
                    Session.Remove("sisidmedicion");
                    int idusuario = Convert.ToInt32(Session["idusuario"]);
                    string rol = objCRoles.ObtenerRol(idusuario);

                    if (rol == "Administrador")
                    {
                        /*Cargo el control gridview con el data source obtenido de instituciones educativas*/
                        gvResultados.DataSourceID = "ldsies";
                        gvResultados.DataBind();
                    }
                    else if (rol == "Consultor" || rol == "MEN")
                    {
                        gvResultados.DataSource = CEE.ObtenerEEs(objCRoles.IdConsultor, true);
                        gvResultados.DataBind();
                    }
                    else
                    {
                        Response.Write("<script>alert('Acceso Denagado!');</script>");
                        Response.Redirect("Login.aspx");
                    }

                    modEESeleccion.Visible = true;
                }
            }
            else
                Response.Redirect("/Login.aspx");
        }

        protected void gvResultados_SelectedIndexChanged(object sender, EventArgs e)
        {
            modEESeleccion.Visible = false;
            ModMediciones.Visible = true;

            Session.Add("codiesis", gvResultados.SelectedRow.Cells[1].Text);
            string codie = Session["codiesis"].ToString();

            Label objIDIE = (Label)gvResultados.SelectedRow.FindControl("IDIE");

            int idie = Convert.ToInt32(objIDIE.Text);
            Session.Add("ideesis", idie);
            ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();
            var ie = from ies in db.Establecimiento_Educativos
                     where ies.IdIE == idie
                     select ies;

            foreach (var item in ie)
            {
                txtNombre.Text = item.Nombre;
                txtTelefonos.Text = item.Telefono;
                txtDireccion.Text = item.Direccion;
                txtCodigo.Text = item.CodigoDane;
                txtCorreoElectronico.Text = item.Email;
                txtNombre.Enabled = false;
                txtTelefonos.Enabled = false;
                txtDireccion.Enabled = false;
                txtCorreoElectronico.Enabled = false;
                txtCodigo.Enabled = false;
            }

            IQueryable<ESM.Model.Sistematizacion> mediciones = CEE.ObtenerMedicionesEESis(idie);

            if (mediciones.Count() != 0 && mediciones != null)
            {
                var med = (from m in mediciones
                           select new { IdMedicion = m.IdMedicion, Fecha = m.Medicione.FechaMedicion }).Take(1);

                gvMediciones.DataSource = med;
                gvMediciones.DataBind();
                gvMediciones.Visible = true;
                btnRegistrar.Visible = false;
            }
            else
                btnRegistrar.Visible = true;


        }

        protected void gvResultados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                this.gvResultados.PageIndex = e.NewPageIndex;
                CRoles objCRoles = new CRoles();

                int idusuario = Convert.ToInt32(Session["idusuario"]);
                string rol = objCRoles.ObtenerRol(idusuario);


                if (rol == "Administrador")
                {
                    /*Cargo el control gridview con el data source obtenido de instituciones educativas*/
                    gvResultados.DataSourceID = "ldsies";
                }
                else if (rol == "Consultor" || rol == "MEN")
                {
                    gvResultados.DataSource = CEE.ObtenerEEs(objCRoles.IdConsultor, true);
                    gvResultados.DataBind();
                }

            }
            catch (Exception) { }

        }

        protected void gvMediciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow objRow = gvMediciones.SelectedRow;

            int idmedicion = Convert.ToInt32(objRow.Cells[1].Text);
            Session.Add("sisidmedicion", idmedicion);
            CargarSistematizacion(idmedicion);
            LC.Visible = true;
            infoEE.Visible = true;

        }

        protected void CargarSistematizacion(int idmedicion)
        {
            ESM.Model.Sistematizacion objSistematizacion = CSistematizacion.GetSistematizacion(idmedicion);

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
                infoEE.Visible = true;
                btnRegistrar.Visible = false;
            }
            else
                Response.Write("<script>alert('No es posible completar la operación.');</script>");
        }

        protected CSistematizacion Asignacion()
        {
            int idee = Convert.ToInt32(Session["ideesis"]);
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
                if (objCSistematizacion.Almacenar())
                    Response.Write("<script>alert('El proceso de almacenamiento finalizó correctamente.');</script>");
                else
                    Response.Write("<script>alert('El proceso de almacenamiento finalizó sin éxito.');</script>");
            }
            else
            {
                int idsis = Convert.ToInt32(Session["idsis"]);

                if (objCSistematizacion.Actualizar(idsis))
                    Response.Write("<script>alert('El proceso de actualización finalizó correctamente.');</script>");
                else
                    Response.Write("<script>alert('El proceso de actualización finalizó sin éxito.');</script>");
            }
        }




    }
}