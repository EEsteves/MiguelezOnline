﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="Orcam.aspx.vb" Inherits="RobotNet.Orcam" %>

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
            width: 358px;
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
            top: 0px;
            left: -2px;
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
                        <span class="style9"><strong>Orçamentos</strong></span>
                    </td>
                    <td class="style6">
                        Situações<wijmo:C1ComboBox ID="C1ComboBox1" runat="server" Width="140px">
                        </wijmo:C1ComboBox>
                    </td>
                    <td class="style8">
                        Cliente<wijmo:C1ComboBox ID="C1ComboBox2" runat="server" Width="400px">
                        </wijmo:C1ComboBox>
                    </td>
                    <td class="style7">
                        Data (Ano)<wijmo:C1ComboBox ID="C1ComboBox3" runat="server" Width="140px">
                        </wijmo:C1ComboBox>
                    </td>
                </tr>
            </table>
            <wijmo:C1Menu ID="C1Menu1" runat="server" CrumbDefaultText="CrumbDefaultDafaultText">
                <Animation Option="">
                </Animation>
                <ShowAnimation Option="">
                </ShowAnimation>
                <HideAnimation Option="">
                    <Animated Effect="fade"></Animated>
                </HideAnimation>
                <Items>
                    <wijmo:C1MenuItem ID="C1MenuItem1" runat="server" Text="Visualiza">
                    </wijmo:C1MenuItem>
                    <wijmo:C1MenuItem ID="C1MenuItem2" runat="server" Text="Estatisticas">
                    </wijmo:C1MenuItem>
                    <wijmo:C1MenuItem ID="C1MenuItem3" runat="server" Text="Lista Orçamentos">
                    </wijmo:C1MenuItem>
                    <wijmo:C1MenuItem ID="C1MenuItem4" runat="server" StaticKey="sk3" Text="Imprime Documento">
                    </wijmo:C1MenuItem>
                </Items>
            </wijmo:C1Menu>
            <wijmo1:c1gridview id="C1GridView1" runat="server" clientselectionmode="SingleRow"
                onclientselectionchanged="xSelectionChanged" allowcolmoving="True" allowcolsizing="True"
                allowpaging="True" allowsorting="True" autogeneratecolumns="True" pagesize="15"
                cssclass="style10">
        <PagerSettings PageButtonCount="20" />
        <SelectedRowStyle BackColor="#99CCFF" ForeColor="Red" />
    </wijmo1:c1gridview>
            <p>
                <input type="text" id="text1" name="text1" size="5" /></p>
            <!-- Script para retornar o mRow seleccionado pelo cliente sem ir ao Servidor -->
            <script type="text/javascript">
                function xSelectionChanged() {
                    var selectedCells = $('#<%= C1GridView1.ClientID %>').c1gridview("selection").selectedCells();
                    var i, cellInfo, mCols, mRow;
                    for (i = 0; i < selectedCells.length(); i++) {
                        mCols = selectedCells.length();
                        cellInfo = selectedCells.item(i);
                        mRow = cellInfo.rowIndex();
                        document.getElementById('text1').value = mRow
                    }
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
