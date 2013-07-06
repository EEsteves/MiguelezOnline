<%@ Page Title="Documento" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="VendasDetail.aspx.vb" Inherits="RobotNet.VendasDetail" %>

<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1GridView"
    TagPrefix="wijmo1" %>
<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1Menu"
    TagPrefix="wijmo" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
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
            <table style="height: 100%; width: 100%">
                <tr>
                    <td>
                        <strong>Documento - Detalhes&nbsp;
                            <asp:TextBox ID="txtSelectedVendas" runat="server" Width="121px" AutoPostBack="True"
                                Font-Bold="True" ForeColor="Blue">12E05146</asp:TextBox>
                        </strong>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
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
                                <wijmo:C1MenuItem runat="server" Text="&lt; Anterior">
                                </wijmo:C1MenuItem>
                                <wijmo:C1MenuItem runat="server" Text="Seguinte &gt;">
                                </wijmo:C1MenuItem>
                                <wijmo:C1MenuItem runat="server" Text="Imprime Encomenda">
                                </wijmo:C1MenuItem>
                            </Items>
                        </wijmo:C1Menu>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="rounded_corners" style="width: 100%; height: 180px; padding-top: 10px;"
                            align="center">
                            <div style="float: left; margin-left: 10px; margin-top: 10px;">
                                <table>
                                    <tr>
                                        <td>
                                            <label for="lblNumero">
                                                Numero</label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNumero" runat="server" ForeColor="Blue" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label for="lblCliente">
                                                Cliente</label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCliente" runat="server" ForeColor="Blue" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label for="lblCliente">
                                                Data</label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblData" runat="server" ForeColor="Blue" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label for="lblCondPage">
                                                Cond Page</label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCondPage" runat="server" ForeColor="Blue" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label for="lblVencimento">
                                                Vencimento</label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblVencimento" runat="server" ForeColor="Blue" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label for="lblAtrazo">
                                                Atrazo</label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblAtrazo" runat="server" ForeColor="Blue" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label for="lblTotalSemIVA">
                                                Total Sem IVA</label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblTotSemIVA" runat="server" ForeColor="#990000" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label for="lblIVA">
                                                IVA</label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblIVA" runat="server" ForeColor="#990000" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label for="lblTotalComIVA">
                                                Total Com IVA</label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblTotalComIVA" runat="server" ForeColor="#990000" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="float: right; margin-right: 10px; margin-top: 10px; width: 40%">
                                <table width="80%">
                                    <tr>
                                        <td colspan="3" align="center">
                                            <label for="sVend">
                                                Tipo de Documento</label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <label for="lblNome">
                                                Nome</label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblNome" runat="server" ForeColor="Blue" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <label for="lblAddress">
                                                Address</label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblAddress" runat="server" ForeColor="Blue" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <wijmo1:c1gridview id="grdVendasDetails" runat="server" clientselectionmode="SingleRow"
                            autogeneratecolumns="False" height="161px" pagesize="9">
                    <PagerSettings Visible="False" />
                    <Columns>
                        <wijmo1:C1BoundField DataField="A_PRODUCT" HeaderText="Produto">
                        </wijmo1:C1BoundField>
                        <wijmo1:C1BoundField DataField="A_NAME" HeaderText="Nome">
                        </wijmo1:C1BoundField>
                        <wijmo1:C1BoundField DataField="A_QUANT" HeaderText="Quant">
                        </wijmo1:C1BoundField>
                        <wijmo1:C1BoundField DataField="A_UNIT" HeaderText="Unit" DataFormatString="n">
                        </wijmo1:C1BoundField>
                        <wijmo1:C1BoundField DataField="A_VALOR" HeaderText="Valor">
                        </wijmo1:C1BoundField>
                        <wijmo1:C1BoundField DataField="A_DESCONTOS" HeaderText="Descontos">
                        </wijmo1:C1BoundField>
                        <wijmo1:C1BoundField DataField="A_VALORCOMIVA" HeaderText="Valor com IVA">
                        </wijmo1:C1BoundField>
                    </Columns>
                    <SelectedRowStyle BackColor="#99CCFF" ForeColor="Red" />
                </wijmo1:c1gridview>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
