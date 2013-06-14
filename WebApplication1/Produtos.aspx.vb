Imports C1.Web.Wijmo.Controls.C1ComboBox
Imports C1.Web.Wijmo.Controls.C1Menu
Imports Microsoft.JScript
Imports MVXSOCKX_SVRLib

Public Class Produtos
    Inherits System.Web.UI.Page
    Dim Sock As New MvxSockX
    Private dataTable1 As New DataTable("Produtos")
    Private dataTable2 As New DataTable("Bobines")
    Private dataTable3 As New DataTable("Fabrica")
    Private PageNum As Integer
    Private mStatus As String = ""
    Private mRefGen As String = ""
    Private rc As Integer = 0
    Private mSelectedProduct As String = ""
    Private mRef As String = ""
    Private mRow As Integer = 0
    Private ComputerName = "192.168.168.10"
    Private UserName = "dlgpor12"
    Private Password = "dlg35por$"

    Private mT1 As Double = 0
    Private mT2 As Double = 0
    Private mT3 As Double = 0
    Private mT4 As Double = 0
    Private mT5 As Double = 0
    Private mT6 As Double = 0
    Private mT7 As Double = 0
    Private mT8 As Double = 0

    Private mTotBobs As Double
    Private mTotFab As Double

    Private strMessage As String
    Private strScript As String
    Private mSelectedRow As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' Repor após teste
        ' ----------------
        'If Session("mAgentLoggedIn") = "NO" Then
        '    Dim xStr As String = "~/Default.aspx"
        '    Response.Redirect(xStr)
        '    Exit Sub
        'End If

        ' If Not IsPostBack Then
        ' Colunas da Tabela de Produtos
        dataTable1.Columns.Add("Cod")
        dataTable1.Columns.Add("Nome")
        dataTable1.Columns.Add("Quant")
        dataTable1.Columns.Add("Gen")

        dataTable2.Columns.Add("Tipo")
        dataTable2.Columns.Add("Matrícula")
        dataTable2.Columns.Add("Res")
        dataTable2.Columns.Add("Quant")

        dataTable3.Columns.Add("CAMU")
        dataTable3.Columns.Add("STQT")
        dataTable3.Columns.Add("DISP")
        dataTable3.Columns.Add("BREF")

        ' Carrega o dropdown de Referencias Genericas a partir do PCFTAB
        Dim cb = code4init()
        Dim fTab As Integer = d4open(cb, fPCFTAB)
        Dim tag As Integer = d4tag(fTab, "PCFTAB1")
        Call d4tagSelect(fTab, tag)
        Dim fTabela As Integer = d4field(fTab, "T_TABELA")
        Dim fRefGen As Integer = d4field(fTab, "T_CODE")
        rc = d4seek(fTab, "X")
        Dim mRefGen As String
        While f4str(fTabela) = "X" And d4eof(fTab) = False
            mRefGen = f4str(fRefGen)
            ' Exclui o que não são Cabos
            If InStr("0123456789", Mid(mRefGen, 1, 1)) > 0 Then
                C1ComboBox1.Items.Add(New C1ComboBoxItem(Trim(f4str(fRefGen))))
            End If
            rc = d4skip(fTab, 1)
        End While
        rc = d4close(fTab)
        rc = code4initUndo(cb)
        mRow = 0
        ' End If
    End Sub

    Function xLoadProducts()
        Dim row As DataRow
        Dim fPro As Integer
        Dim xx As Integer = 0
        Dim rc As Integer
        dataTable1.Clear()

        dataTable2.Clear()
        C1Stocks.DataSource = dataTable2
        C1Stocks.DataBind()

        dataTable3.Clear()
        C1Fabrica.DataSource = dataTable3
        C1Fabrica.DataBind()

        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""

        sSelectedProduct.Text = mSelectedProduct

        Dim mSelRefGen As String = ""
        If Len(Trim(C1ComboBox1.Text)) = 0 Then
            'C1ComboBox1.Text = "3*1.5"
            'mSelRefGen = "3*1.5"
            'mSelectedProduct = "223030185"
        Else
            mSelRefGen = Trim(C1ComboBox1.Text)
        End If

        If Len(Trim(mSelRefGen)) > 0 Then
            Dim cb = code4init()
            fPro = d4open(cb, fPCFPRO)
            Dim tag As Integer = d4tag(fPro, "PCFPRO4")
            Call d4tagSelect(fPro, tag)
            Dim fStatus As Integer = d4field(fPro, "P_STATUS")
            Dim fProduct As Integer = d4field(fPro, "P_PRODUCT")
            Dim fName As Integer = d4field(fPro, "P_NAME")
            Dim fRefGen As Integer = d4field(fPro, "P_REF_GEN")
            Dim fRef As Integer = d4field(fPro, "P_REF")
            Dim fStkIni As Integer = d4field(fPro, "P_STK_INI")
            Dim fStkIn As Integer = d4field(fPro, "P_STK_IN")
            Dim fStkOut As Integer = d4field(fPro, "P_STK_OUT")
            Dim fVen1E As Integer = d4field(fPro, "P_VEN1E")
            d4top(fPro)
            d4seek(fPro, mSelRefGen)
            xRefGen = Trim(f4str(fRefGen))
            While xRefGen = mSelRefGen And d4eof(fPro) = False
                xx = xx + 1
                mStatus = f4str(fStatus)
                If mStatus <> "X" Then
                    Dim mStkAct As Double = f4long(fStkIni) + f4long(fStkIn) - f4long(fStkOut)
                    row = dataTable1.NewRow()
                    row("Cod") = f4str(fProduct)
                    row("Nome") = f4str(fName)
                    row("Quant") = Format(mStkAct, "###,##0")
                    row("Gen") = xRefGen
                    dataTable1.Rows.Add(row)
                End If
                rc = d4skip(fPro, 1)
                xRefGen = Trim(f4str(fRefGen))
            End While
            rc = d4close(fPro)
            rc = code4initUndo(cb)
            C1Prods.Columns(0).Width = 90
            C1Prods.Columns(1).Width = 300
            C1Prods.Columns(2).Width = 90
            C1Prods.Columns(3).Width = 80
            C1Prods.Columns(1).ControlStyle.HorizontalAlign = HorizontalAlign.Right
            C1Prods.Columns(1).ControlStyle.BackColor = Drawing.Color.DarkRed
            C1Prods.DataSource = dataTable1
            C1Prods.DataBind()
        End If
        Return True
    End Function

    Function xLoadBobs()
        Dim row As DataRow
        Dim fBob As Integer
        Dim xx As Integer = 0
        Dim rc As Integer
        dataTable2.Clear()
        Dim cb = code4init()
        fBob = d4open(cb, fPCFBOB)
        Dim tag As Integer = d4tag(fBob, "PCFBOB3")
        Call d4tagSelect(fBob, tag)
        Dim fProduct As Integer = d4field(fBob, "B_PRODUCT")
        Dim fBobine As Integer = d4field(fBob, "B_BOBINE")
        Dim fType As Integer = d4field(fBob, "B_TYPE")
        Dim fRes As Integer = d4field(fBob, "B_RES")
        Dim fQtyIni As Integer = d4field(fBob, "B_QTY_INI")
        Dim fCortes As Integer = d4field(fBob, "B_CORTES")
        Dim mQty As Double = 0
        mTotBobs = 0
        rc = d4top(fBob)
        d4seek(fBob, mSelectedProduct)
        mProduct = f4str(fProduct)
        Dim mType As String
        While Trim(mProduct) = Trim(mSelectedProduct) And d4eof(fBob) = False
            mBobine = f4str(fBobine)
            If InStr(mBobine, "*") = 0 And Trim(mProduct) = Trim(mSelectedProduct) Then
                mQty = f4long(fQtyIni) - f4long(fCortes)
                If mQty <> 0 And f4str(fType) <> "P" Then
                    mReservada = f4str(fRes)
                    If Mid(mBobine, 1, 2) = "PL" Then
                        mType = "CAIXAS"
                    ElseIf Mid(mBobine, 1, 2) = "RL" Then
                        mType = "ROLO"
                    ElseIf Mid(mBobine, 1, 2) = "06" Then
                        mType = "BOB 06"
                    ElseIf Mid(mBobine, 1, 2) = "08" Then
                        mType = "BOB 08"
                    ElseIf Mid(mBobine, 1, 2) = "10" Then
                        mType = "BOB 10"
                    ElseIf Mid(mBobine, 1, 2) = "12" Then
                        mType = "BOB 12"
                    ElseIf Mid(mBobine, 1, 2) = "13" Then
                        mType = "BOB 13"
                    ElseIf Mid(mBobine, 1, 2) = "14" Then
                        mType = "BOB 14"
                    ElseIf Mid(mBobine, 1, 2) = "16" Then
                        mType = "BOB 16"
                    Else
                        mType = f4str(fType)
                    End If
                    xx = xx + 1
                    row = dataTable2.NewRow()
                    row("Tipo") = mType
                    row("Matrícula") = mBobine
                    If Len(Trim(mReservada)) = 0 Then
                        mTotBobs = mTotBobs + mQty
                        row("Res") = Space(1)
                    Else
                        row("Res") = "Reserva"
                    End If
                    row("Quant") = Format(mQty, "###,###")
                    dataTable2.Rows.Add(row)
                End If
            End If
            rc = d4skip(fBob, 1)
            mProduct = f4str(fProduct)
        End While
        If xx = 0 Then
            row = dataTable2.NewRow()
            row("Tipo") = Space(1)
            row("Matrícula") = "SEM STOCK"
            row("Res") = Space(1)
            row("Quant") = Format(0, "###,###")
            dataTable2.Rows.Add(row)
        End If
        rc = d4close(fBob)
        rc = code4initUndo(cb)
        C1Stocks.DataSource = dataTable2
        C1Stocks.DataBind()
        TextBox2.Text = Format(mTotBobs, "###,###,###")
        Return True
    End Function

    Function xGetStocksFabrica()
        Dim cb = code4init()
        fPro = d4open(cb, fPCFPRO)
        Dim tag As Integer = d4tag(fPro, "PCFPRO1")
        Call d4tagSelect(fPro, tag)
        Dim fRef As Integer = d4field(fPro, "P_REF")
        d4seek(fPro, mSelectedProduct)
        If d4eof(fPro) = False Then
            mRef = f4str(fRef)
            If Len(Trim(mRef)) = 0 Then
                strMessage = "Referência da Fábrica em branco " & mSelectedProduct
                strScript = "<script language=JavaScript>"
                strScript = strScript + "alert(""" & strMessage & """);"
                strScript = strScript + "</script>"
                If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript", strScript)
                End If
            Else
                ' OK
            End If
        Else
            strMessage = "Não encontra este produto " & mSelectedProduct
            strScript = "<script language=JavaScript>"
            strScript = strScript + "alert(""" & strMessage & """);"
            strScript = strScript + "</script>"
            If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript", strScript)
            End If
        End If
        rc = d4close(fPro)
        rc = code4initUndo(cb)

        ' Fazer bypass se for teste
        If 1 = 2 Then
            rc = Sock.MvxSockConnect(ComputerName, 6800, UserName, Password, "MMS060MI", "")
            If rc <> 0 Then
                Sock.MvxSockShowLastError("")
                dataTable3.Clear()
                C1Fabrica.DataSource = dataTable3
                C1Fabrica.DataBind()
                mTotFab = 0
                TextBox3.Text = Format(mTotFab, "###,###,###")
                Return False
            Else
                ' OK
            End If

            mT1 = 0
            mT2 = 0
            mT3 = 0
            mT4 = 0
            mT5 = 0
            mT6 = 0
            mT7 = 0
            mT8 = 0
            dataTable3.Clear()
            mTotFab = 0

            xLawsonAccess(mRef, "3")
            xLawsonAccess(mRef, "5")
            xLawsonAccess(mRef, "0")
            xLawsonAccess(mRef, "8")
            xLawsonAccess(mRef, "7")
            xLawsonAccess(mRef, "9")
            xLawsonAccess(mRef, "6")
            xLawsonAccess(mRef, "4")

            C1Fabrica.Columns(1).ControlStyle.HorizontalAlign = HorizontalAlign.Right
            C1Fabrica.Columns(1).ControlStyle.BackColor = Drawing.Color.DarkRed
            C1Fabrica.Columns(2).ControlStyle.HorizontalAlign = HorizontalAlign.Right
            C1Fabrica.DataSource = dataTable3
            C1Fabrica.DataBind()

            ' Calcula o Total de Bobines
            mTot1 = mT1 + mT2 + mT3 + mT4 + mT5 + mT6 + mT7 + mT8
            'sTotFab.Text = Format(mTot1, "###,###")
            'sTotFab.Refresh()
            TextBox3.Text = Format(mTotFab, "###,###,###")
            TextBox4.Text = Format(mTotBobs + mTotFab, "###,###,###")

            Sock.MvxSockClose()
        End If

        Return (True)
    End Function

    Function xLawsonAccess(ByVal mRef, ByVal mEmb)
        Sock.MvxSockSetField("CONO", "001")
        Articulo = Trim(mRef) + mEmb
        Sock.MvxSockSetField("ITNO", Articulo)
        Sock.Trim = False
        rc = Sock.MvxSockAccess("LstViaItem")
        If rc <> 0 Then
            ' Sock.MvxSockShowLastError("")
            ' Sock.MvxSockClose()
            Return False
        End If
        Dim mFields As Double = 0
        Dim mEmbala As String = ""
        While Sock.More
            Dim mCONO As String = Sock.MvxSockGetField("CONO")
            Dim mWHLO As String = Sock.MvxSockGetField("WHLO") ' usado
            Dim mBANO As String = Sock.MvxSockGetField("BANO")
            Dim mCAMU As String = Sock.MvxSockGetField("CAMU") ' usado
            Dim mBREF As String = Sock.MvxSockGetField("BREF") ' usado 
            Dim mSTQT As Double = Sock.MvxSockGetField("STQT") ' usado
            Dim mALQT As Double = Sock.MvxSockGetField("ALQT") ' usado
            If mWHLO = "001" Then
                Dim mDisp As Double = mSTQT - mALQT
                If mEmb = "3" Then
                    mT1 = mT1 + mDisp
                    If Mid(mBREF, 1, 2) = "06" Then
                        mEmbala = "BOB 06"
                    ElseIf Mid(mBREF, 1, 2) = "08" Then
                        mEmbala = "BOB 08"
                    ElseIf Mid(mBREF, 1, 2) = "10" Then
                        mEmbala = "BOB 10"
                    ElseIf Mid(mBREF, 1, 2) = "12" Then
                        mEmbala = "BOB 12"
                    ElseIf Mid(mBREF, 1, 2) = "13" Then
                        mEmbala = "BOB 13"
                    ElseIf Mid(mBREF, 1, 2) = "14" Then
                        mEmbala = "BOB 14"
                    ElseIf Mid(mBREF, 1, 2) = "16" Then
                        mEmbala = "BOB 16"
                    Else
                        mEmbala = "BOBINE ?"
                    End If
                ElseIf mEmb = "5" Then
                    mT2 = mT2 + mDisp
                    mEmbala = "ROL 050"
                ElseIf mEmb = "0" Then
                    mT3 = mT3 + mDisp
                    mEmbala = "ROL 100"
                ElseIf mEmb = "8" Then
                    mT4 = mT4 + mDisp
                    mEmbala = "ROL 200"
                ElseIf mEmb = "7" Then
                    mT5 = mT5 + mDisp
                    mEmbala = "CAR 0500"
                ElseIf mEmb = "9" Then
                    mT6 = mT6 + mDisp
                    mEmbala = "CAR 1000"
                ElseIf mEmb = "6" Then
                    mT7 = mT7 + mDisp
                    mEmbala = "TERRANAX"
                ElseIf mEmb = "4" Then
                    mT8 = mT8 + mDisp
                    mEmbala = "TERRANAX 25"
                Else
                    mEmbala = "??????"
                End If
                If mDisp > 0 Then
                    Dim row As DataRow
                    row = dataTable3.NewRow()
                    row("CAMU") = mEmbala ' Tipo Embalagem
                    row("STQT") = mSTQT   ' Start Quantity
                    row("DISP") = mDisp   ' Disponível
                    row("BREF") = mBREF   ' Bobine 
                    dataTable3.Rows.Add(row)
                    mTotFab = mTotFab + mDisp
                End If
            End If
            rc = Sock.MvxSockAccess("")
            mFields = mFields + 1
        End While
        Return True
    End Function

    Private Sub C1Stocks_PageIndexChanging(sender As Object, e As C1.Web.Wijmo.Controls.C1GridView.C1GridViewPageEventArgs) Handles C1Stocks.PageIndexChanging
        C1Stocks.PageIndex = e.NewPageIndex + 1
        C1Stocks.DataBind()
    End Sub

    Private Sub C1Prods_PageIndexChanging(sender As Object, e As C1.Web.Wijmo.Controls.C1GridView.C1GridViewPageEventArgs) Handles C1Prods.PageIndexChanging
        C1Prods.PageIndex = e.NewPageIndex + 1
        C1Prods.DataBind()
    End Sub

    Public Class Utilities
        Public Shared Sub CreateConfirmBox(ByRef btn As WebControls.Button, ByVal strMessage As String)
            btn.Attributes.Add("onclick", "return confirm('" & strMessage & "');")
        End Sub
    End Class

    Function xProductSortChanged()
        C1Stocks.DataSource = dataTable2
        C1Stocks.DataBind()
        Return True
    End Function

    ' Stocks Miguelez Portugal
    Protected Sub btnStocks_Click(sender As Object, e As EventArgs) Handles btnStocks.Click
        mSelectedProduct = Request.Form("text1")
        mSelectedRow = Request.Form("text3")
        sSelectedProduct.Text = mSelectedProduct

        Dim cb = code4init()
        fPro = d4open(cb, fPCFPRO)
        Dim tag As Integer = d4tag(fPro, "PCFPRO1")
        Call d4tagSelect(fPro, tag)
        Dim fName As Integer = d4field(fPro, "P_NAME")
        d4top(fPro)
        d4seek(fPro, mSelectedProduct)
        mSelectedName = f4str(fName)
        rc = d4close(fPro)
        rc = code4initUndo(cb)

        sSelectedName.Text = mSelectedName
        xLoadBobs()
        xGetStocksFabrica()
        TextBox4.Text = Format(mTotBobs + mTotFab, "###,###,###")
        C1Prods.SelectedIndex = CDbl(mSelectedRow)
    End Sub

    Private Sub C1ComboBox1_SelectedIndexChanged(sender As Object, args As C1.Web.Wijmo.Controls.C1ComboBox.C1ComboBoxEventArgs) Handles C1ComboBox1.SelectedIndexChanged
        xLoadProducts()
    End Sub
End Class