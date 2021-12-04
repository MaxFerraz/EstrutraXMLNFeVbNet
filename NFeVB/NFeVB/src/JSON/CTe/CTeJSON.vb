Namespace src.JSON.CTe
    Public Class Toma3
        Public Property toma As String
    End Class

    Public Class EnderToma
        Public Property xLgr As String
        Public Property nro As String
        Public Property xCpl As String
        Public Property xBairro As String
        Public Property cMun As String
        Public Property xMun As String
        Public Property CEP As String
        Public Property UF As String
        Public Property cPais As String
        Public Property xPais As String
    End Class

    Public Class Toma4
        Public Property toma As String
        Public Property CNPJ As String
        Public Property CPF As String
        Public Property IE As String
        Public Property xNome As String
        Public Property xFant As String
        Public Property fone As String
        Public Property enderToma As EnderToma
        Public Property email As String
    End Class

    Public Class Ide
        Public Property cUF As String
        Public Property cCT As String
        Public Property CFOP As String
        Public Property natOp As String
        Public Property [mod] As String
        Public Property serie As String
        Public Property nCT As String
        Public Property dhEmi As String
        Public Property tpImp As String
        Public Property tpEmis As String
        Public Property cDV As String
        Public Property tpAmb As String
        Public Property tpCTe As String
        Public Property procEmi As String
        Public Property verProc As String
        Public Property cMunEnv As String
        Public Property xMunEnv As String
        Public Property UFEnv As String
        Public Property modal As String
        Public Property tpServ As String
        Public Property cMunIni As String
        Public Property xMunIni As String
        Public Property UFIni As String
        Public Property cMunFim As String
        Public Property xMunFim As String
        Public Property UFFim As String
        Public Property retira As String
        Public Property xDetRetira As String
        Public Property indIEToma As String
        Public Property toma3 As Toma3
        Public Property toma4 As Toma4
        Public Property dhCont As String
        Public Property xJust As String
    End Class

    Public Class Pass
        Public Property xPass As String
    End Class

    Public Class Fluxo
        Public Property xOrig As String
        Public Property pass As List(Of Pass)
        Public Property xDest As String
        Public Property xRota As String
    End Class

    Public Class SemData
        Public Property tpPer As String
    End Class

    Public Class ComData
        Public Property tpPer As String
        Public Property dProg As String
    End Class

    Public Class NoPeriodo
        Public Property tpPer As String
        Public Property dIni As String
        Public Property dFim As String
    End Class

    Public Class SemHora
        Public Property tpHor As String
    End Class

    Public Class ComHora
        Public Property tpHor As String
        Public Property hProg As String
    End Class

    Public Class NoInter
        Public Property tpHor As String
        Public Property hIni As String
        Public Property hFim As String
    End Class

    Public Class Entrega
        Public Property semData As SemData
        Public Property comData As ComData
        Public Property noPeriodo As NoPeriodo
        Public Property semHora As SemHora
        Public Property comHora As ComHora
        Public Property noInter As NoInter
    End Class

    Public Class ObsCont
        Public Property xCampo As String
        Public Property xTexto As String
    End Class

    Public Class ObsFisco
        Public Property xCampo As String
        Public Property xTexto As String
    End Class

    Public Class Compl
        Public Property xCaracAd As String
        Public Property xCaracSer As String
        Public Property xEmi As String
        Public Property fluxo As Fluxo
        Public Property Entrega As Entrega
        Public Property origCalc As String
        Public Property destCalc As String
        Public Property xObs As String
        Public Property ObsCont As List(Of ObsCont)
        Public Property ObsFisco As List(Of ObsFisco)
    End Class

    Public Class EnderEmit
        Public Property xLgr As String
        Public Property nro As String
        Public Property xCpl As String
        Public Property xBairro As String
        Public Property cMun As String
        Public Property xMun As String
        Public Property CEP As String
        Public Property UF As String
        Public Property fone As String
    End Class

    Public Class Emit
        Public Property CNPJ As String
        Public Property IE As String
        Public Property IEST As String
        Public Property xNome As String
        Public Property xFant As String
        Public Property enderEmit As EnderEmit
    End Class

    Public Class EnderReme
        Public Property xLgr As String
        Public Property nro As String
        Public Property xCpl As String
        Public Property xBairro As String
        Public Property cMun As String
        Public Property xMun As String
        Public Property CEP As String
        Public Property UF As String
        Public Property cPais As String
        Public Property xPais As String
    End Class

    Public Class [Rem]
        Public Property CNPJ As String
        Public Property CPF As String
        Public Property IE As String
        Public Property xNome As String
        Public Property xFant As String
        Public Property fone As String
        Public Property enderReme As EnderReme
        Public Property email As String
    End Class

    Public Class EnderExped
        Public Property xLgr As String
        Public Property nro As String
        Public Property xCpl As String
        Public Property xBairro As String
        Public Property cMun As String
        Public Property xMun As String
        Public Property CEP As String
        Public Property UF As String
        Public Property cPais As String
        Public Property xPais As String
    End Class

    Public Class Exped
        Public Property CNPJ As String
        Public Property CPF As String
        Public Property IE As String
        Public Property xNome As String
        Public Property fone As String
        Public Property enderExped As EnderExped
        Public Property email As String
    End Class

    Public Class EnderReceb
        Public Property xLgr As String
        Public Property nro As String
        Public Property xCpl As String
        Public Property xBairro As String
        Public Property cMun As String
        Public Property xMun As String
        Public Property CEP As String
        Public Property UF As String
        Public Property cPais As String
        Public Property xPais As String
    End Class

    Public Class Receb
        Public Property CNPJ As String
        Public Property CPF As String
        Public Property IE As String
        Public Property xNome As String
        Public Property fone As String
        Public Property enderReceb As EnderReceb
        Public Property email As String
    End Class

    Public Class EnderDest
        Public Property xLgr As String
        Public Property nro As String
        Public Property xCpl As String
        Public Property xBairro As String
        Public Property cMun As String
        Public Property xMun As String
        Public Property CEP As String
        Public Property UF As String
        Public Property cPais As String
        Public Property xPais As String
    End Class

    Public Class Dest
        Public Property CNPJ As String
        Public Property CPF As String
        Public Property IE As String
        Public Property xNome As String
        Public Property fone As String
        Public Property ISUF As String
        Public Property enderDest As EnderDest
        Public Property email As String
    End Class

    Public Class Comp
        Public Property xNome As String
        Public Property vComp As String
    End Class

    Public Class VPrest
        Public Property vTPrest As String
        Public Property vRec As String
        Public Property Comp As List(Of Comp)
    End Class

    Public Class ICMS00
        Public Property CST As String
        Public Property vBC As String
        Public Property pICMS As String
        Public Property vICMS As String
    End Class

    Public Class ICMS20
        Public Property CST As String
        Public Property pRedBC As String
        Public Property vBC As String
        Public Property pICMS As String
        Public Property vICMS As String
    End Class

    Public Class ICMS45
        Public Property CST As String
    End Class

    Public Class ICMS60
        Public Property CST As String
        Public Property vBCSTRet As String
        Public Property vICMSSTRet As String
        Public Property pICMSSTRet As String
        Public Property vCred As String
    End Class

    Public Class ICMS90
        Public Property CST As String
        Public Property pRedBC As String
        Public Property vBC As String
        Public Property pICMS As String
        Public Property vICMS As String
        Public Property vCred As String
    End Class

    Public Class ICMSOutraUF
        Public Property CST As String
        Public Property pRedBCOutraUF As String
        Public Property vBCOutraUF As String
        Public Property pICMSOutraUF As String
        Public Property vICMSOutraUF As String
    End Class

    Public Class ICMSSN
        Public Property CST As String
        Public Property indSN As String
    End Class

    Public Class ICMS
        Public Property ICMS00 As ICMS00
        Public Property ICMS20 As ICMS20
        Public Property ICMS45 As ICMS45
        Public Property ICMS60 As ICMS60
        Public Property ICMS90 As ICMS90
        Public Property ICMSOutraUF As ICMSOutraUF
        Public Property ICMSSN As ICMSSN
    End Class

    Public Class ICMSUFFim
        Public Property vBCUFFim As String
        Public Property pFCPUFFim As String
        Public Property pICMSUFFim As String
        Public Property pICMSInter As String
        Public Property pICMSInterPart As String
        Public Property vFCPUFFim As String
        Public Property vICMSUFFim As String
        Public Property vICMSUFIni As String
    End Class

    Public Class Imp
        Public Property ICMS As ICMS
        Public Property vTotTrib As String
        Public Property infAdFisco As String
        Public Property ICMSUFFim As ICMSUFFim
    End Class

    Public Class InfQ
        Public Property cUnid As String
        Public Property tpMed As String
        Public Property qCarga As String
    End Class

    Public Class InfCarga
        Public Property vCarga As String
        Public Property proPred As String
        Public Property infQ As List(Of InfQ)
        Public Property vCargaAverb As String
    End Class

    Public Class LacUnidCarga
        Public Property nLacre As String
    End Class

    Public Class InfUnidCarga
        Public Property tpUnidCarga As String
        Public Property idUnidCarga As String
        Public Property lacUnidCarga As List(Of LacUnidCarga)
        Public Property qtdRat As String
    End Class

    Public Class LacUnidTransp
        Public Property nLacre As String
    End Class

    Public Class InfUnidTransp
        Public Property tpUnidTransp As String
        Public Property idUnidTransp As String
        Public Property lacUnidTransp As List(Of LacUnidTransp)
        Public Property infUnidCarga As List(Of InfUnidCarga)
        Public Property qtdRat As String
    End Class

    Public Class InfNF
        Public Property nRoma As String
        Public Property nPed As String
        Public Property [mod] As String
        Public Property serie As String
        Public Property nDoc As String
        Public Property dEmi As String
        Public Property vBC As String
        Public Property vICMS As String
        Public Property vBCST As String
        Public Property vST As String
        Public Property vProd As String
        Public Property vNF As String
        Public Property nCFOP As String
        Public Property nPeso As String
        Public Property PIN As String
        Public Property dPrev As String
        Public Property unidRat As String
        Public Property infUnidCarga As List(Of InfUnidCarga)
        Public Property infUnidTransp As List(Of InfUnidTransp)
    End Class

    Public Class InfNFe
        Public Property chave As String
        Public Property PIN As String
        Public Property dPrev As String
        Public Property unidRat As String
        Public Property infUnidCarga As List(Of InfUnidCarga)
        Public Property infUnidTransp As List(Of InfUnidTransp)
    End Class

    Public Class InfOutro
        Public Property tpDoc As String
        Public Property descOutros As String
        Public Property nDoc As String
        Public Property dEmi As String
        Public Property vDocFisc As String
        Public Property dPrev As String
        Public Property infUnidCarga As InfUnidCarga()
        Public Property infUnidTransp As InfUnidTransp()
    End Class

    Public Class InfDoc
        Public Property infNF As List(Of InfNF)
        Public Property infNFe As List(Of InfNFe)
        Public Property infOutros As List(Of InfOutro)
    End Class

    Public Class IdDocAntPap
        Public Property tpDoc As String
        Public Property serie As String
        Public Property subser As String
        Public Property nDoc As String
        Public Property dEmi As String
    End Class

    Public Class IdDocAntEle
        Public Property chCTe As String
    End Class

    Public Class IdDocAnt
        Public Property idDocAntPap As List(Of IdDocAntPap)
        Public Property idDocAntEle As List(Of IdDocAntEle)
    End Class

    Public Class EmiDocAnt
        Public Property CNPJ As String
        Public Property CPF As String
        Public Property IE As String
        Public Property UF As String
        Public Property xNome As String
        Public Property idDocAnt As List(Of IdDocAnt)
    End Class

    Public Class DocAnt
        Public Property emiDocAnt As List(Of EmiDocAnt)
    End Class

    Public Class VeicNovo
        Public Property chassi As String
        Public Property cCor As String
        Public Property xCor As String
        Public Property cMod As String
        Public Property vUnit As String
        Public Property vFrete As String
    End Class

    Public Class Fat
        Public Property nFat As String
        Public Property vOrig As String
        Public Property vDesc As String
        Public Property vLiq As String
    End Class

    Public Class Dup
        Public Property nDup As String
        Public Property dVenc As String
        Public Property vDup As String
    End Class

    Public Class Cobr
        Public Property fat As Fat
        Public Property dup As List(Of Dup)
    End Class

    Public Class RefNF
        Public Property CNPJ As String
        Public Property CPF As String
        Public Property [mod] As String
        Public Property serie As String
        Public Property subserie As String
        Public Property nro As String
        Public Property valor As String
        Public Property dEmi As String
    End Class

    Public Class TomaICMS
        Public Property refNFe As String
        Public Property refNF As RefNF
        Public Property refCte As String
    End Class

    Public Class InfCteSub
        Public Property chCte As String
        Public Property refCteAnu As String
        Public Property tomaICMS As TomaICMS
        Public Property indAlteraToma As String
    End Class

    Public Class InfGlobalizado
        Public Property xObs As String
    End Class

    Public Class InfCTeMultimodal
        Public Property chCTeMultimodal As String
    End Class

    Public Class InfServVinc
        Public Property infCTeMultimodal As List(Of InfCTeMultimodal)
    End Class

    Public Class InfCTeNorm
        Public Property infCarga As InfCarga
        Public Property infDoc As InfDoc
        Public Property docAnt As DocAnt
        Public Property veicNovos As List(Of VeicNovo)
        Public Property cobr As Cobr
        Public Property infCteSub As InfCteSub
        Public Property infGlobalizado As InfGlobalizado
        Public Property infServVinc As InfServVinc
    End Class

    Public Class InfCteComp
        Public Property chCTe As String
    End Class

    Public Class InfCteAnu
        Public Property chCTe As String
        Public Property dEmi As String
    End Class

    Public Class AutXML
        Public Property CNPJ As String
        Public Property CPF As String
    End Class

    Public Class InfCte
        Public Property versao As String
        Public Property ide As Ide
        Public Property compl As Compl
        Public Property emit As Emit
        Public Property [rem] As [Rem]
        Public Property exped As Exped
        Public Property receb As Receb
        Public Property dest As Dest
        Public Property vPrest As VPrest
        Public Property imp As Imp
        Public Property infCTeNorm As InfCTeNorm
        Public Property infCteComp As InfCteComp
        Public Property infCteAnu As InfCteAnu
        Public Property autXML As List(Of AutXML)
    End Class

    Public Class CTe
        Public Property infCte As InfCte
    End Class

    Public Class EmiOcc
        Public Property CNPJ As String
        Public Property [cInt] As String
        Public Property IE As String
        Public Property UF As String
        Public Property fone As String
    End Class

    Public Class Occ
        Public Property serie As String
        Public Property nOcc As String
        Public Property dEmi As String
        Public Property emiOcc As EmiOcc
    End Class

    Public Class Rodo
        Public Property xmlns As String
        Public Property RNTRC As String
        Public Property occ As List(Of Occ)
    End Class

    Public Class NatCarga
        Public Property xDime As String
        Public Property cInfManu As String
    End Class

    Public Class Tarifa
        Public Property CL As String
        Public Property cTar As String
        Public Property vTar As String
    End Class

    Public Class InfTotAP
        Public Property qTotProd As String
        Public Property uniAP As String
    End Class

    Public Class Peri
        Public Property nONU As String
        Public Property qTotEmb As String
        Public Property infTotAP As InfTotAP
    End Class

    Public Class Aereo
        Public Property nMinu As String
        Public Property nOCA As String
        Public Property dPrevAereo As String
        Public Property natCarga As NatCarga
        Public Property tarifa As Tarifa
        Public Property peri As List(Of Peri)
    End Class

    Public Class Balsa
        Public Property xBalsa As String
    End Class

    Public Class Lacre
        Public Property nLacre As String
    End Class

    Public Class DetCont
        Public Property nCont As String
        Public Property lacre As List(Of Lacre)
        Public Property infDoc As InfDoc
    End Class

    Public Class Aquav
        Public Property vPrest As String
        Public Property vAFRMM As String
        Public Property xNavio As String
        Public Property balsa As List(Of Balsa)
        Public Property nViag As String
        Public Property direc As String
        Public Property irin As String
        Public Property detCont As List(Of DetCont)
    End Class

    Public Class EnderFerro
        Public Property xLgr As String
        Public Property nro As String
        Public Property xCpl As String
        Public Property xBairro As String
        Public Property cMun As String
        Public Property xMun As String
        Public Property CEP As String
        Public Property UF As String
    End Class

    Public Class FerroEnv
        Public Property CNPJ As String
        Public Property [cInt] As String
        Public Property IE As String
        Public Property xNome As String
        Public Property enderFerro As EnderFerro
    End Class

    Public Class TrafMut
        Public Property respFat As String
        Public Property ferrEmi As String
        Public Property vFrete As String
        Public Property chCTeFerroOrigem As String
        Public Property ferroEnv As List(Of FerroEnv)
    End Class

    Public Class Ferrov
        Public Property tpTraf As String
        Public Property trafMut As TrafMut
        Public Property fluxo As String
    End Class

    Public Class Duto
        Public Property vTar As String
        Public Property dIni As String
        Public Property dFim As String
    End Class

    Public Class InfSeg
        Public Property xSeg As String
        Public Property CNPJ As String
    End Class

    Public Class Seg
        Public Property infSeg As InfSeg
        Public Property nApol As String
        Public Property nAver As String
    End Class

    Public Class Multimodal
        Public Property COTM As String
        Public Property indNegociavel As String
        Public Property seg As Seg
    End Class

    Public Class InfModal
        Public Property versaoModal As String
        Public Property rodo As Rodo
        Public Property aereo As Aereo
        Public Property aquav As Aquav
        Public Property ferrov As Ferrov
        Public Property duto As Duto
        Public Property multimodal As Multimodal
    End Class

    Public Class CTeJSON
        Public Property CTe As CTe
        Public Property infModal As InfModal
    End Class


End Namespace
