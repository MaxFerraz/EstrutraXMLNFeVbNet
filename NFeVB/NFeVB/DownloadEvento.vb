Public Class frmDownloadEvento
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim downloadEventoNFe As DownloadEventoReqNFe = New DownloadEventoReqNFe With {
        .chNFe = txtchNFe.Text,
        .tpDown = txtTPdown.Text,
        .tpAmb = txtTPamb.Text,
        .tpEvento = txtTPevento.Text,
        .nSeqEvento = txtNseqEvento.Text
    }

        Dim resposta = NSSuite.downloadEventoESalvar("55", downloadEventoNFe, "C:\Users\cleiton.fagundes\Desktop\NFeVB", txtchNFe.Text, True)
        MsgBox(resposta)
    End Sub
End Class