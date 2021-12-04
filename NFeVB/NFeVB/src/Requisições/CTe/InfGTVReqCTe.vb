Public Class InfGTVReqCTe
    Public Property chCTe As String
    Public Property tpAmb As String
    Public Property dhEvento As String
    Public Property nSeqEvento As String
    Public Property infGTV As IList(Of InfGTV)
End Class

Public Class InfGTV
    Public Property nDoc As String
    Public Property id As String
    Public Property serie As String
    Public Property subserie As String
    Public Property dEmi As String
    Public Property nDV As String
    Public Property qCarga As String
    Public Property placa As String
    Public Property UF As String
    Public Property RNTRC As String
    Public Property infEspecie As IList(Of InfEspecie)
    Public Property [rem] As [Rem]
    Public Property dest As Dest
End Class

Public Class InfEspecie
    Public Property tpEspecie As String
    Public Property vEspecie As String
End Class

Public Class [Rem]
    Public Property CNPJ As String
    Public Property CPF As String
    Public Property IE As String
    Public Property UF As String
    Public Property xNome As String
End Class

Public Class Dest
    Public Property CNPJ As String
    Public Property CPF As String
    Public Property IE As String
    Public Property UF As String
    Public Property xNome As String
End Class