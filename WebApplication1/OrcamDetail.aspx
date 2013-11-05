<%@ Page Title="Orçamento - Detalhes" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="~/OrcamDetail.aspx.vb"
    Inherits="RobotNet.OrçamentosDetail" %>

<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1GridView"
    TagPrefix="wijmo1" %>
<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1Menu"
    TagPrefix="wijmo" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
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
            <table style="height: 100%; width: 100%">
                <tr>
                    <td>
                        <strong>Orçamentos - Detalhes&nbsp;
                            <asp:TextBox ID="TextBox1" runat="server" Width="121px" AutoPostBack="True" Font-Bold="True"
                                ForeColor="Blue">12E05146</asp:TextBox>
                        </strong>
                    </td>
                </tr>
                <tr>
                    <td>
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
                                <wijmo:C1MenuItem ID="C1MenuItem1" runat="server" Text="&lt; Anterior">
                                </wijmo:C1MenuItem>
                                <wijmo:C1MenuItem ID="C1MenuItem2" runat="server" Text="Seguinte &gt;">
                                </wijmo:C1MenuItem>
                                <wijmo:C1MenuItem ID="C1MenuItem3" runat="server" Text="Imprime Encomenda">
                                </wijmo:C1MenuItem>
                            </Items>
                        </wijmo:C1Menu>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="rounded_corners" style="width: 100%; height: 170px; padding-top: 10px;"
                            align="center">
                            <div style="float: left; margin-left: 10px; margin-top: 10px;">
                                <table>
                                    <tr>
                                        <td colspan="2" align="left">
                                            <asp:Label ID="sNome" runat="server" Text="sNome" ForeColor="Blue"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left">
                                            <asp:Label ID="sContact" runat="server" Text="sContact" ForeColor="Blue"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <label for="sRef">
                                                Referência</label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="sRef" runat="server" Text="sRef" ForeColor="Blue"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <label for="sLoc1">
                                                Local</label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="sLoc1" runat="server" Text="sLoc1" ForeColor="Blue"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <label for="sLoc2">
                                                Entrega</label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="sLoc2" runat="server" Text="sLoc2" ForeColor="Blue"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left">
                                            <asp:Label ID="sLoc3" runat="server" Text="sLoc3" ForeColor="Blue"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left">
                                            <asp:Label ID="sLoc4" runat="server" Text="sLoc4" ForeColor="Blue"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="float: right; margin-right: 10px; margin-top: 10px; width: 25%">
                                <table width="80%">
                                    <tr>
                                        <td align="left">
                                            <label for="sVend">
                                                Vendedor</label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="sVend" runat="server" Text="sVend" ForeColor="Blue"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <label for="sData">
                                                Data</label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="sData" runat="server" Text="sData" ForeColor="Blue"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <label for="sPrevista">
                                                Validade</label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblValidade" runat="server" Text="sValid" ForeColor="Blue"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <label for="sPrazo">
                                                Condições</label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="sPrazo" runat="server" Text="sPrazo" ForeColor="Blue"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <label for="sSoma">
                                                Soma</label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="sSoma" runat="server" Text="sSoma" ForeColor="#990000"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <label for="sIva">
                                                Iva</label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="sIva" runat="server" Text="sIva" ForeColor="#990000"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <label for="sValor">
                                                Valor</label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="sValor" runat="server" Text="sValor" ForeColor="#990000"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <wijmo1:c1gridview id="C1Encomendas" runat="server" clientselectionmode="SingleRow"
                            autogeneratecolumns="False" pagesize="9" height="170px">
                    <PagerSettings Visible="False" />
                    <Columns>
                        <wijmo1:C1BoundField DataField="I_LINHA" HeaderText="Lin">
                        </wijmo1:C1BoundField>
                        <wijmo1:C1BoundField DataField="I_PRODUCT" HeaderText="Produto">
                        </wijmo1:C1BoundField>
                        <wijmo1:C1BoundField DataField="I_DESC_1" HeaderText="Nome">
                        </wijmo1:C1BoundField>
                        <wijmo1:C1BoundField DataField="I_EMBALA" HeaderText="Emb">
                        </wijmo1:C1BoundField>
                        <wijmo1:C1BoundField DataField="I_QUANT" HeaderText="Quant">
                        </wijmo1:C1BoundField>
                        <wijmo1:C1BoundField DataField="X_UNIT" HeaderText="Unit" DataFormatString="n">
                        </wijmo1:C1BoundField>
                        <wijmo1:C1BoundField DataField="I_TOTAL" HeaderText="Valor">
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