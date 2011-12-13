<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportMarcoLogico.aspx.cs"
    Inherits="ESM.ReportMarcoLogico" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="mastercustom.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            background: #fff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <img src="/Icons/plan_operativo.png" width="48px" alt="Evaluacion" />
            </td>
            <td style="vertical-align: middle; font-size: 13px; text-align: left;">
                <h1 style="color: #0b72bc;">
                    Línea Base Competencias Ciudadanas</h1>
            </td>
            <td>
                <asp:RadioButton ID="rbtnresumen" runat="server" AutoPostBack="True" GroupName="groupreport"
                    OnCheckedChanged="rbtnresumen_CheckedChanged" Text="Marco Lógico" 
                    Visible="False" />
                <asp:RadioButton ID="rbtndetalle" runat="server" AutoPostBack="True" GroupName="groupreport"
                    OnCheckedChanged="rbtndetalle_CheckedChanged" Text="Plan Operativo" 
                    Visible="False" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton Text="Export to Excel" runat="server" ID="lknExport" OnClick="lknExport_Click" />
            </td>
        </tr>
    </table>
    <div style="width: 95%; margin: 0 auto; border: 1px solid #ccc;">
        <table id="tbFinalidad" runat="server" border="1" cellpadding="0" cellspacing="0"
            width="100%">
            <tr class="trheader">
                <td width="50%">
                    <b>Descripción</b>
                </td>
                <td width="10%">
                    &nbsp;
                </td>
                <td width="20%">
                    &nbsp;
                </td>
                <td width="20%">
                    &nbsp;
                </td>
            </tr>
            <tr class="trgris">
                <td>
                    Finalidad
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr class="trblanca">
                <td>
                    <asp:Label Text="Finalidad" runat="server" ID="lblfinalidad" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:GridView runat="server" ID="gvproposito" AutoGenerateColumns="False" DataSourceID="sqlMarco_logico_Proposito"
            Width="100%">
            <AlternatingRowStyle CssClass="trblanca" />
            <Columns>
                <asp:BoundField DataField="Proposito" HeaderText="Proposito" SortExpression="Proposito">
                    <HeaderStyle Width="50%" />
                </asp:BoundField>
                <asp:BoundField DataField="Indicador" HeaderText="Indicador" SortExpression="Indicador">
                    <HeaderStyle Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="Medios_de_Verificacion" HeaderText="Medios de Verificacion"
                    ReadOnly="True" SortExpression="Medios_de_Verificacion">
                    <HeaderStyle Width="20%" />
                </asp:BoundField>
                <asp:BoundField DataField="Supuestos" HeaderText="Supuestos" ReadOnly="True" SortExpression="Supuestos">
                    <HeaderStyle Width="20%" />
                </asp:BoundField>
            </Columns>
            <HeaderStyle CssClass="trheader" />
            <RowStyle CssClass="trgris" />
        </asp:GridView>
        <asp:SqlDataSource ID="sqlMarco_logico_Proposito" runat="server" ConnectionString="<%$ ConnectionStrings:esmConnectionString2 %>"
            SelectCommand="SELECT [Proposito], [Indicador], [Medios de Verificacion] AS Medios_de_Verificacion, [Supuestos] FROM [Report_Marco_Logico_Propositos] WHERE ([Id] = @Id)">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="0" Name="Id" QueryStringField="idproyecto"
                    Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:GridView ID="gvresultados" runat="server" AutoGenerateColumns="False" DataSourceID="sqlReportMarcoLogico"
            Width="100%" Visible="False">
            <AlternatingRowStyle CssClass="trblanca" />
            <Columns>
                <asp:BoundField DataField="Resultado" HeaderText="Resultados" SortExpression="Resultado">
                    <HeaderStyle Width="50%" />
                </asp:BoundField>
                <asp:BoundField DataField="Indicador_Resultado" HeaderText="Indicadores" SortExpression="Indicador_Resultado">
                    <HeaderStyle Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="Medios_de_Verificacion" HeaderText="Medios de Verificación"
                    ReadOnly="True" SortExpression="Medios_de_Verificacion">
                    <HeaderStyle CssClass="20%" />
                </asp:BoundField>
                <asp:BoundField DataField="Supuestos" HeaderText="Supuestos" ReadOnly="True" SortExpression="Supuestos">
                    <HeaderStyle CssClass="20%" />
                    <ItemStyle Width="20%" />
                </asp:BoundField>
            </Columns>
            <HeaderStyle CssClass="trheader" />
            <RowStyle CssClass="trgris" />
        </asp:GridView>
        <asp:SqlDataSource ID="sqlReportMarcoLogico" runat="server" ConnectionString="<%$ ConnectionStrings:esmConnectionString2 %>"
            SelectCommand="SELECT [Resultado], [Indicador_Resultado], [Medios de Verificacion] AS Medios_de_Verificacion, [Supuestos] FROM [Report_Macro_Logico] WHERE ([Proyecto_id] = @Proyecto_id)">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="0" Name="Proyecto_id" QueryStringField="idproyecto"
                    Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:GridView ID="gvDetalleActividades" runat="server" AutoGenerateColumns="False"
            DataSourceID="sqlreportfull" Visible="False" Width="100%">
            <AlternatingRowStyle CssClass="trblanca" />
            <Columns>
                <asp:BoundField DataField="Actividad" HeaderText="Actividad" SortExpression="Actividad">
                    <ControlStyle Width="30%" />
                </asp:BoundField>
                <asp:BoundField DataField="Presupuesto" HeaderText="Presupuesto" SortExpression="Presupuesto">
                    <ControlStyle Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="Medios_de_Verificacion" HeaderText="Medios de Verificación"
                    ReadOnly="True" SortExpression="Medios_de_Verificacion">
                    <ControlStyle Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="Indicadores" HeaderText="Indicadores" ReadOnly="True"
                    SortExpression="Indicadores">
                    <ControlStyle Width="20%" />
                </asp:BoundField>
                <asp:BoundField DataField="Resultado" HeaderText="Resultado" SortExpression="Resultado">
                    <ControlStyle Width="0px" />
                </asp:BoundField>
            </Columns>
            <HeaderStyle CssClass="trheader" />
            <RowStyle CssClass="trgris" />
        </asp:GridView>
        <asp:LinqDataSource ID="lqRepourtFull" runat="server" ContextTypeName="ESM.Model.ESMBDDataContext"
            EntityTypeName="" GroupBy="Resultado_id" OrderGroupsBy="key" Select="new (key as Resultado_id, it as Actividades, Min(Actividad) as Min_Actividad, Min(Indicadores) as Min_Indicadores, Min(Actividades_Medios) as Min_Actividades_Medios, Min(Actividades_Responsables) as Min_Actividades_Responsables, Min(Actividades_Supuestos) as Min_Actividades_Supuestos)"
            TableName="Actividades">
        </asp:LinqDataSource>
        <asp:SqlDataSource ID="sqlreportfull" runat="server" ConnectionString="<%$ ConnectionStrings:esmConnectionString2 %>"
            SelectCommand="SELECT [Actividad], [Presupuesto], [Medios de Verificacion] AS Medios_de_Verificacion, [Indicadores], [Resultado] FROM [Report_Full_Actividades] WHERE ([proyecto_id] = @proyecto_id)">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="0" Name="proyecto_id" SessionField="idproyecto"
                    Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
