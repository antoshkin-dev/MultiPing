<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmLogin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLogin))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbxCompany = New System.Windows.Forms.ComboBox()
        Me.cbxUser = New System.Windows.Forms.ComboBox()
        Me.tbxPassword = New System.Windows.Forms.TextBox()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.lblLoading = New System.Windows.Forms.Label()
        Me.tmrProcessor = New System.Windows.Forms.Timer(Me.components)
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tbxSessionPin = New System.Windows.Forms.TextBox()
        Me.picLoading = New System.Windows.Forms.PictureBox()
        Me.lblOTP = New System.Windows.Forms.Label()
        Me.tbxOTP = New System.Windows.Forms.TextBox()
        CType(Me.picLoading, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Компания:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 16)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Пользователь:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 101)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 16)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Пароль:"
        '
        'cbxCompany
        '
        Me.cbxCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxCompany.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbxCompany.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.cbxCompany.FormattingEnabled = True
        Me.cbxCompany.Location = New System.Drawing.Point(11, 28)
        Me.cbxCompany.Name = "cbxCompany"
        Me.cbxCompany.Size = New System.Drawing.Size(317, 24)
        Me.cbxCompany.TabIndex = 1
        '
        'cbxUser
        '
        Me.cbxUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cbxUser.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.cbxUser.FormattingEnabled = True
        Me.cbxUser.Location = New System.Drawing.Point(12, 74)
        Me.cbxUser.Name = "cbxUser"
        Me.cbxUser.Size = New System.Drawing.Size(318, 24)
        Me.cbxUser.TabIndex = 2
        '
        'tbxPassword
        '
        Me.tbxPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbxPassword.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.tbxPassword.Location = New System.Drawing.Point(11, 120)
        Me.tbxPassword.MaxLength = 20
        Me.tbxPassword.Name = "tbxPassword"
        Me.tbxPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tbxPassword.Size = New System.Drawing.Size(237, 23)
        Me.tbxPassword.TabIndex = 3
        Me.tbxPassword.UseSystemPasswordChar = True
        Me.tbxPassword.WordWrap = False
        '
        'btnLogin
        '
        Me.btnLogin.Enabled = False
        Me.btnLogin.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.btnLogin.FlatAppearance.BorderSize = 0
        Me.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.btnLogin.Location = New System.Drawing.Point(257, 120)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(74, 72)
        Me.btnLogin.TabIndex = 6
        Me.btnLogin.Text = "Войти"
        Me.btnLogin.UseVisualStyleBackColor = True
        '
        'lblLoading
        '
        Me.lblLoading.BackColor = System.Drawing.Color.White
        Me.lblLoading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLoading.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblLoading.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.lblLoading.Location = New System.Drawing.Point(11, 201)
        Me.lblLoading.Name = "lblLoading"
        Me.lblLoading.Size = New System.Drawing.Size(320, 139)
        Me.lblLoading.TabIndex = 0
        Me.lblLoading.Text = "Выберите компанию"
        Me.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label4.Location = New System.Drawing.Point(9, 148)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 16)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "PIN-сессии:"
        '
        'tbxSessionPin
        '
        Me.tbxSessionPin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbxSessionPin.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.tbxSessionPin.Location = New System.Drawing.Point(11, 167)
        Me.tbxSessionPin.MaxLength = 8
        Me.tbxSessionPin.Name = "tbxSessionPin"
        Me.tbxSessionPin.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tbxSessionPin.Size = New System.Drawing.Size(94, 23)
        Me.tbxSessionPin.TabIndex = 4
        Me.tbxSessionPin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.tbxSessionPin.UseSystemPasswordChar = True
        Me.tbxSessionPin.WordWrap = False
        '
        'picLoading
        '
        Me.picLoading.BackColor = System.Drawing.Color.Transparent
        Me.picLoading.Image = Global.MultiPing.My.Resources.Resources.loading
        Me.picLoading.Location = New System.Drawing.Point(284, 203)
        Me.picLoading.Name = "picLoading"
        Me.picLoading.Size = New System.Drawing.Size(45, 43)
        Me.picLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picLoading.TabIndex = 6
        Me.picLoading.TabStop = False
        '
        'lblOTP
        '
        Me.lblOTP.AutoSize = True
        Me.lblOTP.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblOTP.Location = New System.Drawing.Point(109, 148)
        Me.lblOTP.Name = "lblOTP"
        Me.lblOTP.Size = New System.Drawing.Size(122, 16)
        Me.lblOTP.TabIndex = 0
        Me.lblOTP.Text = "OTP пользователя:"
        Me.lblOTP.Visible = False
        '
        'tbxOTP
        '
        Me.tbxOTP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbxOTP.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.tbxOTP.Location = New System.Drawing.Point(112, 167)
        Me.tbxOTP.MaxLength = 6
        Me.tbxOTP.Name = "tbxOTP"
        Me.tbxOTP.Size = New System.Drawing.Size(136, 23)
        Me.tbxOTP.TabIndex = 5
        Me.tbxOTP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.tbxOTP.Visible = False
        Me.tbxOTP.WordWrap = False
        '
        'frmLogin
        '
        Me.AcceptButton = Me.btnLogin
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(343, 353)
        Me.Controls.Add(Me.picLoading)
        Me.Controls.Add(Me.lblLoading)
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.tbxOTP)
        Me.Controls.Add(Me.tbxSessionPin)
        Me.Controls.Add(Me.tbxPassword)
        Me.Controls.Add(Me.cbxUser)
        Me.Controls.Add(Me.cbxCompany)
        Me.Controls.Add(Me.lblOTP)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Авторизация в MultiPing"
        CType(Me.picLoading, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbxCompany As System.Windows.Forms.ComboBox
    Friend WithEvents cbxUser As System.Windows.Forms.ComboBox
    Friend WithEvents tbxPassword As System.Windows.Forms.TextBox
    Friend WithEvents btnLogin As System.Windows.Forms.Button
    Friend WithEvents lblLoading As System.Windows.Forms.Label
    Friend WithEvents tmrProcessor As System.Windows.Forms.Timer
    Friend WithEvents Label4 As Label
    Friend WithEvents tbxSessionPin As TextBox
    Friend WithEvents picLoading As PictureBox
    Friend WithEvents lblOTP As Label
    Friend WithEvents tbxOTP As TextBox
End Class
