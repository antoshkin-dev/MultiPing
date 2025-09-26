Imports MultiPing.MyClasses

Public Class frmPassword
    Public ClearTimer As Short
    Public CryptedPassword As String
    Private PasswordIsCrypted As Boolean = True 'Маркер того, что пароль требуется расшифровать перед отображением
    Private CopyAfterTrue As Boolean
    Private TickLabel As String
    Private Sub btnShowPassword_Click(sender As Object, e As EventArgs) Handles btnShowPassword.Click
        If tbxPassword.PasswordChar <> "*" Then
            tbxPassword.PasswordChar = "*"
            tbxPassword.Text = "crypted"
            PasswordIsCrypted = True
        Else
            tbxPassword.PasswordChar = ""
            If PasswordIsCrypted = True Then
                ShowPinPanel(False)
            End If
        End If
    End Sub
    Private Sub ShowPinPanel(CAT As Boolean)
        pnlPIN.Visible = True
        tbxPinCode.Focus()
        CopyAfterTrue = CAT
    End Sub
    Private Sub BeginTimer(msg As String)
        TickLabel = msg
        ClearTimer = 30
        tmrClipClear.Enabled = True
        lblReminder.Visible = True
        UpdateTickLabel()
    End Sub
    Private Sub CopyPassword()
        BeginTimer("Пароль скопирован. Буфер обмена будет очищен через {0}с.")
        Clipboard.SetText(tbxPassword.Text)
    End Sub
    Private Sub btnCopyPassword_Click(sender As Object, e As EventArgs) Handles btnCopyPassword.Click
        If PasswordIsCrypted = True Then
            ShowPinPanel(True)
        Else
            CopyPassword()
        End If
    End Sub
    Private Sub UpdateTickLabel()
        lblReminder.Text = String.Format(TickLabel, ClearTimer)
    End Sub
    Private Sub tmrClipClear_Tick(sender As Object, e As EventArgs) Handles tmrClipClear.Tick
        ClearTimer = ClearTimer - 1
        If ClearTimer = 0 Then
            tmrClipClear.Enabled = False
            lblReminder.Visible = False
            Try
                Clipboard.Clear()
            Catch ex As Exception
                'Буфер обмена может не очиститься, если ПК заблокирован
            End Try
            tbxPassword.Text = "crypted"
            PasswordIsCrypted = True
        Else
            UpdateTickLabel()
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub tbxPinCode_KeyDown(sender As Object, e As KeyEventArgs) Handles tbxPinCode.KeyDown
        If (e.KeyValue = Keys.Return) Then
            'Проверяем введенный пин-код
            Dim DecryptedPassword As String = ""
            If PublicData.DecryptPassword(CryptedPassword, tbxPinCode.Text, DecryptedPassword) = True Then
                'ввели корректный пин-код, производим дешифрацию
                tbxPassword.Text = DecryptedPassword
                PasswordIsCrypted = False
                If CopyAfterTrue = True Then
                    CopyPassword()
                Else
                    BeginTimer("Пароль будет скрыт через {0}с.")
                End If
            End If
            pnlPIN.Visible = False
            tbxPinCode.Text = ""
            e.SuppressKeyPress = True
        ElseIf (e.KeyValue <> 8 And e.KeyValue <> 37 And e.KeyValue <> 39) And (e.KeyValue < 48 Or e.KeyValue > 57) And (e.KeyValue < 96 Or e.KeyValue > 105) Then
            'Нажали не разрешенную клавишу, отменяем ввод
            e.SuppressKeyPress = True
        End If
    End Sub

End Class