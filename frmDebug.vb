Public Class frmDebug
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim PamCls As New PAMConnector.PAMConnector
        With PamCls
            .PamServerURL = "acp.megacenter.kz"
            .ConnectionType = tbxConnectionType.Text
            .ConnectionPoint = tbxIP.Text
            .ConnectionPointUser = tbxUser.Text
            .EndPointName = tbxEndpointName.Text
            .ShowPamWindow()
        End With
    End Sub
End Class