Imports Microsoft.Win32
Public Class ApplicationUser
    'Public FirstName As String = ""
    'Public LastName As String = ""
    Public WindowsAccount As String = ""
    Public SessionID As Int32 = -1
    Public Status As Int32 = 1
    Public AuthorisationLimit As Double = 0.0
    Public UserName As String = ""
    Dim Clients() As String
    Public ID As Int32 = -1
    Private _IsAdmin As Boolean = False
    Private _IsFinance As Boolean = False
    Public IsEngineer As Boolean = False
    Public IsManager As Boolean = False
    Private _IsHexagon As Boolean = False
    Private _EngineerID As Int32 = -1
    Private pRegKey As RegistryKey = Registry.LocalMachine
    Private oLix As Licence = Nothing
    Public Property IsAdmin() As Boolean
        Get
            Return _IsAdmin
        End Get
        Set(ByVal Value As Boolean)
            _IsAdmin = Value
            If Value = True Then
                _IsFinance = True
                IsManager = True
            End If
        End Set
    End Property
    Public Property IsHexagon() As Boolean
        Get
            Return _IsHexagon
        End Get
        Set(ByVal Value As Boolean)
            _IsHexagon = Value
            If Value = True Then
                _IsAdmin = True
                _IsFinance = True
                IsManager = True
            End If
        End Set
    End Property
    Public Property IsFinance() As Boolean
        Get
            Return _IsFinance
        End Get
        Set(ByVal Value As Boolean)
            _IsFinance = Value
            If Value = True Then
                IsManager = True
            End If
        End Set
    End Property
    Public ReadOnly Property UserID() As Int32
        Get
            Return ID
        End Get
    End Property
    Public ReadOnly Property FirstName() As String
        Get
            Return UserName.Split(" ")(0)
        End Get
    End Property
    Public ReadOnly Property LastName() As String
        Get
            Try
                Dim Names() As String
                Names = UserName.Split(" ")

                Return Names(Names.Length)
            Catch
                Return ""
            End Try
        End Get
    End Property
    Public Property EngineerID()
        Get
            Return _EngineerID
        End Get
        Set(ByVal Value)
            _EngineerID = Value
            If Value > 0 Then
                IsEngineer = True
            Else
                IsEngineer = False
            End If
        End Set
    End Property
    Public ReadOnly Property LicencedMachine()
        Get
            Try
                pRegKey = pRegKey.OpenSubKey("Software\\Hexagon Software\\")
                Return pRegKey.GetValue("HelpDesk Registered Machine")
            Catch
                Return ""
            End Try

        End Get
    End Property
    Public ReadOnly Property ActiveLicence()
        Get
            Try
                pRegKey = pRegKey.OpenSubKey("Software\\Hexagon Software\\")
                Return pRegKey.GetValue("LicenceKey")
            Catch
                Return ""
            End Try
        End Get
    End Property
    Public ReadOnly Property ActiveProductCode()
        Get
            Try
                pRegKey = pRegKey.OpenSubKey("Software\\Hexagon Software\\")
                Return pRegKey.GetValue("Product Code")
            Catch
                Return ""
            End Try
        End Get
    End Property

    Public ReadOnly Property Licence()
        Get
            Return oLix
        End Get
    End Property


End Class
