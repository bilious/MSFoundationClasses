Public Class ApplicationWindow
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

  

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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
        Me.Text = "ApplicationWindow"
    End Sub

#End Region

  


    Private blnShowPopup As Boolean = False

    Public Shadows Event Load As EventHandler
    Private oMainPanelSet As ApplicationPanelSet
    Protected oParameterTable As New Hashtable
   
    Public Sub New()
        MyBase.New()

        oMainPanelSet = Scope.GetParameter("MainPanelSet")

    End Sub

    Public Function GetParameter(ByVal name As String) As Object 'Implements IPanelProvider.GetParameter
        Return oParameterTable(name)
    End Function

    Public Sub SetParameter(ByVal name As String, ByVal val As Object) 'Implements IPanelProvider.SetParameter
        oParameterTable(name) = val
    End Sub
    Public ReadOnly Property MainPanelSet() As ApplicationPanelSet
        Get
            Return oMainPanelSet
        End Get
        'Set(ByVal Value As PanelSet)
        '    oMainPanelSet = Value
        'End Set
    End Property



    Protected Overridable Sub OrganisePanels()
    End Sub
    Protected Overridable Sub DeOrganisePanels()
    End Sub
    Protected Overridable Sub ValidateInputs()
    End Sub




    Public Sub This_Form_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        If Not blnShowPopup Then
            OrganisePanels()
        End If

        RaiseEvent Load(Me, EventArgs.Empty)

    End Sub

    Public Sub ShowPaneled(ByVal pnlSet As ApplicationPanelSet)
        oMainPanelSet = pnlSet
        Show()
    End Sub


    Public Shadows Sub Show()


        OrganisePanels()

        RaiseEvent Load(Me, EventArgs.Empty)

    End Sub

    Public Shadows Sub ShowDialog()

        blnShowPopup = True
        MyBase.ShowDialog()

    End Sub


    Public Shadows Sub Close()
        If Not blnShowPopup Then
            DeOrganisePanels()
        Else
            MyBase.Close()
        End If
    End Sub

    Public Sub ThrowException(ByVal message As String)
        Throw New Exception(message)
    End Sub

    Public Function ShowInputBox(ByVal strPrompt As String, Optional ByVal intTYpe As Int32 = 1, Optional ByVal strTitle As String = "", Optional ByVal StrDefault As String = "") As String
        Dim oPopup As New ApplicationMessageBox
        oPopup.Prompt = strPrompt
        oPopup.Type = intTYpe
        If strTitle <> "" Then
            oPopup.Title = strTitle
        End If
        If StrDefault <> "" Then
            If oPopup.Type = 4 Then
                oPopup.Choices = StrDefault
            Else
                oPopup.DefaultValue = StrDefault
            End If

        End If
        oPopup.ShowDialog()

        Return oPopup.DefaultValue

    End Function
    Public Function ShowMessageBox(ByVal strPrompt As String, Optional ByVal intTYpe As Int32 = 1, Optional ByVal strTitle As String = "") As String
        Dim oPopup As New ApplicationMessageBox
        oPopup.Prompt = strPrompt
        oPopup.Type = intTYpe
        If strTitle <> "" Then
            oPopup.Title = strTitle
        End If
        oPopup.ShowDialog()
        Return oPopup.DefaultValue
    End Function
End Class
