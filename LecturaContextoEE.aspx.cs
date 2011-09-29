using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESM.Objetos;

namespace ESM
{
    public partial class LecturaContextoEE : System.Web.UI.Page
    {
        #region

        CLecturaContextoEE objCLecturaContextoEE = new CLecturaContextoEE();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                CRoles objCRoles = new CRoles();
                if (!Page.IsPostBack)
                {
                    int idusuario = Convert.ToInt32(Session["idusuario"]);
                    string rol = objCRoles.ObtenerRol(idusuario);

                    if (rol == "Administrador")
                    {
                        /*Cargo el control gridview con el data source obtenido de instituciones educativas*/
                        gvResultados.DataSourceID = "ldsies";
                        gvResultados.DataBind();
                    }
                    else if (rol == "Consultor")
                    {
                        gvResultados.DataSource = CEE.ObtenerEEs(objCRoles.IdConsultor);
                        gvResultados.DataBind();
                    }
                    else
                    {
                        Response.Write("<script>alert('Acceso Denagado!');</script>");
                        Response.Redirect("Login.aspx");
                    }


                    ObtenerTema(gvResultados);
                    modEESeleccion.Visible = true;
                }
            }
            else
                Response.Write("/Login.aspx");
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            int idmedicion = CEE.CrearMedicionLC(DateTime.Now);

