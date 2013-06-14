Imports Microsoft.JScript
Imports C1.Web.Wijmo.Controls.C1Chart
Imports System.Globalization

Public Class VendasStats
    Inherits System.Web.UI.Page
    Private DataTable1 As DataTable = New DataTable("Data1")
    Private mFunction As String = "All"
    Private Meses As String() = {"Ja", "Fe", "Ma", "Ab", "Ma", "Ju", "Jl", "Ag", "Se", "Ou", "No", "De"}
    Private Anos As String() = {"2010", "2011", "2012"}
    Private mStatus As String = ""
    Private mCliNum As String = ""
    Private mVend As String = ""
    Private rc As Integer = 0
    Private provider As CultureInfo = CultureInfo.InvariantCulture
    Private mStatMode As String = "Ano"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        xLoadStats(mSelectedClient)
    End Sub

    Function xLoadStats(mSelectedClient)
        Dim ii As Integer
        Dim Mes1(12) As Double
        Dim Mes2(12) As Double
        Dim Mes3(12) As Double
        Dim MMM1(12) As Double
        Dim MMM2(12) As Double
        Dim MMM3(12) As Double
        For ii = 0 To 11
            Mes1(ii) = 0
            Mes2(ii) = 0
            Mes3(ii) = 0
            MMM1(ii) = 0
            MMM2(ii) = 0
            MMM3(ii) = 0
        Next ii
        Dim Tri1(4) As Double
        Dim Tri2(4) As Double
        Dim Tri3(4) As Double
        Dim QQQ1(4) As Double
        Dim QQQ2(4) As Double
        Dim QQQ3(4) As Double
        For ii = 0 To 3
            Tri1(ii) = 0
            Tri2(ii) = 0
            Tri3(ii) = 0
            QQQ1(ii) = 0
            QQQ2(ii) = 0
            QQQ3(ii) = 0
        Next ii
        Dim Tot1(2) As Double
        Dim Tot2(2) As Double
        Dim Tot3(2) As Double
        Dim TTT1(2) As Double
        Dim TTT2(2) As Double
        Tot1(0) = 0
        Tot1(1) = 0
        Tot1(2) = 0
        TTT1(0) = 0
        TTT1(1) = 0
        TTT1(2) = 0

        Dim mSelect As String = ""
        Dim sTodos As Boolean = False    ' Todos os Clientes ou apenas um
        Dim sCliCode As String = ""      ' O cliente seleccionado
        Dim mStatus As String = ""
        Dim mType As String = ""
        Dim mTotliq As Double = 0
        Dim mDataFactura As Date
        Dim mMM As Integer = 0
        Dim mTT As Integer = 0
        Dim xNodate As String = "  /  /    "
        Dim FILTRO As String = ""
        Dim mCliCode As String = ""

        If mFunction = "All" Then
            Dim fFct As Integer
            Dim cb = code4init()
            fFct = d4open(cb, fPCFFCT)
            Dim tag As Integer = d4tag(fFct, "PCFFCT6")
            Call d4tagSelect(fFct, tag)

            ' set filter data > 1 Jan 2010

            Dim fType As Integer = d4field(fFct, "F_TYPE")
            Dim fStatus As Integer = d4field(fFct, "F_STATUS")
            Dim fNumber As Integer = d4field(fFct, "F_NUMBER")
            Dim fClient As Integer = d4field(fFct, "F_CLIENT")
            Dim fVend As Integer = d4field(fFct, "F_VEND")
            Dim fInvDt As Integer = d4field(fFct, "F_INV_DT")
            Dim fTotLiq As Integer = d4field(fFct, "F_TOTLIQ")
            Dim fTotCus As Integer = d4field(fFct, "F_TOTCUS")
            Dim fIVA As Integer = d4field(fFct, "F_IVA")
            Dim fMoeda As Integer = d4field(fFct, "F_MOEDA")

            MsgBox("mAgentOnline:" + Session("mAgentOnline"))

            ' Facturas
            d4seek(fFct, "F")
            While (f4str(fType) = "F") And (d4eof(fFct) = 0)
                mStatus = f4str(fStatus)
                mSelect = "s"
                If mStatus = "A" Or mStatus = "D" Then
                    mSelect = "n"
                End If
                mCliCode = f4str(fClient)
                If Trim(sCliCode) <> "" And sCliCode <> mCliCode Then
                    mSelect = "n"
                End If
                Dim xDataFactura As String = f4str(fInvDt)
                mDataFactura = DateTime.ParseExact(xDataFactura, "yyyyMMdd", provider)
                If mDataFactura > Today Then
                    mSelect = "n"
                End If
                mVend = f4str(fVend)
                If Session("mAgentOnline") = "99" Then
                Else
                    If Trim(mVend) <> Session("mAgentOnline") Then
                        mSelect = "n"
                    End If
                End If
                If mSelect = "s" Then
                    mTotliq = f4long(fTotLiq) - f4long(fIVA)
                    If f4str(fMoeda) = "ESC" Then
                        mTotliq = mTotliq / mEscEcu
                    End If
                    mDataFactura = DateTime.ParseExact(xDataFactura, "yyyyMMdd", provider)
                    mMM = DatePart(DateInterval.Month, mDataFactura) - 1
                    mTT = DatePart(DateInterval.Quarter, mDataFactura) - 1
                    If DatePart(DateInterval.Year, mDataFactura) = DatePart(DateInterval.Year, Today()) Then
                        Mes3(mMM) = Mes3(mMM) + mTotliq
                        Tri3(mTT) = Tri3(mTT) + mTotliq
                        Tot1(2) = Tot1(2) + mTotliq
                    ElseIf DatePart(DateInterval.Year, mDataFactura) = DatePart(DateInterval.Year, Today()) - 1 Then
                        Mes2(mMM) = Mes2(mMM) + mTotliq
                        Tri2(mTT) = Tri2(mTT) + mTotliq
                        Tot1(1) = Tot1(1) + mTotliq
                        If mDataFactura > DateAdd(DateInterval.Year, -1, Today()) Then
                            MMM2(mMM) = MMM2(mMM) + mTotliq
                            QQQ2(mTT) = QQQ2(mTT) + mTotliq
                            TTT1(1) = TTT1(1) + mTotliq
                        End If
                    ElseIf DatePart(DateInterval.Year, mDataFactura) = DatePart(DateInterval.Year, Today()) - 2 Then
                        Mes1(mMM) = Mes1(mMM) + mTotliq
                        Tri1(mTT) = Tri1(mTT) + mTotliq
                        Tot1(0) = Tot1(0) + mTotliq
                        If mDataFactura > DateAdd(DateInterval.Year, -2, Today()) Then
                            MMM1(mMM) = MMM1(mMM) + mTotliq
                            QQQ1(mTT) = QQQ1(mTT) + mTotliq
                            TTT1(0) = TTT1(0) + mTotliq
                        End If
                    End If
                End If
                rc = d4skip(fFct, 1)
            End While

            ' Creditos 
            d4seek(fFct, "C")
            While (f4str(fType) = "C") And (d4eof(fFct) = 0)
                mSelect = "s"
                mStatus = f4str(fStatus)
                mSelect = "s"
                If mStatus = "A" Or mStatus = "D" Then
                    mSelect = "n"
                End If
                mCliCode = f4str(fClient)
                If Trim(sCliCode) <> "" And sCliCode <> mCliCode Then
                    mSelect = "n"
                End If
                Dim xDataFactura As String = f4str(fInvDt)
                mDataFactura = DateTime.ParseExact(xDataFactura, "yyyyMMdd", provider)
                If mDataFactura > Today Then
                    mSelect = "n"
                End If
                mVend = f4str(fVend)
                If Session("mAgentOnline") = "99" Then
                Else
                    If Trim(mVend) <> Session("mAgentOnline") Then
                        mSelect = "n"
                    End If
                End If
                If mSelect = "s" Then
                    mTotliq = f4long(fTotLiq) - f4long(fIVA)
                    If f4str(fMoeda) = "ESC" Then
                        mTotliq = mTotliq / mEscEcu
                    End If
                    mDataFactura = DateTime.ParseExact(xDataFactura, "yyyyMMdd", provider)
                    mTotliq = mTotliq * -1
                    mMM = DatePart(DateInterval.Month, mDataFactura) - 1
                    mTT = DatePart(DateInterval.Quarter, mDataFactura) - 1
                    If DatePart(DateInterval.Year, mDataFactura) = DatePart(DateInterval.Year, Today()) Then
                        Mes3(mMM) = Mes3(mMM) + mTotliq
                        Tri3(mTT) = Tri3(mTT) + mTotliq
                        Tot1(2) = Tot1(2) + mTotliq
                    ElseIf DatePart(DateInterval.Year, mDataFactura) = DatePart(DateInterval.Year, Today()) - 1 Then
                        Mes2(mMM) = Mes2(mMM) + mTotliq
                        Tri2(mTT) = Tri2(mTT) + mTotliq
                        Tot1(1) = Tot1(1) + mTotliq
                        If mDataFactura > DateAdd(DateInterval.Year, -1, Today()) Then
                            MMM2(mMM) = MMM2(mMM) + mTotliq
                            QQQ2(mTT) = QQQ2(mTT) + mTotliq
                            TTT1(1) = TTT1(1) + mTotliq
                        End If
                    ElseIf DatePart(DateInterval.Year, mDataFactura) = DatePart(DateInterval.Year, Today()) - 2 Then
                        Mes1(mMM) = Mes1(mMM) + mTotliq
                        Tri1(mTT) = Tri1(mTT) + mTotliq
                        Tot1(0) = Tot1(0) + mTotliq
                        If mDataFactura > DateAdd(DateInterval.Year, -2, Today()) Then
                            MMM1(mMM) = MMM1(mMM) + mTotliq
                            QQQ1(mTT) = QQQ1(mTT) + mTotliq
                            TTT1(0) = TTT1(0) + mTotliq
                        End If
                    End If
                End If
                rc = d4skip(fFct, 1)
            End While

            ' Debitos 
            d4seek(fFct, "D")
            While (f4str(fType) = "D") And (d4eof(fFct) = 0)
                mSelect = "s"
                mStatus = f4str(fStatus)
                mSelect = "s"
                If mStatus = "A" Or mStatus = "D" Then
                    mSelect = "n"
                End If
                mCliCode = f4str(fClient)
                If Trim(sCliCode) <> "" And sCliCode <> mCliCode Then
                    mSelect = "n"
                End If
                Dim xDataFactura As String = f4str(fInvDt)
                mDataFactura = DateTime.ParseExact(xDataFactura, "yyyyMMdd", provider)
                If mDataFactura > Today Then
                    mSelect = "n"
                End If
                mVend = f4str(fVend)
                If Session("mAgentOnline") = "99" Then
                Else
                    If Trim(mVend) <> Session("mAgentOnline") Then
                        mSelect = "n"
                    End If
                End If
                If mSelect = "s" Then
                    mTotliq = f4long(fTotLiq) - f4long(fIVA)
                    If f4str(fMoeda) = "ESC" Then
                        mTotliq = mTotliq / mEscEcu
                    End If
                    mDataFactura = DateTime.ParseExact(xDataFactura, "yyyyMMdd", provider)
                    mMM = DatePart(DateInterval.Month, mDataFactura) - 1
                    mTT = DatePart(DateInterval.Quarter, mDataFactura) - 1
                    If DatePart(DateInterval.Year, mDataFactura) = DatePart(DateInterval.Year, Today()) Then
                        Mes3(mMM) = Mes3(mMM) + mTotliq
                        Tri3(mTT) = Tri3(mTT) + mTotliq
                        Tot1(2) = Tot1(2) + mTotliq
                    ElseIf DatePart(DateInterval.Year, mDataFactura) = DatePart(DateInterval.Year, Today()) - 1 Then
                        Mes2(mMM) = Mes2(mMM) + mTotliq
                        Tri2(mTT) = Tri2(mTT) + mTotliq
                        Tot1(1) = Tot1(1) + mTotliq
                        If mDataFactura > DateAdd(DateInterval.Year, -1, Today()) Then
                            MMM2(mMM) = MMM2(mMM) + mTotliq
                            QQQ2(mTT) = QQQ2(mTT) + mTotliq
                            TTT1(1) = TTT1(1) + mTotliq
                        End If
                    ElseIf DatePart(DateInterval.Year, mDataFactura) = DatePart(DateInterval.Year, Today()) - 2 Then
                        Mes1(mMM) = Mes1(mMM) + mTotliq
                        Tri1(mTT) = Tri1(mTT) + mTotliq
                        Tot1(0) = Tot1(0) + mTotliq
                        If mDataFactura > DateAdd(DateInterval.Year, -2, Today()) Then
                            MMM1(mMM) = MMM1(mMM) + mTotliq
                            QQQ1(mTT) = QQQ1(mTT) + mTotliq
                            TTT1(0) = TTT1(0) + mTotliq
                        End If
                    End If
                End If
                rc = d4skip(fFct, 1)
            End While

            ' Vendas a Dinheiro 
            d4seek(fFct, "$")
            While (f4str(fType) = "$") And (d4eof(fFct) = 0)
                mSelect = "s"
                If mStatus = "A" Or mStatus = "D" Then
                    mSelect = "n"
                End If
                mCliCode = f4str(fClient)
                If Trim(sCliCode) <> "" And sCliCode <> mCliCode Then
                    mSelect = "n"
                End If
                Dim xDataFactura As String = f4str(fInvDt)
                mDataFactura = DateTime.ParseExact(xDataFactura, "yyyyMMdd", provider)
                If mDataFactura > Today Then
                    mSelect = "n"
                End If
                mVend = f4str(fVend)
                If Session("mAgentOnline") = "99" Then
                Else
                    If Trim(mVend) <> Session("mAgentOnline") Then
                        mSelect = "n"
                    End If
                End If
                If mSelect = "s" Then
                    mTotliq = f4long(fTotLiq) - f4long(fIVA)
                    If f4str(fMoeda) = "ESC" Then
                        mTotliq = mTotliq / mEscEcu
                    End If
                    mDataFactura = DateTime.ParseExact(xDataFactura, "yyyyMMdd", provider)
                    mMM = DatePart(DateInterval.Month, mDataFactura) - 1
                    mTT = DatePart(DateInterval.Quarter, mDataFactura) - 1
                    If DatePart(DateInterval.Year, mDataFactura) = DatePart(DateInterval.Year, Today()) Then
                        Mes3(mMM) = Mes3(mMM) + mTotliq
                        Tri3(mTT) = Tri3(mTT) + mTotliq
                        Tot1(2) = Tot1(2) + mTotliq
                    ElseIf DatePart(DateInterval.Year, mDataFactura) = DatePart(DateInterval.Year, Today()) - 1 Then
                        Mes2(mMM) = Mes2(mMM) + mTotliq
                        Tri2(mTT) = Tri2(mTT) + mTotliq
                        Tot1(1) = Tot1(1) + mTotliq
                        If mDataFactura > DateAdd(DateInterval.Year, -1, Today()) Then
                            MMM2(mMM) = MMM2(mMM) + mTotliq
                            QQQ2(mTT) = QQQ2(mTT) + mTotliq
                            TTT1(1) = TTT1(1) + mTotliq
                        End If
                    ElseIf DatePart(DateInterval.Year, mDataFactura) = DatePart(DateInterval.Year, Today()) - 2 Then
                        Mes1(mMM) = Mes1(mMM) + mTotliq
                        Tri1(mTT) = Tri1(mTT) + mTotliq
                        Tot1(0) = Tot1(0) + mTotliq
                        If mDataFactura > DateAdd(DateInterval.Year, -2, Today()) Then
                            MMM1(mMM) = MMM1(mMM) + mTotliq
                            QQQ1(mTT) = QQQ1(mTT) + mTotliq
                            TTT1(0) = TTT1(0) + mTotliq
                        End If
                    End If
                End If
                rc = d4skip(fFct, 1)
            End While
            rc = d4close(fFct)
            rc = code4initUndo(cb)
        Else
            ' Apenas um cliente (mSelectedClient)
            Dim fFct As Integer
            Dim cb = code4init()
            fFct = d4open(cb, fPCFFCT)
            Dim tag As Integer = d4tag(fFct, "PCFFCT2")
            Call d4tagSelect(fFct, tag)

            FILTRO = "F_TYPE $ 'FCD'"
            ' sx_SetFilter(FILTRO)

            Dim fType As Integer = d4field(fFct, "F_TYPE")
            Dim fStatus As Integer = d4field(fFct, "F_STATUS")
            Dim fNumber As Integer = d4field(fFct, "F_NUMBER")
            Dim fClient As Integer = d4field(fFct, "F_CLIENT")
            Dim fName As Integer = d4field(fFct, "F_NAME")
            Dim fVend As Integer = d4field(fFct, "F_VEND")
            Dim fInvDt As Integer = d4field(fFct, "F_INV_DT")
            Dim fTotLiq As Integer = d4field(fFct, "F_TOTLIQ")
            Dim fTotCus As Integer = d4field(fFct, "F_TOTCUS")
            Dim fIVA As Integer = d4field(fFct, "F_IVA")
            Dim fMoeda As Integer = d4field(fFct, "F_MOEDA")

            d4seek(fFct, mSelectedClient)
            If d4eof(fFct) = 0 Then
                Label1.Text = Trim(mSelectedClient) + " " + f4str(fName)
            Else
                Label1.Text = Trim(mSelectedClient) + " - Desconhecido"
            End If

            While (Trim(f4str(fClient)) = mSelectedClient) And d4eof(fFct) = 0
                mSelect = "s"
                mType = f4str(fType)
                mStatus = f4str(fStatus)
                If mStatus = "A" Or mStatus = "D" Or InStr("FCD$X", mType) = 0 Then
                    mSelect = "n"
                End If
                Dim xDataFactura As String = f4str(fInvDt)
                mDataFactura = DateTime.ParseExact(xDataFactura, "yyyyMMdd", provider)
                If mDataFactura > Today Then
                    mSelect = "n"
                End If
                If mSelect = "s" Then
                    mTotliq = f4long(fTotliq) - f4long(fIVA)
                    If f4str(fMoeda) = "ESC" Then
                        mTotliq = mTotliq / mEscEcu
                    End If
                    mDataFactura = DateTime.ParseExact(xDataFactura, "yyyyMMdd", provider)
                    If InStr("CX", mType) > 0 Then
                        mTotliq = mTotliq * -1
                    End If
                    mMM = DatePart(DateInterval.Month, mDataFactura) - 1
                    mTT = DatePart(DateInterval.Quarter, mDataFactura) - 1
                    If DatePart(DateInterval.Year, mDataFactura) = DatePart(DateInterval.Year, Today()) Then
                        Mes3(mMM) = Mes3(mMM) + mTotliq
                        Tri3(mTT) = Tri3(mTT) + mTotliq
                        Tot1(2) = Tot1(2) + mTotliq
                    ElseIf DatePart(DateInterval.Year, mDataFactura) = DatePart(DateInterval.Year, Today()) - 1 Then
                        Mes2(mMM) = Mes2(mMM) + mTotliq
                        Tri2(mTT) = Tri2(mTT) + mTotliq
                        Tot1(1) = Tot1(1) + mTotliq
                        If mDataFactura > DateAdd(DateInterval.Year, -1, Today()) Then
                            MMM2(mMM) = MMM2(mMM) + mTotliq
                            QQQ2(mTT) = QQQ2(mTT) + mTotliq
                            TTT1(1) = TTT1(1) + mTotliq
                        End If
                    ElseIf DatePart(DateInterval.Year, mDataFactura) = DatePart(DateInterval.Year, Today()) - 2 Then
                        Mes1(mMM) = Mes1(mMM) + mTotliq
                        Tri1(mTT) = Tri1(mTT) + mTotliq
                        Tot1(0) = Tot1(0) + mTotliq
                        If mDataFactura > DateAdd(DateInterval.Year, -2, Today()) Then
                            MMM1(mMM) = MMM1(mMM) + mTotliq
                            QQQ1(mTT) = QQQ1(mTT) + mTotliq
                            TTT1(0) = TTT1(0) + mTotliq
                        End If
                    End If
                End If
                rc = d4skip(fFct, 1)
            End While
            rc = d4close(fFct)
            rc = code4initUndo(cb)
        End If

        ' ======== CHART ===================================================
        C1Chart.DataBindings.Clear()
        If mStatMode = "Mes" Then
            DataTable1.Clear()
            Dim xxx As Integer = DataTable1.Columns.Count
            If DataTable1.Columns.Count = 0 Then
                DataTable1.Columns.Add("Mes")
                DataTable1.Columns.Add("Valor1")
                DataTable1.Columns.Add("Valor2")
                DataTable1.Columns.Add("Valor3")
            End If

            Dim row As DataRow
            Dim i1 As Integer
            For i1 = 0 To 11
                row = DataTable1.NewRow()
                row("Mes") = Meses(i1)
                row("Valor1") = CInt(Mes3(i1))
                row("Valor2") = CInt(Mes2(i1))
                row("Valor3") = CInt(Mes1(i1))
                DataTable1.Rows.Add(row)
            Next i1
            C1Chart.DataSource = DataTable1
            Dim cb3 As New C1.Web.Wijmo.Controls.C1Chart.C1ChartBinding()
            cb3.XField = "Mes"
            cb3.XFieldType = C1.Web.Wijmo.Controls.C1Chart.ChartDataXFieldType.String
            cb3.YField = "Valor1"
            cb3.YFieldType = C1.Web.Wijmo.Controls.C1Chart.ChartDataYFieldType.Number
            Dim cb2 As New C1.Web.Wijmo.Controls.C1Chart.C1ChartBinding()
            cb2.XField = "Mes"
            cb2.XFieldType = C1.Web.Wijmo.Controls.C1Chart.ChartDataXFieldType.String
            cb2.YField = "Valor2"
            cb2.YFieldType = C1.Web.Wijmo.Controls.C1Chart.ChartDataYFieldType.Number
            Dim cb1 As New C1.Web.Wijmo.Controls.C1Chart.C1ChartBinding()
            cb1.XField = "Mes"
            cb1.XFieldType = C1.Web.Wijmo.Controls.C1Chart.ChartDataXFieldType.String
            cb1.YField = "Valor3"
            cb1.YFieldType = C1.Web.Wijmo.Controls.C1Chart.ChartDataYFieldType.Number
            C1Chart.DataBindings.Add(cb3)
            C1Chart.DataBindings.Add(cb2)
            C1Chart.DataBindings.Add(cb1)
            C1Chart.DataBind()
            C1Chart.Legend.Visible = False
            'C1Chart.SeriesList.Item(0).Label = "2010"
            'C1Chart.SeriesList.Item(1).Label = "2011"
            'C1Chart.SeriesList.Item(2).Label = "2012"
            ' ===== TABLE ============
            Table1.Rows.Clear()
            Dim MyRow As New TableRow
            Table1.BackColor = Drawing.Color.White
            Table1.BorderWidth = 1
            Table1.BorderColor = Drawing.Color.Blue
            Table1.CellPadding = 4
            Table1.CellSpacing = 3
            Table1.ForeColor = Drawing.Color.Blue
            Dim MyCell As New TableCell
            MyCell.Text = "Mes"
            MyRow.Cells.Add(MyCell)
            MyCell = New TableCell
            MyCell.Text = "2010"
            MyCell.HorizontalAlign = HorizontalAlign.Right
            MyCell.BackColor = Drawing.Color.Green
            MyRow.Cells.Add(MyCell)
            MyCell = New TableCell
            MyCell.Text = "2011"
            MyCell.HorizontalAlign = HorizontalAlign.Right
            MyCell.BackColor = Drawing.Color.DeepSkyBlue
            MyRow.Cells.Add(MyCell)
            MyCell = New TableCell
            MyCell.Text = "2012"
            MyCell.HorizontalAlign = HorizontalAlign.Right
            MyCell.BackColor = Drawing.Color.SteelBlue
            MyRow.Cells.Add(MyCell)
            MyRow.ForeColor = Drawing.Color.White
            Table1.Rows.Add(MyRow)
            For i1 = 1 To 12
                MyRow = New TableRow
                MyCell = New TableCell
                MyCell.Text = Meses(i1 - 1)
                MyCell.HorizontalAlign = HorizontalAlign.Left
                MyCell.Font.Bold = True
                MyRow.Cells.Add(MyCell)
                MyCell = New TableCell
                MyCell.Text = Format(Mes1(i1 - 1), "###,##0")
                MyCell.HorizontalAlign = HorizontalAlign.Right
                MyCell.Font.Bold = False
                MyRow.Cells.Add(MyCell)
                MyCell = New TableCell
                MyCell.Text = Format(Mes2(i1 - 1), "###,##0")
                MyCell.HorizontalAlign = HorizontalAlign.Right
                MyCell.Font.Bold = False
                MyRow.Cells.Add(MyCell)
                MyCell = New TableCell
                MyCell.Text = Format(Mes3(i1 - 1), "###,##0")
                MyCell.HorizontalAlign = HorizontalAlign.Right
                MyCell.Font.Bold = False
                MyRow.Cells.Add(MyCell)
                MyRow.ForeColor = Drawing.Color.DarkGray
                MyRow.BackColor = Drawing.Color.White
                Table1.Rows.Add(MyRow)
            Next
        Else
            ' Ano
            ' ------------
            DataTable1.Clear()
            Dim xxx As Integer = DataTable1.Columns.Count
            If DataTable1.Columns.Count = 0 Then
                DataTable1.Columns.Add("Ano")
                DataTable1.Columns.Add("Valor1")
                DataTable1.Columns.Add("Valor2")
                DataTable1.Columns.Add("Valor3")
            End If
            Dim row As DataRow
            For i = 0 To 2
                row = DataTable1.NewRow()
                row("Ano") = Anos(i)
                'row("Valor1") = CInt(Tot3(i))
                'row("Valor2") = CInt(Tot2(i))
                row("Valor3") = CInt(Tot1(i))
                DataTable1.Rows.Add(row)
            Next i
            C1Chart.DataSource = DataTable1

            'Dim cb3 As New C1.Web.Wijmo.Controls.C1Chart.C1ChartBinding()
            'cb3.XField = "Ano"
            'cb3.XFieldType = C1.Web.Wijmo.Controls.C1Chart.ChartDataXFieldType.String
            'cb3.YField = "Valor1"
            'cb3.YFieldType = C1.Web.Wijmo.Controls.C1Chart.ChartDataYFieldType.Number

            'Dim cb2 As New C1.Web.Wijmo.Controls.C1Chart.C1ChartBinding()
            'cb2.XField = "Ano"
            'cb2.XFieldType = C1.Web.Wijmo.Controls.C1Chart.ChartDataXFieldType.String
            'cb2.YField = "Valor2"
            'cb2.YFieldType = C1.Web.Wijmo.Controls.C1Chart.ChartDataYFieldType.Number

            Dim cb1 As New C1.Web.Wijmo.Controls.C1Chart.C1ChartBinding()
            cb1.XField = "Ano"
            cb1.XFieldType = C1.Web.Wijmo.Controls.C1Chart.ChartDataXFieldType.String
            cb1.YField = "Valor3"
            cb1.YFieldType = C1.Web.Wijmo.Controls.C1Chart.ChartDataYFieldType.Number
            'C1Chart.DataBindings.Add(cb3)
            'C1Chart.DataBindings.Add(cb2)
            C1Chart.DataBindings.Add(cb1)
            C1Chart.DataBind()
            C1Chart.Legend.Visible = False

            ' ===== TABLE ============
            Table1.Rows.Clear()
            Dim MyRow As New TableRow
            Table1.BackColor = Drawing.Color.White
            Table1.BorderWidth = 1
            Table1.BorderColor = Drawing.Color.Blue
            Table1.CellPadding = 4
            Table1.CellSpacing = 3
            Table1.ForeColor = Drawing.Color.Blue
            Dim MyCell As New TableCell
            MyCell.Text = "Ano"
            MyRow.Cells.Add(MyCell)
            MyCell = New TableCell
            MyCell.Text = "Vendas"
            MyCell.HorizontalAlign = HorizontalAlign.Right
            MyCell.BackColor = Drawing.Color.Green
            MyRow.Cells.Add(MyCell)
            MyCell = New TableCell
            MyCell.Text = "Lucro"
            MyCell.HorizontalAlign = HorizontalAlign.Right
            MyCell.BackColor = Drawing.Color.DeepSkyBlue
            MyRow.Cells.Add(MyCell)
            MyCell = New TableCell
            MyCell.Text = "Margem"
            MyCell.HorizontalAlign = HorizontalAlign.Right
            MyCell.BackColor = Drawing.Color.SteelBlue
            MyRow.Cells.Add(MyCell)
            MyRow.ForeColor = Drawing.Color.White
            Table1.Rows.Add(MyRow)
            For i = 1 To 3
                MyRow = New TableRow
                MyCell = New TableCell
                MyCell.Text = Anos(i - 1)
                MyCell.HorizontalAlign = HorizontalAlign.Left
                MyCell.Font.Bold = True
                MyRow.Cells.Add(MyCell)
                MyCell = New TableCell
                MyCell.Text = Format(Tot1(i - 1), "###,##0")
                MyCell.HorizontalAlign = HorizontalAlign.Right
                MyCell.Font.Bold = False
                MyRow.Cells.Add(MyCell)
                'MyCell = New TableCell
                'MyCell.Text = Format(Tot2(i - 1), "###,##0")
                'MyCell.HorizontalAlign = HorizontalAlign.Right
                'MyCell.Font.Bold = False
                'MyRow.Cells.Add(MyCell)
                'MyCell = New TableCell
                'MyCell.Text = Format(Tot3(i - 1), "###,##0")
                'MyCell.HorizontalAlign = HorizontalAlign.Right
                'MyCell.Font.Bold = False
                'MyRow.Cells.Add(MyCell)
                'MyRow.ForeColor = Drawing.Color.DarkGray
                'MyRow.BackColor = Drawing.Color.White
                Table1.Rows.Add(MyRow)
            Next
        End If
        Return True
    End Function

    'Protected Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
    '    If mStatMode = "Ano" Then
    '        mStatMode = "Mes"
    '    Else
    '        mStatMode = "Ano"
    '    End If

    'End Sub

    Protected Sub btnFicha_Click(sender As Object, e As EventArgs) Handles btnFicha.Click
        Dim strMessage As String
        strMessage = "Ainda não está implementado"
        Dim strScript As String = "<script language=JavaScript>"
        strScript = strScript + "alert(""" & strMessage & """);"
        strScript = strScript + "</script>"
        If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript")) Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript", strScript)
        End If
    End Sub

    Protected Sub btnDescontos_Click(sender As Object, e As EventArgs) Handles btnDescontos.Click
        Dim strMessage As String
        strMessage = "Ainda não está implementado"
        Dim strScript As String = "<script language=JavaScript>"
        strScript = strScript + "alert(""" & strMessage & """);"
        strScript = strScript + "</script>"
        If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript")) Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript", strScript)
        End If
    End Sub

    Protected Sub btnMapa_Click(sender As Object, e As EventArgs) Handles btnMapa.Click
        Dim strMessage As String
        strMessage = "Ainda não está implementado"
        Dim strScript As String = "<script language=JavaScript>"
        strScript = strScript + "alert(""" & strMessage & """);"
        strScript = strScript + "</script>"
        If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript")) Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript", strScript)
        End If
    End Sub
End Class