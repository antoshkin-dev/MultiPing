Imports System.Diagnostics.SymbolStore
Imports System.Linq
Imports System.Net
'Imports System.ComponentModel
'Imports System.Security.Cryptography
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web
Imports System.Web.Script.Serialization
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Window
Imports System.Xml
Imports MultiPing.MyClasses
Public Class frmMain
    Private PasswordInventWin As frmPasswordList
    Private ActionsList As New List(Of MPAction)
    Private TypeList As New List(Of MPType)
    Private EndPointsList As New List(Of MPEndPoint)
    Private SelectedRowObject As Extended_Tree.ctlTreeRow = Nothing
    Private SelectedEPID As Integer = -1
    Private Const TreeRowHeight As Integer = 18
    Private x64 As String = "" 'Преффикс 64 битной системы для добавления к путям
    Private frmWeb As frmWebBrowser
    Private PingPacked As New Dictionary(Of String, Net.NetworkInformation.Ping)
    Private CurrentCID As Integer = -1
    Private Delegate Sub UpdateFormElementD(ByRef FormElement As Object, ByRef RunningSub As Func(Of Object, Object), RunningSubArgs As Object)
    Private MenuCommands As New List(Of MenuCommand)

    Private FindDebugCounter As Integer
    ''' <summary>
    ''' Список объектов найденных через поиск узлов
    ''' </summary>
    Private Founded As List(Of MPEndPoint)
    ''' <summary>
    ''' Текущий отображаемый узел из списка найденных узлов
    ''' </summary>
    Private FoundIndex As Integer
    Private Sub UpdateFormElement(ByRef FormElement As Object, ByRef RunningSub As Func(Of Object, Object), RunningSubArgs As Object)
        If FormElement.InvokeRequired = True Then
            Dim args(2) As Object
            args(0) = FormElement
            args(1) = RunningSub
            args(2) = RunningSubArgs
            FormElement.Invoke(New UpdateFormElementD(AddressOf UpdateFormElement), args)
        Else
            'Выполняем само действие
            Dim A = RunningSub(RunningSubArgs)
        End If
    End Sub
    Private Sub frmMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If Me.WindowState <> FormWindowState.Minimized Then
            My.Settings.WindowLeft = Me.Left
            My.Settings.WindowTop = Me.Top
            My.Settings.WindowWidth = Me.Width
            My.Settings.WindowHeight = Me.Height
            My.Settings.Save()
        End If
    End Sub

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        SaveExpandedNodes()
    End Sub
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Загружаем позицию окна
        Me.Left = My.Settings.WindowLeft
        Me.Top = My.Settings.WindowTop
        Me.Width = My.Settings.WindowWidth
        Me.Height = My.Settings.WindowHeight
        If IO.Directory.Exists("c:\program files (x86)") Then x64 = " (x86)"
        tmrPinger.Interval = My.Settings.PingInterval
    End Sub


    Private Sub frmMain_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'заполняем список конфигураций
        Dim SettingsCID As Integer = -1
        If My.Settings.LastConfig <> -1 Then
            SettingsCID = My.Settings.LastConfig
            If PublicData.MPConfigurations.Exists(Function(fndobj) fndobj.CID = SettingsCID) = True Then
                CurrentCID = SettingsCID
            End If
        End If
        For Each Config As MPConfig In PublicData.MPConfigurations
            Dim LI As New ListItem(Config.Name, Config.CID)
            cbxConfigs.Items.Add(LI)
            If LI.Value = CurrentCID Then
                cbxConfigs.SelectedItem = LI
            End If
        Next
        Me.Focus()
    End Sub
    Private Sub cbxConfigs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxConfigs.SelectedIndexChanged
        'обнуляем результаты поиска, если они были
        ResetSearch()
        '

        Dim CID As Integer = CType(cbxConfigs.SelectedItem, ListItem).Value
        Dim XMLText As String = ""
        ' If My.Settings.WorkOffile = False Then
        Dim HP As New HTTPProcessor.HTTPProcessor
        HP.AddSendVar("action", "getconfig")
        HP.AddSendVar("sessid", PublicData.SessionID)
        HP.AddSendVar("cid", CID)
        Dim result As String = HP.SendHTTPRequest(PublicData.MPURL, "POST", True)
        If (Mid(result, 1, 1) = "!") Then
            ShowError(result)
            Exit Sub
        Else
            XMLText = result
        End If

        '1. Сохраняем открытые узлы  текущей конфигурации
        If CurrentCID <> -1 Then
            SaveExpandedNodes()
        End If
        '2. Загружаем новую конфигурацию
        LoadConfig(XMLText)
        My.Settings.LastConfig = CID
        My.Settings.Save()
        CurrentCID = CID
        '3. Разворачиваем дерево
        ExpandTreeFromConfig()
        '4. Сохраняем конфигурацию в архив
        If My.Settings.WorkOffile = False Then
            Dim FileStream = New IO.StreamWriter(Application.StartupPath & "\ConfigCache\" & CID & ".xml", False, System.Text.Encoding.UTF8)
            FileStream.Write(XMLText)
            FileStream.Close()
        End If
    End Sub
    Private Sub SaveExpandedNodes()
        Dim ZPT As String = ""
        Dim values As String = ""
        Dim ExpandedNods As List(Of MPEndPoint) = EndPointsList.FindAll(Function(fndobj) fndobj.Expanded = True)
        For Each EP As MPEndPoint In ExpandedNods
            values = values & ZPT & EP.EPID
            ZPT = ","
        Next
        If My.Settings.OpenedNods IsNot Nothing Then
            'Удаляем предыдущую запись о развернутых узлах
            Dim TMass() As String
            For I = 0 To My.Settings.OpenedNods.Count - 1
                TMass = Split(My.Settings.OpenedNods(I), "|")
                If TMass(0) = CStr(CurrentCID) Then
                    'My.Settings.OpenedNods.Item(I) = Nothing
                    My.Settings.OpenedNods.RemoveAt(I)
                    Exit For
                End If
            Next
        End If
        My.Settings.OpenedNods.Add(CurrentCID & "|" & values)
        Debug.Print(CurrentCID & ": " & values)
    End Sub
    Private Sub LoadConfig(XMLText As String)
        Debug.Print(XMLText)
        tmrPinger.Enabled = False
        'Очищаем ранее созданные списки
        Dim DTimer As New DebugTimer
        ActionsList.Clear()
        TypeList.Clear()
        EndPointsList.Clear()
        Dim XReader As New XmlDocument()
        Dim XMLNodes As XmlNodeList
        XReader.LoadXml(XMLText)
        REM 1. Получаем информацию о видах действий в текущей конфигурации
        XMLNodes = XReader.GetElementsByTagName("actions")
        Dim ActionsNode As XmlNode = XMLNodes(0)
        If ActionsNode IsNot Nothing Then
            For Each Action As XmlNode In ActionsNode
                With Action
                    ActionsList.Add(New MPAction(CInt(.Item("aid").InnerText), .Item("caption").InnerText, .Item("action").InnerText, CByte(.Item("ishttp").InnerText)))
                End With
            Next
        End If
        REM 2. Загружаем типы конечных точек
        XMLNodes = XReader.GetElementsByTagName("types")
        Dim TypesNode As XmlNode = XMLNodes(0)
        For Each TypeInNode As XmlNode In TypesNode
            With TypeInNode
                Dim TypeItem As New MPType(CInt(.Item("tid").InnerText), CByte(.Item("interfaces").InnerText), .Item("image").InnerText)

                'Добавляем в тип привязанные действия, если они есть
                If .Item("typeactions") IsNot Nothing Then
                    For Each TypeAction As XmlNode In .Item("typeactions")
                        Dim TAction As Integer = CInt(TypeAction.InnerText)
                        Dim NewAction As MPAction = ActionsList.Find(Function(fndobj) fndobj.AID = TAction)
                        TypeItem.AddTypeAction(NewAction)
                    Next
                End If
                'Debug.Print(TypeItem.ToString)
                TypeList.Add(TypeItem)
            End With
        Next
        REM 3. Загружаем список конечных точек
        XMLNodes = XReader.GetElementsByTagName("endpoints")
        Dim EPNode As XmlNode = XMLNodes(0)
        Dim ParentsIDsList As New Dictionary(Of Integer, Boolean) 'Для последующего проставления флага "есть дочерние узлы"
        Dim PB As Boolean = False
        For Each EndPointInNode As XmlNode In EPNode
            With EndPointInNode
                Dim ParentID As Integer = -1
                If .Item("pid").InnerText <> "" Then
                    ParentID = CInt(.Item("pid").InnerText)
                End If
                Dim NewEndPoint As New MPEndPoint
                NewEndPoint.EPID = CInt(.Item("epid").InnerText)
                NewEndPoint.ParentID = ParentID
                NewEndPoint.Name = .Item("name").InnerText
                NewEndPoint.Type = CInt(.Item("type").InnerText)
                NewEndPoint.LanIP = .Item("lanip").InnerText
                NewEndPoint.WanIP = .Item("wanip").InnerText

                'Для последующего проставления флага "есть дочерние узлы"
                If ParentID <> -1 And ParentsIDsList.TryGetValue(ParentID, PB) = False Then
                    ParentsIDsList.Add(ParentID, True)
                End If

                'Проверяем, есть ли у конечной точки сохраненные учётные записи, на которые пользователь имеет доступ
                Dim PasswordsNode As XmlNode = .SelectSingleNode("passwords")
                If PasswordsNode IsNot Nothing Then
                    'Пароли есть на точках - добавляем в справочник
                    For Each OnePassword As XmlNode In PasswordsNode.SelectNodes("password")
                        NewEndPoint.AddPassword(CInt(OnePassword.Item("pwid").InnerText), OnePassword.Item("passname").InnerText, OnePassword.Item("description").InnerText)
                    Next
                End If
                EndPointsList.Add(NewEndPoint)

                'Проверяем, есть ли у конечной точки атрибуты для выполнения комманд
                Dim AttributeNode As XmlNode = .SelectSingleNode("attributes")
                If AttributeNode IsNot Nothing Then
                    For Each OneAttribute As XmlNode In AttributeNode.SelectNodes("attribute")
                        NewEndPoint.AddAttribute(OneAttribute.Item("atname").InnerText, OneAttribute.Item("value").InnerText, OneAttribute.Item("macro").InnerText, OneAttribute.Item("macrogroup").InnerText)
                    Next
                End If

            End With
        Next
        'Перебираем все записанные ранее ParentID и ставим статус, что у них есть дети
        For Each PID As Integer In ParentsIDsList.Keys
            Dim ParentNode As MPEndPoint = GetNodeByID(PID)
            ParentNode.HasChilds = True
        Next

        Debug.Print("Parse XML config time: " & DTimer.GetTime() & "ms")
        REM 4. Рисуем дерево мультипинга
        Debug.Print("Begin paint extended tree")
        DTimer.StartTimer()
        PaintTree()
        Debug.Print("Total paint time: " & DTimer.GetTime & "ms")
        tmrPinger.Enabled = True
        REM 5. Открываем ранее развернутые узлы в этой конфигурации
        'My.Settings.OpenedNods(
    End Sub

    Private Sub trExpand(ByRef rowObject As Extended_Tree.ctlTreeRow)
        REM Обработчик нажатия на плюсик строки
        pnlTree.SuspendLayout()
        If rowObject.Expanded = True Then
            'надо показать спрятанные строки
            ShowRows(rowObject)
        Else
            HideRows(rowObject)
        End If
        pnlTree.ResumeLayout()
    End Sub
    Private Sub HideRows(ByRef rowObject As Extended_Tree.ctlTreeRow)
        REM Нужно скрыть всех дочек этого узла, потом подвинуть всех вверх, кто остался видимым
        '1. Скрываем все строки сворачиваемого узла
        Dim RowHiddened As Integer = 0 ' Сколько детей было отображено, что бы сдвинуть открытые ниже элементы вниз
        Dim LastRowID As Integer = rowObject.RowKey
        Dim RootNode As MPEndPoint = EndPointsList.Find(Function(fndobj) fndobj.EPID = LastRowID)
        HideRow(RootNode, RowHiddened, LastRowID, False)
        '2. Теперь поднимаем вверх все видимые ниже строки
        Dim DeltaTop As Integer = RowHiddened * TreeRowHeight 'насколько сократиласть область от кликнутой строки до следующей
        Dim NextTreeRow As Extended_Tree.ctlTreeRow
        Dim NextID As Integer = LastRowID + 1
        Do While Not pnlTree.Controls("treeRow" & NextID) Is Nothing
            NextTreeRow = pnlTree.Controls("treeRow" & NextID)
            If NextTreeRow.Visible = True Then
                NextTreeRow.Top = NextTreeRow.Top - DeltaTop
            End If
            NextID = NextID + 1
        Loop
    End Sub
    Private Sub HideRow(ByRef HiddingNode As MPEndPoint, ByRef RowHiddened As Integer, ByRef LastRowID As Integer, ByVal HideThisNode As Boolean)
        'Прячем текущую строку
        Dim ParentID As Integer = HiddingNode.EPID
        LastRowID = HiddingNode.TreeRowID
        Dim HiddingRow As Extended_Tree.ctlTreeRow = CType(pnlTree.Controls("treeRow" & HiddingNode.TreeRowID), Extended_Tree.ctlTreeRow)
        HiddingRow.Expanded = False
        HiddingNode.Expanded = False
        If HideThisNode = True And HiddingNode.Visible = True Then 'Нужно ли прятать строку
            HiddingNode.Visible = False
            HiddingRow.Visible = False
            RowHiddened = RowHiddened + 1
        End If
        'Прячем ее детей
        Dim ChildNodes As List(Of MPEndPoint)
        ChildNodes = EndPointsList.FindAll(Function(fndobj) fndobj.ParentID = ParentID And fndobj.Visible = True)
        For Each Child As MPEndPoint In ChildNodes
            HideRow(Child, RowHiddened, LastRowID, True)
        Next
    End Sub
    Private Sub ExpandTreeFromConfig()
        If My.Settings.OpenedNods IsNot Nothing Then
            Dim TMass() As String
            For I = 0 To My.Settings.OpenedNods.Count - 1
                TMass = Split(My.Settings.OpenedNods(I), "|")
                If TMass(0) = CStr(CurrentCID) Then 'Нашли текущую конфигурацию
                    Dim ExpandedNodsIDs() As String = Split(TMass(1), ",")
                    For Each EN As String In ExpandedNodsIDs
                        If EN <> "" Then
                            Dim NodeID As Integer = CInt(EN)
                            ExpandNode(NodeID)
                        End If
                    Next
                    Exit For
                End If
            Next
        End If
    End Sub
    Private Sub ExpandNode(NodeID As Integer)
        'PrintDebug("Начинаем разворачивать узел")
        Dim EndPoint As MPEndPoint = GetNodeByID(NodeID)
        If EndPoint Is Nothing Then Exit Sub
        If EndPoint.Expanded = True Then Exit Sub
        '1. Проверяем, что родительский узел развернут
        If EndPoint.ParentID <> -1 Then
            Dim ParentNode As MPEndPoint = EndPointsList.Find(Function(fndobj) fndobj.EPID = EndPoint.ParentID)
            If ParentNode.Expanded = False Then
                ExpandNode(ParentNode.EPID)
            End If
        End If
        '2. разворачиваем запрошенный узел
        Debug.Print("expanding  " & NodeID)
        Dim RowForExpand As Extended_Tree.ctlTreeRow = pnlTree.Controls("treeRow" & EndPoint.TreeRowID)
        RowForExpand.Expanded = True
        ShowRows(RowForExpand)
        'PrintDebug("Закончили разворачивать узел")
    End Sub
    Private Sub ShowRows(ByRef rowObject As Extended_Tree.ctlTreeRow)
        REM Нужно отобразить всех детей данного узла и сдвинуть вниз все строки с большим айди-строки
        '1. Отображаем детей и выставляем их по высоте сразу за родителем
        'pnlTree.SuspendLayout()
        Dim CurTop As Integer = rowObject.Top + TreeRowHeight
        Dim ParentID As Integer = CInt(rowObject.RowKey)
        Dim ChildShowed As Integer = 0 ' Сколько детей было отображено, что бы сдвинуть открытые ниже элементы вниз
        Dim LastRowID As Integer = 0
        Dim ThisNode As MPEndPoint = EndPointsList.Find(Function(fndobj) fndobj.EPID = ParentID)
        ThisNode.Expanded = True 'Отмечаем текущий узел, как развернутый
        Dim ChildNodes As List(Of MPEndPoint)
        ChildNodes = EndPointsList.FindAll(Function(fndobj) fndobj.ParentID = ParentID)

        PrintDebug("Выстраивание разворачиваемых узлов")
        For Each ChildEndPoint As MPEndPoint In ChildNodes
            ChildEndPoint.Visible = True
            Dim ChildRow As Extended_Tree.ctlTreeRow
            ChildRow = CType(pnlTree.Controls("treeRow" & ChildEndPoint.TreeRowID), Extended_Tree.ctlTreeRow)
            ChildRow.Top = CurTop
            'ChildRow.Visible = True
            CurTop = CurTop + TreeRowHeight
            ChildShowed = ChildShowed + 1
            LastRowID = ChildEndPoint.TreeRowID
        Next
        PrintDebug("Сдвигание видимых узлов")
        '2. Сдвигаем вниз все видимые строки с большим ID строки дерева
        Dim DeltaTop As Integer = ChildShowed * TreeRowHeight 'насколько расширелась область от кликнутой строки до следующей
        Dim NextTreeRow As Extended_Tree.ctlTreeRow
        Dim NextNodes As List(Of MPEndPoint) = EndPointsList.FindAll(Function(fndobj) fndobj.TreeRowID > LastRowID And fndobj.Visible = True)
        For Each NN As MPEndPoint In NextNodes
            NextTreeRow = pnlTree.Controls("treeRow" & NN.TreeRowID)
            NextTreeRow.Top = NextTreeRow.Top + DeltaTop
        Next
        'pnlTree.ResumeLayout()

        PrintDebug("Отображение развернутых узлов")

        For Each ChildEndPoint As MPEndPoint In ChildNodes
            Dim ChildRow As Extended_Tree.ctlTreeRow
            ChildRow = CType(pnlTree.Controls("treeRow" & ChildEndPoint.TreeRowID), Extended_Tree.ctlTreeRow)
            ChildRow.Visible = True
        Next

        '3. Запускаем пинги и перезапускаем таймер пинга
        PrintDebug("Начало обнуление пингера при развертки узла")
        tmrPinger.Enabled = False
        SendPingRequests()
        tmrPinger.Enabled = True
        PrintDebug("Закончено обнуление пингера при развертки узла")
    End Sub
    Private Sub PrintDebug(TX As String)
        Debug.Print(DateAndTime.Now.ToString & " " & DateAndTime.Now.Millisecond & ": " & TX)
    End Sub
    Private Sub PaintTree()
        pnlTree.SuspendLayout() 'Отключаем прорисовку дерева
        'Очищаем текущие объекты в панеле дерева
        For Each OldRow As Object In pnlTree.Controls
            OldRow = Nothing
        Next
        pnlTree.Controls.Clear()

        pnlTree.ResumeLayout()
        pnlTree.SuspendLayout() 'Отключаем прорисовку дерева

        GC.Collect()
        Dim CurrentTop As Integer = 0

        Dim RowID As Integer = 0
        Dim RootsList As New List(Of MPEndPoint)
        RootsList = EndPointsList.FindAll(Function(fndobj) fndobj.ParentID = -1) 'Получаем корни дерева конфигурации

        For Each Root As MPEndPoint In RootsList
            'Debug.Print(Root.Name)
            Root.Visible = True
            PaintNode(Root, RowID, CurrentTop, 0)
        Next

        pnlTree.ResumeLayout()
    End Sub
    Private Sub PaintNode(CurNode As MPEndPoint, ByRef RowID As Integer, ByRef CurrentTop As Integer, ByVal Level As Short)
        REM Наличие детей у данной ветки
        Dim HasChild As Boolean
        If EndPointsList.Exists(Function(fndobj) fndobj.ParentID = CurNode.EPID) Then
            HasChild = True
        Else
            HasChild = False
        End If

        Dim RowLeft As Integer = TreeRowHeight / 2 * Level
        CurNode.TreeRowID = RowID

        'AddTreeRow(RowID, CurNode.EPID, RowLeft, CurrentTop, CurNode.Name, CurNode.Type, HasChild, CurNode.Visible)

        AddTreeRow(CurNode, RowID, RowLeft, CurrentTop, HasChild)


        If CurNode.Visible = True Then
            'Стока была видна, поэтому добавляем сдвиг по вертикали
            CurrentTop += TreeRowHeight
        End If
        RowID += 1

        If HasChild = True Then 'Добавляем детей
            Dim Childs As New List(Of MPEndPoint)
            Childs = EndPointsList.FindAll(Function(fndobj) fndobj.ParentID = CurNode.EPID)
            For Each Child As MPEndPoint In Childs
                PaintNode(Child, RowID, CurrentTop, Level + 1)
            Next
        End If

    End Sub
    Private Sub AddTreeRow(CurNode As MPEndPoint, RowId As Integer, RowLeft As Integer, RowTop As Integer, HasChild As Boolean)
        Dim DTimer As New DebugTimer

        'DTimer.StartTimer()
        Dim NewRow As New Extended_Tree.ctlTreeRow
        'DTimer.StopTimer()
        With NewRow
            .Name = "treeRow" & RowId
            .Left = RowLeft
            .Caption = CurNode.Name 'EPID & " " & RowCaption
            .LanState = 0
            .WanState = 0
            .Top = RowTop
            .RowKey = CurNode.EPID
            .Width = pnlTree.Width - RowLeft
            .Anchor = 13
            'Выставляем параметры данной точки, согласно её типа

            Dim CurType As MPType

            CurType = TypeList.Find(Function(fndobj) fndobj.TID = CurNode.Type)


            .LanVisible = IIf(CurNode.LanIP <> "", True, False)
            .WanVisible = IIf(CurNode.WanIP <> "", True, False)

            'If CurType.Interfaces = 0 Then
            '    .LanVisible = False
            '    .WanVisible = False
            'ElseIf CurType.Interfaces = 1 Then
            '    .LanVisible = True
            '    .WanVisible = False
            'ElseIf CurType.Interfaces = 2 Then
            '    .LanVisible = False
            '    .WanVisible = True
            'ElseIf CurType.Interfaces = 3 Then
            '    .LanVisible = True
            '    .WanVisible = True
            'End If

            Try
                .TypePicture = Image.FromFile(PublicData.ImagePath & CurType.Image)
            Catch
                Debug.Print("no image " & PublicData.ImagePath & CurType.Image)
            End Try


            'Если есть дочки, то отображаем плюсик
            .HasChildren = HasChild
            .Visible = CurNode.Visible
        End With
        'Назначаем обработчики событий
        AddHandler NewRow.Expanding, AddressOf trExpand
        AddHandler NewRow.ClickRow, AddressOf TreeRowClick
        AddHandler NewRow.LanMouseIn, AddressOf StatusMouseInLAN
        AddHandler NewRow.LanMouseOut, AddressOf StatusMouseOut
        AddHandler NewRow.WanMouseIn, AddressOf StatusMouseInWAN
        AddHandler NewRow.WanMouseOut, AddressOf StatusMouseOut
        AddHandler NewRow.MouseDownRow, AddressOf TreeRowMouseDown

        pnlTree.Controls.Add(NewRow)

        'Debug.Print("row painted at " & DTimer.GetTime & "ms")
    End Sub
    Private Sub tsbRollUp_Click(sender As Object, e As EventArgs) Handles tsbRollUp.Click
        Dim Roots As List(Of MPEndPoint) = EndPointsList.FindAll(Function(fndobj) fndobj.ParentID = -1)
        pnlTree.SuspendLayout()
        For Each Root As MPEndPoint In Roots
            Dim TreeRow As Extended_Tree.ctlTreeRow = pnlTree.Controls("treeRow" & Root.TreeRowID)
            HideRows(TreeRow)
        Next
        pnlTree.ResumeLayout()
    End Sub

    Private Sub tsbExpandAll_Click(sender As Object, e As EventArgs) Handles tsbExpandAll.Click
        pnlTree.SuspendLayout()
        For Each TreeRow As Extended_Tree.ctlTreeRow In pnlTree.Controls
            If TreeRow.Expanded = False And TreeRow.HasChildren = True Then
                TreeRow.Expanded = True
                ShowRows(TreeRow)
            End If
        Next
        pnlTree.ResumeLayout()
    End Sub

    Private Sub TreeRowMouseDown(ByRef CurrentRow As Extended_Tree.ctlTreeRow, ByVal e As System.Windows.Forms.MouseEventArgs)
        'Подсвечиваем кликнутую строку
        HighlightRow(CurrentRow)
        Dim EPID As Integer = CInt(CurrentRow.RowKey)
        SelectedEPID = EPID

        If e.Button = Windows.Forms.MouseButtons.Right Then ' Правая кнопка, формируем выпадающее меню
            Dim WasMenu As Boolean = False
            '1. Очищаем старое меню
            cmActions.Items.Clear()
            MenuCommands.Clear()

            '2. Получаем данные об активных командах данной точки
            Dim EP As MPEndPoint = EndPointsList.Find(Function(fndobj) fndobj.EPID = EPID)
            Dim EPType As MPType = TypeList.Find(Function(fndobj) fndobj.TID = EP.Type)

            '3. Добавляем пункты меню

            Dim MenuActionID As Short = 0
            Dim MenuCMD As String = ""
            If EPType.TACount > 0 Then
                'Перебираем все типы действий, которые есть у даного узла
                For Each TAction As MPAction In EPType.TypeActions
                    Debug.Print("Активная команда: " & TAction.Caption)
                    If TAction.MacrosesCount > 0 Then
                        'Находим, какие групповые макросы есть в данной активности
                        'Пример активности:
                        'C:\Program Files%X64%\putty\putty.exe %IPCMD%:%SSHPORT%
                        'где IPCMD - групповой макрос, SSHPORT - одиночный макрос,
                        '64 - статичный макрос
                        Dim WasGroupMacro As Boolean = False
                        For Each M As String In TAction.MacrosList
                            If EP.HaveGroupMacros(M) = True Then
                                'У данного узла есть этот групповой макрос,
                                'поэтому создаем группу меню
                                Dim mainMenuItem As New ToolStripMenuItem(TAction.Caption & " ...")
                                'Теперь добавляем подменю из Атрибутов, у которых есть такая группа
                                Dim MacrosReplaced As Boolean = True
                                For Each A As MPAttribute In EP.Attributes
                                    If A.MacroGroup = M Then
                                        'Заменяем групповой макрос
                                        MenuCMD = Replace(TAction.ActiveCommand, "%" & M & "%", A.Value)

                                        'А затем меняем все остальные макросы
                                        If (ReplaceMacroses(MenuCMD, EP.Attributes) = False) Then
                                            'В команде остались макросы и использовать её нельзя
                                            MacrosReplaced = False
                                            Exit For
                                        End If

                                        'Записываем исполняемую команду
                                        Dim MC As New MenuCommand(MenuActionID, MenuCMD, IIf(TAction.IsHttpCommand = 0, False, True))
                                        MenuCommands.Add(MC)

                                        'Добавляем подменю
                                        Dim subMenuItem As New ToolStripMenuItem("... " & A.Name)
                                        subMenuItem.Tag = MenuActionID
                                        subMenuItem.ToolTipText = A.Value
                                        AddHandler subMenuItem.Click, Sub(sender As Object, ev As EventArgs)
                                                                          RunAction(sender.Tag)
                                                                      End Sub
                                        mainMenuItem.DropDownItems.Add(subMenuItem)
                                        MenuActionID += 1
                                    End If
                                Next
                                If MacrosReplaced = True Then 'Признак того, что не было ошибок по замене всех макросов
                                    cmActions.Items.Add(mainMenuItem)
                                    WasMenu = True
                                End If
                                WasGroupMacro = True
                                Exit For 'групповой макрос в команде может быть только один
                            End If
                        Next
                        If WasGroupMacro = False Then
                            'Если не было группового макроса, то выполняем стандартную
                            'подмену макросов и добавляем одиночный пункт меню 
                            MenuCMD = TAction.ActiveCommand
                            If ReplaceMacroses(MenuCMD, EP.Attributes) = True Then
                                Dim MC As New MenuCommand(MenuActionID, MenuCMD, TAction.IsHttpCommand)
                                MenuCommands.Add(MC)
                                Dim mainMenuItem As New ToolStripMenuItem(TAction.Caption)
                                mainMenuItem.Tag = MenuActionID
                                MenuActionID += 1
                                cmActions.Items.Add(mainMenuItem)
                                WasMenu = True
                            End If
                        End If
                    Else
                        'Добавляем меню действия, которое не имеет макросов
                        Dim MC As New MenuCommand(MenuActionID, TAction.ActiveCommand, TAction.IsHttpCommand)
                        MenuCommands.Add(MC)
                        Dim nomacroMenuItem As New ToolStripMenuItem(TAction.Caption)
                        nomacroMenuItem.Tag = MenuActionID
                        MenuActionID += 1
                        cmActions.Items.Add(nomacroMenuItem)
                        WasMenu = True
                    End If
                Next
            End If

            '4. Пункт меню "Атрибуты узла"
            cmActions.Items.Add("-")
            Dim attMenuItem As New ToolStripMenuItem("Атрибуты узла")
            attMenuItem.Tag = "showatt"
            cmActions.Items.Add(attMenuItem)
            cmActions.Items.Add("-")

            '5. Добавляем пункты меню с учётными данными
            If EP.HasPasswords = True Then
                Debug.Print("point have passwords")
                cmActions.Items.Add("--Учетные записи узла--")
                cmActions.Items(cmActions.Items.Count - 1).Enabled = False

                For Each PWD As MPPassword In EP.Passwords
                    Dim pwdMenuItem As New ToolStripMenuItem(PWD.PasswordName)
                    pwdMenuItem.Tag = "pwd" & PWD.PWID
                    pwdMenuItem.ToolTipText = PWD.Description
                    cmActions.Items.Add(pwdMenuItem)
                Next
                WasMenu = True
            End If

            If (WasMenu = True) Then cmActions.Show(CurrentRow, e.Location)
        End If
    End Sub
    Private Sub ShowWebBrowserWin(W As Integer, H As Integer, URL As String)
        If IsNothing(frmWeb) Then
            frmWeb = New frmWebBrowser
        End If
        If frmWeb.IsDisposed Then
            frmWeb = New frmWebBrowser
        End If
        'Выставляем позицию вэб окна
        frmWeb.Top = Me.Top
        frmWeb.Width = W
        frmWeb.Height = H
        If Me.Left > frmWeb.Width Then
            frmWeb.Left = Me.Left - frmWeb.Width - 5
        Else
            frmWeb.Left = Me.Left + Me.Width
        End If
        frmWeb.Visible = False
        frmWeb.Show(Me)
        frmWeb.myWebBrowser.Navigate(URL)
    End Sub
    Private Sub ShowAttributeWindow()
        Debug.Print("Отображаем атрибуты узла " & SelectedEPID)
        ShowWebBrowserWin(400, Me.Height, PublicData.AttributesURL & "?sessid=" & PublicData.SessionID & "&epid=" & SelectedEPID)
    End Sub
    Private Function ReplaceMacroses(ByRef Subject As String, AList As List(Of MPAttribute)) As Boolean
        '1. Заменяем все одиночные атрибуты
        For Each A As MPAttribute In AList

            If A.Macro <> "" Then
                Subject = Replace(Subject, "%" & A.Macro & "%", A.Value)
            End If
        Next

        '2. Заменяем статичные атрибуты 
        Subject = Replace(Subject, "%X64%", x64)
        Subject = Replace(Subject, "&gt;", ">")
        Subject = Replace(Subject, "&amp;", "&")

        '3. Делаем проверку, что все макросы заменены. Если макросы остались, то
        'команду использовать нельзя и следует вернуть ошибку
        Dim pattern As String = "%(.*?)%"
        Dim matches As MatchCollection = Regex.Matches(Subject, pattern)
        If matches.Count > 0 Then
            Debug.Print("Не найдено значение переменной макроса " & matches(0).Value)
            Return False
        End If

        Return True
    End Function
    Private Sub HighlightRow(ByRef ClickedRowObject As Extended_Tree.ctlTreeRow)
        If SelectedRowObject IsNot Nothing Then
            SelectedRowObject.Selected = False
        End If
        ClickedRowObject.Selected = True
        SelectedRowObject = ClickedRowObject
        SelectedEPID = ClickedRowObject.RowKey
    End Sub
    Private Sub cmActions_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles cmActions.ItemClicked
        If IsNumeric(e.ClickedItem.Tag) Then
            Debug.Print("number tag")
            RunAction(e.ClickedItem.Tag)
        Else
            If (IsNothing(e.ClickedItem.Tag) = False) Then
                If e.ClickedItem.Tag.ToString = "showatt" Then
                    'окно атрибутов узла
                    ShowAttributeWindow()
                ElseIf Mid((e.ClickedItem.Tag.ToString), 1, 3) = "pwd" Then
                    'открытие окна учётной записи
                    Dim PWID As Short = CShort(Mid(e.ClickedItem.Tag.ToString, 4))
                    ShowPasswordWin(PWID)
                Else
                    Debug.Print("other tag: " & e.ClickedItem.Tag)
                End If
            Else
                Debug.Print("was empty tag")
            End If
        End If
    End Sub
    Private Sub ShowPasswordWin(PWID As Short)
        REM  Получаем информацию об учётной записи
        Dim Password As PasswordData = RequestPassword(PWID)
        If IsNothing(Password) Then Exit Sub
        REM Показываем окно операций с учётными записями
        Dim PasswordWin As New frmPassword
        PasswordWin.StartPosition = FormStartPosition.CenterParent
        REM Устанавливаем значения полей показанной формы
        With PasswordWin
            .tbxPassName.Text = Password.Name
            .tbxDescription.Text = Password.Description
            .tbxUser.Text = Password.UserName
            .tbxPassword.Text = "crypted"
            .CryptedPassword = Password.CryptedPassword
            .Text = "Учётная запись: " & Password.Name
        End With
        'Debug.Print(Password.PasswordValue)
        PasswordWin.ShowDialog(Me)
        Password = Nothing
    End Sub

    Private Sub RunAction(ByVal ActId As Short)
        Dim RunCMD As MenuCommand
        RunCMD = MenuCommands.Find(Function(fndobj) fndobj.ID = ActId)
        'MsgBox(RunCMD.Command)
        'Exit Sub
        If RunCMD.IsHTTPCommand = 1 Then
            ShowWebBrowserWin(150, 150, RunCMD.Command)
            Exit Sub
        End If

        If (Mid(RunCMD.Command, 1, Len("mppammodule")) = "mppammodule") Then 'Модуль подключения через PAM
            'Сбрасываем учётную запись предыдущего подключения
            PamCls.ResetConnectionPointPassword()
            '
            PreparePAMCommand(RunCMD.Command)
            If PamCls.ConnectionType = "ssh" Or PamCls.ConnectionType = "sftp" Then
                ShowPanelUserForConnect()
            ElseIf PamCls.ConnectionType = "rdp" Then
                PamCls.ShowPamWindow()
            Else
                MsgBox("Отсутствует обработчик указанного типа соединения не задано: " & PamCls.ConnectionType)
            End If
        Else 'Обычное приложение с параметрами запуска
            Try
                Call Shell(RunCMD.Command, AppWinStyle.NormalFocus)
            Catch ex As Exception
                MsgBox("Ошибка запуска команды " & RunCMD.Command & ". " & ex.Message)
            End Try
        End If


    End Sub
    ''' <summary>
    ''' Отображение окошка ввода учётной записи для подключения
    ''' </summary>
    Private Sub ShowPanelUserForConnect()
        'Сбрасываем результаты прошлого взаимодействия с этим окном
        tbxPasswordForConnect.Text = ""
        tbxUserForConnect.Text = ""
        tbxPinForConnect.Text = ""
        lstNodeAccounts.Items.Clear()
        lstNodeAccounts.SelectedItem = Nothing
        lstNodeAccounts.Text = ""
        Dim CurNode As MPEndPoint = GetNodeByID(SelectedEPID)
        If CurNode.HasPasswords Then 'У выбранного узла есть учётные записи, доступные нам - включаем список и пинкод
            'Формируем наполнение списка учётных записей
            For Each NodeAccount As MPPassword In CurNode.Passwords
                Dim Caption As String = NodeAccount.PasswordName & IIf(NodeAccount.Description <> "", ": " & NodeAccount.Description, "")
                lstNodeAccounts.Items.Add(New ListItem(Caption, NodeAccount.PWID))
            Next
            '
            lstNodeAccounts.Enabled = True
            tbxPinForConnect.Enabled = True
            lstNodeAccounts.Focus()
        Else
            lstNodeAccounts.Enabled = False
            tbxPinForConnect.Enabled = False
            tbxUserForConnect.Focus()
        End If
        pnlUserForConnect.Visible = True

    End Sub
    Private Sub PreparePAMCommand(PamRunCMD As String)
        Dim TX As String = Mid(PamRunCMD, 13)
        Dim TMass() As String = TX.Split(":")
        'PamRunOptions = New PAMConnectionOptions With {
        '    .PAMServer = TMass(1),
        '    .ConnectionType = TMass(2),
        '    .RemoteHostIP = TMass(3),
        '    .RemoteHostName = TMass(0)
        '}
        With PamCls
            .PamServerURL = TMass(1)
            .ConnectionType = TMass(2)
            .ConnectionPoint = TMass(3)
            .EndPointName = TMass(0)
        End With
    End Sub
    Private Sub TreeRowClick(ByRef CurrentRow As Extended_Tree.ctlTreeRow)

    End Sub
    Private Sub StatusClick(ByVal WhatPress As System.Windows.Forms.MouseEventArgs)
        Debug.Print("Clicked")
    End Sub
    Private Sub StatusMouseInWAN(ByRef TreeRow As Extended_Tree.ctlTreeRow, ByVal Location As Point)
        Location.Y = Location.Y - 16
        Dim EPID As Integer = CInt(TreeRow.RowKey)
        Dim EP As MPEndPoint = EndPointsList.Find(Function(fndobj) fndobj.EPID = EPID)
        ToolTip1.Show(EP.WanIP, TreeRow, Location)
    End Sub
    Private Sub StatusMouseInLAN(ByRef TreeRow As Extended_Tree.ctlTreeRow, ByVal Location As Point)
        Location.Y = Location.Y - 16
        Dim EPID As Integer = CInt(TreeRow.RowKey)
        Dim EP As MPEndPoint = EndPointsList.Find(Function(fndobj) fndobj.EPID = EPID)
        ToolTip1.Show(EP.LanIP, TreeRow, Location)
    End Sub
    Private Sub StatusMouseOut(ByRef TreeRow As Extended_Tree.ctlTreeRow)
        ToolTip1.Hide(TreeRow)
    End Sub

    Private Sub tmrPinger_Tick(sender As Object, e As EventArgs) Handles tmrPinger.Tick
        SendPingRequests()
        SendDecoderKeepAlive()
    End Sub
    Private Sub SendDecoderKeepAlive()
        REM Отправляем KeepAlive на сервер декодера
        Dim HP As New HTTPProcessor.HTTPProcessor
        HP.AddSendVar("sessionid", DecoderSession.SessionId)

        Dim KeepAliveResult As String = HP.SendHTTPRequest(PublicData.DecoderURL & "?action=ping", "POST", True)

        If HP.WasError = True Then 'Возникла какая-то ошибка при запросе KeepAlive декодера
            lblStatus.Text = "Ошибка: нет связи с декодером"
            Exit Sub
        End If

        Dim JsonParser As New JavaScriptSerializer
        Dim JsonResult As DecoderResults
        Try
            JsonResult = JsonParser.Deserialize(Of DecoderResults)(KeepAliveResult)
        Catch
            lblStatus.Text = "Ошибка: некорректный ответ декодера. " & KeepAliveResult
            Exit Sub
        End Try

        If JsonResult.result.status = False Then
            lblStatus.Text = "Неудалось продлить сессию декодера. " & JsonResult.result.description
        End If

        lblStatus.Text = "Сессия декодера активна"
    End Sub
    Private Sub SendPingRequests()
        REM выбираем EPIDы все видимых узлов
        Dim VisibleEP As List(Of MPEndPoint) = EndPointsList.FindAll(Function(fndobj) fndobj.Visible = True)
        For Each EP As MPEndPoint In VisibleEP
            If EP.Visible = True Then
                If EP.WanIP <> "" And EP.WanIP <> "0.0.0.0" Then
                    pingMonitor(EP.WanIP)
                End If
                If EP.LanIP <> "" And EP.LanIP <> "0.0.0.0" Then
                    pingMonitor(EP.LanIP)
                End If
            End If
        Next
        VisibleEP = Nothing
    End Sub
    Private Sub pingMonitor(ByVal Host As String)
        Dim PingSender As New Net.NetworkInformation.Ping
        'Проверяем корректность ip-адреса
        Dim TMass() As String
        TMass = Split(Host, ".")
        If TMass.Length <> 4 Then
            Debug.Print("bad ip for ping: " & Host)
            Exit Sub
        End If
        For Each SubIP As String In TMass
            If IsNumeric(SubIP) = False Or CInt(SubIP) < 0 Or CInt(SubIP) > 255 Then
                Debug.Print("bad ip for ping: " & Host)
                Exit Sub
            End If
        Next
        TMass = Nothing
        'Всё ок, кидаем пинг
        If PingPacked.ContainsKey(Host) = False Then
            PingPacked.Add(Host, PingSender)
            AddHandler PingSender.PingCompleted, AddressOf eh_pingMonitorComplete
            PingSender.SendAsync(Host, 3000, Host)
        End If
    End Sub
    Private Sub eh_pingMonitorComplete(ByVal sender As Object, ByVal e As Net.NetworkInformation.PingCompletedEventArgs)
        Dim StartTime As Date
        Dim TotalTime As TimeSpan
        StartTime = DateAndTime.Now

        Dim Addr As String = e.UserState
        Dim ctrl As Control = New Control



        Dim IsLanIP, IsWanIP As Boolean
        'Определяем результат пинга: 0 - нет ответа, 1 - плохой пинг, 2 - связь ОК
        Dim PingStatus As Byte
        If e.Reply.Status = Net.NetworkInformation.IPStatus.Success Then
            If e.Reply.RoundtripTime < 1000 Then
                PingStatus = 2
            Else
                PingStatus = 1
            End If
        Else
            PingStatus = 0

        End If

        'Выставляем статус пинга у видимых элементов
        Dim HasPingedIPEP As List(Of MPEndPoint) = EndPointsList.FindAll(
            Function(fndobj) fndobj.LanIP = Addr Or fndobj.WanIP = Addr)
        ' Debug.Print("Ping complite")
        For Each PingedEP As MPEndPoint In HasPingedIPEP
            IsLanIP = IIf(PingedEP.LanIP = Addr, True, False)
            IsWanIP = IIf(PingedEP.WanIP = Addr, True, False)
            'Debug.Print(Addr & ": " & PingStatus & " L:" & IsLanIP & " W:" & IsWanIP)
            'Получаем элемент

            Dim TreeRow As Extended_Tree.ctlTreeRow = pnlTree.Controls("treeRow" & PingedEP.TreeRowID)
            'Debug.Print("try change state " & PingedEP.TreeRowID & " " & Addr)
            'ChangeState(PingedEP.TreeRowID, IsLanIP, IsWanIP, PingStatus)

            If IsLanIP = True Then
                TreeRow.LanState = PingStatus
            End If

            If IsWanIP = True Then
                TreeRow.WanState = PingStatus
            End If

            'Debug.Print(TreeRow.Caption & " rid: " & PingedEP.TreeRowID)
        Next
        'Очищаем мусор от класса пинга
        Try
            DirectCast(PingPacked(Addr), IDisposable).Dispose()
            PingPacked.Remove(Addr)
        Catch
        End Try
        TotalTime = DateAndTime.Now - StartTime

    End Sub
    Private Sub ChangeState(RowID As Integer, IsLanIP As Boolean, IsWanIP As Boolean, PingStatus As Byte)

        If pnlTree.Controls("treeRow" & RowID).InvokeRequired = True Then
            'Debug.Print("invoking")
        Else
            ' Debug.Print("no invoker needed")
            Dim TreeRow As Extended_Tree.ctlTreeRow = pnlTree.Controls("treeRow" & RowID)
            If IsLanIP = True Then
                TreeRow.LanState = PingStatus
            End If
            If IsWanIP = True Then
                'TreeRow.WanState = PingStatus
            End If
        End If
    End Sub

    Private Sub tbxSearch_Click(sender As Object, e As EventArgs) Handles tbxSearch.Click
        tbxSearch.SelectionStart = 0
        tbxSearch.SelectionLength = Len(tbxSearch.Text)
    End Sub

    Private Sub tbxSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles tbxSearch.KeyDown
        If e.KeyCode = Keys.Return Then
            FindDebugCounter = 0
            'SearchNodeByParent(SelectedEPID, tbxSearch.Text.ToLower, -1, True)
            AlternativeFind(tbxSearch.Text.ToLower)
            e.SuppressKeyPress = True
            Me.Focus()
        End If
    End Sub
    Private Sub AlternativeFind(FindWhat As String)
        Founded = New List(Of MPEndPoint)
        Founded.AddRange(EndPointsList.FindAll(Function(fndobj) SearchInString(fndobj.Name, FindWhat)))
        Founded.AddRange(EndPointsList.FindAll(Function(fndobj) SearchInString(fndobj.LanIP, FindWhat)))
        Founded.AddRange(EndPointsList.FindAll(Function(fndobj) SearchInString(fndobj.WanIP, FindWhat)))
        If Founded.Count > 0 Then
            lblInfo.Text = "Найдено: " & Founded.Count
            FoundIndex = 0
            GotoFindResult()
            pnlFindResult.Height = btnFindPrev.Top + btnFindPrev.Height + 4
        Else
            lblInfo.Text = "Искомый узел не найден"
            ResetSearch()
        End If
    End Sub
    Private Sub GotoFindResult()
        'Переходим в первый найденный узел
        ExpandNode(Founded(FoundIndex).ParentID)
        'Подсвечиваем найденную строку
        HighlightRow(pnlTree.Controls("treeRow" & Founded(FoundIndex).TreeRowID))
        'Ставим текст прокрутки результатов
        lblFindResult.Text = "Результат " & FoundIndex + 1 & " из " & Founded.Count
        'ставим текст в статусе окна
        lblInfo.Text = "Найдена: " & Founded(FoundIndex).Name
        'Прокучиваем экан до найденной позиции
        Dim RowForExpand As Extended_Tree.ctlTreeRow = pnlTree.Controls("treeRow" & Founded(FoundIndex).TreeRowID)
        pnlTree.ScrollControlIntoView(RowForExpand)
    End Sub
    Private Function GetNodeByID(NodeID As Integer) As MPEndPoint
        Return EndPointsList.Find(Function(fndobj) fndobj.EPID = NodeID)
    End Function
    Private Function SearchInString(ByVal Where As String, ByVal What As String) As Boolean
        Dim WhereL As String = Where.ToLower
        Return WhereL.Contains(What)
    End Function
    Private Sub ProcessPasswordsInvent()

        Dim PinCode As String = tbxPinCode.Text
        If tbxPinCode.InvokeRequired = True Then
            tbxPinCode.Invoke(New MethodInvoker(Sub() tbxPinCode.Text = ""))
        End If

        Dim HtmlResult As String = "<html>
<head>
<style type='text/css'>
    table
    {
        border-collapse: collapse;
        width:100%;
    }
    td, th
    {
        border: 1px solid gray;
    }
</style>
</head>
<body>
<table>
    <tr>
        <th></th>
        <th>Название узла</th>
        <th>IP-адреса</th>
        <th>Учётная запись</th>
        <th>Имя пользователя</th>
        <th>Пароль</th>
    </tr>

"
        Dim HtmlFooter As String = " 
</table>
</body>
</html>
"
        Dim TableFirstRowTemplate As String = "
<tr>
    <td>{6}</td>
    <td rowspan='{0}'>{1}</td>
    <td rowspan='{0}'>{2}</td>    
    <td>{3}</td>
    <td>{4}</td>
    <td>{5}</td>
</tr>
"
        Dim TableRowTemplate As String = "
<tr>
    <td>{3}</td>
    <td>{0}</td>
    <td>{1}</td>
    <td>{2}</td>
</tr>
"
        '
        Dim CurrentPassword As Short = 1
        Dim TotalPasswords As Short = 0
        'Считаем, сколько всего учётных записей доступно пользователю в данной конфигурации
        For Each EndPoint As MPEndPoint In EndPointsList
            If EndPoint.HasPasswords = True Then
                TotalPasswords += EndPoint.Passwords.Count
            End If
        Next
        If TotalPasswords = 0 Then Exit Sub

        If prgBar.GetCurrentParent.InvokeRequired = True Then
            prgBar.GetCurrentParent.Invoke(New MethodInvoker(Sub() prgBar.Maximum = TotalPasswords))
        End If
        If lblStatus.GetCurrentParent.InvokeRequired = True Then
            lblStatus.GetCurrentParent.Invoke(New MethodInvoker(Sub() lblInfo.Text = "Выгрузка " & TotalPasswords & " учётных записей"))
        End If
        'Перебираем все конечные точки и находим точки с учётными данными
        For Each EndPoint As MPEndPoint In EndPointsList
            If EndPoint.HasPasswords = True Then
                Dim RowSpan As Integer = EndPoint.Passwords.Count
                Dim FirstRow As Boolean = True
                For Each PWD As MPPassword In EndPoint.Passwords
                    'Запрашиваем данные учётной записи
                    Dim Password As PasswordData = RequestPassword(PWD.PWID)
                    If IsNothing(Password) Then Exit Sub
                    'расшифровываем пароль
                    Dim DecryptedPassword As String = ""
                    If PublicData.DecryptPassword(Password.CryptedPassword, PinCode, DecryptedPassword) = False Then
                        MsgBox("Продолжение выгрузки учетных записей невозможно")
                        Exit Sub
                    End If
                    'вносим учётную запись в итоговую строку вывода
                    Dim TableRow As String
                    Dim PasswordHTML As String = WebUtility.HtmlEncode(DecryptedPassword)
                    If FirstRow = True Then
                        'Первая строка, выводим данные об узле и первую учётную запись
                        Dim IPs As String = EndPoint.LanIP & "<br/>" & EndPoint.WanIP
                        TableRow = String.Format(TableFirstRowTemplate, RowSpan, EndPoint.Name, IPs, Password.Name, Password.UserName, PasswordHTML, CurrentPassword)
                        FirstRow = False
                    Else
                        TableRow = String.Format(TableRowTemplate, Password.Name, Password.UserName, PasswordHTML, CurrentPassword)
                    End If
                    If prgBar.GetCurrentParent.InvokeRequired = True Then
                        prgBar.GetCurrentParent.Invoke(New MethodInvoker(Sub() prgBar.Value = CurrentPassword))
                    End If
                    CurrentPassword += 1
                    HtmlResult &= TableRow
                Next
            End If
        Next
        HtmlResult &= HtmlFooter

        If prgBar.GetCurrentParent.InvokeRequired = True Then
            prgBar.GetCurrentParent.Invoke(New MethodInvoker(Sub() prgBar.Value = 0))
        End If


        REM Устанавливаем значения полей показанной формы
        PasswordInventWin.Invoke(New MethodInvoker(Sub()
                                                       PasswordInventWin.WebBrowser1.DocumentText = HtmlResult
                                                       PasswordInventWin.Show()
                                                   End Sub))

        'Application.Run()


    End Sub

    Private Sub tsbPasswordInvent_Click(sender As Object, e As EventArgs) Handles tsbPasswordInvent.Click
        Dim MQuastion As String = "Вы действительно хотите вывести список всех доступных вам учётных записей?" & vbNewLine
        MQuastion &= "Данная операция будет отражена в журналах аудита"
        If MsgBox(MQuastion, MsgBoxStyle.YesNo, "Выгрузка учётных записей") <> MsgBoxResult.Yes Then Exit Sub

        tbxPinCode.Text = ""
        pnlPIN.Visible = True
        tbxPinCode.Focus()


    End Sub

    Private Sub btnCancelPin_Click(sender As Object, e As EventArgs) Handles btnCancelPin.Click
        pnlPIN.Visible = False
    End Sub

    Private Sub tbxPinCode_KeyDown(sender As Object, e As KeyEventArgs) Handles tbxPinCode.KeyDown
        If (e.KeyValue = Keys.Return) Then
            PasswordInventWin = New frmPasswordList
            PasswordInventWin.Show()
            PasswordInventWin.Hide()

            Dim PwdInventThread As New Thread(AddressOf ProcessPasswordsInvent)
            PwdInventThread.SetApartmentState(ApartmentState.STA)
            PwdInventThread.Start()

            pnlPIN.Visible = False

            e.SuppressKeyPress = True
        ElseIf PublicData.CheckIsNumericKeyPress(e.KeyValue) = False Then
            'Нажали неразрешенную клавишу, отменяем ввод
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub tsbGeneratePassword_Click(sender As Object, e As EventArgs) Handles tsbGeneratePassword.Click
        pnlRandomPassword.Visible = True
        GenerateNewPassword()
    End Sub
    Private Sub GenerateNewPassword()
        tbxRandomPassword.Text = PublicData.GeneratePassword(12)
    End Sub

    Private Sub btnClosePwdPnl_Click(sender As Object, e As EventArgs) Handles btnClosePwdPnl.Click
        pnlRandomPassword.Visible = False
    End Sub

    Private Sub btnCopyPassword_Click(sender As Object, e As EventArgs) Handles btnCopyPassword.Click
        Clipboard.SetText(tbxRandomPassword.Text)
    End Sub
    Private Sub btnHideUserForConnect_Click(sender As Object, e As EventArgs) Handles btnHideUserForConnect.Click
        pnlUserForConnect.Visible = False
        tbxUserForConnect.Text = ""
    End Sub

    Private Sub btnFindNext_Click(sender As Object, e As EventArgs) Handles btnFindNext.Click
        If FoundIndex + 1 <= Founded.Count - 1 Then
            FoundIndex += 1
        Else
            FoundIndex = 0
        End If
        GotoFindResult()
    End Sub

    Private Sub btnFindPrev_Click(sender As Object, e As EventArgs) Handles btnFindPrev.Click
        If FoundIndex - 1 >= 0 Then
            FoundIndex -= 1
        Else
            FoundIndex = Founded.Count - 1
        End If
        GotoFindResult()
    End Sub

    Private Sub btnCloseFind_Click(sender As Object, e As EventArgs) Handles btnCloseFind.Click
        ResetSearch()
    End Sub

    Private Sub pnlTree_Scroll(sender As Object, e As ScrollEventArgs) Handles pnlTree.Scroll
        Debug.Print(e.NewValue)
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ResetSearch()
        pnlFindResult.Visible = True
    End Sub
    Private Sub ResetSearch()
        Founded = Nothing
        pnlFindResult.Height = tbxSearch.Top * 2 + tbxSearch.Height
        pnlFindResult.Visible = False
    End Sub

    Private Sub btnPamConnect_Click(sender As Object, e As EventArgs) Handles btnPamConnect.Click
        SetPamConPointUserAndConnect()
    End Sub
    Private Sub SetPamConPointUserAndConnect()
        If tbxUserForConnect.Text <> "" Then 'Подключение через имя пользователя и пароль
            PamCls.ConnectionPointUser = tbxUserForConnect.Text
            If (tbxPasswordForConnect.Text <> "") Then 'Указан какой-то пароль, задаем его в подключении
                PamCls.ConnectionPointPassword = tbxPasswordForConnect.Text
            End If
            PamCls.ShowPamWindow()
            ClearAfterPamConnection()
        ElseIf lstNodeAccounts.SelectedIndex <> -1 Then 'Подключение через сохраненную учетную запись
            If tbxPinForConnect.Text = "" Then
                MsgBox("Необходимо указать PIN-код сессии")
                ClearAfterPamConnection()
                Exit Sub
            End If
            'Запрашиваем пароль от учетной записи с сервера
            Dim SelItem As ListItem = CType(lstNodeAccounts.SelectedItem, ListItem)
            Dim Password As PasswordData = RequestPassword(SelItem.Value)
            If (IsNothing(Password)) Then
                MsgBox("Неудалось получить пароль выбранной учётной записи")
                ClearAfterPamConnection()
                Exit Sub
            End If
            'Пытаемся расшифровать шифрованный пароль пинкодом
            Dim DecryptedPassword As String = ""
            If PublicData.DecryptPassword(Password.CryptedPassword, tbxPinForConnect.Text, DecryptedPassword) Then
                PamCls.ConnectionPointUser = Password.UserName
                PamCls.ConnectionPointPassword = DecryptedPassword
                DecryptedPassword = Nothing
                ClearAfterPamConnection()
                PamCls.ShowPamWindow()
            Else
                MsgBox("Пин-код сессии указан неверно")
            End If
        Else
            MsgBox("Недостаточно данных для подключения")
        End If
    End Sub
    Private Sub ClearAfterPamConnection()
        tbxPasswordForConnect.Text = ""
        tbxUserForConnect.Text = ""
        pnlUserForConnect.Visible = False
        tbxPinForConnect.Text = ""
        lstNodeAccounts.Items.Clear()
        GC.Collect()
    End Sub
    Private Sub tbxPinForConnect_KeyDown(sender As Object, e As KeyEventArgs) Handles tbxPinForConnect.KeyDown
        If PublicData.CheckIsNumericKeyPress(e.KeyValue) = False Then
            'Нажали неразрешенную клавишу, отменяем ввод
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub OnlyNumbersTbx_KeyDown(sender As Object, e As KeyEventArgs) Handles tbxPinForConnect.KeyDown
        If e.KeyValue = Keys.Return Then
            SetPamConPointUserAndConnect()
            e.SuppressKeyPress = True
        ElseIf PublicData.CheckIsNumericKeyPress(e.KeyValue) = False Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub tbxPasswordForConnect_KeyDown(sender As Object, e As KeyEventArgs) Handles tbxPasswordForConnect.KeyDown
        If e.KeyValue = Keys.Return Then
            SetPamConPointUserAndConnect()
            e.SuppressKeyPress = True
        End If
    End Sub

End Class