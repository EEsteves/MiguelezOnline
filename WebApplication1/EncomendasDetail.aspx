<%@ Page Title="Encomenda - Detalhes" Language="vb" MasterPageFile="~/Site.Master"
    AutoEventWireup="false" CodeBehind="EncomendasDetail.aspx.vb" Inherits="RobotNet.EncomendasDetail" %>

<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1GridView"
    TagPrefix="wijmo1" %>
<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1Menu"
    TagPrefix="wijmo" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .newStyle1
        {
        }
        .style4
        {
            font-size: large;
            height: 21px;
            width: 90%;
        }
        .style5
        {
            width: 100%;
        }
        .style15
        {
            color: #FF0000;
        }
        .style22
        {
            height: 12px;
            width: 91px;
        }
        .style23
        {
            width: 400px;
            height: 12px;
        }
        .style24
        {
            width: 135px;
            height: 13px;
        }
        .style25
        {
            height: 13px;
        }
        .style34
        {
            height: 14px;
            width: 241px;
        }
        .style38
        {
            top: 0px;
            left: 0px;
            height: 172px;
        }
        .style39
        {
            height: 13px;
            width: 241px;
        }
        .style40
        {
            top: 0px;
            left: 0px;
            height: 161px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table style="height: 440px">
        <tr>
            <td>
                <strong>Encomenda Nº&nbsp;
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
                <div class="rounded_corners" style="width: 100%; height: 150px; padding-top: 10px;"
                    align="center">
                    <div style="float: left; margin-left: 10px; margin-top: 10px;">
                        <table>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="sNome" runat="server" Text="sNome" ForeColor="Blue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="sContact" runat="server" Text="sContact" ForeColor="Blue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="sRef">
                                        Referência</label>
                                </td>
                                <td>
                                    <asp:Label ID="sRef" runat="server" Text="sRef" ForeColor="Blue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="sLoc1">
                                        Local</label>
                                </td>
                                <td>
                                    <asp:Label ID="sLoc1" runat="server" Text="sLoc1" ForeColor="Blue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="sLoc2">
                                        Entrega</label>
                                </td>
                                <td>
                                    <asp:Label ID="sLoc2" runat="server" Text="sLoc2" ForeColor="Blue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="sLoc3" runat="server" Text="sLoc3" ForeColor="Blue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="sLoc4" runat="server" Text="sLoc4" ForeColor="Blue"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="float: right; margin-right: 10px; margin-top: 10px; width: 25%">
                        <table width="80%">
                            <tr>
                                <td>
                                    <label for="sVend">
                                        Vendedor</label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="sVend" runat="server" Text="sVend" ForeColor="Blue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="sData">
                                        Data</label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="sData" runat="server" Text="sData" ForeColor="Blue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="sPrevista">
                                        Previsão</label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="sPrevista" runat="server" Text="sPrevista" ForeColor="Blue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="sPrazo">
                                        Condições</label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="sPrazo" runat="server" Text="sPrazo" ForeColor="Blue"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="sSoma">
                                        Soma</label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="sSoma" runat="server" Text="sSoma" ForeColor="#990000" CssClass="style15"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="sIva">
                                        Iva</label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="sIva" runat="server" Text="sIva" ForeColor="#990000" CssClass="style15"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="sValor">
                                        Valor</label>
                                </td>
                                <td align="right">
                                    <asp:Label ID="sValor" runat="server" Text="sValor" ForeColor="#990000" CssClass="style15"></asp:Label>
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
                    autogeneratecolumns="False" cssclass="style40" pagesize="9">
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
                        <wijmo1:C1BoundField DataField="I_FALTA" HeaderText="Falta">
                        </wijmo1:C1BoundField>
                    </Columns>
                    <SelectedRowStyle BackColor="#99CCFF" ForeColor="Red" />
                </wijmo1:c1gridview>
            </td>
        </tr>
    </table>
</asp:Content>
