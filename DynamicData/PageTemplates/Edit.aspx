<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeBehind="Edit.aspx.cs" Inherits="ESM.Edit" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .insert
        {
            margin: 0 auto;
            border: 1px solid #dddddd;
            color: #005EA7;
            font: 13px/13px "Lucida Grande" , "Lucida Sans Unicode" , Helvetica, Arial, Verdana, sans-serif;
            font-weight: none;
            -moz-border-radius: 2px;
            -webkit-border-radius: 2px;
            border-radius: 2px; /*IE 7 AND 8 DO NOT SUPPORT BORDER RADIUS*/
            -moz-box-shadow: 0px 0px 2px #000000;
            -webkit-box-shadow: 0px 0px 2px #000000;
            box-shadow: 0px 0px 2px #000000; /*IE 7 AND 8 DO NOT SUPPORT BLUR PROPERTY OF SHADOWS*/
        }
        .insert tr
        {
            height: 50px;
            -moz-border-radius: 2px;
            -webkit-border-radius: 2px;
            border-radius: 2px; /*IE 7 AND 8 DO NOT SUPPORT BORDER RADIUS*/
        }
        .insert td
        {
            line-height: 50px;
        }
        .insert input[type="text"]
        {
            width: 120px;
            height: 20px;
        }
        .insert a
        {
            color: #005EA7;
            font: 13px/13px "Lucida Grande" , "Lucida Sans Unicode" , Helvetica, Arial, Verdana, sans-serif;
            font-weight: none;
            text-decoration: none;
        }
        
        .insert textarea
        {
            height: 50px;
        }
        .insert select
        {
            max-width: 500px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:DynamicDataManager ID="DynamicDataManager1" runat="server" AutoLoadForeignKeys="true">
        <DataControls>
            <asp:DataControlReference ControlID="FormView1" />
        </DataControls>
    </asp:DynamicDataManager>
    <h2 class="DDSubHeader">
        Editar entrada de la tabla
        <%= table.DisplayName.Replace('_',' ') %></h2>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableClientScript="true"
                HeaderText="Lista de errores de validación" CssClass="DDValidator" />
            <asp:DynamicValidator runat="server" ID="DetailsViewValidator" ControlToValidate="FormView1"
                Display="None" CssClass="DDValidator" />
            <asp:FormView runat="server" ID="FormView1" DataSourceID="DetailsDataSource" DefaultMode="Edit"
                OnItemCommand="FormView1_ItemCommand" OnItemUpdated="FormView1_ItemUpdated" RenderOuterTable="false">
                <EditItemTemplate>
                    <table id="detailsTable" class="insert" cellpadding="6">
                        <asp:DynamicEntity runat="server" Mode="Edit" />
                        <tr class="td">
                            <td colspan="2">
                                <asp:LinkButton runat="server" CommandName="Update" Text="Actualizar" />
                                <asp:LinkButton runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                </EditItemTemplate>
                <EmptyDataTemplate>
                    <div class="DDNoItem">
                        No existe tal elemento.</div>
                </EmptyDataTemplate>
            </asp:FormView>
            <asp:LinqDataSource ID="DetailsDataSource" runat="server" EnableUpdate="true" />
            <asp:QueryExtender TargetControlID="DetailsDataSource" ID="DetailsQueryExtender"
                runat="server">
                <asp:DynamicRouteExpression />
            </asp:QueryExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
