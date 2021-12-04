Public Class frmCartaCorrecao
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles button1.Click
        Dim cartaCorrecNFe As CorrigirReqNFe = New CorrigirReqNFe With {
        .chNFe = txtchNFe.Text,
        .xCorrecao = txtXcorrecao.Text,
        .tpAmb = txtTPamb.Text,
        .dhEvento = txtDHevento.Text,
        .nSeqEvento = txtNseqEvento.Text
    }

        Dim resposta = NSSuite.corrigirDocumento("55", cartaCorrecNFe)
        MsgBox(resposta)
    End Sub
End Class