Imports Microsoft.JScript
Imports C1.Web.Wijmo.Controls.C1Chart
Imports System.Globalization

Public Class EncomendasStats
    Inherits System.Web.UI.Page
    Private DataTable1 As DataTable = New DataTable("Data1")
    Private Meses As String() = {"Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"}
    Private Tri As String() = {"T1", "T2", "T3", "T4"}
    Private Anos As String() = {"2011", "2012", "2013"}
    Private provider As CultureInfo = CultureInfo.InvariantCulture
    Private mSelectedAgent As String = ""

    Private mStartDt As String = "01/01/2011"
    Private mEndDt As String = "31/12/2013"

    Private Mes1(12) As Double
    Private Mes2(12) As Double
    Private Mes3(12) As Double

    Private Anual(2) As Double

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mSelectedAgent = Session("mAgentOnline")
        mSelectedAgent = "03"
        xLoadStats()
        ' xShowChartMensal()
        xShowChartAnual()
    End Sub

    Function xLoadStats()
        Dim ii As Integer
        For ii = 0 To 11
            Mes1(ii) = 0
            Mes2(ii) = 0
            Mes3(ii) = 0
        Next ii
        Anual(0) = 0
        Anual(1) = 0
        Anual(2) = 0

        Dim connStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:\PCFFILES\DATA\';Extended Properties=dBase 5.0"
        Dim conn As New OleDb.OleDbConnection(connStr)
        Dim cmd As New OleDb.OleDbCommand()
        cmd.Connection = conn
        mC0 = "SELECT E_CLIENT, E_VEND, E_DATE, E_TOTAL, E_IVA, E_MOEDA FROM PCFECL WHERE "
        mC0 = mC0 + "E_STATUS LIKE '[ABCD]%' "
        If Len(Trim(mSelectedAgent)) > 0 Then
            mC0 = mC0 + "AND E_VEND = '" + mSelectedAgent + "' "
        End If
        mC0 = mC0 + "AND E_DATE BETWEEN CDATE('" + mStartDt + "') AND CDATE('" + mEndDt + "') "
        cmd.CommandText = mC0
        conn.Open()
        Dim reader As OleDb.OleDbDataReader = cmd.ExecuteReader
        While reader.Read()
            mTotal = CDbl(reader("E_TOTAL"))
            mIva = CDbl(reader("E_IVA"))
            mMoeda = reader("E_MOEDA").ToString
            Dim mDataEncomenda As Date = CDate(reader("E_DATE"))
            mTotliq = mTotal - mIva
            If mMoeda = "ESC" Then
                mTotliq = mTotliq / 200.482
            End If
            mTotliq = Math.Round(mTotliq, 0)
            mMM = DatePart(DateInterval.Month, mDataEncomenda) - 1
            mTT = DatePart(DateInterval.Quarter, mDataEncomenda) - 1
            If DatePart(DateInterval.Year, mDataEncomenda) = DatePart(DateInterval.Year, Today()) Then
                Mes3(mMM) = Mes3(mMM) + mTotliq
                Anual(2) = Anual(2) + mTotliq
            ElseIf DatePart(DateInterval.Year, mDataEncomenda) = DatePart(DateInterval.Year, Today()) - 1 Then
                Mes2(mMM) = Mes2(mMM) + mTotliq
                Anual(1) = Anual(1) + mTotliq
            ElseIf DatePart(DateInterval.Year, mDataEncomenda) = DatePart(DateInterval.Year, Today()) - 2 Then
                Mes1(mMM) = Mes1(mMM) + mTotliq
                Anual(0) = Anual(0) + mTotliq
            End If
        End While
        reader.Close()
        conn.Close()

        MsgBox(Anual(0))
        MsgBox(Anual(1))
        MsgBox(Anual(2))

    End Function

    Function xShowChartMensal()
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
        Dim i1 As Integer
        For i1 = 0 To 11
            row = DataTable1.NewRow()
            row("Mes") = Meses(i1)
            row("Valor1") = CInt(Mes1(i1))
            row("Valor2") = CInt(Mes2(i1))
            row("Valor3") = CInt(Mes3(i1))
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
        MyCell.Text = "2011"
        MyCell.HorizontalAlign = HorizontalAlign.Right
        MyCell.BackColor = Drawing.Color.Green
        MyRow.Cells.Add(MyCell)
        MyCell = New TableCell
        MyCell.Text = "2012"
        MyCell.HorizontalAlign = HorizontalAlign.Right
        MyCell.BackColor = Drawing.Color.DeepSkyBlue
        MyRow.Cells.Add(MyCell)
        MyCell = New TableCell
        MyCell.Text = "2013"
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
        Return True
    End Function

    Function xShowChartAnual()
     
        C1Chart.DataBindings.Clear()
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
            row("Valor1") = CInt(Anual(i))
            row("Valor2") = CInt(Anual(i))
            row("Valor3") = CInt(Anual(i))
            DataTable1.Rows.Add(row)
        Next i
        C1Chart.DataSource = DataTable1
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
        'MyCell.Text = "Ano"
        'MyRow.Cells.Add(MyCell)
        'MyCell = New TableCell
        'MyCell.Text = "xxx"
        'MyCell.HorizontalAlign = HorizontalAlign.Right
        'MyCell.BackColor = Drawing.Color.Green
        'MyRow.Cells.Add(MyCell)
        'MyCell = New TableCell
        'MyCell.Text = "xxx"
        'MyCell.HorizontalAlign = HorizontalAlign.Right
        'MyCell.BackColor = Drawing.Color.DeepSkyBlue
        'MyRow.Cells.Add(MyCell)
        'MyCell = New TableCell
        MyCell.Text = "Encomendas"
        MyCell.HorizontalAlign = HorizontalAlign.Right
        MyCell.BackColor = Drawing.Color.SteelBlue
        MyRow.Cells.Add(MyCell)
        MyRow.ForeColor = Drawing.Color.White
        Table1.Rows.Add(MyRow)
        For i = 1 To 3
            MyRow = New TableRow
            'MyCell = New TableCell
            'MyCell.Text = Anos(i - 1)
            'MyCell.HorizontalAlign = HorizontalAlign.Left
            'MyCell.Font.Bold = True
            'MyRow.Cells.Add(MyCell)
            'MyCell = New TableCell
            'MyCell.Text = Format(Anual(i - 1), "##,###,##0")
            'MyCell.HorizontalAlign = HorizontalAlign.Right
            'MyCell.Font.Bold = False
            'MyRow.Cells.Add(MyCell)
            'MyCell = New TableCell
            'MyCell.Text = Format(Anual(i - 1), "##,###,##0")
            'MyCell.HorizontalAlign = HorizontalAlign.Right
            'MyCell.Font.Bold = False
            'MyRow.Cells.Add(MyCell)
            MyCell = New TableCell
            MyCell.Text = Format(Anual(i - 1), "##,###,##0")
            MyCell.HorizontalAlign = HorizontalAlign.Right
            MyCell.Font.Bold = False
            MyRow.Cells.Add(MyCell)
            MyRow.ForeColor = Drawing.Color.DarkGray
            MyRow.BackColor = Drawing.Color.White
            Table1.Rows.Add(MyRow)
        Next
        Return True
    End Function
End Class