Namespace src.JSON.NFe
    Public Class RefNF
        Public Property cUF As String
        Public Property AAMM As String
        Public Property CNPJ As String
        Public Property [mod] As String
        Public Property serie As String
        Public Property nNF As String
    End Class

    Public Class RefNFP
        Public Property cUF As String
        Public Property AAMM As String
        Public Property CNPJ As String
        Public Property CPF As String
        Public Property IE As String
        Public Property [mod] As String
        Public Property serie As String
        Public Property nNF As String
        Public Property refCTe As String
    End Class

    Public Class RefECF
        Public Property [mod] As String
        Public Property nECF As String
        Public Property nCOO As String
    End Class

    Public Class NFref
        Public Property refNFe As String
        Public Property refNF As RefNF
        Public Property refNFP As RefNFP
        Public Property refECF As RefECF
    End Class

    Public Class Ide
        Public Property cUF As String
        Public Property cNF As String
        Public Property natOp As String
        Public Property [mod] As String
        Public Property serie As String
        Public Property nNF As String
        Public Property dhEmi As String
        Public Property dhSaiEnt As String
        Public Property tpNF As String
        Public Property idDest As String
        Public Property cMunFG As String
        Public Property tpImp As String
        Public Property tpEmis As String
        Public Property cDV As String
        Public Property tpAmb As String
        Public Property finNFe As String
        Public Property indFinal As String
        Public Property indPres As String
        Public Property indIntermed as String
        Public Property procEmi As String
        Public Property verProc As String
        Public Property dhCont As String
        Public Property xJust As String
        Public Property NFref As List(Of NFref)
    End Class

    Public Class EnderEmit
        Public Property xLgr As String
        Public Property nro As String
        Public Property xCpl As String
        Public Property xBairro As String
        Public Property cMun As String
        Public Property xMun As String
        Public Property UF As String
        Public Property CEP As String
        Public Property cPais As String
        Public Property xPais As String
        Public Property fone As String
    End Class

    Public Class Emit
        Public Property CNPJ As String
        Public Property CPF As String
        Public Property xNome As String
        Public Property xFant As String
        Public Property enderEmit As EnderEmit
        Public Property IE As String
        Public Property IEST As String
        Public Property IM As String
        Public Property CNAE As String
        Public Property CRT As String
    End Class

    Public Class EnderDest
        Public Property xLgr As String
        Public Property nro As String
        Public Property xCpl As String
        Public Property xBairro As String
        Public Property cMun As String
        Public Property xMun As String
        Public Property UF As String
        Public Property CEP As String
        Public Property cPais As String
        Public Property xPais As String
        Public Property fone As String
    End Class

    Public Class Dest
        Public Property CNPJ As String
        Public Property CPF As String
        Public Property idEstrangeiro As String
        Public Property xNome As String
        Public Property enderDest As EnderDest
        Public Property indIEDest As String
        Public Property IE As String
        Public Property ISUF As String
        Public Property IM As String
        Public Property email As String
    End Class

    Public Class Retirada
        Public Property CNPJ As String
        Public Property CPF As String
        Public Property xLgr As String
        Public Property nro As String
        Public Property xCpl As String
        Public Property xBairro As String
        Public Property cMun As String
        Public Property xMun As String
        Public Property UF As String
    End Class

    Public Class Entrega
        Public Property CNPJ As String
        Public Property CPF As String
        Public Property xLgr As String
        Public Property nro As String
        Public Property xCpl As String
        Public Property xBairro As String
        Public Property cMun As String
        Public Property xMun As String
        Public Property UF As String
    End Class

    Public Class AutXML
        Public Property CNPJ As String
        Public Property CPF As String
    End Class

    Public Class Rastro
        Public Property nLote As String
        Public Property qLote As String
        Public Property dFab As String
        Public Property dVal As String
        Public Property cAgreg As String
    End Class

    Public Class Adi
        Public Property nAdicao As String
        Public Property nSeqAdic As String
        Public Property cFabricante As String
        Public Property vDescDI As String
        Public Property nDraw As String
    End Class

    Public Class DI
        Public Property nDI As String
        Public Property dDI As String
        Public Property xLocDesemb As String
        Public Property UFDesemb As String
        Public Property dDesemb As String
        Public Property tpViaTransp As String
        Public Property vAFRMM As String
        Public Property tpIntermedio As String
        Public Property CNPJ As String
        Public Property UFTerceiro As String
        Public Property cExportador As String
        Public Property adi As Adi
    End Class

    Public Class ExportInd
        Public Property nRE As String
        Public Property chNFe As String
        Public Property qExport As String
    End Class

    Public Class DetExport
        Public Property nDraw As String
        Public Property exportInd As ExportInd
    End Class

    Public Class VeicProd
        Public Property tpOp As String
        Public Property chassi As String
        Public Property cCor As String
        Public Property xCor As String
        Public Property pot As String
        Public Property cilin As String
        Public Property pesoL As String
        Public Property pesoB As String
        Public Property nSerie As String
        Public Property tpComb As String
        Public Property nMotor As String
        Public Property CMT As String
        Public Property dist As String
        Public Property anoMod As String
        Public Property anoFab As String
        Public Property tpPint As String
        Public Property tpVeic As String
        Public Property espVeic As String
        Public Property VIN As String
        Public Property condVeic As String
        Public Property cMod As String
        Public Property cCorDENATRAN As String
        Public Property lota As String
        Public Property tpRest As String
    End Class

    Public Class Med
        Public Property cProdANVISA As String
        Public Property vPMC As String
    End Class

    Public Class Arma
        Public Property tpArma As String
        Public Property nSerie As String
        Public Property nCano As String
        Public Property descr As String
    End Class

    Public Class CIDE
        Public Property qBCProd As String
        Public Property vAliqProd As String
        Public Property vCIDE As String
    End Class

    Public Class Encerrante
        Public Property nBico As String
        Public Property nBomba As String
        Public Property nTanque As String
        Public Property vEncIni As String
        Public Property vEncFin As String
    End Class

    Public Class Comb
        Public Property cProdANP As String
        Public Property descANP As String
        Public Property pGLP As String
        Public Property pGNn As String
        Public Property pGNi As String
        Public Property vPart As String
        Public Property CODIF As String
        Public Property qTemp As String
        Public Property UFCons As String
        Public Property CIDE As CIDE
        Public Property encerrante As Encerrante
    End Class

    Public Class Prod
        Public Property cProd As String
        Public Property cEAN As String
        Public Property xProd As String
        Public Property NCM As String
        Public Property NVE As String
        Public Property CEST As String
        Public Property indEscala As String
        Public Property CNPJFab As String
        Public Property cBenef As String
        Public Property EXTIPI As String
        Public Property CFOP As String
        Public Property uCom As String
        Public Property qCom As String
        Public Property vUnCom As String
        Public Property vProd As String
        Public Property cEANTrib As String
        Public Property uTrib As String
        Public Property qTrib As String
        Public Property vUnTrib As String
        Public Property vFrete As String
        Public Property vSeg As String
        Public Property vDesc As String
        Public Property vOutro As String
        Public Property indTot As String
        Public Property rastro As List(Of Rastro)
        Public Property DI As List(Of DI)
        Public Property detExport As List(Of DetExport)
        Public Property xPed As String
        Public Property nItemPed As String
        Public Property nFCI As String
        Public Property veicProd As VeicProd
        Public Property med As Med
        Public Property arma As List(Of Arma)
        Public Property comb As Comb
        Public Property nRECOPI As String
    End Class

    Public Class ICMS00
        Public Property orig As String
        Public Property CST As String
        Public Property modBC As String
        Public Property vBC As String
        Public Property pICMS As String
        Public Property vICMS As String
        Public Property pFCP As String
        Public Property vFCP As String
    End Class

    Public Class ICMS10
        Public Property orig As String
        Public Property CST As String
        Public Property modBC As String
        Public Property vBC As String
        Public Property pICMS As String
        Public Property vICMS As String
        Public Property vBCFCP As String
        Public Property pFCP As String
        Public Property vFCP As String
        Public Property modBCST As String
        Public Property pMVAST As String
        Public Property pRedBCST As String
        Public Property vBCST As String
        Public Property pICMSST As String
        Public Property vICMSST As String
        Public Property vBCFCPST As String
        Public Property pFCPST As String
        Public Property vFCPST As String
    End Class

    Public Class ICMS20
        Public Property orig As String
        Public Property CST As String
        Public Property modBC As String
        Public Property pRedBC As String
        Public Property vBC As String
        Public Property pICMS As String
        Public Property vICMS As String
        Public Property vBCFCP As String
        Public Property pFCP As String
        Public Property vFCP As String
        Public Property vICMSDeson As String
        Public Property motDesICMS As String
    End Class

    Public Class ICMS30
        Public Property orig As String
        Public Property CST As String
        Public Property modBCST As String
        Public Property pMVAST As String
        Public Property pRedBCST As String
        Public Property vBCST As String
        Public Property pICMSST As String
        Public Property vICMSST As String
        Public Property vBCFCPST As String
        Public Property pFCPST As String
        Public Property vFCPST As String
        Public Property vICMSDeson As String
        Public Property motDesICMS As String
    End Class

    Public Class ICMS40
        Public Property orig As String
        Public Property CST As String
        Public Property vICMSDeson As String
        Public Property motDesICMS As String
    End Class

    Public Class ICMS51
        Public Property orig As String
        Public Property CST As String
        Public Property modBC As String
        Public Property pRedBC As String
        Public Property vBC As String
        Public Property pICMS As String
        Public Property vICMSOp As String
        Public Property pDif As String
        Public Property vICMSDif As String
        Public Property vICMS As String
        Public Property vBCFCP As String
        Public Property pFCP As String
        Public Property vFCP As String
    End Class

    Public Class ICMS60
        Public Property orig As String
        Public Property CST As String
        Public Property vBCSTRet As String
        Public Property pST As String
        Public Property vICMSSTRet As String
        Public Property vICMSSubstituto As String
        Public Property vBCFCPSTRet As String
        Public Property pFCPSTRet As String
        Public Property vFCPSTRet As String
    End Class

    Public Class ICMS70
        Public Property orig As String
        Public Property CST As String
        Public Property modBC As String
        Public Property pRedBC As String
        Public Property vBC As String
        Public Property pICMS As String
        Public Property vICMS As String
        Public Property vBCFCP As String
        Public Property pFCP As String
        Public Property vFCP As String
        Public Property modBCST As String
        Public Property pMVAST As String
        Public Property pRedBCST As String
        Public Property vBCST As String
        Public Property pICMSST As String
        Public Property vICMSST As String
        Public Property vBCFCPST As String
        Public Property pFCPST As String
        Public Property vFCPST As String
        Public Property vICMSDeson As String
        Public Property motDesICMS As String
    End Class

    Public Class ICMS90
        Public Property orig As String
        Public Property CST As String
        Public Property modBC As String
        Public Property vBC As String
        Public Property pRedBC As String
        Public Property pICMS As String
        Public Property vICMS As String
        Public Property vBCFCP As String
        Public Property pFCP As String
        Public Property vFCP As String
        Public Property modBCST As String
        Public Property pMVAST As String
        Public Property pRedBCST As String
        Public Property vBCST As String
        Public Property pICMSST As String
        Public Property vICMSST As String
        Public Property vBCFCPST As String
        Public Property pFCPST As String
        Public Property vFCPST As String
        Public Property vICMSDeson As String
        Public Property motDesICMS As String
    End Class

    Public Class ICMSPart
        Public Property orig As String
        Public Property CST As String
        Public Property modBC As String
        Public Property vBC As String
        Public Property pRedBC As String
        Public Property pICMS As String
        Public Property vICMS As String
        Public Property modBCST As String
        Public Property pMVAST As String
        Public Property pRedBCST As String
        Public Property vBCST As String
        Public Property pICMSST As String
        Public Property vICMSST As String
        Public Property pBCOp As String
        Public Property UFST As String
    End Class

    Public Class ICMSST
        Public Property orig As String
        Public Property CST As String
        Public Property vBCSTRet As String
        Public Property vICMSSTRet As String
        Public Property vBCSTDest As String
        Public Property vICMSSTDest As String
    End Class

    Public Class ICMSSN101
        Public Property orig As String
        Public Property CSOSN As String
        Public Property pCredSN As String
        Public Property vCredICMSSN As String
    End Class

    Public Class ICMSSN102
        Public Property orig As String
        Public Property CSOSN As String
    End Class

    Public Class ICMSSN201
        Public Property orig As String
        Public Property CSOSN As String
        Public Property modBCST As String
        Public Property pMVAST As String
        Public Property pRedBCST As String
        Public Property vBCST As String
        Public Property pICMSST As String
        Public Property vICMSST As String
        Public Property vBCFCPST As String
        Public Property pFCPST As String
        Public Property vFCPST As String
        Public Property pCredSN As String
        Public Property vCredICMSSN As String
    End Class

    Public Class ICMSSN202
        Public Property orig As String
        Public Property CSOSN As String
        Public Property modBCST As String
        Public Property pMVAST As String
        Public Property pRedBCST As String
        Public Property vBCST As String
        Public Property pICMSST As String
        Public Property vICMSST As String
        Public Property vBCFCPST As String
        Public Property pFCPST As String
        Public Property vFCPST As String
    End Class

    Public Class ICMSSN500
        Public Property orig As String
        Public Property CSOSN As String
        Public Property vBCSTRet As String
        Public Property pST As String
        Public Property vICMSSTRet As String
        Public Property vBCFCPSTRet As String
        Public Property pFCPSTRet As String
        Public Property vFCPSTRet As String
    End Class

    Public Class ICMSSN900
        Public Property orig As String
        Public Property CSOSN As String
        Public Property modBC As String
        Public Property vBC As String
        Public Property pRedBC As String
        Public Property pICMS As String
        Public Property vICMS As String
        Public Property modBCST As String
        Public Property pMVAST As String
        Public Property pRedBCST As String
        Public Property vBCST As String
        Public Property pICMSST As String
        Public Property vICMSST As String
        Public Property vBCFCPST As String
        Public Property pFCPST As String
        Public Property vFCPST As String
        Public Property pCredSN As String
        Public Property vCredICMSSN As String
    End Class

    Public Class ICMSUFDest
        Public Property vBCUFDest As String
        Public Property vBCFCPUFDest As String
        Public Property pFCPUFDest As String
        Public Property pICMSUFDest As String
        Public Property pICMSInter As String
        Public Property pICMSInterPart As String
        Public Property vFCPUFDest As String
        Public Property vICMSUFDest As String
        Public Property vICMSUFRemet As String
    End Class

    Public Class ICMS
        Public Property ICMS00 As ICMS00
        Public Property ICMS10 As ICMS10
        Public Property ICMS20 As ICMS20
        Public Property ICMS30 As ICMS30
        Public Property ICMS40 As ICMS40
        Public Property ICMS51 As ICMS51
        Public Property ICMS60 As ICMS60
        Public Property ICMS70 As ICMS70
        Public Property ICMS90 As ICMS90
        Public Property ICMSPart As ICMSPart
        Public Property ICMSST As ICMSST
        Public Property ICMSSN101 As ICMSSN101
        Public Property ICMSSN102 As ICMSSN102
        Public Property ICMSSN201 As ICMSSN201
        Public Property ICMSSN202 As ICMSSN202
        Public Property ICMSSN500 As ICMSSN500
        Public Property ICMSSN900 As ICMSSN900
    End Class

    Public Class IPITrib
        Public Property CST As String
        Public Property vBC As String
        Public Property pIPI As String
        Public Property qUnid As String
        Public Property vUnid As String
        Public Property vIPI As String
    End Class

    Public Class IPINT
        Public Property CST As String
    End Class

    Public Class IPI
        Public Property clEnq As String
        Public Property CNPJProd As String
        Public Property cSelo As String
        Public Property qSelo As String
        Public Property cEnq As String
        Public Property IPITrib As IPITrib
        Public Property IPINT As IPINT
        Public Property vIPIDevol As String
    End Class

    Public Class II
        Public Property vBC As String
        Public Property vDespAdu As String
        Public Property vII As String
        Public Property vIOF As String
    End Class

    Public Class PISAliq
        Public Property CST As String
        Public Property vBC As String
        Public Property pPIS As String
        Public Property vPIS As String
    End Class

    Public Class PISQtde
        Public Property CST As String
        Public Property qBCProd As String
        Public Property vAliqProd As String
        Public Property vPIS As String
    End Class

    Public Class PISNT
        Public Property CST As String
    End Class

    Public Class PISOutr
        Public Property CST As String
        Public Property vBC As String
        Public Property pPIS As String
        Public Property qBCProd As String
        Public Property vAliqProd As String
        Public Property vPIS As String
    End Class

    Public Class PIS
        Public Property PISAliq As PISAliq
        Public Property PISQtde As PISQtde
        Public Property PISNT As PISNT
        Public Property PISOutr As PISOutr
    End Class

    Public Class PISST
        Public Property vBC As String
        Public Property pPIS As String
        Public Property qBCProd As String
        Public Property vAliqProd As String
        Public Property vPIS As String
    End Class

    Public Class COFINSAliq
        Public Property CST As String
        Public Property vBC As String
        Public Property pCOFINS As String
        Public Property vCOFINS As String
    End Class

    Public Class COFINSQtde
        Public Property CST As String
        Public Property qBCProd As String
        Public Property vAliqProd As String
        Public Property vCOFINS As String
    End Class

    Public Class COFINSNT
        Public Property CST As String
    End Class

    Public Class COFINSOutr
        Public Property CST As String
        Public Property vBC As String
        Public Property pCOFINS As String
        Public Property qBCProd As String
        Public Property vAliqProd As String
        Public Property vCOFINS As String
    End Class

    Public Class COFINS
        Public Property COFINSAliq As COFINSAliq
        Public Property COFINSQtde As COFINSQtde
        Public Property COFINSNT As COFINSNT
        Public Property COFINSOutr As COFINSOutr
    End Class

    Public Class COFINSST
        Public Property vBC As String
        Public Property pCOFINS As String
        Public Property qBCProd As String
        Public Property vAliqProd As String
        Public Property vCOFINS As String
    End Class

    Public Class ISSQN
        Public Property vBC As String
        Public Property vAliq As String
        Public Property vISSQN As String
        Public Property cMunFG As String
        Public Property cListServ As String
        Public Property vDeducao As String
        Public Property vOutro As String
        Public Property vDescIncond As String
        Public Property vDescCond As String
        Public Property vISSRet As String
        Public Property indISS As String
        Public Property cServico As String
        Public Property cMun As String
        Public Property cPais As String
        Public Property nProcesso As String
        Public Property indIncentivo As String
    End Class

    Public Class Imposto
        Public Property vTotTrib As String
        Public Property ICMS As ICMS
        Public Property ICMSUFDest As ICMSUFDest
        Public Property IPI As IPI
        Public Property II As II
        Public Property PIS As PIS
        Public Property PISST As PISST
        Public Property COFINS As COFINS
        Public Property COFINSST As COFINSST
        Public Property ISSQN As ISSQN
    End Class
    
    Public Class IPIDevol
        Public Property vIPIDevol As String  
    End Class
    
    Public Class ImpostoDevol
        Public Property pDevol As String
        Public Property IPI As IPIDevol
    End Class

    Public Class Det
        Public Property prod As Prod
        Public Property imposto As Imposto
        Public Property impostoDevol As ImpostoDevol
        Public Property infAdProd As String
        Public Property nItem As String
    End Class

    Public Class ICMSTot
        Public Property vBC As String
        Public Property vICMS As String
        Public Property vICMSDeson As String
        Public Property vFCP As String
        Public Property vFCPUFDest As String
        Public Property vICMSUFDest As String
        Public Property vICMSUFRemet As String
        Public Property vBCST As String
        Public Property vST As String
        Public Property vFCPST As String
        Public Property vFCPSTRet As String
        Public Property vProd As String
        Public Property vFrete As String
        Public Property vSeg As String
        Public Property vDesc As String
        Public Property vII As String
        Public Property vIPI As String
        Public Property vIPIDevol As String
        Public Property vPIS As String
        Public Property vCOFINS As String
        Public Property vOutro As String
        Public Property vNF As String
        Public Property vTotTrib As String
    End Class

    Public Class ISSQNtot
        Public Property vServ As String
        Public Property vBC As String
        Public Property vISS As String
        Public Property vPIS As String
        Public Property vCOFINS As String
        Public Property dCompet As String
        Public Property vDeducao As String
        Public Property vOutro As String
        Public Property vDescIncond As String
        Public Property vDescCond As String
        Public Property vISSRet As String
        Public Property cRegTrib As String
    End Class

    Public Class RetTrib
        Public Property vRetPIS As String
        Public Property vRetCOFINS As String
        Public Property vRetCSLL As String
        Public Property vBCIRRF As String
        Public Property vIRRF As String
        Public Property vBCRetPrev As String
        Public Property vRetPrev As String
    End Class

    Public Class Total
        Public Property ICMSTot As ICMSTot
        Public Property ISSQNtot As ISSQNtot
        Public Property retTrib As RetTrib
    End Class

    Public Class Transporta
        Public Property CNPJ As String
        Public Property CPF As String
        Public Property xNome As String
        Public Property IE As String
        Public Property xEnder As String
        Public Property xMun As String
        Public Property UF As String
    End Class

    Public Class RetTransp
        Public Property vServ As String
        Public Property vBCRet As String
        Public Property pICMSRet As String
        Public Property vICMSRet As String
        Public Property CFOP As String
        Public Property cMunFG As String
    End Class

    Public Class VeicTransp
        Public Property placa As String
        Public Property UF As String
        Public Property RNTC As String
    End Class

    Public Class Reboque
        Public Property placa As String
        Public Property UF As String
        Public Property RNTC As String
    End Class

    Public Class Lacre
        Public Property nLacre As String
    End Class

    Public Class Vol
        Public Property qVol As String
        Public Property esp As String
        Public Property marca As String
        Public Property nVol As String
        Public Property pesoL As String
        Public Property pesoB As String
        Public Property lacres As List(Of Lacre)
    End Class

    Public Class Transp
        Public Property modFrete As String
        Public Property transporta As Transporta
        Public Property retTransp As RetTransp
        Public Property veicTransp As VeicTransp
        Public Property reboque As List(Of Reboque)
        Public Property vagao As String
        Public Property balsa As String
        Public Property vol As List(Of Vol)
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

    Public Class Card
        Public Property CNPJ As String
        Public Property tBand As String
        Public Property cAut As String
        Public Property tpIntegra As String
    End Class

    Public Class DetPag
        Public Property indPag As String
        Public Property tPag As String
        Public Property vPag As String
        Public Property card As Card
    End Class

    Public Class Pag
        Public Property detPag As List(Of DetPag)
        Public Property vTroco As String
    End Class

    Public Class InfRespTec
        Public Property CNPJ
        Public Property xContato
        Public Property email
        Public Property fone
        Public Property idCSRT
        Public Property hashCSRT
    End Class

    Public Class InfIntermed
        Public Property CNPJ As String
        Public Property idCadIntTrand As String
    End Class

    Public Class ObsCont
        Public Property xCampo As String
        Public Property xTexto As String
    End Class

    Public Class ObsFisco
        Public Property xCampo As String
        Public Property xTexto As String
    End Class

    Public Class ProcRef
        Public Property nProc As String
        Public Property indProc As String
    End Class

    Public Class InfAdic
        Public Property infAdFisco As String
        Public Property infCpl As String
        Public Property obsCont As List(Of ObsCont)
        Public Property obsFisco As List(Of ObsFisco)
        Public Property procRef As List(Of ProcRef)
    End Class

    Public Class Exporta
        Public Property UFSaidaPais As String
        Public Property xLocExporta As String
        Public Property xLocDespacho As String
    End Class

    Public Class Compra
        Public Property xNEmp As String
        Public Property xPed As String
        Public Property xCont As String
    End Class

    Public Class ForDia
        Public Property dia As String
        Public Property qtde As String
        Public Property qTotMes As String
        Public Property qTotAnt As String
        Public Property qTotGer As String
    End Class

    Public Class Deduc
        Public Property xDed As String
        Public Property vDed As String
        Public Property vFor As String
        Public Property vTotDed As String
        Public Property vLiqFor As String
    End Class

    Public Class Cana
        Public Property safra As String
        Public Property ref As String
        Public Property forDia As List(Of ForDia)
        Public Property deduc As List(Of Deduc)
    End Class

    Public Class InfNFe
        Public Property versao As String
        Public Property Id As String
        Public Property ide As Ide
        Public Property emit As Emit
        Public Property dest As Dest
        Public Property retirada As Retirada
        Public Property entrega As Entrega
        Public Property autXML As List(Of AutXML)
        Public Property det As List(Of Det)
        Public Property total As Total
        Public Property transp As Transp
        Public Property cobr As Cobr
        Public Property pag As Pag
        Public Property infRespTec As InfRespTec
        Public Property infIntermed as InfIntermed
        Public Property infAdic As InfAdic
        Public Property exporta As Exporta
        Public Property compra As Compra
        Public Property cana As Cana
    End Class

    Public Class NFe
        Public Property infNFe As InfNFe
    End Class

    Public Class NFeJSON
        Public Property NFe As NFe
    End Class


End Namespace
