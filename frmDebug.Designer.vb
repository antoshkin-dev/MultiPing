<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDebug
    Inherits System.Windows.Forms.Form

    'Форма переопределяет dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.tbxIP = New System.Windows.Forms.TextBox()
        Me.tbxUser = New System.Windows.Forms.TextBox()
        Me.tbxConnectionType = New System.Windows.Forms.TextBox()
        Me.tbxEndpointName = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button1.Location = New System.Drawing.Point(78, 116)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Connect"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'tbxIP
        '
        Me.tbxIP.Location = New System.Drawing.Point(12, 64)
        Me.tbxIP.Name = "tbxIP"
        Me.tbxIP.Size = New System.Drawing.Size(211, 20)
        Me.tbxIP.TabIndex = 1
        Me.tbxIP.Text = "10.22.0.18"
        Me.tbxIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbxUser
        '
        Me.tbxUser.Location = New System.Drawing.Point(12, 90)
        Me.tbxUser.Name = "tbxUser"
        Me.tbxUser.Size = New System.Drawing.Size(211, 20)
        Me.tbxUser.TabIndex = 1
        Me.tbxUser.Text = "a.vanichkin_adm@megacenter.lan"
        Me.tbxUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbxConnectionType
        '
        Me.tbxConnectionType.Location = New System.Drawing.Point(12, 38)
        Me.tbxConnectionType.Name = "tbxConnectionType"
        Me.tbxConnectionType.Size = New System.Drawing.Size(211, 20)
        Me.tbxConnectionType.TabIndex = 1
        Me.tbxConnectionType.Text = "rdp"
        Me.tbxConnectionType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbxEndpointName
        '
        Me.tbxEndpointName.Location = New System.Drawing.Point(11, 12)
        Me.tbxEndpointName.Name = "tbxEndpointName"
        Me.tbxEndpointName.Size = New System.Drawing.Size(212, 20)
        Me.tbxEndpointName.TabIndex = 1
        Me.tbxEndpointName.Text = "mcm-vsrv-sqlsec"
        Me.tbxEndpointName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'frmDebug
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(234, 145)
        Me.Controls.Add(Me.tbxUser)
        Me.Controls.Add(Me.tbxEndpointName)
        Me.Controls.Add(Me.tbxConnectionType)
        Me.Controls.Add(Me.tbxIP)
        Me.Controls.Add(Me.Button1)
        Me.Name = "frmDebug"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmDebug"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents tbxIP As TextBox
    Friend WithEvents tbxUser As TextBox
    Friend WithEvents tbxConnectionType As TextBox
    Friend WithEvents tbxEndpointName As TextBox
End Class
