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
    public partial class ActaVisitaEE : System.Web.UI.Page
    {
        #region Propiedades Publicas y Privadas

        protected CRoles _objCRoles = new CRoles();
        protected CEvaluacion _objCEvaluacion = new CEvaluacion();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                if (!Page.IsPostBack)
                {
                    modEESeleccion.Visible = true;
                    if (Session["idusuario"] != null)
                    {
                        int idusuario = Convert.ToInt32(Session["idusuario"]);
                        string rol = _objCRoles.ObtenerRol(idusuario);
                        if (rol == "Administrador")
                        {
                            gvResultados.DataSourceID = "ldsies";
                            gvResultados.DataBind();
                            ObtenerTema(gvResultados);

                        }
                        else if (rol == "Consultor")
                        {
                            Session.Add("idcon", _objCRoles.IdConsultor);
                            gvResultados.DataSource = CEE.ObtenerEEs(_objCRoles.IdConsultor);
                            gvResultados.DataBind();
                            ObtenerTema(gvResultados);

                        }
                        cboActores.DataSourceID = "lqdsActores";
                        cboActores.DataBind();
                    }
                }
            }
            else
                Response.Redirect("Login.aspx");
        }

        protected void Unnamed2_Click(object sender, EventArgs e)
        {
            Filtro(txtFiltro.Text);
        }

        protected bool Filtro(string texto)
        {
            try
            {
                /*Instancio*/
                Model.ESMBDDataContext db = new Model.ESMBDDataContext();

                var rFiltro = from i in db.Establecimiento_Educativos
                              where i.Nombre.Contains(texto)
                              select i;

                gvResultados.DataSourceID = null;
                gvResultados.DataSource = rFiltro;
                gvResultados.DataBind();

                ObtenerTema(gvResultados);
                return true;
            }
            catch (Exception) { return false; }

        }

        protected void ObtenerTema(GridView objGridView)
        {
            if (objGridView.Rows.Count != 0)
            {
                objGridView.HeaderStyle.CssClass = "trheader";

                int color = 0;
                for (int i = 0; i < objGridView.Rows.Count; i++)
                {
                    if (color == 0)
                    {
                        objGridView.Rows[i].CssClass = "trgris";
                        color = 1;
                    }
                    else if (color == 1)
                    {
                        objGridView.Rows[i].CssClass = "trblanca";
                        color = 0;
                    }
                }
            }

        }

        protected void gvResultados_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow _objRow = gvResultados.SelectedRow;
            Label lblIdIe = (Label)_objRow.Cells[5].FindControl("IDIE");
            Session.Remove("idieacc");
            Session.Add("idieacc", lblIdIe.Text);
            lblCodIe.Text = _objRow.Cells[1].Text;
            lblIE.Text = _objRow.Cells[2].Text;
            lblMunicipio.Text = _objRow.Cells[3].Text;

            int idie = Convert.ToInt32(lblIdIe.Text);

            ESM.Model.Establecimiento_Educativo objee = CEE.ObtenerEE(idie);

            txtNombreEE.Text = objee.Nombre;
            txtNombreEE.Enabled = false;
            txtDireccion.Text = objee.Direccion;
            txtDireccion.Enabled = false;
            txtCodigoDANE.Text = objee.CodigoDane;
            txtCodigoDANE.Enabled = false;
            txtTelefonoEE.Text = objee.Telefono;
            txtTelefonoEE.Enabled = false;

            var mediciones = _objCEvaluacion.MedicionesIE(Convert.ToInt32(lblIdIe.Text));

            if (mediciones != null)
            {
                gvMediciones.DataSource = mediciones;
                gvMediciones.DataBind();
                ObtenerTema(gvMediciones);

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

        protected void CargarActores(int idacta)
        {
            ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();
            var dir = from d in db.AsociadosActaVisitaEEs
                      where d.IdActaVisita == idacta && d.IdActor == 6
                      select new { d.Nombre, d.Telefono, d.CorreoElectronico, d.Cargo };

            gvDirectivos.DataSource = dir;
            gvDirectivos.DataBind();

            ObtenerTema(gvDirectivos);

            var est = from e in db.AsociadosActaVisitaEEs
                      where e.IdActaVisita == idacta && e.IdActor == 1
                      select new { e.Nombre, e.Telefono, e.CorreoElectronico, e.Grado };

            gvEstudiantes.DataSource = est;
            gvEstudiantes.DataBind();

            ObtenerTema(gvEstudiantes);

            var pad = from p in db.AsociadosActaVisitaEEs
                      where p.IdActaVisita == idacta && p.IdActor == 4
                      select new { p.Nombre, p.Telefono, p.CorreoElectronico, p.GradoHijos, p.NivelesEducativo.NivelEducativo };

            gvPadresFamilia.DataSource = pad;
            gvPadresFamilia.DataBind();

            ObtenerTema(gvPadresFamilia);

            var edu = from ed in db.AsociadosActaVisitaEEs
                      where ed.IdActaVisita == idacta && ed.IdActor == 3
                      select new { ed.Nombre, ed.Telefono, ed.CorreoElectronico, ed.AreasEnseñansa, ed.GradosEnseñansa };

            gvEducadores.DataSource = edu;
            gvEducadores.DataBind();

            ObtenerTema(gvEducadores);

            var ob = (from o in db.ActaVisitaEEs
                      where o.IdActaVisita == idacta
                      select new { o.Observaciones }).Single();

            if (ob != null)
                txtObservacion.Text = ob.Observaciones;

        }

        protected void gvMediciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow objRow = gvMediciones.SelectedRow;

            int idmedicion = Convert.ToInt32(objRow.Cells[1].Text);
            Session.Add("idmedAcc", idmedicion);
            ModActaVisitaEE.Visible = true;
            int exist = ExistActa(idmedicion);
            int idactavisita = 0;

            documentos.Title = "Documentación";
            documentos.HRef = Request.Url.Scheme + "://" + Request.Url.Authority + "/ModuloDocumentos.aspx?idmedicion=" + idmedicion.ToString() + "&iframe=true&amp;width=100%&amp;height=100%";

            Session.Remove("idactaEE");

            if (exist == 0)
            {
                idactavisita = InsertarActaVisita();
                Session.Add("idactaEE", idactavisita);
            }
            else
            {
                Session.Add("idactaEE", exist);
                CargarActores(exist);
            }

            txtFechaInicial.Text = objRow.Cells[3].Text;
            txtFechaInicial.Enabled = false;

            txtFechaFinal.Text = DateTime.Now.ToString();
            txtFechaFinal.Enabled = false;

            cboActores.SelectedValue = "7";
        }

        protected int ExistActa(int medicion)
        {
            try
            {
                ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();

                var ac = from a in db.ActaVisitaEEs
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
                int idmedicion = Convert.ToInt32(Session["idmedAcc"]);
                int idusuario = Convert.ToInt32(Session["idusuario"]);
                int idie = Convert.ToInt32(Session["idieacc"]);

                ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();

                ESM.Model.ActaVisitaEE objActaVisitaEE = new Model.ActaVisitaEE
                {
                    IdUsuario = idusuario,
                    IdEE = idie,
                    IdMedicion = idmedicion,
                };

                db.ActaVisitaEEs.InsertOnSubmit(objActaVisitaEE);
                db.SubmitChanges();

                return objActaVisitaEE.IdActaVisita;
            }
            catch (Exception)
            {
                return 0;
            }

        }

        protected void AlmacenarActorActa()
        {
            int idacta = Convert.ToInt32(Session["idactaEE"]);

            ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();
            ESM.Model.AsociadosActaVisitaEE objAsociadosActaVisitaEE = null;

            if (cboActores.SelectedItem.Value == "1")
            {
                objAsociadosActaVisitaEE = new Model.AsociadosActaVisitaEE
                {
                    IdActor = Convert.ToInt32(cboActores.SelectedItem.Value),
                    IdActaVisita = idacta,
                    Grado = txtGrado.Text,
                    Telefono = txtTelefono.Text,
                    Nombre = txtNombre.Text,
                    CorreoElectronico = txtCorreo.Text
                };
            }
            else if (cboActores.SelectedItem.Value == "3")
            {
                objAsociadosActaVisitaEE = new Model.AsociadosActaVisitaEE
                {
                    IdActor = Convert.ToInt32(cboActores.SelectedItem.Value),
                    IdActaVisita = idacta,
                    AreasEnseñansa = txtGensenanza.Text,
                    GradosEnseñansa = txtGradosEnsenanza.Text,
                    Telefono = txtTelefono.Text,
                    Nombre = txtNombre.Text,
                    CorreoElectronico = txtCorreo.Text
                };
            }
            else if (cboActores.SelectedItem.Value == "4")
            {
                objAsociadosActaVisitaEE = new Model.AsociadosActaVisitaEE
                {
                    IdActor = Convert.ToInt32(cboActores.SelectedItem.Value),
                    IdActaVisita = idacta,
                    GradoHijos = txtGradoHijos.Text,
                    Telefono = txtTelefono.Text,
                    Nombre = txtNombre.Text,
                    CorreoElectronico = txtCorreo.Text,
                    IdNivelEducativo = Convert.ToInt32(cboNivelesEducativos.SelectedValue)
                };
            }
            else if (cboActores.SelectedItem.Value == "6")
            {
                objAsociadosActaVisitaEE = new Model.AsociadosActaVisitaEE
                {
                    IdActor = Convert.ToInt32(cboActores.SelectedItem.Value),
                    IdActaVisita = idacta,
                    Cargo = txtCargo.Text,
                    Telefono = txtTelefono.Text,
                    Nombre = txtNombre.Text,
                    CorreoElectronico = txtCorreo.Text
                };
            }

            db.AsociadosActaVisitaEEs.InsertOnSubmit(objAsociadosActaVisitaEE);
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

        protected void cboActores_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idactor = Convert.ToInt32(cboActores.SelectedItem.Value);

            txtNombre.Enabled = true;
            txtTelefono.Enabled = true;
            txtCorreo.Enabled = true;

            switch (idactor)
            {
                case 1:
                    lblGrado.Visible = true;
                    txtGrado.Visible = true;
                    lblCargo.Visible = false;
                    txtCargo.Visible = false;
                    lblNivelEducativo.Visible = false;
                    cboNivelesEducativos.Visible = false;
                    lblAEnsenansa.Visible = false;
                    txtGensenanza.Visible = false;
                    lblgh.Visible = false;
                    txtGradoHijos.Visible = false;
                    lblGradosEnsenanza.Visible = false;
                    txtGradosEnsenanza.Visible = false;
                    break;
                case 3:
                    lblGrado.Visible = false;
                    txtGrado.Visible = false;
                    lblCargo.Visible = false;
                    txtCargo.Visible = false;
                    lblNivelEducativo.Visible = false;
                    cboNivelesEducativos.Visible = false;
                    lblAEnsenansa.Visible = true;
                    txtGensenanza.Visible = true;
                    lblGradosEnsenanza.Visible = true;
                    txtGradosEnsenanza.Visible = true;
                    lblgh.Visible = false;
                    txtGradoHijos.Visible = false;
                    break;
                case 4:
                    lblGrado.Visible = false;
                    txtGrado.Visible = false;
                    lblCargo.Visible = false;
                    txtCargo.Visible = false;
                    lblNivelEducativo.Visible = true;
                    cboNivelesEducativos.Visible = true;
                    lblAEnsenansa.Visible = false;
                    txtGensenanza.Visible = false;
                    lblgh.Visible = true;
                    txtGradoHijos.Visible = true;
                    lblGradosEnsenanza.Visible = false;
                    txtGradosEnsenanza.Visible = false;
                    break;
                case 6:
                    lblGrado.Visible = false;
                    txtGrado.Visible = false;
                    lblCargo.Visible = true;
                    txtCargo.Visible = true;
                    lblNivelEducativo.Visible = false;
                    cboNivelesEducativos.Visible = false;
                    lblAEnsenansa.Visible = false;
                    txtGensenanza.Visible = false;
                    lblgh.Visible = false;
                    txtGradoHijos.Visible = false;
                    lblGradosEnsenanza.Visible = false;
                    txtGradosEnsenanza.Visible = false;
                    break;

            }
        }

        protected bool Validar()
        {
            if (txtTelefono.Text.Length == 0)
                return false;
            if (txtNombre.Text.Length == 0)
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
                txtTelefono.Text = " ";
                txtDireccion.Text = " ";
                txtGensenanza.Text = " ";
                txtGrado.Text = " ";
                txtGradoHijos.Text = " ";
                txtGradosEnsenanza.Text = " ";
                txtCorreo.Text = " ";

                cboNivelesEducativos.DataBind();

                #endregion
            }
            else
                Response.Write("<script>alert('los campos Nombre y Telefono son obligatorios para el actor seleccionado.');</script>");
        }

        protected void btnAlmacenarActa_Click(object sender, EventArgs e)
        {
            if (Session["idactaEE"] != null)
            {
                int idacta = Convert.ToInt32(Session["idactaEE"]);
                ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();

                var acee = (from aee in db.ActaVisitaEEs
                            where aee.IdActaVisita == idacta
                            select aee).Single();

                acee.Observaciones = txtObservacion.Text;

                try
                {
                    db.SubmitChanges();
                    Response.Write("<script>alert('Acta almacenada Correctamente.');</script>");
                }
                catch (Exception) { Response.Write("<script>alert('Error de almacenamiento.');</script>"); }

            }
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
                        ObtenerTema(gvResultados);

                    }
                    else if (rol == "Consultor")
                    {
                        Session.Add("idcon", _objCRoles.IdConsultor);
                        gvResultados.DataSource = CEE.ObtenerEEs(_objCRoles.IdConsultor);
                        gvResultados.DataBind();
                        ObtenerTema(gvResultados);

                    }
                }
            }
            catch (Exception) { }

        }
    }
}