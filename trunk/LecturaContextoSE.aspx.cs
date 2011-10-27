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
                            gvSE.DataSource = _objLecturaContextoSECRE.ObtenerSE();
                            gvSE.DataBind();
                            ObtenerTema(gvSE);
                        }
                        else if (rol == "Consultor" || rol == "MEN")
                        {
                            ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();
                            var se = from s in db.Secretaria_Educacions
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

                if (chxEE.Checked)
                    _objLecturaContextoSECRE.__2_2_3EE = true;
                else
                    _objLecturaContextoSECRE.__2_2_3EE = false;
                if (chxest.Checked)
                    _objLecturaContextoSECRE.__2_2_3EST = true;
                else
                    _objLecturaContextoSECRE.__2_2_3EST = false;

                if (chxEdu.Checked)
                    _objLecturaContextoSECRE.__2_2_3EDU = true;
                else
                    _objLecturaContextoSECRE.__2_2_3EDU = false;

                if (chxdirectivos.Checked)
                    _objLecturaContextoSECRE.__2_2_3DIR = true;
                else
                    _objLecturaContextoSECRE.__2_2_3DIR = false;

                if (chxpad.Checked)
                    _objLecturaContextoSECRE.__2_2_3PAD = true;
                else
                    _objLecturaContextoSECRE.__2_2_3PAD = false;

                _objLecturaContextoSECRE.__2_2_3EE_Cant = Convert.ToInt32(txtcantee.Text);
                _objLecturaContextoSECRE.__2_2_3EST_Cant = Convert.ToInt32(txtcantest.Text);
                _objLecturaContextoSECRE.__2_2_3EDU_Cant = Convert.ToInt32(txtcantedu.Text);
                _objLecturaContextoSECRE.__2_2_3DIR_Cant = Convert.ToInt32(txtcantdir.Text);
                _objLecturaContextoSECRE.__2_2_3PAD_Cant = Convert.ToInt32(txtcantpad.Text);

                _objLecturaContextoSECRE.__2_2_3Otro_Cual_1 = txtotrocual1.Text;
                _objLecturaContextoSECRE.__2_2_3Otro_Cual_2 = txtotrocual2.Text;
                _objLecturaContextoSECRE.__2_2_3Otro_Cual_3 = txtotrocual3.Text;
                _objLecturaContextoSECRE.__2_2_3Otro_Cual_4 = txtotrocual4.Text;
                _objLecturaContextoSECRE.__2_2_3Otro_Cual_5 = txtotrocual5.Text;

                _objLecturaContextoSECRE.__2_2_3Otro_Cual_1_Cant = Convert.ToInt32(Cantidadotro1.Text);
                _objLecturaContextoSECRE.__2_2_3Otro_Cual_2_Cant = Convert.ToInt32(Cantidadotro2.Text);
                _objLecturaContextoSECRE.__2_2_3Otro_Cual_3_Cant = Convert.ToInt32(Cantidadotro3.Text);
                _objLecturaContextoSECRE.__2_2_3Otro_Cual_4_Cant = Convert.ToInt32(Cantidadotro4.Text);
                _objLecturaContextoSECRE.__2_2_3Otro_Cual_5_Cant = Convert.ToInt32(Cantidadotro5.Text);

                #endregion

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

                if (cblist51DedMun.Items[3].Selected)
                    _objLecturaContextoSECRE.__5_1_INTDEP = true;
                else
                    _objLecturaContextoSECRE.__5_1_INTDEP = false;
                #endregion

                #region Local

                if (cblist51Local.Items[0].Selected)
                    _objLecturaContextoSECRE.__5_1_RADMUN = true;
                else
                    _objLecturaContextoSECRE.__5_1_RADMUN = false;

                if (cblist51Local.Items[1].Selected)
                    _objLecturaContextoSECRE.__5_1_PRENMUN = true;
                else
                    _objLecturaContextoSECRE.__5_1_PRENMUN = false;

                if (cblist51Local.Items[2].Selected)
                    _objLecturaContextoSECRE.__5_1_TELMUN = true;
                else
                    _objLecturaContextoSECRE.__5_1_TELMUN = false;

                if (cblist51Local.Items[3].Selected)
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
                    var lcm = from lc in new ESM.Model.ESMBDDataContext().LecturaContextoSEs
                              where lc.IdMedicion == idmedicion
                              select lc;

                    //if (lcm.Count() != 0)
                    //{
                    if (_objLecturaContextoSECRE.Almacenar(idmedicion))
                    {
                        Session.Add("idlectura", _objLecturaContextoSECRE.IdLectura);
                        Response.Write("<script>alert('El proceso de almacenamiento para Lectura de Contexto Finalizo satisfactoriamente.');</script>");
                    }
                    else
                        Response.Write("<script>alert('El proceso de almacenamiento para Lectura de Contexto Finalizo sin exito.');</script>");
                    //}
                    //else
                    //    Response.Write("<script>alert('Ya existe un instrumento de lectura de contexto para la secretaría de educación seleccionada.');</script>");

                }
                else if (Session["idlectura"] != null)
                {
                    if (_objLecturaContextoSECRE.Actualizar(Convert.ToInt32(Session["idlectura"])))
                    {
                        Response.Write("<script>alert('El proceso de actualización para lectura de contexto finalizó satisfactoriamente.');</script>");
                    }
                    else
                        Response.Write("<script>alert('El proceso de actualización para lectura de contexto finalizó sin exito.');</script>");
                }
                return true;
            }
            catch (Exception) { return false; }
        }

        protected void btnAlmacenar_Click(object sender, EventArgs e)
        {
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
            ESM.Model.LecturaContextoSE objList = _objLecturaContextoSECRE.CargarLCSE(idmedicion, idse);
            if (objList != null)
            {
                Session.Add("idlectura", objList.IdLecturaContexto);
                #region Cargar Controles
                txt21.Text = objList._2_1_;

                if ((bool)objList._2_2_)
                    rbtn22Si.Checked = true;
                else if (!(bool)objList._2_2_)
                    rbtn22No.Checked = true;

                txt221.Text = objList._2_2_1_;
                txt222.Text = objList._2_2_2_;

                if (txt221.Text.Length != 0)
                    txt221.Enabled = true;

                if (txt222.Text.Length != 0)
                    txt222.Enabled = true;



                if ((bool)objList._2_2_3_DIR_)
                {
                    chxdirectivos.Checked = true;
                    txtcantdir.Enabled = true;
                }
                if ((bool)objList._2_2_3_EDU)
                {
                    chxEdu.Checked = true;
                    txtcantedu.Enabled = true;
                }
                if ((bool)objList._2_2_3_EE_)
                {
                    chxEE.Checked = true;
                    txtcantee.Enabled = true;
                }
                if ((bool)objList._2_2_3_EST_)
                {
                    chxest.Checked = true;
                    txtcantest.Enabled = true;
                }
                if ((bool)objList._2_2_3_PAD_)
                {
                    chxpad.Checked = true;
                    txtcantpad.Enabled = true;
                }

                txtcantee.Text = objList._2_2_3_EE_Cant.ToString();
                txtcantest.Text = objList._2_2_3_EST_Cant.ToString();
                txtcantedu.Text = objList._2_2_3_EDU_Cant.ToString();
                txtcantdir.Text = objList._2_2_3_DIR_Cant.ToString();
                txtcantpad.Text = objList._2_2_3_PAD_Cant.ToString();

                txtotrocual1.Text = objList._2_2_3_OTR_1;
                txtotrocual2.Text = objList._2_2_3_OTR_2;
                txtotrocual3.Text = objList._2_2_3_OTR_3_;
                txtotrocual4.Text = objList._2_2_3_OTR_4_;
                txtotrocual5.Text = objList._2_2_3_OTR_5_;

                if (txtotrocual1.Text.Trim().Length != 0)
                    Cantidadotro1.Enabled = true;
                if (txtotrocual2.Text.Trim().Length != 0)
                    Cantidadotro2.Enabled = true;
                if (txtotrocual3.Text.Trim().Length != 0)
                    Cantidadotro3.Enabled = true;
                if (txtotrocual4.Text.Trim().Length != 0)
                    Cantidadotro4.Enabled = true;
                if (txtotrocual5.Text.Trim().Length != 0)
                    Cantidadotro5.Enabled = true;

                Cantidadotro1.Text = objList._2_2_3_OTR_1_Cant.ToString();
                Cantidadotro2.Text = objList._2_2_3_OTR_2_Cant.ToString();
                Cantidadotro3.Text = objList._2_2_3_OTR_3_Cant.ToString();
                Cantidadotro4.Text = objList._2_2_3_OTR_4_Cant.ToString();
                Cantidadotro5.Text = objList._2_2_3_OTR_5_Cant.ToString();

                txt224.Text = objList._2_2_4_;
                txt225.Text = objList._2_2_5_;

                if ((bool)objList._2_3_)
                {
                    rbtn23Si.Checked = true;
                    txt231.Enabled = true;
                }
                else
                    rbtn23No.Checked = true;

                txt231.Text = objList._2_3_1_;

                txt31.Text = objList._3_1_;
                txt32.Text = objList._3_2_;
                txt33.Text = objList._3_3_;
                txt34.Text = objList._3_4_;
                txt35.Text = objList._3_5_;
                txt36.Text = objList._3_6_;

                if ((bool)objList._4_1_)
                {
                    rbtn41Si.Checked = true;
                    txt411.Enabled = true;
                }
                else
                    rbtn41No.Checked = true;

                txt411.Text = objList._4_1_1_;


                if ((bool)objList._5_1_RADD_)
                    cblist51DedMun.Items[0].Selected = true;

                if ((bool)objList._5_1_RADM)
                    cblist51Local.Items[0].Selected = true;

                if ((bool)objList._5_1_PREND)
                    cblist51DedMun.Items[1].Selected = true;

                if ((bool)objList._5_1_PRENM)
                    cblist51Local.Items[1].Selected = true;

                if ((bool)objList._5_1_TELD)
                    cblist51DedMun.Items[2].Selected = true;

                if ((bool)objList._5_1_TELM)
                    cblist51Local.Items[2].Selected = true;

                if ((bool)objList._5_1_INT)
                    cblist51DedMun.Items[3].Selected = true;

                if ((bool)objList._5_1_INTM)
                    cblist51Local.Items[3].Selected = true;

                txt52.Text = objList._5_2_;

                if ((bool)objList._1_1_8_)
                {
                    rbtn118Si.Checked = true;
                    txt119.Enabled = true;
                }
                else if (!(bool)objList._1_1_8_)
                    rbtn118No.Checked = true;

                txt119.Text = objList._1_1_9_;
                txtObservaciones.Text = objList.Observaciones;

                btnAlmacenar.Visible = false;
                #endregion
                lecturaContextoTable.Visible = true;
            }
        }

        protected void chxEE_CheckedChanged(object sender, EventArgs e)
        {
            if (chxEE.Checked)
                txtcantee.Enabled = true;
            else
                txtcantee.Enabled = false;
        }

        protected void chxest_CheckedChanged(object sender, EventArgs e)
        {
            if (chxest.Checked)
                txtcantest.Enabled = true;
            else
                txtcantest.Enabled = false;
        }

        protected void chxEdu_CheckedChanged(object sender, EventArgs e)
        {
            if (chxEdu.Checked)
                txtcantedu.Enabled = true;
            else
                txtcantedu.Enabled = false;
        }

        protected void chxdirectivos_CheckedChanged(object sender, EventArgs e)
        {
            if (chxdirectivos.Checked)
                txtcantdir.Enabled = true;
            else
                txtcantdir.Enabled = false;
        }

        protected void chxpad_CheckedChanged(object sender, EventArgs e)
        {
            if (chxpad.Checked)
                txtcantpad.Enabled = true;
            else
                txtcantpad.Enabled = false;
        }
    }
}