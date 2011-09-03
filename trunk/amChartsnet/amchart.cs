using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Web.UI;

namespace ESM.amChartsnet
{
    public class AmCharts
    {
        //FLASH
        public string genera_chart_Pie_Flash(string DivId, string Sdatos, string sTitulo, Page pagina, Int32 ancho = 600, Int32 alto = 400, string sColors = "FF0F00,FF6600,FF9E01,FCD202,F8FF01,B0DE09,04D215,0D8ECF,0D52D1,2A0CD0,8A0CCF,CD0D74,754DEB,DDDDDD,999999,333333,990000")
        {
            try
            {
                Type cstype = pagina.GetType();
                ClientScriptManager cs = pagina.ClientScript;
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("var params ={bgcolor: \"#FFFFFF\"}");
                sb.AppendLine("var flashVars = { path: \"amcharts/flash/\", chart_settings: encodeURIComponent(\"<settings><redraw>1</redraw><background><alpha>100</alpha></background><balloon><alpha>77</alpha><border_width>3</border_width><border_color>000000</border_color><corner_radius>5</corner_radius></balloon><legend><enabled>0</enabled><align>center</align></legend><pie><colors>" + sColors + "</colors><y>50%</y><radius>30%</radius><inner_radius>40</inner_radius><height>40</height><angle>20</angle><start_angle>27</start_angle><outline_alpha>13</outline_alpha><brightness_step>24</brightness_step><gradient>radial</gradient></pie><animation><start_time>1.8</start_time><start_radius>251%</start_radius><start_alpha>8</start_alpha><pull_out_time>0.3</pull_out_time></animation><data_labels><show>{title}: {value} %</show><radius>40%</radius><text_size>14</text_size><max_width>160</max_width><line_alpha>7</line_alpha></data_labels><export_as_image><file>amcharts/flash/export.aspx</file><color>#CC0000</color><alpha>50</alpha></export_as_image><labels><label><x>0</x><y>40</y><align>center</align><text_size>12</text_size><text><![CDATA[<b>" + sTitulo + "</b>]]></text></label></labels></settings>\") ");
                sb.AppendLine(", chart_data: encodeURIComponent(\"" + Sdatos + "\") };");
                sb.AppendLine("window.onload = function () {");
                sb.AppendLine(" if (swfobject.hasFlashPlayerVersion(\"8\")) {swfobject.embedSWF(\"amcharts/flash/ampie.swf?cache=0\", \"" + DivId + "\", \"" + ancho + "\", \"" + alto + "\", \"8.0.0\", \"amcharts/flash/expressInstall.swf\", flashVars, params);");
                sb.AppendLine(" }else {");
                sb.AppendLine(" var amFallback = new AmCharts.AmFallback();");
                sb.AppendLine(" amFallback.type = \"pie\";");
                sb.AppendLine("  amFallback.write(\"" + DivId + "\");");
                sb.AppendLine(" }");
                sb.AppendLine("}");
                if ((cs.IsStartupScriptRegistered(DivId) == false))
                {
                    cs.RegisterStartupScript(cstype, DivId, sb.ToString(), true);
                }
                return "";
            }
            catch (Exception ex)
            {
                return "<span>" + ex.Message + "</span>";
            }
        }

        public string genera_chart_column_Flash(string DivId, string Sdatos, string sTitulo, Page pagina, Int32 ancho = 600, Int32 alto = 400)
        {
            try
            {
                Type cstype = pagina.GetType();
                ClientScriptManager cs = pagina.ClientScript;
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("var params ={bgcolor: \"#FFFFFF\"}");
                sb.AppendLine("var flashVars = { path: \"/AmCharts/flash/\", chart_settings: encodeURIComponent(\"<settings>  <thousands_separator>18</thousands_separator><decimals_separator>.</decimals_separator><digits_after_decimal>3</digits_after_decimal><background><alpha>100</alpha><border_alpha>0</border_alpha></background><grid><category><dashed>1</dashed></category><value><dashed>1</dashed></value></grid><axes><category><width>1</width><color>E7E7E7</color></category><value><width>1</width><color>E7E7E7</color></value></axes><values><value><min>0</min></value></values><balloon><alpha>62</alpha></balloon><column><type>clustered</type><width>89</width><alpha>85</alpha><border_alpha>95</border_alpha><balloon_text><![CDATA[En {series} {percents}% de las mejoras están {title}</b>]]></balloon_text><grow_time>3</grow_time><sequenced_grow>1</sequenced_grow></column><depth>15</depth><graphs><graph gid='0'><title>Medicion Inicial</title><color>e0081d</color></graph><graph gid='1'><title>Medicion Final</title><color>005ea7</color></graph><graph gid='2'><title>Line</title><color>FEC514</color></graph></graphs><labels><label lid='0'><text><![CDATA[<b>" + sTitulo + "</b>]]></text><y>18</y><text_color>000000</text_color><text_size>13</text_size><align>center</align></label></labels></settings>\") ");
                sb.AppendLine(", chart_data: encodeURIComponent(\"" + Sdatos + "\") };");
                sb.AppendLine("window.onload = function () {");
                sb.AppendLine(" if (swfobject.hasFlashPlayerVersion(\"8\")) {swfobject.embedSWF(\"/AmCharts/flash/amcolumn.swf?cache=0\", \"" + DivId + "\", \"" + ancho + "\", \"" + alto + "\", \"8.0.0\", \"/AmCharts/flash/expressInstall.swf\", flashVars, params);");
                sb.AppendLine(" }else {");
                sb.AppendLine("  var amFallback = new AmCharts.AmFallback();");
                sb.AppendLine("  amFallback.pathToImages = \"/AmCharts/javascript/images/\";");
                sb.AppendLine("  amFallback.type = \"column\";");
                sb.AppendLine("  amFallback.write(\"" + DivId + "\");");
                sb.AppendLine(" }");
                sb.AppendLine("}");
                if ((cs.IsStartupScriptRegistered(DivId) == false))
                {
                    cs.RegisterStartupScript(cstype, DivId, sb.ToString(), true);
                    
                }
                return "";
            }
            catch (Exception ex)
            {
                return "<span>" + ex.Message + "</span>";
            }
        }

