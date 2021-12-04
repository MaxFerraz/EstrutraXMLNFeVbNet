<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCartaCorrecao
    Inherits System.Windows.Forms.Form

    'Descartar substituições de formulário para limpar a lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Exigido pelo Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'OBSERVAÇÃO: o procedimento a seguir é exigido pelo Windows Form Designer
    'Pode ser modificado usando o Windows Form Designer.  
    'Não o modifique usando o editor de códigos.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.button1 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtXcorrecao = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtNseqEvento = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtDHevento = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtTPamb = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtchNFe = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(374, 37)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(89, 13)
        Me.Label9.TabIndex = 9
        Me.Label9.Text = "via MessageBOX"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(374, 8)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(112, 13)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "DADOS DE SAÍDA"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(35, 8)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(134, 13)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "DADOS DE ENTRADA"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.button1)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtXcorrecao)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtNseqEvento)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtDHevento)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtTPamb)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtchNFe)
        Me.Panel1.Location = New System.Drawing.Point(38, 24)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(192, 394)
        Me.Panel1.TabIndex = 6
        '
        'button1
        '
        Me.button1.Location = New System.Drawing.Point(16, 307)
        Me.button1.Name = "button1"
        Me.button1.Size = New System.Drawing.Size(75, 23)
        Me.button1.TabIndex = 12
        Me.button1.Text = "Enviar"
        Me.button1.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 245)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(55, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "xCorrecao"
        '
        'txtXcorrecao
        '
        Me.txtXcorrecao.Location = New System.Drawing.Point(16, 262)
        Me.txtXcorrecao.Name = "txtXcorrecao"
        Me.txtXcorrecao.Size = New System.Drawing.Size(100, 20)
        Me.txtXcorrecao.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 188)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "nSeqEvento"
        '
        'txtNseqEvento
        '
        Me.txtNseqEvento.Location = New System.Drawing.Point(16, 205)
        Me.txtNseqEvento.Name = "txtNseqEvento"
        Me.txtNseqEvento.Size = New System.Drawing.Size(100, 20)
        Me.txtNseqEvento.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 124)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "dhEvento"
        '
        'txtDHevento
        '
        Me.txtDHevento.Location = New System.Drawing.Point(16, 141)
        Me.txtDHevento.Name = "txtDHevento"
        Me.txtDHevento.Size = New System.Drawing.Size(100, 20)
        Me.txtDHevento.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 67)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "tpAmb"
        '
        'txtTPamb
        '
        Me.txtTPamb.Location = New System.Drawing.Point(16, 84)
        Me.txtTPamb.Name = "txtTPamb"
        Me.txtTPamb.Size = New System.Drawing.Size(100, 20)
        Me.txtTPamb.TabIndex = 3
        Me.txtTPamb.Text = "1 ou 2"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "chNFe"
        '
        'txtchNFe
        '
        Me.txtchNFe.Location = New System.Drawing.Point(16, 30)
        Me.txtchNFe.Name = "txtchNFe"
        Me.txtchNFe.Size = New System.Drawing.Size(173, 20)
        Me.txtchNFe.TabIndex = 1
        '
        'frmCartaCorrecao
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(564, 450)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmCartaCorrecao"
        Me.Text = "CartaCorrecao NFe"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents button1 As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents txtXcorrecao As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtNseqEvento As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtDHevento As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtTPamb As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtchNFe As TextBox
End Class
