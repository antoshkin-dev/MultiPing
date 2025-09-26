Imports System.Buffers.Text
Imports System.IO
Imports System.Linq
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.RegularExpressions

Public Class MyClasses
    Public Class ListItem
        Private _Text As String
        Private _Value As String
        Public Sub New(Text As String, Value As String)
            _Text = Text
            _Value = Value
        End Sub
        Public Property Text As String
            Get
                Return _Text
            End Get
            Set(value As String)
                _Text = value
            End Set
        End Property
        Public Property Value As String
            Get
                Return _Value
            End Get
            Set(value As String)
                _Value = value
            End Set
        End Property
        Public Overrides Function ToString() As String
            Return _Text
        End Function
    End Class
    Public Class UserCompany
        Private _UserName As String
        Private _UID As Integer
        Private _CID As Integer
        Public Property UserName As String
            Get
                Return _UserName
            End Get
            Set(value As String)
                _UserName = value
            End Set
        End Property
        Public Property UID As Integer
            Get
                Return _UID
            End Get
            Set(value As Integer)
                _UID = value
            End Set
        End Property
        Public Property CID As Integer
            Get
                Return _CID
            End Get
            Set(value As Integer)
                _CID = value
            End Set
        End Property
        Public Overloads Function Equals(CID As Integer) As Boolean
            If (_CID = CID) Then
                Return True
            Else
                Return False
            End If
        End Function
        Public Overrides Function ToString() As String
            Return _UserName
        End Function
        Public Sub New()

        End Sub
        Public Sub New(ByVal UserName As String, UID As Integer, CID As Integer)
            _UserName = UserName
            _UID = UID
            _CID = CID
        End Sub
    End Class
    Public Class MPConfig
        Private _CID As Integer
        Private _NAME As String
        Private _LastUpdate As String
        Public Property CID As Integer
            Get
                Return _CID
            End Get
            Set(value As Integer)
                _CID = value
            End Set
        End Property
        Public Property Name As String
            Get
                Return _NAME
            End Get
            Set(value As String)
                _NAME = value
            End Set
        End Property

        Public Property LastUpdate As String
            Get
                Return _LastUpdate
            End Get
            Set(value As String)
                _LastUpdate = value
            End Set
        End Property
        Sub New(CID As Integer, Name As String, LastUpdate As String)
            _CID = CID
            _NAME = Name
            _LastUpdate = LastUpdate
        End Sub
        Public Overrides Function ToString() As String
            Return _NAME
        End Function
    End Class
    Public Class MPAction
        Private _AID As Integer
        Private _Caption As String
        Private _ActiveCommand As String
        Private _ishttp As Byte
        Private _Macroses As New List(Of String)
        Public ReadOnly Property MacrosesCount As Integer
            Get
                If _Macroses Is Nothing Then Return 0
                Return _Macroses.Count
            End Get
        End Property
        Public ReadOnly Property MacrosList As List(Of String)
            Get
                Return _Macroses
            End Get
        End Property
        Public Property AID As Integer
            Get
                Return _AID
            End Get
            Set(value As Integer)
                _AID = value
            End Set
        End Property
        Public Property Caption As String
            Get
                Return _Caption
            End Get
            Set(value As String)
                _Caption = value
            End Set
        End Property
        Public Property ActiveCommand As String
            Get
                Return _ActiveCommand
            End Get
            Set(value As String)
                _ActiveCommand = value
            End Set
        End Property
        Public Property IsHttpCommand As Byte
            Get
                Return _ishttp
            End Get
            Set(value As Byte)
                _ishttp = value
            End Set
        End Property
        Public Sub New(AID, Caption, ActiveCommand, IsHTTP)
            _AID = AID
            _Caption = Caption
            _ActiveCommand = ActiveCommand
            _ishttp = IsHTTP

            REM Формируем список макросов, которые есть в комманде и определяем, есть ли они вообще
            'Регулярное выражение для поиска текста между символами процента
            Dim pattern As String = "%(.*?)%"

            ' объект Regex
            Dim matches As MatchCollection = Regex.Matches(ActiveCommand, pattern)

            ' Добавление найденных значений в массив
            For Each match As Match In matches
                _Macroses.Add(match.Groups(1).Value)
                'Debug.Print(match.Groups(1).Value)
            Next

        End Sub
        Public Function HaveMacros(Macros As String) As Boolean
            Return _Macroses.Contains(Macros)
        End Function
    End Class
    Public Class MPType
        Private _TID As Integer
        Private _Interfaces As Byte
        Private _Image As String
        'Private _TypeActions As New List(Of Integer)
        Private _TypeActions As New List(Of MPAction)
        Private _TACount As Integer = 0
        Public Property TID As Integer
            Get
                Return _TID
            End Get
            Set(value As Integer)
                _TID = value
            End Set
        End Property
        Public Property Interfaces As Byte
            Get
                Return _Interfaces
            End Get
            Set(value As Byte)
                _Interfaces = value
            End Set
        End Property
        Public Property Image As String
            Get
                Return _Image
            End Get
            Set(value As String)
                _Image = value
            End Set
        End Property
        Public ReadOnly Property TypeActions As List(Of MPAction)
            Get
                Return _TypeActions
            End Get
        End Property
        Public Sub AddTypeAction(Action As MPAction)
            _TypeActions.Add(Action)
            _TACount = _TACount + 1
        End Sub
        Public ReadOnly Property TACount() As Integer
            Get
                Return _TACount
            End Get
        End Property
        Public Sub New(TID As Integer, Interfaces As Byte, Image As String)
            _TID = TID
            _Interfaces = Interfaces
            _Image = Image
        End Sub
        Public Overrides Function ToString() As String
            Return _TID & " " & _Interfaces & " " & _Image & " TAC:" & _TACount
        End Function
    End Class
    Public Class MPEndPoint
        Private _EPID As Integer
        Private _PID As Integer
        Private _NAME As String
        Private _TYPE As Integer
        Private _LANIP As String
        Private _WANIP As String
        Private _Visible As Boolean = False
        Private _TreeRowID As Integer = 0
        Private _Expanded As Boolean = False
        Private _PasswordsList As New List(Of MPPassword)
        Private _AttributesList As New List(Of MPAttribute)

        Private _GroupMacroses As New List(Of String)
        Private _SingleMacroses As New List(Of String)
        Public Property HasChilds As Boolean = False
        Public Property HasPasswords As Boolean = False
        Public Property HasAttributes As Boolean = False
        Public Property EPID As Integer
            Get
                Return _EPID
            End Get
            Set(value As Integer)
                _EPID = value
            End Set
        End Property
        Public Property ParentID As Integer
            Get
                Return _PID
            End Get
            Set(value As Integer)
                _PID = value
            End Set
        End Property
        Public Property Name As String
            Get
                Return _NAME
            End Get
            Set(value As String)
                _NAME = value
            End Set
        End Property
        Public Property Type As Integer
            Get
                Return _TYPE
            End Get
            Set(value As Integer)
                _TYPE = value
            End Set
        End Property
        Public Property LanIP As String
            Get
                Return _LANIP
            End Get
            Set(value As String)
                _LANIP = value
            End Set
        End Property
        Public Property WanIP As String
            Get
                Return _WANIP
            End Get
            Set(value As String)
                _WANIP = value
            End Set
        End Property
        'Public Sub New(EPID As Integer, PID As Integer, Name As String, PointType As Integer, LanIP As String, WanIP As String)
        '    _EPID = EPID
        '    _PID = PID
        '    _NAME = Name
        '    _TYPE = PointType
        '    _LANIP = LanIP
        '    _WANIP = WanIP
        '    'Debug.Print(EPID & " " & PID & " " & Name & " " & PointType & " " & LanIP & " " & WanIP)
        'End Sub
        Public Overloads Function Equals(ParentID As Integer) As Boolean
            If _PID = ParentID Then
                Return True
            Else
                Return False
            End If
        End Function
        Public Property Visible As Boolean
            Get
                Return _Visible
            End Get
            Set(value As Boolean)
                _Visible = value
            End Set
        End Property
        Public Property TreeRowID As Integer
            Get
                Return _TreeRowID
            End Get
            Set(value As Integer)
                _TreeRowID = value
            End Set
        End Property
        Public Property Expanded As Boolean
            Get
                Return _Expanded
            End Get
            Set(value As Boolean)
                _Expanded = value
            End Set
        End Property
        Public Sub AddPassword(PWID As Integer, PasswordName As String, PasswordDescription As String)
            Dim NewPasswordRecord As New MPPassword()
            NewPasswordRecord.PWID = PWID
            NewPasswordRecord.PasswordName = PasswordName
            NewPasswordRecord.Description = PasswordDescription
            If _PasswordsList Is Nothing Then _PasswordsList = New List(Of MPPassword)
            _PasswordsList.Add(NewPasswordRecord)
            HasPasswords = True
        End Sub
        Public ReadOnly Property Passwords As List(Of MPPassword)
            Get
                Return _PasswordsList
            End Get
        End Property

        Public Sub AddAttribute(Name As String, Value As String, Macro As String, MacroGroup As String)
            Dim NewAttribute As New MPAttribute()
            With NewAttribute
                .Name = Name
                .Value = Value
                .Macro = Macro
                .MacroGroup = MacroGroup
            End With
            If _AttributesList Is Nothing Then _AttributesList = New List(Of MPAttribute)
            _AttributesList.Add(NewAttribute)

            'Записываем узлу название группового макроса и одиночного макроса
            If MacroGroup <> "" Then
                If _GroupMacroses IsNot Nothing Then
                    'Проверяем, что данной группы макросов нет в списке
                    If _GroupMacroses.Contains(MacroGroup) = False Then
                        _GroupMacroses.Add(MacroGroup)
                    End If
                Else
                    _GroupMacroses.Add(MacroGroup)
                End If
            End If
            If Macro <> "" Then
                If _SingleMacroses IsNot Nothing Then
                    'Проверяем, что данного макроса нет в списке
                    If _SingleMacroses.Contains(Macro) = False Then
                        _SingleMacroses.Add(Macro)
                    End If
                Else
                    _SingleMacroses.Add(Macro)
                End If
            End If

            HasAttributes = True
        End Sub
        Public Function HaveGroupMacros(GroupMacros As String) As Boolean
            If _GroupMacroses Is Nothing Then Return False
            Return _GroupMacroses.Contains(GroupMacros)
        End Function
        Public Function HaveMacros(Macros As String) As Boolean
            If _SingleMacroses Is Nothing Then Return False
            Return _SingleMacroses.Contains(Macros)
        End Function
        Public ReadOnly Property Attributes As List(Of MPAttribute)
            Get
                Return _AttributesList
            End Get
        End Property
    End Class
    Public Class MenuCommand
        Public Property ID As Integer
        Public Property Command As String
        Public Property IsHTTPCommand As Boolean = False
        Public Sub New(CMDID As Integer, ExCommand As String, IsHTTP As Boolean)
            ID = CMDID
            Command = ExCommand
            IsHTTPCommand = IsHTTP
        End Sub
    End Class
    Public Class DebugTimer
        Private _start As Date
        Private _end As Date
        Private _timerrunned As Boolean = False

        ''' <summary>
        ''' Таймер запускается автоматически при инициализации класса
        ''' </summary>
        Public Sub New()
            StartTimer()
        End Sub

        ''' <summary>
        ''' Повторный запуск таймера
        ''' </summary>
        Public Sub StartTimer()
            _start = System.DateTime.Now
            _timerrunned = True
        End Sub

        ''' <summary>
        ''' Остановка таймера и фиксация времени
        ''' </summary>
        Public Sub StopTimer()
            _end = System.DateTime.Now
            _timerrunned = False
        End Sub

        ''' <summary>
        ''' Возврат затраченного времени в милисекундках
        ''' </summary>
        ''' <returns></returns>
        Public Function GetTime() As Long
            If _timerrunned = True Then StopTimer()
            Dim StartMS As Long = DateToLong(_start)
            Dim EndMS As Long = DateToLong(_end)
            Dim Result As Long = EndMS - StartMS
            Return Result
        End Function

        ''' <summary>
        ''' Перевод переменной времени из формата даты в милисекунды
        ''' </summary>
        ''' <param name="TM"></param>
        ''' <returns></returns>
        Private Function DateToLong(TM As Date) As Long
            Dim Result As Long
            Dim SecT, MinT, HourT As Long
            SecT = TM.Second
            MinT = TM.Minute
            HourT = TM.Hour
            Result = TM.Millisecond + SecT * 1000 + MinT * 60000 + HourT * 3600000
            Return Result
        End Function
    End Class

    Public Class MPPassword
        Private _PWID As Integer
        Private _PasswordName As String
        Private _Description As String
        Public Property PWID As Integer
            Get
                Return _PWID
            End Get
            Set(value As Integer)
                _PWID = value
            End Set
        End Property
        Public Property PasswordName As String
            Get
                Return _PasswordName
            End Get
            Set(value As String)
                _PasswordName = value
            End Set
        End Property
        Public Property Description As String
            Get
                Return _Description
            End Get
            Set(value As String)
                _Description = value
            End Set
        End Property
    End Class

    Public Class MPAttribute
        Public Property Name As String
        Public Property Value As String
        Public Property Macro As String
        Public Property MacroGroup As String
    End Class

    Public Class DecoderResults
        Public Property result As DecoderResultStatus
        Public Property data As JsonDecoderUserSession 'As Object ' = Nullable
    End Class
    Public Class DecoderResultStatus
        Public Property status As Boolean
        Public Property description As String
    End Class

    ''' <summary>
    ''' Полученные данные сессии из JSON-ответа от Decoder
    ''' </summary>
    Public Class JsonDecoderUserSession
        ''' <summary>
        ''' Код сессии декодера
        ''' </summary>
        ''' <returns></returns>
        Public Property sessionid As String

        ''' <summary>
        ''' Ключ сессии декодера
        ''' </summary>
        ''' <returns></returns>
        Public Property sessionkey As String
    End Class
    ''' <summary>
    ''' Данные сессии декодера. Ключ сессии декодера закрыт пин-кодом при авторизации
    ''' </summary>
    Public Class DecoderUserSession

        ''' <summary>
        ''' Код сессии декодера
        ''' </summary>
        Public SessionId As String

        ''' <summary>
        ''' Ключ сессии декодера закрытый пин-кодом после успешной авторизации
        ''' </summary>
        Public SessionKeyClosedByPin As String
    End Class

    ''' <summary>
    ''' Класс для выполнения шифрования и дешифрования текста алгоритмом AES128-CBC
    ''' </summary>
    Public Class MyCryptor
        REM Для работы с PHP
        'Using (var aes = New AesManaged())
        '{
        '    Byte[] key = Encoding.ASCII.GetBytes("YELLOW SUBMARINE");
        '    Aes.Key = key;
        '    Aes.Mode = CipherMode.ECB;

        '    String b64 = File.ReadAllText("7.txt");
        '    Byte[] bytes = Convert.FromBase64String(b64);

        '    Using (var decryptor = aes.CreateDecryptor())
        '    {
        '        Byte[] result = decryptor.TransformFinalBlock(bytes, 0, bytes.Length);
        '        String Text = Encoding.ASCII.GetString(Result);
        '    }
        '}

        ''' <summary>
        ''' Расшифровка шифрованного AES128 CBC текста при помощи ключа шифрования
        ''' </summary>
        ''' <param name="HashedString">Зашифрованный текст в формате Base64</param>
        ''' <param name="Key">Ключ, которым зашифрован текст</param>
        ''' <param name="Result">Результат дешифровки</param>
        ''' <returns>Boolean</returns>
        Public Function DeCrypt(HashedString As String, Key As String, ByRef Result As String) As Boolean
            Using myAES As Aes = Aes.Create()
                myAES.Mode = CipherMode.CBC
                myAES.KeySize = 128
                myAES.BlockSize = 128
                myAES.Padding = PaddingMode.PKCS7
                myAES.Key = PublicData.StringToByteArray(Key)

                'Debug.Print("Session key: " & Convert.ToBase64String(myAES.Key))

                ' Переводим полученное сообщение в массив байт
                Dim CryptedBytes() As Byte = Convert.FromBase64String(HashedString)

                REM Теперь надо вычлинить вектор, чексумму и полезную нагрузку
                'IV
                Dim IVBytes(15) As Byte
                Array.Copy(CryptedBytes, 0, IVBytes, 0, 16)
                Debug.Print("IV B64: " & Convert.ToBase64String(IVBytes))
                myAES.IV = IVBytes

                'Source CheckSum
                Dim CheckSumBytes(31) As Byte
                Array.Copy(CryptedBytes, 16, CheckSumBytes, 0, 32)
                Dim C1 As String = Convert.ToBase64String(CheckSumBytes)
                Debug.Print("CheckSum B64: " & C1)

                'Payload
                Dim Payload(CryptedBytes.Length - 49) As Byte
                Array.Copy(CryptedBytes, 48, Payload, 0, CryptedBytes.Length - 48)
                Debug.Print("Payload B64: " & Convert.ToBase64String(Payload))

                'Проверка чексуммы сообщения и текущей
                Dim GeneratedCheckSum() As Byte = CalculateHMAC(myAES.Key, Payload)
                Dim C2 As String = Convert.ToBase64String(GeneratedCheckSum)
                Debug.Print("Calculated checksum: " & C2)

                If C1 <> C2 Then
                    Debug.Print("Чек-суммы неверны")
                    'MsgBox("Ключ авторизации не подходит для открытия пароля", MsgBoxStyle.Critical, "Ошибка декодирования")
                    Return False
                Else
                    Debug.Print("Чек-суммы совпали")
                End If

                'Создаем дикриптора
                Dim myDecryptor As ICryptoTransform = myAES.CreateDecryptor
                '        Byte[] result = decryptor.TransformFinalBlock(bytes, 0, bytes.Length);
                '        String Text = Encoding.ASCII.GetString(Result);
                Dim ResultBytes() As Byte = myDecryptor.TransformFinalBlock(Payload, 0, Payload.Length)
                'byteArray = Encoding.UTF8.GetBytes(SendData)
                Debug.Print("password byte len: " & ResultBytes.Length)
                Result = Encoding.UTF8.GetString(ResultBytes)
                Return True
            End Using
        End Function
        Private Function CalculateHMAC(ByRef myKey() As Byte, ByRef BytesForHash() As Byte) As Byte()
            Dim HmacBytes() As Byte
            Dim myHMAC As New HMACSHA256(myKey)
            HmacBytes = myHMAC.ComputeHash(BytesForHash)
            'Debug.Print("CheckSum lenght:" & HmacBytes.Length)
            'Debug.Print("CheckSum B64:" & Convert.ToBase64String(HmacBytes))
            Return HmacBytes
        End Function
        ''' <summary>
        ''' Шифрование текста по алгоритму AES128 CBC при помощи заданного ключа
        ''' </summary>
        ''' <param name="TextForCrypt">Текст, который будет зашифрован</param>
        ''' <param name="Key">Ключ в формате SHA256, который будет использоваться для шифрования</param>
        ''' <returns>Base64 Encoded String</returns>
        Public Function Crypt(TextForCrypt As String, Key As String) As String
            Dim CryptedBytes() As Byte
            Using myAES As Aes = Aes.Create()

                myAES.Mode = CipherMode.CBC
                myAES.KeySize = 128
                myAES.BlockSize = 128
                myAES.Padding = PaddingMode.PKCS7

                'myAES.Key = Encoding.ASCII.GetBytes(Key)
                myAES.Key = PublicData.StringToByteArray(Key) ' Encoding.ASCII.GetBytes(Key)
                Debug.Print("AES key lenght: " & myAES.Key.Length)
                Debug.Print("AES key b64: " & Convert.ToBase64String(myAES.Key))
                myAES.GenerateIV()

                Debug.Print("IV len: " & myAES.IV.Length)
                Debug.Print("IV b64: " & Convert.ToBase64String(myAES.IV))

                Dim Cryptor As ICryptoTransform = myAES.CreateEncryptor() 'myAES.Key, myAES.IV)

                Using msEncrypt As New MemoryStream
                    Using csEncrypt As New CryptoStream(msEncrypt, Cryptor, CryptoStreamMode.Write)
                        Using swEncrypt As New StreamWriter(csEncrypt)
                            swEncrypt.Write(TextForCrypt)
                        End Using
                        CryptedBytes = msEncrypt.ToArray()
                    End Using
                End Using

                'Генерим проверочную сумму
                Dim HmacBytes() As Byte = CalculateHMAC(myAES.Key, CryptedBytes)
                'Dim myHMAC As New HMACSHA256(myAES.Key)
                'HmacBytes = myHMAC.ComputeHash(CryptedBytes)
                'Debug.Print("CheckSum lenght:" & HmacBytes.Length)
                'Debug.Print("CheckSum B64:" & Convert.ToBase64String(HmacBytes))

                'Собираем всю информацию в единый массив байтов IV + HMAC + MSG
                Dim ResultBytes(myAES.IV.Length + HmacBytes.Length + CryptedBytes.Length - 1) As Byte

                Debug.Print("CryptedMSG B64:" & Convert.ToBase64String(CryptedBytes))

                myAES.IV.CopyTo(ResultBytes, 0)

                HmacBytes.CopyTo(ResultBytes, myAES.IV.Length)

                CryptedBytes.CopyTo(ResultBytes, myAES.IV.Length + HmacBytes.Length)

                Return Convert.ToBase64String(ResultBytes)
            End Using
        End Function
    End Class

    Public Class PasswordData
        Public Name As String
        Public Description As String
        Public UserName As String
        Public PasswordValue As String
        Public CryptedPassword As String
    End Class

    REM Класс пин-кода
    Public Class PinCode
        Private WhitePaper As String
        Private PinSHA As String
        Public Sub New(ByRef NewPin As String)
            REM Генерим случайные цифры что бы не хранить прям открытый пин в памяти
            Dim NumGen As New Random

            'Генерил случайную последовательность
            WhitePaper = PublicData.GeneratePassword(16)
            Debug.Print("Pin WP: " & WhitePaper)

            'Генерим хеш введеного пинкода
            PinSHA = PublicData.getSHAHash(WhitePaper & NewPin)
            Debug.Print("Pin SHA: " & PinSHA)

        End Sub
        Public Function CheckPin(PinForCheck As String)
            Dim CheckHash As String = PublicData.getSHAHash(WhitePaper & PinForCheck)
            If CheckHash = PinSHA Then
                Return True
            Else
                Return False
            End If
        End Function

    End Class
End Class
