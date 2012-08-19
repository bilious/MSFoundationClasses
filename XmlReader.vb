Imports System.Xml

Public Class XmlReader

    Dim oXmlDocument As XmlDocument

    Public Sub New()
        oXmlDocument = New XmlDocument
    End Sub
    Public Sub New(ByVal file As String)

        Me.New()

        oXmlDocument.Load(file)
        If oXmlDocument.InnerXml.TrimEnd.TrimStart = "" Then
            Throw New Exception("Config file couldnt be loaded.")
        End If

    End Sub

    Public Function GetNodes(ByVal path As String) As XmlNodeList

        Return oXmlDocument.SelectNodes(path)

    End Function

    Public Function GetNode(ByVal path As String) As XmlNode

        Return oXmlDocument.SelectSingleNode(path)

    End Function

    Public Sub AddAttribToNode(ByVal doc As XmlDocument, ByVal node As XmlNode, ByVal name As String, ByVal value As Object)

        Dim oAttrib As XmlNode = doc.CreateNode(XmlNodeType.Attribute, name, "")
        oAttrib.Value = CType(value, String)
        node.Attributes.Append(oAttrib)

    End Sub

    Public Function CreateNode(ByVal oDoc As XmlDocument, ByVal nodeName As String) As XmlNode

        Dim oCurrentNode As XmlNode = oDoc.CreateNode(XmlNodeType.Element, nodeName, "")
        Return oCurrentNode

    End Function

End Class
