<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWebBrowser
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.myWebBrowser = New System.Windows.Forms.WebBrowser()
        Me.SuspendLayout()
        '
        'myWebBrowser
        '
        Me.myWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.myWebBrowser.Location = New System.Drawing.Point(0, 0)
        Me.myWebBrowser.MinimumSize = New System.Drawing.Size(20, 20)
        Me.myWebBrowser.Name = "myWebBrowser"
        Me.myWebBrowser.Size = New System.Drawing.Size(184, 178)
        Me.myWebBrowser.TabIndex = 0
        '
        'frmWebBrowser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(184, 178)
        Me.Controls.Add(Me.myWebBrowser)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "frmWebBrowser"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "HTTP viewer"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents myWebBrowser As System.Windows.Forms.WebBrowser
End Class
