Public Class CorrigirReqCTe
    Inherits CorrigirReq

    Public Property chCTe As String
    Public Property infCorrecao As InfCorrecao
End Class

Public Class InfCorrecao
    Public Property grupoAlterado As String
    Public Property campoAlterado As String
    Public Property valorAlterado As String
    Public Property nroItemAlterado As String
End Class