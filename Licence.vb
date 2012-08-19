Public Class Licence

    'Private property variables
    Private mblnIsValid As Boolean
    Private mdtmExpiry As Date
    Private mi32NumberOfLicences As Int32
    Private mi32NumberOfEnquiryLicences As Int32
    Private mstrLicenceCode As String

    'Application product code enumerated list
    Public Enum ELApplication
        BenchMark_Personal_Assistant = 417
        BenchMark_Partner = 261
        BenchMark_Senior_Partner = 723
        Hexagon_HelpDesk_Client = 411
        Hexagon_HelpDesk_Server = 412
        Hexagon_Server = 413
        HelpDesk_Plus_Client = 211
    End Enum

    Private AmstrYearCodes(29) As Byte
    Private AmstrMonthCodes(11) As Byte
#Region "Properties"
    Public ReadOnly Property ExpiryDate() As Date
        Get
            Return mdtmExpiry
        End Get
    End Property

    Public ReadOnly Property IsValid() As Boolean
        Get
            Return mblnIsValid
        End Get
    End Property

    Public ReadOnly Property NumberOfLicences() As Int32
        Get
            Return mi32NumberOfLicences
        End Get
    End Property

    Public ReadOnly Property NumberOfEnquiryLicences() As Int32
        Get
            Return mi32NumberOfEnquiryLicences
        End Get
    End Property
    Public ReadOnly Property LicenceCode() As String
        Get
            Return mstrLicenceCode
        End Get
    End Property
#End Region

#Region "Constructors"

    Public Sub New(ByVal strCompanyName As String, ByVal strLicenceCode As String, ByVal i16Application As ELApplication)
        If strLicenceCode.IndexOf("-") <> -1 Then
            ParseNewFormatLicenceCode(strCompanyName, strLicenceCode, i16Application)
        Else
            mi32NumberOfEnquiryLicences = 0
            ParseOriginalLicenceCode(strCompanyName, strLicenceCode, i16Application)
        End If
    End Sub


    Public Sub New(ByVal i32Application As Int32, ByVal strCompanyName As String, ByVal dtmExpiry As Date, ByVal i32Licence As Int32)
        'This constructor is the original used to generate the original format licence numbers that don't have separate full and enquiry only licences
        Dim strCodeString As New System.Text.StringBuilder
        Dim i32 As Int32
        Dim chrSwapChar As Char

        Dim i32ConvertedName As Int32 = CodeCalculatedFromString(strCompanyName, True)

        If i32ConvertedName.ToString.Length < 6 Then
            i32ConvertedName = (i32ConvertedName * (10 ^ (6 - i32ConvertedName.ToString.Length))) + i32ConvertedName.ToString.Length
        End If

        strCodeString.Append(i32ConvertedName.ToString)

        strCodeString.Append(CodeCalculatedFromDate(dtmExpiry))

        i32 = ((i32Licence ^ 2) + 12345) * i32Application
        strCodeString.Append(i32.ToString)
        'Now swap over characters 3 and 14, 6 and 11, 8 and 16
        chrSwapChar = strCodeString.Chars(2)
        strCodeString.Chars(2) = strCodeString.Chars(13)
        strCodeString.Chars(13) = chrSwapChar
        chrSwapChar = strCodeString.Chars(5)
        strCodeString.Chars(5) = strCodeString.Chars(10)
        strCodeString.Chars(10) = chrSwapChar
        chrSwapChar = strCodeString.Chars(7)
        strCodeString.Chars(7) = strCodeString.Chars(15)
        strCodeString.Chars(15) = chrSwapChar

        mstrLicenceCode = strCodeString.ToString
    End Sub

    Public Sub New(ByVal i32Application As Int32, ByVal strCompanyName As String, ByVal dtmExpiry As Date, ByVal i32FullLicence As Int32, ByVal i32EnquiryLicence As Int32)
        'This constructor is the new one used to handle licence numbers with full and enquiry only users
        Dim strCodeString As New System.Text.StringBuilder
        Dim i32 As Int32
        Dim chrSwapChar As Char

        Dim i32ConvertedName As Int32 = CodeCalculatedFromString(strCompanyName, True)

        If i32ConvertedName.ToString.Length < 6 Then
            i32ConvertedName = (i32ConvertedName * (10 ^ (6 - i32ConvertedName.ToString.Length))) + i32ConvertedName.ToString.Length
        End If

        strCodeString.Append(i32ConvertedName.ToString)

        strCodeString.Append(CodeCalculatedFromDate(dtmExpiry))

        'Now perform a calculation to convert the number of full users into a 7 or 8 digit number
        i32 = ((i32FullLicence ^ 2) + 12345) * i32Application
        strCodeString.Append(i32.ToString)
        'Now swap over characters 3 and 14, 6 and 11, 8 and 16
        chrSwapChar = strCodeString.Chars(2)
        strCodeString.Chars(2) = strCodeString.Chars(13)
        strCodeString.Chars(13) = chrSwapChar
        chrSwapChar = strCodeString.Chars(5)
        strCodeString.Chars(5) = strCodeString.Chars(10)
        strCodeString.Chars(10) = chrSwapChar
        chrSwapChar = strCodeString.Chars(7)
        strCodeString.Chars(7) = strCodeString.Chars(15)
        strCodeString.Chars(15) = chrSwapChar

        'Now characters have been swapped, insert a "-" character after the 8th character in the string and append one at the end
        strCodeString.Insert(8, "-", 1)
        strCodeString.Append("-")
        'Store the start position of the enquiry licence part of the code
        Dim i32EnquiryStartPos As Int32 = strCodeString.ToString.Length

        'Now use the same algorhythm to calculate the code for the enquiry only users and used for the full users
        i32 = ((i32EnquiryLicence ^ 2) + 12345) * i32Application
        strCodeString.Append(i32.ToString)

        'Finally, using the stored position of the second "-" (before the enquiry only users) swap over the 2nd character after the "-" with the 
        '2nd character of the main code and the 5th character after the "-" with the final character of the date part of the main code
        chrSwapChar = strCodeString.Chars(1)
        strCodeString.Chars(1) = strCodeString.Chars(i32EnquiryStartPos + 1)
        strCodeString.Chars(i32EnquiryStartPos + 1) = chrSwapChar
        chrSwapChar = strCodeString.Chars(12)
        strCodeString.Chars(12) = strCodeString.Chars(i32EnquiryStartPos + 4)
        strCodeString.Chars(i32EnquiryStartPos + 4) = chrSwapChar

        mstrLicenceCode = strCodeString.ToString
    End Sub

