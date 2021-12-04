Imports NFeVB.src.Classes
Imports NFeVB.src
Imports Newtonsoft.Json

Public Class frmEmissorNFe
    'Dim oDadosEmitente As New DadosNFe.Emitente
    'Dim oDadosDestinatario As New DadosNFe.Destinarario
    'Dim oDadosProdutos As New DadosNFe.DadosProdutos
    'Dim oDadosGerais As New DadosNFe.DadosGerais
    'Dim nproduto = {"Alface", "Agriao", "Rucula"}
    'Dim x As Integer = 0

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'PreencherGrid()
    End Sub

    Private Sub btnEnviarNF_Click(sender As Object, e As EventArgs) Handles btnEnviarNF.Click
        'Lembrando que os dados gerados aqui precisam ser salvos no banco de dados para possiveis necessidade de reprocessar a nota
        'Sendo o ideal depois que o cliente preencher os campos da nota, ele clicar em salvar, gravar no banco e depois gerar este xml com essas mesmas informações
        Dim notaFiscal As nfe_v4_00 = New nfe_v4_00()
        notaFiscal.infNFe = New TNFeInfNFe()
        notaFiscal.infNFe.versao = "4.00"
        notaFiscal.infNFe.ide = New TNFeInfNFeIde()
        'Aqui Codigo UF do emitente, pode vir do cadastro do emitente, aqui eu deixei fixo na variavel
        Dim cUF As Integer = 43
        notaFiscal.infNFe.ide.cUF = CType([Enum].Parse(GetType(TCodUfIBGE), "Item" & cUF), TCodUfIBGE)
        'Aqui Codigo Numerico da NFe, deve ser gerado um numero randomico de 8 caracteres
        notaFiscal.infNFe.ide.cNF = GerarRandomico()
        'Aqui Descrição da natureza de operação da NFe(CFOP)
        notaFiscal.infNFe.ide.natOp = "VENDA A PRAZO - S"
        'Aqui modelo da NFe, por padrão é sempre 55, da pra deixar fixo aqui ou colocar na tabela do emitente da nota.
        Dim modelo As Integer = 55
        notaFiscal.infNFe.ide.[mod] = CType([Enum].Parse(GetType(TMod), "Item" & 55), TMod)
        'Aqui serie da NFe, que pode ir de 0 até 999, essa informação não é bom deixar aberto para o cliente mudar, pode deixar fixo aqui ou colocar na tabela do emitente da nota tbm.
        notaFiscal.infNFe.ide.serie = "6"
        'Aqui é o numero da NFe
        notaFiscal.infNFe.ide.nNF = "150"
        notaFiscal.infNFe.ide.dhEmi = DateTime.Now.ToString("s") & "-03:00"
        notaFiscal.infNFe.ide.dhSaiEnt = ""
        'Aqui é o tipo de Operação 0=Entrada 1=saída, este campo é bom ter um combobox para selecionar as opções
        notaFiscal.infNFe.ide.tpNF = CType([Enum].Parse(GetType(TNFeInfNFeIdeTpNF), "Item" & txtTPnf.Text.Trim), TNFeInfNFeIdeTpNF)

        'Identificador de local de destino da operação - 1=Operação interna; 2=Operação interestadual; 3=Operação com exterior, este campo é bom ter um combobox para selecionar as opções
        notaFiscal.infNFe.ide.idDest = CType([Enum].Parse(GetType(TNFeInfNFeIdeIdDest), "Item" & txtIDdest.Text.Trim), TNFeInfNFeIdeIdDest)
        'Aqui é o codigo do municipio de ocorrência do fato gerado, este dado pode vir da tabela do emitente
        notaFiscal.infNFe.ide.cMunFG = ""
        'Aqui formato de Impressão do DANFE - 1=DANFE normal, Retrato; 2=DANFE normal, Paisagem, este campo é bom ter um combobox para selecionar as opções
        notaFiscal.infNFe.ide.tpImp = CType([Enum].Parse(GetType(TNFeInfNFeIdeTpImp), "Item" & txtTPimp.Text.Trim), TNFeInfNFeIdeTpImp)
        'Aqui é o tipo de emissão, por padrão será usado sempre a opção 1 emissão normal, mas raramente será necessário emitir em contingencia dai sim sendo necessário alterar para 6,7 ou 8, essa informação pode ficar no cadastro do emitente
        notaFiscal.infNFe.ide.tpEmis = CType([Enum].Parse(GetType(TNFeInfNFeIdeTpEmis), "Item" & 1), TNFeInfNFeIdeTpEmis)
        'Aqui é o cDV - Dígito Verificador da Chave de Acesso da NF-e, ele pode ir em branco pois é preenchido pela API da NS
        notaFiscal.infNFe.ide.cDV = ""
        'Aqui é a identificação do ambiente de emissão 1=Produção; 2=Homologação, este campo pode ficar dentro do cadastro do emitente, pois uma vez definido o ambiente ele não altera
        notaFiscal.infNFe.ide.tpAmb = CType([Enum].Parse(GetType(TAmb), "Item" & 2), TAmb)

        'Aqui é o tipo de Emissão da NF-e - 1=NF-e normal; 2=NF-e complementar; 3=NF-e de ajuste; 4=Devolução de mercadoria, este campo é bom ter um combobox para selecionar as opções, mas normalmente é usada a opção 1
        notaFiscal.infNFe.ide.finNFe = CType([Enum].Parse(GetType(TFinNFe), "Item" & txtFinNFe.Text.Trim), TFinNFe)

        'Aqui é o indica operação com Consumidor final - 0=Normal; 1 = Consumidor final, este campo é bom ter um combobox para selecionar as opções, mas normalmente é usada a opção 0
        notaFiscal.infNFe.ide.indFinal = CType([Enum].Parse(GetType(TNFeInfNFeIdeIndFinal), "Item" & txtIndFinal.Text.Trim), TNFeInfNFeIdeIndFinal)

        'Aqui é o tipo de Emissão da NF-e - 0=Não se aplica; 1=Operação presencial; 2=Operação não presencial, pela Internet; 3=Operação não presencial, Teleatendimento; 4=NFC-e em operação com entrega a domicílio; 5=Operação presencial, fora do estabelecimento; 9=Operação não presencial, outros, este campo é bom ter um combobox para selecionar as opções, mas normalmente é usada a opção 1
        notaFiscal.infNFe.ide.indPres = CType([Enum].Parse(GetType(TNFeInfNFeIdeIndPres), "Item" & txtIndPres.Text.Trim), TNFeInfNFeIdeIndPres)
        'Indicador de intermediador/marketplace
        'notaFiscal.infNFe.ide.indIntermed = CType([Enum].Parse(GetType(TNFeInfNFeIdeIndIntermed), "Item" & 0), TNFeInfNFeIdeIndIntermed)
        'Processo de emissão da NF-e, essa informação é sempre usado 0, este campo pode ficar no cadastro do emitente tbm
        notaFiscal.infNFe.ide.procEmi = CType([Enum].Parse(GetType(TProcEmi), "Item" & 0), TProcEmi)
        'DADOS EMITENTE
        notaFiscal.infNFe.emit = New TNFeInfNFeEmit()
        notaFiscal.infNFe.emit.ItemElementName = ItemChoiceType2.CNPJ
        notaFiscal.infNFe.emit.Item = txtEcnpj.Text.Trim
        notaFiscal.infNFe.emit.IE = txtEie.Text.Trim
        notaFiscal.infNFe.emit.xNome = txtEnomeEmpresa.Text.Trim
        notaFiscal.infNFe.emit.xFant = txtEnomeEmpresa.Text.Trim
        'Essa informação é o regime tributario da empresa, se o seu cliente por regime normal será sempre 3, este campo pode ficar dentro do cadastro do emitente tbm
        notaFiscal.infNFe.emit.CRT = CType([Enum].Parse(GetType(TNFeInfNFeEmitCRT), "Item" & 3), TNFeInfNFeEmitCRT)
        'DADOS ENDERECO EMITENTE
        notaFiscal.infNFe.emit.enderEmit = New TEnderEmi()
        notaFiscal.infNFe.emit.enderEmit.xLgr = txtElogradouro.Text.Trim
        notaFiscal.infNFe.emit.enderEmit.nro = txtEnumero.Text.Trim
        notaFiscal.infNFe.emit.enderEmit.xCpl = txtEcomplemento.Text.Trim
        notaFiscal.infNFe.emit.enderEmit.xBairro = txteBairro.Text.Trim
        notaFiscal.infNFe.emit.enderEmit.CEP = txtEcep.Text.Trim
        notaFiscal.infNFe.emit.enderEmit.cMun = txtEcodMunc.Text.Trim
        notaFiscal.infNFe.emit.enderEmit.xMun = txtEmunic.Text.Trim
        notaFiscal.infNFe.emit.enderEmit.UF = CType([Enum].Parse(GetType(TUfEmi), txtEuf.Text.Trim), TUfEmi)
        notaFiscal.infNFe.emit.enderEmit.fone = txtEtel.Text.Trim
        'DADOS DESTINATARIO
        notaFiscal.infNFe.dest = New TNFeInfNFeDest()
        'na variavel cnpj_cpf define se o que esta vindo do banco de dados é um cnpj ou cpf
        Dim cnpj_cpf = txtDcnpj.Text.Trim
        'Remove os espaços em branco
        cnpj_cpf = cnpj_cpf.Trim()
        'Remove a mascara, os pontos e traços
        cnpj_cpf = RemoverMascaraDocumento(cnpj_cpf)

        If cnpj_cpf.Length <> 11 Then
            notaFiscal.infNFe.dest.ItemElementName = ItemChoiceType3.CNPJ
            notaFiscal.infNFe.dest.Item = cnpj_cpf
            'Aqui é o campo Indicador da IE do Destinatário, 1=Contribuinte ICMS (informar a IE do destinatário);, 2=Contribuinte isento de Inscrição no cadastro de Contribuintes, 9=Não Contribuinte, que pode ou não possuir Inscrição, a regra é se o destinatario tiver CNPJ e IE ele é contribuinte de ICMS, opção 1, se ele for CPF e não tiver IE, deve ser usada a opção 9
            notaFiscal.infNFe.dest.indIEDest = TNFeInfNFeDestIndIEDest.Item1
        Else
            notaFiscal.infNFe.dest.ItemElementName = ItemChoiceType3.CPF
            notaFiscal.infNFe.dest.Item = cnpj_cpf
            'Aqui é o campo Indicador da IE do Destinatário, 1=Contribuinte ICMS (informar a IE do destinatário);, 2=Contribuinte isento de Inscrição no cadastro de Contribuintes, 9=Não Contribuinte, que pode ou não possuir Inscrição, a regra é se o destinatario tiver CNPJ e IE ele é contribuinte de ICMS, opção 1, se ele for CPF e não tiver IE, deve ser usada a opção 9
            notaFiscal.infNFe.dest.indIEDest = TNFeInfNFeDestIndIEDest.Item9
        End If
        notaFiscal.infNFe.dest.IE = txtDie.Text.Trim
        notaFiscal.infNFe.dest.xNome = txtDnomeEmpresa.Text.Trim
        'DADOS ENDERECO DESTINATARIO
        notaFiscal.infNFe.dest.enderDest = New TEndereco()
        notaFiscal.infNFe.dest.enderDest.xLgr = txtDlogradouro.Text.Trim
        notaFiscal.infNFe.dest.enderDest.nro = txtDnumero.Text.Trim
        notaFiscal.infNFe.dest.enderDest.xCpl = txtDcomplemento.Text.Trim
        notaFiscal.infNFe.dest.enderDest.xBairro = txtdBairro.Text.Trim
        notaFiscal.infNFe.dest.enderDest.cMun = txtDcodMunic.Text.Trim
        notaFiscal.infNFe.dest.enderDest.xMun = txtDmunic.Text.Trim
        notaFiscal.infNFe.dest.enderDest.UF = CType([Enum].Parse(GetType(TUf), txtDuf.Text.Trim), TUf)
        notaFiscal.infNFe.dest.enderDest.CEP = txtDcep.Text.Trim
        notaFiscal.infNFe.dest.enderDest.fone = txtDtel.Text.Trim
        notaFiscal.infNFe.dest.enderDest.cPais = txtCpais.Text.Trim
        notaFiscal.infNFe.dest.enderDest.xPais = txtNpais.Text.Trim

        'ITENS DA NOTA
        notaFiscal.infNFe.det = New TNFeInfNFeDet(GridProdutos.Rows.Count()) {}

        Dim indice As Integer = 0

        For Each produto As DataGridViewRow In GridProdutos.Rows
            If Not produto.IsNewRow Then
                notaFiscal.infNFe.det(indice) = New TNFeInfNFeDet()
                notaFiscal.infNFe.det(indice).nItem = indice + 1
                notaFiscal.infNFe.det(indice).prod = New TNFeInfNFeDetProd()
                notaFiscal.infNFe.det(indice).prod.cProd = produto.Cells("cProd").Value.ToString()
                notaFiscal.infNFe.det(indice).prod.xProd = produto.Cells("xProd").Value.ToString()
                notaFiscal.infNFe.det(indice).prod.cEAN = produto.Cells("cEAN").Value.ToString()
                notaFiscal.infNFe.det(indice).prod.NCM = produto.Cells("NCM").Value.ToString()
                notaFiscal.infNFe.det(indice).prod.CEST = produto.Cells("CEST").Value.ToString()
                notaFiscal.infNFe.det(indice).prod.CFOP = produto.Cells("CFOP").Value.ToString()
                notaFiscal.infNFe.det(indice).prod.uCom = produto.Cells("uCom").Value.ToString()
                notaFiscal.infNFe.det(indice).prod.qCom = produto.Cells("qCom").Value.ToString()
                notaFiscal.infNFe.det(indice).prod.vUnCom = produto.Cells("vUnCom").Value.ToString()
                notaFiscal.infNFe.det(indice).prod.vProd = produto.Cells("vProd").Value.ToString()
                notaFiscal.infNFe.det(indice).prod.vFrete = produto.Cells("vFrete").Value.ToString()
                notaFiscal.infNFe.det(indice).prod.vSeg = produto.Cells("vSeg").Value.ToString()
                notaFiscal.infNFe.det(indice).prod.vDesc = produto.Cells("vDesc").Value.ToString()
                notaFiscal.infNFe.det(indice).prod.vOutro = produto.Cells("vOutro").Value.ToString()
                notaFiscal.infNFe.det(indice).prod.indTot = produto.Cells("indTot").Value.ToString()
                'notaFiscal.infNFe.det(indice).prod.nItemPed = produto.Cells(15).Value.ToString()
                notaFiscal.infNFe.det(indice).prod.cEANTrib = produto.Cells("cEAN").Value.ToString()
                notaFiscal.infNFe.det(indice).prod.uTrib = produto.Cells("uCom").Value.ToString()
                notaFiscal.infNFe.det(indice).prod.qTrib = produto.Cells("qCom").Value.ToString()
                notaFiscal.infNFe.det(indice).prod.vUnTrib = produto.Cells("vUnCom").Value.ToString()
                'DADOS DO IMPOSTO
                notaFiscal.infNFe.det(indice).imposto = New TNFeInfNFeDetImposto()
                notaFiscal.infNFe.det(indice).imposto.vTotTrib = "0.00"
                Dim impICMS = New TNFeInfNFeDetImpostoICMS()

                'DADOS DO ICMS
                notaFiscal.infNFe.det(indice).imposto.Items = New TNFeInfNFeDetImpostoICMS(0) {}

                'Aqui iremos definir o tipo de ICMS que será usado na tributação do producao, dependendo do tipo do ICMS informado pode haver calculo ou não. Aqui iremos deixar o CST 40 que não precisa calcular nada e tbm o ICMS CST 20 que necessita realizar calculo do icms, informar percentual de base de calculo e percentual de redução da base de calculo
                Dim tipo_de_CSTICMS As Integer = produto.Cells("CST_ICMS").Value.ToString()
                If Not String.IsNullOrWhiteSpace(tipo_de_CSTICMS) Then
                    Select Case tipo_de_CSTICMS
                        Case 40, 41, 50
                            Dim icms40 = New TNFeInfNFeDetImpostoICMSICMS40()
                            icms40.CST = CType([Enum].Parse(GetType(TNFeInfNFeDetImpostoICMSICMS40CST), "Item" & produto.Cells("CST_ICMS").Value.ToString()), TNFeInfNFeDetImpostoICMSICMS40CST)
                            icms40.orig = CType([Enum].Parse(GetType(Torig), "Item" & produto.Cells("Orig").Value.ToString()), Torig)
                            'icms40.vICMSDeson = "0.00"
                            'icms40.motDesICMS = "1"
                            impICMS.Item = icms40
                        Case 20
                            Dim icms20 = New TNFeInfNFeDetImpostoICMSICMS20()
                            icms20.CST = CType([Enum].Parse(GetType(TNFeInfNFeDetImpostoICMSICMS20CST), "Item" & produto.Cells("CST_ICMS").Value.ToString()), TNFeInfNFeDetImpostoICMSICMS20CST)
                            icms20.orig = CType([Enum].Parse(GetType(Torig), "Item" & produto.Cells("Orig").Value.ToString()), Torig)

                            'a modBC é importante olhar no manual para verificar o que é este campo
                            'a modBC tem essas três opções: 0=Margem Valor Agregado (%);, 1=Pauta (Valor);, 2=Preço Tabelado Máx. (valor);, 3=Valor da operação
                            'vou deixar o valor fixo, mas este deve vir da configuração usada na tela ou do cadastro de produtos
                            icms20.modBC = produto.Cells("modBCICMS").Value.ToString()

                            'a pRedBC é importante olhar no manual para verificar o que é este campo
                            'a pRedBC é o percentual de redução da BC de calculo do ICMS, este percentual a contabilidade sabe informar qunado necessário
                            icms20.pRedBC = Double.Parse(produto.Cells("pRedBC").Value.ToString())

                            'a vBC é importante olhar no manual para verificar o que é este campo
                            'a base de calculo usada para calcular o ICMS normalmente é o valor do Produto
                            'Vou deixar fixo este valor aqui, mas essa informação
                            icms20.vBC = produto.Cells("vProd").Value.ToString()
                            icms20.pICMS = produto.Cells("pICMS").Value.ToString()

                            'Aqui para calcular o valor do ICMS você tem duas opções, ou já adiciona no gridVie o ICMS Calculado ou retira o campo do valor e deixa para calcular aqui na montagem do XML
                            'Exemplo do calculo, vICMS = (icms20.vBC) * (1-(pRedBC/100))
                            icms20.vICMS = Double.Parse(produto.Cells("vICMS").Value.ToString())
                            'icms20.vICMSDeson = "0.00"
                            'icms40.motDesICMS = "1"
                            impICMS.Item = icms20
                    End Select
                End If

                notaFiscal.infNFe.det(indice).imposto.Items(0) = impICMS

                'DADOS DO PIS
                Dim tipo_de_CSTPIS As Integer = produto.Cells("CST_PIS").Value.ToString()
                If Not String.IsNullOrWhiteSpace(tipo_de_CSTPIS) Then
                    Select Case tipo_de_CSTPIS
                        Case 1, 2
                            notaFiscal.infNFe.det(indice).imposto.PIS = New TNFeInfNFeDetImpostoPIS()
                            Dim impPIS = New TNFeInfNFeDetImpostoPIS()
                            Dim pisAliq = New TNFeInfNFeDetImpostoPISPISAliq()
                            pisAliq.CST = TNFeInfNFeDetImpostoPISPISAliqCST.Item01
                            pisAliq.vBC = "0.00"
                            pisAliq.pPIS = "0.00"
                            pisAliq.vPIS = "0.00"
                            impPIS.Item = pisAliq
                            notaFiscal.infNFe.det(indice).imposto.PIS = impPIS
                        Case 4, 5, 6, 7, 8, 9, 49, 50, 51, 52, 53, 54, 55, 56, 60, 61, 62, 63, 64, 65, 66, 67, 70, 71, 72, 73, 74, 98, 99
                            notaFiscal.infNFe.det(indice).imposto.PIS = New TNFeInfNFeDetImpostoPIS()
                            Dim impPIS = New TNFeInfNFeDetImpostoPIS()
                            Dim pisNT = New TNFeInfNFeDetImpostoPISPISNT
                            pisNT.CST = TNFeInfNFeDetImpostoPISPISAliqCST.Item01
                            impPIS.Item = pisNT
                            notaFiscal.infNFe.det(indice).imposto.PIS = impPIS

                    End Select
                End If


                'DADOS DO COFINS
                Dim tipo_de_CSTCOFINS As Integer = produto.Cells("CST_COFINS").Value.ToString()
                If Not String.IsNullOrWhiteSpace(tipo_de_CSTCOFINS) Then
                    Select Case tipo_de_CSTCOFINS
                        Case 1, 2
                            notaFiscal.infNFe.det(indice).imposto.COFINS = New TNFeInfNFeDetImpostoCOFINS()
                            Dim impCOFINS = New TNFeInfNFeDetImpostoCOFINS()
                            Dim cofinsAliq = New TNFeInfNFeDetImpostoCOFINSCOFINSAliq()
                            cofinsAliq.CST = TNFeInfNFeDetImpostoCOFINSCOFINSAliqCST.Item01
                            cofinsAliq.vBC = "0.00"
                            cofinsAliq.pCOFINS = "0.00"
                            cofinsAliq.vCOFINS = "0.00"
                            impCOFINS.Item = cofinsAliq
                            notaFiscal.infNFe.det(indice).imposto.COFINS = impCOFINS
                        Case 4, 5, 6, 7, 8, 9, 49, 50, 51, 52, 53, 54, 55, 56, 60, 61, 62, 63, 64, 65, 66, 67, 70, 71, 72, 73, 74, 98, 99
                            notaFiscal.infNFe.det(indice).imposto.COFINS = New TNFeInfNFeDetImpostoCOFINS()
                            Dim impCOFINS = New TNFeInfNFeDetImpostoCOFINS()
                            Dim cofinsAliq = New TNFeInfNFeDetImpostoCOFINSCOFINSNT()
                            cofinsAliq.CST = TNFeInfNFeDetImpostoCOFINSCOFINSNTCST.Item04
                            impCOFINS.Item = cofinsAliq
                            notaFiscal.infNFe.det(indice).imposto.COFINS = impCOFINS
                    End Select
                End If


                indice += 1
            End If
        Next

        'DADOS TOTAIS DA NFE
        notaFiscal.infNFe.total = New TNFeInfNFeTotal()
        notaFiscal.infNFe.total.ICMSTot = New TNFeInfNFeTotalICMSTot()
        'Passando Double.Parce(txtCampoForm.Text) será preenchido com o que vai ser posto no form
        If Not String.IsNullOrWhiteSpace(txtBCicms.Text) Then
            notaFiscal.infNFe.total.ICMSTot.vBC = Double.Parse(txtBCicms.Text).ToString("0.00")
        Else
            notaFiscal.infNFe.total.ICMSTot.vBC = "0.00"
        End If
        notaFiscal.infNFe.total.ICMSTot.vICMS = Double.Parse(txtTicms.Text).ToString("0.00")
        notaFiscal.infNFe.total.ICMSTot.vBCST = Double.Parse(txtBCicmsSubst.Text).ToString("0.00")
        notaFiscal.infNFe.total.ICMSTot.vST = Double.Parse(txtVicms.Text).ToString("0.00")
        notaFiscal.infNFe.total.ICMSTot.vProd = Double.Parse(txtTotalProdutos.Text).ToString("0.00")
        notaFiscal.infNFe.total.ICMSTot.vFrete = Double.Parse(txtVfrete.Text).ToString("0.00")
        notaFiscal.infNFe.total.ICMSTot.vSeg = Double.Parse(txtVseguro.Text).ToString("0.00")
        notaFiscal.infNFe.total.ICMSTot.vDesc = Double.Parse("0.00").ToString("0.00")
        notaFiscal.infNFe.total.ICMSTot.vII = Double.Parse("0.00").ToString("0.00")
        notaFiscal.infNFe.total.ICMSTot.vIPI = Double.Parse(txtVTipi.Text).ToString("0.00")
        notaFiscal.infNFe.total.ICMSTot.vPIS = Double.Parse("0.00").ToString("0.00")
        notaFiscal.infNFe.total.ICMSTot.vCOFINS = Double.Parse("0.00").ToString("0.00")
        notaFiscal.infNFe.total.ICMSTot.vOutro = Double.Parse("0.00").ToString("0.00")
        notaFiscal.infNFe.total.ICMSTot.vNF = Double.Parse(txtVTnota.Text).ToString("0.00")
        notaFiscal.infNFe.total.ISSQNtot = New TNFeInfNFeTotalISSQNtot()

        'DADOS DO TRANSPORTADOR
        notaFiscal.infNFe.transp = New TNFeInfNFeTransp()
        Dim modFrete = "0"

        Select Case txtModFrete.Text
            Case "0"
                notaFiscal.infNFe.transp.modFrete = TNFeInfNFeTranspModFrete.Item0
            Case "1"
                notaFiscal.infNFe.transp.modFrete = TNFeInfNFeTranspModFrete.Item1
            Case "2"
                notaFiscal.infNFe.transp.modFrete = TNFeInfNFeTranspModFrete.Item2
            Case "9"
                notaFiscal.infNFe.transp.modFrete = TNFeInfNFeTranspModFrete.Item9
        End Select

        If chkFrete.Checked = True Then
            notaFiscal.infNFe.transp.transporta = New TNFeInfNFeTranspTransporta()
            Dim transCnpj = RemoverMascaraDocumento(txtTcnpj.Text)

            If Not String.IsNullOrWhiteSpace(transCnpj) Then
                notaFiscal.infNFe.transp.transporta.ItemElementName = ItemChoiceType6.CNPJ
                notaFiscal.infNFe.transp.transporta.Item = txtTcnpj.Text
            End If

            notaFiscal.infNFe.transp.transporta.IE = txtTie.Text
            notaFiscal.infNFe.transp.transporta.xNome = txtNomeTransp.Text
            notaFiscal.infNFe.transp.transporta.xEnder = txtTendereco.Text
            notaFiscal.infNFe.transp.transporta.xMun = txtTmunic.Text
            notaFiscal.infNFe.transp.transporta.UF = txtTuf.Text
        End If

        'DADOS DO PAGAMENTO
        notaFiscal.infNFe.pag = New TNFeInfNFePag()
        notaFiscal.infNFe.pag.vTroco = Double.Parse("0.00").ToString("0.00")

        notaFiscal.infNFe.pag.detPag = New TNFeInfNFePagDetPag(1) {}

        notaFiscal.infNFe.pag.detPag(0) = New TNFeInfNFePagDetPag()
        notaFiscal.infNFe.pag.detPag(0).indPag = "0"
        notaFiscal.infNFe.pag.detPag(0).tPag = "01"
        notaFiscal.infNFe.pag.detPag(0).vPag = txtVTnota.Text


        notaFiscal.infNFe.infAdic = New TNFeInfNFeInfAdic()
        notaFiscal.infNFe.infAdic.infAdFisco = "DESCRICAO PADRAO DA SEFAZ"

        'Console.WriteLine(notaFiscal.ToXMLString())
        'Console.ReadLine()

        Dim tpDown As String = "XP" 'Aqui é o tipo de download solicitado na API da NS, XP = XML+PDF
        Dim tpAmb As String = "2" 'Aqui é o ambiente da sefaz que esta sendo processado o documento, essa informação pode vir do cadastro de emitente
        Dim caminhoPC As String = "C:\NotasEmitidas\NFe" 'Aqui é o caminho onde deseja salvar o xml e o pdf, pode ser passado qualquer caminho
        Dim exibirPDFNaTela As Boolean = True
        Dim respostaAPI As String
        respostaAPI = NFeVB.NSSuite.emitirNFeSincrono(notaFiscal.ToXMLString(), "xml", txtEcnpj.Text.Trim, tpDown, tpAmb, caminhoPC, exibirPDFNaTela)
        Dim respostaJson = JsonConvert.DeserializeObject(Of EmitirSincronoRetNFe)(respostaAPI)
        'Aqui são as informações do retorno da emissão, sempre que o retorno for de Autorizado (cStat=100) é importante salvar estes dados no banco de dados do parceiro
        Dim cStat = respostaJson.cStat
        Dim chNFe = respostaJson.chNFe
        Dim nProt = respostaJson.nProt
        Dim motivo = respostaJson.motivo
        Dim nsNRec = respostaJson.nsNRec
        Dim status = respostaJson.statusEnvio
        If (cStat = "100") Then
            MsgBox(motivo)
        Else
            MsgBox(respostaAPI)
        End If

    End Sub

    Public Function RemoverMascaraDocumento(texto As String)
        Return texto.Trim().Replace(".", String.Empty).Replace("-", String.Empty).Replace("/", String.Empty)
    End Function

    Public Function GerarRandomico()
        Dim number As String = ""
        Dim random As New Random()
        Dim n As Integer = random.[Next](0, 100000)
        number += n.ToString("D7")
        Return number

    End Function
    'Sub PreencherGrid()
    '    GridProdutos.Rows.Add(2)
    '    Do While x < 3
    '        PreencherOprodutos()
    '        GridProdutos.Rows(x).Cells("cProd").Value = oDadosProdutos.cProd.ToString
    '        GridProdutos.Rows(x).Cells("xProd").Value = oDadosProdutos.xProd.ToString
    '        GridProdutos.Rows(x).Cells("NCM").Value = oDadosProdutos.NCM.ToString
    '        GridProdutos.Rows(x).Cells("CEST").Value = oDadosProdutos.CEST.ToString
    '        GridProdutos.Rows(x).Cells("CFOP").Value = oDadosProdutos.CFOP.ToString
    '        GridProdutos.Rows(x).Cells("uCom").Value = oDadosProdutos.uCom.ToString
    '        GridProdutos.Rows(x).Cells("qCom").Value = oDadosProdutos.qCom.ToString
    '        GridProdutos.Rows(x).Cells("vUnCom").Value = oDadosProdutos.vUnCom.ToString
    '        GridProdutos.Rows(x).Cells("vProd").Value = oDadosProdutos.vProd.ToString
    '        GridProdutos.Rows(x).Cells("uTrib").Value = oDadosProdutos.uTrib.ToString
    '        GridProdutos.Rows(x).Cells("vUnTrib").Value = oDadosProdutos.vUnTrib.ToString
    '        GridProdutos.Rows(x).Cells("indTot").Value = oDadosProdutos.indTot.ToString
    '        GridProdutos.Rows(x).Cells("nItemPed").Value = oDadosProdutos.nItemPed.ToString
    '        GridProdutos.Rows(x).Cells("vICMS").Value = oDadosProdutos.vICMS.ToString
    '        GridProdutos.Rows(x).Cells("vIPI").Value = oDadosProdutos.vIPI.ToString
    '        GridProdutos.Rows(x).Cells("vPIS").Value = oDadosProdutos.vPIS.ToString
    '        GridProdutos.Rows(x).Cells("vCOFINS").Value = oDadosProdutos.vCOFINS.ToString
    '        GridProdutos.Rows(x).Cells("pICMS").Value = oDadosProdutos.pICMS.ToString
    '        GridProdutos.Rows(x).Cells("pIPI").Value = oDadosProdutos.pIPI.ToString
    '        GridProdutos.Rows(x).Cells("pPIS").Value = oDadosProdutos.pPIS.ToString
    '        GridProdutos.Rows(x).Cells("pCOFINS").Value = oDadosProdutos.pCOFINS.ToString
    '        x = x + 1
    '    Loop
    'End Sub

    'Sub PreencherOprodutos()
    '    Try
    '        oDadosProdutos.cProd = x + 1
    '        oDadosProdutos.xProd = nproduto(x).ToString
    '        oDadosProdutos.NCM = 709400
    '        oDadosProdutos.CEST = 40
    '        oDadosProdutos.CFOP = 5101
    '        oDadosProdutos.uCom = "não se aplica"
    '        oDadosProdutos.qCom = "não se aplica"
    '        oDadosProdutos.vUnCom = "não se aplica"
    '        oDadosProdutos.vProd = Val(oDadosProdutos.vProd) + 1
    '        oDadosProdutos.uTrib = "UN"
    '        oDadosProdutos.vUnTrib = "Nao sei"
    '        oDadosProdutos.indTot = Val(oDadosProdutos.vProd.ToString) * 10
    '        oDadosProdutos.nItemPed = 10
    '        oDadosProdutos.vICMS = 0
    '        oDadosProdutos.vIPI = 0
    '        oDadosProdutos.vPIS = 0
    '        oDadosProdutos.vCOFINS = 0
    '        oDadosProdutos.pICMS = "0%"
    '        oDadosProdutos.pIPI = "0%"
    '        oDadosProdutos.pPIS = "0%"
    '        oDadosProdutos.pCOFINS = "0%"

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try

    'End Sub
    'Sub PreencherOemitente()
    '    oDadosEmitente.eNomeEmpresa = txtEnomeEmpresa.Text
    '    oDadosEmitente.eCnpj = txtEcnpj.Text
    '    oDadosEmitente.eCep = txtEcep.Text
    '    oDadosEmitente.eLogradouro = txtElogradouro.Text
    '    oDadosEmitente.eNumero = txtEnumero.Text
    '    oDadosEmitente.eComplemento = txtEcomplemento.Text
    '    oDadosEmitente.eBairro = txteBairro.Text
    '    oDadosEmitente.eMunic = txtEmunic.Text
    '    oDadosEmitente.eCodMunic = txtEcodMunc.Text
    '    oDadosEmitente.eUF = txtEuf.Text
    '    oDadosEmitente.eTel = txtEtel.Text
    '    oDadosEmitente.eIE = txtEie.Text
    '    oDadosEmitente.eEmail = txtEemail.Text

    'End Sub
    'Sub PreencherOdestinatario()
    '    oDadosDestinatario.dnomeempresa = txtDnomeEmpresa.Text
    '    oDadosDestinatario.dcnpj = txtDcnpj.Text
    '    oDadosDestinatario.dcep = txtDcep.Text
    '    oDadosDestinatario.dlogradouro = txtDlogradouro.Text
    '    oDadosDestinatario.dnumero = txtDnumero.Text
    '    oDadosDestinatario.dcomplemento = txtDcomplemento.Text
    '    oDadosDestinatario.dBairro = txtdBairro.Text
    '    oDadosDestinatario.dmunic = txtDmunic.Text
    '    oDadosDestinatario.dcodmunic = txtDcodMunic.Text
    '    oDadosDestinatario.duf = txtDuf.Text
    '    oDadosDestinatario.dtel = txtDtel.Text
    '    oDadosDestinatario.npais = txtNpais.Text
    '    oDadosDestinatario.cpais = txtCpais.Text
    '    oDadosDestinatario.indedest = txtIndIEdest.Text
    '    oDadosDestinatario.die = txtDie.Text
    '    oDadosDestinatario.demail = txtDemail.Text

    'End Sub
    'Sub PreencherOdadosGerais()
    '    oDadosGerais.cnf = txtCnf.Text
    '    oDadosGerais.natop = txtNatOP.Text
    '    oDadosGerais.xMod = txtxMod.Text
    '    oDadosGerais.serie = txtSerie.Text
    '    oDadosGerais.nnf = txtNnf.Text
    '    oDadosGerais.dhemissao = txtDHemissao.Text
    '    oDadosGerais.tpnf = txtTPnf.Text
    '    oDadosGerais.iddest = txtIDdest.Text
    '    oDadosGerais.cmunfg = txtCmunFG.Text
    '    oDadosGerais.tpimp = txtTPimp.Text
    '    oDadosGerais.tpemis = txtTPemis.Text
    '    oDadosGerais.cdv = txtCdv.Text
    '    oDadosGerais.tpamb = txtTPamb.Text
    '    oDadosGerais.finnfe = txtFinNFe.Text
    '    oDadosGerais.indfinal = txtIndFinal.Text
    '    oDadosGerais.indpres = txtIndPres.Text
    '    oDadosGerais.procemi = txtProcEmi.Text
    '    oDadosGerais.verproc = txtVerProc.Text
    'End Sub

    Private Sub btnCancelarNF_Click(sender As Object, e As EventArgs) Handles btnCancelarNF.Click

        frmCancelarNFe.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        frmCartaCorrecao.Show()
    End Sub

    Private Sub btnConsultarNF_Click(sender As Object, e As EventArgs) Handles btnConsultarNF.Click
        frmConsultarSituacao.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        frmDownloadEvento.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        frmInutilizaNumNFe.Show()
    End Sub

    Private Sub Label25_Click(sender As Object, e As EventArgs) Handles Label25.Click

    End Sub

    Private Sub txtTPnf_TextChanged(sender As Object, e As EventArgs) Handles txtTPnf.TextChanged

    End Sub

    Private Sub txtVerProc_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label38_Click(sender As Object, e As EventArgs)

    End Sub
End Class
