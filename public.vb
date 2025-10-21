Imports System.Security.Cryptography
Imports System.Text
Imports System.Xml
Imports MultiPing.MyClasses
Module PublicData
    Public PamCls As New PAMConnector.PAMConnector
    Public SessionID As String = ""
    Public MPConfigurations As New List(Of MPConfig)
    Public MPURL As String = My.Settings.ServerURL & "/multiping/clientexchanger.php"
    Public AttributesURL As String = My.Settings.ServerURL & "/multiping/endpointinfo.php"
    Public DecoderURL As String = My.Settings.DecoderURL & "/processor.php"
    Public ImagePath As String = Application.StartupPath & "\images\"
    Public DecoderSession As New DecoderUserSession
    ' Public SessionPinCode As PinCode
    Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New frmLogin())
    End Sub
    Public Function StringToByteArray(HexedString As String)
        Dim NumberChars As Integer = HexedString.Length
        Dim bytes((NumberChars / 2) - 1) As Byte
        For I = 0 To NumberChars - 1 Step 2
            bytes(I / 2) = Convert.ToByte(HexedString.Substring(I, 2), 16)
        Next
        Return bytes
    End Function

    Public Function getSHAHash(ByVal strToHash As String) As String

        Dim sha256Obj As New Security.Cryptography.SHA256CryptoServiceProvider '.SHA1CryptoServiceProvider
        Dim bytesToHash() As Byte = System.Text.Encoding.ASCII.GetBytes(strToHash)

        bytesToHash = sha256Obj.ComputeHash(bytesToHash)

        Dim strResult As String = ""

        For Each b As Byte In bytesToHash
            strResult += b.ToString("x2")
        Next

        Return strResult

    End Function

    Public Function RequestPassword(ByVal PWID As Short) As PasswordData
        Dim HP As New HTTPProcessor.HTTPProcessor
        HP.AddSendVar("action", "getpassword") 'Действие - получить пароль
        HP.AddSendVar("sessid", PublicData.SessionID) 'Код сессии авторизации Multiping
        HP.AddSendVar("decodeid", PublicData.DecoderSession.SessionId) ' Код авторизации на декодере
        HP.AddSendVar("pwid", PWID) 'ID запрашиваемого пароля
        Dim result As String = HP.SendHTTPRequest(PublicData.MPURL, "POST", True)
        If (Mid(result, 1, 1) = "!") Or result = "" Then
            ShowError(result)
            Return Nothing
        End If
        REM Разбираем XML-ответ
        Dim XReader As New XmlDocument()
        Dim XMLNodes As XmlNodeList
        XReader.LoadXml(result)
        REM 1. Получаем информацию о видах действий в текущей конфигурации
        XMLNodes = XReader.GetElementsByTagName("passworddata")

        Dim PasswordCrypted As String = XMLNodes(0).Item("password").InnerText

        Dim Password As New PasswordData With {
            .Name = XMLNodes(0).Item("passname").InnerText,
            .Description = XMLNodes(0).Item("description").InnerText,
            .UserName = XMLNodes(0).Item("username").InnerText,
            .PasswordValue = "",
            .CryptedPassword = PasswordCrypted
        }
        Return Password
    End Function

    Public Function DecryptPassword(CryptedPassword As String, PinCode As String, ByRef Result As String) As Boolean
        Dim MyDecoder As New MyCryptor()
        Dim CryptKey As String = ""
        If MyDecoder.DeCrypt(PublicData.DecoderSession.SessionKeyClosedByPin, Strings.Left(getSHAHash(PinCode), 32), CryptKey) = False Then
            MsgBox("Пин-код указан неверно")
            Return False
        End If

        Dim PasswordValue As String = ""
        Debug.Print("CryptKey: " & CryptKey)
        If MyDecoder.DeCrypt(CryptedPassword, CryptKey, PasswordValue) = False Then
            MsgBox("Некорректный ключ декодера. Расшифровка невозможна", MsgBoxStyle.Critical, "Ошибка декодирования")
            Return False
        End If

        Result = PasswordValue
        Return True
    End Function

    Public Sub ShowError(TX As String)
        MsgBox("Что-то пошло не так... " & TX)
    End Sub


    Public Function GeneratePassword(length As Integer) As String
        Dim Password As New StringBuilder()
        Dim Random As New Random()
        Dim CharLevels(3) As String
        CharLevels(0) = "abcdefghijklmnopqrstuvwxyz"
        CharLevels(1) = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        CharLevels(2) = "0123456789"
        CharLevels(3) = "!@#$%^&*_-+?.=<>"

        For i As Integer = 1 To length
            Dim CharType As Integer
            Dim SymbolType As Integer = Random.Next(11)
            'CharType = Random.Next(3) 'Равноправный рандом выбора символа
            If SymbolType <= 1 Then 'Появление спецсимвола - Два к 10
                CharType = 3
            ElseIf SymbolType >= 2 And SymbolType <= 4 Then 'Появление цифры - три к десяти
                CharType = 2
            ElseIf SymbolType >= 5 And SymbolType <= 7 Then 'Появление буквы - три к восьми
                CharType = 0
            Else
                CharType = 1
            End If

            Dim Characters As String = CharLevels(CharType)
            Dim Index As Integer = Random.Next(Characters.Length)
            Password.Append(Characters(Index))
        Next
        Return Password.ToString()
    End Function

    ''' <summary>
    ''' Функция проверяет введена ли с клавиатуры цифра.
    ''' Разрешены клавишы стрелок, бакспейса и делита
    ''' Возвращает True, если была нажата цифровая клавиша;
    ''' False если нажата любая другая клавиша
    ''' </summary>
    ''' <param name="KeyCode">Код нажатой клавиши</param>
    ''' <returns>Boolean</returns>
    Public Function CheckIsNumericKeyPress(KeyCode As Integer) As Boolean
        If (KeyCode <> 8 And KeyCode <> 37 And KeyCode <> 39) And (KeyCode < 48 Or KeyCode > 57) And (KeyCode < 96 Or KeyCode > 105) Then
            Return False
        Else
            Return True
        End If
    End Function
End Module
