using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESM.Objetos;
using EvaluationSettings;

namespace ESM
{
    public partial class ActaVisitaSistematizacion : System.Web.UI.Page
    {

        #region Propiedades Publicas y Privadas

        protected CRoles _objCRoles = new CRoles();

        #endregion

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

        protected bool Filtro(string texto)
        {
            try
            {
                /*Instancio*/
                Model.ESMBDDataContext db = new Model.ESMBDDataContext();

                var rFiltro = from i in db.Secretaria_Educacions
                              where i.Nombre.Contains(texto)
                              select i;

                gvResultados.DataSourceID = null;
                gvResultados.DataSource = rFiltro;
                gvResultados.DataBind();

                return true;
            }
            catch (Exception) { return false; }

        }

        protected void gvMediciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow objRow = gvMediciones.SelectedRow;

            int idmedicion = Convert.ToInt32(objRow.Cells[1].Text);
            Session.Remove("idmedAccsis");
            Session.Add("idmedAccsis", idmedicion);
            ModActaVisitaEE.Visible = true;
            int exist = ExistActa(idmedicion);
            int idactavisita = 0;

            documentos.Title = "Documentación";
            documentos.HRef = Request.Url.Scheme + "://" + Request.Url.Authority + "/ModuloDocumentosSis.aspx?idmedicion=" + idmedicion.ToString() + "&iframe=true&amp;width=100%&amp;height=100%";

            Session.Remove("idactasis");

            if (exist == 0)
            {
                idactavisita = InsertarActaVisita();
                Session.Add("idactasis", idactavisita);
            }
            else
            {
                Session.Add("idactasis", exist);
                CargarActores(exist);
            }

            txtFechaInicial.Text = objRow.Cells[2].Text;
            txtFechaInicial.Enabled = false;

            txtFechaFinal.Text = DateTime.Now.ToString();
            txtFechaFinal.Enabled = false;
        }

        protected int ExistActa(int medicion)
        {
            try
            {
                ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();

                var ac = from a in db.ActaVisitaSistematizacions
                         where a.IdMedicion == medicion
                         select a;

                if (ac.Count() != 0)
                    return ac.First().IdActaVisita;
                else
                    return 0;
            }
            catch (Exception) { return 0; }
        }

        protected int InsertarActaVisita()
        {
            try
            {
                int idmedicion = Convert.ToInt32(Session["idmedAccsis"]);
                int idusuario = Convert.ToInt32(Session["idusuario"]);
                int idee = Convert.ToInt32(Session["idieaccsis"]);

                ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();

                ESM.Model.ActaVisitaSistematizacion objActaVisitaSis = new Model.ActaVisitaSistematizacion
                {
                    IdUsuario = idusuario,
                    IdEE = idee,
                    IdMedicion = idmedicion,
                };

                db.ActaVisitaSistematizacions.InsertOnSubmit(objActaVisitaSis);
                db.SubmitChanges();

                return objActaVisitaSis.IdActaVisita;
            }
            catch (Exception)
            {
                return 0;
            }

        }

        protected void CargarActores(int idacta)
        {
            try
            {
                ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();

                var sisacc = from s in db.AsociadoActaVisitaSistematizacions
                             where s.IdActaVisita == idacta
                             select new { s.Nombre, s.Telefono, s.CorreoElectronico, s.Cargo, s.Institucion };

                gvIndividuos.DataSource = sisacc;
                gvIndividuos.DataBind();

                gvIndividuos.RowHeaderColumn.Replace("Telefono", "Teléfono");
                gvIndividuos.RowHeaderColumn.Replace("CorreoElectronico", "Correo Electrónico");

                var ob = (from o in db.ActaVisitaSistematizacions
                          where o.IdActaVisita == idacta
                          select new { o.Observaciones }).Single();

                if (ob != null)
                    txtObservacion.Text = ob.Observaciones;
            }
            catch { }

        }

        protected void AlmacenarActorActa()
        {
            int idacta = Convert.ToInt32(Session["idactasis"]);

            ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();
            ESM.Model.AsociadoActaVisitaSistematizacion objAsociadosActaVisitasis = null;


            objAsociadosActaVisitasis = new Model.AsociadoActaVisitaSistematizacion
            {
                IdActaVisita = idacta,
                Cargo = txtCargo.Text,
                Telefono = txtTelefono.Text,
                Nombre = txtNombre.Text,
                CorreoElectronico = txtCorreo.Text,
                Institucion = txtinstitucion.Text
            };

            db.AsociadoActaVisitaSistematizacions.InsertOnSubmit(objAsociadosActaVisitasis);

            try
            {
                db.SubmitChanges();
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Error en BD.');</script>");
            }

            CargarActores(idacta);

        }

