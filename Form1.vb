Public Class Form1
    ' Daftar username dan password sederhana
    Private userList As New Dictionary(Of String, String) From {
        {"kasimirus", "4212111044"},
        {"arie", "4212111048"},
        {"gresya", "4212111050"},
        {"destito", "4212111053"},
        {"galuh", "4212111046"},
        {"tiara", "4212111045"},
        {"bintang", "4212111049"},
        {"arnoldus", "4212111051"},
        {"christian", "4212111052"},
        {"muhajir", "4212111047"}
    }

    Private Sub Button111_Click(sender As Object, e As EventArgs) Handles Button111.Click
        Dim username As String = txtUsername.Text
        Dim password As String = txtPassword.Text

        ' Validasi username dan password
        If userList.ContainsKey(username) AndAlso userList(username) = password Then
            MessageBox.Show("Login berhasil!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ' Buka form kontrol HMI
            Dim controlForm As New HMIG()
            controlForm.Show()
            Me.Hide()
        Else
            MessageBox.Show("Username atau password salah!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class