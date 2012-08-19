Imports System.Xml

Public Class ApplicationConfiguration
    Private xDoc As XmlDocument
    Private oConfigWriter As New ConfigWriter
    Private oConfigReader As New ConfigReader
    Public ReadOnly Property Location()
        Get
            Return (AppDomain.CurrentDomain.BaseDirectory & "..\config\HelpDesk.config")
        End Get
    End Property
    Private Sub AddElement(ByVal strName As String, ByVal strValue As String, ByVal oNode As XmlNode)
        Try
            Dim newElem As XmlElement = xDoc.CreateElement(strName)
            If strValue <> "" Then
                newElem.SetAttribute("value", strValue)
            End If
            oNode.AppendChild(newElem)
        Catch ex As Exception
        End Try
    End Sub
    Sub New()
        xDoc = New XmlDocument
        xDoc.Load(AppDomain.CurrentDomain.BaseDirectory & "..\config\HelpDesk.config")
    End Sub
    'Public Sub AddSection(ByVal strSectionName As String, ByVal strValue As String, Optional ByVal oNode As XmlNode = Nothing)
    '    If oNode Is Nothing Then
    '        oNode = GetNode("")
    '    End If
    '    AddElement(strSectionName, strValue, oNode)
    'End Sub
    Public Sub AddSetting(ByVal strSettingName As String, ByVal strValue As String)
        Dim tPOs As Int32 = strSettingName.LastIndexOf("/")
        Dim strNewName As String = strSettingName.Substring(tPOs)
        Dim strPath As String = strSettingName.Substring(0, (tPOs - 1))
        Dim oNode As XmlNode = GetNode(strPath)
        If oNode Is Nothing Then
            oNode = GetNode("")
        End If
        AddElement(strNewName, strValue, oNode)
    End Sub
    Public Sub AddSetting(ByVal strSettingName As String, ByVal oHash As Hashtable)
        Dim tPOs As Int32 = strSettingName.LastIndexOf("/")
        Dim strNewName As String = strSettingName.Substring(tPOs)
        Dim strPath As String = strSettingName.Substring(0, (tPOs - 1))
        Dim oNode As XmlNode = GetNode(strPath)
        If oNode Is Nothing Then
            oNode = GetNode("")
        End If
        Dim oEnum As IDictionaryEnumerator = oHash.GetEnumerator
        Dim newElem As XmlElement = xDoc.CreateElement(strNewName)
        While oEnum.MoveNext
            newElem.SetAttribute(oEnum.Key.ToString, oEnum.Value.ToString)
        End While
        oNode.AppendChild(newElem)
        'AddElement(strSettingName, strValue, oNode)
    End Sub

    Public Sub AddNode(ByVal oNode As XmlNode, Optional ByVal oPNode As XmlNode = Nothing)
        If oPNode Is Nothing Then
            oPNode = oConfigReader.GetNode("")
        End If
    End Sub
    Public Sub AddNode(ByVal strNodeName As String, Optional ByVal oPNode As XmlNode = Nothing)
        If oPNode Is Nothing Then
            oPNode = oConfigReader.GetNode("")
        End If
    End Sub
    Public Function GetNode(ByVal strPath As String) As XmlNode
        If strPath = "" Then
            Return xDoc.SelectSingleNode("/configuration/application")
        Else
            Return xDoc.SelectSingleNode("/configuration/application/" & strPath)
        End If

    End Function

    Public Function GetSection(ByVal strPath As String) As String
        Return GetNode(strPath).InnerXml
    End Function


    Public Function GetSetting(ByVal strPath As String) As String
        Dim oNode As XmlNode
        oNode = GetNode(strPath)
        Return oNode.Attributes("value").Value
    End Function

    Public Function GetBaseWebPath() As String
        Return oConfigReader.GetBaseWebPath
    End Function

    Public Sub Save()
        xDoc.Save(AppDomain.CurrentDomain.BaseDirectory & "..\config\HelpDesk.config")
    End Sub
End Class
