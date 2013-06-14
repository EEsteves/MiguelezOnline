Imports System.Web

Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim xOper As String = C1ComboBox1.Text

        Dim xx As String = sPassword.Text

        Dim mPassword As String = Mid(xx, 1, 4)
        Dim mOper = Mid(xOper, 1, 2)

        'Dim strMessage1 As String
        'strMessage1 = "Password " & mOper & mPassword
        'Dim strScript1 As String = "<script language=JavaScript>"
        'strScript1 = strScript1 + "alert(""" & strMessage1 & """);"
        'strScript1 = strScript1 + "</script>"
        'If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript")) Then
        '    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript", strScript1)
        'End If

        If mOper = "01" And mPassword = "1717" Then
            Session("mAgentLoggedIn") = "YES"
            Session("mAgentName") = "Sr. Branquinho"
            Session("mAgentOnline") = "99"
            Dim xStr As String = "~/Default.aspx"
            Response.Redirect(xStr)
        End If
        If mOper = "02" And mPassword = "8569" Then
            Session("mAgentLoggedIn") = "YES"
            Session("mAgentOnline") = "02"
            Session("mAgentName") = "Sr. Fernando Guerreiro"
            Dim xStr As String = "~/Default.aspx"
            Response.Redirect(xStr)
        End If
        If mOper = "03" And mPassword = "6969" Then
            Session("mAgentLoggedIn") = "YES"
            Session("mAgentOnline") = "03"
            Session("mAgentName") = "Sr. José Pais"
            Dim xStr As String = "~/Default.aspx"
            Response.Redirect(xStr)
        End If
        If mOper = "04" And mPassword = "9696" Then
            Session("mAgentLoggedIn") = "YES"
            Session("mAgentName") = "Sr. Pereira da Silva"
            Session("mAgentOnline") = "04"
            Dim xStr As String = "~/Default.aspx"
            Response.Redirect(xStr)
        End If

        Session("mAgentLoggedIn") = "NO"
        Dim strMessage As String
        strMessage = "Login Inválido " & mOper + " " + mPassword
        Dim strScript As String = "<script language=JavaScript>"
        strScript = strScript + "alert(""" & strMessage & """);"
        strScript = strScript + "</script>"
        If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript")) Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript", strScript)
        End If
    End Sub
End Class