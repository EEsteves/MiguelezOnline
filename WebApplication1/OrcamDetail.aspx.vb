Imports System.Data.OleDb

Public Class OrçamentosDetail
    Inherits Page

    Private selectedOrcam As String = ""
    Private selectedAgent As String = ""
    Private dtOrcam As New DataTable("Quotation")
    Private currentOrcamDate As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mSelectedAgent = Session("mAgentOnline")

        ' Colunas da Tabela de Encomendas
        dtOrcam.Columns.Add("Linha")
        dtOrcam.Columns.Add("Produto")
        dtOrcam.Columns.Add("Nome")
        dtOrcam.Columns.Add("Embala")
        dtOrcam.Columns.Add("Quant")
        dtOrcam.Columns.Add("Unit")
        dtOrcam.Columns.Add("Valor")
        dtOrcam.Columns.Add("Falta")

        If IsPostBack = False Then
            selectedOrcam = Request.QueryString("field1")
            If selectedOrcam = Nothing Then
                selectedOrcam = "12E05162"
            End If
            TextBox1.Text = selectedOrcam

            ShowOrcamHeader()
            ShowOrcamDetails()
        End If
    End Sub

    Private Sub ShowOrcamHeader()
        Dim connStr As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='C:\PCFFILES\DATA\';Extended Properties=dBase 5.0"
        Dim conn As New OleDbConnection(connStr)
        Dim cmd As New OleDbCommand()
        cmd.Connection = conn

        mC0 = "SELECT * FROM PCFORC WHERE K_ORCAM = '" + selectedOrcam + "' "
        cmd.CommandText = mC0
        conn.Open()

        Dim reader As OleDbDataReader = cmd.ExecuteReader

        While reader.Read()
            sNome.Text = reader("K_NAME").ToString
            sContact.Text = reader("K_CONTACT").ToString
            sData.Text = Mid(reader("K_ORC_DT").ToString, 1, 10)
            mCurrOrderDate = sData.Text
            sRef.Text = reader("K_REF").ToString
            sLoc1.Text = reader("K_ADDR_1").ToString
            xAddr2 = reader("K_ADDR_2").ToString

            If Len(Trim(xAddr2)) = 0 Then
                xCity = reader("K_CITY").ToString

                If Len(Trim(xCity)) = 0 Then
                    sLoc2.Text = Trim(Convert.ToString(reader("K_POSTAL"))) + " " + Trim(Convert.ToString(reader("K_PTL_NM")))
                    sLoc3.Text = String.Empty
                    sLoc4.Text = String.Empty
                Else
                    sLoc2.Text = xCity
                    sLoc3.Text = Trim(Convert.ToString(reader("K_POSTAL"))) + " " + Trim(Convert.ToString(reader("K_PTL_NM")))
                    sLoc4.Text = String.Empty
                End If
            Else
                sLoc2.Text = xAddr2
                xCity = reader("K_CITY").ToString

                If Len(Trim(xCity)) = 0 Then
                    sLoc3.Text = Trim(reader("K_POSTAL").ToString) + " " + Trim(reader("K_PTL_NM").ToString)
                    sLoc4.Text = ""
                Else
                    sLoc3.Text = xCity
                    sLoc4.Text = Trim(reader("K_POSTAL").ToString) + " " + Trim(reader("K_PTL_NM").ToString)
                End If
            End If

            sVend.Text = Convert.ToString(reader("K_VEND"))
            lblValidade.Text = Convert.ToString(reader("K_VALID"))
            sPrazo.Text = Convert.ToString(reader("K_PRAZOENT"))

            xSoma = CDbl(reader("K_TOTLIQ").ToString) - CDbl(reader("K_IVA").ToString)
            sSoma.Text = Format(xSoma, "###,##0.00")

            xIva = CDbl(reader("K_IVA").ToString)
            sIva.Text = Format(xIva, "###,##0.00")

            xTotal = CDbl(reader("K_TOTLIQ").ToString)
            sValor.Text = Format(xTotal, "###,##0.00")
        End While
        reader.Close()
        conn.Close()
    End Sub

    Private Sub ShowOrcamDetails()
        Dim connStr As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='C:\PCFFILES\DATA\';Extended Properties=dBase 5.0"
        Dim conn As New OleDb.OleDbConnection(connStr)
        Dim cmd As New OleDb.OleDbCommand()
        cmd.Connection = conn

        mC0 = "SELECT I_LINHA, I_PRODUCT, I_DESC_1, I_EMBALA, I_QUANT, " & _
            " FORMAT(I_UNIT,'###,##0.00000') AS X_UNIT, " & _
            " I_QUANT * I_UNIT AS I_TOTAL " & _
            " FROM PCFITE WHERE I_ORCAM = '" + selectedOrcam + "' "
        mC0 = mC0 + "ORDER BY I_LINHA"

        cmd.CommandText = mC0
        conn.Open()

        Dim reader As OleDb.OleDbDataReader = cmd.ExecuteReader
        Dim dataTable1 As New DataTable

        dataTable1.Load(reader)

        C1Encomendas.Columns(0).Width = 60  ' Linha
        C1Encomendas.Columns(1).Width = 185 ' Produto
        C1Encomendas.Columns(2).Width = 300 ' Nome
        C1Encomendas.Columns(3).Width = 60  ' Embala
        C1Encomendas.Columns(4).Width = 90  ' Quant
        C1Encomendas.Columns(5).Width = 90  ' Unit
        C1Encomendas.Columns(6).Width = 110 ' Valor  
        C1Encomendas.Columns(0).ItemStyle.HorizontalAlign = HorizontalAlign.Center ' Linha
        C1Encomendas.Columns(3).ItemStyle.HorizontalAlign = HorizontalAlign.Center ' Embalagem
        C1Encomendas.Columns(4).ItemStyle.HorizontalAlign = HorizontalAlign.Right  ' Quant
        C1Encomendas.Columns(5).ItemStyle.HorizontalAlign = HorizontalAlign.Right  ' Unit
        C1Encomendas.Columns(6).ItemStyle.HorizontalAlign = HorizontalAlign.Right  ' Valor

        C1Encomendas.DataSource = dataTable1
        C1Encomendas.ScrollMode = C1.Web.Wijmo.Controls.C1GridView.ScrollMode.Vertical
        C1Encomendas.DataBind()
        C1Encomendas.PageSize = 9

        reader.Close()
        conn.Close()
    End Sub

    Private Sub C1Menu1_ItemClick(sender As Object, e As C1.Web.Wijmo.Controls.C1Menu.C1MenuEventArgs) Handles C1Menu1.ItemClick
        Dim nextRecord As String = String.Empty
        Dim orcamDate As String = String.Empty

        If e.Item.Text = "< Anterior" Then
            ' Atenção: este «Anterior» vai apanhar todos os registos independentemente do critério de selecção
            ' indicado no ecrã anterior
            Dim connStr As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='C:\PCFFILES\DATA\';Extended Properties=dBase 5.0"
            Dim conn As New OleDb.OleDbConnection(connStr)
            Dim cmd As New OleDb.OleDbCommand()
            cmd.Connection = conn

            selectedOrcam = TextBox1.Text

            mC0 = "SELECT TOP 1 K_ORCAM, K_ORC_DT FROM PCFORC WHERE K_ORCAM < '" + selectedOrcam + "' "

            If Len(Trim(selectedAgent)) > 0 Then
                mC0 = mC0 + "AND K_AGENTE = '" + selectedAgent + "' "
            End If

            mC0 = mC0 + "ORDER BY K_ORCAM DESC"

            cmd.CommandText = mC0
            conn.Open()

            Dim reader As OleDb.OleDbDataReader = cmd.ExecuteReader

            If (reader.Read()) Then
                nextRecord = Convert.ToString(reader("K_ORCAM"))
                orcamDate = Mid(reader("K_ORC_DT").ToString, 1, 10)
            End If

            reader.Close()
            conn.Close()

            If CDate(orcamDate) > CDate(sData.Text) Then
                xMessageEnc("Inicio do Ficheiro")
            Else
                selectedOrcam = nextRecord
                TextBox1.Text = selectedOrcam

                ShowOrcamHeader()
                ShowOrcamDetails()
            End If
        ElseIf e.Item.Text = "Seguinte >" Then
            ' Atenção: este «Seguinte» vai apanhar todos os registos independentemente do critério de selecção
            ' indicado no ecrã anterior
            Dim connStr As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='C:\PCFFILES\DATA\';Extended Properties=dBase 5.0"
            Dim conn As New OleDb.OleDbConnection(connStr)
            Dim cmd As New OleDb.OleDbCommand()
            cmd.Connection = conn

            selectedOrcam = TextBox1.Text

            mC0 = "SELECT TOP 1 K_ORCAM, K_ORC_DT FROM PCFORC WHERE K_ORCAM > '" + selectedOrcam + "' "

            If Len(Trim(selectedAgent)) > 0 Then
                mC0 = mC0 + "AND K_AGENTE = '" + selectedAgent + "' "
            End If

            mC0 = mC0 + "ORDER BY K_ORCAM"
            cmd.CommandText = mC0
            conn.Open()

            Dim reader As OleDbDataReader = cmd.ExecuteReader
            reader.Read()

            nextRecord = reader("K_ORCAM").ToString
            orcamDate = Mid(reader("K_ORC_DT").ToString, 1, 10)
            reader.Close()
            conn.Close()

            If CDate(orcamDate) < CDate(sData.Text) Then
                xMessageEnc("Fim do Ficheiro")
            Else
                selectedOrcam = nextRecord
                TextBox1.Text = selectedOrcam

                ShowOrcamHeader()
                ShowOrcamDetails()
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