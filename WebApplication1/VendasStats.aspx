<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="VendasStats.aspx.vb" Inherits="RobotNet.VendasStats" %>


    <%@ register assembly="C1.Web.Wijmo.Controls.4" namespace="C1.Web.Wijmo.Controls.C1Chart" tagprefix="wijmo" %>
    <%@ register assembly="C1.Web.Wijmo.Extenders.4" namespace="C1.Web.Wijmo.Extenders.C1Chart" tagprefix="wijmo" %>
    <%@ register assembly="C1.Web.Wijmo.Controls.4" namespace="C1.Web.Wijmo.Controls.C1Menu" tagprefix="wijmo" %>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 93%;
            border: 0px solid #000000;
            height: 40px;
        }
        .style3
        {
            font-weight: 700;
            font-size: large;
        }
        .style4
        {
            font-weight: 700;
            font-size: xx-large;
            color: #FFFFFF;
            height: 250px;
        }
        .style5
        {
            height: 268px;
            width: 250px;
        }
        .style6
        {
            width: 268px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <table class="style1">
        <tr>
            <td class="style3" colspan="2">
                &nbsp;
                <asp:TextBox ID="TextBox1" runat="server" Width="60px" AutoPostBack="True"></asp:TextBox>
            </td>
            <td colspan="2" class="style6">
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style4" colspan="2">
                <asp:Table ID="Table1" runat="server" Style="font-size: small">
                </asp:Table>
            </td>
            <td class="style5" colspan="2">
                &nbsp;
                <wijmo:C1BarChart ID="C1Chart" runat="server" Horizontal="False" Width="700px" Height="350px">
                    <Animation Easing="EaseOutBounce" />
                    <Header Compass="North">
                    </Header>
                    <Footer Compass="South" Visible="False">
                    </Footer>
                    <Legend Visible="False"></Legend>
                    <Axis>
                        <X>
                            <GridMajor Visible="True">
                            </GridMajor>
                            <GridMinor Visible="False">
                            </GridMinor>
                        </X>
                        <Y Visible="False" Compass="West" AutoMin="False" AutoMinor="False">
                            <Labels TextAlign="Center">
                            </Labels>
                            <GridMajor Visible="True">
                            </GridMajor>
                            <GridMinor Visible="False">
                            </GridMinor>
                        </Y>
                    </Axis>
                </wijmo:C1BarChart>
            </td>
        </tr>
    </table>
    <p class="style1">
        <asp:Button ID="btnFicha" runat="server" Text="Ficha de Cliente" Height="40px" Width="140px" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnDescontos" runat="server" Text="Descontos" Height="40px" Width="140px" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnMapa" runat="server" Text="Mapa" Height="40px" Width="140px" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <input type="text" id="text1" name="text1" size="5" /></p>
    <script type="text/javascript">
        function Button() {
            var y = document.getElementById('<%= TextBox1.ClientID %>').value
            document.getElementById('text1').value = y
        }
    </script>
</asp:Content>
