Public Class InfGTVRespCTe
    Public Property status As String
    Public Property motivo As String
    Public Property retEvento As RetEventoCTe
    Public Property erros As Erros
End Class

Public Class Erros
    Public Property codigo As String
    Public Property descricao As String
End Class