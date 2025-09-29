<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.pnlTree = New System.Windows.Forms.Panel()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbExpandAll = New System.Windows.Forms.ToolStripButton()
        Me.tsbRollUp = New System.Windows.Forms.ToolStripButton()
        Me.tsbPasswordInvent = New System.Windows.Forms.ToolStripButton()
        Me.tsbGeneratePassword = New System.Windows.Forms.ToolStripButton()
        Me.cbxConfigs = New System.Windows.Forms.ToolStripComboBox()
        Me.tbxSearch = New System.Windows.Forms.ToolStripTextBox()
        Me.cmActions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.TestToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnCopyPassword = New System.Windows.Forms.Button()
        Me.btnClosePwdPnl = New System.Windows.Forms.Button()
        Me.btnCloseFind = New System.Windows.Forms.Button()
        Me.tmrPinger = New System.Windows.Forms.Timer(Me.components)
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblInfo = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.prgBar = New System.Windows.Forms.ToolStripProgressBar()
        Me.pnlRandomPassword = New System.Windows.Forms.Panel()
        Me.tbxRandomPassword = New System.Windows.Forms.TextBox()
        Me.pnlPIN = New System.Windows.Forms.Panel()
        Me.btnCancelPin = New System.Windows.Forms.Button()
        Me.tbxPinCode = New System.Windows.Forms.TextBox()
        Me.lblEnterPin = New System.Windows.Forms.Label()
        Me.pnlUserForConnect = New System.Windows.Forms.Panel()
        Me.btnHideUserForConnect = New System.Windows.Forms.Button()
        Me.tbxUserForConnect = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlFindResult = New System.Windows.Forms.Panel()
        Me.btnFindNext = New System.Windows.Forms.Button()
        Me.btnFindPrev = New System.Windows.Forms.Button()
        Me.lblFindResult = New System.Windows.Forms.Label()
        Me.ToolStrip1.SuspendLayout()
        Me.cmActions.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.pnlRandomPassword.SuspendLayout()
        Me.pnlPIN.SuspendLayout()
        Me.pnlUserForConnect.SuspendLayout()
        Me.pnlFindResult.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTree
        '
        Me.pnlTree.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlTree.AutoScroll = True
        Me.pnlTree.BackColor = System.Drawing.Color.White
        Me.pnlTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTree.Location = New System.Drawing.Point(4, 23)
        Me.pnlTree.Name = "pnlTree"
        Me.pnlTree.Size = New System.Drawing.Size(389, 486)
        Me.pnlTree.TabIndex = 0
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbExpandAll, Me.tsbRollUp, Me.tsbPasswordInvent, Me.tsbGeneratePassword, Me.cbxConfigs, Me.tbxSearch})
        Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(399, 23)
        Me.ToolStrip1.TabIndex = 6
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsbExpandAll
        '
        Me.tsbExpandAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbExpandAll.Image = CType(resources.GetObject("tsbExpandAll.Image"), System.Drawing.Image)
        Me.tsbExpandAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbExpandAll.Name = "tsbExpandAll"
        Me.tsbExpandAll.Size = New System.Drawing.Size(23, 20)
        Me.tsbExpandAll.Text = "Развернуть все ветки"
        '
        'tsbRollUp
        '
        Me.tsbRollUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbRollUp.Image = CType(resources.GetObject("tsbRollUp.Image"), System.Drawing.Image)
        Me.tsbRollUp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbRollUp.Name = "tsbRollUp"
        Me.tsbRollUp.Size = New System.Drawing.Size(23, 20)
        Me.tsbRollUp.Text = "Свернуть все ветки"
        '
        'tsbPasswordInvent
        '
        Me.tsbPasswordInvent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbPasswordInvent.Image = CType(resources.GetObject("tsbPasswordInvent.Image"), System.Drawing.Image)
        Me.tsbPasswordInvent.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbPasswordInvent.Name = "tsbPasswordInvent"
        Me.tsbPasswordInvent.Size = New System.Drawing.Size(23, 20)
        Me.tsbPasswordInvent.Text = "Выгрузка учётных записей"
        '
        'tsbGeneratePassword
        '
        Me.tsbGeneratePassword.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbGeneratePassword.Image = CType(resources.GetObject("tsbGeneratePassword.Image"), System.Drawing.Image)
        Me.tsbGeneratePassword.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbGeneratePassword.Name = "tsbGeneratePassword"
        Me.tsbGeneratePassword.Size = New System.Drawing.Size(23, 20)
        Me.tsbGeneratePassword.Text = "Сгенерить пароль"
        '
        'cbxConfigs
        '
        Me.cbxConfigs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxConfigs.DropDownWidth = 200
        Me.cbxConfigs.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.cbxConfigs.Name = "cbxConfigs"
        Me.cbxConfigs.Size = New System.Drawing.Size(150, 21)
        '
        'tbxSearch
        '
        Me.tbxSearch.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.tbxSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.tbxSearch.MaxLength = 30
        Me.tbxSearch.Name = "tbxSearch"
        Me.tbxSearch.Size = New System.Drawing.Size(120, 22)
        Me.tbxSearch.Text = "Поиск"
        '
        'cmActions
        '
        Me.cmActions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TestToolStripMenuItem})
        Me.cmActions.Name = "cmActions"
        Me.cmActions.Size = New System.Drawing.Size(94, 26)
        Me.cmActions.Text = "test"
        '
        'TestToolStripMenuItem
        '
        Me.TestToolStripMenuItem.Name = "TestToolStripMenuItem"
        Me.TestToolStripMenuItem.Size = New System.Drawing.Size(93, 22)
        Me.TestToolStripMenuItem.Text = "test"
        Me.TestToolStripMenuItem.ToolTipText = "tooltip here"
        '
        'ToolTip1
        '
        Me.ToolTip1.AutomaticDelay = 200
        Me.ToolTip1.AutoPopDelay = 2000
        Me.ToolTip1.InitialDelay = 200
        Me.ToolTip1.ReshowDelay = 5000
        '
        'btnCopyPassword
        '
        Me.btnCopyPassword.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnCopyPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCopyPassword.Image = CType(resources.GetObject("btnCopyPassword.Image"), System.Drawing.Image)
        Me.btnCopyPassword.Location = New System.Drawing.Point(145, 5)
        Me.btnCopyPassword.Name = "btnCopyPassword"
        Me.btnCopyPassword.Size = New System.Drawing.Size(23, 22)
        Me.btnCopyPassword.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.btnCopyPassword, "Скопировать")
        Me.btnCopyPassword.UseVisualStyleBackColor = True
        '
        'btnClosePwdPnl
        '
        Me.btnClosePwdPnl.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnClosePwdPnl.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClosePwdPnl.Image = CType(resources.GetObject("btnClosePwdPnl.Image"), System.Drawing.Image)
        Me.btnClosePwdPnl.Location = New System.Drawing.Point(174, 1)
        Me.btnClosePwdPnl.Name = "btnClosePwdPnl"
        Me.btnClosePwdPnl.Size = New System.Drawing.Size(23, 22)
        Me.btnClosePwdPnl.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.btnClosePwdPnl, "Закрыть")
        Me.btnClosePwdPnl.UseVisualStyleBackColor = True
        '
        'btnCloseFind
        '
        Me.btnCloseFind.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnCloseFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCloseFind.Image = CType(resources.GetObject("btnCloseFind.Image"), System.Drawing.Image)
        Me.btnCloseFind.Location = New System.Drawing.Point(166, 1)
        Me.btnCloseFind.Margin = New System.Windows.Forms.Padding(1)
        Me.btnCloseFind.Name = "btnCloseFind"
        Me.btnCloseFind.Size = New System.Drawing.Size(23, 22)
        Me.btnCloseFind.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.btnCloseFind, "Закрыть")
        Me.btnCloseFind.UseVisualStyleBackColor = True
        '
        'tmrPinger
        '
        Me.tmrPinger.Interval = 5000
        '
        'StatusStrip1
        '
        Me.StatusStrip1.AllowItemReorder = True
        Me.StatusStrip1.AutoSize = False
        Me.StatusStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblInfo, Me.lblStatus, Me.prgBar})
        Me.StatusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 511)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(399, 50)
        Me.StatusStrip1.TabIndex = 7
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblInfo
        '
        Me.lblInfo.Margin = New System.Windows.Forms.Padding(0, 3, 10, 2)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(16, 15)
        Me.lblInfo.Text = "..."
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblStatus
        '
        Me.lblStatus.Margin = New System.Windows.Forms.Padding(0, 3, 10, 2)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(16, 15)
        Me.lblStatus.Text = "..."
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'prgBar
        '
        Me.prgBar.Name = "prgBar"
        Me.prgBar.Size = New System.Drawing.Size(100, 16)
        Me.prgBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.prgBar.Visible = False
        '
        'pnlRandomPassword
        '
        Me.pnlRandomPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlRandomPassword.Controls.Add(Me.btnClosePwdPnl)
        Me.pnlRandomPassword.Controls.Add(Me.btnCopyPassword)
        Me.pnlRandomPassword.Controls.Add(Me.tbxRandomPassword)
        Me.pnlRandomPassword.Location = New System.Drawing.Point(76, 26)
        Me.pnlRandomPassword.Name = "pnlRandomPassword"
        Me.pnlRandomPassword.Size = New System.Drawing.Size(200, 34)
        Me.pnlRandomPassword.TabIndex = 1
        Me.pnlRandomPassword.Visible = False
        '
        'tbxRandomPassword
        '
        Me.tbxRandomPassword.BackColor = System.Drawing.Color.White
        Me.tbxRandomPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbxRandomPassword.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.tbxRandomPassword.Location = New System.Drawing.Point(3, 3)
        Me.tbxRandomPassword.Name = "tbxRandomPassword"
        Me.tbxRandomPassword.ReadOnly = True
        Me.tbxRandomPassword.Size = New System.Drawing.Size(141, 26)
        Me.tbxRandomPassword.TabIndex = 1
        Me.tbxRandomPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'pnlPIN
        '
        Me.pnlPIN.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlPIN.BackColor = System.Drawing.Color.FromArgb(CType(CType(105, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(146, Byte), Integer))
        Me.pnlPIN.Controls.Add(Me.btnCancelPin)
        Me.pnlPIN.Controls.Add(Me.tbxPinCode)
        Me.pnlPIN.Controls.Add(Me.lblEnterPin)
        Me.pnlPIN.Location = New System.Drawing.Point(31, 211)
        Me.pnlPIN.Name = "pnlPIN"
        Me.pnlPIN.Size = New System.Drawing.Size(345, 131)
        Me.pnlPIN.TabIndex = 14
        Me.pnlPIN.Visible = False
        '
        'btnCancelPin
        '
        Me.btnCancelPin.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnCancelPin.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnCancelPin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelPin.Location = New System.Drawing.Point(142, 90)
        Me.btnCancelPin.Name = "btnCancelPin"
        Me.btnCancelPin.Size = New System.Drawing.Size(61, 28)
        Me.btnCancelPin.TabIndex = 2
        Me.btnCancelPin.Text = "Отмена"
        Me.btnCancelPin.UseVisualStyleBackColor = False
        '
        'tbxPinCode
        '
        Me.tbxPinCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbxPinCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tbxPinCode.Font = New System.Drawing.Font("Tahoma", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.tbxPinCode.Location = New System.Drawing.Point(20, 42)
        Me.tbxPinCode.MaxLength = 8
        Me.tbxPinCode.Name = "tbxPinCode"
        Me.tbxPinCode.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tbxPinCode.Size = New System.Drawing.Size(300, 39)
        Me.tbxPinCode.TabIndex = 1
        Me.tbxPinCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.tbxPinCode.UseSystemPasswordChar = True
        '
        'lblEnterPin
        '
        Me.lblEnterPin.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblEnterPin.BackColor = System.Drawing.Color.Transparent
        Me.lblEnterPin.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblEnterPin.ForeColor = System.Drawing.Color.White
        Me.lblEnterPin.Location = New System.Drawing.Point(9, 5)
        Me.lblEnterPin.Name = "lblEnterPin"
        Me.lblEnterPin.Size = New System.Drawing.Size(326, 37)
        Me.lblEnterPin.TabIndex = 0
        Me.lblEnterPin.Text = "Введите PIN-сессии"
        Me.lblEnterPin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlUserForConnect
        '
        Me.pnlUserForConnect.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlUserForConnect.BackColor = System.Drawing.Color.FromArgb(CType(CType(105, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(146, Byte), Integer))
        Me.pnlUserForConnect.Controls.Add(Me.btnHideUserForConnect)
        Me.pnlUserForConnect.Controls.Add(Me.tbxUserForConnect)
        Me.pnlUserForConnect.Controls.Add(Me.Label1)
        Me.pnlUserForConnect.Location = New System.Drawing.Point(31, 211)
        Me.pnlUserForConnect.Name = "pnlUserForConnect"
        Me.pnlUserForConnect.Size = New System.Drawing.Size(345, 108)
        Me.pnlUserForConnect.TabIndex = 16
        Me.pnlUserForConnect.Visible = False
        '
        'btnHideUserForConnect
        '
        Me.btnHideUserForConnect.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnHideUserForConnect.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnHideUserForConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHideUserForConnect.Location = New System.Drawing.Point(142, 67)
        Me.btnHideUserForConnect.Name = "btnHideUserForConnect"
        Me.btnHideUserForConnect.Size = New System.Drawing.Size(61, 28)
        Me.btnHideUserForConnect.TabIndex = 2
        Me.btnHideUserForConnect.Text = "Отмена"
        Me.btnHideUserForConnect.UseVisualStyleBackColor = False
        '
        'tbxUserForConnect
        '
        Me.tbxUserForConnect.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbxUserForConnect.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tbxUserForConnect.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.tbxUserForConnect.Location = New System.Drawing.Point(20, 42)
        Me.tbxUserForConnect.MaxLength = 0
        Me.tbxUserForConnect.Name = "tbxUserForConnect"
        Me.tbxUserForConnect.Size = New System.Drawing.Size(300, 19)
        Me.tbxUserForConnect.TabIndex = 1
        Me.tbxUserForConnect.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.tbxUserForConnect.WordWrap = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(0, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(344, 37)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Введите пользователя узла"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlFindResult
        '
        Me.pnlFindResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFindResult.Controls.Add(Me.btnCloseFind)
        Me.pnlFindResult.Controls.Add(Me.btnFindNext)
        Me.pnlFindResult.Controls.Add(Me.btnFindPrev)
        Me.pnlFindResult.Controls.Add(Me.lblFindResult)
        Me.pnlFindResult.Location = New System.Drawing.Point(174, 22)
        Me.pnlFindResult.Name = "pnlFindResult"
        Me.pnlFindResult.Size = New System.Drawing.Size(192, 34)
        Me.pnlFindResult.TabIndex = 17
        Me.pnlFindResult.Visible = False
        '
        'btnFindNext
        '
        Me.btnFindNext.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnFindNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFindNext.Location = New System.Drawing.Point(138, 3)
        Me.btnFindNext.Name = "btnFindNext"
        Me.btnFindNext.Size = New System.Drawing.Size(25, 26)
        Me.btnFindNext.TabIndex = 2
        Me.btnFindNext.Text = ">"
        Me.btnFindNext.UseVisualStyleBackColor = True
        '
        'btnFindPrev
        '
        Me.btnFindPrev.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnFindPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFindPrev.Location = New System.Drawing.Point(1, 3)
        Me.btnFindPrev.Name = "btnFindPrev"
        Me.btnFindPrev.Size = New System.Drawing.Size(25, 26)
        Me.btnFindPrev.TabIndex = 1
        Me.btnFindPrev.Text = "<"
        Me.btnFindPrev.UseVisualStyleBackColor = True
        '
        'lblFindResult
        '
        Me.lblFindResult.Location = New System.Drawing.Point(28, 6)
        Me.lblFindResult.Name = "lblFindResult"
        Me.lblFindResult.Size = New System.Drawing.Size(110, 18)
        Me.lblFindResult.TabIndex = 0
        Me.lblFindResult.Text = "Результат 1 из 3"
        Me.lblFindResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(399, 561)
        Me.Controls.Add(Me.pnlFindResult)
        Me.Controls.Add(Me.pnlUserForConnect)
        Me.Controls.Add(Me.pnlPIN)
        Me.Controls.Add(Me.pnlRandomPassword)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.pnlTree)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "MultiPing Client by Anton Vanichkin"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.cmActions.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.pnlRandomPassword.ResumeLayout(False)
        Me.pnlRandomPassword.PerformLayout()
        Me.pnlPIN.ResumeLayout(False)
        Me.pnlPIN.PerformLayout()
        Me.pnlUserForConnect.ResumeLayout(False)
        Me.pnlUserForConnect.PerformLayout()
        Me.pnlFindResult.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlTree As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbExpandAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbRollUp As System.Windows.Forms.ToolStripButton
    Friend WithEvents cbxConfigs As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents cmActions As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents tmrPinger As System.Windows.Forms.Timer
    Friend WithEvents TestToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents tbxSearch As ToolStripTextBox
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents lblStatus As ToolStripStatusLabel
    Friend WithEvents tsbPasswordInvent As ToolStripButton
    Friend WithEvents prgBar As ToolStripProgressBar
    Friend WithEvents lblInfo As ToolStripStatusLabel
    Friend WithEvents tsbGeneratePassword As ToolStripButton
    Friend WithEvents pnlRandomPassword As Panel
    Friend WithEvents tbxRandomPassword As TextBox
    Friend WithEvents btnCopyPassword As Button
    Friend WithEvents btnClosePwdPnl As Button
    Friend WithEvents pnlPIN As Panel
    Friend WithEvents btnCancelPin As Button
    Friend WithEvents tbxPinCode As TextBox
    Friend WithEvents lblEnterPin As Label
    Friend WithEvents pnlUserForConnect As Panel
    Friend WithEvents btnHideUserForConnect As Button
    Friend WithEvents tbxUserForConnect As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents pnlFindResult As Panel
    Friend WithEvents lblFindResult As Label
    Friend WithEvents btnFindPrev As Button
    Friend WithEvents btnFindNext As Button
    Friend WithEvents btnCloseFind As Button
End Class
