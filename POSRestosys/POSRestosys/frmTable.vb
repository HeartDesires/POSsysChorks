Imports MySql.Data.MySqlClient
Public Class frmTable
    Dim table As String

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If txtTable.Text = String.Empty Then Return
            If MsgBox("Save Table No?", vbYesNo + vbQuestion) = vbYes Then
                cn.Open()
                cm = New MySqlCommand("INSERT INTO tbltable (tableno)VALUES(@tableno)", cn)
                cm.Parameters.AddWithValue("@tableno", txtTable.Text)
                cm.ExecuteNonQuery()
                cn.Close()
                MsgBox("Table no has been successfully saved", vbInformation)
                Loadrecord()
                btnCancel_Click(sender, e)
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Sub Loadrecord()
        Dim i As Integer
        DataGridView1.Rows.Clear()
        cn.Open()
        cm = New MySqlCommand("SELECT * FROM tbltable", cn)
        dr = cm.ExecuteReader
        While dr.Read
            i += 1
            DataGridView1.Rows.Add(i, dr.Item("tableno").ToString)
        End While
        dr.Close()
        cn.Close()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim colname As String = DataGridView1.Columns(e.ColumnIndex).Name
        If colname = "colEdit" Then
            table = DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString
            txtTable.Text = table
            btnSave.Enabled = False
            btnUpdate.Enabled = True
        ElseIf colname = "colDelete" Then
            If MsgBox("Delete this table no permanently?", vbYesNo + vbQuestion) = vbYes Then
                cn.Open()
                cm = New MySqlCommand("delete from tbltable where tableno  like '" & DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString & "'", cn)
                cm.ExecuteNonQuery()
                cn.Close()
                MsgBox("Table No has been successfully deleted!", vbInformation)
                Loadrecord()
            End If
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        table = ""
        btnSave.Enabled = True
        btnUpdate.Enabled = False
        txtTable.Clear()
        txtTable.Focus()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            If txtTable.Text = String.Empty Then Return
            If MsgBox("Update Table No?", vbYesNo + vbQuestion) = vbYes Then
                cn.Open()
                cm = New MySqlCommand("update tbltable set tableno = @tableno where tableno like '" & table & "'", cn)
                cm.Parameters.AddWithValue("@tableno", txtTable.Text)
                cm.ExecuteNonQuery()
                cn.Close()
                MsgBox("Table no has been successfully updated", vbInformation)
                Loadrecord()
                btnCancel_Click(sender, e)
            End If
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)
        End Try

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Me.Dispose()
    End Sub

    Private Sub frmTable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
    End Sub

    Private Sub frmTable_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Dispose()
    End Sub
End Class