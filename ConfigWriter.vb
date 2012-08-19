Imports System.Xml
Public Class ConfigWriter
    Public xdoc As XmlDocument
    Public strFilePath As String = ""

    Public Sub AddElement(ByVal strName As String, ByVal strValue As String)
        ' Add a price element.
        Dim newElem As XmlElement = xdoc.CreateElement(strName)
        newElem.InnerText = strValue
        xdoc.DocumentElement.AppendChild(newElem)
    End Sub
    Public Function GetNode(ByVal path As String) As XmlNode

        Return xdoc.SelectSingleNode(path)

    End Function

    Public Sub Save()
        Dim writer As XmlTextWriter = New XmlTextWriter(strFilePath, Nothing)
        writer.Formatting = Formatting.Indented
        xdoc.Save(writer)
    End Sub
    Sub New()
        xdoc = New XmlDocument
        xdoc.Load(AppDomain.CurrentDomain.BaseDirectory & "..\config\HelpDesk.config")
        strFilePath = AppDomain.CurrentDomain.BaseDirectory & "..\config\HelpDesk.config"
    End Sub

End Class
