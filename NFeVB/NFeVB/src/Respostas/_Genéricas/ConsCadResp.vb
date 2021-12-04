Public Class ConsCadResp

    Public Property status As String
    Public Property motivo As String
    Public Property RetConsCad As RetConsCad
    'Public Property IE As String
    'Public Property CNPJ As String
    'Public Property UF As String
    'Public Property xNome As String
    'Public Property xFant As String
    'Public Property xLgr As String
    'Public Property nro As String
    'Public Property xCpl As String
    'Public Property cMun As String
    'Public Property xMun As String

End Class

Public Class RetConsCad
    Public Property infCons As InfCons
    Public Property versao As String
End Class

Public Class InfCons
    Public Property verAplic As String
    Public Property cStat As Integer
    Public Property xMotivo As String
    Public Property UF As String
    Public Property CNPJ As String
    Public Property dhCons As String
    Public Property cUF As Integer
    Public Property infCad As List(Of InfCad)
End Class

Public Class InfCad
    Public Property IE As String
    Public Property CNPJ As String
    Public Property CPF As String
    Public Property UF As String
    Public Property cSit As Integer
    Public Property indCredNFe As Integer
    Public Property indCredCTe As Integer
    Public Property xNome As String
    Public Property xFant As String
    Public Property xRegApur As String
    Public Property CNAE As Integer
    Public Property dIniAtiv As String
    Public Property dUltSit As String
    Public Property dBaixa As String
    Public Property IEUnica As String
    Public Property IEAtual As String
    Public Property ender As Ender
End Class

Public Class Ender
    Public Property xLgr As String
    Public Property nro As String
    Public Property xCpl As String
    Public Property xBairro As String
    Public Property cMun As String
    Public Property xMun As String
    Public Property CEP As String
End Class
