<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="Encomendas.aspx.vb" Inherits="RobotNet.Encomendas" %>

<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1Menu"
    TagPrefix="wijmo" %>
<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1GridView"
    TagPrefix="wijmo1" %>
<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1Input"
    TagPrefix="wijmo2" %>
<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1ComboBox"
    TagPrefix="wijmo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style6
        {
            width: 15px;
            height: 26px;
        }
        .style8
        {
            height: 26px;
            width: 370px;
        }
        .style7
        {
            height: 26px;
            width: 150px;
        }
        .style2
        {
            width: 100%;
        }
        .style9
        {
            font-size: x-large;
        }
        .style10
        {
            width: 6px;
        }
        .style11
        {
            top: 0px;
            left: 0px;
        }
        .style12
        {
            width: 5px;
        }
        .style13
        {
            height: 35px;
            width: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdateProgress ID="progressBar" runat="server" AssociatedUpdatePanelID="uplMainPanel"
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="overlay">
                <div class="ajaxloader">
                    <img src="Imagens/ajax-loader.gif" style="vertical-align: middle" alt="Processing" />Processing....
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="uplMainPanel" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <table class="style2">
                <tr>
                    <td class="style6">
                        <span class="style9"><strong>Encomendas</strong></span>
                    </td>
                    <td class="style6">
                        Situações<wijmo:C1ComboBox ID="ddlStatus" runat="server" Width="140px" AutoPostBack="True">
                        </wijmo:C1ComboBox>
                    </td>
                    <td class="style8">
                        Cliente<wijmo:C1ComboBox ID="ddlCliente" runat="server" Width="400px" AutoPostBack="True">
                        </wijmo:C1ComboBox>
                    </td>
                    <td class="style13">
                        <input type="text" id="text1" name="text1" size="5" class="style12" style="display:none" />
                        <input type="text" id="text2" name="text2" size="5" class="style10" style="display:none" />
                    </td>
                    <td class="style7">
                        Período<wijmo:C1ComboBox ID="ddlDataPeriod" runat="server" Width="140px" AutoPostBack="True">
                        </wijmo:C1ComboBox>
                    </td>
                </tr>
            </table>
            <wijmo:C1Menu ID="C1Menu1" runat="server" CrumbDefaultText="CrumbDefaultDafaultText"
                AutoPostBack="True">
                <Animation Option="">
                </Animation>
                <ShowAnimation Option="">
                </ShowAnimation>
                <HideAnimation Option="">
                    <Animated Effect="fade"></Animated>
                </HideAnimation>
                <Items>
                    <wijmo:C1MenuItem ID="C1MenuItem1" runat="server" Text="Visualiza Encomenda">
                    </wijmo:C1MenuItem>
                    <wijmo:C1MenuItem ID="C1MenuItem2" runat="server" Text="Estatísticas">
                    </wijmo:C1MenuItem>
                    <wijmo:C1MenuItem ID="C1MenuItem3" runat="server" Text="Imprime Lista de Encomendas">
                    </wijmo:C1MenuItem>
                    <wijmo:C1MenuItem ID="C1MenuItem4" runat="server" Text="Imprime uma Encomenda">
                    </wijmo:C1MenuItem>
                </Items>
            </wijmo:C1Menu>
            <wijmo1:c1gridview id="C1Encomendas" runat="server" clientselectionmode="SingleRow"
                onclientselectionchanged="xSelectionChanged" allowpaging="True" allowsorting="True"
                autogeneratecolumns="False" pagesize="15" cssclass="style11">
        <PagerSettings Mode="NextPreviousFirstLast" />
        <Columns>
            <wijmo1:C1BoundField DataField="E_CLIENT" HeaderText="Cliente" Width="80px" SortExpression="E_CLIENT">
            </wijmo1:C1BoundField>
            <wijmo1:C1BoundField DataField="E_NAME" HeaderText="Nome" SortExpression="E_NAME">
            </wijmo1:C1BoundField>
            <wijmo1:C1BoundField DataField="E_NUMBER" HeaderText="Numero" SortExpression="E_NUMBER">
            </wijmo1:C1BoundField>
            <wijmo1:C1BoundField DataField="E_VEND" HeaderText="Vend">
            </wijmo1:C1BoundField>
            <wijmo1:C1BoundField DataField="X_DATE" HeaderText="Data" SortExpression="X_DATE" DataFormatString="d">
            </wijmo1:C1BoundField>
            <wijmo1:C1BoundField DataField="E_STATUS" HeaderText="Sit">
            </wijmo1:C1BoundField>
            <wijmo1:C1BoundField DataField="E_ENCNUM" HeaderText="Enc Cliente" SortExpression="E_ENCNUM">
            </wijmo1:C1BoundField>
            <wijmo1:C1BoundField DataField="E_TOTAL" HeaderText="Valor">
            </wijmo1:C1BoundField>
        </Columns>
        <AlternatingRowStyle BackColor="#E6F2FF" />
        <SelectedRowStyle BackColor="#99CCFF" ForeColor="Red" />
    </wijmo1:c1gridview>
            <!-- Script para retornar o mRow seleccionado pelo cliente sem ir ao Servidor -->
            <script type="text/javascript">
                function xSelectionChanged() {
                    var mCod = $('#<%= C1Encomendas.ClientID %>').c1gridview("currentCell").row().data[2];
                    var mRow = $('#<%= C1Encomendas.ClientID %>').c1gridview("currentCell").rowIndex();
                    document.getElementById('text1').value = mCod;
                    document.getElementById('text2').value = mRow;
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
