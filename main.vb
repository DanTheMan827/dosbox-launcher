Imports System.Security.Principal
Imports System.Windows.Forms
Imports System.IO
Imports Microsoft.Win32
Module main
    Private configPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & System.IO.Path.DirectorySeparatorChar & "DOSBox Launcher"
    Private ini As IniFile
    Private baseDir As String = System.AppDomain.CurrentDomain.BaseDirectory
    Function changeDosboxPath() As Boolean
        MsgBox("Please choose where DOSBox.exe is located.", MsgBoxStyle.Information)
        Using diaOpen As New OpenFileDialog
            diaOpen.Filter = "DOSBox.exe|DOSBox.exe|All Files|*.*"
            diaOpen.Multiselect = False
            If diaOpen.ShowDialog = DialogResult.OK Then
                ini.WriteString("main", "dosbox", diaOpen.FileName)
                Return True
            End If
        End Using
        Return False
    End Function
    Function getDefaultConfigFile() As String
        Dim defaultConfigFile As String = configPath & Path.DirectorySeparatorChar & "default.dosbox"
        If File.Exists(defaultConfigFile) Then
            Return File.ReadAllText(defaultConfigFile)
        Else
            Dim oStream As System.IO.Stream
            Dim oAssembly As System.Reflection.Assembly



            'open the executing assembly
            oAssembly = System.Reflection.Assembly.LoadFrom(Application.ExecutablePath)

            'create stream for the config file
            oStream = oAssembly.GetManifestResourceStream("DOSBox_Launcher.default.config")

            'return the config file
            Using reader As New StreamReader(oStream)
                Dim defaultConfigFileData As String = reader.ReadToEnd
                File.WriteAllText(defaultConfigFile, defaultConfigFileData)

                Return defaultConfigFileData
            End Using
        End If

    End Function
    Function isElevated() As Boolean
        Dim identity = WindowsIdentity.GetCurrent()
        Dim principal = New WindowsPrincipal(identity)
        Return principal.IsInRole(WindowsBuiltInRole.Administrator)
    End Function

    Sub selfExec(arguments As String, ByVal Optional elevate As Boolean = True)
        Dim proc As New ProcessStartInfo
        proc.UseShellExecute = True
        proc.WorkingDirectory = Environment.CurrentDirectory
        proc.FileName = Application.ExecutablePath
        proc.Arguments = arguments

        If elevate Then
            proc.Verb = "runas"
        End If

        Process.Start(proc)

        End  ' Quit itself
    End Sub

    Sub Install()
        If Not isElevated() Then
            Try
                selfExec("-install")
            Catch ex As Exception
                Return
            End Try
        End If
        Registry.SetValue("HKEY_CLASSES_ROOT\dosboxlauncherfile", "", "DOSBox Launcher Configuration File")
        Registry.SetValue("HKEY_CLASSES_ROOT\dosboxlauncherfile\DefaultIcon", "", Application.ExecutablePath & ",0")
        Registry.SetValue("HKEY_CLASSES_ROOT\dosboxlauncherfile\shell", "", "open,Edit")
        Registry.SetValue("HKEY_CLASSES_ROOT\dosboxlauncherfile\shell\Edit", "", "Edit")
        Registry.SetValue("HKEY_CLASSES_ROOT\dosboxlauncherfile\shell\Edit\command", "", """" & Application.ExecutablePath & """ -edit ""%1""")
        Registry.SetValue("HKEY_CLASSES_ROOT\dosboxlauncherfile\shell\open", "", "&Open")
        Registry.SetValue("HKEY_CLASSES_ROOT\dosboxlauncherfile\shell\open\command", "", """" & Application.ExecutablePath & """ ""%1""")
        Registry.SetValue("HKEY_CLASSES_ROOT\.dosbox\dosboxlauncherfile\ShellNew", "Command", """" & Application.ExecutablePath & """ -new ""%1""")
        Registry.SetValue("HKEY_CLASSES_ROOT\.dosbox", "", "dosboxlauncherfile")

        Dim DOSBoxPath As String = ini.GetString("main", "dosbox", baseDir & "dosbox.exe")

        If Not File.Exists(DOSBoxPath) Then
            changeDosboxPath()
        End If

    End Sub

    Sub Uninstall()
        If Not isElevated() Then
            Try
                selfExec("-uninstall")
            Catch ex As Exception
                Return
            End Try
        End If

        Win32.AllocConsole()
        If Directory.Exists(configPath) Then
            Do While True
                Console.Clear()
                Console.WriteLine("Do you want to remove the application settings? (Y/N)")
                Select Case Console.ReadKey().KeyChar.ToString.ToLower
                    Case "y"
                        Directory.Delete(configPath, True)
                        Exit Do
                    Case "n"
                        Exit Do
                End Select
            Loop
        End If


        Registry.ClassesRoot.DeleteSubKeyTree("dosboxlauncherfile")
        Registry.ClassesRoot.DeleteSubKeyTree(".dosbox", True)




    End Sub
    Sub Main()
        Try
            Dim CommandLineArgs As System.Collections.ObjectModel.ReadOnlyCollection(Of String) = My.Application.CommandLineArgs

            If Not Directory.Exists(configPath) Then
                Directory.CreateDirectory(configPath)
            End If

            ini = New IniFile(configPath & Path.DirectorySeparatorChar & "settings.ini")

            If CommandLineArgs.Count > 0 Then
                Dim startInfo As New ProcessStartInfo
                Select Case CommandLineArgs(0)
                    Case "-install"
                        Install()

                    Case "-uninstall"
                        Uninstall()

                    Case "-choosePath"
                        changeDosboxPath()

                    Case "-edit"
                        With startInfo
                            .FileName = "NOTEPAD.EXE"
                            .Arguments = """" & CommandLineArgs(1) & """"
                        End With
                        Process.Start(startInfo)

                    Case "-new"
                        With New frmNew
                            .strNewFileName = CommandLineArgs(1)
                            .strTemplate = getDefaultConfigFile()
                            .ShowDialog()
                        End With

                    Case Else

                        Dim DOSBoxPath As String = ini.GetString("main", "dosbox", baseDir & "dosbox.exe")

                        If Not File.Exists(DOSBoxPath) Then
                            If changeDosboxPath() = False Then
                                Exit Sub
                            Else
                                DOSBoxPath = ini.GetString("main", "dosbox", "")
                            End If
                        End If

                        If File.Exists(CommandLineArgs(0)) Then
                            Dim oConfPath As New FileInfo(CommandLineArgs(0))

                            With startInfo
                                .FileName = DOSBoxPath
                                .WorkingDirectory = oConfPath.Directory.FullName

                                .Arguments = "-conf """ & oConfPath.FullName & """ -noconsole"
                            End With
                            Process.Start(startInfo)
                        End If

                End Select
            Else
                Win32.AllocConsole()

                Do While True
                    Console.Clear()

                    Console.WriteLine("You can do one of the following:    ")
                    Console.WriteLine()
                    Console.WriteLine("1: Associate .dosbox files with this app and add a entry to the ""New"" menu")
                    Console.WriteLine("2: Remove the file association (uninstall)")
                    Console.WriteLine("3: Change the location of DOSBox.exe")
                    Console.WriteLine("4: Exit")
                    Console.WriteLine()
                    Console.WriteLine("Choose an option (1-4)")
                    Select Case Console.ReadKey().KeyChar
                        Case "1"
                            Install()

                        Case "2"
                            Uninstall()

                        Case "3"
                            selfExec("-choosePath", False)

                        Case Else
                            End
                    End Select
                Loop

            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message.ToString)
        End Try
    End Sub

End Module
Public Class Win32
    <System.Runtime.InteropServices.DllImport("kernel32.dll")> Public Shared Function AllocConsole() As Boolean

    End Function
    <System.Runtime.InteropServices.DllImport("kernel32.dll")> Public Shared Function FreeConsole() As Boolean

    End Function

End Class