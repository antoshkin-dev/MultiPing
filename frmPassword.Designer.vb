<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPassword
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPassword))
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbxPassName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbxDescription = New System.Windows.Forms.TextBox()
        Me.tbxUser = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tbxPassword = New System.Windows.Forms.TextBox()
        Me.btnShowPassword = New System.Windows.Forms.Button()
        Me.btnCopyPassword = New System.Windows.Forms.Button()
        Me.tmrClipClear = New System.Windows.Forms.Timer(Me.components)
        Me.lblReminder = New System.Windows.Forms.Label()
        Me.pnlPIN = New System.Windows.Forms.Panel()
        Me.tbxPinCode = New System.Windows.Forms.TextBox()
        Me.lblEnterPin = New System.Windows.Forms.Label()
        Me.pnlPIN.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Location = New System.Drawing.Point(272, 194)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(83, 27)
        Me.btnClose.TabIndex = 10
        Me.btnClose.Text = "Закрыть"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(144, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Название учётной записи: "
        '
        'tbxPassName
        '
        Me.tbxPassName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbxPassName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbxPassName.Location = New System.Drawing.Point(15, 25)
        Me.tbxPassName.Name = "tbxPassName"
        Me.tbxPassName.ReadOnly = True
        Me.tbxPassName.Size = New System.Drawing.Size(340, 20)
        Me.tbxPassName.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(141, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Описание учётной записи:"
        '
        'tbxDescription
        '
        Me.tbxDescription.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbxDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbxDescription.Location = New System.Drawing.Point(15, 64)
        Me.tbxDescription.Multiline = True
        Me.tbxDescription.Name = "tbxDescription"
        Me.tbxDescription.ReadOnly = True
        Me.tbxDescription.Size = New System.Drawing.Size(340, 36)
        Me.tbxDescription.TabIndex = 3
        '
        'tbxUser
        '
        Me.tbxUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbxUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbxUser.Location = New System.Drawing.Point(15, 119)
        Me.tbxUser.Name = "tbxUser"
        Me.tbxUser.ReadOnly = True
        Me.tbxUser.Size = New System.Drawing.Size(340, 20)
        Me.tbxUser.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 142)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Пароль:"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 103)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(83, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Пользователь:"
        '
        'tbxPassword
        '
        Me.tbxPassword.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbxPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbxPassword.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.tbxPassword.Location = New System.Drawing.Point(15, 158)
        Me.tbxPassword.Name = "tbxPassword"
        Me.tbxPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tbxPassword.ReadOnly = True
        Me.tbxPassword.Size = New System.Drawing.Size(286, 26)
        Me.tbxPassword.TabIndex = 7
        Me.tbxPassword.Text = "showed password"
        '
        'btnShowPassword
        '
        Me.btnShowPassword.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnShowPassword.Image = Global.MultiPing.My.Resources.Resources.eye
        Me.btnShowPassword.Location = New System.Drawing.Point(307, 159)
        Me.btnShowPassword.Name = "btnShowPassword"
        Me.btnShowPassword.Size = New System.Drawing.Size(24, 24)
        Me.btnShowPassword.TabIndex = 8
        Me.btnShowPassword.UseVisualStyleBackColor = True
        '
        'btnCopyPassword
        '
        Me.btnCopyPassword.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCopyPassword.Image = Global.MultiPing.My.Resources.Resources.page_copy
        Me.btnCopyPassword.Location = New System.Drawing.Point(331, 159)
        Me.btnCopyPassword.Name = "btnCopyPassword"
        Me.btnCopyPassword.Size = New System.Drawing.Size(24, 24)
        Me.btnCopyPassword.TabIndex = 9
        Me.btnCopyPassword.UseVisualStyleBackColor = True
        '
        'tmrClipClear
        '
        Me.tmrClipClear.Interval = 1000
        '
        'lblReminder
        '
        Me.lblReminder.Location = New System.Drawing.Point(12, 194)
        Me.lblReminder.Name = "lblReminder"
        Me.lblReminder.Size = New System.Drawing.Size(252, 38)
        Me.lblReminder.TabIndex = 11
        Me.lblReminder.Text = "Пароль скопирован. Очистка буфера через 30с."
        Me.lblReminder.Visible = False
        '
        'pnlPIN
        '
        Me.pnlPIN.BackColor = System.Drawing.Color.FromArgb(CType(CType(105, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(146, Byte), Integer))
        Me.pnlPIN.Controls.Add(Me.tbxPinCode)
        Me.pnlPIN.Controls.Add(Me.lblEnterPin)
        Me.pnlPIN.Location = New System.Drawing.Point(11, 48)
        Me.pnlPIN.Name = "pnlPIN"
        Me.pnlPIN.Size = New System.Drawing.Size(344, 129)
        Me.pnlPIN.TabIndex = 12
        Me.pnlPIN.Visible = False
        '
        'tbxPinCode
        '
        Me.tbxPinCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tbxPinCode.Font = New System.Drawing.Font("Tahoma", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.tbxPinCode.Location = New System.Drawing.Point(20, 47)
        Me.tbxPinCode.MaxLength = 8
        Me.tbxPinCode.Name = "tbxPinCode"
        Me.tbxPinCode.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tbxPinCode.Size = New System.Drawing.Size(299, 39)
        Me.tbxPinCode.TabIndex = 1
        Me.tbxPinCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.tbxPinCode.UseSystemPasswordChar = True
        '
        'lblEnterPin
        '
        Me.lblEnterPin.BackColor = System.Drawing.Color.Transparent
        Me.lblEnterPin.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblEnterPin.ForeColor = System.Drawing.Color.White
        Me.lblEnterPin.Location = New System.Drawing.Point(-1, 10)
        Me.lblEnterPin.Name = "lblEnterPin"
        Me.lblEnterPin.Size = New System.Drawing.Size(344, 37)
        Me.lblEnterPin.TabIndex = 0
        Me.lblEnterPin.Text = "Введите PIN сессии:"
        Me.lblEnterPin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmPassword
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(367, 233)
        Me.Controls.Add(Me.pnlPIN)
        Me.Controls.Add(Me.lblReminder)
        Me.Controls.Add(Me.btnShowPassword)
        Me.Controls.Add(Me.btnCopyPassword)
        Me.Controls.Add(Me.tbxPassword)
        Me.Controls.Add(Me.tbxUser)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.tbxDescription)
        Me.Controls.Add(Me.tbxPassName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnClose)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPassword"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Учётные данные ""****"""
        Me.pnlPIN.ResumeLayout(False)
        Me.pnlPIN.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnClose As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents tbxPassName As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents tbxDescription As TextBox
    Friend WithEvents tbxUser As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents tbxPassword As TextBox
    Friend WithEvents btnCopyPassword As Button
    Friend WithEvents btnShowPassword As Button
    Friend WithEvents tmrClipClear As Timer
    Friend WithEvents lblReminder As Label
    Friend WithEvents pnlPIN As Panel
    Friend WithEvents tbxPinCode As TextBox
    Friend WithEvents lblEnterPin As Label
End Class
