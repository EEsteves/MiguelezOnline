<%@ Page Title="Encomendas - Estatísticas" Language="vb" MasterPageFile="~/Site.Master"
    AutoEventWireup="false" CodeBehind="EncomendasStats.aspx.vb" Inherits="RobotNet.EncomendasStats" %>

<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1Menu"
    TagPrefix="wijmo" %>
<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1Chart"
    TagPrefix="wijmo1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style4
        {
            width: 100%;
        }
        .style7
        {
            font-size: x-large;
        }
        .style8
        {
            width: 100%;
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
            <span class="style7">Encomendas - Estatísticas</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox1" runat="server" Width="60px" AutoPostBack="True" Visible="False"></asp:TextBox>
            &nbsp;<table class="style4">
                <tr>
                    <td class="style8">
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
                                <wijmo:C1MenuItem ID="C1MenuItem1" runat="server" Text="Mensal" StaticKey="sk1">
                                </wijmo:C1MenuItem>
                                <wijmo:C1MenuItem ID="C1MenuItem3" runat="server" Text="Anual" StaticKey="sk3">
                                </wijmo:C1MenuItem>
                            </Items>
                        </wijmo:C1Menu>
                    </td>
                </tr>
                <tr>
                    <td class="style8">
                        <asp:Table ID="Table1" runat="server" Style="font-size: small" Width="145px">
                        </asp:Table>
                    </td>
                    <td>
                        <wijmo1:C1BarChart ID="C1Chart" runat="server" Horizontal="False" Width="700px" Height="350px">
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
                        </wijmo1:C1BarChart>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
