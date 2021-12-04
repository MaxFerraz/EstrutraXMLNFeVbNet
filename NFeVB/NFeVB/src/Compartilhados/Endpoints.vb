Public Class Endpoints
    'BP-e
    Public Property BPeEnvio As String = "https://bpe.ns.eti.br/v1/bpe/issue"
    Public Property BPeConsStatusProcessamento As String = "https://bpe.ns.eti.br/v1/bpe/issue/status"
    Public Property BPeDownload As String = "https://bpe.ns.eti.br/v1/bpe/get"
    Public Property BPeDownloadEvento As String = "https://bpe.ns.eti.br/v1/bpe/get/event"
    Public Property BPeCancelamento As String = "https://bpe.ns.eti.br/v1/bpe/cancel"
    Public Property BPeNaoEmb As String = "https://bpe.ns.eti.br/v1/bpe/naoemb"
    Public Property BPeConsSit As String = "https://bpe.ns.eti.br/v1/bpe/status"

    'CT-e
    Public Property CTeEnvio As String = "https://cte.ns.eti.br/cte/issue"
    Public Property CTeOSEnvio As String = "https://cte.ns.eti.br/cte/issueos"
    Public Property CTeConsStatusProcessamento As String = "https://cte.ns.eti.br/cte/issueStatus/300"
    Public Property CTeDownload As String = "https://cte.ns.eti.br/cte/get/300"
    Public Property CTeDownloadEvento As String = "https://cte.ns.eti.br/cte/get/event/300"
    Public Property CTeCancelamento As String = "https://cte.ns.eti.br/cte/cancel/300"
    Public Property CTeCCe As String = "https://cte.ns.eti.br/cte/cce/300"
    Public Property CTeConsCad As String = "https://cte.ns.eti.br/util/conscad"
    Public Property CTeConsSit As String = "https://cte.ns.eti.br/cte/stats/300"
    Public Property CTeInfGTV As String = "https://cte.ns.eti.br/cte/gtv"
    Public Property CTeInutilizacao As String = "https://cte.ns.eti.br/cte/inut"
    Public Property CTeListarNSNRecs As String = "https://cte.ns.eti.br/util/list/nsnrecs"

    'DDF-e
    Public Property DDFeDesacordo As String = "https://ddfe.ns.eti.br/events/cte/disagree"
    Public Property DDFeDownloadUnico As String = "https://ddfe.ns.eti.br/dfe/unique"
    Public Property DDFeDownloadLote As String = "https://ddfe.ns.eti.br/dfe/bunch"
    Public Property DDFeManifestacao As String = "https://ddfe.ns.eti.br/events/manif"

    'MDF-e
    Public Property MDFeEnvio As String = "https://mdfe.ns.eti.br/mdfe/issue"
    Public Property MDFeConsStatusProcessamento As String = "https://mdfe.ns.eti.br/mdfe/issue/status"
    Public Property MDFeDownload As String = "https://mdfe.ns.eti.br/mdfe/get"
    Public Property MDFeDownloadEvento As String = "https://mdfe.ns.eti.br/mdfe/get/event"
    Public Property MDFeCancelamento As String = "https://mdfe.ns.eti.br/mdfe/cancel"
    Public Property MDFeEncerramento As String = "https://mdfe.ns.eti.br/mdfe/closure"
    Public Property MDFeIncCondutor As String = "https://mdfe.ns.eti.br/mdfe/adddriver"
    Public Property MDFeConsNaoEncerrados As String = "https://mdfe.ns.eti.br/util/consnotclosed"
    Public Property MDFeConsSit As String = "https://mdfe.ns.eti.br/mdfe/stats"
    Public Property MDFePrevia As String = "https://mdfe.ns.eti.br/util/preview/mdfe"
    Public Property MDFeListarNSNRecs As String = "https://mdfe.ns.eti.br/util/list/nsnrecs"
    Public Property MDFeIncluirDFe As String = "https://mdfe.ns.eti.br/mdfe/adddfe"

    'NFC-e
    Public Property NFCeEnvio As String = "https://nfce.ns.eti.br/v1/nfce/issue"
    Public Property NFCeDownload As String = "https://nfce.ns.eti.br/v1/nfce/get"
    Public Property NFCeCancelamento As String = "https://nfce.ns.eti.br/v1/nfce/cancel"
    Public Property NFCeConsSit As String = "https://nfce.ns.eti.br/v1/nfce/status"
    Public Property NFCeEnvioEmail As String = "https://nfce.ns.eti.br/v1/util/resendemail"
    Public Property NFCeInutilizacao As String = "https://nfce.ns.eti.br/v1/nfce/inut"

    'NF-e
    Public Property NFeEnvio As String = "https://nfe.ns.eti.br/nfe/issue"
    Public Property NFeConsStatusProcessamento As String = "https://nfe.ns.eti.br/nfe/issue/status"
    Public Property NFeDownload As String = "https://nfe.ns.eti.br/nfe/get"
    Public Property NFeDownloadEvento As String = "https://nfe.ns.eti.br/nfe/get/event"
    Public Property NFeCancelamento As String = "https://nfe.ns.eti.br/nfe/cancel"
    Public Property NFeCCe As String = "https://nfe.ns.eti.br/nfe/cce"
    Public Property NFeConsStatusSefaz As String = "https://nfe.ns.eti.br/util/wssefazstatus"
    Public Property NFeConsCad As String = "https://nfe.ns.eti.br/util/conscad"
    Public Property NFeConsSit As String = "https://nfe.ns.eti.br/nfe/stats"
    Public Property NFeEnvioEmail As String = "https://nfe.ns.eti.br/util/resendemail"
    Public Property NFeInutilizacao As String = "https://nfe.ns.eti.br/nfe/inut"
    Public Property NFeListarNSNRecs As String = "https://nfe.ns.eti.br/util/list/nsnrecs"
    Public Property NFePrevia As String = "https://nfe.ns.eti.br/util/preview/nfe"
End Class
