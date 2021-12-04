Public Class EncerrarResp
    Public Property status As String
    Public Property motivo As String
    Public Property retEvento As RetEvento
    Public Property erros As IList(Of String)
    Public Property erro As Erro
End Class