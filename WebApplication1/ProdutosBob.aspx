<%@ Page Title="Bobines" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="ProdutosBob.aspx.vb"
    Inherits="RobotNet.ProdutosBob" %>
<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1GridView" TagPrefix="wijmo" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style2
        {
            width: 100%;
        }
        .style10
        {
            width: 107px;
        }
        .style5
        {
        }
        .style11
        {
            height: 21px;
            width: 107px;
        }
        .style7
        {
            width: 332px;
            height: 21px;
        }
        .style12
        {
            width: 332px;
        }
        #text1
        {
            width: 89px;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        bobines 1</h2>
    <table class="style2">
        <tr>
            <td class="style10">
                Código Produto
            </td>
            <td class="style12">
                <asp:TextBox ID="TextBox1" runat="server" Width="116px" AutoPostBack="True"></asp:TextBox>
            </td>
            <td class="style5" rowspan="8">
                <wijmo:C1GridView ID="C1GridView1" runat="server" Style="top: 0px; left: 0px" AllowPaging="True" AutogenerateColumns="True">
                </wijmo:C1GridView>
            </td>
        </tr>
        <tr>
            <td class="style10">
                Nome
            </td>
            <td class="style12">
                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style10">
                Refª Fornecedor
            </td>
            <td class="style12">
                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style10">
                Refª Genérica
            </td>
            <td class="style12">
                <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style11">
                Quant em Stock
            </td>
            <td class="style7">
                <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style10">
                Total em Peças
            </td>
            <td class="style12">
                <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style10">
                Total em Bob
            </td>
            <td class="style12">
                <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style10">
                &nbsp;
            </td>
            <td class="style12">
                &nbsp;
            </td>
        </tr>
    </table>
    <p>
        <asp:Button ID="btnReturn" runat="server" Text="Retorna" Height="40px" Width="113px" UseSubmitBehavior="False" />
        &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnFabrica" runat="server" Text="Stocks Fábrica" Height="40px" Width="114px" UseSubmitBehavior="False" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnPrev" runat="server" Text="&lt;" Height="40px" Width="60px" OnClientClick="Button()" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnNext" runat="server" Text="&gt;" Height="40px" Width="60px" UseSubmitBehavior="False" OnClientClick="Button()" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <input type="text" id="text1" name="text1" size="5" /></p>
    <script type="text/javascript">
        function Button() {
            var y = document.getElementById('<%= TextBox1.ClientID %>').value
            document.getElementById('text1').value = y
        }
    </script>
</asp:Content>
