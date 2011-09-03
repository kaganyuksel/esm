<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeBehind="Insert.aspx.cs"
    Inherits="ESM.Insert" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:DynamicDataManager ID="DynamicDataManager1" runat="server" AutoLoadForeignKeys="true">
        <DataControls>
            <asp:DataControlReference ControlID="FormView1" />
        </DataControls>
    </asp:DynamicDataManager>
    <div style="width: 90%; margin: 0 auto;">
        <br />
        <br />
        <table border="0" cellpadding="0" cellspacing="0" style="margin: 0 auto;">
            <tr>
                <td>
                    <img height="48" src="/Icons/1314641093_plus_48.png" alt="Add" />
                </td>
                <td>
                    <h1 style="margin: 0 auto; color: #005EA7; width: 100%;">
                        Agregar nueva entrada a la tabla
                        <%= table.DisplayName.Replace('_',' ') %></h1>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableClientScript="true"
                    HeaderText="Lista de errores de validación" CssClass="DDValidator" />
                <asp:DynamicValidator runat="server" ID="DetailsViewValidator" ControlToValidate="FormView1"
                    Display="None" CssClass="DDValidator" />
                <asp:FormView runat="server" ID="FormView1" DataSourceID="DetailsDataSource" DefaultMode="Insert"
                    OnItemCommand="FormView1_ItemCommand" OnItemInserted="FormView1_ItemInserted"
                    RenderOuterTable="false">
                    <InsertItemTemplate>
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
                        <table id="detailsTable" class="insert" cellpadding="6">
                            <tr>
                                <td colspan="2" style="text-align: center; font-size: 15px; background: #dddddd;
                                    line-height: 50px;">
                                    <%= table.DisplayName.Replace('_', ' ') %>
                                </td>
                            </tr>
                            <asp:DynamicEntity runat="server" Mode="Insert" />
                            <tr class="td">
                                <td colspan="2">
                                    <asp:LinkButton runat="server" CommandName="Insert" Text="<img src='/Icons/added.png' alt='Insertar' />" />
                                    <asp:LinkButton runat="server" CommandName="Cancel" Text="<img src='/Icons/cancel.png' alt='Cancelar' />"
                                        CausesValidation="false" />
                                </td>
                            </tr>
                        </table>
                    </InsertItemTemplate>
                </asp:FormView>
                <asp:LinqDataSource ID="DetailsDataSource" runat="server" EnableInsert="true" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
