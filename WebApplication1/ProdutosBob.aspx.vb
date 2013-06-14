Imports C1.Web.Wijmo.Controls.C1ComboBox
Imports C1.Web.Wijmo.Controls.C1Menu
Imports Microsoft.JScript

Public Class ProdutosBob
    Inherits System.Web.UI.Page
    Private dataTable1 As New DataTable("Bobines")
    Private mStatus As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        mSelectedProduct = Request.QueryString("field1")
        If mSelectedProduct = Nothing Then
            mSelectedProduct = "223030285"
        End If

        dataTable1.Columns.Add("Tipo")
        dataTable1.Columns.Add("Matrícula")
        dataTable1.Columns.Add("Local")
        dataTable1.Columns.Add("Res")
        dataTable1.Columns.Add("Quant")

        xLoadFicha()
        xLoadBobs()
    End Sub

    Function xLoadFicha()
        Dim fPro As Integer
        Dim rc As Integer
        Dim cb = code4init()
        fPro = d4open(cb, fPCFPRO)
        Dim tag As Integer = d4tag(fPro, "PCfPro1")
        Call d4tagSelect(fPro, tag)
        Dim fProduct As Integer = d4field(fPro, "P_PRODUCT")
        Dim fName As Integer = d4field(fPro, "P_NAME")
        Dim fRef As Integer = d4field(fPro, "P_REF")
        Dim fRefGen As Integer = d4field(fPro, "P_REF_GEN")
        Dim fStkIni As Integer = d4field(fPro, "P_STK_INI")
        Dim fStkIn As Integer = d4field(fPro, "P_STK_IN")
        Dim fStkOut As Integer = d4field(fPro, "P_STK_OUT")

        If d4seek(fPro, mSelectedProduct) = 0 Then
            TextBox1.Text = mSelectedProduct
            Label2.Text = f4str(fName)
            Label3.Text = f4str(fRef)
            Label4.Text = f4str(fRefGen)
            Label5.Text = Format(f4double(fStkIni) + f4double(fStkIn) - f4double(fStkOut), "###,##0")
        Else
            ' Erro: Cliente not found
        End If
        rc = d4close(fPro)
        rc = code4initUndo(cb)
    End Function

    Function xLoadBobs()
        Dim row As DataRow
        Dim fBob As Integer
        Dim xx As Integer = 0
        Dim rc As Integer
        dataTable1.Clear()
        Dim cb = code4init()
        fBob = d4open(cb, fPCFBOB)
        Dim tag As Integer = d4tag(fBob, "PCFBOB3")
        Call d4tagSelect(fBob, tag)
        Dim fProduct As Integer = d4field(fBob, "B_PRODUCT")
        Dim fBobine As Integer = d4field(fBob, "B_BOBINE")
        Dim fType As Integer = d4field(fBob, "B_TYPE")
        Dim fLocal As Integer = d4field(fBob, "B_LOCAL")
        Dim fRes As Integer = d4field(fBob, "B_RES")
        Dim fQtyIni As Integer = d4field(fBob, "B_QTY_INI")
        Dim fCortes As Integer = d4field(fBob, "B_CORTES")
        rc = d4top(fBob)
        d4seek(fBob, mSelectedProduct)
        Dim mProduct As String = f4str(fProduct)
        Dim mQty As Double = 0
        While Trim(mProduct) = Trim(mSelectedProduct) And d4eof(fBob) = 0
            mQty = f4long(fQtyIni) - f4long(fCortes)
            If mQty <> 0 Then
                xx = xx + 1
                row = dataTable1.NewRow()
                mProduct = f4str(fProduct)
                row("Matrícula") = f4str(fBobine)
                row("Tipo") = f4str(fType)
                row("Local") = f4str(fLocal)
                row("Res") = f4str(fRes)
                row("Quant") = Format(mQty, "###,###")
                dataTable1.Rows.Add(row)
            End If
            rc = d4skip(fBob, 1)
        End While
        If xx = 0 Then
            row = dataTable1.NewRow()
            row("Matrícula") = "SEM STOCK"
            row("Tipo") = Space(1)
            row("Local") = Space(1)
            row("Res") = Space(1)
            row("Quant") = Format(0, "###,###")
            dataTable1.Rows.Add(row)
        End If
        rc = d4close(fBob)
        rc = code4initUndo(cb)
        C1GridView1.DataSource = dataTable1
        C1GridView1.DataBind()
        Return True
    End Function

    Protected Sub btnReturn_Click(sender As Object, e As EventArgs) Handles btnReturn.Click
        Dim xStr As String = "~/Produtos.aspx"
        Response.Redirect(xStr)
    End Sub

    Protected Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        mSelectedProduct = Mid(Trim(Request.Form("text1")) + Space(9), 1, 9)
        Dim fPro As Integer
        Dim rc As Integer
        Dim cb = code4init()
        fPro = d4open(cb, fPCFPRO)
        Dim tag As Integer = d4tag(fPro, "PCFPRO1")
        Call d4tagSelect(fPro, tag)
        Dim fStatus As Integer = d4field(fPro, "P_STATUS")
        Dim fProduct As Integer = d4field(fPro, "P_PRODUCT")
        d4seek(fPro, mSelectedProduct)
        rc = d4skip(fPro, 1)
        While d4eof(fPro) = 0
            mStatus = f4str(fStatus)
            If mStatus = "X" Then
                rc = d4skip(fPro, 1)
            Else
                mSelectedProduct = f4str(fProduct)
                TextBox1.Text = mSelectedProduct
                rc = d4close(fPro)
                rc = code4initUndo(cb)
                xLoadFicha()
                xLoadBobs()
                Exit Sub
            End If
        End While
        If d4eof(fPro) <> 0 Then
            rc = d4bottom(fPro)
            mSelectedProduct = f4str(fProduct)
            TextBox1.Text = mSelectedProduct
            rc = d4close(fPro)
            rc = code4initUndo(cb)
            Dim strMessage As String
            strMessage = "Fim do Ficheiro"
            Dim strScript As String = "<script language=JavaScript>"
            strScript = strScript + "alert(""" & strMessage & """);"
            strScript = strScript + "</script>"
            If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript", strScript)
            End If
            Exit Sub
        End If
    End Sub

    Protected Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        mSelectedProduct = Mid(Trim(Request.Form("text1")) + Space(9), 1, 9)
        Dim fPro As Integer
        Dim rc As Integer
        Dim cb = code4init()
        fPro = d4open(cb, fPCFPRO)
        Dim tag As Integer = d4tag(fPro, "PCFPRO1")
        Call d4tagSelect(fPro, tag)
        Dim fStatus As Integer = d4field(fPro, "P_STATUS")
        Dim fProduct As Integer = d4field(fPro, "P_PRODUCT")
        d4seek(fPro, mSelectedProduct)
        rc = d4skip(fPro, -1)
        While d4bof(fPro) = 0
            mStatus = f4str(fStatus)
            If mStatus = "X" Then
                rc = d4skip(fPro, -1)
            Else
                mSelectedProduct = f4str(fProduct)
                TextBox1.Text = mSelectedProduct
                rc = d4close(fPro)
                rc = code4initUndo(cb)
                xLoadFicha()
                xLoadBobs()
                Exit Sub
            End If
        End While
        If d4bof(fPro) <> 0 Then
            rc = d4top(fPro)
            mSelectedProduct = f4str(fProduct)
            TextBox1.Text = mSelectedProduct
            rc = d4close(fPro)
            rc = code4initUndo(cb)
            Dim strMessage As String
            strMessage = "Inicio do Ficheiro"
            Dim strScript As String = "<script language=JavaScript>"
            strScript = strScript + "alert(""" & strMessage & """);"
            strScript = strScript + "</script>"
            If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript", strScript)
            End If
            Exit Sub
        End If
    End Sub

    Protected Sub btnFabrica_Click(sender As Object, e As EventArgs) Handles btnFabrica.Click
        Dim strMessage As String
        strMessage = "Ainda não está implementado"
        Dim strScript As String = "<script language=JavaScript>"
        strScript = strScript + "alert(""" & strMessage & """);"
        strScript = strScript + "</script>"
        If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript")) Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript", strScript)
        End If
    End Sub

    Private Sub C1GridView1_PageIndexChanging(sender As Object, e As C1.Web.Wijmo.Controls.C1GridView.C1GridViewPageEventArgs) Handles C1GridView1.PageIndexChanging
        C1GridView1.PageIndex = e.NewPageIndex
        C1GridView1.DataBind()
    End Sub
End Class