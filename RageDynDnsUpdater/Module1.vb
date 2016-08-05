Imports System
Imports System.Net
Imports System.IO
Imports Microsoft.Win32


Module Module1
    'Checks to see if ip has changed in publicIP.txt file if so update dyndns
    'Link to notes on update method used
    '-- https://help.dyn.com/remote-access-api/
    '

    Sub Main()
        Dim sURL As String
        Dim PublicIP As String = ""

        'Get the current public ip
        sURL = "http://myip.dnsomatic.com"

        Dim wrGETURL As WebRequest
        wrGETURL = WebRequest.Create(sURL)

        Dim objStream As Stream
        objStream = wrGETURL.GetResponse.GetResponseStream()

        Dim objReader As New StreamReader(objStream)
        Dim sLine As String = ""
        Dim i As Integer = 0

        Do While Not sLine Is Nothing
            i += 1
            sLine = objReader.ReadLine
            If PublicIP = "" Then
                PublicIP = sLine
            End If

            If Not sLine Is Nothing Then
                Console.WriteLine("{0}:{1}", i, sLine)
            End If
        Loop

        'Check if public IP has changed since last time
        Dim userid As String = "rage-inc"
        Dim passwd As String = "06f5f136ac9e11e48004e26d171c5f22"
        Dim hostname As String = Environment.GetEnvironmentVariable("StoreNo") & ".rage-inc.com"

        userid = "test"
        passwd = "test"
        hostname = "test1.customtest.dyndns.org"

        'Get the current public ip
        sURL = "http://" & userid & ":" & passwd & "@members.dyndns.org/nic/update?hostname=" & hostname & "&myip=" & PublicIP
        wrGETURL = WebRequest.Create(sURL)
        objStream = wrGETURL.GetResponse.GetResponseStream()

        sLine = ""
        objReader = Nothing
        i = 0
        Do While Not sLine Is Nothing
            i += 1
            sLine = objReader.ReadLine
            If Not sLine Is Nothing Then
                Console.WriteLine("{0}:{1}", i, sLine)
            End If
        Loop

        Console.ReadLine()

    End Sub

    Sub CreateRegKey(TheKey As String)
        Dim regKey As RegistryKey
        regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE", True)
        regKey.CreateSubKey(TheKey)
        regKey.Close()
    End Sub

End Module

