Imports C1.Web.Wijmo.Controls.C1GridView

Public Class VendasDetail
    Inherits System.Web.UI.Page

    Private selectedVendas As String = String.Empty
    Private dtVendasDetails As New DataTable("Facturas")
    Private selectedAgent As String = String.Empty

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
                'ShowVendasDetails(selectedVendas)
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
        Dim connStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:\PCFFILES\DATA\';Extended Properties=dBase 5.0"
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
        'ShowVendasDetails(selectedVendas)
    End Sub

    Private Sub GetPreviousRecords()
        Dim connStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:\PCFFILES\DATA\';Extended Properties=dBase 5.0"
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
        'ShowVendasDetails(selectedVendas)
    End Sub

    Private Sub ShowVendasHeader(selectedVendas As String)
        Dim connStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:\PCFFILES\DATA\';Extended Properties=dBase 5.0"
        Dim conn As New OleDb.OleDbConnection(connStr)
        Dim cmd As New OleDb.OleDbCommand()
        cmd.Connection = conn

        Dim sqlString As String = "SELECT F_NUMBER, F_CLIENT, F_INV_DT, F_DAYS_PP, F_TOTLIQ, "
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
        Dim connStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:\PCFFILES\DATA\';Extended Properties=dBase 5.0"
        Dim conn As New OleDb.OleDbConnection(connStr)
        Dim cmd As New OleDb.OleDbCommand()
        cmd.Connection = conn

        Dim sqlString As String = "SELECT A_PRODUCT, A_QUANT, FORMAT(A_UNIT,'###,##0.00000') AS A_UNIT, "
        sqlString += "A_QUANT * A_UNIT AS A_VALOR, A_DESCT + A_DESCT2 + A_DESCT3 + A_DESCT4 AS A_DESCONTOS, "
        sqlString += "A_IVA AS A_VALORCOMIVA FROM PCFMOV WHERE A_NUMBER = '" + selectedVendas + "' "

        cmd.CommandText = sqlString
        conn.Open()

        Dim reader As OleDb.OleDbDataReader = cmd.ExecuteReader
        dtVendasDetails.Load(reader)

        'grdVendasDetails.Columns(0).ItemStyle.HorizontalAlign = HorizontalAlign.Center ' Linha
        'grdVendasDetails.Columns(3).ItemStyle.HorizontalAlign = HorizontalAlign.Center ' Embalagem
        'grdVendasDetails.Columns(4).ItemStyle.HorizontalAlign = HorizontalAlign.Right  ' Quant
        'grdVendasDetails.Columns(5).ItemStyle.HorizontalAlign = HorizontalAlign.Right  ' Unit
        'grdVendasDetails.Columns(6).ItemStyle.HorizontalAlign = HorizontalAlign.Right  ' Valor
        'grdVendasDetails.Columns(7).ItemStyle.HorizontalAlign = HorizontalAlign.Right  ' Falta  

        grdVendasDetails.DataSource = dtVendasDetails
        grdVendasDetails.ScrollMode = ScrollMode.Vertical
        grdVendasDetails.DataBind()
        grdVendasDetails.PageSize = 9

        reader.Close()
        conn.Close()
    End Sub
End Class