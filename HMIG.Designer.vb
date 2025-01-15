<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class HMIG
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        btnConnect = New Button()
        btnDisconnect = New Button()
        IPPLC = New Label()
        txtIpAddress = New TextBox()
        txtPort = New TextBox()
        stp = New Button()
        btnstart = New Button()
        btnClose = New Button()
        Button6 = New Button()
        Button7 = New Button()
        Label1 = New Label()
        lbllampstatus = New Label()
        lbllampstatus2 = New Label()
        SuspendLayout()
        ' 
        ' btnConnect
        ' 
        btnConnect.Location = New Point(158, 97)
        btnConnect.Margin = New Padding(3, 2, 3, 2)
        btnConnect.Name = "btnConnect"
        btnConnect.Size = New Size(82, 22)
        btnConnect.TabIndex = 0
        btnConnect.Text = "Connect"
        btnConnect.UseVisualStyleBackColor = True
        ' 
        ' btnDisconnect
        ' 
        btnDisconnect.Location = New Point(297, 97)
        btnDisconnect.Margin = New Padding(3, 2, 3, 2)
        btnDisconnect.Name = "btnDisconnect"
        btnDisconnect.Size = New Size(82, 22)
        btnDisconnect.TabIndex = 1
        btnDisconnect.Text = "Disconnect"
        btnDisconnect.UseVisualStyleBackColor = True
        ' 
        ' IPPLC
        ' 
        IPPLC.AutoSize = True
        IPPLC.Location = New Point(177, 55)
        IPPLC.Name = "IPPLC"
        IPPLC.Size = New Size(41, 15)
        IPPLC.TabIndex = 2
        IPPLC.Text = "IP PLC"
        ' 
        ' txtIpAddress
        ' 
        txtIpAddress.Location = New Point(145, 72)
        txtIpAddress.Margin = New Padding(3, 2, 3, 2)
        txtIpAddress.Name = "txtIpAddress"
        txtIpAddress.Size = New Size(110, 23)
        txtIpAddress.TabIndex = 3
        ' 
        ' txtPort
        ' 
        txtPort.Location = New Point(284, 72)
        txtPort.Margin = New Padding(3, 2, 3, 2)
        txtPort.Name = "txtPort"
        txtPort.Size = New Size(110, 23)
        txtPort.TabIndex = 4
        ' 
        ' stp
        ' 
        stp.Location = New Point(136, 227)
        stp.Margin = New Padding(3, 2, 3, 2)
        stp.Name = "stp"
        stp.Size = New Size(82, 22)
        stp.TabIndex = 6
        stp.Text = "Stop"
        stp.UseVisualStyleBackColor = True
        ' 
        ' btnstart
        ' 
        btnstart.Location = New Point(30, 227)
        btnstart.Margin = New Padding(3, 2, 3, 2)
        btnstart.Name = "btnstart"
        btnstart.Size = New Size(82, 22)
        btnstart.TabIndex = 5
        btnstart.Text = "Start"
        btnstart.UseVisualStyleBackColor = True
        ' 
        ' btnClose
        ' 
        btnClose.Location = New Point(546, 287)
        btnClose.Margin = New Padding(3, 2, 3, 2)
        btnClose.Name = "btnClose"
        btnClose.Size = New Size(123, 40)
        btnClose.TabIndex = 7
        btnClose.Text = "Close"
        btnClose.UseVisualStyleBackColor = True
        ' 
        ' Button6
        ' 
        Button6.Location = New Point(412, 227)
        Button6.Margin = New Padding(3, 2, 3, 2)
        Button6.Name = "Button6"
        Button6.Size = New Size(82, 22)
        Button6.TabIndex = 10
        Button6.Text = "Stop"
        Button6.UseVisualStyleBackColor = True
        ' 
        ' Button7
        ' 
        Button7.Location = New Point(312, 227)
        Button7.Margin = New Padding(3, 2, 3, 2)
        Button7.Name = "Button7"
        Button7.Size = New Size(82, 22)
        Button7.TabIndex = 9
        Button7.Text = "Start"
        Button7.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(321, 55)
        Label1.Name = "Label1"
        Label1.Size = New Size(35, 15)
        Label1.TabIndex = 11
        Label1.Text = "PORT"
        ' 
        ' lbllampstatus
        ' 
        lbllampstatus.AutoSize = True
        lbllampstatus.Location = New Point(77, 202)
        lbllampstatus.Name = "lbllampstatus"
        lbllampstatus.Size = New Size(117, 15)
        lbllampstatus.TabIndex = 13
        lbllampstatus.Text = "INDIKATOR LAMPU 1"
        ' 
        ' lbllampstatus2
        ' 
        lbllampstatus2.AutoSize = True
        lbllampstatus2.Location = New Point(335, 202)
        lbllampstatus2.Name = "lbllampstatus2"
        lbllampstatus2.Size = New Size(117, 15)
        lbllampstatus2.TabIndex = 14
        lbllampstatus2.Text = "INDIKATOR LAMPU 2"
        ' 
        ' HMIG
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.LightCyan
        ClientSize = New Size(679, 341)
        Controls.Add(lbllampstatus2)
        Controls.Add(lbllampstatus)
        Controls.Add(Label1)
        Controls.Add(Button6)
        Controls.Add(Button7)
        Controls.Add(btnClose)
        Controls.Add(stp)
        Controls.Add(btnstart)
        Controls.Add(txtPort)
        Controls.Add(txtIpAddress)
        Controls.Add(IPPLC)
        Controls.Add(btnDisconnect)
        Controls.Add(btnConnect)
        Margin = New Padding(3, 2, 3, 2)
        Name = "HMIG"
        Text = "HMI"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents btnConnect As Button
    Friend WithEvents btnDisconnect As Button
    Friend WithEvents IPPLC As Label
    Friend WithEvents txtIpAddress As TextBox
    Friend WithEvents txtPort As TextBox
    Friend WithEvents stp As Button
    Friend WithEvents btnstart As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents lbllampstatus As Label
    Friend WithEvents lbllampstatus2 As Label

End Class
