using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.Text;

namespace ESM
{
    public partial class BancoProyectos : System.Web.UI.Page
    {

        #region Propiedades publicas y privadas

        protected Objetos.Cproyecto _objProyecto = new Objetos.Cproyecto();
        int proyecto_id = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.IsAuthenticated)
            {

                if (ban_proyecto_id.Value != " ")
                {
                    proyecto_id = Convert.ToInt32(ban_proyecto_id.Value);
                    ban_proyecto_id.Value = ban_proyecto_id.Value;

                    refreshFiles(proyecto_id);
                    CargarMarcoLogico();
                }

                if (!Page.IsPostBack)
                {
                    CargarColeccionProyectos();
                }
            }
            else
                Response.Redirect("Login.aspx");
        }

        protected void btnNuevoProyecto_Click(object sender, EventArgs e)
        {

        }

        protected void btnalmacenarproyecto_Click(object sender, EventArgs e)
        {
            AlmacenarProyetco();
        }

        protected void AlmacenarProyetco()
        {
            try
            {
                string nombre_proyecto = txtnombreproyecto.Value;
                string problema = txtproblema.Value;
                string proposito = txtproposito.Value;
                string finalidad = txtfinalidad.Value;

                if (ban_proyecto_id.Value == " ")
                {
                    int proyecto_id = _objProyecto.Add(problema, nombre_proyecto, proposito, finalidad);
                    ban_proyecto_id.Value = proyecto_id.ToString();
                }
                else
                {
                    int proyecto_id = Convert.ToInt32(ban_proyecto_id.Value);
                    _objProyecto.Update(proyecto_id, nombre_proyecto, problema, proposito, finalidad);
                }

                Response.Redirect("/BancoProyectos.aspx");
            }
            catch (Exception)
            { Response.Write("<script type='text/javascript'>alert('Opss... Ocurrio un error inesperado.');<script/>"); }
        }

        protected void ActualziarProyecto(int proyecto_id)
        {
            try
            {
                Model.Proyecto objProyecto_Obtenido = _objProyecto.GetProyecto(proyecto_id);
                if (objProyecto_Obtenido != null)
                {
                    txtnombreproyecto.Value = objProyecto_Obtenido.Proyecto1.ToString();
                    txtproblema.Value = objProyecto_Obtenido.Problema.ToString();
                    ban_proyecto_id.Value = objProyecto_Obtenido.Id.ToString();
                }
                else
                    Response.Write("alert('Opss... Ocurrio un error inesperado.');");
            }
            catch (Exception)
            {
                Response.Write("alert('Opss... Ocurrio un error inesperado.');");
            }
        }

        protected void CargarColeccionProyectos()
        {
            cmbproyectos.DataSource = _objProyecto.GetProyectos();
            cmbproyectos.DataTextField = "Proyecto1";
            cmbproyectos.DataValueField = "Id";
            cmbproyectos.DataBind();
        }

        protected void btncargarproyecto_Click(object sender, EventArgs e)
        {
            ActualziarProyecto(Convert.ToInt32(cmbproyectos.SelectedValue));
            Response.Write("$('#magazine').turn('next');");
        }

        protected void btncargar_proyecto_Click(object sender, EventArgs e)
        {

        }

        protected void btnalmacenarregistro_Click(object sender, EventArgs e)
        {
            Almacenar_Registro();
        }

        public void Almacenar_Registro()
        {
            try
            {
                Model.Registro_Proyecto proyecto_informacion = (from p in new Model.ESMBDDataContext().Registro_Proyectos
                                                                where p.proyecto_id == Convert.ToInt32(ban_proyecto_id.Value)
                                                                select p).Single();

                Objetos.CRegistro_Proyectos _objCRegistro_Proyectos = new Objetos.CRegistro_Proyectos();

                if (proyecto_informacion == null)
                    _objCRegistro_Proyectos.AddItem(txtcargo.Value, txtdependencia.Value, Convert.ToDateTime(txtfechaelaboracion.Value), txtjustificacion.Value, txtmpp1.Value, txtmpp2.Value, txtmpp3.Value, proyecto_id, txtresponsable.Value);
                else
                    _objCRegistro_Proyectos.UpdateItem(proyecto_informacion.Id, txtcargo.Value, txtdependencia.Value, Convert.ToDateTime(txtfechaelaboracion.Value), txtjustificacion.Value, txtmpp1.Value, txtmpp2.Value, txtmpp3.Value, txtresponsable.Value);

            }
            catch (Exception) { }
        }

        protected void btncargar_Click(object sender, EventArgs e)
        {
            ban_proyecto_id.Value = cmbproyectos.SelectedValue;
            if_c_e.Attributes.Add("src", "/jqgrid_causas_efectos.aspx?proyecto_id=" + ban_proyecto_id.Value);
            if_marco_logico.Attributes.Add("src", "/jqgrid_marco_logico.aspx?proyecto_id=" + ban_proyecto_id.Value);
            if_ejecucion.Attributes.Add("src", "/jqgrid_ejecucion.aspx?proyecto_id=" + ban_proyecto_id.Value);
            if_valores_indicadores.Attributes.Add("src", "jqgrid_valores.aspx?proyecto_id=" + ban_proyecto_id.Value);

            Model.Proyecto proyecto_informacion = (from p in new Model.ESMBDDataContext().Proyectos
                                                   where p.Id == Convert.ToInt32(ban_proyecto_id.Value)
                                                   select p).Single();

            txtnombreproyecto.Value = proyecto_informacion.Proyecto1;
            txtproblema.Value = proyecto_informacion.Problema;
            txtproposito.Value = proyecto_informacion.Proposito;
            txtfinalidad.Value = proyecto_informacion.Finalidad;

            proyecto_id = Convert.ToInt32(ban_proyecto_id.Value);

            CargarMarcoLogico();

            try
            {
                var registro_proyecto = proyecto_informacion.Registro_Proyectos.Single();

                txtdependencia.Value = registro_proyecto.Dependencia;
                txtfechaelaboracion.Value = registro_proyecto.Fecha.ToString();
                txtjustificacion.Value = registro_proyecto.Justificacion;
                txtmpp1.Value = registro_proyecto.Mpp_1;
                txtmpp2.Value = registro_proyecto.Mpp_2;
                txtmpp3.Value = registro_proyecto.Mpp_3;
                txtresponsable.Value = registro_proyecto.responsable;
                txtcargo.Value = registro_proyecto.Cargo;

                string html_arbolObjetivos = generateArbolObjetivosWeb();
                string html_arbolProblemas = generateArbolProlemasWeb();
                org_objetivos.InnerHtml = html_arbolObjetivos;
                org.InnerHtml = html_arbolProblemas;

                refreshFiles(Convert.ToInt32(ban_proyecto_id.Value));
            }
            catch (Exception)
            {

            }

        }

        protected void UploadFiles()
        {
            try
            {
                string nombre_proyecto = txtnombreproyecto.Value.Substring(0, 15);
                FileUpload objFileUpload = files;
                #region Carga de Documento
                if (objFileUpload.PostedFile.FileName != "")
                {
                    string path = Server.MapPath("~/Files/Proyectos");

                    if (!Directory.Exists(path + "/" + nombre_proyecto))
                    {
                        Directory.CreateDirectory(path + "/" + nombre_proyecto);
                        Directory.CreateDirectory(path + "/" + nombre_proyecto + "/Actas");
                        path = path + "/" + nombre_proyecto + "/Actas";
                    }
                    else
                        path = path + "/" + nombre_proyecto + "/Actas";

                    path = path + "/" + objFileUpload.FileName;

                    int iniciar = objFileUpload.FileName.Length - 3;
                    int finalizar = objFileUpload.FileName.Length;

                    var extencion = objFileUpload.FileName.Substring(iniciar);

                    string strextencion = extencion.ToString();

                    if (strextencion != "pdf" && strextencion != "zip" && strextencion != "rar" && strextencion != "doc" && strextencion != "docx")
                    {
                        Response.Write("<script>alert('Error el archivo no pudo ser cargado, revise el tipo de documento que intenta cargar. Formatos compatibles:pdf,doc,zip,rar.');</script>");
                    }
                    else
                    {
                        bool update = false;
                        if (objFileUpload.PostedFile.ContentLength <= 5242880)
                        {
                            if (File.Exists(path))
                            {
                                update = true;
                                File.Delete(path);
                            }

                            objFileUpload.PostedFile.SaveAs(path);

                            if (!update)
                            {
                                if (new Objetos.Cproyecto().CargarDocumentos(objFileUpload.FileName, objFileUpload.PostedFile.ContentType, objFileUpload.PostedFile.ContentLength.ToString(), proyecto_id, "add", 0))
                                    ban_files.Value = "1";
                                else
                                    File.Delete(path);
                            }
                            else
                                ban_files.Value = "1";
                        }
                        else
                        {
                            Response.Write("<script>alert('Para cargar un archivo se debe tener en cuenta que el tamaño sea menor o igual a 5MB');</script>");
                        }
                    }
                }


                #endregion
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Error al cargar el archivo seleccionado.');</script>");
            }
        }

        protected void btnuploadfile_Click(object sender, EventArgs e)
        {
            UploadFiles();
            refreshFiles(proyecto_id);

        }

        protected void refreshFiles(int Proyecto_id)
        {
            var files_proyect = from d_p in new Model.ESMBDDataContext().Documentos_Proyectos
                                where d_p.proyectoid == Proyecto_id
                                select d_p;

            string html_file = "";

            foreach (var item in files_proyect)
            {
                html_file += "<li><strong><a href='/Files/Proyectos/" + item.Proyecto.Proyecto1 + "/Actas/" + item.Ruta + "'>" + item.Ruta + "</a></strong> ( " + item.Tipo + " ) --  Tamaño: " + (Convert.ToInt32(item.Tamano) / 1024).ToString() + "<b>KB</b></li>";

            }

            list.InnerHtml = "<ul>" + html_file + "</ul>";
        }

        protected void btnExportarProyecto_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre_proyecto = txtnombreproyecto.Value.Substring(0, 15);

                string path = Server.MapPath("~/Files/Proyectos/");

                if (Directory.Exists(path + nombre_proyecto))
                {
                    if (!Directory.Exists(path + nombre_proyecto + "/Actas"))
                        Directory.CreateDirectory(path + nombre_proyecto + "/Actas");
                    if (!Directory.Exists(path + nombre_proyecto + "/Programacion"))
                        Directory.CreateDirectory(path + nombre_proyecto + "/Programacion");
                    if (!Directory.Exists(path + nombre_proyecto + "/M.S.E"))
                        Directory.CreateDirectory(path + nombre_proyecto + "/M.S.E");

                    string path_files = path + nombre_proyecto + @"\Programacion\MarcoLogico.xls";
                    string path_files_arbol_problemas = path + nombre_proyecto + @"\Programacion\ArbolProblemas.xls";
                    string path_files_arbol_objetivos = path + nombre_proyecto + @"\Programacion\ArbolObjetivos.xls";

                    string html_marcologico = generateMarcoLogico(proyecto_id);
                    string html_arbolproblemas = generateArbolProblemas();
                    string html_arbolobjetivos = generateArbolObjetivosOrg();
                    if (!File.Exists(path_files))
                    {
                        StreamWriter objFile = new StreamWriter(path_files, true, Encoding.UTF8);
                        objFile.Write(html_marcologico);
                        objFile.Flush();
                        objFile.Close();
                    }
                    else
                    {
                        File.Delete(path_files);

                        StreamWriter objFile = new StreamWriter(path_files, true, Encoding.UTF8);
                        objFile.Write(html_marcologico);
                        objFile.Flush();
                        objFile.Close();
                    }

                    if (!File.Exists(path_files_arbol_problemas))
                    {
                        StreamWriter objFile = new StreamWriter(path_files_arbol_problemas, true, Encoding.UTF8);
                        objFile.Write(html_arbolproblemas);
                        objFile.Flush();
                        objFile.Close();
                    }
                    else
                    {
                        File.Delete(path_files_arbol_problemas);

                        StreamWriter objFile = new StreamWriter(path_files_arbol_problemas, true, Encoding.UTF8);
                        objFile.Write(html_arbolproblemas);
                        objFile.Flush();
                        objFile.Close();
                    }

                    if (!File.Exists(path_files_arbol_objetivos))
                    {
                        StreamWriter objFile = new StreamWriter(path_files_arbol_objetivos, true, Encoding.UTF8);
                        objFile.Write(html_arbolobjetivos);
                        objFile.Flush();
                        objFile.Close();
                    }
                    else
                    {
                        File.Delete(path_files_arbol_objetivos);

                        StreamWriter objFile = new StreamWriter(path_files_arbol_objetivos, true, Encoding.UTF8);
                        objFile.Write(html_arbolobjetivos);
                        objFile.Flush();
                        objFile.Close();
                    }
                }
                else
                {
                    Directory.CreateDirectory(path + "/" + nombre_proyecto);

                    if (!Directory.Exists(path + nombre_proyecto + "/Actas"))
                        Directory.CreateDirectory(path + nombre_proyecto + "/Actas");
                    if (!Directory.Exists(path + nombre_proyecto + "/Programacion"))
                        Directory.CreateDirectory(path + nombre_proyecto + "/Programacion");
                    if (!Directory.Exists(path + nombre_proyecto + "/M.S.E"))
                        Directory.CreateDirectory(path + nombre_proyecto + "/M.S.E");

                    string path_files = path + nombre_proyecto + @"\Programacion\MarcoLogico.xls";
                    string path_files_arbol_problemas = path + nombre_proyecto + @"\Programacion\ArbolProblemas.xls";
                    string path_files_arbol_objetivos = path + nombre_proyecto + @"\Programacion\ArbolObjetivos.xls";

                    string html_marcologico = generateMarcoLogico(proyecto_id);
                    string html_arbolproblemas = generateArbolProblemas();
                    string html_arbolobjetivos = generateArbolObjetivosOrg();
                    if (!File.Exists(path_files))
                    {
                        StreamWriter objFile = new StreamWriter(path_files, true, Encoding.UTF8);
                        objFile.Write(html_marcologico);
                        objFile.Flush();
                        objFile.Close();
                    }
                    else
                    {
                        File.Delete(path_files);

                        StreamWriter objFile = new StreamWriter(path_files, true, Encoding.UTF8);
                        objFile.Write(html_marcologico);
                        objFile.Flush();
                        objFile.Close();
                    }

                    if (!File.Exists(path_files_arbol_problemas))
                    {
                        StreamWriter objFile = new StreamWriter(path_files_arbol_problemas, true, Encoding.UTF8);
                        objFile.Write(html_arbolproblemas);
                        objFile.Flush();
                        objFile.Close();
                    }
                    else
                    {
                        File.Delete(path_files_arbol_problemas);

                        StreamWriter objFile = new StreamWriter(path_files_arbol_problemas, true, Encoding.UTF8);
                        objFile.Write(html_arbolproblemas);
                        objFile.Flush();
                        objFile.Close();
                    }

                    if (!File.Exists(path_files_arbol_objetivos))
                    {
                        StreamWriter objFile = new StreamWriter(path_files_arbol_objetivos, true, Encoding.UTF8);
                        objFile.Write(html_arbolobjetivos);
                        objFile.Flush();
                        objFile.Close();
                    }
                    else
                    {
                        File.Delete(path_files_arbol_objetivos);

                        StreamWriter objFile = new StreamWriter(path_files_arbol_objetivos, true, Encoding.UTF8);
                        objFile.Write(html_arbolobjetivos);
                        objFile.Flush();
                        objFile.Close();
                    }

                }


                if (!File.Exists(path + nombre_proyecto + ".zip"))
                {
                    using (var zip = new Ionic.Zip.ZipFile())
                    {
                        zip.AddDirectory(path + "/" + nombre_proyecto);
                        zip.Save(path + nombre_proyecto + ".zip");
                    }
                }
                else
                {
                    File.Delete(path + nombre_proyecto + ".zip");

                    using (var zip = new Ionic.Zip.ZipFile())
                    {
                        zip.AddDirectory(path + "/" + nombre_proyecto);
                        zip.Save(path + nombre_proyecto + ".zip");
                    }
                }


                Response.Redirect("/Files/Proyectos/" + nombre_proyecto + ".zip");
            }
            catch (Exception ex) { /*Error*/}
        }

        protected void CargarMarcoLogico()
        {
            content_marcologico.InnerHtml = generateMarcoLogico(proyecto_id);
        }

        protected string generateMarcoLogico(int Proyecto_id)
        {
            Model.ESMBDDataContext db = new Model.ESMBDDataContext();

            var procesos = from p in db.Causas_Efectos
                           where p.Proyecto_id == Proyecto_id
                           select p;

            string html = "<table border='1' cellspacing='0' style='border: 1px solid #000;'><caption style='border: 1px solid #000;'>Marco Lógico</caption>";
            html += "<tr><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'>INDICADOR</td><td style='border: 1px solid #000;'>MEDIOS DE VERIFICACIÓN</td><td style='border: 1px solid #000;'>SUPUESTOS</td></tr>";
            int color = 0;
            string color_cadena = "D6D6D6";
            foreach (var procesos_item in procesos)
            {
                if (color == 0)
                {
                    color_cadena = "E0E0E0";
                    color++;
                }
                else
                {
                    color_cadena = "ffffff";
                    color = 0;
                }

                html += "<tr style='background: #" + color_cadena + "'><td style='border: 1px solid #000;'><b>PROCESO:</b></td><td style='border: 1px solid #000;'>" + procesos_item.Causa + "</td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><tr>";
                var subprocesos = from sp in db.Subprocesos
                                  where sp.Causas_Efecto.Proyecto_id == proyecto_id && sp.Proceso_id == procesos_item.Id
                                  select sp;

                foreach (var subprocesos_item in subprocesos)
                {
                    var actividades = from a in db.Actividades
                                      where a.Subproceso.Causas_Efecto.Proyecto_id == proyecto_id && a.Subproceso_id == subprocesos_item.Id
                                      select a;

                    html += "<tr style='background: #" + color_cadena + "'><td style='border: 1px solid #000;'><b>SUBPROCESO:</b></td><td style='border: 1px solid #000;'>" + subprocesos_item.Subproceso1 + "</td><td style='border: 1px solid #000;'>" + subprocesos_item.Indicador + "</td><td style='border: 1px solid #000;'>" + subprocesos_item.Medios + "</td><td style='border: 1px solid #000;'>" + subprocesos_item.Supuestos + "</td><tr>";
                    int count_actividades = 0;
                    foreach (var actividades_item in actividades)
                    {
                        if (count_actividades == 0)
                        {
                            if (actividades.Count() <= 1)
                                html += "<tr style='background: #" + color_cadena + "'><td style='vertical-align: middle; text-align: center; border: 1px solid #000;' rowspan='" + actividades.Count() + "'><b>ACTIVIDAD:</b></td><td style='border: 1px solid #000;'>" + actividades_item.Actividad + "</td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><tr>";
                            else
                                html += "<tr style='background: #" + color_cadena + "'><td style='vertical-align: middle; text-align: center; border: 1px solid #000;' rowspan='" + (actividades.Count() * 2) + "'><b>ACTIVIDAD:</b></td><td style='border: 1px solid #000;'>" + actividades_item.Actividad + "</td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><tr>";
                        }
                        else
                            html += "<tr style='background: #" + color_cadena + "'><td style='border: 1px solid #000;'>" + actividades_item.Actividad + "</td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><td style='border: 1px solid #000;'></td><tr>";

                        count_actividades++;

                    }
                }
            }
            html = html + "</table>";

            return html;
        }

        protected string generateArbolProblemas()
        {
            var arbolproblemas = from p in new Model.ESMBDDataContext().Causas_Efectos
                                 where p.Proyecto_id == proyecto_id
                                 select p;

            string html_arbol_problemas = "<h1>Arbol Problemas</h1><h3>Efectos</h3><table><tr>";
            string problema = "";
            foreach (var item in arbolproblemas)
            {
                problema = item.Proyecto.Problema;
                html_arbol_problemas += "<td style='border: solid 2px #000; width: 100px;'>" + item.Efecto + "</td>";
            }

            html_arbol_problemas += "</tr><tr><td style='height:100px; text-align:center;' colspan='" + arbolproblemas.Count().ToString() + "'>" + problema + "</td></tr><tr>";

            foreach (var item in arbolproblemas)
            {
                problema = item.Proyecto.Problema;
                html_arbol_problemas += "<td style='border: solid 2px #000; width: 100px;'>" + item.Causa + "</td>";
            }

            html_arbol_problemas += "</tr><table><h3>Causas</h3>";

            return html_arbol_problemas;
        }

        protected string generateArbolObjetivos()
        {

            var arbolobjetivos = from p in new Model.ESMBDDataContext().Causas_Efectos
                                 where p.Proyecto_id == proyecto_id
                                 select p;

            string html_arbol_objetivos = "<h1>Arbol Objetivos</h1><h3>Beneficios</h3><table><tr>";
            string problema = "";
            foreach (var item in arbolobjetivos)
            {
                problema = item.Proyecto.Problema;
                html_arbol_objetivos += "<td style='border: solid 2px #000; width: 100px;'>" + item.Beneficios + "</td>";
            }

            html_arbol_objetivos += "</tr><tr><td style='height:100px; text-align:center;' colspan='" + arbolobjetivos.Count().ToString() + "'>" + problema + "</td></tr><tr>";

            foreach (var item in arbolobjetivos)
            {
                problema = item.Proyecto.Problema;
                html_arbol_objetivos += "<td style='border: solid 2px #000 ; width: 100px;'>" + item.Causa + "</td>";
            }

            html_arbol_objetivos += "</tr><table><h3>Objetivos</h3>";

            return html_arbol_objetivos;
        }

        protected string generateArbolProlemasWeb()
        {

            var arbolproblemas = (from p in new Model.ESMBDDataContext().Causas_Efectos
                                  where p.Proyecto_id == proyecto_id
                                  select p).Take(3);

            var proyecto = (from p in new Model.ESMBDDataContext().Proyectos
                            where p.Id == proyecto_id
                            select p).Single();

            string html_arbol_problemas = "<table><tr>";
            string problema = "";
            foreach (var item in arbolproblemas)
            {
                problema = item.Proyecto.Problema;
                html_arbol_problemas += "<td style='border: solid 2px #000; width: 80px; height: 80px; background: #D1AC19; color: #000; text-align: center; vertical-align: middle;' colspan='2'><b>" + item.Efecto + "</b></td><td></td><td style='width: 5px;'></td>";
            }
            html_arbol_problemas += "</tr><tr style='height: 20px;'>";
            int cant_tds = (arbolproblemas.Count() * 2);
            int colspan_proyecto = (arbolproblemas.Count() * 4);
            int count_tds = 0;
            for (int i = 0; i < (cant_tds * 2); i++)
            {
                if (count_tds == 1)
                {
                    html_arbol_problemas += "<td style='border-left: dashed #000 2px;'></td>";
                    count_tds++;
                }
                else if (count_tds == 3)
                {
                    html_arbol_problemas += "<td></td>";
                    count_tds = 0;
                }
                else
                {
                    html_arbol_problemas += "<td ></td>";
                    count_tds++;
                }
            }
            html_arbol_problemas += "</tr><tr><td style='height:100px; text-align:center; vertical-align: middle; border: dashed #000 2px;' colspan='" + colspan_proyecto.ToString() + "'>" + problema + "</td></tr><tr style='height: 20px;'>";

            for (int i = 0; i < (cant_tds * 2); i++)
            {
                if (count_tds == 1)
                {
                    html_arbol_problemas += "<td style='border-left: dashed #000 2px;'></td>";
                    count_tds++;
                }
                else if (count_tds == 3)
                {
                    html_arbol_problemas += "<td></td>";
                    count_tds = 0;
                }
                else
                {
                    html_arbol_problemas += "<td ></td>";
                    count_tds++;
                }
            }

            html_arbol_problemas += "</tr><tr>";
            foreach (var item in arbolproblemas)
            {
                problema = item.Proyecto.Problema;
                html_arbol_problemas += "<td style='border: solid 2px #000; width: 80px; height: 80px; background: #1966D1; color: #fff; text-align: center; vertical-align: middle;' colspan='2'><b>" + item.Causa + "</b></td><td></td><td style='width: 5px;'></td>";
            }

            html_arbol_problemas += "</tr></table>";

            return html_arbol_problemas;
        }

        protected string generateArbolObjetivosWeb()
        {

            var arbolobjetivos = (from p in new Model.ESMBDDataContext().Causas_Efectos
                                  where p.Proyecto_id == proyecto_id
                                  select p).Take(3);

            var proyecto = (from p in new Model.ESMBDDataContext().Proyectos
                            where p.Id == proyecto_id
                            select p).Single();

            string html_arbol_objetivos = "<table><tr>";
            string problema = "";
            foreach (var item in arbolobjetivos)
            {
                problema = item.Proyecto.Problema;
                html_arbol_objetivos += "<td style='border: solid 2px #000; width: 80px; height: 80px; background: #92C414; color: #fff; text-align: center; vertical-align: middle;' colspan='2'><b>" + item.Beneficios + "</b></td><td></td><td style='width: 5px;'></td>";
            }
            html_arbol_objetivos += "</tr><tr style='height: 20px;'>";
            int cant_tds = (arbolobjetivos.Count() * 2);
            int colspan_proyecto = (arbolobjetivos.Count() * 4);
            int count_tds = 0;
            for (int i = 0; i < (cant_tds * 2); i++)
            {
                if (count_tds == 1)
                {
                    html_arbol_objetivos += "<td style='border-left: dashed #000 2px;'></td>";
                    count_tds++;
                }
                else if (count_tds == 3)
                {
                    html_arbol_objetivos += "<td></td>";
                    count_tds = 0;
                }
                else
                {
                    html_arbol_objetivos += "<td ></td>";
                    count_tds++;
                }
            }
            html_arbol_objetivos += "</tr><tr><td style='height:100px; text-align:center; vertical-align: middle; border: dashed #000 2px;' colspan='" + colspan_proyecto.ToString() + "'>" + problema + "</td></tr><tr style='height: 20px;'>";

            for (int i = 0; i < (cant_tds * 2); i++)
            {
                if (count_tds == 1)
                {
                    html_arbol_objetivos += "<td style='border-left: dashed #000 2px;'></td>";
                    count_tds++;
                }
                else if (count_tds == 3)
                {
                    html_arbol_objetivos += "<td></td>";
                    count_tds = 0;
                }
                else
                {
                    html_arbol_objetivos += "<td ></td>";
                    count_tds++;
                }
            }

            html_arbol_objetivos += "</tr><tr>";
            foreach (var item in arbolobjetivos)
            {
                problema = item.Proyecto.Problema;
                html_arbol_objetivos += "<td style='border: solid 2px #000; width: 80px; height: 80px; background: #0571AB; color: #fff; text-align: center; vertical-align: middle;' colspan='2'><b>" + item.Causa + "</b></td><td></td><td style='width: 5px;'></td>";
            }

            html_arbol_objetivos += "</tr></table>";

            return html_arbol_objetivos;
        }

        protected string generateArbolObjetivosOrg()
        {

            var arbolobjetivos = from p in new Model.ESMBDDataContext().Causas_Efectos
                                 where p.Proyecto_id == proyecto_id
                                 select p;

            var proyecto = (from p in new Model.ESMBDDataContext().Proyectos
                            where p.Id == proyecto_id
                            select p).Single();

            string html_arbol_objetivos = "<h1>ÁRBOL OBJETIVOS " + proyecto.Proyecto1.ToUpper() + "</h1><h3 style='color : #92C414;'>BENEFICIOS</h3><table><tr>";
            string problema = "";
            foreach (var item in arbolobjetivos)
            {
                problema = item.Proyecto.Problema;
                html_arbol_objetivos += "<td style='border: solid 2px #000; width: 80px; height: 80px; background: #92C414; color: #fff; text-align: center; vertical-align: middle;' colspan='2'><b>" + item.Beneficios + "</b></td><td></td><td style='width: 5px;'></td>";
            }
            html_arbol_objetivos += "</tr><tr>";
            int cant_tds = (arbolobjetivos.Count() * 2);
            int colspan_proyecto = (arbolobjetivos.Count() * 4);
            int count_tds = 0;
            for (int i = 0; i < (cant_tds * 2); i++)
            {
                if (count_tds == 1)
                {
                    html_arbol_objetivos += "<td style='border-left: dashed #000 2px;'></td>";
                    count_tds++;
                }
                else if (count_tds == 3)
                {
                    html_arbol_objetivos += "<td></td>";
                    count_tds = 0;
                }
                else
                {
                    html_arbol_objetivos += "<td ></td>";
                    count_tds++;
                }
            }
            html_arbol_objetivos += "</tr><tr><td style='height:100px; text-align:center; vertical-align: middle; border: dashed #000 2px;' colspan='" + colspan_proyecto.ToString() + "'>" + problema + "</td></tr><tr>";

            for (int i = 0; i < (cant_tds * 2); i++)
            {
                if (count_tds == 1)
                {
                    html_arbol_objetivos += "<td style='border-left: dashed #000 2px;'></td>";
                    count_tds++;
                }
                else if (count_tds == 3)
                {
                    html_arbol_objetivos += "<td></td>";
                    count_tds = 0;
                }
                else
                {
                    html_arbol_objetivos += "<td ></td>";
                    count_tds++;
                }
            }

            html_arbol_objetivos += "</tr><tr>";
            foreach (var item in arbolobjetivos)
            {
                problema = item.Proyecto.Problema;
                html_arbol_objetivos += "<td style='border: solid 2px #000; width: 80px; height: 80px; background: #0571AB; color: #fff; text-align: center; vertical-align: middle;' colspan='2'><b>" + item.Causa + "</b></td><td></td><td style='width: 5px;'></td>";
            }

            html_arbol_objetivos += "</tr></table><h3 style='color: #0571AB;'>OBJETIVOS</h3>";

            return html_arbol_objetivos;
        }

    }
}