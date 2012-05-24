<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cita.aspx.cs" Inherits="ESM.Cita" %>

<%@ Register Assembly="Artem.GoogleMap" Namespace="Artem.Web.UI.Controls" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
    <script type="text/javascript" src="https://maps.google.com/maps/api/js?sensor=false"></script>
    <link href="/mastercustom.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .descripcion tr
        {
            height: 40px;
            font-size: 13px;
        }
    </style>
    <script type="text/javascript">

        var map;
        var geocoder;

        function initialize() {
            geocoder = new google.maps.Geocoder();

            var latlng = new google.maps.LatLng(4.6261, -74.076605);
            var myOptions = {
                zoom: 17,
                center: latlng,
                mapTypeId: google.maps.MapTypeId.HYBRID
            };
            map = new google.maps.Map(document.getElementById("map"), myOptions);
        }

        function codeAddress(vadress) {
            var address = "";
            if (vadress == undefined) {
                var address_string = document.getElementById("ban_direccion");
                address = address_string.value;
            }
            else
                address = vadress;
            if (geocoder) {
                geocoder.geocode({ 'address': address }, function (results, status) {
                    if (status == google.maps.GeocoderStatus.OK) {
                        map.setCenter(results[0].geometry.location);
                        //                        switch (results[0].geometry.location_type) {
                        //                            case 'ROOFTOP':
                        //                                $("#divMensajeGeo").css("color", "green").html("Ubicación exacta");
                        //                                break;
                        //                            default:
                        //                                $("#divMensajeGeo").css("color", "red").html("Ubicación aproximada");
                        //                                break;
                        //                        }
                        var marker = new google.maps.Marker({
                            map: map,
                            position: results[0].geometry.location
                        });
                    } else {
                        alert("No fue posible la reubicación por la siguiente causa \n: " + status);
                    }
                });
            }
        }
    </script>
</head>
<body style="background: #ffffff;" onload="initialize(); codeAddress();">
    <form id="form1" runat="server">
    <div style="width: 100%;">
        <table class="descripcion" border="0" cellpadding="0" cellspacing="0" style="-moz-border-radius: 4px;
            -webkit-border-radius: 4px; border-radius: 4px; /*ie 7 and 8 do not support border radius*/
-moz-box-shadow: 0px 0px 5px #ababab; -webkit-box-shadow: 0px 0px 5px #ababab; box-shadow: 0px 0px 5px #ababab;
            /*ie 7 and 8 do not support blur property of shadows*/
 margin: 0 auto; width: 100%;">
            <tr class="trheader" style="text-align: center;">
                <td colspan="2">
                    <h3>
                        <img src="Icons/google-maps-icon.gif" alt="cita-icon" />
                        Descripción de la Cita
                    </h3>
                </td>
            </tr>
            <tr class="trgris">
                <td>
                    Nombre:
                </td>
                <td>
                    <asp:Label Text="" ID="lblNombre" runat="server" />
                </td>
            </tr>
            <tr class="trblanca">
                <td>
                    Teléfono:
                </td>
                <td>
                    <asp:Label Text="" runat="server" ID="lblTelefono" />
                </td>
            </tr>
            <tr class="trgris">
                <td>
                    Dirección
                </td>
                <td>
                    <asp:Label Text="" runat="server" ID="lblDirección" />
                </td>
            </tr>
            <tr class="trblanca">
                <td>
                    Departamento/Municipio:
                </td>
                <td>
                    <asp:Label Text="" ID="lblmunicipio" runat="server" />
                </td>
            </tr>
            <%--<tr class="trgris">--%>
            <%--<td colspan="2" style="">
                    <cc1:GoogleMap ID="GoogleMap1" DefaultMapView="Hybrid" runat="server" Width="100%"
                        Height="300px" Key="ABQIAAAAVNRP5Todd4lQRsBU4IdOBBRJqE_INvko5YLG01IOLFEiGeKckBRz9h3r9X2aLaKXGloK0GJFkq7f-A"
                        Zoom="17">
                    </cc1:GoogleMap>
                </td>--%>
            <%--</tr>--%>
            <tr class="trgris">
                <td colspan="2">
                    <div id="map" style="width: 100%; height: 400px;">
                    </div>
                </td>
                <td>
                </td>
            </tr>
        </table>
        <%--ABQIAAAAVNRP5Todd4lQRsBU4IdOBBS0YSEoQWZ4vBhDgqen0k5fEl4mFhSdWdSfFKifdG6QgygYJeL8Wn-MHw--%>
    </div>
    <input type="hidden" runat="server" name="ban_direccion" value="" id="ban_direccion" />
    </form>
</body>
</html>
