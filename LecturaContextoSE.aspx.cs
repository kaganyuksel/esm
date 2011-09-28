using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESM.Model;

namespace ESM
{
    public partial class LecturaContexto : System.Web.UI.Page
    {
        #region Objetos, Propiedades Publicas y Privadas

        LecturasContexto.LecturaContextoSECRE _objLecturaContextoSECRE = new LecturasContexto.LecturaContextoSECRE();
        ESM.Objetos.CRoles _objCRoles = new Objetos.CRoles();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
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
                            gvSE.DataSource = _objLecturaContextoSECRE.ObtenerSE();
                            gvSE.DataBind();
                            ObtenerTema(gvSE);
                        }
                        else if (rol == "Consultor")
                        {
                            ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();
                            var se = from s in db.Secretaria_Educacion
                                     where s.IdConsultor == _objCRoles.IdConsultor
                                     select s;

                            gvSE.DataSource = se;
                            gvSE.DataBind();
                            ObtenerTema(gvSE);

                        }
                    }
                    else
                        Response.Redirect("/Login.aspx");
                }
            }
            else
                Response.Redirect("Login.aspx");

        }

        protected void CargarSECRE(int idsecre)
        {

            Secretaria_Educacion objSecretaria_Educacion = _objLecturaContextoSECRE.RecuperarSE(idsecre);
            if (objSecretaria_Educacion != null)
            {
                txtNombreSE.Text = objSecretaria_Educacion.Nombre;
                txtNombreSE.ReadOnly = true;
                txtDireccionSE.Text = objSecretaria_Educacion.Direccion;
                txtDireccionSE.ReadOnly = true;
                txtTelefonoSE.Text = objSecretaria_Educacion.Telefono;
                txtTelefonoSE.ReadOnly = true;
                txtNombreSecre.Text = objSecretaria_Educacion.Nombre_Secretario_Educacion;
                txtNombreSecre.ReadOnly = true;
                txtTelefonoSecre.Text = objSecretaria_Educacion.Telefonos_Secretario_Educacion;
                txtCorreoSecre.Text = objSecretaria_Educacion.Correo_Electronico;
                if (objSecretaria_Educacion.Departamental != null)
                    rbtnDepartamentalSE.Checked = (bool)objSecretaria_Educacion.Departamental;
                if (objSecretaria_Educacion.Municipal != null)
                    rbtnMunicipalSE.Checked = (bool)objSecretaria_Educacion.Municipal;
            }
            else
                Response.Write("No puede ser cargada la información correspondiente para la Secretaria de Educación.");
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

        protected bool Almacenar()
        {
            try
            {
                _objLecturaContextoSECRE.IdSec = Convert.ToInt32(Session["idse"]);
                _objLecturaContextoSECRE.TelefonoSe = txtTelefonoSE.Text;
                _objLecturaContextoSECRE.TelefonoSecretrio = txtTelefonoSecre.Text;
                _objLecturaContextoSECRE.Direccion = txtDireccionSE.Text;
                _objLecturaContextoSECRE.CorreoElectronico = txtCorreoSecre.Text;

                if (rbtnDepartamentalSE.Checked)
                    _objLecturaContextoSECRE.Departamental = true;
                else
                    _objLecturaContextoSECRE.Departamental = false;

                if (rbtnMunicipalSE.Checked)
                    _objLecturaContextoSECRE.Municipal = true;
                else
                    _objLecturaContextoSECRE.Municipal = false;

                _objLecturaContextoSECRE.__2_1 = txt21.Text;
                if (rbtn22Si.Checked)
                    _objLecturaContextoSECRE.__2_2 = true;

                else if (rbtn22No.Checked == true)
                    _objLecturaContextoSECRE.__2_2 = false;

                _objLecturaContextoSECRE.__2_2_1 = txt221.Text;
                _objLecturaContextoSECRE.__2_2_2 = txt222.Text;

                #region 223

                if (cblist223.Items[0].Selected)
                    _objLecturaContextoSECRE.__2_2_3EE = true;
                else
                    _objLecturaContextoSECRE.__2_2_3EE = false;
                if (cblist223.Items[1].Selected)
                    _objLecturaContextoSECRE.__2_2_3EST = true;
                else
                    _objLecturaContextoSECRE.__2_2_3EST = false;

                if (cblist223.Items[2].Selected)
                    _objLecturaContextoSECRE.__2_2_3EDU = true;
                else
                    _objLecturaContextoSECRE.__2_2_3EDU = false;

                if (cblist223.Items[3].Selected)
                    _objLecturaContextoSECRE.__2_2_3DIR = true;
                else
                    _objLecturaContextoSECRE.__2_2_3DIR = false;

                if (cblist223.Items[4].Selected)
                    _objLecturaContextoSECRE.__2_2_3PAD = true;
                else
                    _objLecturaContextoSECRE.__2_2_3PAD = false;

                #endregion

                _objLecturaContextoSECRE.__2_2_3Otro = txt223Orto.Text;

                _objLecturaContextoSECRE.__2_2_4 = txt224.Text;
                _objLecturaContextoSECRE.__2_2_5 = txt225.Text;

                if (rbtn23Si.Checked)
                    _objLecturaContextoSECRE.__2_3 = true;
                else if (rbtn23No.Checked)
                    _objLecturaContextoSECRE.__2_3 = false;

                _objLecturaContextoSECRE.__2_3_1 = txt231.Text;
                _objLecturaContextoSECRE.__3_1 = txt31.Text;
                _objLecturaContextoSECRE.__3_2 = txt32.Text;
                _objLecturaContextoSECRE.__3_3 = txt33.Text;
                _objLecturaContextoSECRE.__3_4 = txt34.Text;
                _objLecturaContextoSECRE.__3_5 = txt35.Text;
                _objLecturaContextoSECRE.__3_6 = txt36.Text;

                if (rbtn41Si.Checked)
                    _objLecturaContextoSECRE.__4_1 = true;
                else if (rbtn41No.Checked)
                    _objLecturaContextoSECRE.__4_1 = false;

                _objLecturaContextoSECRE.__4_1_1 = txt411.Text;

                #region 5.1 Departamental/Municipal
                if (cblist51DedMun.Items[0].Selected)
                    _objLecturaContextoSECRE.__5_1_RADDEP = true;
                else
                    _objLecturaContextoSECRE.__5_1_RADDEP = false;

                if (cblist51DedMun.Items[1].Selected)
                    _objLecturaContextoSECRE.__5_1_PRENDEP = true;
                else
                    _objLecturaContextoSECRE.__5_1_PRENDEP = false;

                if (cblist51DedMun.Items[2].Selected)
                    _objLecturaContextoSECRE.__5_1_TELDEP = true;
                else
                    _objLecturaContextoSECRE.__5_1_TELDEP = false;

                if (cblist51DedMun.Items[0].Selected)
                    _objLecturaContextoSECRE.__5_1_INTDEP = true;
                else
                    _objLecturaContextoSECRE.__5_1_INTDEP = false;
                #endregion

                #region Local

                if (cblist51DedMun.Items[0].Selected)
                    _objLecturaContextoSECRE.__5_1_RADMUN = true;
                else
                    _objLecturaContextoSECRE.__5_1_RADMUN = false;

                if (cblist51DedMun.Items[1].Selected)
                    _objLecturaContextoSECRE.__5_1_PRENMUN = true;
                else
                    _objLecturaContextoSECRE.__5_1_PRENMUN = false;

                if (cblist51DedMun.Items[2].Selected)
                    _objLecturaContextoSECRE.__5_1_TELMUN = true;
                else
                    _objLecturaContextoSECRE.__5_1_TELMUN = false;

                if (cblist51DedMun.Items[0].Selected)
                    _objLecturaContextoSECRE.__5_1_INTMUN = true;
                else
                    _objLecturaContextoSECRE.__5_1_INTMUN = false;

                #endregion

                _objLecturaContextoSECRE.__5_2 = txt52.Text;
                if (rbtn118Si.Checked)
                    _objLecturaContextoSECRE.__1_18 = true;
                else
                    _objLecturaContextoSECRE.__1_18 = false;

                if (rbtn118No.Checked)
                    _objLecturaContextoSECRE.__1_18 = true;
                else
                    _objLecturaContextoSECRE.__1_18 = false;

                _objLecturaContextoSECRE.__1_19 = txt119.Text;
                _objLecturaContextoSECRE.Observaciones = txtObservaciones.Text;

                int idmedicion = Convert.ToInt32(Session["idmedicionLC"]);
                if (Session["idlectura"] == null)
                {
                    if (_objLecturaContextoSECRE.Almacenar(idmedicion))
                    {
                        Session.Add("idlectura", _objLecturaContextoSECRE.IdLectura);
                        Response.Write("<script>alert('El proceso de almacenamiento para Lectura de Contexto Finalizo satisfactoriamente.');</script>");
                    }
                    else
                        Response.Write("<script>alert('El proceso de almacenamiento para Lectura de Contexto Finalizo sin exito.');</script>");
                }
                else if (Session["idlectura"] != null)
                {
                    if (_objLecturaContextoSECRE.Actualizar(Convert.ToInt32(Session["idlectura"])))
                    {
                        Response.Write("<script>alert('El proceso de actualización para Lectura de Contexto Finalizo satisfactoriamente.');</script>");
                    }
                    else
                        Response.Write("<script>alert('El proceso de actualización para Lectura de Contexto Finalizo sin exito.');</script>");
                }
                return true;
            }
            catch (Exception) { return false; }
        }

        protected void btnAlmacenar_Click(object sender, EventArgs e)
        {
            //if (validar())
            if (!Almacenar())
                Response.Write("<script>alert('Para realizar el proceso de almacenamiento corrija las advertencias.');</script>");
        }

        protected void CargarMediciones()
        {
            int idse = Convert.ToInt32(Session["idse"]);
            gvMediciones.DataSource = _objLecturaContextoSECRE.CargarMediciones(idse);
            gvMediciones.DataBind();
            ObtenerTema(gvMediciones);
        }

        protected void gvMediciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow objRow = gvMediciones.SelectedRow;
            Session.Add("idmedicionLC", objRow.Cells[1].Text);
            btnAlmacenar.Visible = false;
            CargarSE();
            CargarLecturaContexto();
            lecturaContextoTable.Visible = true;
            btnRegistrar.Visible = false;
            ModMediciones.Visible = false;
            LC.Visible = true;
            btnAlmacenar.Visible = true;

        }

        protected void CargarSE()
        {
            gvSE.DataSource = _objLecturaContextoSECRE.ObtenerSE();
            gvSE.DataBind();
            ObtenerTema(gvSE);
        }

        protected void gvSE_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow objRow = gvSE.SelectedRow;
            int idsec = Convert.ToInt32(objRow.Cells[1].Text);
            Session.Add("idse", objRow.Cells[1].Text);
            CargarSECRE(idsec);
            CargarMediciones();
            gvMediciones.Visible = true;
            btnRegistrar.Visible = true;
            titulomediciones.Visible = true;
            ModMediciones.Visible = true;
            ModseleccionSE.Visible = false;

            if (gvMediciones.Rows.Count != 0)
                btnRegistrar.Visible = false;
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            int idmedicion = _objLecturaContextoSECRE.RegistrarMedicion();
            if (idmedicion != 0)
            {
                Session.Remove("idlectura");
                lecturaContextoTable.Visible = true;
                Session.Add("idmedicionLC", idmedicion);
                lecturaContextoTable.Visible = true;
                btnRegistrar.Visible = false;
                ModMediciones.Visible = false;
                LC.Visible = true;
            }

        }

        protected void rbtn22Si_CheckedChanged(object sender, EventArgs e)
        {
            txt221.Enabled = true;
            txt222.Enabled = true;
        }

        protected void rbtn22No_CheckedChanged(object sender, EventArgs e)
        {
            txt221.Enabled = false;
            txt222.Enabled = false;
            txt221.Text = " ";
            txt222.Text = " ";
        }

        protected void rbtn23No_CheckedChanged(object sender, EventArgs e)
        {
            txt231.Enabled = false;
            txt231.Text = " ";
        }

        protected void rbtn23Si_CheckedChanged(object sender, EventArgs e)
        {
            txt231.Enabled = true;
        }

        protected void rbtn41No_CheckedChanged(object sender, EventArgs e)
        {
            txt411.Enabled = false;
            txt411.Text = " ";
        }

        protected void rbtn41Si_CheckedChanged(object sender, EventArgs e)
        {
            txt411.Enabled = true;
        }

        protected void rbtn118No_CheckedChanged(object sender, EventArgs e)
        {
            txt119.Enabled = false;
            txt119.Text = "";
        }

        protected void rbtn118Si_CheckedChanged(object sender, EventArgs e)
        {
            txt119.Enabled = true;
        }

        protected bool validar()
        {
            if (cblist51DedMun.Items[0].Selected && cblist51Local.Items[0].Selected)
            {
                Response.Write("<script>alert('Para el item 5.1 no puede estar seleccionada la misma casilla en las dos listas.');</script>");
                return false;
            }
            else if (cblist51DedMun.Items[1].Selected && cblist51Local.Items[1].Selected)
            {
                Response.Write("<script>alert('Para el item 5.1 no puede estar seleccionada la misma casilla en las dos listas.');</script>");
                return false;
            }
            else if (cblist51DedMun.Items[2].Selected && cblist51Local.Items[2].Selected)
            {
                Response.Write("<script>alert('Para el item 5.1 no puede estar seleccionada la misma casilla en las dos listas.');</script>");
                return false;
            }
            else if (cblist51DedMun.Items[3].Selected && cblist51Local.Items[3].Selected)
            {
                Response.Write("<script>alert('Para el item 5.1 no puede estar seleccionada la misma casilla en las dos listas.');</script>");
                return false;
            }
            else
                return true;

        }

        protected void CargarLecturaContexto()
        {
            int idse = Convert.ToInt32(Session["idse"]);
            int idmedicion = Convert.ToInt32(Session["idmedicionLC"]);
            List<LecturaContextoSE> objList = _objLecturaContextoSECRE.CargarLCSE(idmedicion, idse);
            if (objList[0] != null)
            {
                #region Cargar Controles
                txt21.Text = objList[0]._2_1_;

                if ((bool)objList[0]._2_2_)
                    rbtn22Si.Checked = true;
                else if (!(bool)objList[0]._2_2_)
                    rbtn22No.Checked = true;

                txt221.Text = objList[0]._2_2_1_;
                txt222.Text = objList[0]._2_2_2_;

                if ((bool)objList[0]._2_2_3_DIR_)
                    cblist223.Items[3].Selected = true;
                if ((bool)objList[0]._2_2_3_EDU)
                    cblist223.Items[2].Selected = true;
                if ((bool)objList[0]._2_2_3_EE_)
                    cblist223.Items[0].Selected = true;
                if ((bool)objList[0]._2_2_3_EST_)
                    cblist223.Items[1].Selected = true;
                if ((bool)objList[0]._2_2_3_PAD_)
                    cblist223.Items[4].Selected = true;

                txt223Orto.Text = objList[0]._2_2_3_OTR_;
                txt224.Text = objList[0]._2_2_4_;
                txt225.Text = objList[0]._2_2_5_;

                if ((bool)objList[0]._2_3_)
                    rbtn23Si.Checked = true;
                else
                    rbtn23No.Checked = true;

                txt231.Text = objList[0]._2_3_1_;
                txt31.Text = objList[0]._3_1_;
                txt32.Text = objList[0]._3_2_;
                txt33.Text = objList[0]._3_3_;
                txt34.Text = objList[0]._3_4_;
                txt35.Text = objList[0]._3_5_;
                txt36.Text = objList[0]._3_6_;

                if ((bool)objList[0]._4_1_)
                    rbtn41Si.Checked = true;
                else
                    rbtn41No.Checked = true;

                txt411.Text = objList[0]._4_1_1_;

                if ((bool)objList[0]._5_1_INT)
                    cblist51DedMun.Items[3].Selected = true;
                if ((bool)objList[0]._5_1_INTM)
                    cblist51Local.Items[3].Selected = true;
                if ((bool)objList[0]._5_1_PREND)
                    cblist51DedMun.Items[1].Selected = true;
                if ((bool)objList[0]._5_1_PRENM)
                    cblist51Local.Items[2].Selected = true;
                if ((bool)objList[0]._5_1_RADD_)
                    cblist51DedMun.Items[0].Selected = true;
                if ((bool)objList[0]._5_1_RADM)
                    cblist51Local.Items[0].Selected = true;
                if ((bool)objList[0]._5_1_TELD)
                    cblist51DedMun.Items[2].Selected = true;
                if ((bool)objList[0]._5_1_TELM)
                    cblist51Local.Items[2].Selected = true;

                txt52.Text = objList[0]._5_2_;

                if ((bool)objList[0]._1_1_8_)
                    rbtn118Si.Checked = true;
                else if (!(bool)objList[0]._1_1_8_)
                    rbtn118No.Checked = true;

                txt119.Text = objList[0]._1_1_9_;
                txtObservaciones.Text = objList[0].Observaciones;

                btnAlmacenar.Visible = false;
                #endregion
                lecturaContextoTable.Visible = true;

            }
            else
                Response.Write("<script>alert('No se puede cargar la informacion para lectura de contexto.');</script>");



        }
    }
}