Imports C1.Web.Wijmo.Controls.C1GridView

Public Class VendasDetail
    Inherits System.Web.UI.Page

    Dim selectedVendas As String = String.Empty
    Dim dtVendasDetails As New DataTable("Facturas")
    Dim selectedAgent As String = String.Empty
    Dim rc As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        selectedAgent = Session("mAgentOnline")

        ' Colunas da Tabela de Encomendas
        dtVendasDetails.Columns.Add("Produto")
        dtVendasDetails.Columns.Add("Nome")
        dtVendasDetails.Columns.Add("Quant")
        dtVendasDetails.Columns.Add("Unit")
        dtVendasDetails.Columns.Add("Valor")
        dtVendasDetails.Columns.Add("Descontos")
        dtVendasDetails.Columns.Add("ValorcomIVA")

        If IsPostBack = False Then
            selectedVendas = Request.QueryString("field1")
            If selectedVendas IsNot Nothing Then
                txtSelectedVendas.Text = selectedVendas

                ShowVendasHeader(selectedVendas)
                ShowVendasDetails(selectedVendas)
            End If
        End If
    End Sub

    Private Sub C1Menu1_ItemClick(sender As Object, e As C1.Web.Wijmo.Controls.C1Menu.C1MenuEventArgs) Handles C1Menu1.ItemClick
        If e.Item.Text = "< Anterior" Then
            ' Atenção: este «Anterior» vai apanhar todos os registos independentemente do critério de selecção
            ' indicado no ecrã anterior
            GetPreviousRecords()
        ElseIf e.Item.Text = "Seguinte >" Then
            ' Atenção: este «Seguinte» vai apanhar todos os registos independentemente do critério de selecção
            ' indicado no ecrã anterior
            GetNextRecords()
        ElseIf e.Item.Text = "Imprime Encomenda" Then
            'Dim xStr As String = "~/EncomendasPrint.aspx?field1=" + Trim(mOrderNum)
            'Response.Redirect(xStr)
            '  xMessageEnc("Ainda não está implementado")
        Else
            ' Substituir esta msgbox
            '  xMessageEnc("Opção errada " & e.Item.Text)
        End If
    End Sub

    Private Sub GetNextRecords()
        Dim connStr As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='C:\PCFFILES\DATA\';Extended Properties=""dBase 5.0;HDR=Yes;IMEX=1"""
        Dim conn As New OleDb.OleDbConnection(connStr)
        Dim cmd As New OleDb.OleDbCommand()
        cmd.Connection = conn

        selectedVendas = txtSelectedVendas.Text
        sqlString = "SELECT TOP 1 F_NUMBER FROM PCFFCT "
        sqlString += "WHERE F_NUMBER > '" + selectedVendas + "' "

        If Len(Trim(selectedAgent)) > 0 Then
            sqlString += "AND F_VEND = '" + selectedAgent + "' "
        End If

        sqlString += "ORDER BY F_NUMBER"

        cmd.CommandText = sqlString
        conn.Open()

        Dim reader As OleDb.OleDbDataReader = cmd.ExecuteReader
        reader.Read()

        selectedVendas = Convert.ToString(reader("F_NUMBER"))

        reader.Close()
        conn.Close()

        txtSelectedVendas.Text = selectedVendas

        ShowVendasHeader(selectedVendas)
        ShowVendasDetails(selectedVendas)
    End Sub

    Private Sub GetPreviousRecords()
        Dim connStr As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='C:\PCFFILES\DATA\';Extended Properties=""dBase 5.0;HDR=Yes;IMEX=1"""
        Dim conn As New OleDb.OleDbConnection(connStr)
        Dim cmd As New OleDb.OleDbCommand()
        cmd.Connection = conn
        selectedVendas = txtSelectedVendas.Text

        Dim sqlString As String = "SELECT TOP 1 F_NUMBER FROM PCFFCT "
        sqlString += "WHERE F_NUMBER < '" + selectedVendas + "' "

        If Len(Trim(selectedAgent)) > 0 Then
            sqlString += "AND F_VEND = '" + selectedAgent + "' "
        End If

        sqlString = sqlString + "ORDER BY F_NUMBER DESC"
        cmd.CommandText = sqlString
        conn.Open()

        Dim reader As OleDb.OleDbDataReader = cmd.ExecuteReader
        reader.Read()

        selectedVendas = Convert.ToString(reader("F_NUMBER"))
        txtSelectedVendas.Text = selectedVendas

        reader.Close()
        conn.Close()

        ShowVendasHeader(selectedVendas)
        ShowVendasDetails(selectedVendas)
    End Sub

    Private Sub ShowVendasHeader(selectedVendas As String)
        Dim connStr As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='C:\PCFFILES\DATA\';Extended Properties=""dBase 5.0;HDR=Yes;IMEX=1"""
        Dim conn As New OleDb.OleDbConnection(connStr)
        Dim cmd As New OleDb.OleDbCommand()
        cmd.Connection = conn

        Dim sqlString As String = "SELECT F_NUMBER, F_TYPE, F_CLIENT, F_INV_DT, F_DAYS_PP, F_TOTLIQ, "
        sqlString += "F_IVA, F_NAME, F_ADDR_1, F_ADDR_2, F_CITY, F_POSTAL, F_PTL_NM, F_COUNTRY "
        sqlString += "FROM PCFFCT WHERE F_NUMBER = '" + selectedVendas + "' "
        cmd.CommandText = sqlString

        conn.Open()
        Dim reader As OleDb.OleDbDataReader = cmd.ExecuteReader

        While reader.Read()
            lblNumero.Text = Convert.ToString(reader("F_NUMBER"))
            lblCliente.Text = Convert.ToString(reader("F_CLIENT"))
            lblData.Text = Convert.ToDateTime(reader("F_INV_DT"))
            lblCondPage.Text = Convert.ToString(reader("F_DAYS_PP"))
            'lblVencimento.Text = Convert.ToDateTime(reader("F_VENC_DT"))
            'lblAtrazo.Text = DateTime.Now.Subtract(Convert.ToDateTime(reader("F_VENC_DT"))).ToString()

            Dim totalLiq As Decimal = Convert.ToDecimal(reader("F_TOTLIQ"))
            Dim vatAmount As Decimal = Convert.ToDecimal(reader("F_IVA"))

            lblTotSemIVA.Text = Format(totalLiq - vatAmount, "###,##0.00")
            lblIVA.Text = Format(vatAmount, "###,##0.00")
            lblTotalComIVA.Text = Format(totalLiq, "###,##0.00")

            lblTipoDeDocumento.Text = GetTextForTipoDeDocumento(Convert.ToString(reader("F_TYPE")))

            lblNome.Text = Convert.ToString(reader("F_NAME"))
            Dim address As String = Convert.ToString(reader("F_ADDR_1")) + ", " + _
                Convert.ToString(reader("F_ADDR_2")) + ", " + _
                Convert.ToString(reader("F_CITY")) + ", " + _
                Convert.ToString(reader("F_POSTAL")) + ", " + _
                Convert.ToString(reader("F_PTL_NM")) + ", " + _
                Convert.ToString(reader("F_COUNTRY"))

            lblAddress.Text = address
        End While

        reader.Close()
        conn.Close()
    End Sub

    Private Sub ShowVendasDetails(selectedVendas As String)
        Dim connStr As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='C:\PCFFILES\DATA\';Extended Properties=""dBase 5.0;HDR=Yes;IMEX=1"""
        Dim conn As New OleDb.OleDbConnection(connStr)
        Dim cmd As New OleDb.OleDbCommand()
        cmd.Connection = conn

        mC0 = "SELECT A_PRODUCT AS Produto, A_DESC_1 AS Nome, A_QUANT AS Quant, FORMAT(A_UNIT,'###,##0.00') AS Unit, A_QUANT * A_UNIT AS Valor, " & _
        "FORMAT(A_DESCT + A_DESCT2 + A_DESCT3 + A_DESCT4,'###,##0.00') AS Descontos, FORMAT(A_IVA, '###,##0.00') AS ValorcomIVA FROM PCFMOV "

        mC0 += "WHERE A_NUMBER = '" + selectedVendas + "' "
        mC0 = mC0 + "ORDER BY A_NUMBER"

        cmd.CommandText = mC0
        conn.Open()

        Dim reader As OleDb.OleDbDataReader = cmd.ExecuteReader
        Dim dataTable1 As New DataTable

        dataTable1.Load(reader)

        grdVendasDetails.Columns(0).ItemStyle.HorizontalAlign = HorizontalAlign.Left ' Producto
        grdVendasDetails.Columns(1).ItemStyle.HorizontalAlign = HorizontalAlign.Left ' Nome
        grdVendasDetails.Columns(2).ItemStyle.HorizontalAlign = HorizontalAlign.Right ' Quant
        grdVendasDetails.Columns(3).ItemStyle.HorizontalAlign = HorizontalAlign.Right ' Unit
        grdVendasDetails.Columns(4).ItemStyle.HorizontalAlign = HorizontalAlign.Right ' Valor     
        grdVendasDetails.Columns(5).ItemStyle.HorizontalAlign = HorizontalAlign.Right ' Descontos
        grdVendasDetails.Columns(6).ItemStyle.HorizontalAlign = HorizontalAlign.Right ' Valor com IVA

        grdVendasDetails.DataSource = dataTable1
        grdVendasDetails.ScrollMode = C1.Web.Wijmo.Controls.C1GridView.ScrollMode.Vertical
        grdVendasDetails.DataBind()
        grdVendasDetails.PageSize = 9

        reader.Close()
        conn.Close()
    End Sub

    Private Function ShowVendasDetails1(selectedVendas As String)
        Dim row As DataRow
        Dim expr As String
        Dim relation As Long
        Dim fMov As Long
        dtVendasDetails.Clear()
        Dim cb = code4init()

        fMov = d4open(cb, fPCFMOV)

        Dim product As Integer = d4field(fMov, "A_PRODUCT")
        Dim name As String = d4field(fMov, "A_DESC_1")
        Dim quantity As Integer = d4field(fMov, "A_QUANT")
        Dim unit As Integer = d4field(fMov, "A_UNIT")
        Dim firstDiscount As Decimal = d4field(fMov, "A_DESCT")
        Dim secondDiscount As Decimal = d4field(fMov, "A_DESCT2")
        Dim thirdDiscount As Decimal = d4field(fMov, "A_DESCT3")
        Dim fourthDiscount As Decimal = d4field(fMov, "A_DESCT4")
        Dim fVat As Integer = d4field(fMov, "A_IVA")
        relation = relate4init(fMov)

        If relation = 0 Then
            Return False
        End If

        expr = "A_NUMBER = '" + selectedVendas + "'"

        rc = relate4querySet(relation, expr)
        rc = relate4top(relation)

        Dim count As Integer = 0
        Do While rc = r4success
            count = count + 1
            row = dtVendasDetails.NewRow()
            row("Produto") = f4long(product)
            row("Nome") = f4str(name)
            row("Quant") = f4number(quantity)
            row("Unit") = f4decimals(unit)
            row("Valor") = f4number(quantity) * f4decimals(unit)
            row("Descontos") = Format(f4number(firstDiscount) + f4number(secondDiscount) + f4number(thirdDiscount) + f4number(fourthDiscount), "###,##0.00")
            row("ValorcomIVA") = Format(f4number(fVat), "###,##0.00")
            dtVendasDetails.Rows.Add(row)
            rc = relate4skip(relation, 1)
        Loop

        rc = relate4free(relation, 0)
        rc = d4close(fMov)
        rc = code4initUndo(cb)


        grdVendasDetails.Columns(0).ItemStyle.HorizontalAlign = HorizontalAlign.Left ' Producto
        grdVendasDetails.Columns(1).ItemStyle.HorizontalAlign = HorizontalAlign.Left ' Nome
        grdVendasDetails.Columns(2).ItemStyle.HorizontalAlign = HorizontalAlign.Right ' Quant
        grdVendasDetails.Columns(3).ItemStyle.HorizontalAlign = HorizontalAlign.Right ' Unit
        grdVendasDetails.Columns(4).ItemStyle.HorizontalAlign = HorizontalAlign.Right ' Valor     
        grdVendasDetails.Columns(5).ItemStyle.HorizontalAlign = HorizontalAlign.Right ' Descontos
        grdVendasDetails.Columns(6).ItemStyle.HorizontalAlign = HorizontalAlign.Right ' Valor com IVA

        grdVendasDetails.DataSource = dtVendasDetails
        grdVendasDetails.ScrollMode = C1.Web.Wijmo.Controls.C1GridView.ScrollMode.Vertical
        grdVendasDetails.DataBind()

        ' Go to last page
        grdVendasDetails.PageIndex = grdVendasDetails.PageCount
        Return True
    End Function

    Private Function GetTextForTipoDeDocumento(fType As String)
        Dim fTypeString As String = String.Empty

        Select Case fType
            Case "F"
                fTypeString = "FATURA"
            Case "C"
                fTypeString = "NOTA DE CREDITO"
            Case "D"
                fTypeString = "NOTA DE DEBITO"
            Case "G"
                fTypeString = "GUIA DE REMESSA"
            Case "T"
                fTypeString = "GUIA DE TRANSPORTE"
        End Select

        Return fTypeString
    End Function
End Class