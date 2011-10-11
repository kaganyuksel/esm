using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ESM
{
    public partial class ActaVisitaSE : System.Web.UI.Page
    {
        #region Propiedades Privadas y Publicas

        ESM.Objetos.CRoles _objCRoles = new Objetos.CRoles();

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
                            ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();
                            var se = from s in db.Secretaria_Educacions
                                     select s;
                            gvResultados.DataSource = se;
                            gvResultados.DataBind();
                            ObtenerTema(gvResultados);

                        }
                        else if (rol == "Consultor")
                        {
                            ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();
                            var se = from s in db.Secretaria_Educacions
                                     where s.IdConsultor == _objCRoles.IdConsultor
                                     select s;

                            gvResultados.DataSource = se;
                            gvResultados.DataBind();
                            ObtenerTema(gvResultados);

                        }
                    }
                }
            }
            else
                Response.Redirect("Login.aspx");
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

                var rFiltro = from i in db.Secretaria_Educacions
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

        

        protected void gvMediciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow objRow = gvMediciones.SelectedRow;

            int idmedicion = Convert.ToInt32(objRow.Cells[1].Text);
            Session.Remove("idmedAcc");
            Session.Add("idmedAcc", idmedicion);
            ModActaVisitaEE.Visible = true;
            int exist = ExistActa(idmedicion);
            int idactavisita = 0;

            documentos.Title = "Documentación";
            documentos.HRef = Request.Url.Scheme + "://" + Request.Url.Authority + "/ModuloDocumentosSE.aspx?idmedicion=" + idmedicion.ToString() + "&iframe=true&amp;width=100%&amp;height=100%";

            Session.Remove("idactaSE");

            if (exist == 0)
            {
                idactavisita = InsertarActaVisita();
                Session.Add("idactaSE", idactavisita);
            }
            else
            {
                Session.Add("idactaSE", exist);
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

                var ac = from a in db.ActaVisitaSEs
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
                int idse = Convert.ToInt32(Session["idseacc"]);

                ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();

                ESM.Model.ActaVisitaSE objActaVisitaSE = new Model.ActaVisitaSE
                {
                    IdUsuario = idusuario,
                    IdSE = idse,
                    IdMedicion = idmedicion,
                };

                db.ActaVisitaSEs.InsertOnSubmit(objActaVisitaSE);
                db.SubmitChanges();

                return objActaVisitaSE.IdActaVisita;
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

                var seacc = from s in db.AsociadoActaVisitaSEs
                            where s.IdActaVisita == idacta
                            select new { s.Nombre, s.Telefono, s.CorreoElectronico, s.Cargo };

                gvIndividuos.DataSource = seacc;
                gvIndividuos.DataBind();

                gvIndividuos.RowHeaderColumn.Replace("Telefono", "Teléfono");

                ObtenerTema(gvIndividuos);

                var ob = (from o in db.ActaVisitaSEs
                          where o.IdActaVisita == idacta
                          select new { o.Observaciones }).Single();

                if (ob != null)
                    txtObservacion.Text = ob.Observaciones;
            }
            catch { }

        }

        protected void AlmacenarActorActa()
        {
            int idacta = Convert.ToInt32(Session["idactaSE"]);

            ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();
            ESM.Model.AsociadoActaVisitaSE objAsociadosActaVisitaSE = null;


            objAsociadosActaVisitaSE = new Model.AsociadoActaVisitaSE
            {
                IdActaVisita = idacta,
                Cargo = txtCargo.Text,
                Telefono = txtTelefono.Text,
                Nombre = txtNombre.Text,
                CorreoElectronico = txtCorreo.Text
            };


            db.AsociadoActaVisitaSEs.InsertOnSubmit(objAsociadosActaVisitaSE);
            try
            {
                db.SubmitChanges();
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Error en BD.');</script>");
                //Alert.Show("");
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

                #endregion
            }
            else
                Response.Write("<script>alert('Los campos Nombre, Telefono, y Cargo, son obligatorios.');</script>");
        }

        protected void btnAlmacenarActa_Click(object sender, EventArgs e)
        {
            if (Session["idactaSE"] != null)
            {
                int idacta = Convert.ToInt32(Session["idactaSE"]);
                ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();

                var acse = (from ase in db.ActaVisitaSEs
                            where ase.IdActaVisita == idacta
                            select ase).Single();

                acse.Observaciones = txtObservacion.Text;

                try
                {
                    db.SubmitChanges();
                    Response.Write("<script>alert('Acta almacenada Correctamente.');</script>");
                }
                catch (Exception) { Response.Write("<script>alert('Error de almacenamiento.');</script>"); }
            }
        }

        protected void gvResultados_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow _objRow = gvResultados.SelectedRow;
            Label lblIdIe = (Label)_objRow.Cells[4].FindControl("IDIE");
            Session.Remove("idseacc");
            Session.Add("idseacc", lblIdIe.Text);
            lblNombrese.Text = _objRow.Cells[1].Text;
            lblTelefonose.Text = _objRow.Cells[3].Text;
            lblMunicipio.Text = _objRow.Cells[2].Text;

            int idse = Convert.ToInt32(lblIdIe.Text);

            ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();

            var seinfo = (from se in db.Secretaria_Educacions
                          where se.IdSecretaria == idse
                          select se).Single();

            txtNombreEE.Text = seinfo.Nombre;
            txtNombreEE.Enabled = false;
            txtDireccion.Text = seinfo.Direccion;
            txtDireccion.Enabled = false;
            txtTelefonoEE.Text = seinfo.Telefono;
            txtTelefonoEE.Enabled = false;


            var medse = (from lc in db.LecturaContextoSEs
                         where lc.IdSecretaria == idse
                         select new { lc.IdMedicion, Fecha = lc.Medicione.FechaMedicion }).Distinct();

            if (medse != null)
            {
                gvMediciones.DataSource = medse;
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

                Response.Write("<script>alert('No existen mediciones existentes para la SE.');</script>");
            }
        }

    }
}