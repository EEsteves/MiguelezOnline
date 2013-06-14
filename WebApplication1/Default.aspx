<%@ Page Title="Home Page" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="Default.aspx.vb" Inherits="RobotNet._Default" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
    .style1
    {
        font-size: x-small;
    }
        .style2
        {
            font-size: medium;
        }
        .style3
        {
            font-size: x-large;
        }
        .style7
        {
            font-size: large;
            width: 1045px;
            height: 10px;
        }
        .style9
        {
            color: #000099;
            font-size: x-large;
        }
        </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <p>
        <table style="height: 40px">
            <tr>
                <td class="style7">
                    <span class="style9">Benvindo ao sistema de consulta da Miguélez, Lda.</span></td>
            </tr>
        </table>
    </p>
    <p>
        Para saber mais acerca do Robot Dreams Software visite <a href="http://www.robotdreams.ca">www.robotdreams.ca</a>&nbsp;&nbsp;&nbsp;
    </p>
    <p>
        <img alt="" src="Imagens/LogoMiguelez.jpg" style="height: 66px; width: 196px" />&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="mMsg" runat="server" BorderStyle="None" Font-Bold="True" ForeColor="Red" ReadOnly="True" 
                        style="margin-bottom: 0px; font-size: large; text-align: right;" Width="481px">Por favor faça login</asp:TextBox>
    </p>
    <p>
        Versão 1.0 (Desenvolvimento)</p>
    <p class="style1">
        Robot Dreams Software - all rights reserved 2012&nbsp;&nbsp;
    </p>
    <p class="style1">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagens/under_construction.png" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </p>
    <p class="style1">
        &nbsp; <span class="style2"><strong>Site em desenvolvimento / Under Construction<asp:TextBox ID="UserId" runat="server" 
            Visible="False" Width="62px" style="text-align: left"></asp:TextBox>
        </strong></span></p>
    <p class="style1">
        &nbsp;</p>
    <p class="style1">
        &nbsp;</p>
    <p class="style1">
        &nbsp;</p>

    <!-- Control Definition -->
    
</asp:Content>