        protected bool Validar()
        {
            if (txtNombre.Text.Length == 0)
                return false;
            if (txtTelefono.Text.Length == 0)
                return false;
            if (txtCargo.Text.Length == 0)
                return false;
            else
                return true;
        }

        protected void btnAlmacenar_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                AlmacenarActorActa();

                #region Limpiar Controles

                txtNombre.Text = " ";
                txtDireccion.Text = " ";
                txtTelefono.Text = " ";
                txtCargo.Text = " ";
                txtCorreo.Text = " ";
                txtinstitucion.Text = " ";

                #endregion
            }
            else
                Response.Write("<script>alert('Los campos Nombre, Telefono, y Cargo, son obligatorios.');</script>");
        }

        protected void btnAlmacenarActa_Click(object sender, EventArgs e)
        {
            if (Session["idactasis"] != null)
            {
                int idacta = Convert.ToInt32(Session["idactasis"]);
                ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();

                var acsis = (from ase in db.ActaVisitaSistematizacions
                             where ase.IdActaVisita == idacta
                             select ase).Single();

                acsis.Observaciones = txtObservacion.Text;
                acsis.Medicione.FechaMedicion = DateTime.Now.AddHours(2);

                try
                {
                    db.SubmitChanges();
                    Response.Write("<script>alert('El proceso de almacenamiento finalizó correctamente.');</script>");
                }
                catch (Exception) { Response.Write("<script>alert('El proceso de almacenamiento finalizó sin éxito.');</script>"); }
            }
        }

        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            Filtro(txtFiltro.Text);
        }

        protected void gvResultados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                this.gvResultados.PageIndex = e.NewPageIndex;
                if (Session["idusuario"] != null)
                {
                    int idusuario = Convert.ToInt32(Session["idusuario"]);
                    string rol = _objCRoles.ObtenerRol(idusuario);
                    if (rol == "Administrador")
                    {
                        gvResultados.DataSourceID = "ldsies";
                        gvResultados.DataBind();

                    }
                    else if (rol == "Consultor" || rol == "MEN")
                    {
                        Session.Add("idcon", _objCRoles.IdConsultor);
                        gvResultados.DataSource = CEE.ObtenerEEs(_objCRoles.IdConsultor);
                        gvResultados.DataBind();

                    }
                }
            }
            catch (Exception) { }

        }

        protected void gvResultados_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow _objRow = gvResultados.SelectedRow;
            Label lblIdIe = (Label)_objRow.Cells[5].FindControl("IDIE");
            Session.Remove("idieaccsis");
            Session.Add("idieaccsis", lblIdIe.Text);
            lblCodIe.Text = _objRow.Cells[1].Text;
            lblIE.Text = _objRow.Cells[2].Text;
            lblMunicipio.Text = _objRow.Cells[3].Text;

            int idie = Convert.ToInt32(lblIdIe.Text);

            ESM.Model.Establecimiento_Educativo objee = CEE.ObtenerEE(idie);

            txtNombreEE.Text = objee.Nombre;
            txtNombreEE.Enabled = false;
            txtDireccion.Text = objee.Direccion;
            txtDireccion.Enabled = false;
            //txtCodigoDANE.Text = objee.CodigoDane;
            //txtCodigoDANE.Enabled = false;
            txtTelefonoEE.Text = objee.Telefono;
            txtTelefonoEE.Enabled = false;

            var mediciones = CSistematizacion.ObtenerMediciones(Convert.ToInt32(lblIdIe.Text));

            if (mediciones != null)
            {
                gvMediciones.DataSource = mediciones;
                gvMediciones.DataBind();

                #region Visualizacion de Controles

                modEESeleccion.Visible = false;
                ModMediciones.Visible = true;
                gvMediciones.Visible = true;
                modEESeleccion.Visible = false;

                for (int i = 0; i < gvMediciones.Rows.Count; i++)
                {
                    if (i == gvMediciones.Rows.Count - 1)
                        gvMediciones.Rows[i].Visible = true;
                    else
                        gvMediciones.Rows[i].Visible = false;
                }
                #endregion
            }
            else
            {
                #region Visualizacion de Controles
                modEESeleccion.Visible = false;
                ModMediciones.Visible = true;
                gvMediciones.Visible = true;
                modEESeleccion.Visible = false;
                #endregion

                Response.Write("<script>alert('No existen mediciones existentes para el EE.');</script>");
            }
        }
    }
}