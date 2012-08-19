Imports System.Windows.Forms

Public Interface IParameterPasser

    Sub SetParameter(ByVal name As String, ByVal val As Object)
    Function GetParameter(ByVal name As String) As Object

    Sub OrganisePanels()
    Sub ShowForm()
    Sub CloseForm()

    Property MainPanelSet() As ApplicationPanelSet


End Interface
