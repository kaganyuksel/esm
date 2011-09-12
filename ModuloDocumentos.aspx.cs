using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace ESM
{
    public partial class ModuloDocumentos : System.Web.UI.Page
    {
        #region Propiedades Publicas y Privadas

        EvaluationSettings.CDocumentos objCDocumentos = new EvaluationSettings.CDocumentos();
        int idmedicion = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["idmedicion"] != null)
            {
                idmedicion = Convert.ToInt32(Request.QueryString["idmedicion"].ToString());
            }
            else
                Response.Redirect("");

            if (!Page.IsPostBack)
            {
                for (int i = 1; i < 11; i++)
                {
                    bool exist = false;
                    exist = objCDocumentos.ExisteDocumento(i, idmedicion);
                    IQueryable<ESM.Model.AsignaDocumentos> docs = objCDocumentos.Show(idmedicion);

                    foreach (var item in docs)
                    {
                        if (i == 1)
                        {
                            if (exist && i == item.IdDocumento)
                            {
                                lknPEI.Visible = true;
                                lknPEI.HRef = "/Files/" + item.Ruta;
                                lknPEI.InnerHtml = item.Ruta.ToString();
                                btnUpPEIS.Text = "Reemplazar";
                            }
                        }
                        else if (i == 2)
                        {
                            if (exist && i == item.IdDocumento)
                            {
                                lknPMI.Visible = true;
                                lknPMI.HRef = "/Files/" + item.Ruta;
                                lknPMI.InnerHtml = item.Ruta.ToString();
                                btnUpPMI.Text = "Reemplazar";
                            }
                        }
                        else if (i == 3)
                        {
                            if (exist && i == item.IdDocumento)
                            {
                                lknMC.Visible = true;
                                lknMC.HRef = "/Files/" + item.Ruta;
                                lknMC.InnerHtml = item.Ruta.ToString();
                                btnUpMC.Text = "Reemplazar";
                            }
                        }
                        else if (i == 4)
                        {
                            if (exist && i == item.IdDocumento)
                            {
                                lknPS.Visible = true;
                                lknPS.HRef = "/Files/" + item.Ruta;
                                lknPS.InnerHtml = item.Ruta.ToString();
                                btnUpPS.Text = "Reemplazar";
                            }
                        }
                        else if (i == 9)
                        {
                            if (exist && i == item.IdDocumento)
                            {
                                lknDPP.Visible = true;
                                lknDPP.HRef = "/Files/" + item.Ruta;
                                lknDPP.InnerHtml = item.Ruta.ToString();
                                btnUpDPP.Text = "Reemplazar";
                            }
                        }
                        else if (i == 10)
                        {
                            if (exist && i == item.IdDocumento)
                            {
                                lknOtros.Visible = true;
                                lknOtros.HRef = "/Files/" + item.Ruta;
                                lknOtros.InnerHtml = item.Ruta.ToString();
                                btnUpOtros.Text = "Reemplazar";
                            }
                        }
                    }


                }

            }
        }

        protected void CargarArchivos(FileUpload objFileUpload, int iddocumento)
        {
            try
            {
                if (idmedicion != 0)
                {
                    if (!objCDocumentos.ExisteDocumento(iddocumento, idmedicion))
                    {
                        #region Carga de Documento
                        if (objFileUpload.PostedFile.FileName != "")
                        {
                            string path = String.Format("{0}{1}", Server.MapPath("~/Files/"), objFileUpload.FileName);
                            int iniciar = objFileUpload.FileName.Length - 3;
                            int finalizar = objFileUpload.FileName.Length;

                            var extencion = objFileUpload.FileName.Substring(iniciar);

                            string strextencion = extencion.ToString();

                            if (strextencion != "pdf" && strextencion != "zip" && strextencion != "rar" && strextencion != "doc")
                            {
                                Response.Write("<script>Alert('Error el archivo no pudo ser cargado, revise el tipo de documento que intenta cargar. \n\nFormatos compatibles:pdf,doc,zip,rar.');</script>");
                                lblMensaje.Text = "Error el archivo no pudo ser cargado, revise el tipo de documento que intenta cargar. \n\nFormatos compatibles:pdf,doc,zip,rar.";
                            }
                            else
                            {
                                if (objFileUpload.PostedFile.ContentLength <= 2097152)
                                {
                                    if (File.Exists(path))
                                        File.Delete(path);

                                    objFileUpload.PostedFile.SaveAs(path);

                                    if (objCDocumentos.CargarDocumento(idmedicion, iddocumento, objFileUpload.FileName))
                                    {
                                        Response.Write("<script>Alert('El proceso de carga para el archivo finalizo correctamente.');</script>");
                                        lblMensaje.Text = "El proceso de carga para el archivo finalizo correctamente.";

                                    }
                                    else
                                        File.Delete(path);
                                }
                                else
                                {
                                    Response.Write("<script>Alert('Para cargar un archivo se debe tener en cuenta que el tamaño sea menor o igual a 2MB');</script>");
                                    lblMensaje.Text = "Para cargar un archivo se debe tener en cuenta que el tamaño sea menor o igual a 2MB";
                                }
                            }
                        }
                        lblMensaje.Visible = true;

                        switch (iddocumento)
                        {
                            case 1:
                                lknPEI.Visible = true;
                                lknPEI.HRef = "/Files/" + objFileUpload.FileName;
                                lknPEI.InnerHtml = objFileUpload.FileName;
                                btnUpPEIS.Text = "Reemplazar";
                                break;
                            case 2:
                                lknPMI.Visible = true;
                                lknPMI.HRef = "/Files/" + objFileUpload.FileName;
                                lknPMI.InnerHtml = objFileUpload.FileName;
                                btnUpPMI.Text = "Reemplazar";
                                break;
                            case 3:
                                lknMC.Visible = true;
                                lknMC.HRef = "/Files/" + objFileUpload.FileName;
                                lknMC.InnerHtml = objFileUpload.FileName;
                                btnUpMC.Text = "Reemplazar";
                                break;
                            case 4:
                                lknPS.Visible = true;
                                lknPS.HRef = "/Files/" + objFileUpload.FileName;
                                lknPEI.InnerHtml = objFileUpload.FileName;
                                btnUpPS.Text = "Reemplazar";
                                break;
                            case 9:
                                lknDPP.Visible = true;
                                lknDPP.HRef = "/Files/" + objFileUpload.FileName;
                                lknDPP.InnerHtml = objFileUpload.FileName;
                                btnUpDPP.Text = "Reemplazar";
                                break;

                            case 10:
                                lknOtros.Visible = true;
                                lknOtros.HRef = "/Files/" + objFileUpload.FileName;
                                lknOtros.InnerHtml = objFileUpload.FileName;
                                btnUpOtros.Text = "Reemplazar";
                                break;
                        }

                        #endregion
                    }
                    else
                    {
                        if (objCDocumentos.EliminarArchivo(idmedicion, iddocumento))
                        {
                            #region Carga de Documento
                            if (objFileUpload.PostedFile.FileName != "")
                            {
                                string path = String.Format("{0}{1}", Server.MapPath("~/Files/"), objFileUpload.FileName);
                                int iniciar = objFileUpload.FileName.Length - 3;
                                int finalizar = objFileUpload.FileName.Length;

                                var extencion = objFileUpload.FileName.Substring(iniciar);

                                string strextencion = extencion.ToString();

                                if (strextencion != "pdf" && strextencion != "zip" && strextencion != "rar" && strextencion != "doc")
                                {
                                    Response.Write("<script>Alert('Error el archivo no pudo ser cargado, revise el tipo de documento que intenta cargar. \n\nFormatos compatibles:pdf,doc,zip,rar.');</script>");
                                    lblMensaje.Text = "Error el archivo no pudo ser cargado, revise el tipo de documento que intenta cargar. \n\nFormatos compatibles:pdf,doc,zip,rar.";
                                }
                                else
                                {
                                    if (objFileUpload.PostedFile.ContentLength <= 2097152)
                                    {
                                        if (File.Exists(path))
                                            File.Delete(path);

                                        FPEI.PostedFile.SaveAs(path);

                                        if (objCDocumentos.CargarDocumento(idmedicion, iddocumento, objFileUpload.FileName))
                                        {
                                            Response.Write("<script>Alert('El proceso de carga para el archivo finalizo correctamente.');</script>");
                                            lblMensaje.Text = "El proceso de carga para el archivo finalizo correctamente.";
                                        }
                                        else
                                            File.Delete(path);
                                    }
                                    else
                                    {
                                        Response.Write("<script>Alert('Para cargar un archivo se debe tener en cuenta que el tamaño sea menor o igual a 2MB');</script>");
                                        lblMensaje.Text = "Para cargar un archivo se debe tener en cuenta que el tamaño sea menor o igual a 2MB";
                                    }
                                }
                            }
                            lblMensaje.Visible = true;

                            switch (iddocumento)
                            {
                                case 1:
                                    lknPEI.Visible = true;
                                    lknPEI.HRef = "/Files/" + objFileUpload.FileName;
                                    btnUpPEIS.Text = "Reemplazar";
                                    lknPEI.InnerHtml = objFileUpload.FileName;
                                    break;
                                case 2:
                                    lknPMI.Visible = true;
                                    lknPMI.HRef = "/Files/" + objFileUpload.FileName;
                                    btnUpPMI.Text = "Reemplazar";
                                    break;
                                case 3:
                                    lknMC.Visible = true;
                                    lknMC.HRef = "/Files/" + objFileUpload.FileName;
                                    lknMC.InnerHtml = objFileUpload.FileName;
                                    btnUpMC.Text = "Reemplazar";
                                    break;
                                case 4:
                                    lknPS.Visible = true;
                                    lknPS.HRef = "/Files/" + objFileUpload.FileName;
                                    lknPS.InnerHtml = objFileUpload.FileName;
                                    btnUpPS.Text = "Reemplazar";
                                    break;
                                case 9:
                                    lknDPP.Visible = true;
                                    lknDPP.HRef = "/Files/" + objFileUpload.FileName;
                                    lknDPP.InnerHtml = objFileUpload.FileName;
                                    btnUpDPP.Text = "Reemplazar";
                                    break;
                                case 10:
                                    lknOtros.Visible = true;
                                    lknOtros.HRef = "/Files/" + objFileUpload.FileName;
                                    lknOtros.InnerHtml = objFileUpload.FileName;
                                    btnUpOtros.Text = "Reemplazar";
                                    break;
                            }

                            #endregion
                        }
                        else
                        {
                            lblMensaje.Text = "No se puede reemplazar este documento.";
                        }
                    }
                }
                else
                {
                    lblMensaje.Text = "La medicion no pudo ser identificada o no existe.";
                }
            }
            catch (Exception)
            {
                Response.Write("<script>Alert('Error al cargar el archivo seleccionado.');</script>");
                lblMensaje.Text = "Error al cargar el archivo seleccionado.";

            }
        }

        protected void btnUpPS_Click(object sender, EventArgs e)
        {
            CargarArchivos(FPS, 4);
        }

        protected void btnUpActas_Click(object sender, EventArgs e)
        {
            CargarArchivos(FOtros, 10);
        }


        protected void btnUpPcc_Click(object sender, EventArgs e)
        {
            CargarArchivos(FDPP, 9);
        }

        protected void btnUpPEIS_Click(object sender, EventArgs e)
        {
            CargarArchivos(FPEI, 1);
        }

        protected void ReemplazarControles()
        {
            ESM.Model.ESMBDDataContext db = new Model.ESMBDDataContext();

            var a = from asig in db.AsignaDocumentos
                    where asig.IdMedicion == idmedicion
                    select asig;

            foreach (var item in a)
            {
                if (item.IdDocumento == 1)
                {
                    lknPEI.Visible = true;
                    btnUpPEIS.Text = "Reemplazar";
                    FPEI.Visible = false;
                }
                if (item.IdDocumento == 2)
                {
                    lknPMI.Visible = true;
                    btnUpPMI.Text = "Reemplazar";
                    FPMI.Visible = false;
                }
                if (item.IdDocumento == 3)
                {
                    lknMC.Visible = true;
                    btnUpMC.Text = "Reemplazar";
                    FMC.Visible = false;
                }
            }
        }

        protected void btnUpPMI_Click(object sender, EventArgs e)
        {
            CargarArchivos(FPMI, 2);
        }

        protected void btnUpMC_Click(object sender, EventArgs e)
        {
            CargarArchivos(FMC, 3);
        }

    }
}