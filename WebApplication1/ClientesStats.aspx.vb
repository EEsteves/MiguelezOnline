Imports Microsoft.JScript
Imports C1.Web.Wijmo.Controls.C1Chart
Imports System.Globalization

Public Class ClientesStats
    Inherits System.Web.UI.Page
    Private DataTable1 As DataTable = New DataTable("Data1")
    Private mFunction As String = "One Client"
    Private Meses As String() = {"Ja", "Fe", "Ma", "Ab", "Ma", "Ju", "Jl", "Ag", "Se", "Ou", "No", "De"}
    Private mStatus As String = ""
    Private mCliNum As String = ""
    Private rc As Integer = 0
    Private i As Integer

    Private provider As CultureInfo = CultureInfo.InvariantCulture

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mSelectedClient = Request.QueryString("field1")
        If mSelectedClient = Nothing Then
            mSelectedClient = "006     "
            TextBox1.Text = mSelectedClient
        End If

        Dim fCli As Integer
        Dim rc As Integer
        Dim cb = code4init()
        fCli = d4open(cb, fPCFCLI)
        Dim tag As Integer = d4tag(fCli, "PCFCLI1")
        Call d4tagSelect(fCli, tag)
        Dim fStatus As Integer = d4field(fCli, "C_STATUS")
        Dim fClient As Integer = d4field(fCli, "C_CLIENT")
        Dim fName As Integer = d4field(fCli, "C_NAME")
        If d4seek(fCli, mSelectedClient) = 0 Then
            Label1.Text = Trim(mSelectedClient) + " " + f4str(fName)
        Else
            ' Erro: Cliente not found
        End If
        rc = d4close(fCli)
        rc = code4initUndo(cb)

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

        ' Apenas um cliente (mSelectedClient)
        Dim fFct As Integer
        Dim cb = code4init()
        fFct = d4open(cb, fPCFFCT)
        Dim tag As Integer = d4tag(fFct, "PCFFCT2")
        Call d4tagSelect(fFct, tag)

        FILTRO = "F_TYPE $ 'FCD'"
        ' sx_SetFilter(FILTRO)

        Dim masterRel As Integer = relate4init(fFct)
        relate4querySet(masterRel, "F_TYPE $ 'FCD'")
        relate4sortSet(masterRel, "F_CLIENT+DTOS(F_INV_DT)")
        relate4top(masterRel)

        Dim fType As Integer = d4field(fFct, "F_TYPE")
        Dim fStatus As Integer = d4field(fFct, "F_STATUS")
        Dim fNumber As Integer = d4field(fFct, "F_NUMBER")
        Dim fClient As Integer = d4field(fFct, "F_CLIENT")
        Dim fName As Integer = d4field(fFct, "F_NAME")
        Dim fVend As Integer = d4field(fFct, "F_VEND")
        Dim fInvDt As Integer = d4field(fFct, "F_INV_DT")
        Dim fTotliq As Integer = d4field(fFct, "F_TOTLIQ")
        Dim fIVA As Integer = d4field(fFct, "F_IVA")
        Dim fMoeda As Integer = d4field(fFct, "F_MOEDA")
        d4seek(fFct, mSelectedClient)
        While (f4str(fClient) = mSelectedClient) And d4eof(fFct) = 0
            mSelect = "s"
            mType = f4str(fType)
            mStatus = f4str(fStatus)
            If mStatus = "A" Or mStatus = "D" Or InStr("FCD$X", mType) = 0 Then
                mSelect = "n"
            End If
            If mSelect = "s" Then
                mTotliq = f4long(fTotliq) - f4long(fIVA)
                If f4str(fMoeda) = "ESC" Then
                    mTotliq = mTotliq / mEscEcu
                End If
                Dim xDataFactura As String = f4str(fInvDt)
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

        ' ======== CHART ===================================================
        C1Chart.DataBindings.Clear()

        DataTable1.Clear()
        Dim xxx As Integer = DataTable1.Columns.Count
        If DataTable1.Columns.Count = 0 Then
            DataTable1.Columns.Add("Mes")
            DataTable1.Columns.Add("Valor1")
            DataTable1.Columns.Add("Valor2")
            DataTable1.Columns.Add("Valor3")
        End If

        Dim row As DataRow

        For i = 0 To 11
            row = DataTable1.NewRow()
            row("Mes") = Meses(i)
            row("Valor1") = CInt(Mes3(i))
            row("Valor2") = CInt(Mes2(i))
            row("Valor3") = CInt(Mes1(i))
            DataTable1.Rows.Add(row)
        Next i
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

        C1Chart.SeriesList.Item(0).Label = "2010"
        C1Chart.SeriesList.Item(1).Label = "2011"
        C1Chart.SeriesList.Item(2).Label = "2012"

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
        MyCell.BackColor = Drawing.Color.LightGreen
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

        For i = 1 To 12
            MyRow = New TableRow
            MyCell = New TableCell
            MyCell.Text = Meses(i - 1)
            MyCell.HorizontalAlign = HorizontalAlign.Left
            MyCell.Font.Bold = True
            MyRow.Cells.Add(MyCell)

            MyCell = New TableCell
            MyCell.Text = Format(Mes1(i - 1), "###,##0")
            MyCell.HorizontalAlign = HorizontalAlign.Right
            MyCell.Font.Bold = False
            MyRow.Cells.Add(MyCell)

            MyCell = New TableCell
            MyCell.Text = Format(Mes2(i - 1), "###,##0")
            MyCell.HorizontalAlign = HorizontalAlign.Right
            MyCell.Font.Bold = False
            MyRow.Cells.Add(MyCell)

            MyCell = New TableCell
            MyCell.Text = Format(Mes3(i - 1), "###,##0")
            MyCell.HorizontalAlign = HorizontalAlign.Right
            MyCell.Font.Bold = False
            MyRow.Cells.Add(MyCell)

            MyRow.ForeColor = Drawing.Color.DarkGray
            MyRow.BackColor = Drawing.Color.White

            Table1.Rows.Add(MyRow)
        Next
        Return True
    End Function

    Protected Sub btnPrev_Click1(sender As Object, e As EventArgs) Handles btnPrev.Click
        mSelectedClient = Mid(Trim(Request.Form("text1")) + Space(8), 1, 8)
        Dim fCli As Integer
        Dim rc As Integer
        Dim cb = code4init()
        fCli = d4open(cb, fPCFCLI)
        Dim tag As Integer = d4tag(fCli, "PCFCLI1")
        Call d4tagSelect(fCli, tag)
        Dim fStatus As Integer = d4field(fCli, "C_STATUS")
        Dim fClient As Integer = d4field(fCli, "C_CLIENT")
        Dim fCliNm As Integer = d4field(fCli, "C_NAME")
        d4seek(fCli, mSelectedClient)
        rc = d4skip(fCli, -1)
        While d4bof(fCli) = 0
            mStatus = f4str(fStatus)
            If mStatus = "X" Then
                rc = d4skip(fCli, -1)
            Else
                mSelectedClient = f4str(fClient)
                Dim mCliNm As String = f4str(fCliNm)
                TextBox1.Text = mSelectedClient
                mCliNum = mSelectedClient
                rc = d4close(fCli)
                rc = code4initUndo(cb)
                Label1.Text = Trim(mSelectedClient) + " " + mCliNm
                xLoadStats(mSelectedClient)
                Exit Sub
            End If
        End While
        If d4bof(fCli) <> 0 Then
            rc = d4top(fCli)
            mSelectedClient = f4str(fClient)
            TextBox1.Text = mSelectedClient
            mCliNum = mSelectedClient
            rc = d4close(fCli)
            rc = code4initUndo(cb)
            ' substituir msgbox
            ' MsgBox("Início do ficheiro", MsgBoxStyle.Information)
            Exit Sub
        End If
    End Sub

    Protected Sub btnNext_Click1(sender As Object, e As EventArgs) Handles btnNext.Click
        mSelectedClient = Mid(Trim(Request.Form("text1")) + Space(8), 1, 8)
        Dim fCli As Integer
        Dim rc As Integer
        Dim cb = code4init()
        fCli = d4open(cb, fPCFCLI)
        Dim tag As Integer = d4tag(fCli, "PCFCLI1")
        Call d4tagSelect(fCli, tag)
        Dim fStatus As Integer = d4field(fCli, "C_STATUS")
        Dim fClient As Integer = d4field(fCli, "C_CLIENT")
        Dim fCliNm As Integer = d4field(fCli, "C_NAME")
        d4seek(fCli, mSelectedClient)
        rc = d4skip(fCli, 1)
        While d4eof(fCli) = 0
            mStatus = f4str(fStatus)
            If mStatus = "X" Then
                rc = d4skip(fCli, 1)
            Else
                mSelectedClient = f4str(fClient)
                Dim mCliNm As String = f4str(fCliNm)
                TextBox1.Text = mSelectedClient
                mCliNum = mSelectedClient
                rc = d4close(fCli)
                rc = code4initUndo(cb)
                Label1.Text = Trim(mSelectedClient) + " " + mCliNm
                xLoadStats(mSelectedClient)
                Exit Sub
            End If
        End While
        If d4eof(fCli) <> 0 Then
            rc = d4bottom(fCli)
            mSelectedClient = f4str(fClient)
            TextBox1.Text = mSelectedClient
            mCliNum = mSelectedClient
            rc = d4close(fCli)
            rc = code4initUndo(cb)
            ' substituir msgbox
            ' MsgBox("Fim do ficheiro", MsgBoxStyle.Information)
            Exit Sub
        End If
    End Sub

End Class