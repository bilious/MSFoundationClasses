
Public Class PopupHelper
    Public Function ShowInputBox(ByVal strPrompt As String, Optional ByVal intTYpe As Int32 = 1, Optional ByVal strTitle As String = "", Optional ByVal StrDefault As String = "") As String
        Dim oPopup As New ApplicationMessageBox
        oPopup.Prompt = strPrompt
        oPopup.Type = intTYpe
        If strTitle <> "" Then
            oPopup.Title = strTitle
        End If
        If intTYpe = 4 Then
            'StrDefault Is Used For ComboBox Values
            oPopup.Choices = StrDefault
        Else
            If StrDefault <> "" Then
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
