Imports MySql.Data.MySqlClient
Module Module1
    Public cn As New MySqlConnection
    Public cm As New MySqlCommand
    Public dr As MySqlDataReader
    Public str_user, str_pass, str_name, str_role As String
    Public startid As String


    Sub Connection()
        cn = New MySqlConnection
        With cn
            .ConnectionString = "server=localhost;user id=root;password=;database=chorksdb"
        End With
    End Sub

    Function CheckTransaction()
        Dim isOpen As Boolean
        cn.Open()
        cm = New MySqlCommand("SELECT * FROM tblstart WHERE id like '" & startid & "' AND status like 'open'", cn)
        dr = cm.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            isOpen = True

        Else
            isOpen = False

        End If
        dr.Close()
        cn.Close()

        Return isOpen

    End Function

    Function CheckStatus() As Boolean
        Dim found As Boolean
        Dim sdate As String = Now.ToString("yyyy-MM-dd")

        cn.Open()
        cm = New MySqlCommand("SELECT * FROM tblstart WHERE sdate BETWEEN '" & sdate & "' AND '" & sdate & "' AND status LIKE 'open'", cn)
        dr = cm.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            startid = dr.Item("id").ToString
            found = True
        Else
            found = False
        End If
        dr.Close()
        cn.Close()
        Return found
    End Function

End Module
