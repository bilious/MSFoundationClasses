Public Class UserSession
    Dim oConfigReader As New ConfigReader
    Dim oUser As New ApplicationUser

    Sub New(ByVal oDataReader As IDataReader)
        oUser.UserName = oDataReader("Username")
        oUser.AuthorisationLimit = oDataReader("AuthorisationLimit")
        oUser.Status = oDataReader("Status")
        oUser.ID = oDataReader("UserID")
        Select Case oDataReader("RoleID")
            Case 3
                oUser.IsAdmin = True
            Case 2
                oUser.IsFinance = True
            Case Else
        End Select

            oUser.EngineerID = oDataReader("EngineerID")

        Scope.SetParameter("CurrentUser", oUser)
        Scope.SetParameter("Username", oDataReader("Username"))
        Scope.SetParameter("UserRole", oDataReader("RoleID"))
        Scope.SetParameter("UserID", oDataReader("UserID"))
        Scope.SetParameter("GroupID", oDataReader("GroupID"))
        Scope.SetParameter("AuthorisationLimit", oDataReader("AuthorisationLimit"))
        If IsDBNull(oDataReader("WindowsAccount")) Then
            Scope.SetParameter("WindowsAccount", "")
        Else
            oUser.WindowsAccount = oDataReader("WindowsAccount")
            Scope.SetParameter("WindowsAccount", oDataReader("WindowsAccount"))
        End If
        Scope.SetParameter("CallTab", oDataReader("CallTab"))
        Scope.SetParameter("InfoTab", oDataReader("InfoTab"))
        Scope.SetParameter("WebTab", oDataReader("WebTab"))
        Scope.SetParameter("ActionTab", oDataReader("ActionTab"))
        Scope.SetParameter("CostsTab", oDataReader("CostsTab"))
        Scope.SetParameter("PartsTab", oDataReader("PartsTab"))
        Scope.SetParameter("ChargesTab", oDataReader("ChargesTab"))
        Scope.SetParameter("CallListTab", oDataReader("CallListTab"))
        Scope.SetParameter("NotesTab", oDataReader("NotesTab"))
        Scope.SetParameter("BPTab", oDataReader("BPTab"))
        Scope.SetParameter("AuditTab", oDataReader("AuditTab"))
        Scope.SetParameter("BudgetTab", oDataReader("BudgetTab"))
        Scope.SetParameter("POTab", oDataReader("POTab"))
        Scope.SetParameter("ReportsTab", oDataReader("ReportsTab"))
        Scope.SetParameter("MyClients", oDataReader("Clients"))
        Scope.SetParameter("smarticons", oConfigReader.GetNode("userinterface/smarticons"))
        Scope.SetParameter("dreamexportpath", oConfigReader.GetSetting("dream/exportpath"))
        Scope.SetParameter("userinterfaceconfig", oConfigReader.GetSetting("userinterface"))
        Scope.SetParameter("User", oUser)
    End Sub
End Class
