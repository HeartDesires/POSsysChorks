Imports MySql.Data.MySqlClient
Public Class frmDailySales


    Function GenerateSales()
        Try
            Dim sdate As String = Now.ToString("yyyy-MM-dd")
            cn.Open()
            cm = New MySqlCommand("SELECT ifnull(SUM(total),0) as total FROM tblcart WHERE tdate BETWEEN '" & sdate & "' AND '" & sdate & "' AND status LIKE 'Completed'", cn)
            lblSale.Text = Format(CDbl(cm.ExecuteScalar), "#,##0.00")
            cn.Close()


            cn.Open()
            cm = New MySqlCommand("SELECT ifnull(SUM(initialcash),0) as total FROM tblstart WHERE sdate BETWEEN '" & sdate & "' AND '" & sdate & "'", cn)
            lblInitialCash.Text = Format(CDbl(cm.ExecuteScalar), "#,##0.00")
            cn.Close()

            Dim _total As Double = CDbl(lblSale.Text) + CDbl(lblInitialCash.Text)

            lblTotalSale.Text = Format(_total, "#,##0.00")
        Catch ex As Exception

        End Try
    End Function


    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Me.Dispose()

    End Sub

    Private Sub frmDailySales_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnDetails_Click(sender As Object, e As EventArgs) Handles btnDetails.Click
        With frmSalesDetails
            .LoadSales()
            .ShowDialog()
        End With
    End Sub
End Class