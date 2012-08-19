Public Class ApplicationMessageBox
    Inherits ApplicationWindow

    Private _Title As String = "Hexagon Helpdesk"
    Private _Prompt As String = ""
    Private _Type As Int32 = 1
    Private _ShowCancel As Boolean = False
    Private _ShowOk As Boolean = True
    Private _strDefault As String = ""
    Private strChoices As String = ""

    Public Property Title()
        Get
            Return _Title
        End Get
        Set(ByVal Value)
            _Title = Value
        End Set
    End Property
    Public Property Prompt()
        Get
            Return _Prompt
        End Get
        Set(ByVal Value)
            _Prompt = Value
        End Set
    End Property
    Public Property Type()
        Get
            Return _Type
        End Get
        Set(ByVal Value)
            _Type = Value
        End Set
    End Property
    Public Property DefaultValue()
        Get
            Return _strDefault
        End Get
        Set(ByVal Value)
            _strDefault = Value
        End Set
    End Property
    Public Property Choices()
        Get
            Return strChoices
        End Get
        Set(ByVal Value)
            strChoices = Value
        End Set
    End Property
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents PanelMain As ObjectKeeperPanel
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents PanelOK As System.Windows.Forms.Panel
    Friend WithEvents ButtonOK As System.Windows.Forms.Button
    Friend WithEvents LblPrompt As System.Windows.Forms.Label
    Friend WithEvents PanelOKCancel As System.Windows.Forms.Panel
    Friend WithEvents ButtonOKCancelOK As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents PBLoad As System.Windows.Forms.PictureBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(ApplicationMessageBox))
        Me.PanelMain = New ObjectKeeperPanel
        Me.PBLoad = New System.Windows.Forms.PictureBox
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.PanelOKCancel = New System.Windows.Forms.Panel
        Me.ButtonOKCancelOK = New System.Windows.Forms.Button
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.LblPrompt = New System.Windows.Forms.Label
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.PanelOK = New System.Windows.Forms.Panel
        Me.ButtonOK = New System.Windows.Forms.Button
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.PanelMain.SuspendLayout()
        Me.PanelOKCancel.SuspendLayout()
        Me.PanelOK.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelMain
        '
        Me.PanelMain.BackColor = System.Drawing.Color.White
        Me.PanelMain.Controls.Add(Me.TextBox1)
        Me.PanelMain.Controls.Add(Me.PBLoad)
        Me.PanelMain.Controls.Add(Me.ComboBox1)
        Me.PanelMain.Controls.Add(Me.PanelOKCancel)
        Me.PanelMain.Controls.Add(Me.LblPrompt)
        Me.PanelMain.Controls.Add(Me.PictureBox2)
        Me.PanelMain.Controls.Add(Me.PanelOK)
        Me.PanelMain.Location = New System.Drawing.Point(0, 0)
        Me.PanelMain.Name = "PanelMain"
        Me.PanelMain.Size = New System.Drawing.Size(384, 144)
        Me.PanelMain.TabIndex = 4
        '
        'PBLoad
        '
        Me.PBLoad.Image = CType(resources.GetObject("PBLoad.Image"), System.Drawing.Image)
        Me.PBLoad.Location = New System.Drawing.Point(48, 64)
        Me.PBLoad.Name = "PBLoad"
        Me.PBLoad.Size = New System.Drawing.Size(24, 24)
        Me.PBLoad.TabIndex = 12
        Me.PBLoad.TabStop = False
        Me.PBLoad.Visible = False
        '
        'ComboBox1
        '
        Me.ComboBox1.Location = New System.Drawing.Point(104, 72)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(184, 21)
        Me.ComboBox1.TabIndex = 11
        Me.ComboBox1.Visible = False
        '
        'PanelOKCancel
        '
        Me.PanelOKCancel.Controls.Add(Me.ButtonOKCancelOK)
        Me.PanelOKCancel.Controls.Add(Me.ButtonCancel)
        Me.PanelOKCancel.Location = New System.Drawing.Point(16, 112)
        Me.PanelOKCancel.Name = "PanelOKCancel"
        Me.PanelOKCancel.Size = New System.Drawing.Size(352, 24)
        Me.PanelOKCancel.TabIndex = 10
        Me.PanelOKCancel.Visible = False
        '
        'ButtonOKCancelOK
        '
        Me.ButtonOKCancelOK.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonOKCancelOK.Location = New System.Drawing.Point(72, 0)
        Me.ButtonOKCancelOK.Name = "ButtonOKCancelOK"
        Me.ButtonOKCancelOK.Size = New System.Drawing.Size(96, 20)
        Me.ButtonOKCancelOK.TabIndex = 4
        Me.ButtonOKCancelOK.Text = "OK"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonCancel.Location = New System.Drawing.Point(208, 0)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(96, 20)
        Me.ButtonCancel.TabIndex = 3
        Me.ButtonCancel.Text = "Cancel"
        '
        'LblPrompt
        '
        Me.LblPrompt.BackColor = System.Drawing.Color.White
        Me.LblPrompt.Location = New System.Drawing.Point(32, 40)
        Me.LblPrompt.Name = "LblPrompt"
        Me.LblPrompt.Size = New System.Drawing.Size(320, 64)
        Me.LblPrompt.TabIndex = 9
        Me.LblPrompt.Text = "Label1"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(8, 8)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(368, 32)
        Me.PictureBox2.TabIndex = 6
        Me.PictureBox2.TabStop = False
        '
        'PanelOK
        '
        Me.PanelOK.Controls.Add(Me.ButtonOK)
        Me.PanelOK.Location = New System.Drawing.Point(16, 112)
        Me.PanelOK.Name = "PanelOK"
        Me.PanelOK.Size = New System.Drawing.Size(352, 24)
        Me.PanelOK.TabIndex = 8
        Me.PanelOK.Visible = False
        '
        'ButtonOK
        '
        Me.ButtonOK.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ButtonOK.Location = New System.Drawing.Point(136, 0)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(96, 20)
        Me.ButtonOK.TabIndex = 4
        Me.ButtonOK.Text = "OK"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(32, 72)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(256, 20)
        Me.TextBox1.TabIndex = 13
        Me.TextBox1.Text = ""
        Me.TextBox1.Visible = False
        '
        'BaseMessageBox
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(392, 150)
        Me.Controls.Add(Me.PanelMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "BaseMessageBox"
        Me.Text = "BaseMessageBox"
        Me.PanelMain.ResumeLayout(False)
        Me.PanelOKCancel.ResumeLayout(False)
        Me.PanelOK.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Protected Overrides Sub OrganisePanels()
        MainPanelSet.Mid.Mid.Value = PanelMain
    End Sub

    Protected Overrides Sub DeOrganisePanels()
        MainPanelSet.Mid.Mid.Value = Nothing
    End Sub



    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        DefaultValue = "CANCEL"
        Close()
    End Sub


    Private Sub BaseMessageBox_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = _Title
        LblPrompt.Text = _Prompt
        Select Case _Type
            Case 1
                ' Normal Message Box
                PanelOK.Visible = True
            Case 2
                'OK Cancel
                PanelOK.Visible = False
                PanelOKCancel.Visible = True
            Case 3
                'Yes No
                PanelOK.Visible = False
                PanelOKCancel.Visible = True
                ButtonOKCancelOK.Text = "Yes"
                ButtonCancel.Text = "No"
            Case 4
                ComboBox1.Visible = True
                PanelOK.Visible = False
                PanelOKCancel.Visible = True
                Dim options() As String = strChoices.Split("|")
                Dim i As Int32 = 0
                For i = 0 To UBound(options)
                    ComboBox1.Items.Add(options(i))
                Next
            Case 5
                ' Loading Message Box
                PBLoad.Visible = True
                PanelOK.Visible = False
                PanelOKCancel.Visible = True
            Case 6
                'Text Input Box
                PanelOK.Visible = False
                PanelOKCancel.Visible = True
                TextBox1.Visible = True
                TextBox1.Text = _strDefault
        End Select
        Me.CenterToScreen()
    End Sub

    Private Sub ButtonOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOK.Click
        DefaultValue = "OK"
        Close()
    End Sub

    Private Sub ButtonOKCancelOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOKCancelOK.Click
        If _Type = 4 Then
            DefaultValue = ComboBox1.SelectedItem
        ElseIf _Type = 6 Then
            DefaultValue = TextBox1.Text
        Else
            DefaultValue = "OK"
        End If
        Close()
    End Sub

    Private Sub PanelOKCancel_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub
End Class
