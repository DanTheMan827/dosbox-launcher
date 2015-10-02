using DOSBox_Launcher.Properties;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;

namespace DOSBox_Launcher
{
    internal static class Program
    {
        internal static string configPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DOSBOX Launcher");
        internal static IniFile ini;
        internal static string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Change the DOSBox.exe path in the configuration file.
        /// </summary>
        /// <returns>If it was successful.</returns>
        internal static bool changeDosboxPath()
        {
            MessageBox.Show("Please locate DOSBox.exe", "Locate DOSBox.exe", MessageBoxButtons.OK, MessageBoxIcon.Information);
            using (var diaOpen = new OpenFileDialog())
            {
                diaOpen.Filter = "DOSBox.exe|DOSBox.exe|All Files|*.*";
                diaOpen.Multiselect = false;
                if (diaOpen.ShowDialog() == DialogResult.OK)
                {
                    ini.WriteString("main", "dosbox", diaOpen.FileName);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets the default configuration file for dosbox.
        /// </summary>
        internal static string DefaultConfigFile
        {
            get
            {
                string defaultConfigFile = Path.Combine(configPath, "default.dosbox");
                if (File.Exists(defaultConfigFile))
                {
                    return File.ReadAllText(defaultConfigFile);
                }
                else
                {
                    File.WriteAllText(defaultConfigFile, Resources.DefaultConfig);
                    return Resources.DefaultConfig;
                }
            }
        }

        /// <summary>
        /// If the application is running in an elevated state.
        /// </summary>
        internal static bool IsElevated
        {
            get
            {
                var identity = WindowsIdentity.GetCurrent();
                var principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        /// <summary>
        /// Tells the application to spawn a copy of itself.
        /// </summary>
        /// <param name="arguments">Arguments to pass.</param>
        /// <param name="elevate">If the runas verb should be used for process elevation.</param>
        internal static void selfExec(string arguments, bool elevate = true)
        {
            var proc = new ProcessStartInfo();
            proc.UseShellExecute = true;
            proc.WorkingDirectory = Environment.CurrentDirectory;
            proc.FileName = Application.ExecutablePath;
            proc.Arguments = arguments;

            if (elevate)
                proc.Verb = "runas";

            Process.Start(proc);

            Process.GetCurrentProcess().Kill(); // kill the current process
        }

        /// <summary>
        /// Writes the .dosbox file association to the registry, creates option in the new context menu of explorer, and sets the DOSBox.exe path in the settings file.
        /// </summary>
        internal static void install()
        {
            if (!IsElevated)
            {
                try
                {
                    selfExec("-install");
                }
                catch
                {
                    return;
                }
            }
            Registry.SetValue("HKEY_CLASSES_ROOT\\dosboxlauncherfile", "", "DOSBox Launcher Configuration File");
            Registry.SetValue("HKEY_CLASSES_ROOT\\dosboxlauncherfile\\DefaultIcon", "", $"\"{Application.ExecutablePath}\",0");
            Registry.SetValue("HKEY_CLASSES_ROOT\\dosboxlauncherfile\\shell", "", "open,Edit");
            Registry.SetValue("HKEY_CLASSES_ROOT\\dosboxlauncherfile\\shell\\Edit", "", "Edit");
            Registry.SetValue("HKEY_CLASSES_ROOT\\dosboxlauncherfile\\shell\\Edit\\command", "", $"\"{Application.ExecutablePath}\" -edit \"%1\"");
            Registry.SetValue("HKEY_CLASSES_ROOT\\dosboxlauncherfile\\shell\\open", "", "&Open");
            Registry.SetValue("HKEY_CLASSES_ROOT\\dosboxlauncherfile\\shell\\open\\command", "", $"\"{Application.ExecutablePath}\" \"%1\"");
            Registry.SetValue("HKEY_CLASSES_ROOT\\.dosbox\\dosboxlauncherfile\\ShellNew", "Command", $"\"{Application.ExecutablePath}\" -new \"%1\"");
            Registry.SetValue("HKEY_CLASSES_ROOT\\.dosbox", "", "dosboxlauncherfile");

            string DOSBoxPath = ini.GetString("main", "dosbox", Path.Combine(baseDir, "dosbox.exe"));

            if (!File.Exists(DOSBoxPath))
            {
                changeDosboxPath();
            }
        }

        /// <summary>
        /// Removes all DOSBox Launcher registry keys, and optionally deletes the settings file.
        /// </summary>
        internal static void uninstall()
        {
            if (!IsElevated)
            {
                try
                {
                    selfExec("-uninstall");
                }
                catch
                {
                    return;
                }
            }

            if (Directory.Exists(configPath) && MessageBox.Show("Do you want to remove the application settings?", "Remove Settings?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Directory.Delete(configPath, true);
            }

            Registry.ClassesRoot.DeleteSubKeyTree("dosboxlauncherfile");
            Registry.ClassesRoot.DeleteSubKeyTree(".dosbox");
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (!Directory.Exists(configPath))
                Directory.CreateDirectory(configPath);

            ini = new IniFile(Path.Combine(configPath, "settings.ini"));

            if (args.Length > 0)
            {
                switch (args[0])
                {
                    case "-install":
                        install();
                        break;

                    case "-uninstall":
                        uninstall();
                        break;

                    case "-choosePath":
                        changeDosboxPath();
                        break;

                    case "-edit":
                        Process.Start(new ProcessStartInfo()
                        {
                            FileName = "NOTEPAD.EXE",
                            Arguments = $"\"{args[1]}\""
                        });
                        break;

                    case "-new":
                        (new frmNew()
                        {
                            NewFileName = args[1],
                            Template = DefaultConfigFile
                        }).ShowDialog();
                        break;

                    default:
                        string dosboxPath = ini.GetString("main", "dosbox", Path.Combine(baseDir, "dosbox.exe"));

                        if (!File.Exists(dosboxPath))
                        {
                            if (!changeDosboxPath())
                                return;

                            dosboxPath = ini.GetString("main", "dosbox", "");
                        }

                        if (File.Exists(args[0]))
                        {
                            var confInfo = new FileInfo(args[0]);

                            Process.Start(new ProcessStartInfo()
                            {
                                FileName = dosboxPath,
                                WorkingDirectory = confInfo.Directory.FullName,
                                Arguments = $"-conf \"{confInfo.FullName}\" -noconsole"
                            });
                        }
                        break;
                }
            }
            else
            {
                if (!IsElevated)
                {
                    selfExec(null, true);
                    return;
                }

                new frmMain().ShowDialog();
            }
        }
    }
}