#End Region

    Private Sub ParseOriginalLicenceCode(ByVal strCompanyName As String, ByVal strLicenceCode As String, ByVal i16Application As ELApplication)
        Dim i32ConvertedName As Int32
        Dim strSwap As New System.Text.StringBuilder(strLicenceCode)
        Dim strSwappedLicence As String
        Dim chrSwapChar As Char

        'First swap back swapped pairs of characters
        chrSwapChar = strSwap.Chars(2)
        strSwap.Chars(2) = strSwap.Chars(13)
        strSwap.Chars(13) = chrSwapChar
        chrSwapChar = strSwap.Chars(5)
        strSwap.Chars(5) = strSwap.Chars(10)
        strSwap.Chars(10) = chrSwapChar
        chrSwapChar = strSwap.Chars(7)
        strSwap.Chars(7) = strSwap.Chars(15)
        strSwap.Chars(15) = chrSwapChar

        'Now convert passed company name and compared to licence code to validate company within the licence string
        i32ConvertedName = CodeCalculatedFromString(strCompanyName, True)
        If i32ConvertedName.ToString.Length < 6 Then
            i32ConvertedName = (i32ConvertedName * (10 ^ (6 - i32ConvertedName.ToString.Length))) + i32ConvertedName.ToString.Length
        End If
        strSwappedLicence = strSwap.ToString
        If i32ConvertedName.ToString <> strSwappedLicence.Substring(0, 6) Then
            mblnIsValid = False
            Exit Sub
        End If

        'Licence is valid when compared with company name so now retrieve expiry date
        SetupDateArray()
        Dim strMonth As String = GetMonth(Byte.Parse(strSwappedLicence.Substring(8, 2)))
        Dim strYear As String = GetYear(Byte.Parse(strSwappedLicence.Substring(6, 2)))
        If strMonth = "" Or strYear = "" Then
            mblnIsValid = False
            Exit Sub
        End If
        Dim strDateString As New System.Text.StringBuilder
        strDateString.Append(strYear)
        strDateString.Append("/")
        strDateString.Append(strMonth)
        strDateString.Append("/")
        If strMonth = "4" Or strMonth = "6" Or strMonth = "9" Or strMonth = "11" Then
            strDateString.Append("30")
        ElseIf strMonth = "2" Then
            strDateString.Append("28")
        Else
            strDateString.Append("31")
        End If
        mdtmExpiry = DateTime.Parse(strDateString.ToString)

        'Licence is valid when compared with company name and expiry date so now retrieve the number of licences
        Dim i32ProductAndLicence As Int32 = Int32.Parse(strSwappedLicence.Substring(10))
        Dim dblLicences As Double = System.Math.Sqrt((i32ProductAndLicence / i16Application) - 12345)
        If Math.Round(dblLicences) <> dblLicences Then
            mblnIsValid = False
            Exit Sub
        End If
        mi32NumberOfLicences = dblLicences
        mblnIsValid = True
    End Sub

    Private Sub ParseNewFormatLicenceCode(ByVal strCompanyName As String, ByVal strLicenceCode As String, ByVal i16Application As ELApplication)
        Dim i32ConvertedName As Int32
        Dim strSwap As New System.Text.StringBuilder(strLicenceCode)
        Dim strSwappedLicence As String
        Dim chrSwapChar As Char
        Dim i32EnquiryStartPos As Int32
        Dim i32ProductAndLicence As Int32
        Dim dblLicences As Double

        'First, we need to locate the last "-" character in the licence code to establish the location of the enquiry only users licence count
        i32EnquiryStartPos = strLicenceCode.LastIndexOf("-")
        'Now swap back the pairs of characters
        chrSwapChar = strSwap.Chars(1)
        strSwap.Chars(1) = strSwap.Chars(i32EnquiryStartPos + 2)
        strSwap.Chars(i32EnquiryStartPos + 2) = chrSwapChar
        chrSwapChar = strSwap.Chars(12)
        strSwap.Chars(12) = strSwap.Chars(i32EnquiryStartPos + 5)
        strSwap.Chars(i32EnquiryStartPos + 5) = chrSwapChar

        i32ProductAndLicence = Int32.Parse(strSwap.ToString.Substring(i32EnquiryStartPos + 1))
        dblLicences = System.Math.Sqrt((i32ProductAndLicence / i16Application) - 12345)
        If Math.Round(dblLicences) <> dblLicences Then
            mblnIsValid = False
            Exit Sub
        End If
        mi32NumberOfEnquiryLicences = dblLicences

        'Remove enquiry only part of licence number and the "-" character from the early part of the code
        strSwap.Remove(i32EnquiryStartPos, (strSwap.Length - i32EnquiryStartPos))
        strSwap.Replace("-", "")

        'Swap back swapped pairs of characters
        chrSwapChar = strSwap.Chars(2)
        strSwap.Chars(2) = strSwap.Chars(13)
        strSwap.Chars(13) = chrSwapChar
        chrSwapChar = strSwap.Chars(5)
        strSwap.Chars(5) = strSwap.Chars(10)
        strSwap.Chars(10) = chrSwapChar
        chrSwapChar = strSwap.Chars(7)
        strSwap.Chars(7) = strSwap.Chars(15)
        strSwap.Chars(15) = chrSwapChar

        'Now convert passed company name and compared to licence code to validate company within the licence string
        i32ConvertedName = CodeCalculatedFromString(strCompanyName, True)
        If i32ConvertedName.ToString.Length < 6 Then
            i32ConvertedName = (i32ConvertedName * (10 ^ (6 - i32ConvertedName.ToString.Length))) + i32ConvertedName.ToString.Length
        End If
        strSwappedLicence = strSwap.ToString
        If i32ConvertedName.ToString <> strSwappedLicence.Substring(0, 6) Then
            mblnIsValid = False
            Exit Sub
        End If

        'Licence is valid when compared with company name so now retrieve expiry date
        SetupDateArray()
        Dim strMonth As String = GetMonth(Byte.Parse(strSwappedLicence.Substring(8, 2)))
        Dim strYear As String = GetYear(Byte.Parse(strSwappedLicence.Substring(6, 2)))
        If strMonth = "" Or strYear = "" Then
            mblnIsValid = False
            Exit Sub
        End If
        Dim strDateString As New System.Text.StringBuilder
        strDateString.Append(strYear)
        strDateString.Append("/")
        strDateString.Append(strMonth)
        strDateString.Append("/")
        If strMonth = "4" Or strMonth = "6" Or strMonth = "9" Or strMonth = "11" Then
            strDateString.Append("30")
        ElseIf strMonth = "2" Then
            strDateString.Append("28")
        Else
            strDateString.Append("31")
        End If
        mdtmExpiry = DateTime.Parse(strDateString.ToString)

        'Licence is valid when compared with company name and expiry date so now retrieve the number of licences
        i32ProductAndLicence = Int32.Parse(strSwappedLicence.Substring(10))
        dblLicences = System.Math.Sqrt((i32ProductAndLicence / i16Application) - 12345)
        If Math.Round(dblLicences) <> dblLicences Then
            mblnIsValid = False
            Exit Sub
        End If
        mi32NumberOfLicences = dblLicences
        mblnIsValid = True
    End Sub

    Private Function GetMonth(ByVal bytMonthCode As Byte) As String
        Dim i16Index As Int16

        For i16Index = 0 To 11
            If AmstrMonthCodes(i16Index) = bytMonthCode Then
                Dim i16Return As Int16 = i16Index + 1
                Return i16Return.ToString
            End If
        Next
        Return ""
    End Function

    Private Function GetYear(ByVal bytYearCode As Byte) As String
        Dim i16Index As Int16

        For i16Index = 0 To 28
            If AmstrYearCodes(i16Index) = bytYearCode Then
                Dim i16Return As Int16 = i16Index + 2000
                Return i16Return.ToString
            End If
        Next
        Return ""
    End Function

    Private Sub SetupDateArray()
        AmstrYearCodes(0) = 17
        AmstrYearCodes(1) = 93
        AmstrYearCodes(2) = 87
        AmstrYearCodes(3) = 19
        AmstrYearCodes(4) = 23
        AmstrYearCodes(5) = 31
        AmstrYearCodes(6) = 42
        AmstrYearCodes(7) = 12
        AmstrYearCodes(8) = 55
        AmstrYearCodes(9) = 67
        AmstrYearCodes(10) = 98
        AmstrYearCodes(11) = 48
        AmstrYearCodes(12) = 59
        AmstrYearCodes(13) = 28
        AmstrYearCodes(14) = 81
        AmstrYearCodes(15) = 72
        AmstrYearCodes(16) = 51
        AmstrYearCodes(17) = 40
        AmstrYearCodes(18) = 15
        AmstrYearCodes(19) = 34
        AmstrYearCodes(20) = 82
        AmstrYearCodes(21) = 10
        AmstrYearCodes(22) = 69
        AmstrYearCodes(23) = 20
        AmstrYearCodes(24) = 36
        AmstrYearCodes(25) = 76
        AmstrYearCodes(26) = 46
        AmstrYearCodes(27) = 79
        AmstrYearCodes(28) = 61

        AmstrMonthCodes(0) = 28
        AmstrMonthCodes(1) = 88
        AmstrMonthCodes(2) = 73
        AmstrMonthCodes(3) = 56
        AmstrMonthCodes(4) = 13
        AmstrMonthCodes(5) = 91
        AmstrMonthCodes(6) = 20
        AmstrMonthCodes(7) = 41
        AmstrMonthCodes(8) = 38
        AmstrMonthCodes(9) = 17
        AmstrMonthCodes(10) = 97
        AmstrMonthCodes(11) = 62
    End Sub

    Private Function CodeCalculatedFromDate(ByVal dtmCalculationDate As Date) As String
        Dim strCode As New System.Text.StringBuilder
        Dim AstrYearCodes(29) As Byte
        Dim AstrMonthCodes(11) As Byte

        AstrYearCodes(0) = 17
        AstrYearCodes(1) = 93
        AstrYearCodes(2) = 87
        AstrYearCodes(3) = 19
        AstrYearCodes(4) = 23
        AstrYearCodes(5) = 31
        AstrYearCodes(6) = 42
        AstrYearCodes(7) = 12
        AstrYearCodes(8) = 55
        AstrYearCodes(9) = 67
        AstrYearCodes(10) = 98
        AstrYearCodes(11) = 48
        AstrYearCodes(12) = 59
        AstrYearCodes(13) = 28
        AstrYearCodes(14) = 81
        AstrYearCodes(15) = 72
        AstrYearCodes(16) = 51
        AstrYearCodes(17) = 40
        AstrYearCodes(18) = 15
        AstrYearCodes(19) = 34
        AstrYearCodes(20) = 82
        AstrYearCodes(21) = 10
        AstrYearCodes(22) = 69
        AstrYearCodes(23) = 20
        AstrYearCodes(24) = 36
        AstrYearCodes(25) = 76
        AstrYearCodes(26) = 46
        AstrYearCodes(27) = 79
        AstrYearCodes(28) = 61

        AstrMonthCodes(0) = 28
        AstrMonthCodes(1) = 88
        AstrMonthCodes(2) = 73
        AstrMonthCodes(3) = 56
        AstrMonthCodes(4) = 13
        AstrMonthCodes(5) = 91
        AstrMonthCodes(6) = 20
        AstrMonthCodes(7) = 41
        AstrMonthCodes(8) = 38
        AstrMonthCodes(9) = 17
        AstrMonthCodes(10) = 97
        AstrMonthCodes(11) = 62

        strCode.Append(AstrYearCodes(dtmCalculationDate.Year - 2000).ToString)
        strCode.Append(AstrMonthCodes(dtmCalculationDate.Month - 1).ToString)
        Return strCode.ToString
    End Function

    Private Function CodeCalculatedFromString(ByVal strStringToConvert As String, ByVal blnMultiplyByLength As Boolean) As Int32
        Dim i32 As Int32
        Dim i32Code As Int32 = 0
        For i32 = 0 To strStringToConvert.Length - 1
            i32Code += Microsoft.VisualBasic.Asc(strStringToConvert.Chars(i32))
        Next
        If blnMultiplyByLength Then i32Code = i32Code * strStringToConvert.Length
        Return i32Code
    End Function
End Class
