
Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("mAgentLoggedIn") = "NO" Then
                mMsg.Text = "Por favor, faça Login!"
                mMsg.ForeColor = Drawing.Color.Red
            Else
                mMsg.Text = "Benvindo " + Trim(Session("mAgentName"))
                mMsg.ForeColor = Drawing.Color.DarkGreen
            End If
        End If
    End Sub

End Class