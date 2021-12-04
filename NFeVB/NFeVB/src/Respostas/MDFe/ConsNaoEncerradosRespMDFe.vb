Public Class InfMDFe
    Public Property chMDFe As String
    Public Property nProt As String
End Class

Public Class RetConsMDFeNaoEnc
    Public Property tpAmb As String
    Public Property cStat As String
    Public Property xMotivo As String
    Public Property cUF As String
    Public Property infMDFe As IList(Of InfMDFe)
End Class

Public Class ConsNaoEncerradosRespMDFe
    Inherits ConsNaoEncerradosResp
    Public Property retConsMDFeNaoEnc As RetConsMDFeNaoEnc
End Class