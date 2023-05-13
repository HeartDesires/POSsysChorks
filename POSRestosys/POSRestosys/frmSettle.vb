Imports MySql.Data.MySqlClient
Public Class frmSettle


    Private Sub txtCash_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCash.KeyPress
        Select Case Asc(e.KeyChar)
            Case 48 To 57
            Case 46
            Case 8
            Case 13
                btnAccept_Click(sender, e)
            Case Else
                e.Handled = True
        End Select
    End Sub

    Private Sub txtCash_TextChanged(sender As Object, e As EventArgs) Handles txtCash.TextChanged
        Try
            Dim total As Double = CDbl(txtTotal.Text)
            Dim change As Double = CDbl(txtCash.Text) - total
            If change < 0 Then
                txtChange.Text = "0.00"
            Else
                txtChange.Text = Format(change, "#,##0.00")
            End If
        Catch ex As Exception
            txtChange.Text = ""

        End Try
    End Sub

    Private Sub btnAccept_Click(sender As Object, e As EventArgs) Handles btnAccept.Click
        Try
            Dim total As Double = CDbl(txtTotal.Text)
            Dim change As Double = CDbl(txtCash.Text) - total
            If change < 0 Then
                MsgBox("Insufficient Cash! Please enter the correct amount.", vbExclamation)
                Return
            Else

                If MsgBox("Save this payment?", vbYesNo + vbQuestion) = vbYes Then
                    SavePayment()
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Sub SavePayment()
        Try
            Dim sdate As String = Now.ToString("yyyy-MM-dd")
            Dim stime As String = Now.ToString("hh:mm:ss")
            cn.Open()
            cm = New MySqlCommand("insert into tblsales(transno, total, sdate, stime)values(@transno, @total, @sdate, @stime)", cn)
            With cm
                .Parameters.AddWithValue("@transno", frmPOS.lblTransNo.Text)
                .Parameters.AddWithValue("@total", CDbl(txtTotal.Text))
                .Parameters.AddWithValue("@sdate", sdate)
                .Parameters.AddWithValue("@stime", stime)

                .ExecuteNonQuery()

            End With
            cn.Close()

            cn.Open()
            cm = New MySqlCommand("update tblcart set status = 'Completed' where transno like '" & frmPOS.lblTransNo.Text & "'", cn)
            cm.ExecuteNonQuery()
            cn.Close()
            MsgBox("Payment successfully saved!", vbInformation)

            With frmPOS
                .lblTransNo.Text = .GetTransno()
                .LoadCart()
            End With

            Me.Dispose()


        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message, vbCritical)

        End Try
    End Sub

    Private Sub frmSettle_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True

    End Sub

    Private Sub frmSettle_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Dispose()
        End If
    End Sub

    Private Sub btnCancelPayment_Click(sender As Object, e As EventArgs)
        Me.Dispose()

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs)
        Me.Dispose()
    End Sub
End Class