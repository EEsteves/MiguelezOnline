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
            <table width="910px;">
                <tr>
                    <td colspan="2">
                        <span class="style8">&nbsp;Clientes&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                        <wijmo1:C1ComboBox ID="C1ComboBox1" runat="server" AutoPostBack="True" MinLength="1"
                            Width="70px" Delay="0" Style="font-size: small" AutoFilter="False" DropdownHeight="350"
                            ForceSelectionText="True" SelectElementWidthFix="12">
                        </wijmo1:C1ComboBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Código de Cliente&nbsp;
                        <input type="text" id="text1" name="text1" size="5" />
                        &nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;
                        <input type="text" id="text2" name="text2" size="5" style="diplay: none;" hidden="hidden" />
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="left">
                        <div class="rounded_corners" style="width: 580px; height: 450px; padding-top: 10px;"
                            align="left">
                            <wijmo:c1gridview id="C1Clientes" runat="server" style="top: 0px; left: 0px; font-size: small;"
                                autogeneratecolumns="False" width="559px" height="270px" pagesize="25" clientselectionmode="SingleRow"
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
                            <br />
                            <table width="100%">
                                <tr>
                                    <td colspan="2" align="left" style="padding-left: 10px;">
                                        <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="#CC3300" Style="font-size: large"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left: 10px;">
                                        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td rowspan="2" align="right" style="padding-right: 6px;">
                                        <asp:Button ID="Button1" runat="server" Text="Mostrar Informação ao Cliente" Height="40px"
                                            Width="200px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left: 10px;">
                                        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left: 10px;">
                                        <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td rowspan="2" align="right" style="padding-right: 6px;">
                                        <asp:Button ID="Button2" runat="server" Text="Mostrar Descontos" Height="40px" Width="200px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left: 10px;">
                                        <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left: 10px;">
                                        Contacto&nbsp; &nbsp; &nbsp;
                                        <asp:Label ID="lblContacto" runat="server" />
                                    </td>
                                    <td align="right" style="padding-right: 6px; width: 190px;">
                                        <br />
                                        Telefone &nbsp; &nbsp; &nbsp;
                                        <a style="cursor:pointer;" ID="lnkTelefone" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td valign="top" align="right">
                        <table width="300px;" height="462px;" border="0px" class="rounded_corners">
                            <tr>
                                <th colspan="2" style="background-color: #4b6c9e; height: 30px; color: White;">
                                    Client Information
                                </th>
                            </tr>
                            <tr>
                                <td align="left" style="padding-left: 5px;">
                                    <label for="sPlafond">
                                        Plafond</label>
                                </td>
                                <td align="right" style="padding-right: 5px;">
                                    <asp:Label ID="sPlafond" runat="server" Text="0.00"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="padding-left: 5px;">
                                    Plafond Interno
                                </td>
                                <td align="right" style="padding-right: 5px;">
                                    <asp:Label ID="sPlafInt" runat="server" Text="0.00"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="padding-left: 5px;">
                                    Plafond Total
                                </td>
                                <td align="right" style="padding-right: 5px;">
                                    <asp:Label ID="sPlafTot" runat="server" Text="0.00"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="padding-left: 5px;">
                                    Saldo Corrente
                                </td>
                                <td align="right" style="padding-right: 5px;">
                                    <asp:Label ID="sSaldoCorr" runat="server" Text="0.00"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="padding-left: 5px;">
                                    Saldo Atrazado
                                </td>
                                <td align="right" style="padding-right: 5px;">
                                    <asp:Label ID="sSaldoAtrz" runat="server" Text="0.00"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="padding-left: 5px;">
                                    Guias por Facturar
                                </td>
                                <td align="right" style="padding-right: 5px;">
                                    <asp:Label ID="sGuiasPorFact" runat="server" Text="0.00"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="padding-left: 5px;">
                                    Letras
                                </td>
                                <td align="right" style="padding-right: 5px;">
                                    <asp:Label ID="sLetras" runat="server" Text="0.00"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="padding-left: 5px;">
                                    Crédito Disponível
                                </td>
                                <td align="right" style="padding-right: 5px;">
                                    <asp:Label ID="sCredDisp" runat="server" Text="0.00"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="padding-left: 5px;">
                                    Encomendas Pendentes
                                </td>
                                <td align="right" style="padding-right: 5px;">
                                    <asp:Label ID="sEncPend" runat="server" Text="0.00"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="padding-left: 5px;">
                                    Ultimo Ano
                                </td>
                                <td align="right" style="padding-right: 5px;">
                                    <asp:Label ID="sUltAno" runat="server" Text="0.00"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="padding-left: 5px;">
                                    Observações
                                </td>
                                <td align="right" style="padding-right: 5px;">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" style="padding-left: 5px;">
                                    <a runat="server" id="lnkEmail" />  
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="padding-left: 5px;">
                                    Prazo Médio
                                </td>
                                <td align="right" style="padding-right: 5px;">
                                    <asp:Label ID="Label10" runat="server" Text="0 dias"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="padding-left: 5px;">
                                    Vendedor
                                </td>
                                <td align="right" style="padding-right: 5px;">
                                    <asp:Label ID="Label6" runat="server" Text="01"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="padding-left: 5px;">
                                    Contribuinte
                                </td>
                                <td align="right" style="padding-right: 5px;">
                                    <asp:Label ID="Label7" runat="server" Text="99999990"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="padding-left: 5px;">
                                    Fax
                                </td>
                                <td align="right" style="padding-right: 5px;">
                                    <asp:Label ID="Label9" runat="server" Text="555 555 555"></asp:Label>
                                </td>
                            </tr>
                        </table>
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
