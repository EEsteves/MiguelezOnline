<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="Clientes.aspx.vb" Inherits="RobotNet.Clientes" %>

<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1GridView"
    TagPrefix="wijmo" %>
<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1Menu"
    TagPrefix="wijmo" %>
<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1Input"
    TagPrefix="wijmo" %>
<%@ Register Assembly="C1.Web.Wijmo.Controls.4" Namespace="C1.Web.Wijmo.Controls.C1ComboBox"
    TagPrefix="wijmo1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style4
        {
            width: 912px;
            height: 330px;
        }
        .style8
        {
            font-size: large;
        }
        .style9
        {
            width: 540px;
            height: 21px;
            margin-left: 0px;
        }
        .style5
        {
            width: 400px;
            height: 21px;
        }
        .style18
        {
            width: 120px;
            height: 21px;
        }
        .style25
        {
            width: 345px;
            height: 21px;
        }
        .style33
        {
            width: 159px;
            height: 37px;
        }
        .style36
        {
            width: 466px;
            height: 42px;
        }
        .style37
        {
            width: 198px;
            height: 21px;
        }
        .style38
        {
            width: 198px;
            height: 13px;
        }
        .style41
        {
            width: 120px;
        }
        .style44
        {
            width: 198px;
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
            <span class="style8">&nbsp;Clientes&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
            <wijmo1:C1ComboBox ID="C1ComboBox1" runat="server" AutoPostBack="True" MinLength="1"
                Width="70px" Delay="0" Style="font-size: small" AutoFilter="False" DropdownHeight="350"
                ForceSelectionText="True" SelectElementWidthFix="12">
            </wijmo1:C1ComboBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Código de Cliente&nbsp;
            <input type="text" id="text1" name="text1" size="5" />
            &nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;
            <input type="text" id="text2" name="text2" size="5" hidden="hidden" />&nbsp;&nbsp;&nbsp;
            &nbsp;<table class="style4">
                <tr>
                    <td class="style9" colspan="2" rowspan="12">
                        &nbsp; <strong>
                            <wijmo:c1gridview id="C1Clientes" runat="server" style="top: 0px; left: 1px; font-size: small;"
                                autogeneratecolumns="False" width="559px" height="283px" pagesize="25" clientselectionmode="SingleRow"
                                allowautosort="False" scrollmode="Vertical" onclientselectionchanged="xSelChanged"
                                onclientrendered="xGridLoaded" allowsorting="True">
                        <Columns>
                            <wijmo:C1BoundField DataField="C_CLIENT" Width="100px" HeaderText="Cod">
                            </wijmo:C1BoundField>
                            <wijmo:C1BoundField DataField="C_NAME" Width="300px" HeaderText="Nome">
                            </wijmo:C1BoundField>
                            <wijmo:C1BoundField DataField="C_PHONE1" HeaderText="Telefone" Width="150px">
                            </wijmo:C1BoundField>
                        </Columns>
                    </wijmo:c1gridview>
                        </strong>
                    </td>
                    <td class="style37">
                        Plafond
                    </td>
                    <td class="style41">
                        <asp:Label ID="sPlafond" runat="server" Text="0.00" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style37">
                        Plafond Interno
                    </td>
                    <td class="style18">
                        <asp:Label ID="sPlafInt" runat="server" Text="0.00" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style37" align="left" style="font-weight: bold; color: #FF0000;">
                        Plafond Total
                    </td>
                    <td class="style18">
                        <asp:Label ID="sPlafTot" runat="server" Text="0.00" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style37">
                        Saldo Corrente
                    </td>
                    <td class="style18">
                        <asp:Label ID="sSaldoCorr" runat="server" Text="0.00" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style37">
                        Saldo Atrazado
                    </td>
                    <td class="style18">
                        <asp:Label ID="sSaldoAtrz" runat="server" Text="0.00" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style37">
                        Guias por Facturar
                    </td>
                    <td class="style18">
                        <asp:Label ID="sGuiasPorFact" runat="server" Text="0.00" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style37">
                        Letras
                    </td>
                    <td class="style18">
                        <asp:Label ID="sLetras" runat="server" Text="0.00" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style37" style="font-weight: bold; color: #FF0000">
                        Crédito Disponível
                    </td>
                    <td class="style18">
                        <asp:Label ID="sCredDisp" runat="server" Text="0.00" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style37">
                        Encomendas Pendentes
                    </td>
                    <td class="style18">
                        <asp:Label ID="sEncPend" runat="server" Text="0.00" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style38">
                        Ultimo Ano
                    </td>
                    <td class="style18">
                        <asp:Label ID="sUltAno" runat="server" Text="0.00" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style38">
                        Observações
                    </td>
                    <td class="style18">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style38">
                        Contacto
                    </td>
                    <td class="style18">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style36" rowspan="2">
                        <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="#CC3300" Style="font-size: large"></asp:Label>
                    </td>
                    <td class="style33" rowspan="2">
                        <strong>
                            <asp:Button ID="Button1" runat="server" Text="Calc " Height="36px" />
                            &nbsp;<asp:Button ID="Button2" runat="server" Text="Desc" Height="36px" />
                        </strong>
                    </td>
                    <td class="style25" colspan="2">
                        <asp:Label ID="lblEmail" runat="server" Text="Email" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style44">
                        Prazo Médio
                    </td>
                    <td class="style41">
                        <asp:Label ID="Label10" runat="server" Text="0 dias" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style5" colspan="2">
                        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td class="style37">
                        Vendedor
                    </td>
                    <td class="style18">
                        <asp:Label ID="Label6" runat="server" Text="01" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style5" colspan="2">
                        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td class="style37">
                        Contribuinte
                    </td>
                    <td class="style18">
                        <asp:Label ID="Label7" runat="server" Text="99999990" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style5" colspan="2">
                        <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td class="style37">
                        Telefone
                    </td>
                    <td class="style18">
                        <asp:Label ID="Label8" runat="server" Text="211 234 567" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style5" colspan="2">
                        <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td class="style37">
                        Fax
                    </td>
                    <td class="style18">
                        <asp:Label ID="Label9" runat="server" Text="555 555 555" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
            </table>
            <!-- Script para retornar o mRow seleccionado pelo cliente sem ir ao Servidor -->
            <script type="text/javascript">
                function xSelChanged() {
                    var mRow = $('#<%= C1Clientes.ClientID %>').c1gridview("currentCell").rowIndex();
                    var mCod = $('#<%= C1Clientes.ClientID %>').c1gridview("currentCell").row().data[0];
                    document.getElementById('text1').value = mCod;
                    document.getElementById('text2').value = mRow;
                    //            if (mRow != 0)
                    //                __doPostBack('#<%= C1Clientes.ClientID %>', mCod);
                }

                function xGridLoaded() {
                    //          var $grid = $('#<%= C1Clientes.ClientID %>'),
                    //          $row = $($grid.data('#<%= C1Clientes.ClientID %>')._rows().item(index)),
                    //          $panel = $grid.closest('.wijmo-wijgrid').find('.wijmo-wijsuperpanel');
                    //          $panel.wijsuperpanel('vScrollTo', $row.position().top);
                    //          $grid.wijgrid('selection').addRange(0, index, 0xFFFFFF, index);
                }

         
        
            </script>
            <%--    <script runat="server">
          Sub xxx(ByVal sender As Object, ByVal e As EventArgs)
              ' Get the currently selected row using the SelectedRow property.
              Dim xRow As C1GridViewRow
              xRow = C1Clientes.SelectedRow
              ' Display the client name from the selected row.
              ' In this example, the second column (index 1) contains the client name.
              MessageLabel.Text = "Seleccionou " & xRow.Cells(1).Text & "."
          End Sub

        Sub CustomersGridView_SelectedIndexChanging(ByVal sender As Object, ByVal e As GridViewSelectEventArgs)
            ' Get the currently selected row. Because the SelectedIndexChanging event
            ' occurs before the select operation in the GridView control, the
            ' SelectedRow property cannot be used. Instead, use the Rows collection
            ' and the NewSelectedIndex property of the e argument passed to this event handler.
            Dim row As C1GridViewRow = C1Clientes.Rows(e.NewSelectedIndex)
            ' You can cancel the select operation by using the Cancel property.
            ' For this example, if the user selects a customer with the ID "006",
            ' the select operation is canceled and an error message is displayed.
            If row.Cells(0).Text = "006" Then
                e.Cancel = True
                MessageLabel.Text = "Não pode seleccionar " + row.Cells(1).Text & "."
            End If
        End Sub
    </script>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
