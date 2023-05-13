Imports MySql.Data.MySqlClient
Public Class frmSelectTable
    Dim btnTable As New Button
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Me.Dispose()
    End Sub

    Sub LoadTable()
        cn.Open()
        cm = New MySqlCommand("select *from vwtable", cn)
        dr = cm.ExecuteReader
        While dr.Read
            btnTable = New Button
            btnTable.Width = 150
            btnTable.Height = 35
            If CDbl(dr.Item("bill").ToString) > 1 Then
                btnTable.Text = dr.Item("tableno").ToString & "  -  ₱" & dr.Item("bill").ToString
                btnTable.BackColor = Color.Crimson
            Else
                btnTable.Text = dr.Item("tableno").ToString
                btnTable.BackColor = Color.FromArgb(47, 54, 64)


            End If
            btnTable.Tag = dr.Item("tableno").ToString
            btnTable.FlatStyle = FlatStyle.Flat

            btnTable.ForeColor = Color.White
            btnTable.Cursor = Cursors.Hand
            btnTable.TextAlign = ContentAlignment.MiddleLeft
            FlowLayoutPanel1.Controls.Add(btnTable)
            AddHandler btnTable.Click, AddressOf GetTable_click

        End While
        dr.Close()
        cn.Close()
    End Sub

    Sub GetTable_click(sender As Object, E As EventArgs)
        Dim table As String = sender.tag.ToString
        With frmPOS
            .lblTable.Text = table
            .GetOrder()
        End With
        Me.Dispose()

    End Sub
End Class