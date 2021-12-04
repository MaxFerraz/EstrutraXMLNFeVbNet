Public Class frmConsultarSituacao
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim consultarNFe As ConsSitParamNFe = New ConsSitParamNFe() With {
            .chNFe = txtchNFe.Text,
            .licencaCnpj = txtLicencaCNPJ.Text,
            .tpAmb = txtTPamb.Text,
            .versao = txtVersao.Text
        }

        Dim resposta = NSSuite.consultarSituacaoDocumento("55", consultarNFe)
        MsgBox(resposta)
    End Sub

End Class