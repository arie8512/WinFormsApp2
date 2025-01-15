Imports System.Net.Sockets
Imports Modbus.Device
Imports Microsoft.Office.Interop.Excel
Imports System.Runtime.InteropServices

Public Class HMIG
    'MODBUS 
    Private client As ModbusIpMaster
    Private plcIpAddress As String = ""
    Private plcPort As Integer = 502 ' Default port Modbus TCP

    'DATABASE
    Private xlApp As Application = Nothing
    Private xlWorkBook As Workbook = Nothing
    Private xlWorkSheet As Worksheet = Nothing

    ' Lokasi file log Excel
    Private filePath As String = "D:\DatabaseDCS.xlsx"

    '''''''''''''''''''''''''''''''''''''''''''''

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Membuat objek Excel hanya sekali
        xlApp = New Application()
        xlApp.Visible = True
        xlApp.DisplayAlerts = False ' Menonaktifkan notifikasi konfirmasi

        ' Membuka atau membuat file Excel
        If System.IO.File.Exists(filePath) Then
            xlWorkBook = xlApp.Workbooks.Open(filePath)
        Else
            xlWorkBook = xlApp.Workbooks.Add()
            xlWorkSheet = xlWorkBook.Sheets(1)
            xlWorkSheet.Name = "ActivityLog"

            ' Tambahkan header
            xlWorkSheet.Cells(1, 1).Value = "ActivityTime"
            xlWorkSheet.Cells(1, 2).Value = "ActivityType"
            xlWorkSheet.Cells(1, 3).Value = "Description"
            xlWorkSheet.Cells(1, 4).Value = "IP Address"
            xlWorkSheet.Cells(1, 5).Value = "Port"

            ' Simpan file baru
            xlWorkBook.SaveAs(filePath)
        End If

        ' Log aktivitas saat aplikasi dimulai
        LogActivity("START", "Aplikasi dimulai.", "", "")
    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''

    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        Try
            ' Mendapatkan IP dan port dari TextBox
            plcIpAddress = txtIpAddress.Text
            Dim portInput As String = txtPort.Text

            ' Validasi alamat IP dan port
            If String.IsNullOrWhiteSpace(plcIpAddress) Then
                LogActivity("CONNECT", "Alamat IP tidak diisi.", "", "")
                MessageBox.Show("Alamat IP tidak boleh kosong.")
                Return
            End If

            If Not Integer.TryParse(portInput, plcPort) Then
                LogActivity("CONNECT", "Port tidak valid. Menggunakan port default 502.", plcIpAddress, "502")
                MessageBox.Show("Port yang dimasukkan tidak valid. Menggunakan port default 502.")
                plcPort = 502 ' Default port
            Else
                LogActivity("CONNECT", "Port valid.", plcIpAddress, plcPort.ToString())
            End If

            ' Koneksi ke PLC
            Dim tcpClient As New TcpClient(plcIpAddress, plcPort)
            client = ModbusIpMaster.CreateIp(tcpClient)


            ' Mencatat aktivitas koneksi
            LogActivity("CONNECT", "Terhubung ke PLC dengan IP: " & plcIpAddress & " dan Port: " & plcPort.ToString(), plcIpAddress, plcPort.ToString())
            MessageBox.Show("Terhubung ke PLC dengan IP: " & plcIpAddress & " dan Port: " & plcPort.ToString())
        Catch ex As Exception
            ' Jika ada error saat koneksi
            LogActivity("CONNECT", "Terjadi kesalahan saat menghubungkan ke PLC: " & ex.Message, plcIpAddress, plcPort.ToString())
            MessageBox.Show("Terjadi kesalahan saat menghubungkan ke PLC: " & ex.Message)
        End Try
    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''

    ' LogActivity hanya akan menulis data ke Excel yang sudah terbuka
    Private Sub LogActivity(ActivityType As String, Description As String, ip As String, port As String)
        Try
            ' Pilih sheet "ActivityLog"
            xlWorkSheet = xlWorkBook.Sheets("ActivityLog")

            ' Cari baris kosong berikutnya
            Dim row As Integer = xlWorkSheet.Cells(xlWorkSheet.Rows.Count, 1).End(XlDirection.xlUp).Row + 1

            ' Tambahkan data log baru
            xlWorkSheet.Cells(row, 1).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            xlWorkSheet.Cells(row, 2).Value = ActivityType
            xlWorkSheet.Cells(row, 3).Value = Description
            xlWorkSheet.Cells(row, 4).Value = If(String.IsNullOrEmpty(ip), "N/A", ip) ' Mencatat IP yang dimasukkan
            xlWorkSheet.Cells(row, 5).Value = If(String.IsNullOrEmpty(port), "N/A", port) ' Mencatat Port yang dimasukkan

            ' Simpan perubahan tanpa menimpa file
            xlWorkBook.Save()

        Catch ex As Exception
            ' Tangani kesalahan
            MessageBox.Show("Terjadi kesalahan: " & ex.Message)
        End Try
    End Sub
    '''''''''''''''''''''''''''''''''''''''''''''
    Private Sub UpdateLampStatus()
        Try
            ' Membaca status lampu dari PLC (dari register 0 dan 1)
            Dim lampStatus As UShort() = client.ReadHoldingRegisters(0, 2) ' Membaca 2 register

            ' Memastikan ada nilai yang dibaca
            If lampStatus.Length >= 2 Then
                ' Lampu 1
                If lampStatus(0) = 1 Then
                    lbllampstatus.Text = "Lampu 1: ON"
                    lbllampstatus.BackColor = Color.Green
                Else
                    lbllampstatus.Text = "Lampu 1: OFF"
                    lbllampstatus.BackColor = Color.Red
                End If

                ' Lampu 2
                If lampStatus(1) = 1 Then
                    lbllampstatus2.Text = "Lampu 2: ON"
                    lbllampstatus2.BackColor = Color.Green
                Else
                    lbllampstatus2.Text = "Lampu 2: OFF"
                    lbllampstatus2.BackColor = Color.Red
                End If
            Else
                MessageBox.Show("Data status lampu tidak lengkap atau tidak terbaca.")
            End If
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan saat membaca status lampu: " & ex.Message)
        End Try
    End Sub

    ' Handle Form Closing
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Log aktivitas saat aplikasi ditutup
        LogActivity("CLOSE", "Aplikasi ditutup.", "", "")
        CloseExcelAndApp()
    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''

    ' Close Excel and the application properly
    Private Sub CloseExcelAndApp()
        If xlWorkBook IsNot Nothing Then
            xlWorkBook.Close(False)
            Marshal.ReleaseComObject(xlWorkBook)
        End If
        If xlApp IsNot Nothing Then
            xlApp.Quit()
            Marshal.ReleaseComObject(xlApp)
        End If

        ' Menutup form utama, yang secara tidak langsung akan menutup aplikasi
        Me.Close()

        ' Pastikan aplikasi keluar dengan bersih
        Environment.Exit(0)
    End Sub
    '''''''''''''''''''''''''''''''''''''''''''''

    ' Close Button Click Handler
    ' Fungsi untuk menutup aplikasi dan Excel
    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            ' Log aktivitas aplikasi ditutup
            LogActivity("CLOSE", "Aplikasi ditutup.", "", "")

            ' Menutup Excel dengan melepaskan objek yang digunakan
            If xlWorkBook IsNot Nothing Then
                xlWorkBook.Close(False)  ' Menutup workbook tanpa menyimpan
                Marshal.ReleaseComObject(xlWorkBook)  ' Membebaskan objek workbook
            End If

            If xlApp IsNot Nothing Then
                xlApp.Quit()  ' Menutup aplikasi Excel
                Marshal.ReleaseComObject(xlApp)  ' Membebaskan objek aplikasi
            End If

            ' Menutup aplikasi VB secara langsung
            Environment.Exit(0)  ' Menutup seluruh aplikasi
        Catch ex As Exception
            MessageBox.Show("Error while closing the application: " & ex.Message)
        End Try
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles lbllampstatus2.Click

    End Sub

    Private Sub btnstart_Click_1(sender As Object, e As EventArgs) Handles btnstart.Click
        Try
            ' Menulis ke coil %MX0.1 (alamat 1)
            client.WriteSingleRegister(0, 1) ' Menghidupkan coil %MX0.1

            ' Update status lampu setelah tombol ditekan
            UpdateLampStatus()

            'Update Log Tombol Start
            LogActivity("START 1", "Lampu 1 dinyalakan.", "", "")
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan saat mengirim sinyal: " & ex.Message)
        End Try
    End Sub

    Private Sub stp_Click_1(sender As Object, e As EventArgs) Handles stp.Click
        Try
            ' Mengirim sinyal untuk tombol Stop (set register 0 menjadi 0)
            client.WriteSingleRegister(0, 0) ' Menghidupkan tombol Stop

            ' Update status lampu setelah tombol ditekan
            UpdateLampStatus()

            'Update Log Tombol Stop
            LogActivity("STOP 1", "Lampu 1 dimatikan.", "", "")
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan saat mengirim sinyal: " & ex.Message)
        End Try
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Try
            ' Menulis ke coil %MX0.1 (alamat 1)
            client.WriteSingleRegister(1, 1) ' Menghidupkan coil %MX0.1

            ' Update status lampu setelah tombol ditekan
            UpdateLampStatus()

            'Update Log Tombol Start
            LogActivity("START 2", "Lampu 2 dinyalakan.", "", "")
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan saat mengirim sinyal: " & ex.Message)
        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            ' Mengirim sinyal untuk tombol Stop (set register 0 menjadi 0)
            client.WriteSingleRegister(1, 0) ' Menghidupkan tombol Stop

            ' Update status lampu setelah tombol ditekan
            UpdateLampStatus()

            'Update Log Tombol Stop
            LogActivity("STOP 2", "Lampu 2 dimatikan.", "", "")
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan saat mengirim sinyal: " & ex.Message)
        End Try
    End Sub

    Private Sub btnDisconnect_Click(sender As Object, e As EventArgs) Handles btnDisconnect.Click

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles lbllampstatus.Click

    End Sub

    Private Sub IPPLC_Click(sender As Object, e As EventArgs) Handles IPPLC.Click

    End Sub
End Class