Public Class frmInutilizaNumNFe
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim inutilizarNFe As InutilizarReqNFe = New InutilizarReqNFe() With {
        .nNFIni = txtNnfINI.Text,
        .nNFFin = txtNnfFIN.Text,
        .cUF = txtcUF.Text,
        .ano = txtAno.Text,
        .tpAmb = txtTPamb.Text,
        .CNPJ = txtCnpj.Text,
        .serie = txtSerie.Text,
        .xJust = txtxJust.Text
    }

        Dim resposta = NSSuite.inutilizarNumeracao("55", inutilizarNFe)
        MsgBox(resposta)
    End Sub
End Class