<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ClientesDetail.aspx.vb"
    Inherits="RobotNet.ClientDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            font-size: large;
        }
        .style2
        {
            width: 100%;
        }
        .style4
        {
            width: 267px;
        }
        .style5
        {
            width: 149px;
        }
        .style7
        {
            width: 267px;
            height: 21px;
        }
        .style10
        {
            width: 83px;
        }
        .style11
        {
            height: 21px;
            width: 83px;
        }
        #text1
        {
            width: 68px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p class="style1">
        <strong>Ficha de Cliente</strong></p>
    <table class="style2">
        <tr>
            <td class="style10">
                Código
            </td>
            <td class="style4">
                <asp:TextBox ID="TextBox1" runat="server" Width="60px" AutoPostBack="True"></asp:TextBox>
            </td>
            <td class="style5">
                Plafond
            </td>
            <td>
                <asp:TextBox ID="sPlafond" runat="server" Style="text-align: right;" Width="80px">0.00</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style10">
                Nome
            </td>
            <td class="style4">
                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="style5">
                Plafond Interno
            </td>
            <td>
                <asp:TextBox ID="sPlafInt" runat="server" Style="text-align: right;" Width="80px">0.00</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style10">
                Endereço
            </td>
            <td class="style4">
                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="style5" align="left" style="font-weight: bold; color: #FF0000;">
                Total
            </td>
            <td width="80">
                <asp:TextBox ID="sPlafTot" runat="server" Style="text-align: right;" Width="80px" Font-Bold="True" ForeColor="Red">0.00</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style10">
                &nbsp;
            </td>
            <td class="style4">
                <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="style5">
                Saldo Corrente
            </td>
            <td>
                <asp:TextBox ID="sSaldoCorr" runat="server" Style="text-align: right;" Width="80px">0.00</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style11">
                Local
            </td>
            <td class="style7">
                <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="style5">
                Saldo Atrazado
            </td>
            <td>
                <asp:TextBox ID="sSaldoAtrz" runat="server" Style="text-align: right;" Width="80px">0.00</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style10">
                Cód Postal
            </td>
            <td class="style4">
                <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="style5">
                Guias por Facturar
            </td>
            <td>
                <asp:TextBox ID="sGuiasPorFact" runat="server" Style="text-align: right;" Width="80px">0.00</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style10">
                País
            </td>
            <td class="style4">
                <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="style5">
                Letras
            </td>
            <td>
                <asp:TextBox ID="sLetras" runat="server" Style="text-align: right;" Width="80px">0.00</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style10">
                Vendedor
            </td>
            <td class="style4">
                <asp:Label ID="Label12" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="style5" style="font-weight: bold; color: #FF0000">
                Crédito Disponível
            </td>
            <td>
                <asp:TextBox ID="sCredDisp" runat="server" Style="text-align: right;" Width="80px" Font-Bold="True" ForeColor="Red" Height="22px">0.00</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style5">
                Nº Contribuinte
            </td>
            <td>
                <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="style5">
                Encomendas Pendentes
            </td>
            <td>
                <asp:TextBox ID="sEncPend" runat="server" Style="text-align: right;" Width="80px">0.00</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style5">
                Telefone
            </td>
            <td>
                <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="style5">
                Ultimo Ano
            </td>
            <td>
                <asp:TextBox ID="sUltAno" runat="server" Style="text-align: right;" Width="80px">0.00</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style5">
                Fax
            </td>
            <td>
                <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="style5">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style5">
                Email
            </td>
            <td>
                <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="style5">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label13" runat="server" Text="1.2" Style="font-size: small" Width="200px"></asp:Label>
            </td>
        </tr>
    </table>
    <p class="style1">
        <asp:Button ID="PrintFicha" runat="server" Text="Imprime Ficha de Cliente" Height="40px" Width="180px" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnDescontos" runat="server" Text="Descontos" Height="40px" Width="140px" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnMapa" runat="server" Text="Mapa" Height="40px" Width="140px" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnPrev" runat="server" Text="&lt;" Height="40px" Width="60px" OnClientClick="Button()" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnNext" runat="server" Text="&gt;" Height="40px" Width="60px" OnClientClick="Button()" UseSubmitBehavior="False" />
        &nbsp;&nbsp;&nbsp;
        <input type="text" id="text1" name="text1" size="5" />&nbsp;&nbsp;
    </p>

      <script type="text/javascript">
          function Button() {
              var y = document.getElementById('<%= TextBox1.ClientID %>').value
              document.getElementById('text1').value = y
          }
    </script>

</asp:Content>
