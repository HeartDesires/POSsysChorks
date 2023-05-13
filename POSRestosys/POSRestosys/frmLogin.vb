Imports MySql.Data.MySqlClient
Public Class frmLogin
    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtPass.PasswordChar = Chr(149)
        Connection()

    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click

        Try
            Dim found As Boolean = False
            If txtUser.Text = String.Empty Or txtPass.Text = String.Empty Then
                MsgBox("Required empty field ", vbExclamation)
                Return

            End If
            cn.Open()
            cm = New MySqlCommand("select * from tbluser where username=@username and password=@password", cn)
            With cm
                .Parameters.AddWithValue("@username", txtUser.Text)
                .Parameters.AddWithValue("@password", txtPass.Text)
            End With
            dr = cm.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                found = True
                str_user = dr.Item("username").ToString
                str_pass = dr.Item("password").ToString
                str_name = dr.Item("name").ToString
                str_role = dr.Item("role").ToString
            Else
                found = False
            End If
            dr.Close()
            cn.Close()

            If found = True Then
                With frmPOS
                    If CheckStatus() = True Then
                        .btnNewOrder.Enabled = True
                        .btnStart.Enabled = False
                        .btnEnd.Enabled = True

                    Else
                        .btnStart.Enabled = True
                        .btnEnd.Enabled = False
                    End If
                    MsgBox("Access Granted! Welcome " & str_name, vbInformation)
                    .lblName.Text = "Name: " & str_name
                    .lblRole.Text = "Role: " & str_role
                    .Show()


                End With
            Else
                MsgBox("Invalid username or password", vbExclamation)

            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)

        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Dispose()
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs)

    End Sub
End Class