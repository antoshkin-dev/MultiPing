'Imports System.Security.Cryptography
'Imports System.Text
Imports System.Threading
Imports System.Web.Script.Serialization
'Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Window
Imports System.Xml
Imports Microsoft.VisualBasic.ApplicationServices

'Imports System.Xml.Serialization
Imports MultiPing.MyClasses

Public Class frmLogin

    Dim ProcessThread As Thread
    Dim Users As New List(Of UserCompany)
    'Делегаты
    Private Delegate Sub SetMessageD(ByVal TX As String)
    Private Delegate Sub AddInListD(ByRef UpdateList As ComboBox, ByVal ListKey As Integer, ByVal Caption As String)
    Private Delegate Sub SelectInListD(ByRef UpdateList As ComboBox, ByRef SelectedItem As ListItem)
    Private Delegate Sub HideLoadingD()
    Private Delegate Sub ShowLoadingD()
    Private Delegate Sub EnableLoginD()
    Private Delegate Sub CloseFormD()
    Private Delegate Sub SetPasswordFocusD()
    Private Sub HideLoading()
        If picLoading.InvokeRequired = True Then
            picLoading.Invoke(New HideLoadingD(AddressOf HideLoading))
        Else
            picLoading.Visible = False
        End If
        'Включаем кнопку логина
        EnableLoginButton()
    End Sub
    Private Sub ShowLoading()
        If picLoading.InvokeRequired = True Then
            picLoading.Invoke(New ShowLoadingD(AddressOf ShowLoading))
        Else
            picLoading.Visible = True
        End If
    End Sub
    Private Sub EnableLoginButton()
        If btnLogin.InvokeRequired = True Then
            btnLogin.Invoke(New EnableLoginD(AddressOf EnableLoginButton))
        Else
            btnLogin.Enabled = True
        End If
    End Sub
    Private Sub SetMessage(TX As String)
        If lblLoading.InvokeRequired = True Then
            lblLoading.Invoke(New SetMessageD(AddressOf SetMessage), TX)
        Else
            lblLoading.Text = TX
        End If
        ShowLoading()
    End Sub
    Private Sub AddInList(ByRef UpdateList As ComboBox, ByVal ListKey As Integer, ByVal Caption As String)
        If UpdateList.InvokeRequired = True Then
            Dim args(2) As Object
            args(0) = UpdateList
            args(1) = ListKey
            args(2) = Caption
            UpdateList.Invoke(New AddInListD(AddressOf AddInList), args)
        Else
            UpdateList.Items.Add(New ListItem(Caption, ListKey))
        End If
    End Sub
    Private Sub SelectInList(ByRef UpdateList As ComboBox, ByRef SelectedItem As ListItem)
        If UpdateList.InvokeRequired = True Then
            Dim args(1) As Object
            args(0) = UpdateList
            args(1) = SelectedItem
            UpdateList.Invoke(New SelectInListD(AddressOf SelectInList), args)
        Else
            UpdateList.SelectedItem = SelectedItem
        End If
    End Sub
    Private Sub frmLogin_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        StartThreadGetUsers()
    End Sub
    Private Sub StartThreadGetUsers()
        'Загрузка списка компаний и сотрудников
        ProcessThread = New Thread(AddressOf GetUsers)
        ProcessThread.Start()
    End Sub
    Private Sub StartAutoLoginThread()
        ProcessThread = New Thread(AddressOf AutoLogin)
        ProcessThread.Start()
    End Sub
    Private Sub AutoLogin()
        Thread.Sleep(100)
        If ProcessLogin(My.Settings.UID, My.Settings.Password) = False Then
            'Что-то пошло не так, догружаем список компании
            Thread.Sleep(3000)
            StartThreadGetUsers()
        End If
    End Sub
    Private Sub GetUsers()
        Thread.Sleep(100)
        Dim Result As String
        Dim HP As New HTTPProcessor.HTTPProcessor
        HP.AddSendVar("action", "getusers")
        Result = HP.SendHTTPRequest(PublicData.MPURL, "POST", True)
        If (Mid(Result, 1, 1) = "!") Then
            SetMessage("Ошибка подключения." & vbNewLine & PublicData.MPURL)
        Else
            Debug.Print(Result)
            LoadLogins(Result)
            HideLoading()
        End If
    End Sub
    Private Sub LoadLogins(XMLText As String)
        Dim XReader As New XmlDocument()
        Dim XMLNodes As XmlNodeList
        Try
            XReader.LoadXml(XMLText)
        Catch ex As Exception
            MsgBox("Ошибка обработки данных от сервера. " & ex.Message & vbNewLine & "Полученные данные: " & XMLText, MsgBoxStyle.Critical, "Ошибка")
            End
        End Try

        REM 1. Формируем список компаний
        XMLNodes = XReader.GetElementsByTagName("companies")
        For Each CompaniesNode As XmlNode In XMLNodes
            For Each Company As XmlNode In CompaniesNode
                AddInList(cbxCompany, CInt(Company("cid").InnerText), Company("name").InnerText)
            Next
        Next

        REM 2. Формируем справочник сотрудников компании
        XMLNodes = XReader.GetElementsByTagName("users")
        For Each UsersNode As XmlNode In XMLNodes
            For Each User As XmlNode In UsersNode
                Dim NewUser As New UserCompany()
                NewUser.UserName = User("name").InnerText
                NewUser.UID = CInt(User("uid").InnerText)
                NewUser.CID = CInt(User("cid").InnerText)
                NewUser.HaveAD = IIf(CInt(User("havead").InnerText) = 1, True, False)
                NewUser.HaveOTP = IIf(CInt(User("haveotp").InnerText) = 1, True, False)
                Users.Add(NewUser)
            Next
        Next

        REM 3. Выбираем компанию из конфигурации, если она была сохранена ранее
        SelectLastCompany()
        REM 4. Выбираем пользователя, если он ранее был сохранен в конфигурации
        SelectLastUser()
    End Sub
    Private Sub SelectLastCompany()
        If IsNumeric(My.Settings.CompanyID) Then
            For Each lstItem As ListItem In cbxCompany.Items
                If lstItem.Value = My.Settings.CompanyID Then
                    SelectInList(cbxCompany, lstItem)
                    Exit For
                End If
            Next
        End If
    End Sub
    Private Sub SelectLastUser()
        If IsNumeric(My.Settings.UID) Then
            For Each lstItem As ListItem In cbxUser.Items
                If lstItem.Value = My.Settings.UID Then
                    SelectInList(cbxUser, lstItem)
                    SetPasswordFocus()
                    Exit For
                End If
            Next
        End If
    End Sub
    Private Sub SetPasswordFocus()
        If tbxPassword.InvokeRequired = True Then
            tbxPassword.Invoke(New SetPasswordFocusD(AddressOf SetPasswordFocus))
        Else
            tbxPassword.Focus()
        End If
    End Sub
    Private Sub cbxCompany_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxCompany.SelectedIndexChanged
        Dim SelItem As ListItem = CType(cbxCompany.SelectedItem, ListItem)
        'MsgBox(SelItem.Value & " " & SelItem.Text)
        cbxUser.Items.Clear()
        cbxUser.SelectedItem = Nothing
        For Each User As UserCompany In Users
            If User.CID = SelItem.Value Then
                cbxUser.Items.Add(New ListItem(User.UserName, CStr(User.UID)))
            End If
        Next
        lblLoading.Text = "Выберите пользователя"
    End Sub
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If cbxUser.SelectedItem Is Nothing Or tbxPassword.Text = "" Then
            MsgBox("Необходимо выбрать сотрудника и указать пароль", MsgBoxStyle.Information, "Ошибка авторизации")
            Exit Sub
        End If
        If tbxSessionPin.TextLength < 4 Then
            MsgBox("Необходимо придумать и ввести разовый PIN-код сессии, состоящий из 4-х и более цифр")
            tbxSessionPin.Focus()
            tbxSessionPin.SelectAll()
            Exit Sub
        End If

        btnLogin.Enabled = False
        Dim SelItem As ListItem = CType(cbxUser.SelectedItem, ListItem)
        Dim SelectedCompany As ListItem = CType(cbxCompany.SelectedItem, ListItem)
        My.Settings.UID = SelItem.Value
        My.Settings.CompanyID = SelectedCompany.Value

        My.Settings.Save()

        'Отправляем запрос на авторизацию
        ProcessLogin(CInt(SelItem.Value), tbxPassword.Text)
    End Sub

    Private Function ProcessLogin(UID As Integer, Password As String) As Boolean
        REM Получаем код сессии
        Dim HP As New HTTPProcessor.HTTPProcessor
        SetMessage("Активация сессии")
        Dim SessionResultText As String = HP.SendHTTPRequest(PublicData.DecoderURL & "?action=newsession", "GET", True)

        If HP.WasError = True Then 'Возникла какая-то ошибка при запросе новой сессии
            SetMessage("Ошибка связи с декодером. " & HP.LastError)
            SleepAndHide(3000)
            Return False
        End If
        Debug.Print(SessionResultText)
        Dim JsonParser As New JavaScriptSerializer
        Dim JsonResult As DecoderResults
        Try
            JsonResult = JsonParser.Deserialize(Of DecoderResults)(SessionResultText)
        Catch
            SetMessage("Ошибка JSON ответа декодера. " & SessionResultText)
            SleepAndHide(3000)
            Return False
        End Try

        If JsonResult.result.status = False Then
            Debug.Print(JsonResult.result.description)
            SetMessage("Неудалось активировать сессию. " & JsonResult.result.description)
            SleepAndHide(3000)
            Return False
        End If
        Dim JSONUserSession As New MyClasses.JsonDecoderUserSession
        JSONUserSession = JsonResult.data 'JsonParser.Deserialize(Of DecoderUserSession)(JsonResult.data)
        Dim CryptKey As String = Strings.Left(getSHAHash(JSONUserSession.sessionkey), 32)
        Debug.Print("Crypt key string: " & CryptKey)

        REM продолжаем авторизацию
        SetMessage("Авторизация...")
        'Готовим запрос на авторизацию

        HP.AddSendVar("userid", UID)
        ' Находим объект пользователя, с которым будем работать
        Dim User As UserCompany = FindUserByUID(UID)

        'Если требуется авторизация OTP, прикладываем проверочное значение в запрос
        If User.HaveOTP Then
            If tbxOTP.TextLength <> 6 Then
                SetMessage("Требуется ввести одноразовый код из приложения-генератора OTP")
                SleepAndHide(3000)
                Return False
            End If
            HP.AddSendVar("otp", tbxOTP.Text)
        End If

        If User.HaveAD Then 'Доменная авторизация
            HP.AddSendVar("rawPassword", tbxPassword.Text)
            HP.AddSendVar("action", "adauth")
        Else 'внутренняя авторизация WebApps
            REM Готовим UserKeyHash
            Dim WhitePaper As String = "<tkfz<evfufLkzGfhjkz"
            Dim UserKeyHash As String = getSHAHash(WhitePaper & Password & UID)

            REM Готовим CryptAuth
            Dim DateFormated As String
            Dim DF As String = "yyyyMMdd"
            DateFormated = DateAndTime.Now.ToString(DF)
            Dim Cryptor As New MyCryptor
            Dim AuthHash As String = getSHAHash(UserKeyHash & DateFormated)
            Debug.Print("Auth Hash: " & AuthHash)

            Dim CryptAuth As String = Cryptor.Crypt(AuthHash, CryptKey)
            Debug.Print("crypt auth: " & CryptAuth)
            REM Отправляем CryptHash на проверку авторизации
            HP.AddSendVar("action", "secureauth")
            HP.AddSendVar("sessid", JSONUserSession.sessionid)
            HP.AddSendVar("cryptauth", CryptAuth)
        End If

        'Отправляем запрос на сервер авторизации
        Debug.Print(HP.GetRequestText)
        Dim Result As String = HP.SendHTTPRequest(PublicData.MPURL, "POST", True)
        If (Mid(Result, 1, 1) = "!") Then
            If Result = "!auth error" Then
                SetMessage("Неверный пароль, попробуйте повторить ввод")
                SleepAndHide(3000)
            Else
                SetMessage("Возникла ошибка авторизации: " & vbNewLine & Result)
                SleepAndHide(10000)
            End If
            Return False
        End If

        REM Авторизация прошла успешно. Запоминаем сессию декодера и закрываем ключ декодера пин-кодом авторизации
        PublicData.DecoderSession.SessionId = JSONUserSession.sessionid
        Dim PinCryptor As New MyClasses.MyCryptor
        PublicData.DecoderSession.SessionKeyClosedByPin = PinCryptor.Crypt(CryptKey, Strings.Left(getSHAHash(tbxSessionPin.Text), 32))
        REM Загружаем список конфигураций, полученный от сервера Multiping
        LoadConfigs(Result)
        Return True
    End Function
    Private Sub LoadConfigs(XMLText As String)
        Dim XReader As New XmlDocument()
        Dim XMLNodes As XmlNodeList
        XReader.LoadXml(XMLText)
        REM 1. Получаем код сессии
        XMLNodes = XReader.GetElementsByTagName("sessionid")
        PublicData.SessionID = (XMLNodes(0).InnerText)
        REM 2. Получаем список конфигураций, доступных пользоватлю
        XMLNodes = XReader.GetElementsByTagName("configurations")
        For Each ConfigNode As XmlNode In XMLNodes
            For Each config As XmlNode In ConfigNode
                Debug.Print(config.Item("id").InnerText & config.Item("name").InnerText & config.Item("lastupdate").InnerText)
                PublicData.MPConfigurations.Add(New MPConfig(CInt(config.Item("id").InnerText), config.Item("name").InnerText, config.Item("lastupdate").InnerText))
            Next
        Next
        REM 3. Сохраняем конфигурацию в архив
        'If My.Settings.WorkOffile = False Then
        '    Dim FileStream = New IO.StreamWriter(Application.StartupPath & "\ConfigCache\configs.xml", False, System.Text.Encoding.UTF8)
        '    FileStream.Write(XMLText)
        '    FileStream.Close()
        'End If
        REM 4. Запускаем основное окно MultiPing
        GC.Collect()
        OpenMainForm()
        CloseForm()


    End Sub
    Private Sub OpenMainForm()
        ProcessThread = New Thread(AddressOf OpenMainFormT)
        ProcessThread.SetApartmentState(ApartmentState.STA)
        ProcessThread.Start()

    End Sub
    Private Sub OpenMainFormT()
        Application.Run(frmMain)
    End Sub
    Private Sub CloseForm()
        If Me.InvokeRequired = True Then
            Me.Invoke(New CloseFormD(AddressOf CloseForm))
        Else
            Me.Close()
        End If
    End Sub
    Private Sub SleepAndHide(WaitFor As Integer)
        ProcessThread = New Thread(AddressOf SleepAndHideAction)
        ProcessThread.Start(WaitFor)
    End Sub
    Private Sub SleepAndHideAction(WaitFor As Integer)
        Thread.Sleep(WaitFor)
        HideLoading()
    End Sub

    Private Sub tbxSessionPin_KeyDown(sender As Object, e As KeyEventArgs) Handles tbxSessionPin.KeyDown, tbxOTP.KeyDown
        'If e.KeyCode = Keys.N Then
        Debug.Print(e.KeyValue)
        If (e.KeyValue <> 8 And e.KeyValue <> 37 And e.KeyValue <> 39) And (e.KeyValue < 48 Or e.KeyValue > 57) And (e.KeyValue < 96 Or e.KeyValue > 105) Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub cbxUser_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxUser.SelectedIndexChanged
        Dim SelItem As ListItem = CType(cbxUser.SelectedItem, ListItem)
        Dim UserID As Integer = CInt(SelItem.Value)
        Dim User As UserCompany = FindUserByUID(UserID)
        If User.HaveAD Then
            lblLoading.Text = "Введите пароль от доменной учетной записи"
        Else
            lblLoading.Text = "Введите ваш пароль"
        End If

        If IsNothing(User) = False And User.HaveOTP = True Then
            lblOTP.Visible = True
            tbxOTP.Visible = True
            Exit Sub
        End If
        lblOTP.Visible = False
        tbxOTP.Visible = False
    End Sub
    Private Function FindUserByUID(UID As Integer) As UserCompany
        For Each User As UserCompany In Users
            If User.UID = UID Then
                Return User
            End If
        Next
        Return Nothing
    End Function
    Private Sub tbxPassword_KeyUp(sender As Object, e As KeyEventArgs) Handles tbxPassword.KeyUp
        lblLoading.Text = "Укажите разовый PIN-код сессии" & vbCrLf & "от 4 до 8 цифр"
    End Sub

    Private Sub tbxSessionPin_KeyUp(sender As Object, e As KeyEventArgs) Handles tbxSessionPin.KeyUp
        If tbxOTP.Visible = True Then
            lblLoading.Text = "Введите одноразовый код из приложения-генератора OTP"
        End If
    End Sub

End Class

