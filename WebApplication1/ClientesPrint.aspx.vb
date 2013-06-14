Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class ClientesPrint
    Inherits System.Web.UI.Page

    Public WithEvents oRpt As ReportDocument
    Private mFichPrincipal As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim rpt As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rpt = New CrystalDecisions.CrystalReports.Engine.ReportDocument()
        rpt.Load(Server.MapPath("CR_PCFCLI1.rpt"))
    End Sub

End Class