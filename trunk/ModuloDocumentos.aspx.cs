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
                gvDocumentos.DataSource = objCDocumentos.Show(idmedicion);
                gvDocumentos.DataBind();
            }
            else
                Response.Redirect("");

            if (!Page.IsPostBack)
            {

            }
        }

        protected void btnUpPEI_Click(object sender, ImageClickEventArgs e)
        {
            CargarArchivos(FPEI, 1);
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

                        gvDocumentos.DataSource = objCDocumentos.Show(idmedicion);
                        gvDocumentos.DataBind();

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

                            gvDocumentos.DataSource = objCDocumentos.Show(idmedicion);
                            gvDocumentos.DataBind();

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

        protected void btnUpPMI_Click(object sender, ImageClickEventArgs e)
        {
            CargarArchivos(FPMI, 2);
        }

        protected void btnUpMC_Click(object sender, ImageClickEventArgs e)
        {
            CargarArchivos(FMC, 3);
        }

        protected void btnUpPS_Click(object sender, ImageClickEventArgs e)
        {
            CargarArchivos(FPS, 4);
        }

        protected void btnUpActas_Click(object sender, ImageClickEventArgs e)
        {
            CargarArchivos(FActas, 5);
        }

        protected void btnUpProyectos_Click(object sender, ImageClickEventArgs e)
        {
            CargarArchivos(FProyecto, 6);
        }

        protected void btnUpConvenio_Click(object sender, ImageClickEventArgs e)
        {
            CargarArchivos(FConvenio, 7);
        }

        protected void btnUpPcc_Click(object sender, ImageClickEventArgs e)
        {
            CargarArchivos(FPCC, 8);
        }

        protected void btnUpPEIS_Click(object sender, EventArgs e)
        {
            CargarArchivos(FPEI, 1);
        }

    }
}