        public string genera_chart_line_Flash(string DivId, string Sdatos, string sTitulo, string Graphs, Page pagina, Int32 ancho = 600, Int32 alto = 400)
        {
            try
            {
                Type cstype = pagina.GetType();
                ClientScriptManager cs = pagina.ClientScript;
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("var params ={bgcolor: \"#FFFFFF\"}");
                sb.AppendLine("var flashVars = { path: \"amcharts/flash/\", chart_settings: encodeURIComponent(\"<settings><text_color>oooooo</text_color><digits_after_decimal>2</digits_after_decimal><background><file></file></background><plot_area><margins><left>0</left><right>0</right><top>15</top></margins></plot_area><grid><x><alpha>0</alpha><approx_count>10</approx_count></x><y_left><color>FFFFF1</color><alpha>10</alpha></y_left></grid><axes><x><width>1</width><color>FFFFFF</color><alpha>0</alpha></x><y_left><width>1</width><alpha>0</alpha></y_left></axes><values><x><skip_first>1</skip_first><skip_last>1</skip_last></x><y_left><inside>1</inside><skip_last>1</skip_last><unit></unit><unit_position>left</unit_position></y_left></values><scroller><height>15</height><color>000000</color><alpha>30</alpha><bg_color>FFFFFF</bg_color><bg_alpha>20</bg_alpha></scroller><indicator><selection_color>000000</selection_color></indicator><legend><alpha>20</alpha><margins>15</margins><align>center</align><values><enabled>1</enabled><text>{value}</text></values></legend><zoom_out_button><alpha>100</alpha><text_color>FFFFFF</text_color><text_color_hover>000000</text_color_hover></zoom_out_button><graphs>" + Graphs + "</graphs><export_as_image><file>amcharts/flash/export.aspx</file><color>#CC0000</color><alpha>50</alpha></export_as_image><labels><label lid='0'><text><![CDATA[<b>" + sTitulo + "</b>]]></text><y>5</y><text_size>15</text_size><align>center</align></label></labels></settings>\") ");
                sb.AppendLine(", chart_data: encodeURIComponent(\"" + Sdatos + "\") };");
                sb.AppendLine("window.onload = function () {");
                sb.AppendLine(" if (swfobject.hasFlashPlayerVersion(\"8\")) {swfobject.embedSWF(\"amcharts/flash/amline.swf?cache=0\", \"" + DivId + "\", \"" + ancho + "\", \"" + alto + "\", \"8.0.0\", \"amcharts/flash/expressInstall.swf\", flashVars, params);");
                sb.AppendLine(" }else {");
                sb.AppendLine("  var amFallback = new AmCharts.AmFallback();");
                sb.AppendLine("  amFallback.pathToImages = \"amcharts/javascript/images/\";");
                sb.AppendLine("  amFallback.type = \"line\";");
                sb.AppendLine("  amFallback.write(\"" + DivId + "\");");
                sb.AppendLine(" }");
                sb.AppendLine("}");
                if ((cs.IsStartupScriptRegistered(DivId) == false))
                {
                    cs.RegisterStartupScript(cstype, DivId, sb.ToString(), true);
                }
                return "";
            }
            catch (Exception ex)
            {
                return "<span>" + ex.Message + "</span>";
            }
        }

        //JAVASCRIPT
        public string genera_chart_Pie_Javascript(string DivId, Page Pagina)
        {
            try
            {
                string Qry = "";
                Type cstype = Pagina.GetType();
                ClientScriptManager cs = Pagina.ClientScript;
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("var chartData = [{ country: \"Implementada\", visits: 68 },{ country: \"En proceso de implementación\", visits: 16 },{ country: \"Sin implementar\", visits: 16}];");
                sb.AppendLine("var chart;");
                sb.AppendLine("window.onload = function () {");
                sb.AppendLine(" chart = new AmCharts.AmPieChart();");
                sb.AppendLine(" chart.dataProvider = chartData;");
                sb.AppendLine(" chart.titleField = \"country\";");
                sb.AppendLine(" chart.valueField = \"visits\";");
                sb.AppendLine(" chart.depth3D = 5;");
                sb.AppendLine(" chart.angle = 5;");
                sb.AppendLine(" chart.innerRadius = \"30%\";");
                sb.AppendLine(" chart.sequencedanimation = true;");
                sb.AppendLine(" chart.startDuration = 2;");
                sb.AppendLine(" chart.labelRadius = 30;");
                sb.AppendLine(" chart.colors = [\"#B0DE09\", \"#0D52D1\", \"#FF0F00\"];");
                sb.AppendLine(" chart.write(\"" + DivId + "\");");
                sb.AppendLine("}");
                if ((cs.IsStartupScriptRegistered(DivId) == false))
                {
                    cs.RegisterStartupScript(cstype, DivId, sb.ToString(), true);
                }
                return "";
            }
            catch (Exception ex)
            {
                return "<span>" + ex.Message + "</span>";
            }
        }
    }
}