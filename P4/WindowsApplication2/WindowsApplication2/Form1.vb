Public Class Form1
    Dim radius As Integer = 20
    Dim velocity As Integer = 100
    Dim xC As Integer, yC As Integer, xDelta As Integer = 1, yDelta As Integer = 1, xSize As Integer, ySize As Integer
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles StartButton.Click
        Timer1.Start()
    End Sub
    Private Sub Form1(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1_Tick_1(sender, e)
    End Sub
    Private Sub Timer1_Tick_1(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Start()
        Timer1.Interval = velocity
        DrawBall()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles StopButton.Click
        Timer1.Stop()
    End Sub
    Private Sub Form1_Resize(sender As Object, e As System.EventArgs) Handles MyBase.Resize
        xSize = Me.ClientSize.Width
        ySize = Me.ClientSize.Height
        xC = xSize / 2
        yC = ySize / 2
        Me.Invalidate()
        DrawBall()
    End Sub
    Private Sub DrawBall()
        Dim g As Graphics = Me.CreateGraphics()
        Dim b As Brush = New SolidBrush(Me.BackColor)
        g.FillEllipse(b, xC - radius, yC - radius, 2 * radius, 2 * radius)
        xC = xC + (xDelta * 90)
        yC = yC + (yDelta * 90)
        If (xC + radius >= ClientSize.Width) OrElse (xC - radius <= 0) Then
            xDelta = -xDelta
        End If
        If (yC + radius >= ClientSize.Height) OrElse (yC - radius <= 0) Then
            yDelta = -yDelta
        End If
        b = New SolidBrush(Color.DeepSkyBlue)
        g.FillEllipse(b, xC - radius, yC - radius, 2 * radius, 2 * radius)
        b.Dispose()
        g.Dispose()
    End Sub
    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        velocity = InputBox("enter a new velocity between 1 and 100", "velocity", , , )
        velocity = 101 - velocity
    End Sub
    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        xDelta = 1
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        xDelta = -1
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        yDelta = 1
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        yDelta = -1
    End Sub
End Class