<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="Produtos.aspx.vb" Inherits="RobotNet.Produtos" %>

<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1ComboBox"
    TagPrefix="wijmo" %>
<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1Menu"
    TagPrefix="wijmo" %>
<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1Input"
    TagPrefix="wijmo" %>
<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1GridView"
    TagPrefix="wijmo1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style2
        {
            width: 80px;
            height: 95px;
        }
        .style3
        {
            width: 340px;
            height: 270px;
            font-size: small;
        }
        .style4
        {
            width: 570px;
            height: 270px;
            font-size: small;
        }
        #text1
        {
            width: 100px;
        }
        .style7
        {
            font-size: medium;
        }
        .style8
        {
            font-size: large;
        }
        .style9
        {
            width: 900px;
            height: 330px;
        }
        .style10
        {
            width: 238px;
            height: 95px;
        }
        .style11
        {
            width: 246px;
            height: 95px;
        }
        .style12
        {
            color: #000099;
        }
        #text2
        {
            width: 263px;
        }
        #text3
        {
            width: 50px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
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
            <table width="910px;">
                <tr>
                    <td colspan="2">
                        <span class="style8">&nbsp;Stocks&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><wijmo:C1ComboBox
                            ID="C1ComboBox1" runat="server" AutoPostBack="True" MinLength="1" Width="200px"
                            Delay="0" Style="font-size: small" AutoFilter="False" DropdownHeight="350" ForceSelectionText="True"
                            SelectElementWidthFix="12">
                        </wijmo:C1ComboBox>
                        <input type="text" id="text3" name="text3" size="5" hidden="hidden" /><input type="text"
                            id="text1" name="text1" size="5" hidden="hidden" />
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="left">
                        <div class="rounded_corners" style="width: 580px; height: 500px; padding-top: 10px;"
                            align="left">
                            <wijmo1:c1gridview id="C1Prods" runat="server" style="top: 0px; left: 1px; font-size: small;"
                                autogeneratecolumns="False" height="290px" pagesize="20" allowkeyboardnavigation="True"
                                clientselectionmode="SingleRow" allowautosort="False" onclientselectionchanged="xProductSelectionChanged"
                                width="570px" scrollmode="Vertical">
                        <Columns>
                            <wijmo1:C1BoundField DataField="Cod" Width="100px" HeaderText="Cod">
                            </wijmo1:C1BoundField>
                            <wijmo1:C1BoundField DataField="Nome" Width="300px" HeaderText="Produto">
                            </wijmo1:C1BoundField>
                            <wijmo1:C1BoundField DataField="Quant" HeaderText="Quant" Width="100px" DataFormatString="n0">
                            </wijmo1:C1BoundField>
                            <wijmo1:C1BoundField DataField="Gen" HeaderText="Ref" Width="80px">
                            </wijmo1:C1BoundField>
                        </Columns>
                    </wijmo1:c1gridview>
                            <br />
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="Produto" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="sSelectedProduct" runat="server" Width="111px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Nome" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="sSelectedName" runat="server" Width="227px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="Total em Portugal (n/Resv)" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox2" runat="server" Width="65px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="Total na Fábrica" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox3" runat="server" Width="65px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text="Total Combinado" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox4" runat="server" Width="65px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnStocks" runat="server" Height="32px" Text="Stocks" Width="84px"
                                            Style="text-align: center" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td valign="top" align="right">
                        <table width="300px;" height="245;" border="0px" class="rounded_corners">
                            <tr>
                                <th style="background-color: #4b6c9e; height: 30px; color: White;">
                                    Portugal
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <wijmo1:c1gridview id="C1Stocks" runat="server" style="top: 0px; left: 3px; font-size: small;"
                                        autogeneratecolumns="False" height="180px" width="340px" scrollmode="Vertical">
                            <Columns>
                                <wijmo1:C1BoundField DataField="Tipo" Width="80px" HeaderText="Embala">
                                </wijmo1:C1BoundField>
                                <wijmo1:C1BoundField DataField="Matrícula" Width="90px" HeaderText="Matricula">
                                </wijmo1:C1BoundField>
                                <wijmo1:C1BoundField DataField="Res" Width="80px" HeaderText="Reserva">
                                </wijmo1:C1BoundField>
                                <wijmo1:C1BoundField DataField="Quant" Width="70px" HeaderText="Quant" DataFormatString="n">
                                </wijmo1:C1BoundField>
                            </Columns>
                        </wijmo1:c1gridview>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table width="300px;" height="248;" border="0px" class="rounded_corners">
                            <tr>
                                <th style="background-color: #4b6c9e; height: 30px; color: White;">
                                    Fábrica (Leon)
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <wijmo1:c1gridview id="C1Fabrica" runat="server" style="top: 0px; left: 3px; font-size: small;"
                                        autogeneratecolumns="False" height="180px" width="340px" scrollmode="Vertical">
                        <Columns>
                            <wijmo1:C1BoundField DataField="CAMU" Width="80px" HeaderText="Embala">
                            </wijmo1:C1BoundField>
                            <wijmo1:C1BoundField DataField="STQT" Width="90px" HeaderText="Inicial" DataFormatString="n">
                            </wijmo1:C1BoundField>
                            <wijmo1:C1BoundField DataField="DISP" Width="80px" HeaderText="Quant" DataFormatString="n">
                            </wijmo1:C1BoundField>
                            <wijmo1:C1BoundField DataField="BREF" Width="70px" HeaderText="Bobine">
                            </wijmo1:C1BoundField>
                        </Columns>
                    </wijmo1:c1gridview>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <script type="text/javascript">
       <!-- Script: retornar o mRow seleccionado pelo cliente sem ir ao Servidor -->
        function xProductSelectionChanged() {
            var selectedCells = $('#<%= C1Prods.ClientID %>').c1gridview("selection").selectedCells();
            var i, cellInfo, mCols, mRow, mValue;
            for (i = 0; i < selectedCells.length(); i++) {
                mCols = selectedCells.length();
                cellInfo = selectedCells.item(i);
                mRow = cellInfo.rowIndex();
                mValue = selectedCells.item(0).value();
                mName = selectedCells.item(1).value();
                document.getElementById('text1').value = mValue;
                document.getElementById('text3').value = mRow;
             }
        }

        function xProductSortChanged() {
            var selectedCells = $('#<%= C1Prods.ClientID %>').c1gridview("selection").selectedCells();
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
<%-- 
<table id="dataGrid" data-bind="
    wijgrid: {
        selectionMode: 'singleRow',
        data: dataRows,
        pageSize: pageSize,
        pageIndex: pageIndex,
        totalRows: totalRows,
        allowPaging: true,
        allowSorting: true,
        sorted: sorted,
        pageIndexChanged: paged,
        columns: [
            { dataKey: 'ProductID', sortDirection: 'ascending', dataType: 'number', dataFormatString: 'n0', headerText: 'ID', width: 60 },
            { dataKey: 'ProductName', headerText: 'Product' },
            { dataKey: 'UnitPrice', dataType: 'currency', headerText: 'Price', width: 100},
            { dataKey: 'UnitsInStock', dataType: 'number', dataFormatString: 'n0', headerText: 'Units', width: 100},
            { visible: true, cellFormatter: cellFormatter, width: 100 }]
    }">
</table>
--%>
