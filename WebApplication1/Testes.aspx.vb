Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Public Class Testes
    Inherits System.Web.UI.Page

    Public WithEvents oRpt As ReportDocument
    Private mFichPrincipal As Integer
    Private dataTable1 As New DataTable("Clientes")

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'C1PickerView1.Pickers(0).NumberRangeStart = 25
        'C1PickerView1.Pickers(0).NumberRangeEnd = 35
        'C1PickerView1.Pickers(0).Items.Clear()
        'Dim Game1 As New C1PickerItem()
        'Dim Game2 As New C1PickerItem()
        'Dim Game3 As New C1PickerItem()
        'Game1.Text = "Force Unleashed"
        'Game2.Text = "Morrowind"
        'Game3.Text = "The Witcher"
        'C1PickerView1.Pickers(0).Items.Add(Game1)
        'C1PickerView1.Pickers(0).Items.Add(Game2)
        'C1PickerView1.Pickers(0).Items.Add(Game3)

        'If IsPostBack Then
        '    Dim index As Integer = Convert.ToInt32(Request("__EVENTARGUMENT"))
        '    C1Clientes.SelectedIndex = index
        '    selectedIndex(index)
        'End If

        Session("mAgentLoggedIn") = "YES"
        Session("mAgentOnline") = "99"

        ' Colunas da Tabela de Clientes
        dataTable1.Columns.Add("Cod")
        dataTable1.Columns.Add("Nome")
        dataTable1.Columns.Add("Telefone")
        ' xLoadClientes()

    End Sub

    Private Sub selectedIndex(index As Integer)
        mSelectedClient = Request.Form("text1")
        ' MsgBox("Hello " & CStr(index) & "-->" & mSelectedClient)
    End Sub

    Function xLoadClientes()
        Dim row As DataRow
        Dim fCli As Integer
        Dim xx As Integer = 0
        Dim rc As Integer
        dataTable1.Clear()

        Dim cb = code4init()
        fCli = d4open(cb, fPCFCLI)
        Dim tag As Integer = 0
        tag = d4tag(fCli, "PCFCLI5")
        Call d4tagSelect(fCli, tag)
        Dim fStatus As Integer = d4field(fCli, "C_STATUS")
        Dim fClient As Integer = d4field(fCli, "C_CLIENT")
        Dim fName As Integer = d4field(fCli, "C_NAME")
        Dim fVend As Integer = d4field(fCli, "C_VEND")
        Dim fPhone1 As Integer = d4field(fCli, "C_PHONE1")
        Dim fContact As Integer = d4field(fCli, "C_CONTACT")
        If Session("mAgentOnline") = "99" Then
            rc = d4top(fCli)
        Else
            rc = d4seek(fCli, Session("mAgentOnline"))
        End If
        Dim agt As String = Session("mAgentOnline")
        ' MsgBox(agt + "<-")
        While d4eof(fCli) = 0
            mStatus = f4str(fStatus)
            mVend = f4str(fVend)
            Dim mSelect As String = "n"
            If mStatus = "A" Then
                If Session("mAgentOnline") = "99" Then
                    mSelect = "s"
                Else
                    If Trim(mVend) = Session("mAgentOnline") Then
                        mSelect = "s"
                    Else
                        mSelect = "n"
                    End If
                End If
            Else
                mSelect = "n"
            End If
            If mSelect = "s" Then
                row = dataTable1.NewRow()
                row("Cod") = f4str(fClient)
                row("Nome") = f4str(fName)
                row("Telefone") = f4str(fPhone1)
                dataTable1.Rows.Add(row)
            End If
            rc = d4skip(fCli, 1)
            If Session("mAgentOnline") = "99" Then
            Else
                If Trim(f4str(fVend)) <> Session("mAgentOnline") Then
                    rc = d4goEof(fCli)
                    rc = d4skip(fCli, 1)
                End If
            End If
        End While
        rc = d4close(fCli)
        rc = code4initUndo(cb)

        C1Clientes.Columns(0).Width = 60
        C1Clientes.Columns(1).Width = 350
        C1Clientes.Columns(2).Width = 150

        C1Clientes.DataSource = dataTable1
        C1Clientes.DataBind()
        Return True
    End Function

    'Public Function getAllOrders() As DataTable
    '    'Connection string replace 'databaseservername' with your db server name
    '    Dim sqlCon As String = "User ID=sa;PWD=sa; server=databaseservername;INITIAL CATALOG=SampleDB;" + "PERSISTSECURITY INFO=FALSE;Connect Timeout=0"
    '    Dim Con As New SqlConnection(sqlCon)
    '    Dim cmd As New SqlCommand()
    '    Dim ds As DataSet = Nothing
    '    Dim adapter As SqlDataAdapter
    '    Try
    '        Con.Open()
    '        'Stored procedure calling. It is already in sample db.
    '        cmd.CommandText = "getAllOrders"
    '        cmd.CommandType = CommandType.StoredProcedure
    '        cmd.Connection = Con
    '        ds = New DataSet()
    '        adapter = New SqlDataAdapter(cmd)
    '        adapter.Fill(ds, "Users")
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    Finally
    '        cmd.Dispose()
    '        If Con.State <> ConnectionState.Closed Then
    '            Con.Close()
    '        End If
    '    End Try
    '    Return ds.Tables(0)
    'End Function

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim strMessage As String
        C1Dialog1.ShowOnLoad = False
        strMessage = "Teste de mensagem no Cliente"
        Dim strScript As String = "<script language=JavaScript>"
        strScript = strScript + "alert(""" & strMessage & """);"
        strScript = strScript + "</script>"
        If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript")) Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript", strScript)
        End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        C1Dialog1.ShowOnLoad = True
        C1Dialog1.Visible = True
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim connStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='C:\PCFFILES\DATA\';Extended Properties=dBase 5.0"
        Dim conn As New OleDb.OleDbConnection(connStr)
        Dim cmd As New OleDb.OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SELECT C_CLIENT, C_NAME, C_PHONE1 FROM PCFCLI"
        conn.Open()
        Dim reader As OleDb.OleDbDataReader = cmd.ExecuteReader
        Dim dataTable1 As New DataTable
        dataTable1.Load(reader)
        C1Clientes.Columns(0).Width = 60
        C1Clientes.Columns(1).Width = 350
        C1Clientes.Columns(2).Width = 150
        C1Clientes.DataSource = dataTable1
        C1Clientes.ScrollMode = C1.Web.Wijmo.Controls.C1GridView.ScrollMode.Vertical
        C1Clientes.DataBind()
        conn.Close()
    End Sub
End Class