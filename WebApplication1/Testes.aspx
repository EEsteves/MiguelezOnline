<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Testes.aspx.vb" Inherits="RobotNet.Testes" %>
<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1Dialog" TagPrefix="wijmo" %>
<%@ Register assembly="C1.Web.Wijmo.Controls.4" namespace="C1.Web.Wijmo.Controls.C1GridView" tagprefix="wijmo" %>
<%@ Register assembly="C1.Web.Wijmo.Extenders.4" namespace="C1.Web.Wijmo.Extenders.C1DataSource" tagprefix="wijmo" %>
<%@ Register assembly="EO.Web" namespace="EO.Web" tagprefix="eo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style6
        {
            width: 100%;
        }
        .style7
        {
            font-size: x-large;
        }
        .style8
        {
            width: 710px;
        }
        .style9
        {
            width: 710px;
            height: 129px;
        }
        .style11
        {
            width: 710px;
            height: 2px;
        }
        .style12
        {
            width: 499px;
            height: 129px;
        }
        .style13
        {
            width: 499px;
            height: 2px;
        }
        .style14
        {
            width: 499px;
        }
        #text1
        {
            width: 117px;
        }
    </style>
     </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <span class="style7">Testes Diversos</span><br />
      <!-- Then use phone links to explicitly create a link. --> 
     <p>A phone number: <a href="tel:1-408-555-5555">1-408-555-5555</a></p> 
     <!-- Otherwise, numbers that look like phone numbers are not links. --> 
     <p>Not a phone number: 408-555-5555</p> 
    <table class="style6">
        <tr>
            <td class="style9">
                &nbsp;
                <wijmo:C1GridView ID="C1Clientes" runat="server" AutogenerateColumns="False" Height="267px" ScrollMode="Vertical" 
                    style="top: 0px; left: 1px; margin-right: 0px" 
                    ClientSelectionMode="SingleRow" onclientselectionchanged="xSelChanged">
                    <callbacksettings action="Selection" />
                    <Columns>
                        <wijmo:C1BoundField DataField="C_CLIENT" HeaderText="Cód" Width="70px" ReadOnly="True">
                        </wijmo:C1BoundField>
                        <wijmo:C1BoundField DataField="C_NAME" HeaderText="Nome" Width="300px" AllowMoving="False" ReadOnly="True">
                        </wijmo:C1BoundField>
                        <wijmo:C1BoundField DataField="C_PHONE1" HeaderText="Telefone">
                        </wijmo:C1BoundField>
                    </Columns>
                </wijmo:C1GridView>
            </td>
            <td class="style12">
                <wijmo:C1Dialog ID="C1Dialog1" runat="server" MaintainVisibilityOnPostback="False" MinHeight="100" MinWidth="100"
                    Resizable="False" Show="blind" Title="RobotNet Diálogo" Visible="False" Height="100px" ToolTip="Isto é um teste" 
                    Width="220px" CloseText="Closing ...">
                    <CollapsingAnimation Option="">
                    </CollapsingAnimation>
                    <ExpandingAnimation Option="">
                    </ExpandingAnimation>
                    <CaptionButtons>
                        <Pin IconClassOn="ui-icon-pin-w" IconClassOff="ui-icon-pin-s" Visible="False"></Pin>
                        <Refresh IconClassOn="ui-icon-refresh" Visible="False"></Refresh>
                        <Toggle Visible="False" />
                        <Minimize IconClassOn="ui-icon-minus" Visible="False"></Minimize>
                        <Maximize IconClassOn="ui-icon-extlink" Visible="False"></Maximize>
                        <Close IconClassOn="ui-icon-close"></Close>
                    </CaptionButtons>
                    <Content>
                        Teste de dialogo do Wijmo
                    </Content>
                </wijmo:C1Dialog>
            </td>
        </tr>
        <tr>
            <td class="style11">
                &nbsp; &nbsp;&nbsp;<input type="text" id="text1" name="text1" size="5" />&nbsp;
            </td>
            <td class="style13">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style8">
                &nbsp;
            </td>
            <td class="style14">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style8">
                &nbsp;
            </td>
            <td class="style14">
                &nbsp;
                <asp:ImageButton ID="btn2" runat="server" ImageUrl="~/Imagens/2.jpg" Width="48px" />
                <asp:ImageButton ID="btn3" runat="server" ImageUrl="~/Imagens/3.jpg" Width="48px" />
                <asp:ImageButton ID="btn4" runat="server" ImageUrl="~/Imagens/4.jpg" Width="48px" />
            </td>
        </tr>
    </table>
    <asp:Button ID="Button1" runat="server" Text="Message Test" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button2" runat="server" Text="Wijmo Dialog" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button3" runat="server" Text="Botão 3" OnClientClick="Button3()" />
    <script type="text/javascript">
        function Button3() {
            alert("Button 3 pressed - vai fazer teste")
        }
        function xSelChanged() {
            var mSelectedCells = $('#<%= C1Clientes.ClientID %>').c1gridview("selection").selectedCells();
            var mCol0 = mSelectedCells.item(0).value();
            var mCol1 = mSelectedCells.item(1).value();
            var mCol2 = mSelectedCells.item(2).value();
            document.getElementById('text1').value = mCol0;

//            var mRow = $('#<%= C1Clientes.ClientID %>').c1gridview("currentCell").rowIndex();
//            var mCod = $('#<%= C1Clientes.ClientID %>').c1gridview("currentCell").row().data[0];
//            var mNam = $('#<%= C1Clientes.ClientID %>').c1gridview("currentCell").row().data[1];
//            var mTel = $('#<%= C1Clientes.ClientID %>').c1gridview("currentCell").row().data[2];
//            document.getElementById('text1').value = mCod;
//            if (mRow != 0)
//               __doPostBack('#<%= C1Clientes.ClientID %>', mRow);
        }
    </script>
    </asp:Content>
