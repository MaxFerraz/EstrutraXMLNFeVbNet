Public Class IncluirDFeReq
    Public Property chMDFe As String
    Public Property tpAmb As String
    Public Property dhEvento As String
    Public Property nSeqEvento As String
    Public Property nProt As String
    Public Property xMun As String
    Public Property cMun As String
    Public Property infDocs As IList(Of InfDocs)
End Class
Public Class InfDocs
    Public Property chNFe As String
    Public Property xMun As String
    Public Property cMun As String
End Class