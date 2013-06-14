Imports Microsoft.JScript

Public Class ClientDetail
    Inherits System.Web.UI.Page
    Private mStatus As String = ""
    Private mVend As String = ""
    Private ii As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mSelectedClient = Request.QueryString("field1")
        If mSelectedClient = Nothing Then
            mSelectedClient = "006     "
            TextBox1.Text = mSelectedClient
        End If
        xShowClientData(mSelectedClient)
    End Sub

    Function xShowClientData(mSelectedClient)
        Dim fCli As Integer
        Dim rc As Integer
        Dim cb = code4init()
        fCli = d4open(cb, fPCFCLI)
        Dim tag As Integer = d4tag(fCli, "PCFCLI1")
        Call d4tagSelect(fCli, tag)
        Dim fName As Integer = d4field(fCli, "C_NAME")
        Dim fAddr1 As Integer = d4field(fCli, "C_ADDR_1")
        Dim fAddr2 As Integer = d4field(fCli, "C_ADDR_2")
        Dim fCity As Integer = d4field(fCli, "C_CITY")
        Dim fPostal As Integer = d4field(fCli, "C_POSTAL")
        Dim fPtlNm As Integer = d4field(fCli, "C_PTL_NM")
        Dim fCountry As Integer = d4field(fCli, "C_COUNTRY")
        Dim fSIN As Integer = d4field(fCli, "C_SIN")
        Dim fPhone1 As Integer = d4field(fCli, "C_PHONE1")
        Dim fPhone2 As Integer = d4field(fCli, "C_PHONE2")
        Dim fPhone3 As Integer = d4field(fCli, "C_PHONE3")
        Dim fPlafond2 As Integer = d4field(fCli, "C_PLAFOND2")
        Dim fPlafIn2 As Integer = d4field(fCli, "C_PLAF_IN2")
        Dim fVend As Integer = d4field(fCli, "C_VEND")
        Dim fContact As Integer = d4field(fCli, "C_CONTACT")
        Dim fStatus As Integer = d4field(fCli, "C_STATUS")
        Dim fClient As Integer = d4field(fCli, "C_CLIENT")
        If d4seek(fCli, mSelectedClient) = 0 Then
            TextBox1.Text = mSelectedClient
            Label2.Text = f4str(fName)
            Label3.Text = f4str(fAddr1)
            Label4.Text = f4str(fAddr2)
            Label5.Text = f4str(fCity)
            Label6.Text = Trim(f4str(fPostal)) + " " + Trim(f4str(fPtlNm))
            Label7.Text = f4str(fSIN)
            Label8.Text = f4str(fCountry)
            Label9.Text = f4str(fPhone1)
            Label10.Text = f4str(fPhone2)
            Label11.Text = f4str(fPhone3)
            Label12.Text = f4str(fVend)

            'Dim fDate As Date
            'Dim fNm As String

            '' fNm = Me.Page.Form.ClientID
            'fNm = My.Application.Info.DirectoryPath
            '' MsgBox(fNm)

            'fDate = IO.File.GetLastWriteTime(fNm)
            ''MsgBox(CStr(fDate))
            'Label13.Text = CStr(fDate)

            sPlafond.Text = Format(f4double(fPlafond2), "###,##0.00")
            sPlafInt.Text = Format(f4double(fPlafIn2), "###,##0.00")
            sPlafTot.Text = Format(f4double(fPlafond2) + f4double(fPlafIn2), "###,##0.00")
            zPlafond2 = f4double(fPlafond2)
            zPlafInt2 = f4double(fPlafIn2)
        Else
            ' Erro: Cliente not found
        End If
        rc = d4close(fCli)
        rc = code4initUndo(cb)

        xComprasUltAno(mSelectedClient)

        'xCalcLetras(mSelectedClient)
        'xReceivables(mSelectedClient)
        'xInvoiced(mSelectedClient)
        'xGuiasPendentes(mSelectedClient)
        'xPendingOrders(mSelectedClient)
        'xCalcTotais()

        'sSaldoCorr.Text = Format(zTotNaoVencido2, "###,##0.00")
        'sSaldoAtrz.Text = Format(zTotAtrazado2, "###,##0.00")
        'sGuiasPorFact.Text = Format(zTotGuias, "###,##0.00")
        'sLetras.Text = Format(nLetras2, "###,##0.00")
        'sCredDisp.Text = xDisponivel2

        Return True
    End Function

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
        Dim fVend As Integer = d4field(fCli, "C_VEND")
        d4seek(fCli, mSelectedClient)
        rc = d4skip(fCli, 1)
        While d4eof(fCli) = 0
            mStatus = f4str(fStatus)
            mVend = f4str(fVend)
            If (mStatus = "X") Or (Trim(Session("mAgentOnline")) <> "99" And mVend <> Session("mAgentOnline")) Then
                rc = d4skip(fCli, 1)
            Else
                mSelectedClient = f4str(fClient)
                TextBox1.Text = mSelectedClient
                mCliNum = mSelectedClient
                rc = d4close(fCli)
                rc = code4initUndo(cb)
                xShowClientData(mSelectedClient)
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
        Dim fVend As Integer = d4field(fCli, "C_VEND")
        d4seek(fCli, mSelectedClient)
        rc = d4skip(fCli, -1)
        While d4bof(fCli) = 0
            mStatus = f4str(fStatus)
            mVend = f4str(fVend)
            If (mStatus = "X") Or (Trim(Session("mAgentOnline")) <> "99" And mVend <> Session("mAgentOnline")) Then
                rc = d4skip(fCli, -1)
            Else
                mSelectedClient = f4str(fClient)
                TextBox1.Text = mSelectedClient
                mCliNum = mSelectedClient
                rc = d4close(fCli)
                rc = code4initUndo(cb)
                xShowClientData(mSelectedClient)
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
            Dim strMessage As String
            strMessage = "Inicío do Ficheiro"
            Dim strScript As String = "<script language=JavaScript>"
            strScript = strScript + "alert(""" & strMessage & """);"
            strScript = strScript + "</script>"
            If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript", strScript)
            End If
            Exit Sub
        End If
    End Sub

    ' Total de Compras com IVA nos ultimos 365 dias
    ' ---------------------------------------------
    Public Function xComprasUltAno(mSelectedClient)
        Dim W_INI As String = Format(DateAdd(DateInterval.Year, -1, Today), "yyyyMMdd")
        zFactLastYear = 0
        Dim mType As String = ""
        Dim mStatus As String = ""
        Dim mTotliq As Double = 0
        Dim fFct As Integer
        Dim rc As Integer
        Dim cb = code4init()
        fFct = d4open(cb, fPCFFCT)
        Dim tag As Integer = d4tag(fFct, "PCFFCT2")
        Call d4tagSelect(fFct, tag)
        Dim fClient As Integer = d4field(fFct, "F_CLIENT")
        Dim fType As Integer = d4field(fFct, "F_TYPE")
        Dim fStatus As Integer = d4field(fFct, "F_STATUS")
        Dim fTotliq As Integer = d4field(fFct, "F_TOTLIQ")
        rc = d4seekNext(fFct, mSelectedClient + W_INI)
        Dim mClient As String = f4str(fClient)
        Dim rc1 As Integer = d4eof(fFct)
        While mClient = mSelectedClient And d4eof(fFct) = 0
            mClient = f4str(fClient)
            mType = f4str(fType)
            mStatus = f4str(fStatus)
            mTotliq = f4double(fTotliq)
            If mStatus = "A" Or mStatus = "D" Or InStr("FCD", mType) = 0 Then
                ' NÃO SÃO PROCESSADAS
            Else
                If InStr("CX", mType) > 0 Then
                    mTotliq = mTotliq * -1
                End If
                zFactLastYear = zFactLastYear + mTotliq
            End If
            rc = d4skip(fFct, 1)
        End While
        rc = d4close(fFct)
        rc = code4initUndo(cb)
        sUltAno.Text = Format(zFactLastYear, "##,###,##0.00")
    End Function

    Function xReceivables(mSelectedClient)
        Dim zValue As Double = 0
        Dim zDate As Date = Today
        Dim nPreDatCorr As Double = 0.0
        Dim nPreDat3060 As Double = 0.0
        Dim nPreDat6090 As Double = 0.0
        Dim nPreDatOver As Double = 0.0
        Dim nPreDatTotal As Double = 0.0
        'Dim mRec As Integer = XUSE(fPCFREC, "SHARE")
        'sx_SetOrder(sx_TagArea("PCFREC1"))
        'sx_Seek(mSelectedClient)
        'While Trim(sx_GetString("R_CLIENT")) = mSelectedClient And sx_Eof() = 0
        '    ' só deixa passar os P's (pre datados) 
        '    If sx_GetString("R_TYPE") = "P" Then
        '        zValue = sx_GetDouble("R_VALUE")
        '        zDate = sx_GetDateString("R_VENC_DT")
        '        Dim mGap As Integer = DateDiff(DateInterval.Day, Today, zDate)
        '        If mGap >= 0 Then
        '            If mGap < 31 Then
        '                nPreDatCorr = nPreDatCorr + zValue
        '            ElseIf mGap < 61 Then
        '                nPreDat3060 = nPreDat3060 + zValue
        '            ElseIf mGap < 91 Then
        '                nPreDat6090 = nPreDat6090 + zValue
        '            Else
        '                nPreDatOver = nPreDatOver + zValue
        '            End If
        '            nPreDatTotal = nPreDatTotal + zValue
        '        End If
        '    End If
        '    sx_Skip(+1)
        'End While
        'sx_Close()
        xPreDatCorr = Format(nPreDatCorr, "###,###.##")
        xPreDat3060 = Format(nPreDat3060, "###,###.##")
        xPreDat6090 = Format(nPreDat6090, "###,###.##")
        xPreDatOver = Format(nPreDatOver, "###,###.##")
        xPreDatTotal = Format(nPreDatTotal, "###,###.##")
    End Function

    Public Function xCalcLetras(mSelectedClient)
        nLetras1 = 0
        nLetras2 = 0
        'Dim mLet As Integer = XUSE(fPCFLET, "SHARE")
        'sx_SetOrder(sx_TagArea("PCFLET3"))
        'sx_Seek(mSelectedClient)
        'While (Trim(sx_GetString("L_CLIENT")) = mSelectedClient) And (Not sx_Eof())
        '    Dim zValor As Double = sx_GetDouble("L_VALUE")
        '    Dim zLetDt As String = sx_GetDateString("L_ENTRY_DT")
        '    If zLetDt <= DateSerial(2012, 3, 31) Then
        '        If InStr("LRVXB", sx_GetString("L_TYPE")) > 0 Then
        '            nLetras1 = nLetras1 + zValor
        '        ElseIf InStr("DAY", sx_GetString("L_TYPE")) > 0 Then
        '            nLetras1 = nLetras1 - zValor
        '        End If
        '    Else
        '        If InStr("LRVXB", sx_GetString("L_TYPE")) > 0 Then
        '            nLetras2 = nLetras2 + zValor
        '        ElseIf InStr("DAY", sx_GetString("L_TYPE")) > 0 Then
        '            nLetras2 = nLetras2 - zValor
        '        End If
        '    End If
        '    sx_Skip(+1)
        'End While
        'sx_Close()
        xLetras1 = Format(nLetras1, "##,###,##0.00")
        xLetras2 = Format(nLetras2, "##,###,##0.00")
        xLetrasTotal = Format(nLetras1 + nLetras2, "##,###,##0.00")
    End Function

    ' Total Facturado é com IVA !!!
    ' -----------------------------
    Public Function xInvoiced(mSelectedClient)
        Dim mInvDt, mRecvdDt As Date
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
        'Dim mFct As Integer = XUSE(fPCFFCT, "SHARE")
        'sx_SetOrder(sx_TagArea("PCFFCT2"))
        'sx_Seek(mSelectedClient)
        'While Trim(sx_GetString("F_CLIENT")) = mSelectedClient And sx_Eof() = 0
        '    Dim w_select As String = "s"
        '    Dim zType As String = sx_GetString("F_TYPE")
        '    Dim zStatus As String = sx_GetString("F_STATUS")
        '    If zStatus = "A" Or zStatus = "D" Or InStr("FCD", zType) = 0 Then
        '        w_select = "n"
        '    End If
        '    If w_select = "s" Then
        '        Dim zTotLiq As Double = sx_GetDouble("F_TOTLIQ")
        '        Dim mRecvd As Double = sx_GetDouble("F_RECVD")
        '        If sx_GetString("F_MOEDA") = "ESC" Then
        '            zTotLiq = zTotLiq / 200.482
        '            mRecvd = mRecvd / 200.482
        '        End If
        '        Dim x1 = sx_GetString("F_INV_DT")
        '        Dim x2 = sx_GetString("F_RECVD_DT")
        '        mInvDt = IIf(Trim(sx_GetString("F_INV_DT")) = "", mNodate, sx_GetDateString("F_INV_DT"))
        '        mRecvdDt = IIf(Trim(sx_GetString("F_RECVD_DT")) = "", mNodate, sx_GetDateString("F_RECVD_DT"))
        '        Dim mDaysPP As Integer = sx_GetDouble("F_DAYS_PP")
        '        If InStr("CX", zType) > 0 Then
        '            zTotLiq = zTotLiq * -1
        '        End If

        '        ' Atenção: está com IVA
        '        ' ---------------------
        '        Dim mMM As Integer = DatePart(DateInterval.Month, mInvDt) - 1
        '        If DatePart(DateInterval.Year, mInvDt) = DatePart(DateInterval.Year, Today()) Then
        '            Ano3(mMM) = Ano3(mMM) + zTotLiq
        '        ElseIf DatePart(DateInterval.Year, mInvDt) = DatePart(DateInterval.Year, Today()) - 1 Then
        '            Ano2(mMM) = Ano2(mMM) + zTotLiq
        '        ElseIf DatePart(DateInterval.Year, mInvDt) = DatePart(DateInterval.Year, Today()) - 2 Then
        '            Ano1(mMM) = Ano1(mMM) + zTotLiq
        '        End If

        '        Dim mValor As Double = zTotLiq - mRecvd
        '        Dim mGap As Integer = 0
        '        If zType = "F" And zStatus = "C" Then

        '            'MsgBox(xNodate)
        '            'MsgBox(mInvDt)
        '            'MsgBox(mRecvdDt)

        '            If mInvDt <> mNodate And mRecvdDt <> mNodate Then
        '                mGap = DateDiff(DateInterval.Day, mInvDt, mRecvdDt)
        '            Else
        '                mGap = 0
        '            End If
        '            mGapTotal = mGapTotal + mGap
        '            mNumeroFacturas = mNumeroFacturas + 1
        '        End If
        '        If zType = "F" And InStr("X", zStatus) > 0 Then
        '            If mRecvdDt = mNodate Then
        '                mGap = DateDiff(DateInterval.Day, mInvDt, Today)
        '            Else
        '                mGap = DateDiff(DateInterval.Day, mInvDt, mRecvdDt)
        '            End If
        '            mGapTotal = mGapTotal + mGap
        '            mNumeroFacturas = mNumeroFacturas + 1
        '        End If
        '        Dim w_process As String = "s"
        '        If mInvDt > Today Or InStr("AC", zStatus) > 0 Then
        '            w_process = "n"
        '        End If
        '        If w_process = "s" Then
        '            If Today > DateAdd(DateInterval.Day, mDaysPP, mInvDt) Then
        '                If mInvDt <= DateSerial(2012, 3, 31) Then
        '                    zTotAtrazado = zTotAtrazado + mValor
        '                Else
        '                    zTotAtrazado2 = zTotAtrazado2 + mValor
        '                End If
        '            Else
        '                If mInvDt <= DateSerial(2012, 3, 31) Then
        '                    zTotNaoVencido = zTotNaoVencido + mValor
        '                Else
        '                    zTotNaoVencido2 = zTotNaoVencido2 + mValor
        '                End If
        '            End If
        '            If DateDiff(DateInterval.Day, mInvDt, Today) < 31 Then
        '                nFactCorr = nFactCorr + mValor
        '            ElseIf DateDiff(DateInterval.Day, mInvDt, Today) < 61 Then
        '                nFact3060 = nFact3060 + mValor
        '            ElseIf DateDiff(DateInterval.Day, mInvDt, Today) < 91 Then
        '                nFact6090 = nFact6090 + mValor
        '            Else
        '                nFactOver = nFactOver + mValor
        '            End If
        '            nFactTotal = nFactTotal + mValor
        '        End If
        '    End If
        '    sx_Skip(+1)
        'End While
        'sx_Close()
        Dim xAverage As String = ""
        If mNumeroFacturas = 0 Or mGapTotal = 0 Then
            xAverage = "??"
        Else
            xAverage = LTrim(CStr(Int(mGapTotal / mNumeroFacturas))) & " dias"
        End If
        xTotNaoVencido = Format(zTotNaoVencido, "###,##0.00")
        xTotNaoVencido2 = Format(zTotNaoVencido2, "###,##0.00")
        xTotAtrazado = Format(zTotAtrazado, "###,##0.00")
        xTotAtrazado2 = Format(zTotAtrazado2, "###,##0.00")

        xFactCorr = Format(nFactCorr, "###,###.##")
        xFact3060 = Format(nFact3060, "###,###.##")
        xFact6090 = Format(nFact6090, "###,###.##")
        xFactOver = Format(nFactOver, "###,###.##")
        xFactTotal = Format(nFactTotal, "###,###.##")
    End Function

    ' Guias Pendentes é com IVA
    ' -------------------------
    Function xGuiasPendentes(mSelectedClient)
        zTotGuias = 0.0
        'Dim mFct As Integer = XUSE(fPCFFCT, "SHARE")
        'sx_SetOrder(sx_TagArea("PCFFCT2"))
        'sx_Seek(mSelectedClient)
        'Dim d = sx_GetString("F_CLIENT")
        'Dim e = sx_Eof()
        'While Trim(sx_GetString("F_CLIENT")) = mSelectedClient And sx_Eof() = 0
        '    Dim zType As String = sx_GetString("F_TYPE")
        '    If zType = "G" Then
        '        Dim zStatus As String = sx_GetString("F_STATUS")
        '        Dim zTotLiq As Double = sx_GetDouble("F_TOTLIQ")
        '        Dim w_processa As String = "n"
        '        If zStatus = "B" Then
        '            w_processa = "s"
        '        End If
        '        If w_processa = "s" Then
        '            zTotGuias = zTotGuias + zTotLiq
        '        End If
        '    End If
        '    sx_Skip(+1)
        'End While
        'sx_Close()
        xTotGuias = Format(zTotGuias, "###,##0.00")
    End Function

    ' Encomendas Pendentes é com IVA
    ' ------------------------------
    Public Function xPendingOrders(mSelectedClient)
        zTotEncPendentes = 0
        'Dim mIcl As Integer = XUSE(fPCFICL, "SHARE")
        'sx_SetOrder(sx_TagArea("PCFICL4"))
        'Dim mEcl As Integer = XUSE(fPCFECL, "SHARE")
        'sx_SetOrder(sx_TagArea("PCFECL1"))
        'sx_Select(mIcl)
        'sx_Seek("A" + mSelectedClient)
        'While sx_GetString("I_STATUS") = "A" And Trim(sx_GetString("I_CLIENT")) = mSelectedClient And (sx_Eof() = 0)
        '    Dim W_NUMBER As String = sx_GetString("I_NUMBER")
        '    Dim mEncAutorizada As String = "S"
        '    If mEncAutorizada = "S" Then
        '        Dim zFalta As Double = sx_GetDouble("I_QUANT") - sx_GetDouble("I_ENTREGUE")
        '        Dim zUnit As Double = sx_GetDouble("I_UNIT")
        '        Dim zDesct1 As Double = sx_GetDouble("I_DESCT")
        '        Dim zDesct2 As Double = sx_GetDouble("I_DESCT2")
        '        Dim zDesct3 As Double = sx_GetDouble("I_DESCT3")
        '        Dim zDesct4 As Double = sx_GetDouble("I_DESCT4")
        '        Dim zIvaTp As String = sx_GetString("I_IVA_TP")
        '        Dim zTaxaIva As Double = sx_GetDouble("I_IVA")
        '        Dim zMoeda As String = sx_GetString("I_MOEDA")
        '        Dim zCambio As Double = sx_GetDouble("I_CAMBIO")
        '        Dim mValComIva As Double = 0
        '        xCalcItem(zFalta, zUnit, zDesct1, zDesct2, zDesct3, zDesct4, zIvaTp, zTaxaIva, zMoeda, zCambio)
        '        ' Atenção: com IVA
        '        zTotEncPendentes = zTotEncPendentes + mValComIva
        '    End If
        '    sx_Skip(+1)
        'End While
        'sx_CloseAll()
        xTotEncPendentes = Format(zTotEncPendentes, "###,##0.00")
    End Function

    Public Function xCalcTotais()
        zDisponivel = (zPlafond + zPlafInt) - (zTotNaoVencido + zTotAtrazado + nLetras1 + nPreDatTotal)
        xDisponivel = Format(zDisponivel, "###,##0.00")

        zDisponivel2 = (zPlafond2 + zPlafInt2) - (zTotNaoVencido2 + zTotAtrazado2 + zTotGuias + nLetras2 + nPreDatTotal)
        xDisponivel2 = Format(zDisponivel2, "###,##0.00")

        mTot2 = nFactCorr + nPreDatCorr
        mTot3 = nFact3060 + nPreDat3060
        mTot4 = nFact6090 + nPreDat6090
        mTot5 = nFactOver + nPreDatOver
        mTotT = mTot2 + mTot3 + mTot4 + mTot5 + nLetras1
        xTot2 = Format(mTot2, "###,##0.00")
        xTot3 = Format(mTot3, "###,##0.00")
        xTot4 = Format(mTot4, "###,##0.00")
        xTot5 = Format(mTot5, "###,##0.00")
        xTotT = Format(mTotT, "###,##0.00")
    End Function

    Protected Sub PrintFicha_Click(sender As Object, e As EventArgs) Handles PrintFicha.Click
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

