<%@ Page Title="Imprime Lista de Clientes" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ClientesPrint.aspx.vb"
    Inherits="RobotNet.ClientesPrint" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web"
    TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            font-size: large;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p class="style1">
        <strong>Imprime Lista de Clientes</strong></p>
    <p class="style1">
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" HasDrilldownTabs="False" 
        HasDrillUpButton="False" HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False" PageZoomFactor="75" 
        ReportSourceID="CrystalReportSource1" ToolPanelView="None" />
        <p>
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server" Visible="False">
            <Report FileName="CR_PCFCLI1.rpt">
                <DataSources>
                    <CR:DataSourceRef TableName="c:\pcffiles\data\pcfcli.dbf" />
                </DataSources>
            </Report>
        </CR:CrystalReportSource>
</asp:Content>
