Public Class frmCancelarNFe
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim cancNFe As CancelarReqNFe = New CancelarReqNFe With {
            .chNFe = txtchNFe.Text,
            .nProt = txtNprot.Text,
            .tpAmb = txtTPamb.Text,
            .dhEvento = txtDHevento.Text,
            .xJust = txtXjust.Text
        }
        Dim resposta = NSSuite.cancelarDocumento("55", cancNFe)
        MsgBox(resposta)
    End Sub

End Class