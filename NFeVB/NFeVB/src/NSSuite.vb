Imports System.IO
Imports System.Net
Imports Newtonsoft.Json
Imports System.Threading

Public Class NSSuite
    Private Shared token As String = "TGVvbmNpbyBHb21lcyBkb1V2Q1M="
    Private Shared Endpoints As New Endpoints
    Private Shared Parametros As New Parametros

    'Esta função envia um conteúdo para uma URL, em requisições do tipo POST
    Public Shared Function enviaConteudoParaAPI(ByVal conteudo As String, ByVal url As String, ByVal tpConteudo As String) As String
        Dim retorno As String = ""
        Dim httpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
        httpWebRequest.Method = "POST"
        httpWebRequest.Headers("X-AUTH-TOKEN") = token

        If tpConteudo = "txt" Then
            httpWebRequest.ContentType = "text/plain;charset=utf-8"
        ElseIf tpConteudo = "xml" Then
            httpWebRequest.ContentType = "application/xml;charset=utf-8"
        Else
            httpWebRequest.ContentType = "application/json;charset=utf-8"
        End If

        Using streamWriter = New StreamWriter(httpWebRequest.GetRequestStream())
            streamWriter.Write(conteudo)
            streamWriter.Flush()
            streamWriter.Close()
        End Using

        Try
            Dim httpResponse = CType(httpWebRequest.GetResponse(), HttpWebResponse)

            Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                retorno = streamReader.ReadToEnd()
            End Using

        Catch ex As WebException

            If ex.Status = WebExceptionStatus.ProtocolError Then
                Dim response As HttpWebResponse = CType(ex.Response, HttpWebResponse)

                Using streamReader = New StreamReader(response.GetResponseStream())
                    retorno = streamReader.ReadToEnd()
                End Using

                Select Case CInt(response.StatusCode)
                    Case 401
                        MessageBox.Show("Token nao enviado ou invalido")
                    Case 403
                        MessageBox.Show("Token sem permissao")
                    Case 404
                        MessageBox.Show("Nao encontrado, verifique o retorno para mais informacoes")
                    Case Else
                End Select
            End If
        End Try

        Return retorno
    End Function

    'Métodos específicos de BPe
    Public Shared Function emitirBPeSincrono(ByVal conteudo As String, ByVal tpConteudo As String, ByVal CNPJ As String, ByVal tpDown As String, ByVal tpAmb As String, ByVal caminho As String, ByVal Optional exibeNaTela As Boolean = False, ByVal Optional a3 As Boolean = False) As String
        Dim statusEnvio, statusConsulta, statusDownload, motivo, nsNRec, chBPe, cStat, nProt As String
        Dim retorno, resposta As String
        Dim erros As IList(Of String) = {}
        Dim modelo As String = "63"

        statusEnvio = ""
        statusConsulta = ""
        statusDownload = ""
        motivo = ""
        nsNRec = ""
        chBPe = ""
        cStat = ""
        nProt = ""

        Genericos.gravarLinhaLog(modelo, "[EMISSAO_SINCRONA_INICIO]")

        resposta = emitirDocumento(modelo, conteudo, tpConteudo, CNPJ, a3)

        Dim EmitirRespBPe = JsonConvert.DeserializeObject(Of EmitirRespBPe)(resposta)
        statusEnvio = EmitirRespBPe.status

        'Testa se o envio foi feito com sucesso (200) ou se é para reconsultar (-6)
        If (statusEnvio = "200") OrElse (statusEnvio = "-6") Then
            nsNRec = EmitirRespBPe.nsNRec

            'É necessário aguardar alguns milisegundos antes de consultar o status de processamento
            Thread.Sleep(Parametros.TEMPO_ESPERA)

            Dim ConsStatusProcessamentoReqBPe As New ConsStatusProcessamentoReqBPe With {
                .CNPJ = CNPJ,
                .nsNRec = nsNRec,
                .tpAmb = tpAmb
            }

            resposta = consultarStatusProcessamento(modelo, ConsStatusProcessamentoReqBPe)
            Dim ConsStatusProcessamentoRespBPe = JsonConvert.DeserializeObject(Of ConsStatusProcessamentoRespBPe)(resposta)
            statusConsulta = ConsStatusProcessamentoRespBPe.status

            If (statusConsulta = "-2") Then
                cStat = ConsStatusProcessamentoRespBPe.cStat
                If (cStat = "996") Then
                    motivo = ConsStatusProcessamentoRespBPe.erro.xMotivo
                    For index As Integer = 0 To 2
                        Thread.Sleep(6000 - (index * 1000))
                        resposta = consultarStatusProcessamento(modelo, ConsStatusProcessamentoReqBPe)
                        statusConsulta = JsonConvert.DeserializeObject(Of ConsStatusProcessamentoRespBPe)(resposta).status
                        If (statusConsulta <> -2) Then
                            Exit For
                        End If
                    Next
                Else
                    motivo = ConsStatusProcessamentoRespBPe.motivo
                End If
            End If

            'Testa se a consulta foi feita com sucesso (200)
            If (statusConsulta = "200") Then

                cStat = ConsStatusProcessamentoRespBPe.cStat

                'Testa se o cStat é igual a 100, pois significa "Autorizado"
                If (cStat = "100") Then
                    chBPe = ConsStatusProcessamentoRespBPe.chBPe

                    nProt = ConsStatusProcessamentoRespBPe.nProt

                    motivo = ConsStatusProcessamentoRespBPe.xMotivo

                    Dim DownloadReqBPe As New DownloadReqBPe With {
                            .chBPe = chBPe,
                            .tpAmb = tpAmb,
                            .tpDown = tpDown
                        }

                    resposta = downloadDocumentoESalvar(modelo, DownloadReqBPe, caminho, chBPe + "-NFe", exibeNaTela)

                    Dim DownloadRespBPe = JsonConvert.DeserializeObject(Of DownloadRespBPe)(resposta)
                    statusDownload = DownloadRespBPe.status

                    'Testa se houve problema no download
                    If statusDownload <> "200" Then
                        motivo = DownloadRespBPe.motivo
                    End If
                Else
                    motivo = ConsStatusProcessamentoRespBPe.xMotivo
                End If
            ElseIf statusConsulta = "-2" Then
                cStat = ConsStatusProcessamentoRespBPe.erro.cStat
                motivo = ConsStatusProcessamentoRespBPe.erro.xMotivo
            Else
                motivo = ConsStatusProcessamentoRespBPe.motivo
            End If
        ElseIf (statusEnvio = "-5") Then
            cStat = EmitirRespBPe.erro.cStat
            motivo = EmitirRespBPe.erro.xMotivo
        ElseIf (statusEnvio = "-4") OrElse (statusEnvio = "-2") Then
            motivo = EmitirRespBPe.motivo
            erros = EmitirRespBPe.erros
        Else

            Try
                motivo = EmitirRespBPe.motivo
            Catch ex As Exception
                motivo = EmitirRespBPe.ToString()
            End Try
        End If

        Dim EmitirSincronoRetBPe As New EmitirSincronoRetBPe With {
            .statusEnvio = statusEnvio,
            .statusConsulta = statusConsulta,
            .statusDownload = statusDownload,
            .cStat = cStat,
            .chBPe = chBPe,
            .nProt = nProt,
            .motivo = motivo,
            .nsNRec = nsNRec,
            .erros = erros
        }

        retorno = JsonConvert.SerializeObject(EmitirSincronoRetBPe)

        Genericos.gravarLinhaLog(modelo, "[JSON_RETORNO]")
        Genericos.gravarLinhaLog(modelo, retorno)
        Genericos.gravarLinhaLog(modelo, "[EMISSAO_SINCRONA_FIM]")

        Return retorno
    End Function

    Public Shared Function naoEmbarque(ByVal modelo As String, NaoEmbReq As NaoEmbReqBPe) As String
        Dim urlNaoEmb As String

        Select Case modelo
            Case "63"
                urlNaoEmb = Endpoints.BPeNaoEmb
            Case Else
                Throw New Exception("Não definido endpoint de não embarque para o modelo " + modelo)
        End Select

        Dim json As String = JsonConvert.SerializeObject(NaoEmbReq)

        Genericos.gravarLinhaLog(modelo, "[NAO_EMBARQUE_DADOS]")
        Genericos.gravarLinhaLog(modelo, json)

        Dim resposta As String = enviaConteudoParaAPI(json, urlNaoEmb, "json")

        Genericos.gravarLinhaLog(modelo, "[NAO_EMBARQUE_RESPOSTA]")
        Genericos.gravarLinhaLog(modelo, resposta)

        Return resposta
    End Function

    Public Shared Function naoEmbarqueESalvar(ByVal modelo As String, NaoEmbReq As NaoEmbReqBPe, DownloadEventoReq As DownloadEventoReq, ByVal caminho As String, ByVal chave As String, ByVal Optional exibeNaTela As Boolean = False) As String
        Dim resposta As String = naoEmbarque(modelo, NaoEmbReq)
        Dim NaoEmbResp As New NaoEmbResp
        Dim status As String
        Dim cStat As String

        NaoEmbResp = JsonConvert.DeserializeObject(Of NaoEmbResp)(resposta)
        status = NaoEmbResp.status

        If (status = "200") Then
            cStat = NaoEmbResp.retEvento.cStat
            If (cStat = "135") Then
                Dim respostaDownloadEvento As String = downloadEventoESalvar(modelo, DownloadEventoReq, caminho, chave, "", exibeNaTela)
            End If
        Else
            MessageBox.Show("Ocorreu um erro ao não embarcar, veja o retorno da API para mais informações")
        End If

        Return resposta
    End Function

    'Métodos específicos de CTe
    Public Shared Function emitirCTeSincrono(ByVal conteudo As String, ByVal [mod] As String, ByVal tpConteudo As String, ByVal CNPJ As String, ByVal tpDown As String, ByVal tpAmb As String, ByVal caminho As String, ByVal Optional exibeNaTela As Boolean = False, ByVal Optional a3 As Boolean = False) As String
        Dim statusEnvio, statusConsulta, statusDownload, motivo, nsNRec, chCTe, cStat, nProt As String
        Dim retorno, resposta As String
        Dim erros As IList(Of String) = {}
        Dim modelo As String = "57" 'TROCAR PARA [mod] ESSE 57 

        statusEnvio = ""
        statusConsulta = ""
        statusDownload = ""
        motivo = ""
        nsNRec = ""
        chCTe = ""
        cStat = ""
        nProt = ""

        Genericos.gravarLinhaLog(modelo, "[EMISSAO_SINCRONA_INICIO]")

        resposta = emitirDocumento(modelo, conteudo, tpConteudo, CNPJ, a3)

        Dim EmitirRespCTe = JsonConvert.DeserializeObject(Of EmitirRespCTe)(resposta)
        statusEnvio = EmitirRespCTe.status

        'Testa se o envio foi feito com sucesso (200) ou se é para reconsultar (-6)
        If (statusEnvio = "200") OrElse (statusEnvio = "-7") Then
            nsNRec = EmitirRespCTe.nsNRec

            'É necessário aguardar alguns milisegundos antes de consultar o status de processamento
            Thread.Sleep(Parametros.TEMPO_ESPERA)

            Dim ConsStatusProcessamentoReqCTe As New ConsStatusProcessamentoReqCTe With {
                .CNPJ = CNPJ,
                .nsNRec = nsNRec,
                .tpAmb = tpAmb
            }

            resposta = consultarStatusProcessamento(modelo, ConsStatusProcessamentoReqCTe)

            Dim ConsStatusProcessamentoRespCTe = JsonConvert.DeserializeObject(Of ConsStatusProcessamentoRespCTe)(resposta)
            statusConsulta = ConsStatusProcessamentoRespCTe.status

            'Testa se a consulta foi feita com sucesso (200)
            If statusConsulta = "200" Then

                cStat = ConsStatusProcessamentoRespCTe.cStat

                If (cStat = "100") OrElse (cStat = "150") Then
                    chCTe = ConsStatusProcessamentoRespCTe.chCTe

                    nProt = ConsStatusProcessamentoRespCTe.nProt

                    motivo = ConsStatusProcessamentoRespCTe.xMotivo

                    Dim DownloadReqCTe As New DownloadReqCTe With {
                        .chCTe = chCTe,
                        .CNPJ = CNPJ,
                        .tpAmb = tpAmb,
                        .tpDown = tpDown
                    }

                    resposta = downloadDocumentoESalvar(modelo, DownloadReqCTe, caminho, chCTe + "-CTe", exibeNaTela)

                    Dim DownloadRespCTe = JsonConvert.DeserializeObject(Of DownloadRespCTe)(resposta)

                    statusDownload = DownloadRespCTe.status

                    If statusDownload <> "200" Then
                        motivo = DownloadRespCTe.motivo
                    End If
                Else
                    motivo = ConsStatusProcessamentoRespCTe.xMotivo
                End If
            Else
                motivo = ConsStatusProcessamentoRespCTe.motivo
                erros = ConsStatusProcessamentoRespCTe.erros
            End If
        ElseIf statusEnvio = "-7" Then

            motivo = EmitirRespCTe.motivo
            nsNRec = EmitirRespCTe.nsNRec

        ElseIf statusEnvio = "-4" Then
            motivo = EmitirRespCTe.motivo

            Try
                erros = EmitirRespCTe.erros
            Catch ex As Exception
            End Try

        ElseIf statusEnvio = "-9" Then
            motivo = EmitirRespCTe.erro.xMotivo
            cStat = EmitirRespCTe.erro.cStat
        Else

            Try
                motivo = EmitirRespCTe.motivo
            Catch ex As Exception
                motivo = EmitirRespCTe.ToString()
            End Try
        End If

        Dim EmitirSincronoRetCTe As New EmitirSincronoRetCTe With {
            .statusEnvio = statusEnvio,
            .statusConsulta = statusConsulta,
            .statusDownload = statusDownload,
            .cStat = cStat,
            .chCTe = chCTe,
            .nProt = nProt,
            .motivo = motivo,
            .nsNRec = nsNRec,
            .erros = erros
        }

        retorno = JsonConvert.SerializeObject(EmitirSincronoRetCTe)

        Genericos.gravarLinhaLog(modelo, "[JSON_RETORNO]")
        Genericos.gravarLinhaLog(modelo, retorno)
        Genericos.gravarLinhaLog(modelo, "[EMISSAO_SINCRONA_FIM]")

        Return retorno
    End Function

    Public Shared Function informarGTV(ByVal modelo As String, InfGTVReqCTe As InfGTVReqCTe) As String
        Dim urlInfGTV As String

        Select Case modelo
            Case "57", "67"
                urlInfGTV = Endpoints.CTeCancelamento
            Case Else
                Throw New Exception("Não definido endpoint de informação de GTV para o modelo " + modelo)
        End Select

        Dim json As String = JsonConvert.SerializeObject(InfGTVReqCTe)

        Genericos.gravarLinhaLog(modelo, "[INFORMACOES_GTV_DADOS]")
        Genericos.gravarLinhaLog(modelo, json)

        Dim resposta As String = enviaConteudoParaAPI(json, urlInfGTV, "json")

        Genericos.gravarLinhaLog(modelo, "[INFORMACOES_GTV_RESPOSTA]")
        Genericos.gravarLinhaLog(modelo, resposta)

        Return resposta
    End Function

    Public Shared Function informarGTVESalvar(ByVal modelo As String, InfGTVReqCTe As InfGTVReqCTe, DownloadEventoReq As DownloadEventoReq, ByVal caminho As String, ByVal chave As String, ByVal Optional exibeNaTela As Boolean = False) As String
        Dim resposta As String = informarGTV(modelo, InfGTVReqCTe)
        Dim InfGTVRespCTe As New InfGTVRespCTe
        Dim status As String

        InfGTVRespCTe = JsonConvert.DeserializeObject(Of InfGTVRespCTe)(resposta)
        status = InfGTVRespCTe.status

        If (status = "200") Then
            Dim respostaDownloadEvento As String = downloadEventoESalvar(modelo, DownloadEventoReq, caminho, chave, "", exibeNaTela)
        Else
            MessageBox.Show("Ocorreu um erro ao cancelar, veja o retorno da API para mais informações")
        End If

        Return resposta
    End Function


    'Métodos específicos de MDFe
    Public Shared Function emitirMDFeSincrono(ByVal conteudo As String, ByVal tpConteudo As String, ByVal CNPJ As String, ByVal tpDown As String, ByVal tpAmb As String, ByVal caminho As String, ByVal Optional exibeNaTela As Boolean = False, ByVal Optional a3 As Boolean = False) As String
        Dim statusEnvio, statusConsulta, statusDownload, motivo, nsNRec, chMDFe, cStat, nProt As String
        Dim retorno, resposta As String
        Dim erros As IList(Of String) = {}
        Dim modelo As String = "58"

        statusEnvio = ""
        statusConsulta = ""
        statusDownload = ""
        motivo = ""
        nsNRec = ""
        chMDFe = ""
        cStat = ""
        nProt = ""

        Genericos.gravarLinhaLog(modelo, "[EMISSAO_SINCRONA_INICIO]")

        resposta = emitirDocumento(modelo, conteudo, tpConteudo, CNPJ, a3)

        Dim EmitirRespMDFe = JsonConvert.DeserializeObject(Of EmitirRespMDFe)(resposta)
        statusEnvio = EmitirRespMDFe.status

        'Testa se o envio foi feito com sucesso (200) ou se é para reconsultar (-6)
        If (statusEnvio = "200") OrElse (statusEnvio = "-6") Then
            nsNRec = EmitirRespMDFe.nsNRec

            'É necessário aguardar alguns milisegundos antes de consultar o status de processamento
            Thread.Sleep(Parametros.TEMPO_ESPERA)

            Dim ConsStatusProcessamentoReqMDFe As New ConsStatusProcessamentoReqMDFe With {
                .CNPJ = CNPJ,
                .nsNRec = nsNRec,
                .tpAmb = tpAmb
            }

            resposta = consultarStatusProcessamento(modelo, ConsStatusProcessamentoReqMDFe)

            Dim ConsStatusProcessamentoRespMDFe = JsonConvert.DeserializeObject(Of ConsStatusProcessamentoRespMDFe)(resposta)
            statusConsulta = ConsStatusProcessamentoRespMDFe.status

            'Testa se a consulta foi feita com sucesso (200)
            If statusConsulta = "200" Then

                cStat = ConsStatusProcessamentoRespMDFe.cStat

                'Testa se o cStat é igual a 100, pois significa "Autorizado"
                If cStat = "100" Then
                    chMDFe = ConsStatusProcessamentoRespMDFe.chMDFe

                    nProt = ConsStatusProcessamentoRespMDFe.nProt

                    motivo = ConsStatusProcessamentoRespMDFe.xMotivo

                    Dim DownloadReqMDFe As New DownloadReqMDFe With {
                        .chMDFe = chMDFe,
                        .tpAmb = tpAmb,
                        .tpDown = tpDown
                    }

                    resposta = downloadDocumentoESalvar(modelo, DownloadReqMDFe, caminho, chMDFe + "-MDFe", exibeNaTela)

                    Dim DownloadRespMDFe = JsonConvert.DeserializeObject(Of DownloadRespMDFe)(resposta)

                    statusDownload = DownloadRespMDFe.status

                    'Testa se houve problema no download
                    If statusDownload <> "200" Then
                        motivo = DownloadRespMDFe.motivo
                    End If
                Else
                    motivo = ConsStatusProcessamentoRespMDFe.xMotivo
                End If
            ElseIf statusConsulta = "-2" Then
                cStat = ConsStatusProcessamentoRespMDFe.erro.cStat

                motivo = ConsStatusProcessamentoRespMDFe.erro.xMotivo
            Else
                motivo = ConsStatusProcessamentoRespMDFe.motivo
            End If
        ElseIf statusEnvio = "-5" Then
            cStat = EmitirRespMDFe.erro.cStat

            motivo = EmitirRespMDFe.erro.xMotivo
        ElseIf (statusEnvio = "-4") OrElse (statusEnvio = "-2") Then
            motivo = EmitirRespMDFe.motivo

            Try
                erros = EmitirRespMDFe.erros
            Catch ex As Exception
            End Try
        ElseIf (statusEnvio = "-999") Then
            motivo = EmitirRespMDFe.erro.xMotivo
        Else

            Try
                motivo = EmitirRespMDFe.motivo
            Catch ex As Exception
                motivo = EmitirRespMDFe.ToString()
            End Try
        End If

        Dim EmitirSincronoRetMDFe As New EmitirSincronoRetMDFe With {
            .statusEnvio = statusEnvio,
            .statusConsulta = statusConsulta,
            .statusDownload = statusDownload,
            .cStat = cStat,
            .chMDFe = chMDFe,
            .nProt = nProt,
            .motivo = motivo,
            .nsNRec = nsNRec,
            .erros = erros
        }

        retorno = JsonConvert.SerializeObject(EmitirSincronoRetMDFe)

        Genericos.gravarLinhaLog(modelo, "[JSON_RETORNO]")
        Genericos.gravarLinhaLog(modelo, retorno)
        Genericos.gravarLinhaLog(modelo, "[EMISSAO_SINCRONA_FIM]")

        Return retorno
    End Function

    Public Shared Function encerrarDocumento(ByVal modelo As String, EncerrarReq As EncerrarReq) As String
        Dim urlEncerramento As String

        Select Case modelo
            Case "58"
                urlEncerramento = Endpoints.MDFeEncerramento
            Case Else
                Throw New Exception("Não definido endpoint de encerramento para o modelo " + modelo)
        End Select

        Dim json As String = JsonConvert.SerializeObject(EncerrarReq)

        Genericos.gravarLinhaLog(modelo, "[ENCERRAMENTO_DADOS]")
        Genericos.gravarLinhaLog(modelo, json)

        Dim resposta As String = enviaConteudoParaAPI(json, urlEncerramento, "json")

        Genericos.gravarLinhaLog(modelo, "[ENCERRAMENTO_RESPOSTA]")
        Genericos.gravarLinhaLog(modelo, resposta)

        Return resposta
    End Function

    Public Shared Function encerrarDocumentoESalvar(ByVal modelo As String, EncerrarReq As EncerrarReq, DownloadEventoReq As DownloadEventoReq, ByVal caminho As String, ByVal chave As String, ByVal Optional exibeNaTela As Boolean = False) As String
        Dim resposta As String = encerrarDocumento(modelo, EncerrarReq)
        Dim EncerrarResp As New EncerrarResp
        Dim status As String

        EncerrarResp = JsonConvert.DeserializeObject(Of EncerrarResp)(resposta)
        status = EncerrarResp.status

        If (status = "200") Then
            Dim respostaDownloadEvento As String = downloadEventoESalvar(modelo, DownloadEventoReq, caminho, chave, "", exibeNaTela)
        Else
            MessageBox.Show("Ocorreu um erro ao encerrar, veja o retorno da API para mais informações")
        End If

        Return resposta
    End Function

    Public Shared Function incluirCondutor(ByVal modelo As String, IncCondutorReq As IncCondutorReq) As String
        Dim urlIncCondutor As String

        Select Case modelo
            Case "58"
                urlIncCondutor = Endpoints.MDFeIncCondutor
            Case Else
                Throw New Exception("Não definido endpoint de inclusão de condutor para o modelo " + modelo)
        End Select

        Dim json As String = JsonConvert.SerializeObject(IncCondutorReq)

        Genericos.gravarLinhaLog(modelo, "[INC_CONDUTOR_DADOS]")
        Genericos.gravarLinhaLog(modelo, json)

        Dim resposta As String = enviaConteudoParaAPI(json, urlIncCondutor, "json")

        Genericos.gravarLinhaLog(modelo, "[INC_CONDUTOR_RESPOSTA]")
        Genericos.gravarLinhaLog(modelo, resposta)

        Return resposta
    End Function

    Public Shared Function incluirCondutorESalvar(ByVal modelo As String, IncCondutorReq As IncCondutorReq, DownloadEventoReq As DownloadEventoReq, ByVal caminho As String, ByVal chave As String, ByVal Optional exibeNaTela As Boolean = False) As String
        Dim resposta As String = incluirCondutor(modelo, IncCondutorReq)
        Dim IncCondutorResp As New IncCondutorResp
        Dim status As String

        IncCondutorResp = JsonConvert.DeserializeObject(Of IncCondutorResp)(resposta)
        status = IncCondutorResp.status

        If (status = "200") Then
            Dim respostaDownloadEvento As String = downloadEventoESalvar(modelo, DownloadEventoReq, caminho, chave, "", exibeNaTela)
        Else
            MessageBox.Show("Ocorreu um erro ao incluir condutor, veja o retorno da API para mais informações")
        End If

        Return resposta
    End Function

    Public Shared Function consultarNaoEncerrados(ByVal modelo As String, ConsNaoEncerradosReq As ConsNaoEncerradosReq) As String
        Dim urlConsNaoEncerrados As String

        Select Case modelo
            Case "58"
                urlConsNaoEncerrados = Endpoints.MDFeConsNaoEncerrados
            Case Else
                Throw New Exception("Não definido endpoint de consulta de não encerrados para o modelo " + modelo)
        End Select

        Dim json As String = JsonConvert.SerializeObject(ConsNaoEncerradosReq)

        Genericos.gravarLinhaLog(modelo, "[CONS_NAO_ENCERRADOS_DADOS]")
        Genericos.gravarLinhaLog(modelo, json)

        Dim resposta As String = enviaConteudoParaAPI(json, urlConsNaoEncerrados, "json")

        Genericos.gravarLinhaLog(modelo, "[CONS_NAO_ENCERRADOS_RESPOSTA]")
        Genericos.gravarLinhaLog(modelo, resposta)

        Return resposta
    End Function

    Public Shared Function incluirDFes(ByVal modelo As String, IncluirDFeReq As IncluirDFeReq) As String
        Dim urlIncluirDFe As String

        Select Case modelo
            Case "58"
                urlIncluirDFe = Endpoints.MDFeIncluirDFe
            Case Else
                Throw New Exception("Não definido endpoint de inclusão de condutor para o modelo " + modelo)
        End Select

        Dim json As String = JsonConvert.SerializeObject(IncluirDFeReq)

        Genericos.gravarLinhaLog(modelo, "[INCLUIR_DFE_DADOS]")
        Genericos.gravarLinhaLog(modelo, json)

        Dim resposta As String = enviaConteudoParaAPI(json, urlIncluirDFe, "json")

        Genericos.gravarLinhaLog(modelo, "[INCLUIR_DFE_RESPOSTA]")
        Genericos.gravarLinhaLog(modelo, resposta)

        Return resposta
    End Function

    Public Shared Function incluirDFesESalvar(ByVal modelo As String, IncluirDFeReq As IncluirDFeReq, DownloadEventoReq As DownloadEventoReq, ByVal caminho As String, ByVal chave As String, ByVal Optional exibeNaTela As Boolean = False) As String
        Dim resposta As String = incluirDFes(modelo, IncluirDFeReq)
        Dim IncluirDFeResp As New IncluirDFeResp
        Dim status As String

        IncluirDFeResp = JsonConvert.DeserializeObject(Of IncluirDFeResp)(resposta)
        status = IncluirDFeResp.status

        If (status = "200") Then
            Dim respostaDownloadEvento As String = downloadEventoESalvar(modelo, DownloadEventoReq, caminho, chave, "", exibeNaTela)
        Else
            MessageBox.Show("Ocorreu um erro ao incluir DFe, veja o retorno da API para mais informações")
        End If

        Return resposta
    End Function

    'Métodos específicos de NFCe
    Public Shared Function emitirNFCeSincrono(ByVal conteudo As String, ByVal tpConteudo As String, ByVal CNPJ As String, ByVal tpAmb As String, ByVal caminho As String, ByVal Optional exibeNaTela As Boolean = False, ByVal Optional a3 As Boolean = False) As String
        Dim statusEnvio, statusDownload, motivo, chNFe, cStat, nProt As String
        Dim retorno, resposta As String
        Dim erros As IList(Of String) = {}
        Dim modelo As String = "65"

        statusEnvio = ""
        statusDownload = ""
        motivo = ""
        chNFe = ""
        cStat = ""
        nProt = ""

        Genericos.gravarLinhaLog(modelo, "[EMISSAO_SINCRONA_INICIO]")

        resposta = emitirDocumento(modelo, conteudo, tpConteudo, CNPJ, a3)

        Dim EmitirRespNFCe = JsonConvert.DeserializeObject(Of EmitirRespNFCe)(resposta)
        statusEnvio = EmitirRespNFCe.status

        If (statusEnvio = "100") OrElse (statusEnvio = "-100") Then
            cStat = EmitirRespNFCe.nfeProc.cStat

            If (cStat = "100") OrElse (cStat = "150") Then
                chNFe = EmitirRespNFCe.nfeProc.chNFe

                nProt = EmitirRespNFCe.nfeProc.nProt

                motivo = EmitirRespNFCe.nfeProc.xMotivo

                Thread.Sleep(Parametros.TEMPO_ESPERA)

                Dim DownloadReqNFCe As New DownloadReqNFCe With {
                    .chNFe = chNFe,
                    .tpAmb = tpAmb,
                    .impressao = New Impressao
                }

                resposta = downloadDocumentoESalvar(modelo, DownloadReqNFCe, caminho, chNFe + "-NFe", exibeNaTela)

                Dim DownloadRespNFCe = JsonConvert.DeserializeObject(Of DownloadRespNFCe)(resposta)
                statusDownload = DownloadRespNFCe.status

                If statusDownload <> "100" Then
                    motivo = DownloadRespNFCe.motivo
                End If
            Else
                motivo = EmitirRespNFCe.nfeProc.xMotivo
            End If
        ElseIf statusEnvio = "-995" Then
            motivo = EmitirRespNFCe.motivo

            Try
                erros = EmitirRespNFCe.erros
            Catch ex As Exception
            End Try
        Else
            motivo = EmitirRespNFCe.motivo
        End If

        Dim EmitirSincronoRetNFCe As New EmitirSincronoRetNFCe With {
            .statusEnvio = statusEnvio,
            .statusDownload = statusDownload,
            .cStat = cStat,
            .chNFe = chNFe,
            .nProt = nProt,
            .motivo = motivo,
            .erros = erros
        }

        retorno = JsonConvert.SerializeObject(EmitirSincronoRetNFCe)

        Genericos.gravarLinhaLog(modelo, "[JSON_RETORNO]")
        Genericos.gravarLinhaLog(modelo, retorno)
        Genericos.gravarLinhaLog(modelo, "[EMISSAO_SINCRONA_FIM]")
        Return retorno
    End Function


    'Métodos específicos de NFe
    Public Shared Function emitirNFeSincrono(ByVal conteudo As String, ByVal tpConteudo As String, ByVal CNPJ As String, ByVal tpDown As String, ByVal tpAmb As String, ByVal caminho As String, ByVal Optional exibeNaTela As Boolean = False, ByVal Optional a3 As Boolean = False) As String
        Dim statusEnvio, statusConsulta, statusDownload, motivo, nsNRec, chNFe, cStat, nProt As String
        Dim retorno, resposta As String
        Dim erros As IList(Of String) = {}
        Dim modelo As String = "55"

        statusEnvio = ""
        statusConsulta = ""
        statusDownload = ""
        motivo = ""
        nsNRec = ""
        chNFe = ""
        cStat = ""
        nProt = ""

        Genericos.gravarLinhaLog(modelo, "[EMISSAO_SINCRONA_INICIO]")

        resposta = emitirDocumento(modelo, conteudo, tpConteudo, CNPJ, a3)

        Dim EmitirRespNFe = JsonConvert.DeserializeObject(Of EmitirRespNFe)(resposta)
        statusEnvio = EmitirRespNFe.status

        'Testa se o envio foi feito com sucesso (200) ou se é para reconsultar (-6)
        If (statusEnvio = "200") OrElse (statusEnvio = "-7") Then
            nsNRec = EmitirRespNFe.nsNRec

            'É necessário aguardar alguns milisegundos antes de consultar o status de processamento
            Thread.Sleep(Parametros.TEMPO_ESPERA)

            Dim ConsStatusProcessamentoReqNFe As New ConsStatusProcessamentoReqNFe With {
                .CNPJ = CNPJ,
                .nsNRec = nsNRec,
                .tpAmb = tpAmb
            }

            resposta = consultarStatusProcessamento(modelo, ConsStatusProcessamentoReqNFe)

            Dim ConsStatusProcessamentoRespNFe = JsonConvert.DeserializeObject(Of ConsStatusProcessamentoRespNFe)(resposta)
            statusConsulta = ConsStatusProcessamentoRespNFe.status

            If (statusConsulta = "-2") Then
                cStat = ConsStatusProcessamentoRespNFe.cStat
                If (cStat = "996") Then
                    motivo = ConsStatusProcessamentoRespNFe.erro.xMotivo
                    For index As Integer = 0 To 2
                        Thread.Sleep(6000 - (index * 1000))
                        resposta = consultarStatusProcessamento(modelo, ConsStatusProcessamentoReqNFe)
                        statusConsulta = JsonConvert.DeserializeObject(Of ConsStatusProcessamentoRespNFe)(resposta).status
                        If (statusConsulta <> -2) Then
                            Exit For
                        End If
                    Next
                Else
                    motivo = ConsStatusProcessamentoRespNFe.motivo
                End If
            End If

            'Testa se a consulta foi feita com sucesso (200)
            If statusConsulta = "200" Then

                cStat = ConsStatusProcessamentoRespNFe.cStat

                'Testa se o cStat é igual a 100 ou 150, pois ambos significam "Autorizado"
                If (cStat = "100") OrElse (cStat = "150") Then
                    chNFe = ConsStatusProcessamentoRespNFe.chNFe

                    nProt = ConsStatusProcessamentoRespNFe.nProt

                    motivo = ConsStatusProcessamentoRespNFe.xMotivo

                    Dim DownloadReqNFe As New DownloadReqNFe With {
                        .chNFe = chNFe,
                        .tpAmb = tpAmb,
                        .tpDown = tpDown
                    }

                    resposta = downloadDocumentoESalvar(modelo, DownloadReqNFe, caminho, chNFe + "-NFe", exibeNaTela)

                    Dim DownloadRespNFe = JsonConvert.DeserializeObject(Of DownloadRespNFe)(resposta)
                    statusDownload = DownloadRespNFe.status

                    'Testa se houve problema no download
                    If statusDownload <> "200" Then
                        motivo = DownloadRespNFe.motivo
                    End If
                Else
                    motivo = ConsStatusProcessamentoRespNFe.xMotivo
                End If
            ElseIf (statusConsulta = "-2") Then
                cStat = ConsStatusProcessamentoRespNFe.cStat

                motivo = ConsStatusProcessamentoRespNFe.erro.xMotivo
            Else
                motivo = ConsStatusProcessamentoRespNFe.motivo
            End If
        ElseIf statusEnvio = "-7" Then
            motivo = EmitirRespNFe.motivo

            nsNRec = EmitirRespNFe.nsNRec
        ElseIf (statusEnvio = "-4") OrElse (statusEnvio = "-2") Then

            motivo = EmitirRespNFe.motivo

            Try
                erros = EmitirRespNFe.erros
            Catch ex As Exception
            End Try
        ElseIf (statusEnvio = "-999") OrElse (statusEnvio = "-5") Then
            motivo = EmitirRespNFe.erro.xMotivo

        Else
            Try
                motivo = EmitirRespNFe.motivo
            Catch ex As Exception
                motivo = EmitirRespNFe.ToString()
            End Try
        End If

        Dim EmitirSincronoRetNFe As New EmitirSincronoRetNFe With {
            .statusEnvio = statusEnvio,
            .statusConsulta = statusConsulta,
            .statusDownload = statusDownload,
            .cStat = cStat,
            .chNFe = chNFe,
            .nProt = nProt,
            .motivo = motivo,
            .nsNRec = nsNRec,
            .erros = erros
        }

        retorno = JsonConvert.SerializeObject(EmitirSincronoRetNFe)

        Genericos.gravarLinhaLog(modelo, "[JSON_RETORNO]")
        Genericos.gravarLinhaLog(modelo, retorno)
        Genericos.gravarLinhaLog(modelo, "[EMISSAO_SINCRONA_FIM]")

        Return retorno
    End Function


    'Métodos genéricos, compartilhados entre diversas funções
    Public Shared Function emitirDocumento(ByVal modelo As String, ByVal conteudo As String, ByVal tpConteudo As String, ByVal cnpjEmitente As String, ByVal a3 As Boolean) As String
        Dim urlEnvio As String
        Dim nodo As String
        Select Case modelo
            Case "63"
                urlEnvio = Endpoints.BPeEnvio
                nodo = "infBPe"
            Case "57"
                urlEnvio = Endpoints.CTeEnvio
                nodo = "infCTe"
            Case "67"
                urlEnvio = Endpoints.CTeOSEnvio
                nodo = "infCTe"
            Case "58"
                urlEnvio = Endpoints.MDFeEnvio
                nodo = "infMDFe"
            Case "65"
                urlEnvio = Endpoints.NFCeEnvio
                nodo = "infNFe"
            Case "55"
                urlEnvio = Endpoints.NFeEnvio
                nodo = "infNFe"
            Case Else
                Throw New Exception("Não definido endpoint de envio para o modelo " + modelo)
        End Select

        If (a3) Then
            Dim xml As String
            Try
                If ("JSON".Equals(tpConteudo.ToUpper()) Or "TXT".Equals(tpConteudo.ToUpper())) Then
                    tpConteudo = "xml"
                Else
                    xml = conteudo
                End If
            Catch ex As Exception

            End Try
        End If

        Genericos.gravarLinhaLog(modelo, "[ENVIO_DADOS]")
        Genericos.gravarLinhaLog(modelo, conteudo)

        Dim resposta As String = enviaConteudoParaAPI(conteudo, urlEnvio, tpConteudo)

        Genericos.gravarLinhaLog(modelo, "[ENVIO_RESPOSTA]")
        Genericos.gravarLinhaLog(modelo, resposta)

        Return resposta
    End Function

    Public Shared Function consultarStatusProcessamento(ByVal modelo As String, ConsStatusProcessamentoReq As ConsStatusProcessamentoReq) As String
        Dim urlConsulta As String

        Select Case modelo
            Case "63"
                urlConsulta = Endpoints.BPeConsStatusProcessamento
            Case "57", "67"
                urlConsulta = Endpoints.CTeConsStatusProcessamento
            Case "58"
                urlConsulta = Endpoints.MDFeConsStatusProcessamento
            Case "55"
                urlConsulta = Endpoints.NFeConsStatusProcessamento
            Case Else
                Throw New Exception("Não definido endpoint de consulta para o modelo " + modelo)
        End Select

        Dim json As String = JsonConvert.SerializeObject(ConsStatusProcessamentoReq)

        Genericos.gravarLinhaLog(modelo, "[CONSULTA_DADOS]")
        Genericos.gravarLinhaLog(modelo, json)

        Dim resposta As String = enviaConteudoParaAPI(json, urlConsulta, "json")

        Genericos.gravarLinhaLog(modelo, "[CONSULTA_RESPOSTA]")
        Genericos.gravarLinhaLog(modelo, resposta)
        Return resposta
    End Function

    Public Shared Function downloadDocumento(ByVal modelo As String, DownloadReq As DownloadReq) As String
        Dim urlDownload As String

        Select Case modelo
            Case "63"
                urlDownload = Endpoints.BPeDownload
            Case "57", "67"
                urlDownload = Endpoints.CTeDownload
            Case "58"
                urlDownload = Endpoints.MDFeDownload
            Case "65"
                urlDownload = Endpoints.NFCeDownload
            Case "55"
                urlDownload = Endpoints.NFeDownload
            Case Else
                Throw New Exception("Não definido endpoint de download para o modelo " + modelo)
        End Select

        Dim json As String = JsonConvert.SerializeObject(DownloadReq)

        Genericos.gravarLinhaLog(modelo, "[DOWNLOAD_DADOS]")
        Genericos.gravarLinhaLog(modelo, json)

        Dim resposta As String = enviaConteudoParaAPI(json, urlDownload, "json")

        Dim status As String
        If Not (modelo = "65") Then
            Dim DownloadResp As New DownloadResp
            DownloadResp = JsonConvert.DeserializeObject(Of DownloadResp)(resposta)
            status = DownloadResp.status
        Else
            Dim DownloadRespNFCe As New DownloadRespNFCe
            DownloadRespNFCe = JsonConvert.DeserializeObject(Of DownloadRespNFCe)(resposta)
            status = DownloadRespNFCe.status
        End If

        'O retorno da API será gravado somente em caso de erro, 
        'para não gerar um log extenso com o PDF e XML
        If (status <> "200") And (status <> "100") Then
            Genericos.gravarLinhaLog(modelo, "[DOWNLOAD_RESPOSTA]")
            Genericos.gravarLinhaLog(modelo, resposta)
        Else
            Genericos.gravarLinhaLog(modelo, "[DOWNLOAD_STATUS]")
            Genericos.gravarLinhaLog(modelo, status)
        End If

        Return resposta
    End Function

    Public Shared Function downloadDocumentoESalvar(ByVal modelo As String, DownloadReq As DownloadReq, ByVal caminho As String, ByVal nome As String, ByVal Optional exibeNaTela As Boolean = False) As String
        Dim resposta As String = downloadDocumento(modelo, DownloadReq)
        Dim DownloadResp As New DownloadResp
        Dim DownloadRespNFCe As New DownloadRespNFCe

        Dim status As String
        If Not (modelo = "65") Then
            DownloadResp = JsonConvert.DeserializeObject(Of DownloadResp)(resposta)
            status = DownloadResp.status
        Else
            DownloadRespNFCe = JsonConvert.DeserializeObject(Of DownloadRespNFCe)(resposta)
            status = DownloadRespNFCe.status
        End If

        If (status = "200") OrElse (status = "100") Then
            'Cria o diretório, caso não exista
            Try
                If Not Directory.Exists(caminho) Then Directory.CreateDirectory(caminho)
                If Not caminho.EndsWith("\") Then caminho += "\"
            Catch ex As IOException
                Genericos.gravarLinhaLog(modelo, "[CRIAR_DIRETORIO]" & caminho)
                Genericos.gravarLinhaLog(modelo, ex.Message)
                Throw New Exception("Erro: " & ex.Message)
            End Try

            If Not (modelo = "65") Then
                'Verifica quais arquivos deve salvar
                If DownloadReq.tpDown.ToUpper().Contains("X") Then
                    Dim xml As String = DownloadResp.xml
                    Genericos.salvarXML(xml, caminho, nome)
                End If

                If DownloadReq.tpDown.ToUpper().Contains("P") Then
                    Dim pdf As String = DownloadResp.pdf
                    Genericos.salvarPDF(pdf, caminho, nome)

                    If exibeNaTela Then
                        Process.Start(caminho & nome & ".pdf")
                    End If
                End If
            Else
                Dim xml As String = DownloadRespNFCe.nfeProc.xml
                Genericos.salvarXML(xml, caminho, nome)

                Dim pdf As String = DownloadRespNFCe.pdf
                Genericos.salvarPDF(pdf, caminho, nome)

                If exibeNaTela Then
                    Process.Start(caminho & nome & ".pdf")
                End If
            End If
        Else
            MessageBox.Show("Ocorreu um erro, veja o retorno da API para mais informações")
        End If

        Return resposta
    End Function

    Public Shared Function downloadEvento(ByVal modelo As String, DownloadEventoReq As DownloadEventoReq) As String
        Dim urlDownloadEvento As String

        Select Case modelo
            Case "63"
                urlDownloadEvento = Endpoints.BPeDownloadEvento
            Case "57", "67"
                urlDownloadEvento = Endpoints.CTeDownloadEvento
            Case "58"
                urlDownloadEvento = Endpoints.MDFeDownloadEvento
            Case "65"
                urlDownloadEvento = Endpoints.NFCeDownload
            Case "55"
                urlDownloadEvento = Endpoints.NFeDownloadEvento
            Case Else
                Throw New Exception("Não definido endpoint de download de evento para o modelo " + modelo)
        End Select

        Dim json As String = JsonConvert.SerializeObject(DownloadEventoReq)

        Genericos.gravarLinhaLog(modelo, "[DOWNLOAD_EVENTO_DADOS]")
        Genericos.gravarLinhaLog(modelo, json)

        Dim resposta As String = enviaConteudoParaAPI(json, urlDownloadEvento, "json")

        Dim status As String
        If Not (modelo = "65") Then
            Dim DownloadEventoResp As New DownloadEventoResp
            DownloadEventoResp = JsonConvert.DeserializeObject(Of DownloadEventoResp)(resposta)
            status = DownloadEventoResp.status
        Else
            Dim DownloadRespNFCe As New DownloadRespNFCe
            DownloadRespNFCe = JsonConvert.DeserializeObject(Of DownloadRespNFCe)(resposta)
            status = DownloadRespNFCe.status
        End If

        'O retorno da API será gravado somente em caso de erro, 
        'para não gerar um log extenso com o PDF e XML
        If (status <> "200") And (status <> "100") Then
            Genericos.gravarLinhaLog(modelo, "[DOWNLOAD_EVENTO_RESPOSTA]")
            Genericos.gravarLinhaLog(modelo, resposta)
        Else
            Genericos.gravarLinhaLog(modelo, "[DOWNLOAD_EVENTO_STATUS]")
            Genericos.gravarLinhaLog(modelo, status)
        End If

        Return resposta
    End Function

    Public Shared Function downloadEventoESalvar(ByVal modelo As String, DownloadEventoReq As DownloadEventoReq, ByVal caminho As String, ByVal chave As String, ByVal nSeqEvento As String, ByVal Optional exibeNaTela As Boolean = False) As String
        Dim resposta As String = downloadEvento(modelo, DownloadEventoReq)
        Dim tpEventoSalvar As String = ""
        Dim DownloadEventoResp As New DownloadEventoResp
        Dim DownloadRespNFCe As New DownloadRespNFCe

        Dim status As String
        If Not (modelo = "65") Then
            DownloadEventoResp = JsonConvert.DeserializeObject(Of DownloadEventoResp)(resposta)
            status = DownloadEventoResp.status
        Else
            DownloadRespNFCe = JsonConvert.DeserializeObject(Of DownloadRespNFCe)(resposta)
            status = DownloadRespNFCe.status
        End If

        If (status = "200") OrElse (status = "100") Then
            'Cria o diretório, caso não exista
            Try
                If Not Directory.Exists(caminho) Then Directory.CreateDirectory(caminho)
                If Not caminho.EndsWith("\") Then caminho += "\"
            Catch ex As IOException
                Genericos.gravarLinhaLog(modelo, "[CRIAR_DIRETORIO]" & caminho)
                Genericos.gravarLinhaLog(modelo, ex.Message)
                Throw New Exception("Erro: " & ex.Message)
            End Try

            If Not (modelo = "65") Then
                If DownloadEventoReq.tpEvento.ToUpper().Equals("CANC") Then
                    tpEventoSalvar = "110111"
                ElseIf DownloadEventoReq.tpEvento.ToUpper().Equals("ENC") Then
                    tpEventoSalvar = "110115"
                Else

                    tpEventoSalvar = "110110"
                End If

                'Verifica quais arquivos deve salvar
                If DownloadEventoReq.tpDown.ToUpper().Contains("X") Then
                    Dim xml As String = DownloadEventoResp.xml
                    Genericos.salvarXML(xml, caminho, tpEventoSalvar & chave & nSeqEvento & "-procEven")
                    'ElseIf DownloadReq.tpDown.ToUpper().Contains("J") Then
                    '    Dim json As String = DownloadRet.nfeProc
                    '    Genericos.salvarJSON(Convert.ToString(json), caminho, nome)
                    'End If
                End If

                If DownloadEventoReq.tpDown.ToUpper().Contains("P") Then
                    Dim pdf As String = DownloadEventoResp.pdf
                    If Not (IsNothing(pdf)) Then
                        Genericos.salvarPDF(pdf, caminho, tpEventoSalvar & chave & nSeqEvento & "-procEven")
                        If exibeNaTela Then
                            Process.Start(caminho & tpEventoSalvar & chave & nSeqEvento & "-procEven.pdf")
                        End If
                    End If
                End If
            Else

                Dim xml As String = DownloadRespNFCe.nfeProc.xml
                Genericos.salvarXML(xml, caminho, tpEventoSalvar & chave & nSeqEvento & "-procEven")

                Dim pdf As String = DownloadRespNFCe.pdfCancelamento
                Genericos.salvarPDF(pdf, caminho, tpEventoSalvar & chave & nSeqEvento & "-procEven")

                If exibeNaTela Then
                    Process.Start(caminho & tpEventoSalvar & chave & nSeqEvento & "-procEven.pdf")
                End If
            End If
        Else
            MessageBox.Show("Ocorreu um erro, veja o retorno da API para mais informações")
        End If

        Return resposta
    End Function

    Public Shared Function cancelarDocumento(ByVal modelo As String, CancelarReq As CancelarReq) As String
        Dim urlCancelamento As String

        Select Case modelo
            Case "63"
                urlCancelamento = Endpoints.BPeCancelamento
            Case "57", "67"
                urlCancelamento = Endpoints.CTeCancelamento
            Case "58"
                urlCancelamento = Endpoints.MDFeCancelamento
            Case "65"
                urlCancelamento = Endpoints.NFCeCancelamento
            Case "55"
                urlCancelamento = Endpoints.NFeCancelamento
            Case Else
                Throw New Exception("Não definido endpoint de cancelamento para o modelo " + modelo)
        End Select

        Dim json As String = JsonConvert.SerializeObject(CancelarReq)

        Genericos.gravarLinhaLog(modelo, "[CANCELAMENTO_DADOS]")
        Genericos.gravarLinhaLog(modelo, json)

        Dim resposta As String = enviaConteudoParaAPI(json, urlCancelamento, "json")

        Genericos.gravarLinhaLog(modelo, "[CANCELAMENTO_RESPOSTA]")
        Genericos.gravarLinhaLog(modelo, resposta)

        Return resposta
    End Function

    Public Shared Function cancelarDocumentoESalvar(ByVal modelo As String, CancelarReq As CancelarReq, DownloadEventoReq As DownloadEventoReq, ByVal caminho As String, ByVal chave As String, ByVal exibeNaTela As Boolean) As String
        Dim resposta As String = cancelarDocumento(modelo, CancelarReq)
        Dim CancelarResp As New CancelarResp
        Dim status As String
        Dim cStat As String

        CancelarResp = JsonConvert.DeserializeObject(Of CancelarResp)(resposta)
        status = CancelarResp.status

        If (status = "200") Then
            cStat = CancelarResp.retEvento.cStat
            If (cStat = "135") Then
                Dim respostaDownloadEvento As String = downloadEventoESalvar(modelo, DownloadEventoReq, caminho, chave, "1", exibeNaTela)
            End If
        Else
                MessageBox.Show("Ocorreu um erro ao cancelar, veja o retorno da API para mais informações")
        End If

        Return resposta
    End Function

    Public Shared Function corrigirDocumento(ByVal modelo As String, CorrigirReq As CorrigirReq) As String
        Dim urlCCe As String

        Select Case modelo
            Case "57", "67"
                urlCCe = Endpoints.CTeCCe
            Case "55"
                urlCCe = Endpoints.NFeCCe
            Case Else
                Throw New Exception("Não definido endpoint de carta de correção para o modelo " + modelo)
        End Select

        Dim json As String = JsonConvert.SerializeObject(CorrigirReq)

        Genericos.gravarLinhaLog(modelo, "[CCE_DADOS]")
        Genericos.gravarLinhaLog(modelo, json)

        Dim resposta As String = enviaConteudoParaAPI(json, urlCCe, "json")

        Genericos.gravarLinhaLog(modelo, "[CCE_RESPOSTA]")
        Genericos.gravarLinhaLog(modelo, resposta)

        Return resposta
    End Function

    Public Shared Function corrigirDocumentoESalvar(ByVal modelo As String, CorrigirReq As CorrigirReq, DownloadEventoReq As DownloadEventoReq, ByVal caminho As String, ByVal chave As String, ByVal nSeqEvento As String, ByVal exibeNaTela As Boolean) As String
        Dim resposta As String = corrigirDocumento(modelo, CorrigirReq)
        Dim CorrigirResp As New CorrigirResp
        Dim status As String

        CorrigirResp = JsonConvert.DeserializeObject(Of CorrigirResp)(resposta)
        status = CorrigirResp.status

        If (status = "200") Then
            Dim respostaDownloadEvento As String = downloadEventoESalvar(modelo, DownloadEventoReq, caminho, chave, nSeqEvento, exibeNaTela)
        Else
            MessageBox.Show("Ocorreu um erro ao cancelar, veja o retorno da API para mais informações")
        End If

        Return resposta
    End Function

    Public Shared Function inutilizarNumeracao(ByVal modelo As String, InutilizarReq As InutilizarReq) As String
        Dim urlInutilizacao As String

        Select Case modelo
            Case "57", "67"
                urlInutilizacao = Endpoints.CTeInutilizacao
            Case "65"
                urlInutilizacao = Endpoints.NFCeInutilizacao
            Case "55"
                urlInutilizacao = Endpoints.NFeInutilizacao
            Case Else
                Throw New Exception("Não definido endpoint de inutilização para o modelo " + modelo)
        End Select

        Dim json As String = JsonConvert.SerializeObject(InutilizarReq)

        Genericos.gravarLinhaLog(modelo, "[INUTILIZACAO_DADOS]")
        Genericos.gravarLinhaLog(modelo, json)

        Dim resposta As String = enviaConteudoParaAPI(json, urlInutilizacao, "json")

        Genericos.gravarLinhaLog(modelo, "[INUTILIZACAO_RESPOSTA]")
        Genericos.gravarLinhaLog(modelo, resposta)

        Return resposta
    End Function

    Public Shared Function inutilizarNumeracaoESalvar(ByVal modelo As String, ByVal InutilizarReq As InutilizarReq, ByVal caminho As String) As String
        Dim resposta As String = inutilizarNumeracao(modelo, InutilizarReq)
        Dim status As String
        Dim xml As String = Nothing
        Dim chave As String = Nothing

        Select Case modelo
            Case "57", "67"
                Dim InutilizarRespCTe As InutilizarRespCTe = New InutilizarRespCTe()
                InutilizarRespCTe = JsonConvert.DeserializeObject(Of InutilizarRespCTe)(resposta)
                status = InutilizarRespCTe.status

                If status = "200" Then
                    Dim cStat As String = InutilizarRespCTe.retornoInutCTe.cstat

                    If cStat = "102" Then
                        xml = InutilizarRespCTe.retornoInutCTe.xmlInut
                        chave = InutilizarRespCTe.retornoInutCTe.chave
                    End If
                Else
                    MessageBox.Show("Ocorreu um erro ao inutilizar a numeração, veja o retorno da API para mais informações")
                End If

                Exit Select
            Case "65"
                Dim InutilizarRespNFCe As InutilizarRespNFCe = New InutilizarRespNFCe()
                InutilizarRespNFCe = JsonConvert.DeserializeObject(Of InutilizarRespNFCe)(resposta)
                status = InutilizarRespNFCe.status

                If status = "102" Then
                    Dim cStat As String = InutilizarRespNFCe.retInutNFe.cStat

                    If cStat = "102" Then
                        xml = InutilizarRespNFCe.retInutNFe.xml
                        chave = InutilizarRespNFCe.retInutNFe.chave
                    End If
                Else
                    MessageBox.Show("Ocorreu um erro ao inutilizar a numeração, veja o retorno da API para mais informações")
                End If

                Exit Select
            Case "55"
                Dim InutilizarRespNFe As InutilizarRespNFe = New InutilizarRespNFe()
                InutilizarRespNFe = JsonConvert.DeserializeObject(Of InutilizarRespNFe)(resposta)
                status = InutilizarRespNFe.status

                If status = "200" Then
                    Dim cStat As String = InutilizarRespNFe.retornoInutNFe.cStat

                    If cStat = "102" Then
                        xml = InutilizarRespNFe.retornoInutNFe.xmlInut
                        chave = InutilizarRespNFe.retornoInutNFe.chave
                    End If
                Else
                    MessageBox.Show("Ocorreu um erro ao inutilizar a numeração, veja o retorno da API para mais informações")
                End If

                Exit Select
            Case Else
                Throw New Exception("Não definido endpoint de inutilização para o modelo " & modelo)
        End Select

        If xml IsNot Nothing Then
            If Not Directory.Exists(caminho) Then Directory.CreateDirectory(caminho)
            Genericos.salvarXML(xml, caminho, chave)
        End If

        Return resposta
    End Function

    Public Shared Function consultarCadastroContribuinte(ByVal modelo As String, ConsCadReq As ConsCadReq) As String
        Dim urlConsCad As String

        Select Case modelo
            Case "57", "67"
                urlConsCad = Endpoints.CTeConsCad
            Case "55"
                urlConsCad = Endpoints.NFeConsCad
            Case Else
                Throw New Exception("Não definido endpoint de consulta de cadastro para o modelo " + modelo)
        End Select

        Dim json As String = JsonConvert.SerializeObject(ConsCadReq,
                                                         Newtonsoft.Json.Formatting.None,
                                                         New JsonSerializerSettings With {
                                                            .NullValueHandling = NullValueHandling.Ignore
                                                         })

        Genericos.gravarLinhaLog(modelo, "[CONS_CAD_DADOS]")
        Genericos.gravarLinhaLog(modelo, json)

        Dim resposta As String = enviaConteudoParaAPI(json, urlConsCad, "json")

        Genericos.gravarLinhaLog(modelo, "[CONS_CAD_RESPOSTA]")
        Genericos.gravarLinhaLog(modelo, resposta)

        Return resposta
    End Function

    Public Shared Function consultarSituacaoDocumento(ByVal modelo As String, ConsSitReq As ConsSitReq) As String
        Dim urlConsSit As String

        Select Case modelo
            Case "63"
                urlConsSit = Endpoints.BPeConsSit
            Case "57", "67"
                urlConsSit = Endpoints.CTeConsSit
            Case "58"
                urlConsSit = Endpoints.MDFeConsSit
            Case "65"
                urlConsSit = Endpoints.NFCeConsSit
            Case "55"
                urlConsSit = Endpoints.NFeConsSit
            Case Else
                Throw New Exception("Não definido endpoint de consulta de situação para o modelo " + modelo)
        End Select

        Dim json As String = JsonConvert.SerializeObject(ConsSitReq)

        Genericos.gravarLinhaLog(modelo, "[CONS_SIT_DADOS]")
        Genericos.gravarLinhaLog(modelo, json)

        Dim resposta As String = enviaConteudoParaAPI(json, urlConsSit, "json")

        Genericos.gravarLinhaLog(modelo, "[CONS_SIT_RESPOSTA]")
        Genericos.gravarLinhaLog(modelo, resposta)

        Return resposta
    End Function

    Public Shared Function listarNSNRecs(ByVal modelo As String, ListarNSNRecReq As ListarNSNRecReq) As String
        Dim urlListarNSNRecs As String

        Select Case modelo
            Case "57", "67"
                urlListarNSNRecs = Endpoints.CTeListarNSNRecs
            Case "58"
                urlListarNSNRecs = Endpoints.MDFeListarNSNRecs
            Case "55"
                urlListarNSNRecs = Endpoints.NFeListarNSNRecs
            Case Else
                Throw New Exception("Não definido endpoint de listagem de nsNRec para o modelo " + modelo)
        End Select

        Dim json As String = JsonConvert.SerializeObject(ListarNSNRecReq)

        Genericos.gravarLinhaLog(modelo, "[LISTAR_NSNRECS_DADOS]")
        Genericos.gravarLinhaLog(modelo, json)

        Dim resposta As String = enviaConteudoParaAPI(json, urlListarNSNRecs, "json")

        Genericos.gravarLinhaLog(modelo, "[LISTAR_NSNRECS_RESPOSTA]")
        Genericos.gravarLinhaLog(modelo, resposta)

        Return resposta
    End Function

    Public Shared Function enviarEmailDocumento(ByVal modelo As String, EnviarEmailReq As EnviarEmailReq) As String
        Dim urlEnviarEmail As String

        Select Case modelo
            Case "65"
                urlEnviarEmail = Endpoints.NFCeEnvioEmail
            Case "55"
                urlEnviarEmail = Endpoints.NFeEnvioEmail
            Case Else
                Throw New Exception("Não definido endpoint de envio de e-mail para o modelo " + modelo)
        End Select

        Dim json As String = JsonConvert.SerializeObject(EnviarEmailReq)

        Genericos.gravarLinhaLog(modelo, "[ENVIAR_EMAIL_DADOS]")
        Genericos.gravarLinhaLog(modelo, json)

        Dim resposta As String = enviaConteudoParaAPI(json, urlEnviarEmail, "json")

        Genericos.gravarLinhaLog(modelo, "[ENVIAR_EMAIL_RESPOSTA]")
        Genericos.gravarLinhaLog(modelo, resposta)

        Return resposta
    End Function
End Class
