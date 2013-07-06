<%@ Page Title="Login" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="Login.aspx.vb" Inherits="RobotNet.Login" %>

<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1Menu"
    TagPrefix="wijmo" %>
<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1ComboBox"
    TagPrefix="wijmo" %>
<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1Input"
    TagPrefix="wijmo" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style2
        {
            font-size: large;
            width: 853px;
        }
        .style4
        {
            font-size: large;
        }
        .style5
        {
            font-size: large;
            width: 400px;
        }
        .style6
        {
            width: 400px;
        }
        .style7
        {
            font-size: large;
            width: 160px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Log In
    </h2>
    <p>
        &nbsp;</p>
    <table class="style2">
        <tr>
            <td class="style4" colspan="2">
                Por Favor, entre o seu código de Operador e Palavra Passe
            </td>
        </tr>
        <tr>
            <td class="style7">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style7">
                <div id="divMessage" runat="server" visible="false">
                    <div class="robotnet-err-all">
                        <asp:Label runat="server" ID="lblInvalidLogin" />
                    </div>
                    <div>
                        &nbsp;
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td class="style7">
                Utilizador
            </td>
            <td class="style5">
                &nbsp;<wijmo:C1ComboBox ID="C1ComboBox1" runat="server" Width="300px">
                    <ShowingAnimation Option="">
                    </ShowingAnimation>
                    <HidingAnimation Option="">
                    </HidingAnimation>
                    <Items>
                        <wijmo:C1ComboBoxItem runat="server" Text="01 Luis Branquinho" />
                        <wijmo:C1ComboBoxItem runat="server" Text="02 Pereira da Silva" />
                        <wijmo:C1ComboBoxItem runat="server" Text="03 José Pais" />
                        <wijmo:C1ComboBoxItem runat="server" Text="04 Fernando Guerreiro" />
                    </Items>
                </wijmo:C1ComboBox>
            </td>
        </tr>
        <tr>
            <td class="style7">
                Palavra Passe
            </td>
            <td class="style5">
                &nbsp;<wijmo:C1InputMask ID="sPassword" runat="server" PromptChar=" " PasswordChar="*" Mask="AAAA">
                </wijmo:C1InputMask>
            </td>
        </tr>
        <tr>
            <td class="style7">
                &nbsp;
            </td>
            <td class="style6">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style7">
                &nbsp;
            </td>
            <td class="style6">
                &nbsp;<asp:Button ID="Button1" runat="server" Text="Login" Height="38px" Width="83px" />
            </td>
        </tr>
        <tr>
            <td class="style7">
                &nbsp;
            </td>
            <td class="style6">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style7">
                &nbsp;
            </td>
            <td class="style6">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
