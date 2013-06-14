﻿Imports Microsoft.JScript
Imports C1.Web.Wijmo.Controls.C1ComboBox

Public Class Encomendas
    Inherits System.Web.UI.Page

    Private dataTable1 As New DataTable("Orders")
    Private mSelectedStatus As String = ""
    Private mSelectedClient As String = ""
    Private mSelectedAgent As String = ""
    Private mSelectedPeriod As String = ""
    Private mStartDt As String = "01/01/2000"
    Private mEndDt As String = "01/01/2050"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("mSelectedAgentLoggedIn") = "NO" Then
            Dim xStr As String = "~/Default.aspx"
            Response.Redirect(xStr)
            Exit Sub
        End If

        ' -------------------------------------------------------------------------------------
        ' Teste
        ' -------------------------------------------------------------------------------------
        mSelectedAgent = Session("mAgentOnline")
        'mSelectedAgent = "03"

        ' Colunas da Tabela de Encomendas
        dataTable1.Columns.Add("Cliente")
        dataTable1.Columns.Add("Nome")
        dataTable1.Columns.Add("Numero")
        dataTable1.Columns.Add("Vend")
        dataTable1.Columns.Add("Data")
        dataTable1.Columns.Add("Sit")
        dataTable1.Columns.Add("Encnum")
        dataTable1.Columns.Add("Valor")

        If IsPostBack = False Then
            ' Carrega o dropdown de Clientes
            Dim cb = code4init()
            Dim fCli As Integer = d4open(cb, fPCFCLI)
            Dim tag As Integer = d4tag(fCli, "PCFCLI2")
            Call d4tagSelect(fCli, tag)
            Dim fCliCode As Integer = d4field(fCli, "C_CLIENT")
            Dim fName As Integer = d4field(fCli, "C_NAME")
            Dim fStatus As Integer = d4field(fCli, "C_STATUS")
            Dim fVend As Integer = d4field(fCli, "C_VEND")
            rc = d4top(fCli)
            While d4eof(fCli) = 0
                mCliNm = f4str(fName)
                mCliVend = f4str(fVend)
                If mSelectedAgent = "99" Then
                    mSelect = True
                Else
                    If Trim(mCliVend) = mSelectedAgent Then
                        mSelect = True
                    Else
                        mSelect = False
                    End If
                End If
                If Len(Trim(mCliNm)) = 0 Then
                    mSelect = False
                End If
                If mSelect = True Then
                    C1Cliente.Items.Add(New C1ComboBoxItem(f4str(fCliCode) + Trim(mCliNm)))
                End If
                rc = d4skip(fCli, 1)
            End While
            rc = d4close(fCli)
            rc = code4initUndo(cb)

            ' Carrega o dropdown de Situações
            C1Status.Items.Add(New C1ComboBoxItem("A - Abertas"))
            C1Status.Items.Add(New C1ComboBoxItem("C - Fechadas"))
            C1Status.Items.Add(New C1ComboBoxItem("X - Todas"))
            C1Status.Text = "A - Abertas"

            ' Carrega o dropdown de Datas
            C1Data.Items.Add(New C1ComboBoxItem("1 - Ultimo Mês"))
            C1Data.Items.Add(New C1ComboBoxItem("2 - Ultimo Trimestre"))
            C1Data.Items.Add(New C1ComboBoxItem("3 - Ultimo Ano"))
            C1Data.Items.Add(New C1ComboBoxItem("9 - Todas"))
            C1Data.Text = "2 - Ultimo Trimestre"

            mSelectedStatus = "A"
            dataTable1 = xLoadOrders()

            ' Go to last page - 1
            Dim mPages = C1Encomendas.PageCount
            C1Encomendas.PageIndex = mPages - 2
            C1Encomendas.DataBind()

            mRow = 0

            ' MsgBox("Postback false")
        Else
          
        End If

        mSelectedStatus = Mid(C1Status.Text, 1, 1)
        mSelectedClient = Mid(C1Cliente.Text, 1, 8)
        mSelectedPeriod = Mid(C1Data.Text, 1, 1)

        ' MsgBox("Page Load")

    End Sub

    Function xLoadOrders()
        Dim connStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:\PCFFILES\DATA\';Extended Properties=dBase 5.0"
        Dim conn As New OleDb.OleDbConnection(connStr)
        Dim cmd As New OleDb.OleDbCommand()
        cmd.Connection = conn
        mC0 = "SELECT E_CLIENT, E_NAME, E_NUMBER, E_VEND, E_DATE, CSTR(E_DATE) AS X_DATE, E_STATUS, E_ENCNUM, E_TOTAL FROM PCFECL WHERE "
        If mSelectedStatus = "X" Then
            mC0 = mC0 + "E_STATUS LIKE '[ABCD]%' "
        Else
            mC0 = mC0 + "E_STATUS LIKE '[" + mSelectedStatus + "]%' "
        End If
        If Len(Trim(mSelectedAgent)) > 0 Then
            mC0 = mC0 + "AND E_VEND = '" + mSelectedAgent + "' "
        End If
        If Len(Trim(mSelectedClient)) > 0 Then
            mC0 = mC0 + "AND E_CLIENT = '" + mSelectedClient + "' "
        End If
        ' mC0 = mC0 + " AND E_DATE >= CDATE('29/07/2012') AND E_DATE <= CDATE('09/22/2013') OR USE THIS ONE - WORKS OK TOO"
        mC0 = mC0 + "AND E_DATE BETWEEN CDATE('" + mStartDt + "') AND CDATE('" + mEndDt + "') "
        ' mC0 = mC0 + "ORDER BY E_DATE"
        ' MsgBox(mC0)
        cmd.CommandText = mC0
        conn.Open()
        Dim reader As OleDb.OleDbDataReader = cmd.ExecuteReader
        Dim dataTable1 As New DataTable
        dataTable1.Load(reader)
        C1Encomendas.Columns(0).Width = 75  ' Cliente
        C1Encomendas.Columns(1).Width = 345 ' Nome
        C1Encomendas.Columns(2).Width = 75  ' Numero 
        C1Encomendas.Columns(3).Width = 55  ' Vendedor
        C1Encomendas.Columns(4).Width = 80  ' Data
        C1Encomendas.Columns(5).Width = 40  ' Sit
        C1Encomendas.Columns(6).Width = 150 ' Enc Num
        C1Encomendas.Columns(7).Width = 100 ' Valor 
        C1Encomendas.Columns(0).ItemStyle.HorizontalAlign = HorizontalAlign.Center ' Cliente
        C1Encomendas.Columns(3).ItemStyle.HorizontalAlign = HorizontalAlign.Center ' Vendedor
        C1Encomendas.Columns(4).ItemStyle.HorizontalAlign = HorizontalAlign.Center ' Data     
        C1Encomendas.Columns(5).ItemStyle.HorizontalAlign = HorizontalAlign.Center ' Sit
        ' C1Encomendas.ScrollMode = C1.Web.Wijmo.Controls.C1GridView.ScrollMode.Vertical
        C1Encomendas.DataSource = dataTable1
        C1Encomendas.DataBind()
        C1Encomendas.AllowKeyboardNavigation = True
        C1Encomendas.AllowPaging = True
        reader.Close()
        conn.Close()
        ' MsgBox("load orders " + dataTable1.Rows.Count.ToString)
        Return dataTable1
    End Function

    Private Sub C1Encomendas_PageIndexChanging(sender As Object, e As C1.Web.Wijmo.Controls.C1GridView.C1GridViewPageEventArgs) Handles C1Encomendas.PageIndexChanging
        dataTable1 = xLoadOrders()
        ' MsgBox("index changed to " + e.NewPageIndex.ToString + "recs: " + dataTable1.Rows.Count.ToString)
        C1Encomendas.PageIndex = e.NewPageIndex
        C1Encomendas.DataSource = dataTable1
        C1Encomendas.DataBind()
    End Sub

    Private Sub C1Menu1_ItemClick(sender As Object, e As C1.Web.Wijmo.Controls.C1Menu.C1MenuEventArgs) Handles C1Menu1.ItemClick
        Dim xRow As Integer
        xRow = Request.Form("text2")
        mOrderNum = C1Encomendas.Rows(xRow).Cells(2).Text
        If e.Item.Text = "Visualiza Encomenda" Then
            Dim xStr As String = "~/EncomendasDetail.aspx?field1=" + Trim(mOrderNum)
            Response.Redirect(xStr)
            xMessageEnc("Ainda não está implementado")
        ElseIf e.Item.Text = "Estatísticas" Then
            Dim xStr As String = "~/EncomendasStats.aspx?field1=" + Trim(mOrderNum)
            Response.Redirect(xStr)
        ElseIf e.Item.Text = "Imprime Lista de Encomendas" Then
            'Dim xStr As String = "~/EncomendasLista.aspx?field1=" + Trim(mOrderNum)
            'Response.Redirect(xStr)
            xMessageEnc("Ainda não está implementado")
        ElseIf e.Item.Text = "Imprime uma Encomenda" Then
            'Dim xStr As String = "~/EncomendasPrint.aspx?field1=" + Trim(mOrderNum)
            'Response.Redirect(xStr)
            xMessageEnc("Ainda não está implementado")
        Else
            ' Substituir esta msgbox
            xMessageEnc("Opção errada " & e.Item.Text)
        End If
    End Sub

    Public Function xMessageEnc(xMsg)
        Dim strMessage As String
        strMessage = xMsg
        Dim strScript As String = "<script language=JavaScript>"
        strScript = strScript + "alert(""" & strMessage & """);"
        strScript = strScript + "</script>"
        If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript")) Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript", strScript)
        End If
        Return ""
    End Function

    Private Sub C1Status_SelectedIndexChanged(sender As Object, args As C1.Web.Wijmo.Controls.C1ComboBox.C1ComboBoxEventArgs) Handles C1Status.SelectedIndexChanged
        mSelectedStatus = Mid(C1Status.Text, 1, 1)
        dataTable1 = xLoadOrders()
        ' mSelectedClient = ""
    End Sub

    Private Sub C1Cliente_SelectedIndexChanged(sender As Object, args As C1.Web.Wijmo.Controls.C1ComboBox.C1ComboBoxEventArgs) Handles C1Cliente.SelectedIndexChanged
        mSelectedClient = Mid(C1Cliente.Text, 1, 8)
        ' xMessageEnc("Seleccionou o Cliente: " + mSelectedClient)
        dataTable1 = xLoadOrders()
    End Sub

    Private Sub C1Data_SelectedIndexChanged(sender As Object, args As C1.Web.Wijmo.Controls.C1ComboBox.C1ComboBoxEventArgs) Handles C1Data.SelectedIndexChanged
        mSelectedPeriod = Trim(Mid(C1Data.Text, 1, 1))
        If mSelectedPeriod = "" Or mSelectedPeriod = "9" Then
            mStartDt = "01-01-2000"
            mEndDt = Today.ToShortDateString
        ElseIf mSelectedPeriod = "1" Then
            mStartDt = DateAdd(DateInterval.Month, -1, Today).ToShortDateString
            mEndDt = Today.ToShortDateString
        ElseIf mSelectedPeriod = "2" Then
            mStartDt = DateAdd(DateInterval.Month, -3, Today).ToShortDateString
            mEndDt = Today.ToShortDateString
        ElseIf mSelectedPeriod = "3" Then
            mStartDt = DateAdd(DateInterval.Month, -12, Today).ToShortDateString
            mEndDt = Today.ToShortDateString
        End If
        ' xMessageEnc("Seleccionou o Período: " + mStartDt + " to " + mendDt)
        dataTable1 = xLoadOrders()
    End Sub

    Private Sub C1Encomendas_Sorting(sender As Object, e As C1.Web.Wijmo.Controls.C1GridView.C1GridViewSortEventArgs) Handles C1Encomendas.Sorting
        dataTable1 = xLoadOrders()
        C1Encomendas.DataSource = dataTable1
        C1Encomendas.DataBind()
    End Sub
End Class