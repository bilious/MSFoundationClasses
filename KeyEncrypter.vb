Imports System.Security.Cryptography
Imports System.Text
Imports System.IO
Imports System.Xml.Serialization
Imports System.Xml
Imports Microsoft.Win32


Public Class KeyEncrypter

#Region "Secure Encryption"
    Public Shared Function Encrypt(ByVal plainText As String, _
                                      ByVal passPhrase As String, _
                                      ByVal saltValue As String, _
                                      ByVal hashAlgorithm As String, _
                                      ByVal passwordIterations As Integer, _
                                      ByVal initVector As String, _
                                      ByVal keySize As Integer) _
                              As String

        ' Convert strings into byte arrays.
        ' Let us assume that strings only contain ASCII codes.
        Dim initVectorBytes As Byte()
        initVectorBytes = Encoding.ASCII.GetBytes(initVector)

        Dim saltValueBytes As Byte()
        saltValueBytes = Encoding.ASCII.GetBytes(saltValue)

        ' Convert our plaintext into a byte array.
        ' Let us assume that plaintext contains UTF8-encoded characters.
        Dim plainTextBytes As Byte()
        plainTextBytes = Encoding.UTF8.GetBytes(plainText)

        ' First, we must create a password, from which the key will be derived.
        ' This password will be generated from the specified passphrase and 
        ' salt value. The password will be created using the specified hash 
        ' algorithm. Password creation can be done in several iterations.
        Dim password As PasswordDeriveBytes
        password = New PasswordDeriveBytes(passPhrase, _
                                           saltValueBytes, _
                                           hashAlgorithm, _
                                           passwordIterations)

        ' Use the password to generate pseudo-random bytes for the encryption
        ' key. Specify the size of the key in bytes (instead of bits).
        Dim keyBytes As Byte()
        keyBytes = password.GetBytes(keySize / 8)

        ' Create uninitialized Rijndael encryption object.
        Dim symmetricKey As RijndaelManaged
        symmetricKey = New RijndaelManaged

        ' It is reasonable to set encryption mode to Cipher Block Chaining
        ' (CBC). Use default options for other symmetric key parameters.
        symmetricKey.Mode = CipherMode.CBC

        ' Generate encryptor from the existing key bytes and initialization 
        ' vector. Key size will be defined based on the number of the key 
        ' bytes.
        Dim encryptor As ICryptoTransform
        encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes)

        ' Define memory stream which will be used to hold encrypted data.
        Dim memoryStream As MemoryStream
        memoryStream = New MemoryStream

        ' Define cryptographic stream (always use Write mode for encryption).
        Dim cryptoStream As CryptoStream
        cryptoStream = New CryptoStream(memoryStream, _
                                        encryptor, _
                                        CryptoStreamMode.Write)
        ' Start encrypting.
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length)

        ' Finish encrypting.
        cryptoStream.FlushFinalBlock()

        ' Convert our encrypted data from a memory stream into a byte array.
        Dim cipherTextBytes As Byte()
        cipherTextBytes = memoryStream.ToArray()

        ' Close both streams.
        memoryStream.Close()
        cryptoStream.Close()

        ' Convert encrypted data into a base64-encoded string.
        Dim cipherText As String
        cipherText = Convert.ToBase64String(cipherTextBytes)

        ' Return encrypted string.
        Encrypt = cipherText
    End Function


    Public Shared Function Decrypt(ByVal cipherText As String, _
                                   ByVal passPhrase As String, _
                                   ByVal saltValue As String, _
                                   ByVal hashAlgorithm As String, _
                                   ByVal passwordIterations As Integer, _
                                   ByVal initVector As String, _
                                   ByVal keySize As Integer) _
                           As String

        ' Convert strings defining encryption key characteristics into byte
        ' arrays. Let us assume that strings only contain ASCII codes.
        ' If strings include Unicode characters, use Unicode, UTF7, or UTF8
        ' encoding.
        Dim initVectorBytes As Byte()
        initVectorBytes = Encoding.ASCII.GetBytes(initVector)

        Dim saltValueBytes As Byte()
        saltValueBytes = Encoding.ASCII.GetBytes(saltValue)

        ' Convert our ciphertext into a byte array.
        Dim cipherTextBytes As Byte()
        cipherTextBytes = Convert.FromBase64String(cipherText)

        ' First, we must create a password, from which the key will be 
        ' derived. This password will be generated from the specified 
        ' passphrase and salt value. The password will be created using
        ' the specified hash algorithm. Password creation can be done in
        ' several iterations.
        Dim password As PasswordDeriveBytes
        password = New PasswordDeriveBytes(passPhrase, _
                                           saltValueBytes, _
                                           hashAlgorithm, _
                                           passwordIterations)

        ' Use the password to generate pseudo-random bytes for the encryption
        ' key. Specify the size of the key in bytes (instead of bits).
        Dim keyBytes As Byte()
        keyBytes = password.GetBytes(keySize / 8)

        ' Create uninitialized Rijndael encryption object.
        Dim symmetricKey As RijndaelManaged
        symmetricKey = New RijndaelManaged

        ' It is reasonable to set encryption mode to Cipher Block Chaining
        ' (CBC). Use default options for other symmetric key parameters.
        symmetricKey.Mode = CipherMode.CBC

        ' Generate decryptor from the existing key bytes and initialization 
        ' vector. Key size will be defined based on the number of the key 
        ' bytes.
        Dim decryptor As ICryptoTransform
        decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes)

        ' Define memory stream which will be used to hold encrypted data.
        Dim memoryStream As MemoryStream
        memoryStream = New MemoryStream(cipherTextBytes)

        ' Define memory stream which will be used to hold encrypted data.
        Dim cryptoStream As CryptoStream
        cryptoStream = New CryptoStream(memoryStream, _
                                        decryptor, _
                                        CryptoStreamMode.Read)

        ' Since at this point we don't know what the size of decrypted data
        ' will be, allocate the buffer long enough to hold ciphertext;
        ' plaintext is never longer than ciphertext.
        Dim plainTextBytes As Byte()
        ReDim plainTextBytes(cipherTextBytes.Length)

        ' Start decrypting.
        Dim decryptedByteCount As Integer
        decryptedByteCount = cryptoStream.Read(plainTextBytes, _
                                               0, _
                                               plainTextBytes.Length)

        ' Close both streams.
        memoryStream.Close()
        cryptoStream.Close()

        ' Convert decrypted data into a string. 
        ' Let us assume that the original plaintext string was UTF8-encoded.
        Dim plainText As String
        plainText = Encoding.UTF8.GetString(plainTextBytes, _
                                            0, _
                                            decryptedByteCount)

        ' Return decrypted string.
        Decrypt = plainText
    End Function


