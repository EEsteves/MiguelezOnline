Imports Apollo
Imports Apollo.ApolloAPI

Module Lasernet
    Public fPCFCLI As String = "C:\PCFFILES\DATA\PCFCLI.dbf"
    Public fPCFFCT As String = "C:\PCFFILES\DATA\PCFFCT.dbf"
    Public fPCFMOV As String = "C:\PCFFILES\DATA\PCFMOV.dbf"
    Public fPCFECL As String = "C:\PCFFILES\DATA\PCFECL.dbf"
    Public fPCFICL As String = "C:\PCFFILES\DATA\PCFICL.dbf"
    Public fPCFLET As String = "C:\PCFFILES\DATA\PCFLET.dbf"
    Public fPCFREC As String = "C:\PCFFILES\DATA\PCFREC.dbf"
    Public fPCFPRO As String = "C:\PCFFILES\DATA\PCFPRO.dbf"
    Public fPCFTAB As String = "C:\PCFFILES\DATA\PCFTAB.dbf"
    Public fPCFBOB As String = "C:\PCFFILES\DATA\PCFBOB.dbf"
    Public fPCFCOR As String = "C:\PCFFILES\DATA\PCFCOR.dbf"
    Public fPCFORC As String = "C:\PCFFILES\DATA\PCFORC.dbf"
    Public fPCFITE As String = "C:\PCFFILES\DATA\PCFITE.dbf"

    Public mCliNum As String
    Public mProdNum As String
    Public mDocNum As String
    Public mEncNum As String
    Public mOrcNum As String

    'Public mAgentLoggedIn As String = "NO"
    'Public mAgentName As String = ""
    'Public mAgentOnline As String = ""

    Public mNodate As Date = #1/1/1900#.ToShortDateString
    Public mEscEcu As Double = 200.482
    Public mDec As Integer = 5
    Public mUnitAfterDiscount As Double = 0
    Public mTipodeCalculo As String = "G"
    Public mSelectedClient As String = ""
    ' Public mSelectedProduct As String = ""
    ' Public mRow As Integer = 0

    Public zFactLastYear, zPlafond, zPlafInt, zTotNaoVencido, zTotAtrazado, zDisponivel, zTotEncPendentes, zCredit, zTotGuias, zAverage
    Public xFactLastYear, xPlafond, xPlafInt, xTotNaoVencido, xTotAtrazado, xLetras1, xLetras2, xDisponivel, xTotEncPendentes, xCredit, xTotGuias, xAverage
    Public xLetrasTotal As String

    Public zPlafond2, zPlafInt2, zTotNaoVencido2, zTotAtrazado2, zDisponivel2, zCredit2
    Public xPlafond2, xPlafInt2, xTotNaoVencido2, xTotAtrazado2, xDisponivel2, xCredit2
    Public xStatus
    ' ----------------------------------------------------------------------
    Public nFactCorr, nFact3060, nFact6090, nFactOver, nFactTotal As Double
    Public nPreDatCorr, nPreDat3060, nPreDat6090, nPreDatOver, nPreDatTotal As Double
    Public nLetras1, nLetras2 As Double
    Public mTot2, mTot3, mTot4, mTot5, mTotT As Double
    ' ----------------------------------------------------------------------
    Public xFactCorr, xFact3060, xFact6090, xFactOver, xFactTotal As String
    Public xPreDatCorr, xPreDat3060, xPreDat6090, xPreDatOver, xPreDatTotal As String
    Public xTot2, xTot3, xTot4, xTot5, xTotT As String

    Public Ano1(11), Ano2(11), Ano3(11) As Double

    Public Function xCalcItem(ByVal mxQuant As Double, ByVal mxUnit As Double, ByVal mxDesct1 As Double, ByVal mxDesct2 As Double, ByVal mxDesct3 As Double, ByVal mxDesct4 As Double, ByVal mxIvaTp As String, ByVal mxTaxaIva As Double, ByVal mxMoeda As String, ByVal mxCambio As Double)
        ' mxTaxaIva = cdbl(mxTaxaIva)

        'If Trim(mxQuant) = "" Then mxQuant = 0
        'If Trim(mxUnit) = "" Then mxUnit = 0
        'If Trim(mxDesct1) = "" Then mxDesct1 = 0
        'If Trim(mxDesct2) = "" Then mxDesct2 = 0
        'If Trim(mxDesct3) = "" Then mxDesct3 = 0
        'If Trim(mxDesct4) = "" Then mxDesct4 = 0

        Dim mValComIva As Double = 0

        ' Se a Moeda não está a ser passada
        If Trim(mxMoeda) = "" Then
            mxMoeda = "EUR"
            mxCambio = 1
        End If

        ' Calculo do IVA
        If mxIvaTp = "-" Then
            mxUnit = mxUnit * 100 / (100 + mxTaxaIva)
        End If

        ' Faz os calculos na moeda original
        If mTipodeCalculo = "G" Then
            ' ------------------------------------------------------------------------
            ' Calcula o Gross (Quant x Unit) e depois aplica os descontos a este valor
            ' ------------------------------------------------------------------------
            mGross = Math.Round(mxQuant * mxUnit, mDec)
            mLiq = mGross
            If mLiq <> 0 Then
                If mxDesct1 <> 0 Then
                    mLiq = Math.Round((mLiq * (1 - mxDesct1 / 100)), mDec)
                    If mxDesct2 <> 0 Then
                        mLiq = Math.Round((mLiq * (1 - mxDesct2 / 100)), mDec)
                        If mxDesct3 <> 0 Then
                            mLiq = Math.Round((mLiq * (1 - mxDesct3 / 100)), mDec)
                            If mxDesct4 <> 0 Then
                                mLiq = Math.Round((mLiq * (1 - mxDesct4 / 100)), mDec)
                            End If
                        End If
                    End If
                End If
                mDesctAmt = mGross - mLiq
                mIvaAmt = Math.Round((mLiq * mxTaxaIva) / 100, mDec)
                mValComIva = mLiq + mIvaAmt
            Else
                mDesctAmt = 0
                mIvaAmt = 0
                mValComIva = 0
            End If
            If mLiq <> 0 And mxQuant <> 0 Then
                mUnitAfterDiscount = Math.Round(mLiq / mxQuant, mDec)
            Else
                mUnitAfterDiscount = mxUnit
                If mxDesct1 <> 0 Then
                    mUnitAfterDiscount = Math.Round((mUnitAfterDiscount * (1 - mxDesct1 / 100)), mDec)
                    If mxDesct2 <> 0 Then
                        mUnitAfterDiscount = Math.Round((mUnitAfterDiscount * (1 - mxDesct2 / 100)), mDec)
                        If mxDesct3 <> 0 Then
                            mUnitAfterDiscount = Math.Round((mUnitAfterDiscount * (1 - mxDesct3 / 100)), mDec)
                            If mxDesct4 <> 0 Then
                                mUnitAfterDiscount = Math.Round((mUnitAfterDiscount * (1 - mxDesct4 / 100)), mDec)
                            End If
                        End If
                    End If
                End If
            End If
        ElseIf mTipodeCalculo = "U" Then
            ' ------------------------------------------------------------
            ' Aplica os descontos directamente ao Preço Unitário
            ' No final multiplica o preço resultante pela Quantidade
            ' ------------------------------------------------------------
            mGross = Math.Round(mxQuant * mxUnit, mDec)
            mUnitAfterDiscount = mxUnit
            If mxDesct1 <> 0 Then
                mUnitAfterDiscount = Math.Round(mUnitAfterDiscount * (1 - (mxDesct1 / 100)), mDec)
                If mxDesct2 <> 0 Then
                    mUnitAfterDiscount = Math.Round(mUnitAfterDiscount * (1 - (mxDesct2 / 100)), mDec)
                    If mxDesct3 <> 0 Then
                        mUnitAfterDiscount = Math.Round(mUnitAfterDiscount * (1 - (mxDesct3 / 100)), mDec)
                        If mxDesct4 <> 0 Then
                            mUnitAfterDiscount = Math.Round(mUnitAfterDiscount * (1 - (mxDesct4 / 100)), mDec)
                        End If
                    End If
                End If
            End If
            mLiq = Math.Round(mxQuant * mUnitAfterDiscount, mDec)
            mDesctAmt = mGross - mLiq
            mIvaAmt = Math.Round((mLiq * mxTaxaIva) / 100, mDec)
            mValComIva = mLiq + mIvaAmt
        End If
        ' Daqui sai:
        ' mUnitAfterDiscount, mGross, mLiq, mDesctAmt, mIvaAmt, mValComIva
        ' Tudo na moeda original

        If mxMoeda = "ESC" Then
            mxUnit = mxUnit / mEscEcu
        ElseIf mxMoeda = "PTS" Then
            mxUnit = mxUnit / mEscEcu
        Else
            mxUnit = mxUnit / mxCambio
        End If

        Return mValComIva
    End Function

    Public Function DTOS(ByVal zdate)
        If Mid(zdate, 4, 2) < "01" Or Mid(zdate, 4, 2) > "12" Then
            ' MsgBox("Erro na função DTOS. Zdate = " & zdate, MsgBoxStyle.Critical)
            Return (Space(8))
        ElseIf Mid(zdate, 7, 4) < "1900" Then
            MsgBox("A função DTOS do L2000 não suporta datas inferiores a 1900. Zdate = " & zdate, MsgBoxStyle.Critical)
            Return (Space(8))
        Else
            Return (Mid(zdate, 7, 4) & Mid(zdate, 4, 2) & Mid(zdate, 1, 2))
        End If
    End Function

    'Public Function XUSE(ByVal w_fileid, ByVal w_useind)
    '    Dim w_alias As String = Mid(w_fileid, 18, 6)
    '    Dim rc As Integer
    '    sx_SetCentury(True)     ' yyyy
    '    sx_SetDateFormat(2)     ' dd/mm/yyyy
    '    Select Case w_useind
    '        Case "SHARE"
    '            rc = sx_Use(w_fileid, w_alias, 1, 2) ' Read only
    '        Case "EXCLUSIVE"
    '            rc = sx_Use(w_fileid, w_alias, 2, 2) ' Exclusive
    '        Case "RW"
    '            rc = sx_Use(w_fileid, w_alias, 0, 2) ' ReadWrite
    '    End Select
    '    sx_SetTranslate(1)
    '    If rc = 0 Then
    '        MsgBox("Erro na abertura do ficheiro ")
    '    End If
    '    Return rc
    'End Function

End Module
