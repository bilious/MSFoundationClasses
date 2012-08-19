Imports System.Xml.Serialization
Imports System.Xml
<Serializable()> Public Class LicenceFile
    Public LicenceOwner As String = ""
    Public LicencedApplication As String = ""
    Public ApplicationCode As Int32 = 0
    Public UserLicences As Int32 = 0
    Public LicenceExpiryDate As DateTime = Now
    Public LicenceKey As String = ""
    Public Generator As String = ""

End Class
