Imports Microsoft.JScript
Imports C1.Web.Wijmo.Controls.C1ComboBox
Imports C1.Web.Wijmo.Controls.C1GridView
Imports C1.Web.Wijmo.Controls.C1Menu
Imports System.Data.OleDb

Public Class Encomendas
    Inherits Page

    Private dataTable1 As New DataTable("Orders")
    Private mSelectedStatus As String = String.Empty
    Private mSelectedClient As String = String.Empty
    Private mSelectedAgent As String = String.Empty
    Private mSelectedPeriod As String = String.Empty
    Private mStartDt As String = String.Empty
    Private mEndDt As String = String.Empty

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("mAgentLoggedIn") = "NO" Then
            Response.Redirect("~/Default.aspx")
            Exit Sub
        End If

        mSelectedAgent = Session("mAgentOnline")
        
        ' Colunas da Tabela de Encomendas
        dataTable1.Columns.Add("Cliente")
        dataTable1.Columns.Add("Nome")
        dataTable1.Columns.Add("Numero")
        dataTable1.Columns.Add("Vend")
        dataTable1.Columns.Add("Data")
        dataTable1.Columns.Add("Sit")
        dataTable1.Columns.Add("Encnum")
        dataTable1.Columns.Add("Valor")

        If Not IsPostBack Then
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
                    ddlCliente.Items.Add(New C1ComboBoxItem(f4str(fCliCode) + Trim(mCliNm)))
                End If
                rc = d4skip(fCli, 1)
            End While
            rc = d4close(fCli)
            rc = code4initUndo(cb)

            ''''Carrega o dropdown de Situações
            FillSituaçõesDropDown()

            ''''Carrega o dropdown de Datas
            FillDataPeriodDropDown()

            mSelectedStatus = Mid(ddlStatus.Text, 1, 1)
            
            ''''Get Start date and end date as per the selected parameter
            GetDateValues()

            ''''Gets orders from database
            LoadOrders()

            ' Go to last page - 1
            Dim mPages = C1Encomendas.PageCount
            C1Encomendas.PageIndex = mPages - 2
            mRow = 0
        End If

        mSelectedClient = Mid(ddlCliente.Text, 1, 8)
    End Sub

    ''' <summary>
    ''' Get Orders from database
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function LoadOrders()
        Dim connStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:\PCFFILES\DATA\';Extended Properties=dBase 5.0"
        Dim conn As New OleDbConnection(connStr)
        Dim cmd As New OleDbCommand()
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

        mC0 = mC0 + "AND E_DATE BETWEEN CDATE('" + mStartDt + "') AND CDATE('" + mEndDt + "') "
        ' mC0 = mC0 + "ORDER BY E_DATE"

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

        C1Encomendas.DataSource = dataTable1
        C1Encomendas.DataBind()

        C1Encomendas.AllowKeyboardNavigation = True
        C1Encomendas.AllowPaging = True
        reader.Close()
        conn.Close()

        Return dataTable1
    End Function

    Private Sub C1Menu1_ItemClick(sender As Object, e As C1MenuEventArgs) Handles C1Menu1.ItemClick
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

    Private Sub C1Encomendas_PageIndexChanging(sender As Object, e As C1GridViewPageEventArgs) Handles C1Encomendas.PageIndexChanging
        C1Encomendas.PageIndex = e.NewPageIndex

        ''''Get Start date and end date as per the selected parameter
        GetDateValues()

        LoadOrders()
    End Sub

    Private Sub ddlStatus_SelectedIndexChanged(sender As Object, args As C1ComboBoxEventArgs) Handles ddlStatus.SelectedIndexChanged
        mSelectedStatus = Mid(ddlStatus.Text, 1, 1)

        ''''Get Start date and end date as per the selected parameter
        GetDateValues()

        LoadOrders()
    End Sub

    Private Sub ddlCliente_SelectedIndexChanged(sender As Object, args As C1ComboBoxEventArgs) Handles ddlCliente.SelectedIndexChanged
        mSelectedClient = Mid(ddlCliente.Text, 1, 8)

        ''''Get Start date and end date as per the selected parameter
        GetDateValues()

        LoadOrders()
    End Sub

    Private Sub ddlDataPeriod_SelectedIndexChanged(sender As Object, args As C1ComboBoxEventArgs) Handles ddlDataPeriod.SelectedIndexChanged
        mSelectedPeriod = Trim(Mid(ddlDataPeriod.Text, 1, 1))

        ''''Get Start date and end date as per the selected parameter
        GetDateValues()

        LoadOrders()
    End Sub

    Private Sub C1Encomendas_Sorting(sender As Object, e As C1GridViewSortEventArgs) Handles C1Encomendas.Sorting
        ''''Get Start date and end date as per the selected parameter
        GetDateValues()

        LoadOrders()
    End Sub

    ''' <summary>
    ''' Get Start date and end date as per the selected parameter
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDateValues()
        mSelectedPeriod = Mid(ddlDataPeriod.Text, 1, 1)
        mSelectedStatus = Mid(ddlStatus.Text, 1, 1)

        If mSelectedPeriod = String.Empty Or mSelectedPeriod = "9" Then
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
    End Sub

    ''' <summary>
    ''' Carrega o dropdown de Situações
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillSituaçõesDropDown()
        ddlStatus.Items.Add(New C1ComboBoxItem("A - Abertas"))
        ddlStatus.Items.Add(New C1ComboBoxItem("C - Fechadas"))
        ddlStatus.Items.Add(New C1ComboBoxItem("X - Todas"))
        ddlStatus.Text = "A - Abertas"
    End Sub

    ''' <summary>
    ''' Carrega o dropdown de Datas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillDataPeriodDropDown()
        ddlDataPeriod.Items.Add(New C1ComboBoxItem("1 - Ultimo Mês"))
        ddlDataPeriod.Items.Add(New C1ComboBoxItem("2 - Ultimo Trimestre"))
        ddlDataPeriod.Items.Add(New C1ComboBoxItem("3 - Ultimo Ano"))
        ddlDataPeriod.Items.Add(New C1ComboBoxItem("9 - Todas"))
        ddlDataPeriod.Text = "2 - Ultimo Trimestre"
    End Sub
End Class