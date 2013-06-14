Imports Microsoft.JScript
Imports C1.Web.Wijmo.Controls.C1ComboBox
Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Public Class Clientes
    Inherits System.Web.UI.Page

    Private dataTable1 As New DataTable("Clientes")
    Private mStatus As String = ""
    Private mVend As String = ""
    Private mSelectedLetter As String = ""
    Private nLetras1 As Double = 0
    Private nLetras2 As Double = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = True Then
            Dim index As String = Request("__EVENTARGUMENT")
            ' xSelectedIndex(index)
        End If

        'Session("mSelectedAgentLoggedIn") = "YES"
        'Session("mAgentOnline") = "99"

        If Session("mSelectedAgentLoggedIn") = "NO" Then
            Dim xStr As String = "~/Default.aspx"
            Response.Redirect(xStr)
            Exit Sub
        End If

        'If Not IsPostBack Then
        ' Colunas da Tabela de Clientes
        dataTable1.Columns.Add("Cod")
        dataTable1.Columns.Add("Nome")
        dataTable1.Columns.Add("Telefone")

        ' Carrega o dropdown de letras para selecção de Clientes
        C1ComboBox1.Items.Add(New C1ComboBoxItem("A"))
        C1ComboBox1.Items.Add(New C1ComboBoxItem("B"))
        C1ComboBox1.Items.Add(New C1ComboBoxItem("C"))
        C1ComboBox1.Items.Add(New C1ComboBoxItem("D"))
        C1ComboBox1.Items.Add(New C1ComboBoxItem("E"))
        C1ComboBox1.Items.Add(New C1ComboBoxItem("F"))
        C1ComboBox1.Items.Add(New C1ComboBoxItem("G"))
        C1ComboBox1.Items.Add(New C1ComboBoxItem("H"))
        C1ComboBox1.Items.Add(New C1ComboBoxItem("I"))
        C1ComboBox1.Items.Add(New C1ComboBoxItem("J"))
        C1ComboBox1.Items.Add(New C1ComboBoxItem("K"))
        C1ComboBox1.Items.Add(New C1ComboBoxItem("L"))
        C1ComboBox1.Items.Add(New C1ComboBoxItem("M"))
        C1ComboBox1.Items.Add(New C1ComboBoxItem("N"))
        C1ComboBox1.Items.Add(New C1ComboBoxItem("O"))
        C1ComboBox1.Items.Add(New C1ComboBoxItem("P"))
        C1ComboBox1.Items.Add(New C1ComboBoxItem("Q"))
        C1ComboBox1.Items.Add(New C1ComboBoxItem("R"))
        C1ComboBox1.Items.Add(New C1ComboBoxItem("S"))
        C1ComboBox1.Items.Add(New C1ComboBoxItem("T"))
        C1ComboBox1.Items.Add(New C1ComboBoxItem("U"))
        C1ComboBox1.Items.Add(New C1ComboBoxItem("V"))
        C1ComboBox1.Items.Add(New C1ComboBoxItem("W"))
        C1ComboBox1.Items.Add(New C1ComboBoxItem("X"))
        C1ComboBox1.Items.Add(New C1ComboBoxItem("Y"))
        C1ComboBox1.Items.Add(New C1ComboBoxItem("Z"))

        If IsPostBack = False Then
            xLoadClientes()
            mRow = 0
        End If

        Label1.Text = ""
        Label2.Text = ""
        Label3.Text = ""
        Label4.Text = ""
        Label5.Text = ""
        Label6.Text = ""
        Label7.Text = ""
        Label8.Text = ""
        Label9.Text = ""
        Label10.Text = ""
        lblEmail.Text = ""
        '  End If
    End Sub

    Private Sub xSelectedIndex(index As String)
        mSelectedRow = Request.Form("text2")
        mSelectedClient = index
        xShowClientData(mSelectedClient)
        C1Clientes.SelectedIndex = mSelectedRow
    End Sub

    Function xLoadClientes()

        mSelectedAgent = Session("mAgentOnline")

        ' xMessageCli("Agente Online: " + mSelectedAgent)

        Dim connStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:\PCFFILES\DATA\';Extended Properties=dBase 5.0"
        Dim conn As New OleDb.OleDbConnection(connStr)
        Dim cmd As New OleDb.OleDbCommand()
        cmd.Connection = conn
        mC0 = "SELECT C_CLIENT, C_NAME, C_PHONE1, C_VEND FROM PCFCLI "
        mC1 = "C_NAME LIKE '[" + mSelectedLetter + "]%' "
        mC2 = "C_VEND = '" + mSelectedAgent + "' "
        mC3 = "ORDER BY C_NAME"
        If Len(Trim(mSelectedLetter)) = 0 And mSelectedAgent = "99" Then
            mC9 = mC0 + mC3
        ElseIf Len(Trim(mSelectedLetter)) = 0 And mSelectedAgent <> "99" Then
            mC9 = mC0 + "WHERE " + mC2 + mC3
        ElseIf Len(Trim(mSelectedLetter)) = 1 And mSelectedAgent = "99" Then
            mC9 = mC0 + "WHERE " + mC1 + mC3
        Else
            mC9 = mC0 + "WHERE " + mC1 + "AND " + mC2 + mC3
        End If

        ' MsgBox(MC9)

        cmd.CommandText = mC9

        conn.Open()
        Dim reader As OleDb.OleDbDataReader = cmd.ExecuteReader
        Dim dataTable1 As New DataTable
        dataTable1.Load(reader)
        C1Clientes.Columns(0).Width = 50
        C1Clientes.Columns(1).Width = 350
        C1Clientes.Columns(2).Width = 150
        C1Clientes.DataSource = dataTable1
        C1Clientes.ScrollMode = C1.Web.Wijmo.Controls.C1GridView.ScrollMode.Vertical
        C1Clientes.DataBind()
        reader.Close()
        conn.Close()
        Return True
    End Function

    Private Sub C1ComboBox1_SelectedIndexChanged(sender As Object, args As C1.Web.Wijmo.Controls.C1ComboBox.C1ComboBoxEventArgs) Handles C1ComboBox1.SelectedIndexChanged
        mSelectedLetter = C1ComboBox1.Text
        xLoadClientes()
        mSelectedClient = ""
    End Sub

    Function xShowClientData(mSelectedClient)
        Dim connStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:\PCFFILES\DATA\';Extended Properties=dBase 5.0"
        Dim conn As New OleDb.OleDbConnection(connStr)
        Dim cmd As New OleDb.OleDbCommand()
        cmd.Connection = conn
        mC0 = "SELECT "
        mC1 = "C_CLIENT, C_NAME, C_ADDR_1, C_ADDR_2, C_CITY, C_POSTAL, C_PTL_NM, "
        mC2 = "C_VEND, C_SIN, C_PHONE1, C_PHONE2, C_PHONE3, "
        mC3 = "C_PLAFOND2, C_PLAF_IN2 "
        mC4 = "FROM PCFCLI WHERE C_CLIENT = " + "'" + mSelectedClient + "'"
        mC9 = mC0 + mC1 + mC2 + mC3 + mC4
        cmd.CommandText = mC9
        conn.Open()
        Dim reader As OleDb.OleDbDataReader = cmd.ExecuteReader
        While reader.Read()
            Label1.Text = reader("C_NAME").ToString
            Label2.Text = reader("C_ADDR_1").ToString
            xLinha2 = reader("C_ADDR_2").ToString
            xCity = reader("C_CITY").ToString
            If Len(Trim(xLinha2)) = 0 Then
                If Len(Trim(xCity)) = 0 Then
                    Label3.Text = Trim(reader("C_POSTAL").ToString) + " " + Trim(reader("C_PTL_NM").ToString)
                    Label4.Text = ""
                    Label5.Text = ""
                Else
                    Label3.Text = reader("C_CITY").ToString
                    Label4.Text = Trim(reader("C_POSTAL").ToString) + " " + Trim(reader("C_PTL_NM").ToString)
                    Label5.Text = ""
                End If
            Else
                Label3.Text = reader("C_ADDR_2").ToString
                If Len(Trim(xCity)) = 0 Then
                    Label4.Text = Trim(reader("C_POSTAL").ToString) + " " + Trim(reader("C_PTL_NM").ToString)
                    Label5.Text = ""
                Else
                    Label4.Text = reader("C_CITY").ToString
                    Label5.Text = Trim(reader("C_POSTAL").ToString) + " " + Trim(reader("C_PTL_NM").ToString)
                End If
            End If
            Label6.Text = reader("C_VEND").ToString
            Label7.Text = reader("C_SIN").ToString
            Label8.Text = reader("C_PHONE1").ToString
            Label9.Text = reader("C_PHONE2").ToString
            lblEmail.Text = "Email: " & Mid(reader("C_PHONE3").ToString.ToLower, 1, 30)
            zPlafond2 = CDbl(reader("C_PLAFOND2").ToString)
            zPlafInt2 = CDbl(reader("C_PLAF_IN2").ToString)
            sPlafond.Text = Format(zPlafond2, "###,##0.00")
            sPlafInt.Text = Format(zPlafInt2, "###,##0.00")
            sPlafTot.Text = Format(zPlafond2 + zPlafInt2, "###,##0.00")
        End While
        reader.Close()
        conn.Close()
        xComprasUltAno(mSelectedClient)
        xInvoiced(mSelectedClient)
        xCalcLetras(mSelectedClient)
        xReceivables(mSelectedClient)
        xGuiasPendentes(mSelectedClient)
        xPendingOrders(mSelectedClient)
        xCalcTotais()
        Return True
    End Function

    ' Total de Compras com IVA nos ultimos 365 dias
    ' ---------------------------------------------
    Public Function xComprasUltAno(mSelectedClient)
        Dim xStartDate As Date = DateAdd(DateInterval.Year, -1, Today)
        Dim xEndDate As Date = Today
        Dim connStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:\PCFFILES\DATA\';Extended Properties=dBase 5.0"
        Dim conn As New OleDb.OleDbConnection(connStr)
        Dim cmd As New OleDb.OleDbCommand()
        cmd.Connection = conn
        mC0 = "SELECT "
        mC1 = "F_CLIENT, F_TYPE, F_STATUS, F_TOTLIQ, F_INV_DT "
        mC2 = "FROM PCFFCT WHERE F_CLIENT = " + "'" + mSelectedClient + "     ' "
        mC3 = "AND (F_INV_DT BETWEEN #" & xStartDate & "# AND #" & xEndDate & "#) "
        mC4 = "AND F_TYPE LIKE '[FCD$]%' AND F_STATUS LIKE '[BCX]%' ORDER BY F_INV_DT "
        mC9 = mC0 + mC1 + mC2 + mC3 + mC4
        cmd.CommandText = mC9
        zFactLastYear = 0
        Dim mType As String = ""
        Dim mStatus As String = ""
        Dim mTotliq As Double = 0
        conn.Open()
        Dim reader As OleDb.OleDbDataReader = cmd.ExecuteReader
        mCounter = 0
        While reader.Read()
            mCounter = mCounter + 1
            mType = reader("F_TYPE").ToString
            mTotliq = CDbl(reader("F_TOTLIQ").ToString)
            If InStr("CX", mType) > 0 Then
                mTotliq = mTotliq * -1
            End If
            zFactLastYear = zFactLastYear + mTotliq
        End While
        reader.Close()
        conn.Close()
        sUltAno.Text = Format(zFactLastYear, "##,###,##0.00")
    End Function

    ' Total Facturado é com IVA !!!
    ' -----------------------------
    Public Function xInvoiced(mSelectedClient)
        Dim xEndDate As Date = Today
        Dim connStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:\PCFFILES\DATA\';Extended Properties=dBase 5.0"
        Dim conn As New OleDb.OleDbConnection(connStr)
        Dim cmd As New OleDb.OleDbCommand()
        cmd.Connection = conn
        mC0 = "SELECT "
        mC1 = "F_CLIENT, F_NUMBER, F_TYPE, F_STATUS, F_TOTLIQ, F_INV_DT, F_RECVD, F_RECVD_DT, F_MOEDA, F_DAYS_PP "
        mC2 = "FROM PCFFCT WHERE F_CLIENT = " + "'" + mSelectedClient + "     ' "
        mC3 = "AND F_INV_DT <= #" & xEndDate & "# "
        mC4 = "AND F_TYPE LIKE '[FCD$]%' AND F_STATUS LIKE '[BCX]%' ORDER BY F_INV_DT"
        mC9 = mC0 + mC1 + mC2 + mC3 + mC4

        cmd.CommandText = mC9

        'Dim mType As String = ""
        'Dim mInvDt As Date
        'Dim xTotliq As String = ""
        'Dim xRecvd As String = ""
        'Dim mRecvdDt As Date
        'Dim mDaysPP As Double = ""
        'Dim mMoeda As String = ""

        nFactCorr = 0
        nFact3060 = 0
        nFact6090 = 0
        nFactOver = 0
        nFactTotal = 0
        zTotNaoVencido = 0
        zTotNaoVencido2 = 0
        zTotAtrazado = 0
        zTotAtrazado2 = 0
        Dim mGapTotal = 0
        Dim mNumeroFacturas = 0
        For ii = 0 To 11
            Ano1(ii) = 0
            Ano2(ii) = 0
            Ano3(ii) = 0
        Next
        conn.Open()
        Dim reader As OleDb.OleDbDataReader = cmd.ExecuteReader
        mCounter = 0
        While reader.Read()
            mCounter = mCounter + 1
            mNumber = reader("F_NUMBER").ToString
            ' MsgBox(mNumber)
            mType = reader("F_TYPE").ToString
            mStatus = reader("F_STATUS").ToString
            mInvDt = Mid(reader("F_INV_DT").ToString, 1, 10)
            mRecvdDt = Mid(reader("F_RECVD_DT").ToString, 1, 10)
            xTotliq = reader("F_TOTLIQ").ToString
            If Len(Trim(xTotliq)) = 0 Then
                mTotliq = 0
            Else
                mTotliq = CDbl(xTotliq)
            End If
            xRecvd = reader("F_RECVD").ToString
            If Len(Trim(xRecvd)) = 0 Then
                mRecvd = 0
            Else
                mRecvd = CDbl(xRecvd)
            End If
            mDaysPP = reader("F_DAYS_PP").ToString
            mMoeda = reader("F_MOEDA").ToString

            If mMoeda = "ESC" Then
                mTotliq = mTotliq / 200.482
                mRecvd = mRecvd / 200.482
            End If
            If InStr("CX", mType) > 0 Then
                mTotliq = mTotliq * -1
            End If
            Dim mMes As Integer = DatePart(DateInterval.Month, mInvDt) - 1
            If DatePart(DateInterval.Year, mInvDt) = DatePart(DateInterval.Year, Today()) Then
                Ano3(mMes) = Ano3(mMes) + mTotliq
            ElseIf DatePart(DateInterval.Year, mInvDt) = DatePart(DateInterval.Year, Today()) - 1 Then
                Ano2(mMes) = Ano2(mMes) + mTotliq
            ElseIf DatePart(DateInterval.Year, mInvDt) = DatePart(DateInterval.Year, Today()) - 2 Then
                Ano1(mMes) = Ano1(mMes) + mTotliq
            End If
            Dim mValor As Double = mTotliq - mRecvd
            Dim mGap As Integer = 0
            If mType = "F" And mStatus = "C" Then


                If Len(Trim(mInvDt)) > 0 And Len(Trim(mRecvdDt)) > 0 Then
                    mGap = DateDiff(DateInterval.Day, mInvDt, mRecvdDt)
                Else
                    mGap = 0
                End If
                mGapTotal = mGapTotal + mGap
                mNumeroFacturas = mNumeroFacturas + 1
            End If
            If mType = "F" And mStatus = "X" Then
                If Len(Trim(mRecvdDt)) > 0 Then
                    mGap = DateDiff(DateInterval.Day, mInvDt, Today)
                Else
                    mGap = DateDiff(DateInterval.Day, mInvDt, mRecvdDt)
                End If
                mGapTotal = mGapTotal + mGap
                mNumeroFacturas = mNumeroFacturas + 1
            End If

            If Today > DateAdd(DateInterval.Day, mDaysPP, mInvDt) Then
                If mInvDt <= DateSerial(2012, 3, 31) Then
                    zTotAtrazado = zTotAtrazado + mValor
                Else
                    zTotAtrazado2 = zTotAtrazado2 + mValor
                End If
            Else
                If mInvDt <= DateSerial(2012, 3, 31) Then
                    zTotNaoVencido = zTotNaoVencido + mValor
                Else
                    zTotNaoVencido2 = zTotNaoVencido2 + mValor
                End If
            End If
            If DateDiff(DateInterval.Day, mInvDt, Today) < 31 Then
                nFactCorr = nFactCorr + mValor
            ElseIf DateDiff(DateInterval.Day, mInvDt, Today) < 61 Then
                nFact3060 = nFact3060 + mValor
            ElseIf DateDiff(DateInterval.Day, mInvDt, Today) < 91 Then
                nFact6090 = nFact6090 + mValor
            Else
                nFactOver = nFactOver + mValor
            End If
            nFactTotal = nFactTotal + mValor
        End While
        reader.Close()
        conn.Close()

        Dim xAverage As String = ""

        ' MsgBox("Média " + (mGapTotal / mNumeroFacturas).ToString)

        If mNumeroFacturas = 0 Or mGapTotal = 0 Then
            xAverage = "??"
        Else
            xAverage = LTrim(CStr(Int(mGapTotal / mNumeroFacturas))) & " dias"
        End If
        Label10.Text = xAverage
        xTotNaoVencido = Format(zTotNaoVencido, "###,##0.00")
        xTotNaoVencido2 = Format(zTotNaoVencido2, "###,##0.00")
        xTotAtrazado = Format(zTotAtrazado, "###,##0.00")
        xTotAtrazado2 = Format(zTotAtrazado2, "###,##0.00")
        xFactCorr = Format(nFactCorr, "###,##0.00")
        xFact3060 = Format(nFact3060, "###,##0.00")
        xFact6090 = Format(nFact6090, "###,##0.00")
        xFactOver = Format(nFactOver, "###,##0.00")
        xFactTotal = Format(nFactTotal, "###,##0.00")
        sSaldoCorr.Text = Format(zTotNaoVencido2, "###,##0.00")
        sSaldoAtrz.Text = Format(zTotAtrazado2, "###,##0.00")
    End Function

    Function xReceivables(mSelectedClient)
        ' NÃO ESTÁ A SER USADO - SÓ PPROCESSA PRE-DATADOS
        ' ===============================================
        Dim connStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:\PCFFILES\DATA\';Extended Properties=dBase 5.0"
        Dim conn As New OleDb.OleDbConnection(connStr)
        Dim cmd As New OleDb.OleDbCommand()
        cmd.Connection = conn
        mC0 = "SELECT "
        mC1 = "R_CLIENT, R_TYPE, R_VALUE, R_VENC_DT "
        mC2 = "FROM PCFREC WHERE R_CLIENT = " + "'" + mSelectedClient + "     ' ORDER BY R_VENC_DT "
        mC9 = mC0 + mC1 + mC2
        ' MsgBox(mC9)
        cmd.CommandText = mC9
        Dim zValue As Double = 0
        Dim zDate As Date = Today
        Dim nPreDatCorr As Double = 0.0
        Dim nPreDat3060 As Double = 0.0
        Dim nPreDat6090 As Double = 0.0
        Dim nPreDatOver As Double = 0.0
        Dim nPreDatTotal As Double = 0.0
        conn.Open()
        Dim reader As OleDb.OleDbDataReader = cmd.ExecuteReader
        mCounter = 0
        While reader.Read()
            mType = reader("R_TYPE").ToString
            mVencDt = reader("R_VENC_DT").ToString
            mValor = CDbl(reader("R_VALUE").ToString)
            ' só deixa passar os P's (pre datados) 
            If mType = "P" Then
                Dim mGap As Integer = DateDiff(DateInterval.Day, Today, mVencDt)
                If mGap >= 0 Then
                    If mGap < 31 Then
                        nPreDatCorr = nPreDatCorr + mValor
                    ElseIf mGap < 61 Then
                        nPreDat3060 = nPreDat3060 + mValor
                    ElseIf mGap < 91 Then
                        nPreDat6090 = nPreDat6090 + zValue
                    Else
                        nPreDatOver = nPreDatOver + mValor
                    End If
                    nPreDatTotal = nPreDatTotal + mValor
                End If
            End If
        End While
        reader.Close()
        conn.Close()
        xPreDatCorr = Format(nPreDatCorr, "###,##0.00")
        xPreDat3060 = Format(nPreDat3060, "###,##0.00")
        xPreDat6090 = Format(nPreDat6090, "###,##0.00")
        xPreDatOver = Format(nPreDatOver, "###,##0.00")
        xPreDatTotal = Format(nPreDatTotal, "###,##0.00")
    End Function

    Public Function xCalcLetras(mSelectedClient)
        Dim connStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:\PCFFILES\DATA\';Extended Properties=dBase 5.0"
        Dim conn As New OleDb.OleDbConnection(connStr)
        Dim cmd As New OleDb.OleDbCommand()
        cmd.Connection = conn
        mC0 = "SELECT "
        mC1 = "L_CLIENT, L_TYPE, L_VALUE, L_ENTRY_DT "
        mC2 = "FROM PCFLET WHERE L_CLIENT = " + "'" + mSelectedClient + "     ' ORDER BY L_ENTRY_DT "
        mC9 = mC0 + mC1 + mC2
        cmd.CommandText = mC9
        nLetras1 = 0
        nLetras2 = 0
        conn.Open()
        Dim reader As OleDb.OleDbDataReader = cmd.ExecuteReader
        mCounter = 0
        While reader.Read()
            mType = reader("L_TYPE").ToString
            mEntryDt = reader("L_ENTRY_DT").ToString
            mValor = CDbl(reader("L_VALUE").ToString)
            If mEntryDt <= DateSerial(2012, 3, 31) Then
                If InStr("LRVXB", mType) > 0 Then
                    nLetras1 = nLetras1 + mValor
                ElseIf InStr("DAY", mType) > 0 Then
                    nLetras1 = nLetras1 - mValor
                End If
            Else
                If InStr("LRVXB", mType) > 0 Then
                    nLetras2 = nLetras2 + mValor
                    mCounter = mCounter + 1

                ElseIf InStr("DAY", mType) > 0 Then
                    nLetras2 = nLetras2 - mValor
                    mCounter = mCounter + 1
                End If
            End If
        End While
        reader.Close()
        conn.Close()
        xLetrasTotal = Format(nLetras1 + nLetras2, "###,##0.00")
        sLetras.Text = xLetrasTotal
    End Function

    ' Guias Pendentes é com IVA
    ' -------------------------
    Function xGuiasPendentes(mSelectedClient)
        Dim connStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:\PCFFILES\DATA\';Extended Properties=dBase 5.0"
        Dim conn As New OleDb.OleDbConnection(connStr)
        Dim cmd As New OleDb.OleDbCommand()
        cmd.Connection = conn
        mC0 = "SELECT "
        mC1 = "F_CLIENT, F_TYPE, F_STATUS, F_TOTLIQ "
        mC2 = "FROM PCFFCT WHERE F_CLIENT = " + "'" + mSelectedClient + "     ' AND F_TYPE = 'G' AND F_STATUS = 'B' "
        mC9 = mC0 + mC1 + mC2
        cmd.CommandText = mC9
        zTotGuias = 0.0
        conn.Open()
        Dim reader As OleDb.OleDbDataReader = cmd.ExecuteReader
        mCounter = 0
        While reader.Read()
            mValor = CDbl(reader("F_TOTLIQ").ToString)
            zTotGuias = zTotGuias + mValor
        End While
        reader.Close()
        conn.Close()
        sGuiasPorFact.Text = Format(zTotGuias, "###,##0.00")
    End Function

    ' Encomendas Pendentes é com IVA
    ' ------------------------------
    Public Function xPendingOrders(mSelectedClient)
        Dim connStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:\PCFFILES\DATA\';Extended Properties=dBase 5.0"
        Dim conn As New OleDb.OleDbConnection(connStr)
        Dim cmd As New OleDb.OleDbCommand()
        cmd.Connection = conn
        mC0 = "SELECT "
        mC1 = "I_CLIENT, I_STATUS, I_QUANT, I_ENTREGUE, I_UNIT, I_DESCT, I_DESCT2, I_DESCT3, I_DESCT4, I_IVA, I_IVA_TP, I_MOEDA, I_CAMBIO "
        mC2 = "FROM PCFICL WHERE I_CLIENT = " + "'" + mSelectedClient + "     ' AND I_STATUS = 'A' "
        mC9 = mC0 + mC1 + mC2
        cmd.CommandText = mC9
        zTotEncPendentes = 0
        conn.Open()
        Dim reader As OleDb.OleDbDataReader = cmd.ExecuteReader
        mCounter = 0
        While reader.Read()
            mQuant = CDbl(reader("I_QUANT").ToString)
            mEntregue = CDbl(reader("I_ENTREGUE").ToString)
            zFalta = mQuant - mEntregue
            zUnit = CDbl(reader("I_UNIT").ToString)
            zDesct1 = CDbl(reader("I_DESCT").ToString)
            zDesct2 = CDbl(reader("I_DESCT2").ToString)
            zDesct3 = CDbl(reader("I_DESCT3").ToString)
            zDesct4 = CDbl(reader("I_DESCT4").ToString)
            zIvaTp = reader("I_IVA_TP").ToString
            zTaxaIva = CDbl(reader("I_IVA").ToString)
            zMoeda = reader("I_MOEDA").ToString
            zCambio = CDbl(reader("I_CAMBIO").ToString)
            mValComIva = xCalcItem(zFalta, zUnit, zDesct1, zDesct2, zDesct3, zDesct4, zIvaTp, zTaxaIva, zMoeda, zCambio)
            ' Atenção: este total é com IVA
            zTotEncPendentes = zTotEncPendentes + mValComIva
        End While
        reader.Close()
        conn.Close()
        xTotEncPendentes = Format(zTotEncPendentes, "###,##0.00")
        sEncPend.Text = xTotEncPendentes
    End Function

    Public Function xCalcTotais()
        zDisponivel = (zPlafond + zPlafInt) - (zTotNaoVencido + zTotAtrazado + nLetras1 + nPreDatTotal)
        xDisponivel = Format(zDisponivel, "###,##0.00")
        zDisponivel2 = (zPlafond2 + zPlafInt2) - (zTotNaoVencido2 + zTotAtrazado2 + zTotGuias + nLetras1 + nLetras2 + nPreDatTotal)
        xDisponivel2 = Format(zDisponivel2, "###,##0.00")
        sCredDisp.Text = xDisponivel2
        mTot2 = nFactCorr + nPreDatCorr
        mTot3 = nFact3060 + nPreDat3060
        mTot4 = nFact6090 + nPreDat6090
        mTot5 = nFactOver + nPreDatOver
        mTotT = mTot2 + mTot3 + mTot4 + mTot5 + nLetras1 + nLetras2
        xTot2 = Format(mTot2, "###,##0.00")
        xTot3 = Format(mTot3, "###,##0.00")
        xTot4 = Format(mTot4, "###,##0.00")
        xTot5 = Format(mTot5, "###,##0.00")
        xTotT = Format(mTotT, "###,##0.00")
    End Function

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        mSelectedClient = Request.Form("text1")
        xShowClientData(mSelectedClient)
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        xMessageCli("Acesso ao ecrã de Descontos do Cliente seleccionado - ainda não está implementado")
    End Sub

    Public Function xMessageCli(xMsg)
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