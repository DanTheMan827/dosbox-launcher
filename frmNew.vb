Public Class frmNew
    Public strNewFileName As String
    Private strNewFilePath As String
    Private strNewFileBaseName As String
    Public strTemplate As String
    Private invalidPathChars As Char() = System.IO.Path.InvalidPathChars

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim oFileInfo As New System.IO.FileInfo(strNewFileName)
        txtLauncherName.Text = oFileInfo.Name.Substring(0, oFileInfo.Name.LastIndexOf("."))
        strNewFilePath = oFileInfo.Directory.FullName & "\"
    End Sub


    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub txtFilename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFilename.Click
        With OpenFileDialog1
            .InitialDirectory = strNewFilePath
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                If LCase(.FileName).IndexOf(LCase(strNewFilePath)) = 0 Then
                    txtFilename.Text = .FileName.Substring(strNewFilePath.Length)
                    If .SafeFileName.LastIndexOf(".") > 0 Then

                        txtLauncherName.Text = .SafeFileName.Substring(0, .SafeFileName.LastIndexOf("."))
                    Else
                        If .SafeFileName.Length > 0 Then
                            txtLauncherName.Text = .SafeFileName
                        End If
                    End If
                Else
                    MsgBox("The file must be within the folder that the configuration file was created in.", MsgBoxStyle.Exclamation)
                End If
            End If
        End With
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim strPathInDosbox As String = ""
        If txtFilename.Text.Length = 0 Then
            MsgBox("Select a File to Launch")
            Return
        End If
        If txtLauncherName.Text.Length = 0 Then
            MsgBox("Enter A Launcher Name")
            Return
        End If

        Dim strCD As String

        If InStr(txtFilename.Text, "\") Then
            Dim oFileInfo As New System.IO.FileInfo(strNewFilePath & txtFilename.Text)
            strCD = oFileInfo.Directory.FullName.Substring(strNewFilePath.Length)
            strTemplate = strTemplate & vbCrLf & "cd c:\" & strCD
            strTemplate = strTemplate & vbCrLf
        End If
        If txtFilename.Text.Substring(txtFilename.Text.Length - 4).ToLower = ".bat" Then
                strTemplate = strTemplate & "call "
            End If

            If InStr(txtFilename.Text, "\") Then
            strTemplate = strTemplate & txtFilename.Text.Substring(strCD.Length + 1)
        Else
            strTemplate = strTemplate & txtFilename.Text
        End If
            If chkExit.Checked Then
            strTemplate = strTemplate & vbCrLf & "exit"
        End If

        Dim strFinalName As String = strNewFilePath & txtLauncherName.Text & ".dosbox"
        If System.IO.File.Exists(strFinalName) Then
            If System.Windows.Forms.MessageBox.Show("The file " & strFinalName & " already exists, do you want to overwrite?", "File Exists", Windows.Forms.MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
                Return
            End If
        End If
        System.IO.File.WriteAllText(strFinalName, strTemplate)
        Me.Close()
    End Sub

    Private Sub txtLauncherName_TextChanged(sender As System.Windows.Forms.TextBox, e As EventArgs) Handles txtLauncherName.TextChanged
        For Each sChar As String In invalidPathChars
            If InStr(sender.Text, sChar) Then
                sender.Text = Replace(sender.Text, sChar, "")
            End If
        Next
    End Sub

    Private Sub txtFilename_TextChanged(sender As Object, e As EventArgs) Handles txtFilename.TextChanged

    End Sub
End Class