Namespace src.JSON.BPe
    Public Class Ide
        Public Property cUF As String
        Public Property tpAmb As String
        Public Property [mod] As String
        Public Property serie As String
        Public Property nBP As String
        Public Property cBP As String
        Public Property cDV As String
        Public Property modal As String
        Public Property dhEmi As String
        Public Property tpEmis As String
        Public Property verProc As String
        Public Property tpBPe As String
        Public Property indPres As String
        Public Property UFIni As String
        Public Property cMunIni As String
        Public Property UFFim As String
        Public Property cMunFim As String
        Public Property chCont As String
        Public Property xJust As String
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
        Public Property email As String
    End Class

    Public Class Emit
        Public Property CNPJ As String
        Public Property IE As String
        Public Property IEST As String
        Public Property xNome As String
        Public Property xFant As String
        Public Property IM As String
        Public Property CNAE As String
        Public Property CRT As String
        Public Property enderEmit As EnderEmit
        Public Property TAR As String
    End Class

    Public Class EnderComp
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
        Public Property fone As String
        Public Property email As String
    End Class

    Public Class Comp
        Public Property xNome As String
        Public Property CNPJ As String
        Public Property CPF As String
        Public Property idEstrangeiro As String
        Public Property IE As String
        Public Property tpComp As String
        Public Property vComp As String
        Public Property enderComp As EnderComp
    End Class

    Public Class EnderAgencia
        Public Property xLgr As String
        Public Property nro As String
        Public Property xCpl As String
        Public Property xBairro As String
        Public Property cMun As String
        Public Property xMun As String
        Public Property CEP As String
        Public Property UF As String
        Public Property fone As String
        Public Property email As String
    End Class

    Public Class Agencia
        Public Property xNome As String
        Public Property CNPJ As String
        Public Property enderAgencia As EnderAgencia
    End Class

    Public Class InfBPeSub
        Public Property chBPe As String
        Public Property tpSub As String
    End Class

    Public Class InfPassageiro
        Public Property xNome As String
        Public Property CNPJ As String
        Public Property CPF As String
        Public Property tpDoc As String
        Public Property nDoc As String
        Public Property dNasc As String
        Public Property fone As String
        Public Property email As String
    End Class

    Public Class InfPassagem
        Public Property cLocOrig As String
        Public Property xLocOrig As String
        Public Property cLocDest As String
        Public Property xLocDest As String
        Public Property dhEmb As String
        Public Property dhValidade As String
        Public Property infPassageiro As InfPassageiro
    End Class

    Public Class IdfTravessia
        Public Property tpVeiculo As String
        Public Property sitVeiculo As String
    End Class

    Public Class InfViagem
        Public Property cPercurso As String
        Public Property xPercurso As String
        Public Property tpViagem As String
        Public Property tpServ As String
        Public Property tpAcomodacao As String
        Public Property tpTrecho As String
        Public Property dhViagem As String
        Public Property dhConexao As String
        Public Property prefixo As String
        Public Property poltrona As String
        Public Property plataforma As String
        Public Property idfTravessia As IdfTravessia
    End Class

    Public Class CompPag
        Public Property tpComp As String
        Public Property vComp As String
    End Class

    Public Class InfValorBPe
        Public Property vBP As String
        Public Property vDesconto As String
        Public Property vPgto As String
        Public Property vTroco As String
        Public Property tpDesconto As String
        Public Property xDesconto As String
        Public Property Comp As List(Of CompPag)
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
        Public Property qRedBCOutraUF As String
        Public Property vBCOutraUF As String
        Public Property pICMSOutraUF As String
        Public Property vICMSOutraUF As String
    End Class

    Public Class ICMSSN
        Public Property CST As String
        Public Property indSN As String
        Public Property vTotTrib As String
        Public Property infAdFisco As String
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

    Public Class ICMS
        Public Property ICMS00 As ICMS00
        Public Property ICMS20 As ICMS20
        Public Property ICMS45 As ICMS45
        Public Property ICMS90 As ICMS90
        Public Property ICMSOutraUF As ICMSOutraUF
        Public Property ICMSSN As ICMSSN
        Public Property ICMSUFFim As ICMSUFFim
    End Class

    Public Class Imp
        Public Property ICMS As ICMS
    End Class

    Public Class Card
        Public Property tpIntegra As String
        Public Property CNPJ As String
        Public Property tBand As String
        Public Property cAut As String
    End Class

    Public Class Pag
        Public Property tPag As String
        Public Property vPag As String
        Public Property card As Card
    End Class

    Public Class AutXML
        Public Property CNPJ As String
        Public Property CPF As String
    End Class

    Public Class InfAdic
        Public Property infAdFisco As String
        Public Property infCpl As String
    End Class

    Public Class InfBPe
        Public Property versao As String
        Public Property ide As Ide
        Public Property emit As Emit
        Public Property comp As Comp
        Public Property agencia As Agencia
        Public Property infBPeSub As InfBPeSub
        Public Property infPassagem As InfPassagem
        Public Property infViagem As List(Of InfViagem)
        Public Property infValorBPe As InfValorBPe
        Public Property imp As Imp
        Public Property pag As List(Of Pag)
        Public Property autXML As List(Of AutXML)
        Public Property infAdic As InfAdic
    End Class

    Public Class BPe
        Public Property infBPe As InfBPe
    End Class

    Public Class BPeJSON
        Public Property BPe As BPe
    End Class
End Namespace


