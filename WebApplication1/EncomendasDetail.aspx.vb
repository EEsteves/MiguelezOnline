Public Class EncomendasDetail
    Inherits System.Web.UI.Page

    Private mSelectedOrder As String = ""
    Private mSelectedAgent As String = ""
    Private dataTable1 As New DataTable("Orders")
    Private mCurrOrderDate As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        mSelectedAgent = Session("mAgentOnline")

        ' Colunas da Tabela de Encomendas
        dataTable1.Columns.Add("Linha")
        dataTable1.Columns.Add("Produto")
        dataTable1.Columns.Add("Nome")
        dataTable1.Columns.Add("Embala")
        dataTable1.Columns.Add("Quant")
        dataTable1.Columns.Add("Unit")
        dataTable1.Columns.Add("Valor")
        dataTable1.Columns.Add("Falta")
        If IsPostBack = False Then
            mSelectedOrder = Request.QueryString("field1")
            If mSelectedOrder = Nothing Then
                mSelectedOrder = "12E05162"
            End If
            TextBox1.Text = mSelectedOrder
            xShowOrderHeader()
            xShowOrderDetails()
        End If
    End Sub

    Function xShowOrderHeader()
        Dim connStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:\PCFFILES\DATA\';Extended Properties=dBase 5.0"
        Dim conn As New OleDb.OleDbConnection(connStr)
        Dim cmd As New OleDb.OleDbCommand()
        cmd.Connection = conn
        mC0 = "SELECT * FROM PCFECL WHERE E_NUMBER = '" + mSelectedOrder + "' "
        cmd.CommandText = mC0
        conn.Open()
        Dim reader As OleDb.OleDbDataReader = cmd.ExecuteReader
        While reader.Read()
            sNome.Text = reader("E_NAME").ToString
            sContact.Text = reader("E_CONTACT").ToString
            sData.Text = Mid(reader("E_DATE").ToString, 1, 10)
            mCurrOrderDate = sData.Text
            sRef.Text = reader("E_REF").ToString
            sLoc1.Text = reader("E_ADDR_1").ToString
            xAddr2 = reader("E_ADDR_2").ToString
            If Len(Trim(xAddr2)) = 0 Then
                xCity = reader("E_CITY").ToString
                If Len(Trim(xCity)) = 0 Then
                    sLoc2.Text = Trim(reader("E_POSTAL").ToString) + " " + Trim(reader("E_PTL_NM").ToString)
                    sLoc3.Text = ""
                    sLoc4.Text = ""
                Else
                    sLoc2.Text = xCity
                    sLoc3.Text = Trim(reader("E_POSTAL").ToString) + " " + Trim(reader("E_PTL_NM").ToString)
                    sLoc4.Text = ""
                End If
            Else
                sLoc2.Text = xAddr2
                xCity = reader("E_CITY").ToString
                If Len(Trim(xCity)) = 0 Then
                    sLoc3.Text = Trim(reader("E_POSTAL").ToString) + " " + Trim(reader("E_PTL_NM").ToString)
                    sLoc4.Text = ""
                Else
                    sLoc3.Text = xCity
                    sLoc4.Text = Trim(reader("E_POSTAL").ToString) + " " + Trim(reader("E_PTL_NM").ToString)
                End If
            End If
            sVend.Text = reader("E_VEND").ToString
            sPrevista.Text = Mid(reader("E_PREVISTA").ToString, 1, 10)
            sPrazo.Text = reader("E_DAYS_PP").ToString

            xSoma = CDbl(reader("E_TOTAL").ToString) - CDbl(reader("E_IVA").ToString)
            sSoma.Text = Format(xSoma, "###,##0.00")

            xIva = CDbl(reader("E_IVA").ToString)
            sIva.Text = Format(xIva, "###,##0.00")

            xTotal = CDbl(reader("E_TOTAL").ToString)
            sValor.Text = Format(xTotal, "###,##0.00")
        End While
        reader.Close()
        conn.Close()
    End Function

    Function xShowOrderDetails()
        Dim connStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:\PCFFILES\DATA\';Extended Properties=dBase 5.0"
        Dim conn As New OleDb.OleDbConnection(connStr)
        Dim cmd As New OleDb.OleDbCommand()
        cmd.Connection = conn
        mC0 = "SELECT I_LINHA, I_PRODUCT, I_DESC_1, I_QUANT, FORMAT(I_UNIT,'###,##0.00000') AS X_UNIT, I_UNIT, I_QUANT * I_UNIT AS I_TOTAL, I_QUANT - I_ENTREGUE AS I_FALTA, I_EMBALA FROM PCFICL WHERE I_NUMBER = '" + mSelectedOrder + "' "
        mC0 = mC0 + "ORDER BY I_LINHA"
        '  MsgBox(mC0)
        cmd.CommandText = mC0
        conn.Open()
        Dim reader As OleDb.OleDbDataReader = cmd.ExecuteReader
        Dim dataTable1 As New DataTable
        dataTable1.Load(reader)
        C1Encomendas.Columns(0).Width = 60  ' Linha
        C1Encomendas.Columns(1).Width = 100 ' Produto
        C1Encomendas.Columns(2).Width = 300 ' Nome
        C1Encomendas.Columns(3).Width = 60  ' Embala
        C1Encomendas.Columns(4).Width = 90  ' Quant
        C1Encomendas.Columns(5).Width = 90  ' Unit
        C1Encomendas.Columns(6).Width = 110 ' Valor
        C1Encomendas.Columns(7).Width = 90  ' Falta    
        C1Encomendas.Columns(0).ItemStyle.HorizontalAlign = HorizontalAlign.Center ' Linha
        C1Encomendas.Columns(3).ItemStyle.HorizontalAlign = HorizontalAlign.Center ' Embalagem
        C1Encomendas.Columns(4).ItemStyle.HorizontalAlign = HorizontalAlign.Right  ' Quant
        C1Encomendas.Columns(5).ItemStyle.HorizontalAlign = HorizontalAlign.Right  ' Unit
        C1Encomendas.Columns(6).ItemStyle.HorizontalAlign = HorizontalAlign.Right  ' Valor
        C1Encomendas.Columns(7).ItemStyle.HorizontalAlign = HorizontalAlign.Right  ' Falta  
        C1Encomendas.DataSource = dataTable1
        C1Encomendas.ScrollMode = C1.Web.Wijmo.Controls.C1GridView.ScrollMode.Vertical
        C1Encomendas.DataBind()
        C1Encomendas.PageSize = 9
        reader.Close()
        conn.Close()
    End Function

    Private Sub C1Menu1_ItemClick(sender As Object, e As C1.Web.Wijmo.Controls.C1Menu.C1MenuEventArgs) Handles C1Menu1.ItemClick
        If e.Item.Text = "< Anterior" Then
            ' Atenção: este «Anterior» vai apanhar todos os registos independentemente do critério de selecção
            ' indicado no ecrã anterior
            Dim connStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:\PCFFILES\DATA\';Extended Properties=dBase 5.0"
            Dim conn As New OleDb.OleDbConnection(connStr)
            Dim cmd As New OleDb.OleDbCommand()
            cmd.Connection = conn
            mSelectedOrder = TextBox1.Text
            mC0 = "SELECT TOP 1 E_NUMBER, E_DATE FROM PCFECL WHERE E_NUMBER <'" + mSelectedOrder + "' "
            If Len(Trim(mSelectedAgent)) > 0 Then
                mC0 = mC0 + "AND E_VEND = '" + mSelectedAgent + "' "
            End If
            mC0 = mC0 + "ORDER BY E_NUMBER"
            cmd.CommandText = mC0
            conn.Open()
            Dim reader As OleDb.OleDbDataReader = cmd.ExecuteReader
            reader.Read()
            mNext = reader("E_NUMBER").ToString
            mOrderDate = Mid(reader("E_DATE").ToString, 1, 10)
            reader.Close()
            conn.Close()
            If CDate(mOrderDate) > CDate(sData.Text) Then
                xMessageEnc("Inicio do Ficheiro")
            Else
                mSelectedOrder = mNext
                TextBox1.Text = mSelectedOrder
                xShowOrderHeader()
                xShowOrderDetails()
            End If
        ElseIf e.Item.Text = "Seguinte >" Then
            ' Atenção: este «Seguinte» vai apanhar todos os registos independentemente do critério de selecção
            ' indicado no ecrã anterior
            Dim connStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:\PCFFILES\DATA\';Extended Properties=dBase 5.0"
            Dim conn As New OleDb.OleDbConnection(connStr)
            Dim cmd As New OleDb.OleDbCommand()
            cmd.Connection = conn
            mSelectedOrder = TextBox1.Text
            mC0 = "SELECT TOP 1 E_NUMBER, E_DATE FROM PCFECL WHERE E_NUMBER > '" + mSelectedOrder + "' "
            If Len(Trim(mSelectedAgent)) > 0 Then
                mC0 = mC0 + "AND E_VEND = '" + mSelectedAgent + "' "
            End If
            mC0 = mC0 + "ORDER BY E_NUMBER"
            cmd.CommandText = mC0
            conn.Open()
            Dim reader As OleDb.OleDbDataReader = cmd.ExecuteReader
            reader.Read()
            mNext = reader("E_NUMBER").ToString
            mOrderDate = Mid(reader("E_DATE").ToString, 1, 10)
            reader.Close()
            conn.Close()
            If CDate(mOrderDate) < CDate(sData.Text) Then
                xMessageEnc("Fim do Ficheiro")
            Else
                mSelectedOrder = mNext
                TextBox1.Text = mSelectedOrder
                xShowOrderHeader()
                xShowOrderDetails()
            End If
        ElseIf e.Item.Text = "Imprime Encomenda" Then
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

End Class