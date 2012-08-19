Public Class Scope

    Private Shared oScopeTable As New Hashtable

    Public Shared Function SetParameter(ByVal name As String, ByVal value As Object)

        SyncLock oScopeTable
            oScopeTable(name) = value
        End SyncLock

    End Function

    Public Shared Function GetParameter(ByVal name As String) As Object

        Dim oValue As Object = Nothing

        SyncLock oScopeTable
            oValue = oScopeTable(name)
        End SyncLock

        Return oValue

    End Function


End Class
