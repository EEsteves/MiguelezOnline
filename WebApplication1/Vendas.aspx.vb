Imports Microsoft.JScript

Public Class Vendas
    Inherits System.Web.UI.Page

    Private dataTable1 As New DataTable("Facturas")
    Private PageNum As Integer
    Private mStatus As String = ""
    Private mVend As String = ""
    Private rc As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("mAgentLoggedIn") = "NO" Then
            Dim xStr As String = "~/Default.aspx"
            Response.Redirect(xStr)
            Exit Sub
        End If
        dataTable1.Columns.Add("Número")
        dataTable1.Columns.Add("Cliente")
        dataTable1.Columns.Add("Nome")
        dataTable1.Columns.Add("Vend")
        dataTable1.Columns.Add("Sit")
        dataTable1.Columns.Add("Data")
        dataTable1.Columns.Add("Valor")
        xLoadVendas()
    End Sub

    Function xLoadVendas()
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
        Dim fName As Integer = d4field(fFct, "F_NAME")
        Dim fVend As Integer = d4field(fFct, "F_VEND")
        Dim fDate As Integer = d4field(fFct, "F_INV_DT")
        Dim fTotliq As Integer = d4field(fFct, "F_TOTLIQ")
        relation = relate4init(fFct)
        If relation = 0 Then
            Return False
        End If
        If Session("mAgentOnline") = "99" Then
            expr = "DTOS(F_INV_DT) >= '20120101'"
        Else
            Dim mAgt As String = Session("mAgentOnline")
            expr = "DTOS(F_INV_DT) >= '20120101' .AND. F_VEND = '" + mAgt + "' .AND. F_STATUS $ 'BC'"
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
        Dim mPages = C1GridView1.PageCount
        C1GridView1.PageIndex = mPages - 1
        C1GridView1.DataBind()
        Return True
    End Function

    'Function xTest1()
    '    Dim fFct As Long
    '    Dim cb = code4init()
    '    fFct = d4open(cb, fPCFFCT)
    '    Dim relation As Long
    '    relation = relate4init(fFct)
    '    Call relate4querySet(relation, "F_VEND = '02  '")
    '    Dim record As Integer = relate4top(relation)
    '    Dim count As Integer = 0
    '    Do While record <> r4success
    '        count = count + 1
    '        record = relate4skip(relation, 1)
    '    Loop
    '    rc = d4close(fFct)
    '    rc = code4initUndo(cb)
    '    MsgBox("Number of records in query xTest1 = " + CStr(count))
    '    Return True
    'End Function

    Private Sub C1GridView1_PageIndexChanging(sender As Object, e As C1.Web.Wijmo.Controls.C1GridView.C1GridViewPageEventArgs) Handles C1GridView1.PageIndexChanging
        C1GridView1.PageIndex = e.NewPageIndex + 1
        C1GridView1.DataBind()
    End Sub

    Private Sub C1Menu1_ItemClick(sender As Object, e As C1.Web.Wijmo.Controls.C1Menu.C1MenuEventArgs) Handles C1Menu1.ItemClick
        Dim xRow As Integer
        xRow = Request.Form("text1")
        mDocNum = C1GridView1.Rows(xRow).Cells(1).Text
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

End Class