            if (idmedicion != 0)
            {
                Session.Add("idmedicionlcee", idmedicion);
                LC.Visible = true;
            }
        }

        protected void gvResultados_SelectedIndexChanged(object sender, EventArgs e)
        {
            modEESeleccion.Visible = false;
            ModMediciones.Visible = true;
            //LC.Visible = true;

            Session.Add("codie", gvResultados.SelectedRow.Cells[1].Text);
            string codie = Session["codie"].ToString();

            Label objIDIE = (Label)gvResultados.SelectedRow.FindControl("IDIE");

            int idie = Convert.ToInt32(objIDIE.Text);
            Session.Add("ideelc", idie);
            ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();
            var ie = from ies in db.Establecimiento_Educativo
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

            IQueryable<ESM.Model.LecturaContextoEE> mediciones = CEE.ObtenerMedicionesEE(idie);



            if (mediciones.Count() != 0 && mediciones != null)
            {
                var med = (from m in mediciones
                           select new { IdMedicion = m.IdMedicion, Fecha = m.Mediciones.FechaMedicion }).Take(1);

                gvMediciones.DataSource = med;
                gvMediciones.DataBind();
                gvMediciones.Visible = true;
                ObtenerTema(gvMediciones);
                btnRegistrar.Visible = false;
            }
            else
                btnRegistrar.Visible = true;


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

        protected void btnAlmacenar_Click(object sender, EventArgs e)
        {
            int idmedicion = Convert.ToInt32(Session["idmedicionlcee"]);
            int idee = Convert.ToInt32(Session["ideelc"]);
            Almacenar(idee, idmedicion);
        }

        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            txt25_1.Text = " ";
            txt25_1.Enabled = false;
            txt25_2.Text = " ";
            txt25_2.Enabled = false;
            txt25_3.Text = " ";
            txt25_3.Enabled = false;
        }

        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            txt25_1.Text = " ";
            txt25_1.Enabled = true;
            txt25_2.Text = " ";
            txt25_2.Enabled = true;
            txt25_3.Text = " ";
            txt25_3.Enabled = true;
        }

        protected void RadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            txt341.Enabled = false;
            txt341.Text = " ";
        }

        protected void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            txt341.Enabled = true;
            txt341.Text = " ";
        }

        protected void Almacenar(int idee, int idmed)
        {
            #region Asignacion

            if (cblistjornadas.Items[0].Selected)
                objCLecturaContextoEE.F11 = true;
            if (cblistjornadas.Items[1].Selected)
                objCLecturaContextoEE.F12 = true;
            if (cblistjornadas.Items[2].Selected)
                objCLecturaContextoEE.F13 = true;
            if (cblistjornadas.Items[3].Selected)
                objCLecturaContextoEE.F14 = true;
            if (cblistjornadas.Items[4].Selected)
                objCLecturaContextoEE.F15 = true;


            objCLecturaContextoEE.Sedes = Convert.ToInt32(txtSedes.Text);

            if (rbtnRural.Checked)
                objCLecturaContextoEE._1_2_Rural = true;
            if (rbtnUrbana.Checked)
                objCLecturaContextoEE._1_2_Urbana = true;

            if (cblistTipo.Items[0].Selected)
                objCLecturaContextoEE._1_2_c1 = true;
            if (cblistTipo.Items[1].Selected)
                objCLecturaContextoEE._1_2_c2 = true;
            if (cblistTipo.Items[2].Selected)
                objCLecturaContextoEE._1_2_c3 = true;
            if (cblistTipo.Items[3].Selected)
                objCLecturaContextoEE._1_2_c4 = true;

            objCLecturaContextoEE._2_2_E1_ = Convert.ToInt32(txtE1.Text);
            objCLecturaContextoEE._2_2_E2_ = Convert.ToInt32(txtE2.Text);
            objCLecturaContextoEE._2_2_E2_ = Convert.ToInt32(txtE3.Text);
            objCLecturaContextoEE._2_2_E4_ = Convert.ToInt32(txtE4.Text);
            objCLecturaContextoEE._2_2_E5_ = Convert.ToInt32(txtE5.Text);
            objCLecturaContextoEE._2_2_E6_ = Convert.ToInt32(txtE6.Text);

            objCLecturaContextoEE._2_3_S1_ = Convert.ToInt32(txtS1.Text);
            objCLecturaContextoEE._2_3_S2_ = Convert.ToInt32(txtS2.Text);
            objCLecturaContextoEE._2_3_S3_ = Convert.ToInt32(txtS3.Text);
            objCLecturaContextoEE._2_3_S4_ = Convert.ToInt32(txtS4.Text);
            objCLecturaContextoEE._2_3_SNosabe_ = Convert.ToInt32(txtNoSabe.Text);
            objCLecturaContextoEE._2_3_SNoTiene_ = Convert.ToInt32(txtNotiene.Text);

            if (rbtnSi24.Checked)
                objCLecturaContextoEE._2_4_Si = true;
            else if (rbtnNo24.Checked)
                objCLecturaContextoEE._2_4_No = true;
            else
            {
                objCLecturaContextoEE._2_4_Si = false;
                objCLecturaContextoEE._2_4_No = false;
            }

            objCLecturaContextoEE._2_5_1_ = Convert.ToInt32(txt25_1.Text);
            objCLecturaContextoEE.__2_5_2_ = Convert.ToInt32(txt25_2.Text);
            objCLecturaContextoEE._2_5_3_ = Convert.ToInt32(txt25_3.Text);

            objCLecturaContextoEE._3_1_ = txt31.Text;
            objCLecturaContextoEE._3_2_ = txt32.Text;
            objCLecturaContextoEE._3_3 = txt33.Text;

            if (rbtnSi34.Checked)
                objCLecturaContextoEE._3_4_Si = true;
            else if (rbtnNo34.Checked)
                objCLecturaContextoEE._3_4_No = true;
            else
            {
                objCLecturaContextoEE._3_4_Si = false;
                objCLecturaContextoEE._3_4_No = false;
            }

            objCLecturaContextoEE._3_4_1_ = txt341.Text;

            if (rbtnSi35.Checked)
                objCLecturaContextoEE._3_5_Si = true;
            else if (rbtnNo35.Checked)
                objCLecturaContextoEE._3_5_No = true;
            else
            {
                objCLecturaContextoEE._3_5_Si = false;
                objCLecturaContextoEE._3_5_No = false;
            }

            objCLecturaContextoEE._3_5_1_ = txt351.Text;
            objCLecturaContextoEE._3_5_2_ = txt352.Text;
            objCLecturaContextoEE._3_5_3_ = txt353.Text;
            objCLecturaContextoEE._3_5_4_ = txt354.Text;
            objCLecturaContextoEE._3_5_5_ = txt355.Text;
            objCLecturaContextoEE._3_5_6_ = txt356.Text;
            objCLecturaContextoEE._3_5_7_ = txt357.Text;
            objCLecturaContextoEE._3_6_ = txt36.Text;
            objCLecturaContextoEE._3_7_ = txt37.Text;

            if (rbtnSi38.Checked)
                objCLecturaContextoEE._3_8_Si = true;
            else if (rbtnNo38.Checked)
                objCLecturaContextoEE._3_8_No = true;
            else
            {
                objCLecturaContextoEE._3_8_Si = false;
                objCLecturaContextoEE._3_8_No = false;
            }

            objCLecturaContextoEE._3_8_1 = txt381.Text;

            if (rbtnSi39.Checked)
                objCLecturaContextoEE._3_9_Si = true;
            else if (rbtnNo39.Checked)
                objCLecturaContextoEE._3_9_No = true;
            else
            {
                objCLecturaContextoEE._3_9_Si = false;
                objCLecturaContextoEE._3_9_No = false;
            }

            objCLecturaContextoEE._3_9_1_ = txt391.Text;

            if (rbtnSi41.Checked)
                objCLecturaContextoEE._4_1_Si = true;
            else if (rbtnNo41.Checked)
                objCLecturaContextoEE._4_1_No = true;
            else if (rbtnAlgunnas41.Checked)
                objCLecturaContextoEE._4_1_Algunas = true;
            else
            {
                objCLecturaContextoEE._4_1_Si = false;
                objCLecturaContextoEE._4_1_No = false;
                objCLecturaContextoEE._4_1_Algunas = false;
            }

            objCLecturaContextoEE._4_2_ = Convert.ToInt32(txt42.Text);

            if (rbtnSi43.Checked)
                objCLecturaContextoEE._4_3_Si = true;
            else if (rbtnNo43.Checked)
                objCLecturaContextoEE._4_3_No = true;
            else
            {
                objCLecturaContextoEE._4_3_Si = true;
                objCLecturaContextoEE._4_3_No = true;
            }

            objCLecturaContextoEE._4_3_1_ = txt431.Text;
            objCLecturaContextoEE.Observaciones = txtObservaciones.Text;

            #endregion

            if (objCLecturaContextoEE.Almacenar(idee, idmed))
                Response.Write("<script>alert('El proceso de almacenamiento finalizo correctamente.');</script>");
            else
                Response.Write("<script>alert('El proceso de almacenamiento finalizo sin exito.');</script>");
        }

        protected void gvMediciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            LC.Visible = true;
            CargarLCEE();

        }

        protected void CargarLCEE()
        {

            #region Cargar Controles

            #endregion
        }
    }
}