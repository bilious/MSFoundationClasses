Imports System.Windows.Forms

Public Class ApplicationPanelSet

    Private oLeftContainer As ApplicationPanelSet
    Private oRightContainer As ApplicationPanelSet
    Private oTopContainer As ApplicationPanelSet
    Private oBottomContainer As ApplicationPanelSet
    Private oMidContainer As ApplicationPanelSet

    Private oMainPanel As Panel
    Private oBackupHistory As New Stack
    Private oFormHistory As New Stack
    Private blnLocked As Boolean = False


    Public Sub New()
    End Sub

    Public Sub LockRecursively(ByVal panel As ApplicationPanelSet)
        If Not panel Is Nothing Then
            panel.LockInternal()
        End If
    End Sub

    Public Sub LockInternal()
        blnLocked = True

        LockRecursively(oLeftContainer)
        LockRecursively(oRightContainer)
        LockRecursively(oTopContainer)
        LockRecursively(oBottomContainer)
        LockRecursively(oMidContainer)
    End Sub

    Public Sub Lock()

        LockInternal()

        Scope.SetParameter("MainPanelSet", Me)

    End Sub


    Private Sub CreateIfNull(ByRef value As ApplicationPanelSet)
        If value Is Nothing Then
            value = New ApplicationPanelSet
        End If
    End Sub


    Public Sub RestoreControls()

        Dim oBackedUpControls As ArrayList
        oMainPanel.Controls.Clear()

        oBackedUpControls = oBackupHistory.Pop()
        Dim oEnum As IEnumerator = oBackedUpControls.GetEnumerator()
        While oEnum.MoveNext
            oMainPanel.Controls.Add(oEnum.Current)
        End While
    End Sub


    Public Sub BackupControls()
        Dim oBackedUpControls As New ArrayList

        Dim oEnum As IEnumerator = oMainPanel.Controls.GetEnumerator()
        While oEnum.MoveNext
            oBackedUpControls.Add(oEnum.Current)
        End While

        oBackupHistory.Push(oBackedUpControls)

    End Sub

    Public Sub AddPanelToControls(ByRef value As Panel)
        If Not value Is Nothing Then
            value.Dock = DockStyle.Fill
            oMainPanel.Controls.Clear()
            oMainPanel.Controls.Add(value)
        End If
    End Sub

    Public Property Value() As Panel
        Get
            Return oMainPanel
        End Get
        Set(ByVal Value As Panel)
            If oMainPanel Is Nothing Then
                If Not blnLocked Then
                    oMainPanel = Value
                End If
            ElseIf Value Is Nothing Then
                RestoreControls()
            Else
                BackupControls()
                AddPanelToControls(Value)
            End If
        End Set
    End Property
    Public Property Bottom() As ApplicationPanelSet
        Get
            CreateIfNull(oBottomContainer)
            Return oBottomContainer
        End Get
        Set(ByVal Value As ApplicationPanelSet)
            oBottomContainer = Value
            'oBottomContainer.oMainPanel.Dock = DockStyle.Bottom
        End Set
    End Property
    Public Property Mid() As ApplicationPanelSet
        Get
            CreateIfNull(oMidContainer)
            Return oMidContainer
        End Get
        Set(ByVal Value As ApplicationPanelSet)
            oMidContainer = Value
            'oMidContainer.oMainPanel.Dock = DockStyle.Fill
        End Set
    End Property
    Public Property Left() As ApplicationPanelSet
        Get
            CreateIfNull(oLeftContainer)
            Return oLeftContainer
        End Get
        Set(ByVal Value As ApplicationPanelSet)
            oLeftContainer = Value
            'oLeftContainer.oMainPanel.Dock = DockStyle.Left
        End Set
    End Property
    Public Property Right() As ApplicationPanelSet
        Get
            CreateIfNull(oRightContainer)
            Return oRightContainer
        End Get
        Set(ByVal Value As ApplicationPanelSet)
            oRightContainer = Value
            'oRightContainer.oMainPanel.Dock = DockStyle.Right
        End Set
    End Property
    Public Property Top() As ApplicationPanelSet
        Get
            CreateIfNull(oTopContainer)
            Return oTopContainer
        End Get
        Set(ByVal Value As ApplicationPanelSet)
            oTopContainer = Value
            'oTopContainer.oMainPanel.Dock = DockStyle.Top
        End Set
    End Property


End Class
