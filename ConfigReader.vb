Imports System.Windows.Forms
Imports System.Xml

Public Class ConfigReader

    Private oXmlReader As XmlReader

    Public Sub New()

        oXmlReader = New XmlReader(AppDomain.CurrentDomain.BaseDirectory & "..\config\HelpDesk.config")

    End Sub

    Public Function GetBaseWebPath() As String

        Dim oPathNode As String = ""

        Try
            oPathNode = oXmlReader.GetNode("/configuration/application/web/basepath").Attributes("value").Value
        Catch ex As Exception
        End Try

        Return oPathNode

    End Function
    Public Function GetSetting(ByVal NodeName As String) As String
        Dim oPathNode As String = ""

        Try
            oPathNode = oXmlReader.GetNode("/configuration/application/" & NodeName).Attributes("value").Value
        Catch ex As Exception
        End Try

        Return oPathNode

    End Function
    Public Function GetDefaultSetting(ByVal NodeName As String) As String
        Dim oPathNode As String = ""

        Try
            oPathNode = oXmlReader.GetNode("/configuration/application/default/" & NodeName).Attributes("value").Value
        Catch ex As Exception
        End Try

        Return oPathNode

    End Function
    Public Function GetNode(ByVal NodeName As String) As XmlNode
        Try
            Dim oNode As XmlNode = oXmlReader.GetNode("/configuration/application/" & NodeName)
            Return oNode
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function GetNodeHash(ByVal NodeName As String) As Hashtable
        Dim oHash As New Hashtable
        Try
            Dim oNode As XmlNode = oXmlReader.GetNode("/configuration/application/" & NodeName)
            If oNode.HasChildNodes Then
                Dim oENum As IEnumerator = oNode.ChildNodes.GetEnumerator
                While oENum.MoveNext
                    Dim strName As String = ""
                    Dim strValue As String = ""
                    If Not oENum.Current.Attributes("name") Is Nothing Then
                        strName = oENum.Current.Attributes("name").Value
                    Else
                        strName = ""
                    End If
                    If Not oENum.Current.Attributes("value") Is Nothing Then
                        strValue = oENum.Current.Attributes("value").Value
                    Else
                        strValue = ""
                    End If
                    oHash.Add(strName, strValue)
                End While
            End If
        Catch ex As Exception
        End Try
        Return oHash
    End Function

    Public Function PopulateInitials(ByVal userName As TextBox, ByVal passWord As TextBox) As String

        Dim oSelectedDatabase As String = Nothing
        Try

            Dim oEnum As IEnumerator = oXmlReader.GetNodes("/configuration/application/login").GetEnumerator
            While oEnum.MoveNext
                If Not CType(oEnum.Current, Xml.XmlNode).Attributes("username") Is Nothing Then
                    userName.Text = CType(oEnum.Current, Xml.XmlNode).Attributes("username").Value
                End If
                If Not CType(oEnum.Current, Xml.XmlNode).Attributes("password") Is Nothing Then
                    passWord.Text = CType(oEnum.Current, Xml.XmlNode).Attributes("password").Value
                End If
                If Not CType(oEnum.Current, Xml.XmlNode).Attributes("database") Is Nothing Then
                    oSelectedDatabase = CType(oEnum.Current, Xml.XmlNode).Attributes("database").Value
                End If

            End While
        Catch ex As Exception
            userName.Text = ""
            passWord.Text = ""
            oSelectedDatabase = Nothing
        End Try

        Return oSelectedDatabase

    End Function


    Public Sub PopulateDatabaseCombo(ByVal combo As ComboBox)

        Dim blnPopulated As Boolean = False
        Dim oEnum As IEnumerator = oXmlReader.GetNodes("/configuration/application/database/connection").GetEnumerator

        Dim oDataTable As New DataTable
        oDataTable.Columns.Add("Key")
        oDataTable.Columns.Add("Value")

        While oEnum.MoveNext
            blnPopulated = True

            Dim strKey As String = CType(oEnum.Current, Xml.XmlNode).Attributes("name").Value
            Dim strValue As String = CType(oEnum.Current, Xml.XmlNode).Attributes("value").Value

            oDataTable.Rows.Add(New Object() {strKey, strValue})

        End While

        combo.DataSource = oDataTable
        combo.DisplayMember = "Key"
        combo.ValueMember = "Value"

        If blnPopulated Then
            combo.SelectedItem = 0
        End If


    End Sub


End Class
