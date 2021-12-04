Imports System.IO

Public Class Genericos
    Public Shared Sub salvarXML(ByVal xml As String, ByVal caminho As String, ByVal nome As String, ByVal Optional tpEvento As String = "", ByVal Optional nSeqEvento As String = "")
        Dim localParaSalvar As String = caminho & tpEvento & nome & nSeqEvento & ".xml"
        Dim ConteudoSalvar As String = ""
        ConteudoSalvar = xml.Replace("\""", "")
        File.WriteAllText(localParaSalvar, ConteudoSalvar)
    End Sub

    Public Shared Sub salvarJSON(ByVal json As String, ByVal caminho As String, ByVal nome As String, ByVal Optional tpEvento As String = "", ByVal Optional nSeqEvento As String = "")
        Dim localParaSalvar As String = caminho & tpEvento & nome & nSeqEvento & ".json"
        File.WriteAllText(localParaSalvar, json)
    End Sub

    Public Shared Sub salvarPDF(ByVal pdf As String, ByVal caminho As String, ByVal nome As String, ByVal Optional tpEvento As String = "", ByVal Optional nSeqEvento As String = "")
        Dim localParaSalvar As String = caminho & tpEvento & nome & nSeqEvento & ".pdf"
        Dim bytes As Byte() = Convert.FromBase64String(pdf)
        If File.Exists(localParaSalvar) Then File.Delete(localParaSalvar)
        Dim stream As FileStream = New FileStream(localParaSalvar, FileMode.CreateNew)
        Dim writer As BinaryWriter = New BinaryWriter(stream)
        writer.Write(bytes, 0, bytes.Length)
        writer.Close()
    End Sub

    Public Shared Sub gravarLinhaLog(ByVal modelo As String, ByVal conteudo As String)
        Dim caminho As String = Application.StartupPath & "\log\"

        If Not Directory.Exists(caminho) Then
            Directory.CreateDirectory(caminho)
        End If

        Dim data As String = DateTime.Now.ToShortDateString()
        Dim hora As String = DateTime.Now.ToShortTimeString()
        Dim nomeArq As String = data.Replace("/", "") + "_" + modelo

        Using outputFile As StreamWriter = New StreamWriter(caminho & nomeArq & ".txt", True)
            outputFile.WriteLine(data & " " & hora & " - " & conteudo)
        End Using
    End Sub

    Public Shared Function GerarDigitoVerificador(ByVal chave As String)

        Dim soma As Integer = 0
        Dim restoDivisao As Integer = -1
        Dim digitoVerificador As Integer = -1
        Dim pesoMultiplicacao As Integer = 2
        Dim i As Integer = chave.Length - 1
        Dim ch As Long

        While i <> -1

            ch = Convert.ToInt32(chave(i).ToString)

            soma += ch * pesoMultiplicacao

            If pesoMultiplicacao < 9 Then
                pesoMultiplicacao += 1
            Else
                pesoMultiplicacao = 2
            End If

            i = i - 1

        End While

        restoDivisao = soma Mod 11

        If restoDivisao = 0 Or restoDivisao = 1 Then
            digitoVerificador = 0
        Else
            digitoVerificador = 11 - restoDivisao
        End If

        Return digitoVerificador
    End Function


    Public Shared Function gerarCodigoCNF()

        Dim min As Integer = 10000000
        Dim max As Integer = 99999999
        Dim p As Random = New Random()

        Return p.Next(min, max)

    End Function

    Public Shared Function gerarChaveAcesso(ByVal projeto As String, ByVal cUF As String, ByVal dhEmi As String, ByVal serie As String, ByVal nDocumento As String, ByVal tpEmis As String, ByVal cnpjEmitente As String, Optional tpEvento As String = "", Optional nSeqEvento As String = "")
        Dim chave43 As String
        Dim chave44 As String
        Dim x As Integer = serie.Length
        Dim y As Integer = nDocumento.Length
        Dim aamm As String

        While x < 3
            serie = "0" + serie
            x = x + 1
        End While

        While y < 9
            nDocumento = "0" + nDocumento
            y = y + 1
        End While

        aamm = dhEmi.Trim("T")
        aamm = aamm.Replace("-", "").Replace(":", "")
        aamm = aamm.Remove(aamm.Length - 11, 11)
        aamm = aamm.Remove(aamm.Length - 2, 2)
        aamm = aamm.Remove(0, 2)
        chave43 = cUF & aamm & cnpjEmitente & projeto & serie & nDocumento & tpEmis & gerarCodigoCNF()
        chave44 = tpEvento & chave43 & GerarDigitoVerificador(chave43) & nSeqEvento

        Return chave44
    End Function

    Public Shared Function gerarHashSHA1(ByVal chave44 As String, ByVal idCSRT As String) As String

        Dim hashRespTec As String
        Dim conteudo As String
        Dim data() As Byte
        Dim res() As Byte
        Dim sha As Security.Cryptography.SHA1 = New Security.Cryptography.SHA1Managed()

        conteudo = idCSRT + chave44
        data = Text.Encoding.UTF8.GetBytes(conteudo)
        res = sha.ComputeHash(data)
        hashRespTec = Convert.ToBase64String(res)

        Return hashRespTec
    End Function
End Class
