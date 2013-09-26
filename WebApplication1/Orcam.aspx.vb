Imports Microsoft.JScript
Imports C1.Web.Wijmo.Controls.C1ComboBox
Imports System.Data.OleDb
Imports C1.Web.Wijmo.Controls.C1GridView

Public Class Orcam
    Inherits System.Web.UI.Page

    Dim dataTable1 As New DataTable("Orcam")
    Dim PageNum As Integer
    Dim rc As Long = 0
    Private selectedStatus As String = String.Empty
    Private selectedClient As String = String.Empty
    Private selectedAgent As String = String.Empty
    Private selectedPeriod As String = String.Empty
    Dim startDate As String = String.Empty
    Dim endDate As String = String.Empty

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("mAgentLoggedIn") = "NO" Then
            Dim xStr As String = "~/Default.aspx"
            Response.Redirect(xStr)
            Exit Sub
        End If

        selectedAgent = Session("mAgentOnline")

        ' Colunas da Tabela de Orçamentos
        dataTable1.Columns.Add("Número")
        dataTable1.Columns.Add("Cliente")
        dataTable1.Columns.Add("Nome")
        dataTable1.Columns.Add("Sit")
        dataTable1.Columns.Add("Vend")
        dataTable1.Columns.Add("Data")
        dataTable1.Columns.Add("Valor")

        If (Not IsPostBack) Then
            ''''Carrega o dropdown de Situações
            FillSituaçõesDropDown()

            ''''Carrega o dropdown de Datas
            FillDataPeriodDropDown()

            FillClientDropDown()

            ''''Get Start date and end date as per the selected parameter
            GetDateValues()

            LoadOrcam()
        End If
        mRow = 0
    End Sub

    Function LoadOrcam()
        Dim connStr As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='C:\PCFFILES\DATA\';Extended Properties=""dBase 5.0;HDR=Yes;IMEX=1"""
        Dim conn As New OleDbConnection(connStr)
        Dim cmd As New OleDbCommand()
        cmd.Connection = conn

        Dim sqlString As String = "SELECT K_STATUS, K_ORCAM, K_CLIENT, K_NAME, K_AGENTE, K_ORC_DT, K_TOTLIQ FROM PCFORC WHERE "

        If selectedStatus = "X" Then
            sqlString += "K_STATUS LIKE '[ABCD]%' "
        Else
            sqlString += "K_STATUS = '" + selectedStatus + "' "
        End If

        If Len(Trim(selectedAgent)) > 0 Then
            sqlString += "AND K_AGENTE = '" + selectedAgent + "' "
        End If

        If Len(Trim(selectedClient)) > 0 Then
            sqlString += "AND K_CLIENT = '" + selectedClient + "' "
        End If

        sqlString += "AND K_ORC_DT BETWEEN CDATE('" + startDate + "') AND CDATE('" + endDate + "') "

        cmd.CommandText = sqlString
        conn.Open()

        Dim reader As OleDb.OleDbDataReader = cmd.ExecuteReader
        Dim dataTable1 As New DataTable
        dataTable1.Load(reader)

        grdOrcam.Columns(0).ItemStyle.HorizontalAlign = HorizontalAlign.Center ' Numero
        grdOrcam.Columns(1).ItemStyle.HorizontalAlign = HorizontalAlign.Center ' Cliente
        grdOrcam.Columns(3).ItemStyle.HorizontalAlign = HorizontalAlign.Center ' Vendedor
        grdOrcam.Columns(4).ItemStyle.HorizontalAlign = HorizontalAlign.Center ' Data     
        grdOrcam.Columns(5).ItemStyle.HorizontalAlign = HorizontalAlign.Center ' Sit
        grdOrcam.Columns(6).ItemStyle.HorizontalAlign = HorizontalAlign.Right ' Valor

        grdOrcam.DataSource = dataTable1
        grdOrcam.DataBind()

        reader.Close()
        conn.Close()
        Return True
    End Function

    Private Sub grdOrcam_PageIndexChanging(sender As Object, e As C1GridViewPageEventArgs) Handles grdOrcam.PageIndexChanging
        grdOrcam.PageIndex = e.NewPageIndex

        ''''Get Start date and end date as per the selected parameter
        GetDateValues()

        LoadOrcam()
    End Sub

    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, args As C1ComboBoxEventArgs) Handles ddlStatus.SelectedIndexChanged
        selectedStatus = Mid(ddlStatus.Text, 1, 1)

        ''''Populate grid control from database values
        PopulateGridControl()
    End Sub

    Protected Sub ddlCliente_SelectedIndexChanged(sender As Object, args As C1ComboBoxEventArgs) Handles ddlCliente.SelectedIndexChanged
        selectedClient = Mid(ddlCliente.Text, 1, 8)

        ''''Populate grid control from database values
        PopulateGridControl()
    End Sub

    Protected Sub ddlDataYear_SelectedIndexChanged(sender As Object, args As C1ComboBoxEventArgs) Handles ddlDataYear.SelectedIndexChanged
        selectedPeriod = Trim(Mid(ddlDataYear.Text, 1, 1))

        ''''Populate grid control from database values
        PopulateGridControl()
    End Sub

    Private Sub C1Menu1_ItemClick(sender As Object, e As C1.Web.Wijmo.Controls.C1Menu.C1MenuEventArgs) Handles C1Menu1.ItemClick
        Dim xRow As Integer
        xRow = Request.Form("text1")
        mProdNum = grdOrcam.Rows(xRow).Cells(1).Text

        If e.Item.Text = "Visualiza" Then
            'Dim xStr As String = "~/ClientesDetail.aspx?field1=" + Trim(mCliNum)
            'Response.Redirect(xStr)
            xMessageOrc("Ainda não está implementado")
        ElseIf e.Item.Text = "Estatísticas" Then
            'Dim xStr As String = "~/ClientesStats.aspx?field1=" + Trim(mCliNum)
            'Response.Redirect(xStr)
            xMessageOrc("Ainda não está implementado")
        ElseIf e.Item.Text = "Lista de Orçamentos" Then
            'Dim xStr As String = "~/ClientesStats.aspx?field1=" + Trim(mCliNum)
            'Response.Redirect(xStr)
            xMessageOrc("Ainda não está implementado")
        ElseIf e.Item.Text = "Imprime Orçamento" Then
            'Dim xStr As String = "~/ClientesStats.aspx?field1=" + Trim(mCliNum)
            'Response.Redirect(xStr)
            xMessageOrc("Ainda não está implementado")
        Else
            xMessageOrc("Opção inválida")
        End If
    End Sub

    Function xMessageOrc(xMsg)
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

    Private Sub FillClientDropDown()
        ' Carrega o dropdown de Clientes
        Dim cb = code4init()
        Dim fCli As Integer = d4open(cb, fPCFCLI)
        Dim tag As Integer = d4tag(fCli, "PCFCLI2")
        Call d4tagSelect(fCli, tag)
        Dim fCliCode As Integer = d4field(fCli, "C_CLIENT")
        Dim fName As Integer = d4field(fCli, "C_NAME")
        Dim fVend As Integer = d4field(fCli, "C_VEND")

        rc = d4top(fCli)

        While d4eof(fCli) = 0
            mCliNm = f4str(fName)
            mCliVend = f4str(fVend)

            If selectedAgent = "99" Then
                mSelect = True
            Else
                If Trim(mCliVend) = selectedAgent Then
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
    End Sub

    ''' <summary>
    ''' Get Start date and end date as per the selected parameter
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDateValues()
        selectedPeriod = Mid(ddlDataYear.Text, 1, 1)
        selectedStatus = Mid(ddlStatus.Text, 1, 1)
        selectedClient = Mid(ddlCliente.Text, 1, 8)

        Dim mStartDate As DateTime
        Dim mEndDate As DateTime

        If selectedPeriod = String.Empty Or selectedPeriod = "9" Then
            mStartDate = DateTime.MinValue
            mEndDate = Today.ToShortDateString
        ElseIf selectedPeriod = "1" Then
            mStartDate = DateAdd(DateInterval.Month, -1, Today)
            mEndDate = Today.ToShortDateString
        ElseIf selectedPeriod = "2" Then
            mStartDate = DateAdd(DateInterval.Month, -3, Today)
            mEndDate = Today.ToShortDateString
        ElseIf selectedPeriod = "3" Then
            mStartDate = DateAdd(DateInterval.Month, -12, Today)
            mEndDate = Today.ToShortDateString
        End If

        startDate = String.Format("{0:dd-MM-yyyy}", mStartDate)
        endDate = String.Format("{0:dd-MM-yyyy}", mEndDate)
    End Sub

    ''' <summary>
    ''' Carrega o dropdown de Situações
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillSituaçõesDropDown()
        ddlStatus.Items.Add(New C1ComboBoxItem("A - Abertos"))
        ddlStatus.Items.Add(New C1ComboBoxItem("B - Submetidos"))
        ddlStatus.Items.Add(New C1ComboBoxItem("C - Fechados"))
        ddlStatus.Text = "B - Submetidos"
    End Sub

    ''' <summary>
    ''' Carrega o dropdown de Datas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillDataPeriodDropDown()
        ddlDataYear.Items.Add(New C1ComboBoxItem("1 - Ultimo Mês"))
        ddlDataYear.Items.Add(New C1ComboBoxItem("2 - Ultimo Trimestre"))
        ddlDataYear.Items.Add(New C1ComboBoxItem("3 - Ultimo Ano"))
        ddlDataYear.Items.Add(New C1ComboBoxItem("9 - Todas"))
        ddlDataYear.Text = "2 - Ultimo Trimestre"
    End Sub

    ''' <summary>
    ''' Populate grid control from database values
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PopulateGridControl()
        ''''Get Start date and end date as per the selected parameter
        GetDateValues()

        ' Go to last page
        NavigateGridToLastPage()

        ''''Gets orders from database
        LoadOrcam()
    End Sub

    ''' <summary>
    ''' Go to last page in the grid
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub NavigateGridToLastPage()
        grdOrcam.PageIndex = Int32.MaxValue
        grdOrcam.AllowKeyboardNavigation = True
    End Sub
End Class