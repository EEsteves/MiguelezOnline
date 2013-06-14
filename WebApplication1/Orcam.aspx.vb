Imports Microsoft.JScript
Imports C1.Web.Wijmo.Controls.C1ComboBox

Public Class Orcam
    Inherits System.Web.UI.Page

    Private dataTable1 As New DataTable("Orcam")
    Private PageNum As Integer
    Private rc As Integer = 0
    Private mStatus As String = ""
    Private mVend As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("mAgentLoggedIn") = "NO" Then
            Dim xStr As String = "~/Default.aspx"
            Response.Redirect(xStr)
            Exit Sub
        End If

        ' Colunas da Tabela de Orçamentos
        dataTable1.Columns.Add("Número")
        dataTable1.Columns.Add("Cliente")
        dataTable1.Columns.Add("Nome")
        dataTable1.Columns.Add("Sit")
        dataTable1.Columns.Add("Vend")
        dataTable1.Columns.Add("Data")
        dataTable1.Columns.Add("Valor")


        ' Grid1.ScrollMode = C1.Web.Wijmo.Controls.C1GridView.ScrollMode.Vertical
        ' Grid1.AutogenerateColumns = True

        ' Carrega o dropdown de Clientes
        Dim cb = code4init()
        Dim fOrc As Integer = d4open(cb, fPCFCLI)
        Dim tag As Integer = d4tag(fOrc, "PCFCLI2")
        Call d4tagSelect(fOrc, tag)
        Dim fName As Integer = d4field(fOrc, "C_NAME")
        Dim fStatus As Integer = d4field(fOrc, "C_STATUS")
        rc = d4top(fOrc)
        While d4eof(fOrc) = 0
            C1ComboBox2.Items.Add(New C1ComboBoxItem(Trim(f4str(fName))))
            rc = d4skip(fOrc, 1)
        End While
        rc = d4close(fOrc)
        rc = code4initUndo(cb)

        xLoadOrcam()
        mRow = 0
    End Sub

    Function xLoadOrcam()
        Dim row As DataRow
        Dim expr As String
        Dim relation As Long
        Dim fOrc As Long
        dataTable1.Clear()
        Dim cb = code4init()
        fOrc = d4open(cb, fPCFORC)
        Dim fStatus As Integer = d4field(fOrc, "K_STATUS")
        Dim fNumber As Integer = d4field(fOrc, "K_ORCAM")
        Dim fClient As Integer = d4field(fOrc, "K_CLIENT")
        Dim fName As Integer = d4field(fOrc, "K_NAME")
        Dim fVend As Integer = d4field(fOrc, "K_AGENTE")
        Dim fDate As Integer = d4field(fOrc, "K_ORC_DT")
        Dim fTotliq As Integer = d4field(fOrc, "K_TOTLIQ")
        relation = relate4init(fOrc)
        If relation = 0 Then
            Return False
        End If
        If Session("mAgentOnline") = "99" Then
            expr = "DTOS(K_ORC_DT) >= '20120101'"
        Else
            Dim mAgt As String = Session("mAgentOnline")
            expr = "DTOS(K_ORC_DT) >= '20120101' .AND. K_AGENTE = '" + mAgt + "' .AND. K_STATUS <> 'A'"
        End If
        Dim order As String = "K_ORC_DT"
        rc = relate4querySet(relation, expr)
        rc = relate4sortSet(relation, order)
        rc = relate4top(relation)
        Dim count As Integer = 0
        Do While rc = r4success
            row = dataTable1.NewRow()
            row("Número") = f4str(fNumber)
            row("Cliente") = f4str(fClient)
            row("Nome") = f4str(fName)
            row("Sit") = f4str(fStatus)
            row("Vend") = f4str(fVend)
            Dim xDate As String = f4str(fDate)
            row("Data") = Mid(xDate, 7, 2) + "/" + Mid(xDate, 5, 2) + "/" + Mid(xDate, 1, 4)

            ' Dim st As String = Format(f4long(fTotliq), "#,###,##0.00")
            ' row("Valor") = st.PadLeft(30 - Len(st), " ")

            v1 = f4double(fTotliq)
            row("Valor") = v1


            dataTable1.Rows.Add(row)
            rc = relate4skip(relation, 1)
        Loop
        rc = relate4free(relation, 0)
        rc = d4close(fOrc)
        rc = code4initUndo(cb)
        C1GridView1.DataSource = dataTable1
        C1GridView1.DataBind()
        Dim mPages = C1GridView1.PageCount
        C1GridView1.PageIndex = mPages - 1
        C1GridView1.DataBind()

        'Dim ccc As Integer = C1GridView1.Columns.Count
        'MsgBox(CStr(ccc))

        'C1GridView1.Columns(5).ItemStyle.HorizontalAlign = HorizontalAlign.Right

        Return True
    End Function

    Private Sub C1GridView1_PageIndexChanging(sender As Object, e As C1.Web.Wijmo.Controls.C1GridView.C1GridViewPageEventArgs) Handles C1GridView1.PageIndexChanging
        C1GridView1.PageIndex = e.NewPageIndex
        C1GridView1.DataBind()
    End Sub

    Private Sub C1Menu1_ItemClick(sender As Object, e As C1.Web.Wijmo.Controls.C1Menu.C1MenuEventArgs) Handles C1Menu1.ItemClick
        Dim xRow As Integer
        xRow = Request.Form("text1")
        mProdNum = C1GridView1.Rows(xRow).Cells(1).Text
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

    Private Sub C1GridView1_PreRender(sender As Object, e As System.EventArgs) Handles C1GridView1.PreRender
        'Dim col As New C1.Web.Wijmo.Controls.C1GridView.
        'C1GridView1.Columns(6).ItemStyle.HorizontalAlign = HorizontalAlign.Right
    End Sub
End Class