#End Region

#Region "Other Functions"

    Public Function EncryptPassword(ByVal strString) As String
        Dim i16X As Int16
        Dim i16C As Int16
        Dim chrChar As Char
        Dim Achr(11) As Char
        Dim strChar As String
        Dim stbStr As System.Text.StringBuilder = New System.Text.StringBuilder
        Dim strEncryptCode As String = "EncryptPassword"
        Dim strPasswordToEncrypt As String

        strPasswordToEncrypt = strString
        If strPasswordToEncrypt.Length < 12 Then
            Do Until strPasswordToEncrypt.Length >= 12
                strPasswordToEncrypt &= strString
            Loop
            strPasswordToEncrypt = strPasswordToEncrypt.Substring(0, 12)
        End If
        For i16X = 0 To 11
            chrChar = strEncryptCode.Substring((i16X Mod 12) - 12 * ((i16X Mod 12) = 0), 1).Chars(0)
            strChar = Asc(chrChar)
            Achr(i16X) = Chr(Asc(strPasswordToEncrypt.Substring(i16X, 1)) Xor Int16.Parse(strChar))
            i16C = Asc(Achr(i16X))
            If i16C < 33 Then Achr(i16X) = Chr(i16C + 33)
            stbStr.Append(Achr(i16X))
        Next
        stbStr.Replace("'", "~")
        Return stbStr.ToString

    End Function

    Sub ProcessLicenceFile(ByVal strFileName As String)
        Dim oLixF As New LicenceFile
        Dim fs As New FileStream(strFileName, FileMode.Open)
        Dim s As New StreamReader(fs)
        Dim xml_serializer As New XmlSerializer(GetType(LicenceFile))
        Try
            ' Create the new Person object from the serialization.
            oLixF = xml_serializer.Deserialize(s)
        Catch
            MsgBox("Licence File Was Not Valid !", MsgBoxStyle.Exclamation, "Licence Generator")
            Exit Sub
        End Try

        Try
            Dim plaintext As String = KeyEncrypter.Decrypt(oLixF.LicenceKey, oLixF.Generator, "s@1tValue", "SHA1", 2, "@1B2c3D4e5F6g7H8", 128)
            Dim oList() As String = plaintext.Split("-")
            Dim key As RegistryKey = Registry.LocalMachine.OpenSubKey("Software", True)
            Dim newkey As RegistryKey = key.CreateSubKey("Hexagon Software")
            newkey.SetValue("Full Licences", oLixF.UserLicences)
            newkey.SetValue("Expires", oLixF.LicenceExpiryDate)
            newkey.SetValue("LicenceKey", oLixF.LicenceKey)
            newkey.SetValue("ApplicationCode", oLixF.ApplicationCode)
            Dim oLix As New Licence(oLixF.ApplicationCode, "Hexagon", oLixF.LicenceExpiryDate, oLixF.UserLicences, 0)
            newkey.SetValue("Product Code", oLix.LicenceCode)
            MsgBox("Licence File Processed!", MsgBoxStyle.Information, "Hexagon HelpDesk")
        Catch ex As Exception
            MsgBox("Licence Key in File Was Not Valid", MsgBoxStyle.Exclamation, "Licence Generator")
        End Try
    End Sub

    Function QuickEncrypt(ByVal strOriginal As String) As String
        'Cheesy Encryption
        Dim strEnd As String = ""
        Dim i As Int32 = 0
        Dim j As Int32 = 0
        Dim x As String
        Dim newS As String = ""
        For i = 0 To (Len(strOriginal) - 1)
            j = i + 1
            x = strOriginal.Substring(i, 1)
            newS = newS & Chr((Asc(x) + j))
        Next
        Return newS
    End Function
    Function QuickDeCrypt(ByVal strOriginal As String) As String
        'Cheesy Decryption
        Dim strEnd As String = ""
        Dim i As Int32 = 0
        Dim j As Int32 = 0
        Dim x As String
        Dim newS As String = ""
        For i = 0 To (Len(strOriginal) - 1)
            j = i + 1
            x = strOriginal.Substring(i, 1)
            newS = newS & Chr((Asc(x) - j))
        Next
        Return newS
    End Function
#End Region

End Class
