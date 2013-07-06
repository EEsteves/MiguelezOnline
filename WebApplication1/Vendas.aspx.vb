Imports Microsoft.JScript
Imports C1.Web.Wijmo.Controls.C1GridView
Imports C1.Web.Wijmo.Controls.C1Menu
Imports C1.Web.Wijmo.Controls.C1ComboBox

Public Class Vendas
    Inherits Page

    Private dataTable1 As New DataTable("Facturas")
    Private PageNum As Integer
    Private mStatus As String = String.Empty
    Private mVend As String = String.Empty
    Private mSelectedPeriod As String = String.Empty
    Private mSelectedClient As String = String.Empty
    Dim startDate As String = String.Empty
    Dim endDate As String = String.Empty
    Private rc As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("mAgentLoggedIn") = "NO" Then
            Response.Redirect("~/Default.aspx")
            Exit Sub
        End If

        mVend = Session("mAgentOnline")

        dataTable1.Columns.Add("Número")
        dataTable1.Columns.Add("Cliente")
        dataTable1.Columns.Add("Nome")
        dataTable1.Columns.Add("Vend")
        dataTable1.Columns.Add("Sit")
        dataTable1.Columns.Add("Data")
        dataTable1.Columns.Add("Valor")

        If Not IsPostBack Then
            ''''Fill Situações DropDown
            FillSituaçõesDropDown()

            '''' Fill Data (Ano) DropDown
            FillDataAnoDropDown()

            ' Carrega o dropdown de Clientes
            FillClientDropDown()

            mStatus = Mid(ddlStatus.Text, 1, 1)

            LoadVendas()
        End If

        mSelectedClient = Mid(ddlCliente.Text, 1, 8)
    End Sub

    Function LoadVendas()
        Dim row As DataRow
        Dim expr As String
        Dim relation As Long
        Dim fFct As Long
        dataTable1.Clear()
        Dim cb = code4init()

        fFct = d4open(cb, fPCFFCT)

        Dim fStatus As Integer = d4field(fFct, "F_STATUS")
        Dim fNumber As Integer = d4field(fFct, "F_NUMBER")
        Dim fClient As Integer = d4field(fFct, "F_CLIENT")
        fName = d4field(fFct, "F_NAME")
        fVend = d4field(fFct, "F_VEND")
        Dim fDate As Integer = d4field(fFct, "F_INV_DT")
        Dim fTotliq As Integer = d4field(fFct, "F_TOTLIQ")
        relation = relate4init(fFct)

        If relation = 0 Then
            Return False
        End If

        ''''Get Start date and end date as per the selected parameter
        GetDateValues()

        If Session("mAgentOnline") = "99" Then
            expr = "DTOS(F_INV_DT) >= '20120101'"
        Else
            Dim mAgt As String = Session("mAgentOnline")
            expr = "DTOS(F_INV_DT) >= '" + startDate + "' .AND. DTOS(F_INV_DT) <= '" + endDate + "' "
            expr += ".AND. F_VEND = '" + mAgt + "' .AND. F_STATUS = '" + mStatus + "' "

            If Not String.IsNullOrEmpty(mSelectedClient) Then
                expr += ".AND. F_CLIENT = '" + mSelectedClient + "' "
            End If
        End If

        Dim order As String = "F_INV_DT"
        rc = relate4querySet(relation, expr)
        rc = relate4sortSet(relation, order)
        rc = relate4top(relation)

        Dim count As Integer = 0
        Do While rc = r4success
            count = count + 1
            row = dataTable1.NewRow()
            row("Número") = f4str(fNumber)
            row("Cliente") = f4str(fClient)
            row("Nome") = f4str(fName)
            row("Sit") = f4str(fStatus)
            row("Vend") = f4str(fVend)
            Dim xDate As String = f4str(fDate)
            row("Data") = Mid(xDate, 7, 2) + "/" + Mid(xDate, 5, 2) + "/" + Mid(xDate, 1, 4)
            row("Valor") = Format(f4long(fTotliq), "###,##0.00")
            dataTable1.Rows.Add(row)
            rc = relate4skip(relation, 1)
        Loop

        rc = relate4free(relation, 0)
        rc = d4close(fFct)
        rc = code4initUndo(cb)

        C1GridView1.DataSource = dataTable1
        C1GridView1.DataBind()

        ' Go to last page
        C1GridView1.PageIndex = C1GridView1.PageCount

        Return True
    End Function

    Private Sub C1GridView1_PageIndexChanging(sender As Object, e As C1GridViewPageEventArgs) Handles C1GridView1.PageIndexChanging
        C1GridView1.PageIndex = e.NewPageIndex

        ''''Get Start date and end date as per the selected parameter
        GetDateValues()

        LoadVendas()
    End Sub

    Private Sub C1Menu1_ItemClick(sender As Object, e As C1MenuEventArgs) Handles C1Menu1.ItemClick
        Dim xRow As Integer
        xRow = Request.Form("text1")
        mDocNum = C1GridView1.Rows(xRow).Cells(0).Text

        If e.Item.Text = "Visualiza" Then
            Dim xStr As String = "~/VendasDetail.aspx?field1=" + Trim(mDocNum)
            Response.Redirect(xStr)
        ElseIf e.Item.Text = "Estatísticas" Then
            Dim xStr As String = "~/VendasStats.aspx"
            Response.Redirect(xStr)
        ElseIf e.Item.Text = "Imprime Lista de Documentos" Then
            Dim xStr As String = "~/VendasListaDocumentos.aspx"
            Response.Redirect(xStr)
        ElseIf e.Item.Text = "Imprime Documento" Then
            Dim xStr As String = "~/VendasPrintDoc.aspx?field1=" + Trim(mDocNum)
        Else
            xMessageVen("Opção inválida")
        End If
    End Sub

    Function xMessageVen(xMsg)
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

    ''' <summary>
    ''' Fill Situações DropDown
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillSituaçõesDropDown()
        ddlStatus.Items.Add(New C1ComboBoxItem("A - Abertas"))
        ddlStatus.Items.Add(New C1ComboBoxItem("B - Pendentes"))
        ddlStatus.Items.Add(New C1ComboBoxItem("C - Fechadas"))
        ddlStatus.Text = "B - Pendentes"
    End Sub

    ''' <summary>
    ''' Fill Data (Ano) DropDown
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillDataAnoDropDown()
        ddlDataYear.Items.Add(New C1ComboBoxItem("1 - Ultimo Mês"))
        ddlDataYear.Items.Add(New C1ComboBoxItem("2 - Ultimo Trimestre"))
        ddlDataYear.Items.Add(New C1ComboBoxItem("3 - Ultimo Ano"))
        ddlDataYear.Items.Add(New C1ComboBoxItem("9 - Todas"))
        ddlDataYear.Text = "2 - Ultimo Trimestre"
    End Sub

    Private Sub FillClientDropDown()
        Dim cb = code4init()

        ' Carrega o dropdown de Clientes
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

            If mVend = "99" Then
                mSelect = True
            Else
                If Trim(mCliVend) = mVend Then
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

    Private Sub ddlStatus_SelectedIndexChanged(sender As Object, args As C1ComboBoxEventArgs) Handles ddlStatus.SelectedIndexChanged
        mStatus = Mid(ddlStatus.Text, 1, 1)

        ''''Get Start date and end date as per the selected parameter
        GetDateValues()

        LoadVendas()
    End Sub

    Private Sub ddlCliente_SelectedIndexChanged(sender As Object, args As C1ComboBoxEventArgs) Handles ddlCliente.SelectedIndexChanged
        mSelectedClient = Mid(ddlCliente.Text, 1, 8)

        ''''Get Start date and end date as per the selected parameter
        GetDateValues()

        LoadVendas()
    End Sub

    Private Sub ddlDataPeriod_SelectedIndexChanged(sender As Object, args As C1ComboBoxEventArgs) Handles ddlDataYear.SelectedIndexChanged
        mSelectedPeriod = Trim(Mid(ddlDataYear.Text, 1, 1))

        ''''Get Start date and end date as per the selected parameter
        GetDateValues()

        LoadVendas()
    End Sub

    ''' <summary>
    ''' Get Start date and end date as per the selected parameter
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDateValues()
        mSelectedPeriod = Mid(ddlDataYear.Text, 1, 1)
        mStatus = Mid(ddlStatus.Text, 1, 1)

        Dim mStartDate As DateTime
        Dim mEndDate As DateTime

        If mSelectedPeriod = String.Empty Or mSelectedPeriod = "9" Then
            mStartDate = DateTime.MinValue
            mEndDate = Today.ToShortDateString
        ElseIf mSelectedPeriod = "1" Then
            mStartDate = DateAdd(DateInterval.Month, -1, Today)
            mEndDate = Today.ToShortDateString
        ElseIf mSelectedPeriod = "2" Then
            mStartDate = DateAdd(DateInterval.Month, -3, Today)
            mEndDate = Today.ToShortDateString
        ElseIf mSelectedPeriod = "3" Then
            mStartDate = DateAdd(DateInterval.Month, -12, Today)
            mEndDate = Today.ToShortDateString
        End If

        startDate = String.Format("{0:yyyyMMdd}", mStartDate)
        endDate = String.Format("{0:yyyyMMdd}", mEndDate)
    End Sub
End Class